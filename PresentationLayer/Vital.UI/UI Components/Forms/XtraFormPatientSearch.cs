using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using System.Linq;
using System.Linq.Expressions;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormPatientSearch : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private Patient _currentPatient;        

        #endregion
        
        #region Properties

        /// <summary>
        /// Gets the current patient record
        /// </summary>
        public Patient CurrentPatient
        {
            get { return _currentPatient; }
        }
        
        /// <summary>
        /// Show automatically setting value
        /// </summary>
        public bool AutoShowUpSetting
        {
            get { return checkEditShowAutomatically.Checked; }
        }

        #endregion

        #region Contructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraFormPatientSearch()
        {
            InitializeComponent();
        }

        #endregion

        #region Logic

        /// <summary>
        /// Sets the initial search string when opening the dialog with immediate writing in main form
        /// </summary>
        /// <param name="initialSearch"></param>
        public void SetInitialSearch(string initialSearch)
        {
            buttonEditSearch.Text = initialSearch;            
            gridViewPatients.ApplyFindFilter(initialSearch);
            ShowHidePatients(true);
            buttonEditSearch.Focus();
            buttonEditSearch.SelectionStart = buttonEditSearch.Text.Length;
            buttonEditSearch.SelectionLength = 0;
        }

        /// <summary>
        /// Sets the patients datasource
        /// </summary>
        /// <param name="patients"></param>
        public void SetPatientsDatasource(object patients)
        {
            gridControlPatients.DataSource = patients;
        }

        /// <summary>
        /// Sets the automatic show up setting
        /// </summary>
        /// <param name="showAutomatically"></param>
        public void SetShowAutomaticallySetting(bool showAutomatically)
        {
            checkEditShowAutomatically.Checked = showAutomatically;
        }

        /// <summary>
        /// Gets the button of the button edit by tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private EditorButton GetButtonByTag(string tag)
        {
            return buttonEditSearch.Properties.Buttons.Cast<EditorButton>()
                .FirstOrDefault(button => button.Tag.ToString() == tag);
        }

        /// <summary>
        /// Shows or Hides patients grid
        /// </summary>
        /// <param name="showPatients"></param>
        private void ShowHidePatients(bool showPatients)
        {
            layoutControlItemPatients.Visibility = showPatients ? LayoutVisibility.Always : LayoutVisibility.Never;
            //GetButtonByTag("ShowPatients").Visible = !showPatients;
            GetButtonByTag("Open").Visible = showPatients;
            emptySpaceItem1.Visibility = showPatients ? LayoutVisibility.Never : LayoutVisibility.Always;
            simpleLabelItemHint.Visibility = layoutControlItemPatients.Visibility;
        }

        /// <summary>
        /// Open patient
        /// </summary>
        private void OpenPatient()
        {
            _currentPatient = (Patient)gridViewPatients.GetFocusedRow();
            if (CurrentPatient == null)
            {
                UiHelperClass.ShowInformation("Please select a patient profile to open.","No Patient Profile Selected");
                return;
            }
            DialogResult = DialogResult.Yes;
            Close();
        }

        /// <summary>
        /// Clears the search
        /// </summary>
        private void ClearSearch()
        {
            buttonEditSearch.Text = string.Empty;
            _currentPatient = null;
            gridViewPatients.ApplyFindFilter(string.Empty);
            gridViewPatients.MoveFirst();
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormPatientSearch_Load(object sender, EventArgs e)
        {            
            UiHelperClass.FadeIn(this,true);
            buttonEditSearch.Focus();
            buttonEditSearch.SelectionStart = buttonEditSearch.Text.Length;
            buttonEditSearch.SelectionLength = 0;            
        }

        /// <summary>
        /// Handles text changes in the search box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditSearch_EditValueChanged(object sender, EventArgs e)
        {
            var showPatients = buttonEditSearch.Text != string.Empty;
            ShowHidePatients(showPatients);
            GetButtonByTag("Open").Visible = showPatients;
            GetButtonByTag("Clear").Visible = showPatients;
            GetButtonByTag("NullValuePrompt").Visible = !showPatients;
            gridViewPatients.ApplyFindFilter(buttonEditSearch.Text);
        }

        /// <summary>
        /// Handles navigation between search box and grid and also key actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormPatientSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //Handles hiding the NullValuePrompt quickly without having to wait for 
            //EditValueChanged even which comes late a bit
            if (buttonEditSearch.IsEditorActive)
            {
                if (!(e.KeyData >= Keys.F1 && e.KeyData <= Keys.F12))
                {
                    var key = (char) e.KeyData;
                    if (char.IsLetterOrDigit(key))
                    {
                        GetButtonByTag("NullValuePrompt").Visible = false;
                    }
                }                
            }

            if (e.KeyCode == Keys.Enter)
            {
                OpenPatient();
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.F12)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            else if (e.KeyCode == Keys.Down && buttonEditSearch.IsEditorActive)
            {
                if (buttonEditSearch.Text == string.Empty)
                {
                    ShowHidePatients(true);
                }
                gridControlPatients.Focus();
                gridViewPatients.MoveFirst();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up && buttonEditSearch.IsEditorActive)
            {
                if (buttonEditSearch.Text == string.Empty)
                {
                    ClearSearch();
                    ShowHidePatients(false);
                }                
            }
            else if (gridControlPatients.Focused ||
                     checkEditShowAutomatically.Focused ||
                     simpleButtonClose.Focused)
            {
                if (e.KeyCode == Keys.Up && gridControlPatients.Focused)
                {
                    if (gridViewPatients.FocusedRowHandle == 0)
                    {
                        buttonEditSearch.Focus();
                        buttonEditSearch.SelectionStart = buttonEditSearch.Text.Length;
                        buttonEditSearch.SelectionLength = 0;
                        e.Handled = true;
                    }
                }
                else if (!(e.KeyData >= Keys.F1 && e.KeyData <= Keys.F12) || e.KeyCode == Keys.Back)
                {
                    var key = (char)e.KeyData;
                    if (e.KeyCode == Keys.Back || char.IsLetterOrDigit(key))
                    {
                        buttonEditSearch.Focus();
                        if (e.KeyCode == Keys.Back)
                        {
                            if (buttonEditSearch.Text.Length != 0)
                            {
                                buttonEditSearch.Text = buttonEditSearch.Text.Remove(buttonEditSearch.Text.Length - 1);
                            }
                        }
                        else
                        {
                            buttonEditSearch.Text = buttonEditSearch.Text + e.KeyCode.ToString().ToLower();
                        }
                        buttonEditSearch.SelectionStart = buttonEditSearch.Text.Length;
                        buttonEditSearch.SelectionLength = 0;
                        e.Handled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the search control buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Tag != null)
            {
                if (e.Button.Tag.Equals("Open"))
                {
                    OpenPatient();
                }
                else if (e.Button.Tag.Equals("Clear"))
                {
                    ClearSearch();
                }
                else if (e.Button.Tag.Equals("Export"))
                {
                    UiHelperClass.ExportToCsv<Patient>(gridViewPatients, this, string.Format(StaticKeys.ExportPatientsToCsvFileName, DateTime.Now.ToString(StaticKeys.DateFormatMmmDdYyy)));                    
                }
                else if (e.Button.Tag.Equals("ShowPatients"))
                {
                    ShowHidePatients(layoutControlItemPatients.Visibility != LayoutVisibility.Always);
                }
            }
        }
        
        /// <summary>
        /// Fade out during form closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormPatientSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            UiHelperClass.FadeOut(this, true);
        }

        /// <summary>
        /// Handles clicking the close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Handles double clicking the patients view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewPatients_DoubleClick(object sender, EventArgs e)
        {
            if (UiHelperClass.IsClickInRowByMouse(gridViewPatients))
            {
                OpenPatient();
            }
        }

        #endregion
    }
}