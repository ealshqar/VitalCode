using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.AutoTestDestination;
using Vital.Business.Shared.DomainObjects.Images;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestSource
{
    public class AutoItem : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private TestingPoint _testingPoint;
        private Image _image;
        private Lookup _type;
        private Lookup _gender;
        private Lookup _structureType;
        private Lookup _status;
        private Lookup _childsOrderType;
        private Lookup _childsScanningType;
        private Lookup _scanningMethod;
        private int _scansNumber;
        private int _matchesNumber;
        private string _key;
        private string _name;
        private string _fullName;
        private string _description;
        private string _frequency;
        private string _modelIdentifier;
        private string _userNotes;
        private string _directAccessChecks;
        private bool _isUserItem;
        private bool _isSearchable;
        private bool _insertOnNo;
        private bool _finishAllScanRounds;
        private bool _addResultOnMatch;
        private bool _excludeOnMatch;
        private bool _addAllChildesOnMatch;
        private bool _checkVitalForce;
        private bool _checkFourFactors;
        private bool _checkRatios;
        private bool _checkDilutions;
        private bool _isImprintable;
        private string _visualDescriptionValue;

        private BindingList<Product> _products;
        private BindingList<AutoItemRelation> _parents;
        private BindingList<AutoItemRelation> _children;

        #endregion

        #region LookupEnums

        /// <summary>
        /// AutoItemType Enum
        /// </summary>
        public AutoItemType TypeEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<AutoItemType>(Type);
            }
        }

        /// <summary>
        /// AutoItemGender Enum
        /// </summary>
        public ItemGender GenderEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<ItemGender>(Gender);
            }
        }

        /// <summary>
        /// AutoItemStructureType Enum
        /// </summary>
        public AutoItemStructureType StructureTypeEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<AutoItemStructureType>(StructureType);
            }
        }

        /// <summary>
        /// AutoItemStatus Enum
        /// </summary>
        public AutoItemStatus StatusEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<AutoItemStatus>(Status);
            }
        }

        /// <summary>
        /// AutoItemChildsOrderType Enum
        /// </summary>
        public ChildsOrderType ChildsOrderTypeEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<ChildsOrderType>(ChildsOrderType);
            }
        }

        /// <summary>
        /// AutoItemChildsScanningType Enum
        /// </summary>
        public ChildsScanningType ChildsScanningTypeEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<ChildsScanningType>(ChildsScanningType);
            }
        }

        /// <summary>
        /// AutoItemScanningMethod Enum
        /// </summary>
        public AutoItemScanningMethod ScanningMethodEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<AutoItemScanningMethod>(ScanningMethod);
            }
        }

        #endregion

        #region MemoryOnly Properties

        /// <summary>
        /// Return custom description value based on case
        /// </summary>
        public string VisualDescription
        {
            get
            {
                if (!string.IsNullOrEmpty(_visualDescriptionValue))
                {
                    return _visualDescriptionValue;
                }

                _visualDescriptionValue =  Type == null || TypeEnum != AutoItemType.AutoItemTypeProduct? NameOrDescription: ProductDescription;

                return _visualDescriptionValue;
            }
        }

        /// <summary>
        /// Return Description, Full Name Or Name as ordered and based on what is available
        /// </summary>
        public string NameOrDescription
        {
            get
            {
                return string.IsNullOrEmpty(Description)? (string.IsNullOrEmpty(FullName) ? Name: FullName): Description;
            }
        }

        /// <summary>
        /// Description of product typed items that is generated based on Supports, Suggested Usage & Ingredients
        /// </summary>
        public string ProductDescription
        {
            get
            {
                if (Type == null || TypeEnum != AutoItemType.AutoItemTypeProduct)
                {
                    return NameOrDescription;
                }

                if (!string.IsNullOrEmpty(Description))
                {
                    return Description;
                }

                var product = Products.FirstOrDefault();

                if (product == null || (string.IsNullOrEmpty(product.Supports) && string.IsNullOrEmpty(product.IngredientsString)))
                {
                    return NameOrDescription;
                }

                var supports = product.Supports;
                var contains = product.IngredientsString;
                
                var firstForm = product.ProductForms.FirstOrDefault();

                var suggestedUsage = firstForm == null || string.IsNullOrEmpty(firstForm.SuggestedUsage) ? string.Empty:firstForm.SuggestedUsage;

                var firstSize = firstForm == null ? null : firstForm.ProductSizes.FirstOrDefault();

                var formSizeString = firstForm == null || firstSize == null? string.Empty : firstForm.Form + ", " + firstSize.Size;

                return (string.IsNullOrEmpty(suggestedUsage) ? string.Empty : "Supports:" + Environment.NewLine + supports + Environment.NewLine + Environment.NewLine) +
                       (string.IsNullOrEmpty(suggestedUsage) ? string.Empty : "Suggested Usage:" + Environment.NewLine + suggestedUsage + Environment.NewLine + Environment.NewLine) +
                       (string.IsNullOrEmpty(suggestedUsage) ? string.Empty : "Contains:" + Environment.NewLine + contains + Environment.NewLine + Environment.NewLine) +
                       (string.IsNullOrEmpty(formSizeString) ? string.Empty : formSizeString);
            }
        }

        /// <summary>
        /// Indicates if item is checked to be added as a test result manually
        /// </summary>
        public bool IsChecked { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the TestingPoint.
        /// </summary>
        public TestingPoint TestingPoint
        {
            get { return _testingPoint; }
            set
            {
                if (_testingPoint == null || !_testingPoint.Equals(value))
                {
                    _testingPoint = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_testingPoint == null) return;
                _testingPoint.PropertyChanged += TestingPoint_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the Image.
        /// </summary>
        public Image Image
        {
            get { return _image; }
            set
            {
                if (_image == null || !_image.Equals(value))
                {
                    _image = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_image == null) return;
                _image.PropertyChanged += Image_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        public Lookup Type
        {
            get { return _type; }
            set
            {
                if (_type == null || !_type.Equals(value))
                {
                    _type = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_type == null) return;
                _type.PropertyChanged += Type_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the GenderLookup.
        /// </summary>
        public Lookup Gender
        {
            get { return _gender; }
            set
            {
                if (_gender == null || !_gender.Equals(value))
                {
                    _gender = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_gender == null) return;
                _gender.PropertyChanged += GenderLookup_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the StructureType.
        /// </summary>
        public Lookup StructureType
        {
            get { return _structureType; }
            set
            {
                if (_structureType == null || !_structureType.Equals(value))
                {
                    _structureType = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_structureType == null) return;
                _structureType.PropertyChanged += StructureType_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public Lookup Status
        {
            get { return _status; }
            set
            {
                if (_status == null || !_status.Equals(value))
                {
                    _status = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_status == null) return;
                _status.PropertyChanged += Status_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the ChildsOrderType.
        /// </summary>
        public Lookup ChildsOrderType
        {
            get { return _childsOrderType; }
            set
            {
                if (_childsOrderType == null || !_childsOrderType.Equals(value))
                {
                    _childsOrderType = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_childsOrderType == null) return;
                _childsOrderType.PropertyChanged += ChildsOrderType_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the ChildsScanningType.
        /// </summary>
        public Lookup ChildsScanningType
        {
            get { return _childsScanningType; }
            set
            {
                if (_childsScanningType == null || !_childsScanningType.Equals(value))
                {
                    _childsScanningType = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_childsScanningType == null) return;
                _childsScanningType.PropertyChanged += ChildsScanningType_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the ScanningMethod.
        /// </summary>
        public Lookup ScanningMethod
        {
            get { return _scanningMethod; }
            set
            {
                if (_scanningMethod == null || !_scanningMethod.Equals(value))
                {
                    _scanningMethod = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_scanningMethod == null) return;
                _scanningMethod.PropertyChanged += ScanningMethod_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the ScansNumber.
        /// </summary>
        public int ScansNumber
        {
            get { return _scansNumber; }
            set
            {
                if (!_scansNumber.Equals(value))
                {
                    _scansNumber = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the MatchesNumber.
        /// </summary>
        public int MatchesNumber
        {
            get { return _matchesNumber; }
            set
            {
                if (_matchesNumber == null || !_matchesNumber.Equals(value))
                {
                    _matchesNumber = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public string Key
        {
            get { return _key; }
            set
            {
                if (_key == null || !_key.Equals(value))
                {
                    _key = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == null || !_name.Equals(value))
                {
                    _name = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the FullName.
        /// </summary>
        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (_fullName == null || !_fullName.Equals(value))
                {
                    _fullName = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == null || !_description.Equals(value))
                {
                    _description = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Frequency.
        /// </summary>
        public string Frequency
        {
            get { return _frequency; }
            set
            {
                if (_frequency == null || !_frequency.Equals(value))
                {
                    _frequency = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the ModelIdentifier.
        /// </summary>
        public string ModelIdentifier
        {
            get { return _modelIdentifier; }
            set
            {
                if (_modelIdentifier == null || !_modelIdentifier.Equals(value))
                {
                    _modelIdentifier = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the UserNotes.
        /// </summary>
        public string UserNotes
        {
            get { return _userNotes; }
            set
            {
                if (_userNotes == null || !_userNotes.Equals(value))
                {
                    _userNotes = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the DirectAccessChecks.
        /// </summary>
        public string DirectAccessChecks
        {
            get { return _directAccessChecks; }
            set
            {
                if (_directAccessChecks == null || !_directAccessChecks.Equals(value))
                {
                    _directAccessChecks = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the IsUserItem.
        /// </summary>
        public bool IsUserItem
        {
            get { return _isUserItem; }
            set
            {
                if (!_isUserItem.Equals(value))
                {
                    _isUserItem = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the IsSearchable.
        /// </summary>
        public bool IsSearchable
        {
            get { return _isSearchable; }
            set
            {
                if (!_isSearchable.Equals(value))
                {
                    _isSearchable = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the InsertOnNo.
        /// </summary>
        public bool InsertOnNo
        {
            get { return _insertOnNo; }
            set
            {
                if (!_insertOnNo.Equals(value))
                {
                    _insertOnNo = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the FinishAllScanRounds.
        /// </summary>
        public bool FinishAllScanRounds
        {
            get { return _finishAllScanRounds; }
            set
            {
                if (_finishAllScanRounds == null || !_finishAllScanRounds.Equals(value))
                {
                    _finishAllScanRounds = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the AddResultOnMatch.
        /// </summary>
        public bool AddResultOnMatch
        {
            get { return _addResultOnMatch; }
            set
            {
                if (_addResultOnMatch == null || !_addResultOnMatch.Equals(value))
                {
                    _addResultOnMatch = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the ExcludeOnMatch.
        /// </summary>
        public bool ExcludeOnMatch
        {
            get { return _excludeOnMatch; }
            set
            {
                if (_excludeOnMatch == null || !_excludeOnMatch.Equals(value))
                {
                    _excludeOnMatch = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the AddAllChildesOnMatch.
        /// </summary>
        public bool AddAllChildesOnMatch
        {
            get { return _addAllChildesOnMatch; }
            set
            {
                if (_addAllChildesOnMatch == null || !_addAllChildesOnMatch.Equals(value))
                {
                    _addAllChildesOnMatch = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the CheckVitalForce.
        /// </summary>
        public bool CheckVitalForce
        {
            get { return _checkVitalForce; }
            set
            {
                if (!_checkVitalForce.Equals(value))
                {
                    _checkVitalForce = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the CheckFourFactors.
        /// </summary>
        public bool CheckFourFactors
        {
            get { return _checkFourFactors; }
            set
            {
                if (!_checkFourFactors.Equals(value))
                {
                    _checkFourFactors = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the CheckRatios.
        /// </summary>
        public bool CheckRatios
        {
            get { return _checkRatios; }
            set
            {
                if (!_checkRatios.Equals(value))
                {
                    _checkRatios = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the CheckDilutions.
        /// </summary>
        public bool CheckDilutions
        {
            get { return _checkDilutions; }
            set
            {
                if (!_checkDilutions.Equals(value))
                {
                    _checkDilutions = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the IsImprintable.
        /// </summary>
        public bool IsImprintable
        {
            get { return _isImprintable; }
            set
            {
                if (!_isImprintable.Equals(value))
                {
                    _isImprintable = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Products.
        /// </summary>
        public BindingList<Product> Products
        {
            get { return _products; }
            set
            {
                if (_products != null && _products == value) return;

                _products = value;
                _products.RaiseListChangedEvents = true;
                _products.ListChanged += Products_ListChanged;
            }
        }

        /// <summary>
        /// Gets or sets the Parents.
        /// </summary>
        public BindingList<AutoItemRelation> Parents
        {
            get { return _parents; }
            set
            {
                if (_parents != null && _parents == value) return;

                _parents = value;
                _parents.RaiseListChangedEvents = true;
                _parents.ListChanged += Parents_ListChanged;
            }
        }

        /// <summary>
        /// Gets or sets the Parents.
        /// </summary>
        public BindingList<AutoItemRelation> Children
        {
            get { return _children; }
            set
            {
                if (_children != null && _children == value) return;

                _children = value;
                _children.RaiseListChangedEvents = true;
                _children.ListChanged += Children_ListChanged;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the state for the TestingPoint.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestingPoint_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => TestingPoint), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => TestingPoint, () => TestingPoint.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the Image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Image), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => Image, () => Image.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the Type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Type_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Type), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => Type, () => Type.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the GenderLookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenderLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Gender), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => Gender, () => Gender.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the StructureType.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StructureType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => StructureType), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => StructureType, () => StructureType.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the Status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Status_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Status), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => Status, () => Status.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the ChildsOrderType.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildsOrderType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ChildsOrderType), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => ChildsOrderType, () => ChildsOrderType.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the ChildsScanningType.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildsScanningType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ChildsScanningType), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => ChildsScanningType, () => ChildsScanningType.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the ScanningMethod.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanningMethod_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ScanningMethod), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => ScanningMethod, () => ScanningMethod.Id));
        }

        /// <summary>
        /// Notifies the change of the Products.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Products_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Products), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => Products));
        }

        /// <summary>
        /// Notifies the change of the Parents.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Parents_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Parents), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => Parents));
        }

        /// <summary>
        /// Notifies the change of the Parents.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Children_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Children), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => Children));
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => TestingPoint))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Image))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Type) && !(Type == null || Type.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Gender) && !(Gender == null || Gender.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => StructureType) && !(StructureType == null || StructureType.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Status) && !(Status == null || Status.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ChildsOrderType) && !(ChildsOrderType == null || ChildsOrderType.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ChildsScanningType) && !(ChildsScanningType == null || ChildsScanningType.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ScanningMethod) && !(ScanningMethod == null || ScanningMethod.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ScansNumber))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => MatchesNumber))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => FinishAllScanRounds))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => AddResultOnMatch))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ExcludeOnMatch))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => AddAllChildesOnMatch))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Key))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Name))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => FullName))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Description))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Frequency))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ModelIdentifier))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => UserNotes))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => DirectAccessChecks))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => IsUserItem))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => IsSearchable))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => InsertOnNo))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => CheckVitalForce))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => CheckFourFactors))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => CheckRatios))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => CheckDilutions))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => IsImprintable))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Products) && !ValidateProducts())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Parents) && !ValidateParents())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Children) && !ValidateChildren())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            UpdateErrorsSummary(info);
        }
        
        /// <summary>
        /// Validates the Product.
        /// </summary>
        /// <returns></returns>
        public bool ValidateProducts()
        {
            return Products.All(products => products.Validate());
        }

        /// <summary>
        /// Validates the Parents.
        /// </summary>
        /// <returns></returns>
        public bool ValidateParents()
        {
            return Parents.All(parents => parents.Validate());
        }

        /// <summary>
        /// Validates the Children.
        /// </summary>
        /// <returns></returns>
        public bool ValidateChildren()
        {
            return Children.All(children => children.Validate());
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