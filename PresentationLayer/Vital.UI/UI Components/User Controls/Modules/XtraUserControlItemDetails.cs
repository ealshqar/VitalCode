using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGauges.Core.Resources;
using DevExpress.XtraLayout.Utils;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.BaseForms;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlItemDetails : DevExpress.XtraEditors.XtraUserControl
    {
        #region Fields

        private BindingList<Lookup> _multipleImageOptions;
        private BindingList<Lookup> _multipleImageOptionsFiltered;
        private BindingList<Lookup> _multipleImageViewMode;

        private GalleryItemGroup _galleryItemGroupMultipleImages;

        private MultipleImageModes _lastMultipleImageMode;
        private int _lastNavigatorIndex;
        private int _yesLookupId;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraUserControlItemDetails()
        {
            InitializeComponent();
            SetupGalleryControl();
        }
        
        #endregion

        #region Events

        #region Custom Events

        /// <summary>
        /// Handel the change description request.
        /// </summary>
        public event EditDescriptionEventHandler EditDescription;

        #endregion

        #region EventInvokers

        /// <summary>
        /// Invoke Change description event.
        /// </summary>
        private void InvokeChangeDescription()
        {
            if (EditDescription == null)
                return;

            EditDescription(this);
        }

        #endregion

        #region EventHandlerDelegates

        /// <summary>
        /// Delegate for ChangeDescriptionEventHandler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public delegate void EditDescriptionEventHandler(XtraUserControlItemDetails sender);

        #endregion

        #endregion

        #region Properties

        #region Name & Description

        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        public string ItemName
        {
            get
            {
                return layoutControlGroupItemName.Text;
            }
            set
            {
                layoutControlGroupItemName.Text = string.IsNullOrEmpty(value) ? " " : value;
            }
        }

        /// <summary>
        /// Sets the description value.
        /// </summary>
        public string Description
        {
            set
            {
                memoEditDescription.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets Enabled for the Edit Description button.
        /// </summary>
        public bool EditDescriptionEnabled
        {
            get
            {
                return simpleButtonEditDescription.Enabled;
            }
            set
            {
                simpleButtonEditDescription.Enabled = value;
            }
        }

        /// <summary>
        /// Shows or Hides the Edit Description button.
        /// </summary>
        public bool EditDescriptionButtonHidden
        {
            get
            {
                return layoutControlItemDescriptionButton.Visibility != LayoutVisibility.Always;
            }
            set
            {
                layoutControlItemDescriptionButton.Visibility = value ? LayoutVisibility.Never : LayoutVisibility.Always;
            }
        }

        #endregion

        #region Flags

        /// <summary>
        /// Current Image path
        /// </summary>
        private string CurrentImagePath { get; set; }

        /// <summary>
        /// LastActiveImagePath
        /// </summary>
        private string LastActiveImagePath { get; set; }

        /// <summary>
        /// LastActiveImage
        /// </summary>
        private Image LastActiveImage { get; set; }

        /// <summary>
        /// LastImageViewOption
        /// </summary>
        private MultipleImageViewOptions LastImageViewOption { get; set; }

        #endregion

        #region Current Multi Image Data

        /// <summary>
        /// Current selected item for single image cases
        /// </summary>
        private Item SelectedItem { get; set; }

        /// <summary>
        /// Current Item
        /// </summary>
        private Item CurrentItem { get; set; }

        /// <summary>
        /// Current Parent Item
        /// </summary>
        private Item ItemParent { get; set; }

        /// <summary>
        /// Current Issue Item
        /// </summary>
        private Item Issue { get; set; }

        /// <summary>
        /// List of items for support of multiple images
        /// </summary>
        private BindingList<Item> MultipleItems { get; set; }

        /// <summary>
        /// List of items selected items as set by caller
        /// </summary>
        private BindingList<Item> SelectedItems { get; set; }

        /// <summary>
        /// List of items top items as set by caller
        /// </summary>
        private BindingList<Item> TopItems { get; set; }

        /// <summary>
        /// List of items bottom items as set by caller
        /// </summary>
        private BindingList<Item> BottomItems { get; set; }

        /// <summary>
        /// TopItemsHightlighted as set by caller
        /// </summary>
        private bool TopItemsHightlighted { get; set; }

        #endregion

        #region User Settings

        /// <summary>
        /// Auto Zoom Option
        /// </summary>
        public bool UseAutoZoom
        {
            get
            {
                return checkEditUseAutoZoom.Checked;
            }
            set
            {
                checkEditUseAutoZoom.Checked = value;
            }
        }

        /// <summary>
        /// Gallery Control Zoom Level
        /// </summary>
        public int ZoomLevel
        {
            get
            {
                return trackBarControlSelectionGalleryZoom.Value;
            }
            set
            {
                SetGalleryZoomLevel(value);
            }
        }

        /// <summary>
        /// Property for navigator position
        /// </summary>
        private int NavigatorPosition
        {
            get
            {
                return trackBarControlNavigator.Value - 1;
            }

            set
            {
                trackBarControlNavigator.Value = value + 1;
            }
        }
        #endregion

        #region Other

        /// <summary>
        /// Flag to indicate if current image shouldn't change
        /// </summary>
        public bool KeepCurrentImage
        {
            get
            {
                return checkEditKeepCurrentImage.Checked;
            }
            set
            {
                checkEditKeepCurrentImage.Checked = value;
            }
        }

        /// <summary>
        /// Determines if image refresh request should be ignored
        /// </summary>
        public bool IgnoreImageRefresh { get; set; }

        /// <summary>
        /// Gets or sets the gender enum.
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// When set to true the system will not reference images using the application path and will use the image path as it comes with the item, this is required
        /// for custom cases when the image is loaded from somewhere outside the vital directory
        /// </summary>
        public bool UseImagePathOnly { get; set; }

        #endregion

        #endregion
        
        #region Setup

        /// <summary>
        /// Fill lookups for dropdowns
        /// </summary>
        public void FillLookups()
        {
            _multipleImageOptions = UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.MultipleImageOptions));
            lookUpEditImageSelectionMode.Properties.DataSource = _multipleImageOptions;

            _multipleImageViewMode = UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.MultipleImageModes));
            lookUpEditMultipleImageViewMode.Properties.DataSource = _multipleImageViewMode;

            var lookupsManager = new LookupsManager();

            var yesLookup = lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.Yes)).FirstOrDefault();
            _yesLookupId = yesLookup != null ? yesLookup.Id : 0;

            CurrentImagePath = string.Empty;

            _multipleImageOptionsFiltered = new BindingList<Lookup>();
        }

        /// <summary>
        /// Sets up the gallery control
        /// </summary>
        private void SetupGalleryControl()
        {
            galleryControlMultipleImages.Gallery.CustomDrawItemImage += Gallery_CustomDrawItemImage;
            _galleryItemGroupMultipleImages = new GalleryItemGroup { Caption = "Images" };
            galleryControlMultipleImages.Gallery.Groups.Add(_galleryItemGroupMultipleImages);
        }

        #endregion

        #region Logic

        #region Checks

        /// <summary>
        /// Checks if an item has an image
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool ItemHasImage(Item item)
        {
            if (item == null || item.ItemDetail == null || item.ItemDetail.Image == null) return false;

            var path = UseImagePathOnly ? item.ItemDetail.Image.Path : UiHelperClass.ImagesFolderPath + item.ItemDetail.Image.Path;

            return File.Exists(path);
        }

        /// <summary>
        /// Gets matching image option based on predicate
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private Lookup GetMatchingImageOption(Func<Lookup, bool> condition)
        {
            return _multipleImageOptionsFiltered == null? null:_multipleImageOptionsFiltered.FirstOrDefault(condition);
        }

        /// <summary>
        /// Get selection case for items
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private MultiImageSelectionCases GetItemsSelectionCase(BindingList<Item> items)
        {
            var selectionCase = MultiImageSelectionCases.None;

            if (items == null) return selectionCase;

            if (!items.Any())
            {
                selectionCase = MultiImageSelectionCases.None;
            }
            else if (items.Count == 1)
            {
                var itemHasImage = items.Any(ItemHasImage);
                selectionCase = itemHasImage ? MultiImageSelectionCases.SingleWithImage : MultiImageSelectionCases.SingleNoImage;
            }
            else
            {
                var itemsWithImages = items.Where(ItemHasImage).ToBindingList();

                if (!itemsWithImages.Any())
                {
                    selectionCase = MultiImageSelectionCases.MultipleNone;
                }
                else if (itemsWithImages.Count == 1)
                {
                    selectionCase = MultiImageSelectionCases.MultipleSingleImage;
                }
                else
                {
                    var distinctImagesCount = itemsWithImages.Select(item => item.ItemDetail.Image.Path).Distinct().Count();

                    selectionCase = distinctImagesCount == 1 ? MultiImageSelectionCases.MultipleAllSame : MultiImageSelectionCases.MultipleMix;
                }
            }

            return selectionCase;
        }
        
        #endregion

        #region Name & Description

        /// <summary>
        /// Sets Name & Description depending on current selection case
        /// </summary>
        /// <param name="selectionCase"></param>
        private void SetNameAndDescription(MultiImageSelectionCases selectionCase)
        {
            switch (selectionCase)
            {
                case MultiImageSelectionCases.None:
                case MultiImageSelectionCases.MultipleAllSame:
                case MultiImageSelectionCases.MultipleSingleImage:
                case MultiImageSelectionCases.MultipleNone:
                case MultiImageSelectionCases.MultipleMix:
                    if (LastImageViewOption == MultipleImageViewOptions.MultipleGallery)
                    {
                        ResetNameAndDescription();    
                    }
                    break;
                case MultiImageSelectionCases.SingleWithImage:
                case MultiImageSelectionCases.SingleNoImage:
                    SetCurrentIteNameAndmDescription();
                    break;
            }
        }

        /// <summary>
        /// Set Name & Description depending on current item
        /// </summary>
        private void SetCurrentIteNameAndmDescription()
        {
            Description = CurrentItem.Description;
            ItemName = CurrentItem.Name;
            layoutControlGroupImage.Text = CurrentItem.Name;
            EditDescriptionEnabled = true;
        }

        /// <summary>
        /// Reset name and description
        /// </summary>
        private void ResetNameAndDescription()
        {
            Description = string.Empty;
            ItemName = string.Empty;
            layoutControlGroupImage.Text = " ";
            EditDescriptionEnabled = false;
        }

        #endregion

        #region Image Handling

        /// <summary>
        /// Sets Image for a signle item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="navigatorModeActive"></param>
        private void SetSingleItemImage(Item item, bool navigatorModeActive = false)
        {
            if (!navigatorModeActive)
            {
                SetMode(MultipleImageViewOptions.Single);
            }

            if (item == null)
            {
                Description = string.Empty;
                ItemName = string.Empty;
                EditDescriptionEnabled = false;

                SetDefaultImage();

                return;
            }

            if (item.Key == ItemKeys.TestMainIssue.ToString())
            {
                Description = string.Empty;
                ItemName = string.Empty;
                EditDescriptionEnabled = false;

                return;
            }
            var imagePath = string.Empty;

            if (ItemHasImage(item))
            {
                imagePath = item.ItemDetail.Image.Path;

            }
            else if (ItemHasImage(ItemParent))
            {
                imagePath = ItemParent.ItemDetail.Image.Path;
            }
            else if (ItemHasImage(Issue))
            {
                imagePath = Issue.ItemDetail.Image.Path;
            }
            else
            {
                SetDefaultImage();
            }

            if (imagePath != string.Empty)
            {
                try
                {
                    var path = UseImagePathOnly ? imagePath : UiHelperClass.ImagesFolderPath + imagePath;

                    SetImage(path, item.ItemDetail.X, item.ItemDetail.Y, item.ItemDetail.Image.OldImageBoxWidth, item.ItemDetail.Image.OldImageBoxHeight);
                }
                catch
                {
                    SetDefaultImage();
                }
            }
            else
            {
                SetDefaultImage();
            }
        }

        /// <summary>
        /// Set the image.
        /// </summary>
        /// <param name="imagePath">The Image Path.</param>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        /// <param name="oldImageBoxWidth">The old image box width.</param>
        /// <param name="oldImageBoxHeight">The old image box height.</param>
        private void SetImage(string imagePath, int x, int y, int oldImageBoxWidth, int oldImageBoxHeight)
        {
            try
            {
                //Avoid loading and setting the image when its the current image, remove duplication loaded.
                if (CurrentImagePath != null && CurrentImagePath.Equals(imagePath) && x == 0 && y == 0)
                    return;

                //To speedup the processing for images, cache the last image before set the default image and use its the target for this set.
                //This is will be perfect for auto steps mode when most items in list have same image.
                if (LastActiveImagePath != null && LastActiveImagePath.Equals(imagePath) && LastActiveImage != null && x == 0 && y == 0)
                {
                    lock (pictureEditItemPicture)
                    {
                        pictureEditItemPicture.EditValue = LastActiveImage;
                    }

                    CurrentImagePath = imagePath;

                    return;
                }

                lock (pictureEditItemPicture)
                {
                    Image image = null;

                    if (File.Exists(imagePath))
                    {
                        image = UiHelperClass.LoadImgeWithoutLocking(imagePath);
                    }

                    if (image != null)
                    {

                        if (x != 0 || y != 0)
                        {
                            var itemImage = image;

                            var itemBitmap = new Bitmap(itemImage);

                            var graphics = Graphics.FromImage(itemBitmap);

                            Point pp = VitalBaseForm.TranslateZoomMousePosition(new Point(x, y), itemImage,
                                oldImageBoxWidth,
                                oldImageBoxHeight);

                            graphics.DrawEllipse(new Pen(Brushes.Black, 5), pp.X, pp.Y, 4, 4);
                            graphics.DrawEllipse(new Pen(Brushes.GreenYellow, 3), pp.X, pp.Y, 3, 3);

                            pictureEditItemPicture.EditValue = itemBitmap;
                        }
                        else
                        {
                            pictureEditItemPicture.EditValue = image;
                        }

                    }
                    else
                    {
                        SetDefaultImage();
                    }

                    CurrentImagePath = imagePath;

                }
            }
            catch
            {
                SetDefaultImage();
            }

        }

        /// <summary>
        /// Clears image and description
        /// </summary>
        public void ClearImageAndDetails()
        {
            if (!KeepCurrentImage)
            {
                CurrentImagePath = null;
                pictureEditItemPicture.EditValue = null;

                //KeepCurrentImage = false;
                Description = string.Empty;
                ItemName = string.Empty;
                EditDescriptionEnabled = false;

                SetDefaultImage();
                SetMode(MultipleImageViewOptions.Default);
            }
        }

        /// <summary>
        /// Set Default Image.
        /// </summary>
        private void SetDefaultImage()
        {
            lock (pictureEditItemPicture)
            {
                SetDefaultImageWorker();
            }
        }

        /// <summary>
        /// Set Default Image.
        /// </summary>
        private void SetDefaultImageWorker()
        {
            try
            {
                //To speedup the processing for images, save the last image before set the default image and use its the target for this set.
                //This is will be perfect for auto steps mode when most items in list have same image.
                LastActiveImagePath = CurrentImagePath;
                LastActiveImage = pictureEditItemPicture.EditValue as Image;

                CurrentImagePath = string.Empty;

                pictureEditItemPicture.EditValue = Gender == GenderEnum.Male ? Resources.Male : Resources.Female;
            }
            catch (Exception exception)
            {
                UiHelperClass.ShowError(StaticKeys.ImageErrorOccurred, exception);
            }
        }
        
        #endregion

        #region Single & Multiple Image Handling

        #region Setting User Control Data

        /// <summary>
        /// Sets Details for a single item
        /// </summary>
        /// <param name="item"></param>
        public void SetDetails(Item item)
        {
            SetDetails(new BindingList<Item> { item }, null, null, false, null, null);
        }

        /// <summary>
        /// Sets Details of current selection while testing
        /// </summary>
        /// <param name="selectedItems"></param>
        /// <param name="topItems"></param>
        /// <param name="bottomItems"></param>
        /// <param name="topItemsHightlighted"></param>
        /// <param name="parentItem"></param>
        /// <param name="issueItem"></param>
        public void SetDetails(BindingList<Item> selectedItems,
                               BindingList<Item> topItems,
                               BindingList<Item> bottomItems,
                               bool topItemsHightlighted,
                               Item parentItem,
                               Item issueItem)
        {
            if (IgnoreImageRefresh) return;

            if (selectedItems == null) return;

            SelectedItems = selectedItems.Take(50).ToBindingList();
            TopItems = topItems == null? null : topItems.Take(50).ToBindingList();
            BottomItems = bottomItems == null ? null : bottomItems.Take(50).ToBindingList();
            TopItemsHightlighted = topItemsHightlighted;
            Issue = issueItem;
            ItemParent = parentItem;
            _lastNavigatorIndex = 0;

            var itemsToShow = new BindingList<Item>();

            MultiImageSelectionCases selectionCase;

            var useParentImage = false;

            if (!SelectedItems.Any())
            {
                var oneItemHighlighted = (topItemsHightlighted && TopItems != null && TopItems.Count == 1 && ItemHasImage(TopItems.FirstOrDefault())) ||
                                         (!topItemsHightlighted && BottomItems != null && BottomItems.Count == 1 && ItemHasImage(BottomItems.FirstOrDefault()));

                if (!oneItemHighlighted && ItemParent != null && ItemParent.Properties.HasProperty(PropertiesEnum.UseParentImageOnSplitSwitch, _yesLookupId.ToString()))
                {
                    selectionCase = MultiImageSelectionCases.SingleWithImage;
                    useParentImage = true;

                    if (topItemsHightlighted && TopItems != null)
                    {
                        itemsToShow = TopItems;
                    }
                    else if (!topItemsHightlighted && BottomItems != null)
                    {
                        itemsToShow = BottomItems;
                    }
                }
                else
                {
                    if (topItemsHightlighted && TopItems != null)
                    {
                        selectionCase = GetItemsSelectionCase(TopItems);
                        itemsToShow = TopItems;
                    }
                    else if (!topItemsHightlighted && BottomItems != null)
                    {
                        selectionCase = GetItemsSelectionCase(BottomItems);
                        itemsToShow = BottomItems;
                    }
                    else
                    {
                        selectionCase = MultiImageSelectionCases.None;
                    }   
                }
            }
            else
            {
                selectionCase = GetItemsSelectionCase(SelectedItems);
                itemsToShow = SelectedItems;
            }

            if (!KeepCurrentImage)
            {
                switch (selectionCase)
                {
                    case MultiImageSelectionCases.None:
                        SetMode(MultipleImageViewOptions.Default);
                        SetDefaultImage();
                        break;
                    case MultiImageSelectionCases.SingleWithImage:
                    case MultiImageSelectionCases.SingleNoImage:
                        SelectedItem = itemsToShow.FirstOrDefault();
                        CurrentItem = useParentImage? ItemParent : itemsToShow.FirstOrDefault();
                        SetupMultiImageOptions(selectionCase,useParentImage? MultipleImageOptions.ParentImage : MultipleImageOptions.None);
                        SetSingleItemImage(useParentImage? ItemParent : CurrentItem);
                        break;
                    case MultiImageSelectionCases.MultipleAllSame:
                    case MultiImageSelectionCases.MultipleSingleImage:
                    case MultiImageSelectionCases.MultipleNone:
                    case MultiImageSelectionCases.MultipleMix:
                        MultipleItems = itemsToShow;
                        if (selectionCase == MultiImageSelectionCases.MultipleAllSame)
                        {
                            SelectedItem = itemsToShow.FirstOrDefault();
                            CurrentItem = itemsToShow.FirstOrDefault(ItemHasImage);
                            SetupMultiImageOptions(selectionCase);
                            SetSingleItemImage(CurrentItem);
                        }
                        else
                        {
                            SetupMultiImageOptions(selectionCase);
                            switch (_lastMultipleImageMode)
                            {
                                case MultipleImageModes.Gallery:
                                    SetMode(MultipleImageViewOptions.MultipleGallery);
                                    break;
                                case MultipleImageModes.Navigator:
                                    SetMultipleImageViewMode(MultipleImageModes.Navigator);
                                    break;
                            }
                        }
                        
                        trackBarControlNavigator.TabIndex = 0;
                        trackBarControlNavigator.Properties.Maximum = MultipleItems.Count;

                        _galleryItemGroupMultipleImages.Items.Clear();

                        foreach (var selectedItem in itemsToShow)
                        {
                            Image image = null;

                            if (ItemHasImage(selectedItem))
                            {
                                var path = UseImagePathOnly ? selectedItem.ItemDetail.Image.Path : UiHelperClass.ImagesFolderPath + selectedItem.ItemDetail.Image.Path;
                                
                                if (File.Exists(path))
                                {
                                    image = UiHelperClass.LoadImgeWithoutLocking(path);
                                }                               
                            }
                            
                            if (image == null)
                            {
                                if (selectedItem.TypeLookup == null)
                                {
                                    image = Resources.NoPictureAvailable;
                                }
                                else
                                {
                                    var itemType = EnumNameResolver.LookupAsEnum<ItemTypeEnum>(selectedItem.TypeLookup.Value);

                                    switch (itemType)
                                    {
                                        case ItemTypeEnum.Potency:
                                            image = Resources.Dilution256;
                                            break;
                                        case ItemTypeEnum.Product:
                                            image = Resources.Product256;
                                            break;
                                        case ItemTypeEnum.Bacteria:
                                            image = Resources.Bacteria256;
                                            break;
                                        default:
                                            image = Resources.NoPictureAvailable;
                                            break;
                                    }
                                }
                            }
                            var newItem = new GalleryItem(image, selectedItem.Name, string.Empty)
                            {
                                Tag = itemsToShow.IndexOf(selectedItem)
                            };

                            _galleryItemGroupMultipleImages.Items.Add(newItem);
                        }
                        CheckAutoZoom();

                        var selectedMode = _multipleImageViewMode.FirstOrDefault(l => l.Value == (_lastMultipleImageMode == MultipleImageModes.Navigator ? 
                                                                                MultipleImageModes.Navigator.ToString() : 
                                                                                MultipleImageModes.Gallery.ToString()));

                        if (selectedMode != null)
                        {
                            lookUpEditMultipleImageViewMode.EditValue = selectedMode.Id;
                        }
                        

                        break;
                }

                SetNameAndDescription(selectionCase);   
            }
        }

        #endregion

        #region Image Options Logic: Single, Parent, Issue, Multiple

        /// <summary>
        /// Sets the options for the selected image option
        /// </summary>
        /// <param name="selectedOption"></param>
        private void SetImageOption(MultipleImageOptions selectedOption)
        {
            switch (selectedOption)
            {
                case MultipleImageOptions.SingleImage:
                case MultipleImageOptions.ParentImage:
                case MultipleImageOptions.IssueImage:

                    switch (selectedOption)
                    {
                        case MultipleImageOptions.SingleImage:
                            CurrentItem = SelectedItem;
                            break;
                        case MultipleImageOptions.ParentImage:
                            CurrentItem = ItemParent;
                            break;
                        case MultipleImageOptions.IssueImage:
                            CurrentItem = Issue;
                            break;
                    }
                    SetSingleItemImage(CurrentItem);
                    SetCurrentIteNameAndmDescription();
                    layoutControlGroupSingleImage.Height = layoutControlGroupMultipleImages.Height;
                    break;
                case MultipleImageOptions.MultipleImages:
                    switch (_lastMultipleImageMode)
                    {
                        case MultipleImageModes.Gallery:
                            SetMode(MultipleImageViewOptions.MultipleGallery);
                            layoutControlGroupMultipleImages.Height = layoutControlGroupSingleImage.Height;
                            break;
                        case MultipleImageModes.Navigator:
                            SetMultipleImageViewMode(MultipleImageModes.Navigator);
                            layoutControlGroupSingleImage.Height = layoutControlGroupMultipleImages.Height;
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Setup the dropdowns based on the selection case
        /// </summary>
        /// <param name="selectionCase"></param>
        private void SetupMultiImageOptions(MultiImageSelectionCases selectionCase, MultipleImageOptions multipleImageOption = MultipleImageOptions.None)
        {
            var multipleImageOptionString = EnumNameResolver.GetEnumNameOrDescription(multipleImageOption);
            var singleImageString = EnumNameResolver.GetEnumNameOrDescription(MultipleImageOptions.SingleImage);
            var parentImageString = EnumNameResolver.GetEnumNameOrDescription(MultipleImageOptions.ParentImage);
            var issueImageString = EnumNameResolver.GetEnumNameOrDescription(MultipleImageOptions.IssueImage);
            var multipleImagesString = EnumNameResolver.GetEnumNameOrDescription(MultipleImageOptions.MultipleImages);

            var parentHasImage = ItemHasImage(ItemParent);
            var issueHasImage = ItemHasImage(Issue);
            var parentAndIssueAreSame = ItemParent != null && Issue != null && parentHasImage && issueHasImage && ItemParent.Id == Issue.Id;

            Func<Lookup, bool> multipleImageOptionPredicate = l => l.Value == multipleImageOptionString;
            Func<Lookup, bool> selectionImagePredicate = l => l.Value == singleImageString;
            Func<Lookup, bool> parentImagePredicate = l => l.Value == parentImageString && parentHasImage;
            Func<Lookup, bool> issueImagePredicate = l => l.Value == issueImageString && issueHasImage && !parentAndIssueAreSame;
            Func<Lookup, bool> selectionGalleryPredicate = l => l.Value == multipleImagesString;

            Lookup selectedOption = multipleImageOption == MultipleImageOptions.None ? null : GetMatchingImageOption(multipleImageOptionPredicate);

            if (multipleImageOption == MultipleImageOptions.None)
            {
                switch (selectionCase)
                {
                    case MultiImageSelectionCases.None:
                    case MultiImageSelectionCases.SingleNoImage:
                    case MultiImageSelectionCases.SingleWithImage:

                        Func<Lookup, bool> singleImagePredicate = (l => selectionImagePredicate(l) || parentImagePredicate(l) || issueImagePredicate(l));

                        _multipleImageOptionsFiltered = _multipleImageOptions.Where(singleImagePredicate).ToBindingList();

                        if (_multipleImageOptionsFiltered.Any())
                        {
                            selectedOption = GetMatchingImageOption(selectionImagePredicate) ??
                                            (GetMatchingImageOption(parentImagePredicate) ??
                                             GetMatchingImageOption(issueImagePredicate));
                        }
                        break;
                    case MultiImageSelectionCases.MultipleAllSame:

                        Func<Lookup, bool> multipleAllSamePredicate = (l => selectionImagePredicate(l) ||
                                                                            selectionGalleryPredicate(l) ||
                                                                            parentImagePredicate(l) ||
                                                                            issueImagePredicate(l));

                        _multipleImageOptionsFiltered = _multipleImageOptions.Where(multipleAllSamePredicate).ToBindingList();

                        selectedOption = GetMatchingImageOption(selectionImagePredicate) ??
                                            (GetMatchingImageOption(selectionGalleryPredicate) ??
                                            (GetMatchingImageOption(parentImagePredicate) ??
                                             GetMatchingImageOption(issueImagePredicate)));
                        break;
                    case MultiImageSelectionCases.MultipleSingleImage:
                    case MultiImageSelectionCases.MultipleNone:
                    case MultiImageSelectionCases.MultipleMix:

                        Func<Lookup, bool> multipleImagePredicate = (l => selectionGalleryPredicate(l) ||
                                                                          parentImagePredicate(l) ||
                                                                          issueImagePredicate(l));

                        _multipleImageOptionsFiltered = _multipleImageOptions.Where(multipleImagePredicate).ToBindingList();

                        selectedOption = GetMatchingImageOption(selectionGalleryPredicate) ??
                                        (GetMatchingImageOption(parentImagePredicate) ??
                                         GetMatchingImageOption(issueImagePredicate));
                        break;
                }
            }
            

            foreach (var lookup in _multipleImageOptionsFiltered.Where(parentImagePredicate))
            {
                if (ItemParent != null) lookup.MemoryName = ItemParent.Name;
            }

            foreach (var lookup in _multipleImageOptionsFiltered.Where(issueImagePredicate))
            {
                if (Issue != null) lookup.MemoryName = Issue.Name;
            }

            lookUpEditImageSelectionMode.Properties.DataSource = _multipleImageOptionsFiltered;

            if (selectedOption != null)
            {
                lookUpEditImageSelectionMode.EditValue = selectedOption.Id;
            }
        }

        /// <summary>
        /// Switches to next image option
        /// </summary>
        private void SwitchImageOption()
        {
            lookUpEditImageSelectionMode.EditValue = _multipleImageOptionsFiltered.NextOf(lookUpEditImageSelectionMode.GetSelectedDataRow() as Lookup).Id;
        }

        #endregion

        #region Multiple Image Options: Gallery/Navigator

        /// <summary>
        /// Setup Gallery/Navigator view modes
        /// </summary>
        /// <param name="multipleImageViewMode"></param>
        private void SetMultipleImageViewMode(MultipleImageModes multipleImageViewMode)
        {
            _lastMultipleImageMode = multipleImageViewMode;

            switch (multipleImageViewMode)
            {
                case MultipleImageModes.Gallery:
                    SetMode(MultipleImageViewOptions.MultipleGallery);
                    break;
                case MultipleImageModes.Navigator:
                    try
                    {
                        CurrentItem = MultipleItems[_lastNavigatorIndex];
                    }
                    catch (Exception)
                    {
                        _lastNavigatorIndex = 0;
                        CurrentItem = MultipleItems[_lastNavigatorIndex];
                    }
                    
                    SetSingleItemImage(CurrentItem, true);
                    SetMode(MultipleImageViewOptions.MultipleNavigator);
                    SetCurrentIteNameAndmDescription();//Always call this after setting mode because setting mode will clear the description and title
                    SetupNavigatorButtons();
                    break;
            }
        }

        /// <summary>
        /// Switches to the next multiple images option
        /// </summary>
        private void SwitchToNextMultipleImageOption()
        {
            lookUpEditMultipleImageViewMode.EditValue = _multipleImageViewMode.NextOf(lookUpEditMultipleImageViewMode.GetSelectedDataRow() as Lookup).Id;
        }

        #endregion

        #region Navigator Logic

        /// <summary>
        /// Performs logic for navigator switching 
        /// </summary>
        /// <param name="moveNext"></param>
        /// <param name="usingSwitch"></param>
        /// <param name="usingTrackBar"></param>
        private void SwitchNavigator(bool moveNext, bool usingSwitch = false, bool usingTrackBar = false)
        {
            var currentItemIndex = MultipleItems.IndexOf(CurrentItem);

            var newIndex = usingTrackBar ? NavigatorPosition :
                                          usingSwitch ? currentItemIndex == MultipleItems.Count - 1 ?
                                          0 : currentItemIndex + 1 :
                                          currentItemIndex + (moveNext ? 1 : -1);

            GoToNavigatorImage(newIndex);
        }

        /// <summary>
        /// Goes to an image in the navigator view using an index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="usingTrackBar"></param>
        private void GoToNavigatorImage(int index, bool usingTrackBar = false)
        {
            _lastNavigatorIndex = index;

            if (!usingTrackBar)
            {
                NavigatorPosition = _lastNavigatorIndex;
            }

            try
            {
                CurrentItem = MultipleItems[_lastNavigatorIndex];
                SetSingleItemImage(CurrentItem, true);
                SetCurrentIteNameAndmDescription();
                SetupNavigatorButtons();
            }
            catch (Exception)
            {
                _lastNavigatorIndex = 0;
                GoToNavigatorImage(index, usingTrackBar);
            }
        }

        /// <summary>
        /// Setup Navigator buttons and controls
        /// </summary>
        private void SetupNavigatorButtons()
        {
            var currentItemIndex = MultipleItems.IndexOf(CurrentItem);
            simpleButtonNext.Enabled = currentItemIndex >= 0 && currentItemIndex < MultipleItems.Count - 1;
            simpleButtonPrevious.Enabled = currentItemIndex > 0 && currentItemIndex <= MultipleItems.Count - 1;
            NavigatorPosition = currentItemIndex;
        }

        #endregion

        #region UI Logic

        /// <summary>
        /// Sets the layout controls visibility based on the selected mode
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="navigatorModeActive"></param>
        private void SetMode(MultipleImageViewOptions mode, bool navigatorModeActive = false)
        {
            LastImageViewOption = mode;

            layoutControlGroupImageOptions.Visibility = _multipleImageOptionsFiltered.Count() <= 1 ? LayoutVisibility.Never : LayoutVisibility.Always;

            if (!navigatorModeActive)
            {
                layoutControlGroupNavigator.Visibility = LayoutVisibility.Never;
            }

            layoutControlGroupMultipleImageViewModes.Visibility = LayoutVisibility.Never;
            layoutControlItemKeepCurrentImage.Visibility = LayoutVisibility.Always;

            switch (mode)
            {
                case MultipleImageViewOptions.Single:
                    layoutControlGroupMultipleImages.Visibility = LayoutVisibility.Never;
                    layoutControlGroupSingleImage.Visibility = LayoutVisibility.Always;
                    break;

                case MultipleImageViewOptions.MultipleNavigator:
                    ResetNameAndDescription();
                    layoutControlGroupMultipleImages.Visibility = LayoutVisibility.Never;
                    layoutControlGroupSingleImage.Visibility = LayoutVisibility.Always;
                    if (!navigatorModeActive)
                    {
                        layoutControlGroupNavigator.Visibility = LayoutVisibility.Always;
                    }
                    layoutControlGroupMultipleImageViewModes.Visibility = LayoutVisibility.Always;
                    break;

                case MultipleImageViewOptions.MultipleGallery:
                    ResetNameAndDescription();
                    layoutControlGroupSingleImage.Visibility = LayoutVisibility.Never;
                    layoutControlGroupMultipleImages.Visibility = LayoutVisibility.Always;
                    layoutControlGroupMultipleImageViewModes.Visibility = LayoutVisibility.Always;
                    break;

                case MultipleImageViewOptions.Default:
                    ResetNameAndDescription();
                    layoutControlItemKeepCurrentImage.Visibility = LayoutVisibility.Never;
                    layoutControlGroupImageOptions.Visibility = LayoutVisibility.Never;
                    layoutControlGroupMultipleImages.Visibility = LayoutVisibility.Never;

                    layoutControlGroupSingleImage.Visibility = LayoutVisibility.Always;
                    layoutControlGroupMultipleImages.Visibility = LayoutVisibility.Never;
                    break;

                case MultipleImageViewOptions.KeepCurrentImage:
                    layoutControlGroupMultipleImages.Visibility = LayoutVisibility.Never;
                    layoutControlGroupSingleImage.Visibility = LayoutVisibility.Always;
                    layoutControlGroupImageOptions.Visibility = LayoutVisibility.Never;
                    break;
            }
        }

        /// <summary>
        /// Sets the gallery control zoom level
        /// </summary>
        /// <param name="value"></param>
        private void SetGalleryZoomLevel(int value)
        {
            galleryControlMultipleImages.Gallery.ImageSize = new Size(value, value);
            trackBarControlSelectionGalleryZoom.Value = value;
        }

        /// <summary>
        /// Checks Auto Zoom option and sets size based on it
        /// </summary>
        private void CheckAutoZoom()
        {
            if (UseAutoZoom)
            {
                var columnCount = 1;

                if (_galleryItemGroupMultipleImages.Items.Count <= 4)
                {
                    columnCount = 2;
                }
                else if (_galleryItemGroupMultipleImages.Items.Count <= 9)
                {
                    columnCount = 3;
                }
                else if (_galleryItemGroupMultipleImages.Items.Count > 9)
                {
                    columnCount = 4;
                }
                //22 Is the difference in size between all items and the size of the gallery (Maybe that is the size of the scroll area)
                //column count: number of columns we want to show in gallery
                //12 Is the difference in size between the GalleryItem border andd the Image inside it
                //6 Is the difference between the ImageBounds size which are bounds of the image inside and the manaual size we set in the ImageSize property that we set below
                var newSize = ((galleryControlMultipleImages.Size.Width - 22) / columnCount) - 12 - 6;
                if (newSize >= trackBarControlSelectionGalleryZoom.Properties.Minimum ||
                    newSize <= trackBarControlSelectionGalleryZoom.Properties.Maximum)
                {
                    trackBarControlSelectionGalleryZoom.Value = newSize;    
                }
                
                galleryControlMultipleImages.Gallery.ImageSize = new Size(newSize, newSize);
            }
            else
            {
                SetGalleryZoomLevel(trackBarControlSelectionGalleryZoom.Value);
            }
        }
        #endregion

        #endregion

        #endregion

        #region Event Handlers

        #region Singe/Multi Image Options Handlers

        /// <summary>
        /// Handle Mode Selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditImageSelectionMode_EditValueChanged(object sender, EventArgs e)
        {
            if (!lookUpEditImageSelectionMode.Focused || lookUpEditImageSelectionMode.EditValue == null) return;

            var selectedOption = _multipleImageOptions.FirstOrDefault(l => l.Id == (int)lookUpEditImageSelectionMode.EditValue);

            if (selectedOption == null) return;

            var enumOption = EnumNameResolver.LookupAsEnum<MultipleImageOptions>(selectedOption.Value);

            SetImageOption(enumOption);
        }

        /// <summary>
        /// Handle switch button click to change the image view option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditImageSelectionMode_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Glyph && (string)e.Button.Tag == "Switch")
            {
                SwitchImageOption();
            }
        }

        #endregion

        #region Gallery/Navigator Options Handlers

        /// <summary>
        /// Handle Gallery/Navigator lookup selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditMultipleImageViewMode_EditValueChanged(object sender, EventArgs e)
        {
            if (!lookUpEditMultipleImageViewMode.Focused || lookUpEditMultipleImageViewMode.EditValue == null) return;

            var selectedViewMode = _multipleImageViewMode.FirstOrDefault(l => l.Id == (int)lookUpEditMultipleImageViewMode.EditValue);

            if (selectedViewMode == null) return;

            var enumOption = EnumNameResolver.LookupAsEnum<MultipleImageModes>(selectedViewMode.Value);
            SetMultipleImageViewMode(enumOption);
        }

        /// <summary>
        /// Handle Gallery/Navigator Switching
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditMultipleImageViewMode_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Glyph && (string)e.Button.Tag == "Switch")
            {
                SwitchToNextMultipleImageOption();
            }
        }

        #endregion
                
        #region Navigator Switching

        /// <summary>
        /// Handle zoom for selection gallery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarControlSelectionGalleryZoom_EditValueChanged(object sender, EventArgs e)
        {
            SetGalleryZoomLevel(trackBarControlSelectionGalleryZoom.Value);
            if (trackBarControlSelectionGalleryZoom.Focused)
            {
                UseAutoZoom = false;
            }
        }

        /// <summary>
        /// Handle Navigator next action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonNext_Click(object sender, EventArgs e)
        {
            SwitchNavigator(true);
        }

        /// <summary>
        /// Handle Navigator previous action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrevious_Click(object sender, EventArgs e)
        {
            SwitchNavigator(false);
        }

        /// <summary>
        /// Handle Navigator switch action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSwitchNavigator_Click(object sender, EventArgs e)
        {
            SwitchNavigator(true, true);
        }

        /// <summary>
        /// Handle Navigator trackbar scroll action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarControlNavigator_EditValueChanged(object sender, EventArgs e)
        {
            SwitchNavigator(true, false, true);
        }

        #endregion

        #region Other

        /// <summary>
        /// Handling the click on change description button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonEditDescription_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    Invoke(new EventHandler(simpleButtonEditDescription_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                InvokeChangeDescription();
            }
        }

        /// <summary>
        /// Handle border drawing for gallery images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gallery_CustomDrawItemImage(object sender, GalleryItemCustomDrawEventArgs e)
        {
            var viewInfo = e.ItemInfo as GalleryItemViewInfo;
            e.Cache.Graphics.SetClip(viewInfo.Bounds);

            var borderWidth = 1;
            var drawRectangle = new Rectangle(viewInfo.ImageBounds.X - borderWidth,
                                     viewInfo.ImageBounds.Y - borderWidth,
                                     viewInfo.ImageBounds.Width + borderWidth + borderWidth,
                                     viewInfo.ImageBounds.Height + borderWidth + borderWidth);
            e.Cache.DrawRectangle(new Pen(Color.FromArgb(139, 160, 188), borderWidth), drawRectangle);
            e.Cache.Graphics.DrawImage(e.Item.Image, viewInfo.ImageBounds);

            e.Handled = true;
        }

        /// <summary>
        /// Checks or unchecks the auto zoom option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEditUseAutoZoom_CheckedChanged(object sender, EventArgs e)
        {
            CheckAutoZoom();
        }

        /// <summary>
        /// Handle keep current image checkedit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEditKeepCurrentImage_CheckedChanged(object sender, EventArgs e)
        {
            if (KeepCurrentImage)
            {
                SetMode(MultipleImageViewOptions.KeepCurrentImage);
            }
            else
            {
                SetDetails(SelectedItems, TopItems, BottomItems, TopItemsHightlighted, ItemParent, Issue);
            }
        }

        /// <summary>
        /// Handle item click to open it in navigator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void galleryControlGallery1_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            SwitchToNextMultipleImageOption();
            SetMultipleImageViewMode(MultipleImageModes.Navigator);
            GoToNavigatorImage((int)e.Item.Tag);
        }
        #endregion

        #endregion
    }
}