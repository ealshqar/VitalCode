using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.DomainObjects.Properties;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormProductDosages : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private LookupsManager _lookupsManager;
        private PropertiesManager _propertiesManager;
        private ItemsManager _itemsManager;

        private BindingSource _bindingSource;

        private bool _isOpenEditor;
        private int _productApplicableTypeLookupId;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ScheduleLine value. 
        /// </summary>
        public ScheduleLine ScheduleLine { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct new instance of XtraFormProductDosages.
        /// </summary>
        public XtraFormProductDosages()
        {
            InitializeComponent();

            PerformSpecificIntializationSteps();
        }

        #endregion

        #region Logic

        /// <summary>
        /// Perform the initialization of the test issue
        /// </summary>
        private void PerformSpecificIntializationSteps()
        {
            _bindingSource = new BindingSource();
            _lookupsManager = new LookupsManager();
            _propertiesManager = new PropertiesManager();
            _itemsManager = new ItemsManager();

            FillLocalLookupIds();

            CsaEmdUnitManager.Instance.ActivateConnection(Csa_Instance_Released, _csaManager_ReadingDone, _csaManager_MeterValueChanged);

            UiHelperClass.SetLayoutControlProperties(layoutControlScheduleLine);
        }

        /// <summary>
        /// Bind the form controls.
        /// </summary>
        private void SetBinding()
        {
            StartReading();

            if (ScheduleLine == null || ScheduleLine.Item == null)
                return;

            _bindingSource.DataSource = ScheduleLine;
            
            vGridControlProductDosages.DataSource = _bindingSource;

            UiHelperClass.BindControl(memoEditProductDosagesNotes, ScheduleLine, () => ScheduleLine.Notes);

            var groupText = string.Format(StaticKeys.ProsuctDosagesGroupText, ScheduleLine.Item.Name);

            Text = layoutControlGroupDosages.Text = layoutControlGroupDosages.OptionsToolTip.ToolTip = groupText;

            checkEditSetAsDefaultsDosages.Text = checkEditSetAsDefaultsDosages.ToolTip = string.Format(StaticKeys.SetAsDefaultsDosagesCheckboxText, ScheduleLine.Item.Name);

            xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);

            checkEditSetAsDefaultsDosages.Checked = ScheduleLine.Item.Properties.FirstOrDefault(ip => ip.Property != null && ip.Property.ApplicableTypeLookup != null && ip.Property.ApplicableTypeLookup.Id == _productApplicableTypeLookupId && ip.Value != null && !string.IsNullOrEmpty(ip.Value.ToString())) == null;

        }

        /// <summary>
        /// Set the default values.
        /// </summary>
        private void SetDefaultValues()
        {
            vGridControlProductDosages.CloseEditor();

            TestsManager.SetScheduleLineDefaultValues(ScheduleLine);
        }

        /// <summary>
        /// Fill the local lookups ids.
        /// </summary>
        private void FillLocalLookupIds()
        {
            var productApplicableTypeLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ApplicableType, ApplicableTypesEnum.Product));

            _productApplicableTypeLookupId = productApplicableTypeLookup == null ? 0 : productApplicableTypeLookup.Id;
        }

        /// <summary>
        /// Set default value for the passed property and if not exists add it. 
        /// </summary>
        private bool SetAsDefaultDosagePropertyValue(IEnumerable<Property> productProperties, PropertiesEnum propertiesEnum, object value)
        {
            var existsProperty = ScheduleLine.Item.Properties.FirstProperty(propertiesEnum);

            if (existsProperty == null)
            {
                var dosageProperty = productProperties.FirstOrDefault(p => p.Key.Equals(EnumNameResolver.Resolve(propertiesEnum)));

                if (dosageProperty == null)
                    return false;

                ScheduleLine.Item.Properties.Add(new ItemProperty() { Item = ScheduleLine.Item, Property = dosageProperty, Value = value });
            }
            else
            {
                existsProperty.Value = value;
            }

            return true;
        }

        /// <summary>
        /// Set the current values to be the default dosage properties values.
        /// </summary>
        /// <returns></returns>
        private bool SetAsDefaulttDosageProperties()
        {
            if (ScheduleLine.Item == null)
                return false;

            UiHelperClass.ShowWaitingPanel(StaticKeys.SavingMessgae);

            var productProperties = _propertiesManager.GetProperties(new PropertiesFilter { ApplicableTypeIds = new[] { _productApplicableTypeLookupId } });

            var result = SetAsDefaultDosagePropertyValue(productProperties, PropertiesEnum.DefaultDuration, ScheduleLine.Duration);
            result &= SetAsDefaultDosagePropertyValue(productProperties, PropertiesEnum.DefaultWhenArising, ScheduleLine.WhenArising);
            result &= SetAsDefaultDosagePropertyValue(productProperties, PropertiesEnum.DefaultBreakfast, ScheduleLine.Breakfast);
            result &= SetAsDefaultDosagePropertyValue(productProperties, PropertiesEnum.DefaultBetweenMealsEarly, ScheduleLine.BetweenMealsEarly);
            result &= SetAsDefaultDosagePropertyValue(productProperties, PropertiesEnum.DefaultLunch, ScheduleLine.Lunch);
            result &= SetAsDefaultDosagePropertyValue(productProperties, PropertiesEnum.DefaultBetweenMealsLate, ScheduleLine.BetweenMealsLate);
            result &= SetAsDefaultDosagePropertyValue(productProperties, PropertiesEnum.DefaultBeforeSleep, ScheduleLine.BeforeSleep);
            result &= SetAsDefaultDosagePropertyValue(productProperties, PropertiesEnum.DefaultDinner, ScheduleLine.Dinner);            
            result &= SetAsDefaultDosagePropertyValue(productProperties, PropertiesEnum.DefaultNoPerBottle, ScheduleLine.NoPerBottle);
            result &= SetAsDefaultDosagePropertyValue(productProperties, PropertiesEnum.DefaultPrice, ScheduleLine.Price);
            result &= SetAsDefaultDosagePropertyValue(productProperties, PropertiesEnum.DefaultNotes, ScheduleLine.Notes);

            if (result)
               result &= _itemsManager.SaveItemProperties(ScheduleLine.Item.Properties).IsSucceed;

            UiHelperClass.HideSplash();

            return result;

        }

        /// <summary>
        /// Performs logic of the done action
        /// </summary>
        private void DoneAction()
        {
            vGridControlProductDosages.PostEditor();

            if (checkEditSetAsDefaultsDosages.Checked && !SetAsDefaulttDosageProperties())
                return;

            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region HW_Logic

        /// <summary>
        /// Start the reading.
        /// </summary>
        private void StartReading()
        {
            CsaEmdUnitManager.Instance.Clear();

            ResetHardwareHandlers();

            CsaEmdUnitManager.Instance.StartReading();
        }

        /// <summary>
        /// Reset the hardware handlers.
        /// </summary>
        private void ResetHardwareHandlers()
        {
            RemoveHardwareHandlers();
            AddHardwareHandlers();
        }

        /// <summary>
        /// Remove the hardware handlers.
        /// </summary>
        private void RemoveHardwareHandlers()
        {
            CsaEmdUnitManager.Instance.MeterValueChanged -= _csaManager_MeterValueChanged;
            CsaEmdUnitManager.Instance.ReadingDone -= _csaManager_ReadingDone;
        }

        /// <summary>
        /// Add the hardware handlers.
        /// </summary>
        private void AddHardwareHandlers()
        {
            CsaEmdUnitManager.Instance.MeterValueChanged += _csaManager_MeterValueChanged;
            CsaEmdUnitManager.Instance.ReadingDone += _csaManager_ReadingDone;
        }

        /// <summary>
        /// Stop the reading.
        /// </summary>
        private void StopReading()
        {
            CsaEmdUnitManager.Instance.StopReading();
            RemoveHardwareHandlers();
        }

        #endregion

        #region Events

        #region HW_Events

        /// <summary>
        /// Handel the meter value changed.
        /// </summary>
        void _csaManager_ReadingDone(object sender, int reading, int min, int max, int fall, int rise)
        {
            _csaManager_MeterValueChanged(sender, reading, min, max);

            var csaManager = sender as CsaEmdUnitManager;

            if (csaManager != null && csaManager.IsReadingOn == false) return;

            if (InvokeRequired)
            {
                Invoke(new CsaEmdUnitManager.OnReadingDone(_csaManager_ReadingDone), sender, reading, min, max, fall, rise);
            }
            else
            {
                if (!CsaEmdUnitManager.Instance.HasReading) return;

                StopReading();

                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.WaitingToRelease,string.Empty, 0);
            }
        }

        /// <summary>
        /// Handel the reading done event.
        /// </summary>
        void _csaManager_MeterValueChanged(object sender, int reading, int min, int max)
        {
            if (InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.MeterValueChangedHandle(_csaManager_MeterValueChanged), sender,
                           reading, min, max);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                xtraUserControlReadingGaugeScheduleLine.ReadingValue = reading;

                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.Reading, string.Empty, 0);
                
            }

        }

        /// <summary>
        /// Handel the CSA released  event.
        /// </summary>
        /// <param name="sender">The sender as CSA manager.</param>
        void Csa_Instance_Released(object sender)
        {
            if (InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnReleased(Csa_Instance_Released), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //This check for prevent the released [@UI level only] action get executing twice.
                if (!CsaEmdUnitManager.Instance.HasReading) return;

                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);

                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);

                StartReading();

            }
        }

        #endregion

        #region UI_Events

        /// <summary>
        /// Handle the hide event for editor.
        /// </summary>
        void vGridControlDosages_HiddenEditor(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(vGridControlDosages_HiddenEditor), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _isOpenEditor = false;
            }
        }

        /// <summary>
        /// Handle the show event for editor.
        /// </summary>
        void vGridControlDosages_ShownEditor(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(vGridControlDosages_ShownEditor), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _isOpenEditor = true;
            }
        }

        /// <summary>
        ///  Handel the form load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormProductDosages_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraFormProductDosages_Load), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SetBinding();
            }
        }


        /// <summary>
        /// Close the form by done or cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ItemClickEventHandler(barManager_ItemClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (string.IsNullOrEmpty(e.Item.Name)) return;

                if (e.Item == barButtonItemDone)
                {
                    DoneAction();
                }
                else if (e.Item == barButtonItemResetDefaults)
                {
                    SetDefaultValues();
                }
            }
        }

        /// <summary>
        /// Handel the form closing to stop the reading and remove the events handlers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormProductDosages_FormClosing(object sender, FormClosingEventArgs e)
        {
            CsaEmdUnitManager.Instance.DisposeConnection(Csa_Instance_Released, _csaManager_ReadingDone, _csaManager_MeterValueChanged);
        }

        /// <summary>
        /// Handles the key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormProductDosages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
            {
                if (memoEditProductDosagesNotes.IsEditorActive && e.KeyCode == Keys.Enter)
                    return;

                DoneAction();
            }
            else if (e.KeyCode == Keys.Down)
            {
                //Moves down to the notes field if the last line is focused
                if (vGridControlProductDosages.ActiveEditor != null || vGridControlProductDosages.Focused)
                {
                    if (vGridControlProductDosages.FocusedRow.Index == vGridControlProductDosages.Rows.Count - 1)
                    {
                        memoEditProductDosagesNotes.Focus();
                        e.Handled = true;
                    }
                }
                else if (memoEditProductDosagesNotes.IsEditorActive || memoEditProductDosagesNotes.Focused)
                {
                    //Moves down to the done button if the last line in the notes is focused
                    if (string.IsNullOrEmpty(memoEditProductDosagesNotes.Text) ||
                        memoEditProductDosagesNotes.SelectionStart >=
                        (memoEditProductDosagesNotes.Text.Count() -
                        memoEditProductDosagesNotes.Lines[memoEditProductDosagesNotes.Lines.Count() - 1].Length))
                    {
                        simpleButtonDone.Focus();
                        e.Handled = true;
                    }
                }
                else if (simpleButtonDone.Focused)
                {
                    checkEditSetAsDefaultsDosages.Focus();
                    e.Handled = true;
                }
                else
                {
                    checkEditSetAsDefaultsDosages.Focus();
                    e.Handled = true;
                }                
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (vGridControlProductDosages.ActiveEditor != null || vGridControlProductDosages.Focused)
                {
                    if (vGridControlProductDosages.FocusedRow.Index == 0)
                    {                        
                        e.Handled = true;
                    }
                }
                else if (memoEditProductDosagesNotes.IsEditorActive || memoEditProductDosagesNotes.Focused)
                {
                    //Moves up only if the cursor is in the first
                    if (string.IsNullOrEmpty(memoEditProductDosagesNotes.Text) || 
                        memoEditProductDosagesNotes.SelectionStart <= memoEditProductDosagesNotes.Lines[0].Length)
                    {
                        vGridControlProductDosages.Focus();
                        vGridControlProductDosages.FocusLast();
                        vGridControlProductDosages.ShowEditor();
                        e.Handled = true;
                    }
                }
                else if(simpleButtonDone.Focused)
                {
                    //Move to the last line in the memo edit
                    memoEditProductDosagesNotes.Focus();                    
                    e.Handled = true;
                }
                else if (checkEditSetAsDefaultsDosages.Focused)
                {
                    simpleButtonDone.Focus();
                    e.Handled = true;
                }
                else
                {
                    vGridControlProductDosages.Focus();
                    vGridControlProductDosages.FocusFirst();
                    vGridControlProductDosages.ShowEditor();
                    e.Handled = true;
                }
            }

        }

        /// <summary>
        /// Handles showing of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormProductDosages_Shown(object sender, EventArgs e)
        {
            //This code is needed so the grid if focused initiall and the cursor is visible inside the 
            //cell and the editor is active.
            vGridControlProductDosages.Focus();
            vGridControlProductDosages.FocusFirst();
            vGridControlProductDosages.ShowEditor();
        }

        #endregion        

        /// <summary>
        /// Perform the done action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDone_Click(object sender, EventArgs e)
        {
            DoneAction();
        }

        /// <summary>
        /// Handels checkEditSetAsDefaultsDosages getting focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraUserControlReadingGaugeScheduleLine_Enter(object sender, EventArgs e)
        {
            checkEditSetAsDefaultsDosages.Focus();
        }

        /// <summary>
        /// Handles layoutControlScheduleLine getting focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutControlScheduleLine_Enter(object sender, EventArgs e)
        {
            checkEditSetAsDefaultsDosages.Focus();
            checkEditSetAsDefaultsDosages.Select();
        }

        #endregion
    }
}
