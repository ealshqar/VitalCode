///////////////////////////////////////////////////////////////
// This is generated code. 
//////////////////////////////////////////////////////////////
// Code is generated using LLBLGen Pro version: 3.5
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates.NET20
// Templates vendor: Solutions Design.
// Templates version: 
//////////////////////////////////////////////////////////////
using System;

namespace Vital.DataLayer
{
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: AppImage.</summary>
	public enum AppImageFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Property. </summary>
		Property,
		///<summary>Value. </summary>
		Value,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: AppInfo.</summary>
	public enum AppInfoFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Property. </summary>
		Property,
		///<summary>Value. </summary>
		Value,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: AutoItem.</summary>
	public enum AutoItemFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>TestingPointsId. </summary>
		TestingPointsId,
		///<summary>ImageId. </summary>
		ImageId,
		///<summary>TypeLookupId. </summary>
		TypeLookupId,
		///<summary>StructureTypeLookupId. </summary>
		StructureTypeLookupId,
		///<summary>StatusLookupId. </summary>
		StatusLookupId,
		///<summary>ChildsOrderTypeLookupId. </summary>
		ChildsOrderTypeLookupId,
		///<summary>ChildsScanningTypeLookupId. </summary>
		ChildsScanningTypeLookupId,
		///<summary>ScanningMethodLookupId. </summary>
		ScanningMethodLookupId,
		///<summary>ScansNumber. </summary>
		ScansNumber,
		///<summary>Key. </summary>
		Key,
		///<summary>Name. </summary>
		Name,
		///<summary>FullName. </summary>
		FullName,
		///<summary>Description. </summary>
		Description,
		///<summary>Frequency. </summary>
		Frequency,
		///<summary>UserNotes. </summary>
		UserNotes,
		///<summary>IsUserItem. </summary>
		IsUserItem,
		///<summary>IsSearchable. </summary>
		IsSearchable,
		///<summary>InsertOnNo. </summary>
		InsertOnNo,
		///<summary>IsImprintable. </summary>
		IsImprintable,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>MatchesNumber. </summary>
		MatchesNumber,
		///<summary>FinishAllScanRounds. </summary>
		FinishAllScanRounds,
		///<summary>AddResultOnMatch. </summary>
		AddResultOnMatch,
		///<summary>ExcludeOnMatch. </summary>
		ExcludeOnMatch,
		///<summary>AddAllChildesOnMatch. </summary>
		AddAllChildesOnMatch,
		///<summary>ModelIdentifier. </summary>
		ModelIdentifier,
		///<summary>DirectAccessChecks. </summary>
		DirectAccessChecks,
		///<summary>GenderLookupId. </summary>
		GenderLookupId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: AutoItemRelation.</summary>
	public enum AutoItemRelationFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>AutoItemParentId. </summary>
		AutoItemParentId,
		///<summary>AutoItemChildId. </summary>
		AutoItemChildId,
		///<summary>Order. </summary>
		Order,
		///<summary>IsDeleted. </summary>
		IsDeleted,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: AutoProtocol.</summary>
	public enum AutoProtocolFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>Name. </summary>
		Name,
		///<summary>Key. </summary>
		Key,
		///<summary>IsSystemProtocol. </summary>
		IsSystemProtocol,
		///<summary>IsDefaultProtocol. </summary>
		IsDefaultProtocol,
		///<summary>IsDeleted. </summary>
		IsDeleted,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: AutoProtocolRevision.</summary>
	public enum AutoProtocolRevisionFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>AutoProtocolsId. </summary>
		AutoProtocolsId,
		///<summary>Name. </summary>
		Name,
		///<summary>Key. </summary>
		Key,
		///<summary>IsSystemProtocol. </summary>
		IsSystemProtocol,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: AutoProtocolStage.</summary>
	public enum AutoProtocolStageFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>AutoProtocolsId. </summary>
		AutoProtocolsId,
		///<summary>AutoTestStagesId. </summary>
		AutoTestStagesId,
		///<summary>StageItemsOrderTypeLookupId. </summary>
		StageItemsOrderTypeLookupId,
		///<summary>Order. </summary>
		Order,
		///<summary>IsDeleted. </summary>
		IsDeleted,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: AutoProtocolStageRevision.</summary>
	public enum AutoProtocolStageRevisionFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>AutoProtocolRevisionsId. </summary>
		AutoProtocolRevisionsId,
		///<summary>AutoProtocolStagesId. </summary>
		AutoProtocolStagesId,
		///<summary>AutoTestStagesId. </summary>
		AutoTestStagesId,
		///<summary>Order. </summary>
		Order,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: AutoTest.</summary>
	public enum AutoTestFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>PatientId. </summary>
		PatientId,
		///<summary>AutoProtocolRevisionsId. </summary>
		AutoProtocolRevisionsId,
		///<summary>Name. </summary>
		Name,
		///<summary>Description. </summary>
		Description,
		///<summary>Notes. </summary>
		Notes,
		///<summary>TestDate. </summary>
		TestDate,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: AutoTestResult.</summary>
	public enum AutoTestResultFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>AutoTestsId. </summary>
		AutoTestsId,
		///<summary>AutoItemsId. </summary>
		AutoItemsId,
		///<summary>AutoProtocolStageRevisionsId. </summary>
		AutoProtocolStageRevisionsId,
		///<summary>PreliminaryReading. </summary>
		PreliminaryReading,
		///<summary>SummaryReading. </summary>
		SummaryReading,
		///<summary>IsAddedManually. </summary>
		IsAddedManually,
		///<summary>Notes. </summary>
		Notes,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>AutoTestResultsParentId. </summary>
		AutoTestResultsParentId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: AutoTestResultProduct.</summary>
	public enum AutoTestResultProductFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>AutoTestResultsId. </summary>
		AutoTestResultsId,
		///<summary>Quantity. </summary>
		Quantity,
		///<summary>Price. </summary>
		Price,
		///<summary>Duration. </summary>
		Duration,
		///<summary>Schedule. </summary>
		Schedule,
		///<summary>SuggestedUsage. </summary>
		SuggestedUsage,
		///<summary>Comments. </summary>
		Comments,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>IsChecked. </summary>
		IsChecked,
		///<summary>ProductFormsId. </summary>
		ProductFormsId,
		///<summary>ProductSizesId. </summary>
		ProductSizesId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: AutoTestStage.</summary>
	public enum AutoTestStageFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>StageItemsOrderTypeLookupId. </summary>
		StageItemsOrderTypeLookupId,
		///<summary>Name. </summary>
		Name,
		///<summary>Key. </summary>
		Key,
		///<summary>Description. </summary>
		Description,
		///<summary>Dependencies. </summary>
		Dependencies,
		///<summary>IsMultiLevel. </summary>
		IsMultiLevel,
		///<summary>ScanTypeEnabled. </summary>
		ScanTypeEnabled,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>StageTabKey. </summary>
		StageTabKey,
		///<summary>IsDestinationOnly. </summary>
		IsDestinationOnly,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: ClinicProductPricing.</summary>
	public enum ClinicProductPricingFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>ProductsId. </summary>
		ProductsId,
		///<summary>Form. </summary>
		Form,
		///<summary>Size. </summary>
		Size,
		///<summary>Price. </summary>
		Price,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: DosageOption.</summary>
	public enum DosageOptionFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>ProductFormsId. </summary>
		ProductFormsId,
		///<summary>Order. </summary>
		Order,
		///<summary>Name. </summary>
		Name,
		///<summary>UsageSchedule. </summary>
		UsageSchedule,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>SuggestedUsage. </summary>
		SuggestedUsage,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: FrequencyTest.</summary>
	public enum FrequencyTestFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>PatientId. </summary>
		PatientId,
		///<summary>Name. </summary>
		Name,
		///<summary>Notes. </summary>
		Notes,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>UserId. </summary>
		UserId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: FrequencyTestResult.</summary>
	public enum FrequencyTestResultFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>FrequencyTestId. </summary>
		FrequencyTestId,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>Notes. </summary>
		Notes,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>UserId. </summary>
		UserId,
		///<summary>TimesPerWeek. </summary>
		TimesPerWeek,
		///<summary>NumberOfWeeks. </summary>
		NumberOfWeeks,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: HwProfile.</summary>
	public enum HwProfileFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Name. </summary>
		Name,
		///<summary>MinReading. </summary>
		MinReading,
		///<summary>DisconnectedTimeout. </summary>
		DisconnectedTimeout,
		///<summary>StabilityTimeout. </summary>
		StabilityTimeout,
		///<summary>StabilityRange. </summary>
		StabilityRange,
		///<summary>IsSystemProfile. </summary>
		IsSystemProfile,
		///<summary>IsDefault. </summary>
		IsDefault,
		///<summary>Image. </summary>
		Image,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>Key. </summary>
		Key,
		///<summary>DefaultMinReading. </summary>
		DefaultMinReading,
		///<summary>DefaultDisconnectedTimeout. </summary>
		DefaultDisconnectedTimeout,
		///<summary>DefaultStabilityTimeout. </summary>
		DefaultStabilityTimeout,
		///<summary>DefaultStabilityRange. </summary>
		DefaultStabilityRange,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: Image.</summary>
	public enum ImageFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Data. </summary>
		Data,
		///<summary>Extension. </summary>
		Extension,
		///<summary>Path. </summary>
		Path,
		///<summary>Size. </summary>
		Size,
		///<summary>OldImageBoxWidth. </summary>
		OldImageBoxWidth,
		///<summary>OldImageBoxHeight. </summary>
		OldImageBoxHeight,
		///<summary>Description. </summary>
		Description,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: Invoice.</summary>
	public enum InvoiceFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Number. </summary>
		Number,
		///<summary>TestId. </summary>
		TestId,
		///<summary>Comments. </summary>
		Comments,
		///<summary>TotalAmount. </summary>
		TotalAmount,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>ChequeNumber. </summary>
		ChequeNumber,
		///<summary>PaymentMethod. </summary>
		PaymentMethod,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: IssueNavigationStep.</summary>
	public enum IssueNavigationStepFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>TestIssueId. </summary>
		TestIssueId,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>ParentId. </summary>
		ParentId,
		///<summary>Order. </summary>
		Order,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>UserId. </summary>
		UserId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: Item.</summary>
	public enum ItemFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Name. </summary>
		Name,
		///<summary>FullName. </summary>
		FullName,
		///<summary>Description. </summary>
		Description,
		///<summary>ItemMemo. </summary>
		ItemMemo,
		///<summary>GenderLookupId. </summary>
		GenderLookupId,
		///<summary>TypeLookupId. </summary>
		TypeLookupId,
		///<summary>ListTypeLookupId. </summary>
		ListTypeLookupId,
		///<summary>ItemDetailId. </summary>
		ItemDetailId,
		///<summary>Order. </summary>
		Order,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>ItemCsabinaryCode. </summary>
		ItemCsabinaryCode,
		///<summary>IsStarred. </summary>
		IsStarred,
		///<summary>ItemSourceLookupId. </summary>
		ItemSourceLookupId,
		///<summary>Key. </summary>
		Key,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: ItemDetails.</summary>
	public enum ItemDetailsFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>ImageId. </summary>
		ImageId,
		///<summary>X. </summary>
		X,
		///<summary>Y. </summary>
		Y,
		///<summary>UserId. </summary>
		UserId,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: ItemProperty.</summary>
	public enum ItemPropertyFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>PropertyId. </summary>
		PropertyId,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>Value. </summary>
		Value,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: ItemRelation.</summary>
	public enum ItemRelationFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>ItemParentId. </summary>
		ItemParentId,
		///<summary>ItemChildId. </summary>
		ItemChildId,
		///<summary>RelationTypeLookupId. </summary>
		RelationTypeLookupId,
		///<summary>Order. </summary>
		Order,
		///<summary>Step. </summary>
		Step,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: ItemRelationProperty.</summary>
	public enum ItemRelationPropertyFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>PropertyId. </summary>
		PropertyId,
		///<summary>ItemRelationId. </summary>
		ItemRelationId,
		///<summary>Value. </summary>
		Value,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: ItemTarget.</summary>
	public enum ItemTargetFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>TargetTypeLookupId. </summary>
		TargetTypeLookupId,
		///<summary>UserId. </summary>
		UserId,
		///<summary>ItemTargetCreationDateTime. </summary>
		ItemTargetCreationDateTime,
		///<summary>ItemTargetUpdatedDateTime. </summary>
		ItemTargetUpdatedDateTime,
		///<summary>Order. </summary>
		Order,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: Lookup.</summary>
	public enum LookupFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Value. </summary>
		Value,
		///<summary>Type. </summary>
		Type,
		///<summary>Key. </summary>
		Key,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: OrderItem.</summary>
	public enum OrderItemFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>ShippingOrderId. </summary>
		ShippingOrderId,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>Quantity. </summary>
		Quantity,
		///<summary>Comments. </summary>
		Comments,
		///<summary>Include. </summary>
		Include,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>UserId. </summary>
		UserId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: Patient.</summary>
	public enum PatientFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Number. </summary>
		Number,
		///<summary>FirstName. </summary>
		FirstName,
		///<summary>LastName. </summary>
		LastName,
		///<summary>Address1. </summary>
		Address1,
		///<summary>Address2. </summary>
		Address2,
		///<summary>City. </summary>
		City,
		///<summary>State. </summary>
		State,
		///<summary>Zip. </summary>
		Zip,
		///<summary>GenderLookupId. </summary>
		GenderLookupId,
		///<summary>DateOfBirth. </summary>
		DateOfBirth,
		///<summary>HomePhone. </summary>
		HomePhone,
		///<summary>WorkPhone. </summary>
		WorkPhone,
		///<summary>CellPhone. </summary>
		CellPhone,
		///<summary>Fax. </summary>
		Fax,
		///<summary>Email. </summary>
		Email,
		///<summary>Notes. </summary>
		Notes,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: PatientHistory.</summary>
	public enum PatientHistoryFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>PatientId. </summary>
		PatientId,
		///<summary>Name. </summary>
		Name,
		///<summary>Description. </summary>
		Description,
		///<summary>TypeLookupId. </summary>
		TypeLookupId,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: Product.</summary>
	public enum ProductFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>AutoItemsId. </summary>
		AutoItemsId,
		///<summary>Supplier. </summary>
		Supplier,
		///<summary>IngredientsString. </summary>
		IngredientsString,
		///<summary>Supports. </summary>
		Supports,
		///<summary>UsefulFor. </summary>
		UsefulFor,
		///<summary>Price. </summary>
		Price,
		///<summary>DiscountPercentage. </summary>
		DiscountPercentage,
		///<summary>HasDiscount. </summary>
		HasDiscount,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: ProductForm.</summary>
	public enum ProductFormFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>ProductsId. </summary>
		ProductsId,
		///<summary>StatusLookupId. </summary>
		StatusLookupId,
		///<summary>Form. </summary>
		Form,
		///<summary>SuggestedUsage. </summary>
		SuggestedUsage,
		///<summary>UsageSchedule. </summary>
		UsageSchedule,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: ProductSize.</summary>
	public enum ProductSizeFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>ProductFormsId. </summary>
		ProductFormsId,
		///<summary>StatusLookupsId. </summary>
		StatusLookupsId,
		///<summary>Size. </summary>
		Size,
		///<summary>Price. </summary>
		Price,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: Property.</summary>
	public enum PropertyFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Name. </summary>
		Name,
		///<summary>Key. </summary>
		Key,
		///<summary>Description. </summary>
		Description,
		///<summary>ApplicableTypeLookupId. </summary>
		ApplicableTypeLookupId,
		///<summary>ValueTypeLookupId. </summary>
		ValueTypeLookupId,
		///<summary>SourceConfig. </summary>
		SourceConfig,
		///<summary>MembersConfig. </summary>
		MembersConfig,
		///<summary>Caption. </summary>
		Caption,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: ProtocolItem.</summary>
	public enum ProtocolItemFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>TestProtocolId. </summary>
		TestProtocolId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>UserId. </summary>
		UserId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: ProtocolStep.</summary>
	public enum ProtocolStepFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>TestProtocolId. </summary>
		TestProtocolId,
		///<summary>Order. </summary>
		Order,
		///<summary>TypeLookupId. </summary>
		TypeLookupId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>UserId. </summary>
		UserId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: Reading.</summary>
	public enum ReadingFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>TestId. </summary>
		TestId,
		///<summary>DateTime. </summary>
		DateTime,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>Value. </summary>
		Value,
		///<summary>Min. </summary>
		Min,
		///<summary>Max. </summary>
		Max,
		///<summary>Rise. </summary>
		Rise,
		///<summary>Fall. </summary>
		Fall,
		///<summary>ValueBalanced. </summary>
		ValueBalanced,
		///<summary>ListPointLookupId. </summary>
		ListPointLookupId,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>PointSetItemId. </summary>
		PointSetItemId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: ScheduleLine.</summary>
	public enum ScheduleLineFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>TestScheduleId. </summary>
		TestScheduleId,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>Notes. </summary>
		Notes,
		///<summary>Duration. </summary>
		Duration,
		///<summary>ToBeShipped. </summary>
		ToBeShipped,
		///<summary>WhenArising. </summary>
		WhenArising,
		///<summary>Breakfast. </summary>
		Breakfast,
		///<summary>BetweenMealsEarly. </summary>
		BetweenMealsEarly,
		///<summary>Lunch. </summary>
		Lunch,
		///<summary>BetweenMealsLate. </summary>
		BetweenMealsLate,
		///<summary>Dinner. </summary>
		Dinner,
		///<summary>BeforeSleep. </summary>
		BeforeSleep,
		///<summary>NoPerBottle. </summary>
		NoPerBottle,
		///<summary>NoOfBottle. </summary>
		NoOfBottle,
		///<summary>Price. </summary>
		Price,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>UserId. </summary>
		UserId,
		///<summary>IsDeleted. </summary>
		IsDeleted,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: Service.</summary>
	public enum ServiceFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Key. </summary>
		Key,
		///<summary>Name. </summary>
		Name,
		///<summary>Description. </summary>
		Description,
		///<summary>Comments. </summary>
		Comments,
		///<summary>Price. </summary>
		Price,
		///<summary>IsDefault. </summary>
		IsDefault,
		///<summary>TypeLookupId. </summary>
		TypeLookupId,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>DefaultName. </summary>
		DefaultName,
		///<summary>DefaultDescription. </summary>
		DefaultDescription,
		///<summary>DefaultComments. </summary>
		DefaultComments,
		///<summary>DefaultPrice. </summary>
		DefaultPrice,
		///<summary>DefaultIsDefault. </summary>
		DefaultIsDefault,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: Setting.</summary>
	public enum SettingFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Name. </summary>
		Name,
		///<summary>Key. </summary>
		Key,
		///<summary>Value. </summary>
		Value,
		///<summary>Description. </summary>
		Description,
		///<summary>ValueTypeLookupId. </summary>
		ValueTypeLookupId,
		///<summary>SettingGroupLookupId. </summary>
		SettingGroupLookupId,
		///<summary>SourceConfig. </summary>
		SourceConfig,
		///<summary>MembersConfig. </summary>
		MembersConfig,
		///<summary>Caption. </summary>
		Caption,
		///<summary>DefaultValue. </summary>
		DefaultValue,
		///<summary>IsVisible. </summary>
		IsVisible,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: ShippingOrder.</summary>
	public enum ShippingOrderFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Number. </summary>
		Number,
		///<summary>TestId. </summary>
		TestId,
		///<summary>SentDate. </summary>
		SentDate,
		///<summary>SendToClient. </summary>
		SendToClient,
		///<summary>Sent. </summary>
		Sent,
		///<summary>Comments. </summary>
		Comments,
		///<summary>PatientFirstName. </summary>
		PatientFirstName,
		///<summary>PatientLastName. </summary>
		PatientLastName,
		///<summary>PatientAddress1. </summary>
		PatientAddress1,
		///<summary>PatientAddress2. </summary>
		PatientAddress2,
		///<summary>PatientCity. </summary>
		PatientCity,
		///<summary>PatientState. </summary>
		PatientState,
		///<summary>PatientZip. </summary>
		PatientZip,
		///<summary>PatientHomePhone. </summary>
		PatientHomePhone,
		///<summary>PatientWorkPhone. </summary>
		PatientWorkPhone,
		///<summary>PatientCellPhone. </summary>
		PatientCellPhone,
		///<summary>PatientFax. </summary>
		PatientFax,
		///<summary>PatientEmail. </summary>
		PatientEmail,
		///<summary>TechnicianName. </summary>
		TechnicianName,
		///<summary>TechnicianAddress. </summary>
		TechnicianAddress,
		///<summary>TechnicianState. </summary>
		TechnicianState,
		///<summary>TechnicianZipCode. </summary>
		TechnicianZipCode,
		///<summary>TechnicianCity. </summary>
		TechnicianCity,
		///<summary>TechnicianPhone. </summary>
		TechnicianPhone,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>UserId. </summary>
		UserId,
		///<summary>ShippingMethodLookupId. </summary>
		ShippingMethodLookupId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: SpotCheck.</summary>
	public enum SpotCheckFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>PatientId. </summary>
		PatientId,
		///<summary>Notes. </summary>
		Notes,
		///<summary>Name. </summary>
		Name,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CapsoleTnotes. </summary>
		CapsoleTnotes,
		///<summary>IngredientsNotes. </summary>
		IngredientsNotes,
		///<summary>MondayNotes. </summary>
		MondayNotes,
		///<summary>TuesdayNotes. </summary>
		TuesdayNotes,
		///<summary>WednesdayNotes. </summary>
		WednesdayNotes,
		///<summary>MineralsThree. </summary>
		MineralsThree,
		///<summary>MineralsOne. </summary>
		MineralsOne,
		///<summary>MineralsIvPush. </summary>
		MineralsIvPush,
		///<summary>MineralsSterlieWaterCc. </summary>
		MineralsSterlieWaterCc,
		///<summary>MineralsSterlieWaterCcpriority. </summary>
		MineralsSterlieWaterCcpriority,
		///<summary>MineralsDextroseCc. </summary>
		MineralsDextroseCc,
		///<summary>MineralsDextroseCcpriority. </summary>
		MineralsDextroseCcpriority,
		///<summary>MineralsNormalSalineCc. </summary>
		MineralsNormalSalineCc,
		///<summary>MineralsNormalSalineCcpriority. </summary>
		MineralsNormalSalineCcpriority,
		///<summary>MineralsIvperMin. </summary>
		MineralsIvperMin,
		///<summary>MineralsPerWeek. </summary>
		MineralsPerWeek,
		///<summary>MineralsEdta. </summary>
		MineralsEdta,
		///<summary>TestId. </summary>
		TestId,
		///<summary>IngredientsNumberOfBags. </summary>
		IngredientsNumberOfBags,
		///<summary>IngredientsNumberPerWeek. </summary>
		IngredientsNumberPerWeek,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: SpotCheckResult.</summary>
	public enum SpotCheckResultFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>SpotCheckId. </summary>
		SpotCheckId,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>UserId. </summary>
		UserId,
		///<summary>YesNo. </summary>
		YesNo,
		///<summary>NumberOfBags. </summary>
		NumberOfBags,
		///<summary>NumberOfWeeks. </summary>
		NumberOfWeeks,
		///<summary>Dosage. </summary>
		Dosage,
		///<summary>Notes. </summary>
		Notes,
		///<summary>ResultTypeId. </summary>
		ResultTypeId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: StageAnnouncement.</summary>
	public enum StageAnnouncementFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>Key. </summary>
		Key,
		///<summary>Text. </summary>
		Text,
		///<summary>AudioPath. </summary>
		AudioPath,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: StageAutoItem.</summary>
	public enum StageAutoItemFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>AutoProtocolStagesId. </summary>
		AutoProtocolStagesId,
		///<summary>StageAutoItemParentId. </summary>
		StageAutoItemParentId,
		///<summary>AutoItemsId. </summary>
		AutoItemsId,
		///<summary>ScanningMethodLookupId. </summary>
		ScanningMethodLookupId,
		///<summary>ChildsOrderTypeLookupId. </summary>
		ChildsOrderTypeLookupId,
		///<summary>ChildsScanningTypeLookupId. </summary>
		ChildsScanningTypeLookupId,
		///<summary>Order. </summary>
		Order,
		///<summary>ScansNumber. </summary>
		ScansNumber,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>MatchesNumber. </summary>
		MatchesNumber,
		///<summary>FinishAllScanRounds. </summary>
		FinishAllScanRounds,
		///<summary>DirectAccessChecks. </summary>
		DirectAccessChecks,
		///<summary>TestingPointsId. </summary>
		TestingPointsId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: Test.</summary>
	public enum TestFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>PatientId. </summary>
		PatientId,
		///<summary>TestScheduleId. </summary>
		TestScheduleId,
		///<summary>Name. </summary>
		Name,
		///<summary>PointsGroupId. </summary>
		PointsGroupId,
		///<summary>TestProtocolId. </summary>
		TestProtocolId,
		///<summary>DateTime. </summary>
		DateTime,
		///<summary>Description. </summary>
		Description,
		///<summary>TestTypeLookupId. </summary>
		TestTypeLookupId,
		///<summary>ListPointLookupId. </summary>
		ListPointLookupId,
		///<summary>TestStateLookupId. </summary>
		TestStateLookupId,
		///<summary>Notes. </summary>
		Notes,
		///<summary>NumberOfIssues. </summary>
		NumberOfIssues,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>EvalPeriodChecked. </summary>
		EvalPeriodChecked,
		///<summary>IsOrderSent. </summary>
		IsOrderSent,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: TestImprintableItem.</summary>
	public enum TestImprintableItemFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>TestId. </summary>
		TestId,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>ParentImprintableId. </summary>
		ParentImprintableId,
		///<summary>TestResultId. </summary>
		TestResultId,
		///<summary>IsChecked. </summary>
		IsChecked,
		///<summary>IsImprinted. </summary>
		IsImprinted,
		///<summary>Order. </summary>
		Order,
		///<summary>Comments. </summary>
		Comments,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: TestingPoint.</summary>
	public enum TestingPointFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>UserId. </summary>
		UserId,
		///<summary>Key. </summary>
		Key,
		///<summary>Name. </summary>
		Name,
		///<summary>FullName. </summary>
		FullName,
		///<summary>Hwidentifier. </summary>
		Hwidentifier,
		///<summary>Description. </summary>
		Description,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: TestIssue.</summary>
	public enum TestIssueFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>TestId. </summary>
		TestId,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>ProtocolStepId. </summary>
		ProtocolStepId,
		///<summary>Name. </summary>
		Name,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>UserId. </summary>
		UserId,
		///<summary>IsMainIssue. </summary>
		IsMainIssue,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: TestProtocol.</summary>
	public enum TestProtocolFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Name. </summary>
		Name,
		///<summary>Description. </summary>
		Description,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>UserId. </summary>
		UserId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: TestResult.</summary>
	public enum TestResultFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>IssueId. </summary>
		IssueId,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>DateTime. </summary>
		DateTime,
		///<summary>ParentId. </summary>
		ParentId,
		///<summary>SelectedParentId. </summary>
		SelectedParentId,
		///<summary>VitalForceId. </summary>
		VitalForceId,
		///<summary>StepTypeLookupId. </summary>
		StepTypeLookupId,
		///<summary>TestProtocolId. </summary>
		TestProtocolId,
		///<summary>IsSelected. </summary>
		IsSelected,
		///<summary>IsCurrent. </summary>
		IsCurrent,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>IsImprinted. </summary>
		IsImprinted,
		///<summary>ItemRatioId. </summary>
		ItemRatioId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: TestResultFactors.</summary>
	public enum TestResultFactorsFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>FactorItemId. </summary>
		FactorItemId,
		///<summary>TestResultId. </summary>
		TestResultId,
		///<summary>Reading. </summary>
		Reading,
		///<summary>PotencyItemId. </summary>
		PotencyItemId,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: TestSchedule.</summary>
	public enum TestScheduleFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Tax. </summary>
		Tax,
		///<summary>IsCash. </summary>
		IsCash,
		///<summary>IsCheck. </summary>
		IsCheck,
		///<summary>IsCreditCard. </summary>
		IsCreditCard,
		///<summary>ReevalInWeeks. </summary>
		ReevalInWeeks,
		///<summary>Notes. </summary>
		Notes,
		///<summary>SpecialInstructions. </summary>
		SpecialInstructions,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>EvalPeriodTypeLookupId. </summary>
		EvalPeriodTypeLookupId,
		///<summary>CheckNumber. </summary>
		CheckNumber,
		///<summary>Discount. </summary>
		Discount,
		///<summary>DiscountAsPercentage. </summary>
		DiscountAsPercentage,
		///<summary>DiscountApplyLookupId. </summary>
		DiscountApplyLookupId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: TestService.</summary>
	public enum TestServiceFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>TestId. </summary>
		TestId,
		///<summary>ServiceId. </summary>
		ServiceId,
		///<summary>Key. </summary>
		Key,
		///<summary>Name. </summary>
		Name,
		///<summary>Description. </summary>
		Description,
		///<summary>Comments. </summary>
		Comments,
		///<summary>Price. </summary>
		Price,
		///<summary>TypeLookupId. </summary>
		TypeLookupId,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: User.</summary>
	public enum UserFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Name. </summary>
		Name,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: VFS.</summary>
	public enum VFSFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Name. </summary>
		Name,
		///<summary>TestId. </summary>
		TestId,
		///<summary>DateTime. </summary>
		DateTime,
		///<summary>ThyroidNumOfIssues. </summary>
		ThyroidNumOfIssues,
		///<summary>MercuryNumOfIssues. </summary>
		MercuryNumOfIssues,
		///<summary>EmotionalIssues. </summary>
		EmotionalIssues,
		///<summary>Notes. </summary>
		Notes,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>UserId. </summary>
		UserId,
		///<summary>PatientId. </summary>
		PatientId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: VFSItem.</summary>
	public enum VFSItemFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>VFSId. </summary>
		VFSId,
		///<summary>VFSitemSourceId. </summary>
		VFSitemSourceId,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>PreviousV1. </summary>
		PreviousV1,
		///<summary>PreviousV2. </summary>
		PreviousV2,
		///<summary>CurrentV1. </summary>
		CurrentV1,
		///<summary>CurrentV2. </summary>
		CurrentV2,
		///<summary>IsSkipped. </summary>
		IsSkipped,
		///<summary>Comments. </summary>
		Comments,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>GroupLookupId. </summary>
		GroupLookupId,
		///<summary>SectionLookupId. </summary>
		SectionLookupId,
		///<summary>GridGroupLookupId. </summary>
		GridGroupLookupId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: VFSItemSource.</summary>
	public enum VFSItemSourceFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>V1Min. </summary>
		V1Min,
		///<summary>V1Max. </summary>
		V1Max,
		///<summary>V1MinIdeal. </summary>
		V1MinIdeal,
		///<summary>V1MaxIdeal. </summary>
		V1MaxIdeal,
		///<summary>V2Min. </summary>
		V2Min,
		///<summary>V2Max. </summary>
		V2Max,
		///<summary>V2MinIdeal. </summary>
		V2MinIdeal,
		///<summary>V2MaxIdeal. </summary>
		V2MaxIdeal,
		///<summary>StartingValue1. </summary>
		StartingValue1,
		///<summary>StartingValue2. </summary>
		StartingValue2,
		///<summary>IsActive. </summary>
		IsActive,
		///<summary>V1TypeLookupId. </summary>
		V1TypeLookupId,
		///<summary>V2TypeLookupId. </summary>
		V2TypeLookupId,
		///<summary>V1LookupType. </summary>
		V1LookupType,
		///<summary>V2LookupType. </summary>
		V2LookupType,
		///<summary>SectionLookupId. </summary>
		SectionLookupId,
		///<summary>GroupLookupId. </summary>
		GroupLookupId,
		///<summary>GenderLookupId. </summary>
		GenderLookupId,
		///<summary>TestingOrder. </summary>
		TestingOrder,
		///<summary>HasPreviousV1. </summary>
		HasPreviousV1,
		///<summary>HasPreviousV2. </summary>
		HasPreviousV2,
		///<summary>HasCurrentV1. </summary>
		HasCurrentV1,
		///<summary>HasCurrentV2. </summary>
		HasCurrentV2,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		///<summary>GridGroupLookupId. </summary>
		GridGroupLookupId,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: VFSSecondaryItem.</summary>
	public enum VFSSecondaryItemFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>VfsId. </summary>
		VfsId,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>SectionLookupId. </summary>
		SectionLookupId,
		///<summary>Comments. </summary>
		Comments,
		///<summary>Checked. </summary>
		Checked,
		///<summary>Order. </summary>
		Order,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}
	/// <summary>Index enum to fast-access EntityFields in the IEntityFields collection for the entity: VFSSecondaryItemSource.</summary>
	public enum VFSSecondaryItemSourceFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>ItemId. </summary>
		ItemId,
		///<summary>SectionLookupId. </summary>
		SectionLookupId,
		///<summary>UserId. </summary>
		UserId,
		///<summary>CreationDateTime. </summary>
		CreationDateTime,
		///<summary>UpdatedDateTime. </summary>
		UpdatedDateTime,
		/// <summary></summary>
		AmountOfFields
	}



	/// <summary>Enum definition for all the entity types defined in this namespace. Used by the entityfields factory.</summary>
	public enum EntityType
	{
		///<summary>AppImage</summary>
		AppImageEntity,
		///<summary>AppInfo</summary>
		AppInfoEntity,
		///<summary>AutoItem</summary>
		AutoItemEntity,
		///<summary>AutoItemRelation</summary>
		AutoItemRelationEntity,
		///<summary>AutoProtocol</summary>
		AutoProtocolEntity,
		///<summary>AutoProtocolRevision</summary>
		AutoProtocolRevisionEntity,
		///<summary>AutoProtocolStage</summary>
		AutoProtocolStageEntity,
		///<summary>AutoProtocolStageRevision</summary>
		AutoProtocolStageRevisionEntity,
		///<summary>AutoTest</summary>
		AutoTestEntity,
		///<summary>AutoTestResult</summary>
		AutoTestResultEntity,
		///<summary>AutoTestResultProduct</summary>
		AutoTestResultProductEntity,
		///<summary>AutoTestStage</summary>
		AutoTestStageEntity,
		///<summary>ClinicProductPricing</summary>
		ClinicProductPricingEntity,
		///<summary>DosageOption</summary>
		DosageOptionEntity,
		///<summary>FrequencyTest</summary>
		FrequencyTestEntity,
		///<summary>FrequencyTestResult</summary>
		FrequencyTestResultEntity,
		///<summary>HwProfile</summary>
		HwProfileEntity,
		///<summary>Image</summary>
		ImageEntity,
		///<summary>Invoice</summary>
		InvoiceEntity,
		///<summary>IssueNavigationStep</summary>
		IssueNavigationStepEntity,
		///<summary>Item</summary>
		ItemEntity,
		///<summary>ItemDetails</summary>
		ItemDetailsEntity,
		///<summary>ItemProperty</summary>
		ItemPropertyEntity,
		///<summary>ItemRelation</summary>
		ItemRelationEntity,
		///<summary>ItemRelationProperty</summary>
		ItemRelationPropertyEntity,
		///<summary>ItemTarget</summary>
		ItemTargetEntity,
		///<summary>Lookup</summary>
		LookupEntity,
		///<summary>OrderItem</summary>
		OrderItemEntity,
		///<summary>Patient</summary>
		PatientEntity,
		///<summary>PatientHistory</summary>
		PatientHistoryEntity,
		///<summary>Product</summary>
		ProductEntity,
		///<summary>ProductForm</summary>
		ProductFormEntity,
		///<summary>ProductSize</summary>
		ProductSizeEntity,
		///<summary>Property</summary>
		PropertyEntity,
		///<summary>ProtocolItem</summary>
		ProtocolItemEntity,
		///<summary>ProtocolStep</summary>
		ProtocolStepEntity,
		///<summary>Reading</summary>
		ReadingEntity,
		///<summary>ScheduleLine</summary>
		ScheduleLineEntity,
		///<summary>Service</summary>
		ServiceEntity,
		///<summary>Setting</summary>
		SettingEntity,
		///<summary>ShippingOrder</summary>
		ShippingOrderEntity,
		///<summary>SpotCheck</summary>
		SpotCheckEntity,
		///<summary>SpotCheckResult</summary>
		SpotCheckResultEntity,
		///<summary>StageAnnouncement</summary>
		StageAnnouncementEntity,
		///<summary>StageAutoItem</summary>
		StageAutoItemEntity,
		///<summary>Test</summary>
		TestEntity,
		///<summary>TestImprintableItem</summary>
		TestImprintableItemEntity,
		///<summary>TestingPoint</summary>
		TestingPointEntity,
		///<summary>TestIssue</summary>
		TestIssueEntity,
		///<summary>TestProtocol</summary>
		TestProtocolEntity,
		///<summary>TestResult</summary>
		TestResultEntity,
		///<summary>TestResultFactors</summary>
		TestResultFactorsEntity,
		///<summary>TestSchedule</summary>
		TestScheduleEntity,
		///<summary>TestService</summary>
		TestServiceEntity,
		///<summary>User</summary>
		UserEntity,
		///<summary>VFS</summary>
		VFSEntity,
		///<summary>VFSItem</summary>
		VFSItemEntity,
		///<summary>VFSItemSource</summary>
		VFSItemSourceEntity,
		///<summary>VFSSecondaryItem</summary>
		VFSSecondaryItemEntity,
		///<summary>VFSSecondaryItemSource</summary>
		VFSSecondaryItemSourceEntity
	}


	#region Custom ConstantsEnums Code
	
	// __LLBLGENPRO_USER_CODE_REGION_START CustomUserConstants
	// __LLBLGENPRO_USER_CODE_REGION_END
	#endregion

	#region Included code

	#endregion
}

