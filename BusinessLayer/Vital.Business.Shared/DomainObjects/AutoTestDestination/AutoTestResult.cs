using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.AutoTestSource;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestDestination
{
    public class AutoTestResult : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private AutoTest _autoTest;
        private AutoItem _autoItem;
        private AutoProtocolStageRevision _autoProtocolStageRevision;
        private int? _autoTestResultsParentId;
        private AutoTestResult _autoTestResultParent;
        private int? _preliminaryReading;
        private int? _summaryReading;
        private bool _isAddedManually;
        private string _notes;

        private int? _structureId;
        private int? _structureParentId;

        private BindingList<AutoTestResultProduct> _autoTestResultProducts;
        private BindingList<AutoTestResult> _autoTestResultChildes;

        #endregion

        #region Memory Only Properties

        /// <summary>
        /// Reference to the related StageAutoItemId
        /// </summary>
        public int? StageAutoItemId
        {
            get
            {
                return StageAutoItem != null? StageAutoItem.Id : (int?) null;
            }
        }

        /// <summary>
        /// Reference to the related StageAutoItem
        /// </summary>
        public StageAutoItem StageAutoItem { get; set; }

        /// <summary>
        /// The structure Id of the result in the result tree
        /// </summary>
        public int StructureId
        {
            get
            {
                return _structureId.HasValue ? _structureId.Value : Id;
            }
            set
            {
                _structureId = value;
            }
        }

        /// <summary>
        /// The structure parent Id of the result in the result tree
        /// </summary>
        public int? StructureParentId
        {
            get
            {
                return _structureParentId.HasValue ? _structureParentId : AutoTestResultsParentId;
            }
            set
            {
                _structureParentId = value;
            }
        }

        /// <summary>
        /// The real structure Id without checking for ID
        /// </summary>
        public int? RealStructureId
        {
            get
            {
                return _structureId;
            }
        }

        /// <summary>
        /// The real structure parent Id without checking for ID
        /// </summary>
        public int? RealStructureParentId
        {
            get
            {
                return _structureParentId;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the AutoTest.
        /// </summary>
        public AutoTest AutoTest
        {
            get { return _autoTest; }
            set
            {
                if (_autoTest == null || !_autoTest.Equals(value))
                {
                    _autoTest = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the AutoItem.
        /// </summary>
        public AutoItem AutoItem
        {
            get { return _autoItem; }
            set
            {
                if (_autoItem == null || !_autoItem.Equals(value))
                {
                    _autoItem = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the AutoProtocolStageRevision.
        /// </summary>
        public AutoProtocolStageRevision AutoProtocolStageRevision
        {
            get { return _autoProtocolStageRevision; }
            set
            {
                if (_autoProtocolStageRevision == null || !_autoProtocolStageRevision.Equals(value))
                {
                    _autoProtocolStageRevision = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// AutoTestResultsParentId
        /// </summary>
        public int? AutoTestResultsParentId
        {
            get { return _autoTestResultsParentId; }
            set
            {
                if (!_autoTestResultsParentId.Equals(value))
                {
                    _autoTestResultsParentId = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the AutoTestResultsParent.
        /// </summary>
        public AutoTestResult AutoTestResultParent
        {
            get { return _autoTestResultParent; }
            set
            {
                if (_autoTestResultParent == null || !_autoTestResultParent.Equals(value))
                {
                    _autoTestResultParent = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_autoTestResultParent == null) return;

                StructureParentId = _autoTestResultParent.StructureId;
                _autoTestResultParent.PropertyChanged += AutoTestResultsParent_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the PreliminaryReading.
        /// </summary>
        public int? PreliminaryReading
        {
            get { return _preliminaryReading; }
            set
            {
                if (_preliminaryReading == null || !_preliminaryReading.Equals(value))
                {
                    _preliminaryReading = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the SummaryReading.
        /// </summary>
        public int? SummaryReading
        {
            get { return _summaryReading; }
            set
            {
                if (_summaryReading == null || !_summaryReading.Equals(value))
                {
                    _summaryReading = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the IsAddedManually.
        /// </summary>
        public bool IsAddedManually
        {
            get { return _isAddedManually; }
            set
            {
                if (!_isAddedManually.Equals(value))
                {
                    _isAddedManually = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
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
                if (_notes == null || !_notes.Equals(value))
                {
                    _notes = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the AutoTestResultProducts.
        /// </summary>
        public BindingList<AutoTestResultProduct> AutoTestResultProducts
        {
            get { return _autoTestResultProducts; }
            set
            {
                if (_autoTestResultProducts != null && _autoTestResultProducts == value) return;

                _autoTestResultProducts = value;
                _autoTestResultProducts.RaiseListChangedEvents = true;
                //_autoTestResultProducts.ListChanged += AutoTestResultProducts_ListChanged;
            }
        }

        /// <summary>
        /// Gets or sets the AutoTestResultChildes.
        /// </summary>
        public BindingList<AutoTestResult> AutoTestResultChildes
        {
            get { return _autoTestResultChildes; }
            set
            {
                if (_autoTestResultChildes != null && _autoTestResultChildes == value) return;

                _autoTestResultChildes = value;
                _autoTestResultChildes.RaiseListChangedEvents = true;
                //_autoTestResultChildes.ListChanged += AutoTestResultChildes_ListChanged;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the state for the AutoTestResultsParent.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoTestResultsParent_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => AutoTestResultParent), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => AutoTestResultParent, () => AutoTestResultParent.Id));
        }

        #endregion

        #region Validation

        /// <summary>
        /// Gets the validation errors according to the below cases.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="info">The error info</param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            if (propertyName == ExpressionHelper.GetPropertyName(() => AutoItem))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => AutoProtocolStageRevision))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => AutoTestResultParent))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => PreliminaryReading))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => SummaryReading))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => IsAddedManually))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Notes))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => AutoTestResultProducts) && !ValidateAutoTestResultProducts())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            UpdateErrorsSummary(info);
        }

        /// <summary>
        /// Validates the AutoTestResultProduct.
        /// </summary>
        /// <returns></returns>
        public bool ValidateAutoTestResultProducts()
        {
            return AutoTestResultProducts == null || AutoTestResultProducts.All(autoTestResultProduct => autoTestResultProduct.Validate());
        }

        /// <summary>
        /// Implements the IDXErrorInfo.
        /// </summary>
        /// <param name="info"></param>
        public virtual void GetError(ErrorInfo info)
        {

        }

        /// <summary>
        /// Checks the properties by calling the Get Property error.
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
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