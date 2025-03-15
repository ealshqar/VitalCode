using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.AutoTestDestination;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.AutoTestDestination
{
    public interface IAutoTestDestinationRepository
    {
        #region AutoTests

        /// <summary>
        /// Loads AutoTest by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoTest</returns>
        AutoTest LoadEntityById(AutoTestsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<AutoTest> LoadEntities(AutoTestsFilter filter);

        /// <summary>
        /// Saves a autoTest.
        /// </summary>
        /// <param name="autoTestToSave">The autoTest.</param>
        /// <returns>The autoTest.</returns>
        ProcessResult Save(AutoTest autoTestToSave);

        /// <summary>
        /// Deletes a autoTest.
        /// </summary>
        /// <param name="autoTestToDelete">The autoTest.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(AutoTest autoTestToDelete);

        #endregion

        #region AutoTestResults

        /// <summary>
        /// Loads AutoTestResult by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoTestResult</returns>
        AutoTestResult LoadEntityById(AutoTestResultsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<AutoTestResult> LoadEntities(AutoTestResultsFilter filter);

        /// <summary>
        /// Saves a autoTestResult.
        /// </summary>
        /// <param name="autoTestResultToSave">The autoTestResult.</param>
        /// <returns>The autoTestResult.</returns>
        ProcessResult Save(AutoTestResult autoTestResultToSave);

        /// <summary>
        /// Deletes a autoTestResult.
        /// </summary>
        /// <param name="autoTestResultToDelete">The autoTestResult.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(AutoTestResult autoTestResultToDelete);

        #endregion

        #region AutoTestResultProduct

        /// <summary>
        /// Loads AutoTestResultProduct by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoTestResultProduct</returns>
        AutoTestResultProduct LoadEntityById(AutoTestResultProductFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<AutoTestResultProduct> LoadEntities(AutoTestResultProductFilter filter);

        /// <summary>
        /// Saves a autoTestResultProduct.
        /// </summary>
        /// <param name="autoTestResultProductToSave">The autoTestResultProduct.</param>
        /// <returns>The autoTestResultProduct.</returns>
        ProcessResult Save(AutoTestResultProduct autoTestResultProductToSave);

        /// <summary>
        /// Deletes a autoTestResultProduct.
        /// </summary>
        /// <param name="autoTestResultProductToDelete">The autoTestResultProduct.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(AutoTestResultProduct autoTestResultProductToDelete);

        #endregion

        #region Products

        /// <summary>
        /// Loads Product by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The Product</returns>
        Product LoadEntityById(ProductsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<Product> LoadEntities(ProductsFilter filter);


        /// <summary>
        /// Saves a product.
        /// </summary>
        /// <param name="productToSave">The product.</param>
        /// <returns>The product.</returns>
        ProcessResult Save(Product productToSave);

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="productToDelete">The product.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(Product productToDelete);

        #endregion

        #region ProductForms

        /// <summary>
        /// Loads ProductForm by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The ProductForm</returns>
        ProductForm LoadEntityById(ProductFormsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<ProductForm> LoadEntities(ProductFormsFilter filter);

        /// <summary>
        /// Saves a productForm.
        /// </summary>
        /// <param name="productFormToSave">The productForm.</param>
        /// <returns>The productForm.</returns>
        ProcessResult Save(ProductForm productFormToSave);

        /// <summary>
        /// Deletes a productForm.
        /// </summary>
        /// <param name="productFormToDelete">The productForm.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(ProductForm productFormToDelete);

        #endregion

        #region ProductSizes

        /// <summary>
        /// Loads ProductSize by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The ProductSize</returns>
        ProductSize LoadEntityById(ProductSizesFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<ProductSize> LoadEntities(ProductSizesFilter filter);

        /// <summary>
        /// Saves a productSize.
        /// </summary>
        /// <param name="productSizeToSave">The productSize.</param>
        /// <returns>The productSize.</returns>
        ProcessResult Save(ProductSize productSizeToSave);

        /// <summary>
        /// Deletes a productSize.
        /// </summary>
        /// <param name="productSizeToDelete">The productSize.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(ProductSize productSizeToDelete);

        #endregion

        #region ClinicProductPricings

        /// <summary>
        /// Loads ClinicProductPricing by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The ClinicProductPricing</returns>
        ClinicProductPricing LoadEntityById(ClinicProductPricingsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<ClinicProductPricing> LoadEntities(ClinicProductPricingsFilter filter);

        /// <summary>
        /// Saves a clinicProductPricing.
        /// </summary>
        /// <param name="clinicProductPricingToSave">The clinicProductPricing.</param>
        /// <returns>The clinicProductPricing.</returns>
        ProcessResult Save(ClinicProductPricing clinicProductPricingToSave);

        /// <summary>
        /// Deletes a clinicProductPricing.
        /// </summary>
        /// <param name="clinicProductPricingToDelete">The clinicProductPricing.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(ClinicProductPricing clinicProductPricingToDelete);

        #endregion

        #region DosageOptions

        /// <summary>
        /// Loads DosageOption by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The DosageOption</returns>
        DosageOption LoadEntityById(DosageOptionsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<DosageOption> LoadEntities(DosageOptionsFilter filter);

        /// <summary>
        /// Saves a dosageOption.
        /// </summary>
        /// <param name="dosageOptionToSave">The dosageOption.</param>
        /// <returns>The dosageOption.</returns>
        ProcessResult Save(DosageOption dosageOptionToSave);

        /// <summary>
        /// Deletes a dosageOption.
        /// </summary>
        /// <param name="dosageOptionToDelete">The dosageOption.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(DosageOption dosageOptionToDelete);

        #endregion
    }
}
