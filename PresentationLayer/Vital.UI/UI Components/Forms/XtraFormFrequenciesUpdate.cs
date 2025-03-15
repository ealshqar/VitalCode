using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.FrequencyTests;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormFrequenciesUpdate : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private readonly ItemsManager _itemsManager;
        private List<FrequencyDirectory> _frequencyDirectories;
        private Item _rootFrequenciesFolderItem;
        private int _frequencyUpdateCount;
        private int _currentUpdateFoldersFilesCount;
        private string _startPath;
        private Thread _frequencyUpdateThread;

        #endregion

        #region Properties

        /// <summary>
        /// The starting target path to use for update
        /// </summary>
        public string StartPath
        {
            get
            {
                return _startPath;
            }
            set
            {
                _startPath = value;
            }
        }

        /// <summary>
        /// List of items added
        /// </summary>
        public List<string> AddedItems { get; set; }

        /// <summary>
        /// List of items updated
        /// </summary>
        public List<string> UpdatedItems { get; set; }

        /// <summary>
        /// Count of all folders and files found
        /// </summary>
        public int CurrentUpdateFoldersFilesCount
        {
            get
            {
                return _currentUpdateFoldersFilesCount;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraFormFrequenciesUpdate()
        {
            InitializeComponent();
            _itemsManager = new ItemsManager();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets progress and info
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="file"></param>
        private void SetUpdateProgressAndInfo(string folder, string file)
        {
            Invoke((MethodInvoker)delegate
            {
                progressBarControlUpdate.EditValue = (int)Math.Round((double)(100 * _frequencyUpdateCount) / _currentUpdateFoldersFilesCount);
                simpleLabelItemInfo.Text = "[" + _currentUpdateFoldersFilesCount + @"\" + _frequencyUpdateCount + "] - " + folder + @"\" + file + " ...";
            });
        }

        /// <summary>
        /// The logic to start the frequency update
        /// </summary>
        private void StartFrequencyUpdate()
        {
            UiHelperClass.ShowWaitingPanel("Reading frequencies folder files ...", true);

            var files = Directory.GetFiles(_startPath, StaticKeys.FrequencyFileExtensionFilter, SearchOption.AllDirectories).ToList();
            var directories = Directory.GetDirectories(_startPath, "*", SearchOption.AllDirectories).ToList();

            UiHelperClass.HideSplash();

            //Check there the target folder contains frequency files.
            if (files.Any())
            {
                var frequencyParentItem = _itemsManager.GetItems(new ItemsFilter { Key = StaticKeys.EnergyFrequencyGroupsParentItemKey }).FirstOrDefault();
                _frequencyDirectories = new List<FrequencyDirectory>();

                //Get root folder item which will be used during the update process, details are below inside logic
                _rootFrequenciesFolderItem = _itemsManager.GetItems(new ItemsFilter { Key = StaticKeys.RootFrequencyItemKey }).FirstOrDefault();

                //Set counts for the current update action
                _currentUpdateFoldersFilesCount = files.Count + directories.Count + 1;//Add one for the root directory
                _frequencyUpdateCount = 0;
                AddedItems = new List<string>();
                UpdatedItems = new List<string>();

                //Call logic to start frequency update starting with the root directory
                UpdateFrequencyFolder(StaticKeys.FrequenciesFolderName, _startPath, frequencyParentItem);

                //Close form using Invoke since we are calling Close from a thread different than the one running the form.
                Invoke((MethodInvoker)Close);
            }
            else
            {
                UiHelperClass.ShowError(StaticKeys.UpdateErrorOccured, StaticKeys.IncorrectFrequencyFolder);
            }
        }

        /// <summary>
        /// Checks a frequency folder for matching item and checks its subdirectories and sub files
        /// </summary>
        /// <param name="directoryName"></param>
        /// <param name="path"></param>
        /// <param name="currentDirectoryItem"></param>
        /// <param name="parentDirectoryItem"></param>
        private void UpdateFrequencyFolder(string directoryName, string path, Item currentDirectoryItem = null, Item parentDirectoryItem = null)
        {
            if (string.IsNullOrEmpty(path)) return;// Check path not empty
            if (!Directory.Exists(path)) return;   // Check directory exists

            //Add 1 to count the directory even if it wasn't processed and method exist becuase of the checks below becuase we still need to take down the
            //count to show the right progress
            _frequencyUpdateCount += 1;
            SetUpdateProgressAndInfo(directoryName, string.Empty);

            //Check that the directory wasn't checked before
            if (_frequencyDirectories.Any(d => d.DirectoryName == directoryName && d.Path == path)) return;

            //Get the files of .frq extension in the directory
            var files = Directory.GetFiles(path, StaticKeys.FrequencyFileExtensionFilter).ToList();

            //Check if the directory contains .frq files to add it, otherwise the directory will not be added as an item
            if (!files.Any()) return;

            //Add the current directory to the list of checked directories
            _frequencyDirectories.Add(new FrequencyDirectory { DirectoryName = directoryName, Path = path });

            //Handle current directory item
            //----------------------------------------------------------------------
            //Directory item doesn't exist
            if (currentDirectoryItem == null)
            {
                //Generate a parent item key string, this is only done if parent item is not null, parent item is null only for parent item and the key won't be needed in such case
                var parentItemKeyByParentDirectoryItem = parentDirectoryItem == null ? string.Empty : UiHelperClass.AlphaNumericOnly(parentDirectoryItem.Name);
                var fileKey = UiHelperClass.AlphaNumericOnly(directoryName);

                //Add new item for the director that wasn't found
                currentDirectoryItem = new Item
                {
                    Name = directoryName,
                    Key = StaticKeys.EnergyFrequencyGeneralParentItemKey + parentItemKeyByParentDirectoryItem + "_" + fileKey,
                    FullName = directoryName,
                    TypeLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Substance)),
                    GenderLookup = UiHelperClass.GetSingleLookupFromCacheByKey(ItemGender.ItemGenderBoth.ToString()),
                    ListTypeLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ListType, ListTypeEnum.SystemList)),
                    ItemSourceLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SourceType, SourceTypeEnum.SystemItem))
                };

                //Add relation with the parent item
                var newItemRelation = new ItemRelation
                {
                    RelationType = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.RelationType, RelationTypeEnum.None)),
                    Parent = parentDirectoryItem,
                    Child = currentDirectoryItem
                };

                currentDirectoryItem.Children = new BindingList<ItemRelation> { newItemRelation };

                _itemsManager.SaveItem(currentDirectoryItem);
                AddedItems.Add(directoryName + @"\");
            }

            //In the section below we load an item called "TrueRife Frequencies", this item doesn't exist as folder however we treat it as folder because we put in it
            //all the frequencies that exists in the root directory, this is to make sure those frequencies do not show in the NavGrid first screen becuase it will be too much data
            //specially when it is included along with the folders, this however cause us an issue because the folder doesn't exist and so there will be a conflict between the
            //folders structure and the items structure, to handle that, we load this item based on key and we add a flag to indicate when this item needs to be used, this item will be used
            //only when checking the parent main directory directly in the settings folder, this way all the frequencies in the root will be added under this folder and not under the
            //root parent item in the NavGrid
            var useRootItem = currentDirectoryItem.Key == StaticKeys.EnergyFrequencyGroupsParentItemKey;
            var existingFrequencyChildItems = _itemsManager.GetItemChildren(new SingleItemFilter { ItemId = useRootItem ? _rootFrequenciesFolderItem.Id : currentDirectoryItem.Id });

            //Handle .frq files in path
            //----------------------------------------------------------------------
            foreach (var file in files)
            {
                //Handle file as item
                var info = new FileInfo(file);
                var fileName = Path.GetFileNameWithoutExtension(info.Name).Trim();
                var fileItem = existingFrequencyChildItems.FirstOrDefault(i => i.Name == fileName);

                if (fileItem == null)
                {
                    var parentItemKeyByCurrentDirectoryItem = UiHelperClass.AlphaNumericOnly(useRootItem ? _rootFrequenciesFolderItem.Name : currentDirectoryItem.Name);
                    var fileKey = UiHelperClass.AlphaNumericOnly(fileName);

                    fileItem = new Item
                    {
                        Name = fileName,
                        Key = StaticKeys.EnergyFrequencyGeneralItemKey + parentItemKeyByCurrentDirectoryItem + "_" + fileKey,
                        FullName = fileName,
                        Description = File.ReadAllText(info.FullName),
                        TypeLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Substance)),
                        GenderLookup = UiHelperClass.GetSingleLookupFromCacheByKey(ItemGender.ItemGenderBoth.ToString()),
                        ListTypeLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ListType, ListTypeEnum.None)),
                        ItemSourceLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SourceType, SourceTypeEnum.SystemItem))
                    };

                    //Add relation with the parent item
                    var newItemRelation = new ItemRelation
                    {
                        RelationType = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.RelationType, RelationTypeEnum.None)),
                        Parent = useRootItem ? _rootFrequenciesFolderItem : currentDirectoryItem,
                        Child = fileItem
                    };
                    fileItem.Children = new BindingList<ItemRelation> { newItemRelation };

                    _itemsManager.SaveItem(fileItem);
                    AddedItems.Add(directoryName + @"\" + fileName);
                }
                else if (File.GetLastWriteTime(file) > fileItem.UpdatedDateTime)
                {
                    //Check the last modified date on the file and update the item if the file was updated.
                    var fileDescription = File.ReadAllText(info.FullName);
                    if (fileDescription != fileItem.Description)
                    {
                        //Load item from DB to prevent issues with saving since the item that was loaded before was loaded as child and didn't have all details in it
                        fileItem = _itemsManager.GetItemById(new SingleItemFilter { ItemId = fileItem.Id });

                        //Set the new description
                        fileItem.Description = fileDescription;

                        _itemsManager.SaveItem(fileItem);
                        UpdatedItems.Add(directoryName + @"\" + fileName);
                    }
                }
                SetUpdateProgressAndInfo(directoryName, fileName);
                _frequencyUpdateCount += 1;
            }

            //Handle directories in path
            //----------------------------------------------------------------------
            var subDirectories = Directory.GetDirectories(path).ToList();

            //Get the frequency child items but this time using the current directory item Id without checking for root because this time we want to get the items of the parent
            //without checking for the item created to hold root frequencies because the folders should be added to the root item so this is where they will be unlike the frequency
            //files on the root which shouldn't be added to the root, so we get the items again using the root directory item to prevent using the wrong list of items to check for the
            //existance of the folders as items in Vital.
            existingFrequencyChildItems = _itemsManager.GetItemChildren(new SingleItemFilter { ItemId = currentDirectoryItem.Id });

            foreach (var subDirectoryPath in subDirectories)
            {
                var subDirectoryName = new DirectoryInfo(subDirectoryPath).Name.Trim();

                //Search for the directory within the current parent childs
                var foundSubDirectoryItem = existingFrequencyChildItems.FirstOrDefault(i => i.Name == subDirectoryName);

                //Use recursion to handle the directories found
                UpdateFrequencyFolder(subDirectoryName, subDirectoryPath, foundSubDirectoryItem, currentDirectoryItem);
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handles the form loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormUpdate_Load(object sender, EventArgs e)
        {
            _frequencyUpdateThread = new Thread(StartFrequencyUpdate);
            _frequencyUpdateThread.Start();
        }

        #endregion        
    }
}