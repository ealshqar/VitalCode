using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.SpotChecks
{
    public class SpotCheck : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private Patient _patient;
        private string _name;
        private string _notes;
        private string _capsolTnotes;
        private string _mineralsNotes;
        private string _mondayNotes;
        private string _tuesdayNotes;
        private string _wednesdayNotes;
        private bool _mineralsThree;
        private bool _mineralsOne;
        private bool _mineralsIvPush;
        private int _mineralsSterlieWaterCc;
        private int _mineralsSterlieWaterCcpriority;
        private int _mineralsDextroseCc;
        private int _mineralsDextroseCcpriority;
        private int _mineralsNormalSalineCc;
        private int _mineralsNormalSalineCcpriority;
        private int _mineralsIvperMin;
        private int _mineralsPerWeek;
        private int _mineralsEdta;
        private int _ingredientsNumberOfBags;
        private int _ingredientsNumberPerWeek;

        private BindingList<SpotCheckResult> _spotCheckResults;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Test id.
        /// </summary>
        public int ? TestId { set; get; }

        /// <summary>
        /// Gets or sets the minerals EDTA
        /// </summary>
        public int MineralsEdta
        {
            get { return _mineralsEdta; }
            set
            {
                _mineralsEdta = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the minerals per week
        /// </summary>
        public int MineralsPerWeek
        {
            get { return _mineralsPerWeek; }
            set
            {
                _mineralsPerWeek = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the minerals iv per min
        /// </summary>
        public int MineralsIvperMin
        {
            get { return _mineralsIvperMin; }
            set
            {
                _mineralsIvperMin = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the minerals normal saline cc priority
        /// </summary>
        public int MineralsNormalSalineCcpriority
        {
            get { return _mineralsNormalSalineCcpriority; }
            set
            {
                _mineralsNormalSalineCcpriority = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the minerals normal saline cc
        /// </summary>
        public int MineralsNormalSalineCc
        {
            get { return _mineralsNormalSalineCc; }
            set
            {
                _mineralsNormalSalineCc = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the minerals dextrose cc priority
        /// </summary>
        public int MineralsDextroseCcpriority
        {
            get { return _mineralsDextroseCcpriority; }
            set
            {
                _mineralsDextroseCcpriority = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the minerals dextrose cc
        /// </summary>
        public int MineralsDextroseCc
        {
            get { return _mineralsDextroseCc; }
            set
            {
                _mineralsDextroseCc = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the minerals sterile water cc priority
        /// </summary>
        public int MineralsSterlieWaterCcpriority
        {
            get { return _mineralsSterlieWaterCcpriority; }
            set
            {
                _mineralsSterlieWaterCcpriority = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the minerals sterile Water cc
        /// </summary>
        public int MineralsSterlieWaterCc
        {
            get { return _mineralsSterlieWaterCc; }
            set
            {
                _mineralsSterlieWaterCc = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the minerals is iv push.
        /// </summary>
        public bool MineralsIvPush
        {
            get { return _mineralsIvPush; }
            set
            {
                _mineralsIvPush = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the minerals one.
        /// </summary>
        public bool MineralsOne
        {
            get { return _mineralsOne; }
            set
            {
                _mineralsOne = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the minerals three.
        /// </summary>
        public bool MineralsThree
        {
            get { return _mineralsThree; }
            set
            {
                _mineralsThree = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the ingredients Monday notes.
        /// </summary>
        public string WednesdayNotes
        {
            get { return _wednesdayNotes; }
            set
            {
                _wednesdayNotes = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the ingredients Tuesday notes.
        /// </summary>
        public string TuesdayNotes
        {
            get { return _tuesdayNotes; }
            set
            {
                _tuesdayNotes = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the ingredients Monday notes.
        /// </summary>
        public string MondayNotes
        {
            get { return _mondayNotes; }
            set
            {
                _mondayNotes = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the ingredients notes.
        /// </summary>
        public string IngredientsNotes
        {
            get { return _mineralsNotes; }
            set
            {
                _mineralsNotes = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }


        /// <summary>
        /// Gets or sets the Capsule T notes.
        /// </summary>
        public string CapsoleTnotes
        {
            get { return _capsolTnotes; }
            set
            {
                _capsolTnotes = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        public Patient Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the spot check results.
        /// </summary>
        public BindingList<SpotCheckResult> SpotCheckResults
        {
            get { return _spotCheckResults; }
            set
            {
                _spotCheckResults = value;
                _spotCheckResults.RaiseListChangedEvents = true;
                _spotCheckResults.ListChanged += SpotCheckResults_ListChanged;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the ingredients number of bags
        /// </summary>
        public int IngredientsNumberOfBags
        {
            get { return _ingredientsNumberOfBags; }
            set
            {
                _ingredientsNumberOfBags = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the ingredients number per week
        /// </summary>
        public int IngredientsNumberPerWeek
        {
            get { return _ingredientsNumberPerWeek; }
            set
            {
                _ingredientsNumberPerWeek = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Notifies the change of the spot check results list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SpotCheckResults_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => SpotCheckResults), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => SpotCheckResults));
        }

        #endregion

        #region Validation

        /// <summary>
        /// Gets the validation errors according to the below cases.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="info">The error info.</param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            if (propertyName == ExpressionHelper.GetPropertyName(() => Name))
            {
                IsRequiredStringPropertyValid(propertyName, Name, info);
            }

            UpdateErrorsSummary(info);
        }

        /// <summary>
        /// Gets the errors by updating the Error Info.
        /// </summary>
        /// <param name="info"></param>
        public void GetError(ErrorInfo info)
        {

        }

        /// <summary>
        /// Checks the properties by calling the Get Property error.
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (!IsChanged) return true;

            ValidationErrors.Clear();

            foreach (PropertyInfo property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }

            return IsValid;
        }

        #endregion
    }
}
