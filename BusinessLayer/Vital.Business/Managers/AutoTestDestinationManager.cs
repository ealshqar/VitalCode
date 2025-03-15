using System;
using System.ComponentModel;
using System.Linq;
using Vital.Business.Repositories.DatabaseRepositories.AutoTestDestination;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.AutoTestDestination;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class AutoTestDestinationManager : BaseManager
    {
        #region Private Variables

        private readonly IAutoTestDestinationRepository _autoTestDestinationRepository;

        #endregion

        #region Private Related Managers


        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public AutoTestDestinationManager()
        {
            _autoTestDestinationRepository = new AutoTestDestinationDatabaseRepository();
        }

        #endregion

        #region Public Methods

        #region AutoTests

        /// <summary>
        /// Gets a autoTest.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public AutoTest GetAutoTestById(AutoTestsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<AutoTest> GetAutoTests(AutoTestsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the autoTest.
        /// </summary>
        /// <param name="autoTest">The autoTest.</param>
        /// <returns></returns>
        public ProcessResult Save(AutoTest autoTest)
        {
            Check.Argument.IsNotNull(() => autoTest);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoTest.Id };

                if (!autoTest.IsChanged) return processResult;

                autoTest.SetUserAndDates();

                processResult = _autoTestDestinationRepository.Save(autoTest);

                if (processResult.IsSucceed)
                {
                    autoTest.ObjectState = DomainEntityState.Unchanged;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the autoTest with details
        /// </summary>
        /// <param name="autoTest">The autoTest.</param>
        /// <returns></returns>
        public ProcessResult SaveWithDetails(AutoTest autoTest)
        {
            Check.Argument.IsNotNull(() => autoTest);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoTest.Id };

                if (!autoTest.IsChanged &&
                     autoTest.AutoTestResults.All(a => !a.IsChanged)) return processResult;

                autoTest.SetUserAndDates();

                processResult = _autoTestDestinationRepository.Save(autoTest);

                //Set the autoTest inside the AutoTestResults
                if (autoTest.AutoTestResults != null)
                    autoTest.AutoTestResults.ToList().ForEach(s => s.AutoTest = autoTest);

                var processResultAutoTestResults = SaveWithDetails(autoTest.AutoTestResults);

                if (processResult.IsSucceed &&
                    processResultAutoTestResults.IsSucceed)
                {
                    autoTest.ObjectState = DomainEntityState.Unchanged;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the AutoTests collection.
        /// </summary>
        /// <param name="autoTests"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<AutoTest> autoTests)
        {
            Check.Argument.IsNotNull(() => autoTests);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var autoTest in autoTests)
                {
                    result = autoTest.IsDeleted ? Delete(autoTest) : Save(autoTest);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a autoTest.
        /// </summary>
        /// <param name="autoTest">The autoTest.</param>
        /// <returns></returns>
        public ProcessResult Delete(AutoTest autoTest)
        {
            Check.Argument.IsNotNull(() => autoTest);

            try
            {
                autoTest.AutoTestResults = GetAutoTestResults(new AutoTestResultsFilter { AutoTestsId = autoTest.Id });
                var processResultAutoTestResults = Delete(autoTest.AutoTestResults);

                var processResult = _autoTestDestinationRepository.Delete(autoTest);

                if (processResult.IsSucceed &&
                    processResultAutoTestResults.IsSucceed)
                {
                    autoTest.ObjectState = DomainEntityState.Deleted;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes  the AutoTests collection.
        /// </summary>
        /// <param name="autoTests"></param>
        public ProcessResult Delete(BindingList<AutoTest> autoTests)
        {
            Check.Argument.IsNotNull(() => autoTests);

            try
            {
                foreach (var autoTest in autoTests)
                {
                    var result = Delete(autoTest);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region AutoTestResults

        /// <summary>
        /// Gets a autoTestResult.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public AutoTestResult GetAutoTestResultById(AutoTestResultsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<AutoTestResult> GetAutoTestResults(AutoTestResultsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the autoTestResult.
        /// </summary>
        /// <param name="autoTestResult">The autoTestResult.</param>
        /// <returns></returns>
        public ProcessResult Save(AutoTestResult autoTestResult)
        {
            Check.Argument.IsNotNull(() => autoTestResult);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoTestResult.Id };

                if (!autoTestResult.IsChanged) return processResult;

                autoTestResult.SetUserAndDates();

                processResult = _autoTestDestinationRepository.Save(autoTestResult);

                if (processResult.IsSucceed)
                {
                    autoTestResult.ObjectState = DomainEntityState.Unchanged;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the autoTestResult with details
        /// </summary>
        /// <param name="autoTestResult">The autoTestResult.</param>
        /// <returns></returns>
        public ProcessResult SaveWithDetails(AutoTestResult autoTestResult)
        {
            Check.Argument.IsNotNull(() => autoTestResult);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoTestResult.Id };

                if (!autoTestResult.IsChanged &&
                     autoTestResult.AutoTestResultProducts.All(a => !a.IsChanged)) return processResult;

                autoTestResult.SetUserAndDates();

                processResult = _autoTestDestinationRepository.Save(autoTestResult);

                //Set the autoTestResult inside the AutoTestResultProducts
                if (autoTestResult.AutoTestResultProducts != null)
                    autoTestResult.AutoTestResultProducts.ToList().ForEach(s => s.AutoTestResult = autoTestResult);

                var processResultAutoTestResultProducts = Save(autoTestResult.AutoTestResultProducts);

                if (processResult.IsSucceed &&
                    processResultAutoTestResultProducts.IsSucceed)
                {
                    autoTestResult.ObjectState = DomainEntityState.Unchanged;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the AutoTestResults collection.
        /// </summary>
        /// <param name="autoTestResults"></param>
        /// <returns></returns>
        public ProcessResult SaveWithDetails(BindingList<AutoTestResult> autoTestResults)
        {
            Check.Argument.IsNotNull(() => autoTestResults);

            var result = ProcessResult.Succeed;

            try
            {
                //Order the results ascending to save them in order of dependency and avoid saving child before parent
                var resultsToSave = autoTestResults.Where(resultNoDeleted => !resultNoDeleted.IsDeleted).OrderBy(resultToSave => resultToSave.StructureId);
                
                //Order the items to delete in descending order to prevent dependency issue
                var resultsToDelete = autoTestResults.Where(resultDeleted => resultDeleted.IsDeleted).OrderByDescending(resultToDelete => resultToDelete.StructureId);

                foreach (var autoTestResult in resultsToSave)
                {
                    //Make sure to set the real parentId after saving it inside its child item
                    if (autoTestResult.AutoTestResultParent != null && !autoTestResult.AutoTestResultParent.IsNew)
                    {
                        autoTestResult.AutoTestResultsParentId = autoTestResult.AutoTestResultParent.Id;
                    }

                    result = SaveWithDetails(autoTestResult);

                    if (!result.IsSucceed)
                        return result;
                }

                //Delete items that should be removed.
                foreach (var autoTestResult in resultsToDelete)
                {
                    result = Delete(autoTestResult);

                    if (!result.IsSucceed)
                        return result;
                }

                //Logic below will make sure that deleted results are completely removed from the collection after saving is done, this is important
                //because in the current case we used a different delete approach where we didn't add the record to a DeleteCollection like we used to do
                //but rather kept it in its original collection and only marked it as deleted and applied filter on grid to hide deleted records.
                //This way we don't need to call a separate logic for deleting records in delete colleciton but just need to save the parent Test and saving
                //logic will take care of removal however it will only remove the record in DB so in the logic below we also remove the record from collection
                //to avoid issues in case the user tried to save again, if the record is still there and it was deleted, the system will try to delete again
                //and this will cause error, luckily the collection keeps its reference and so removing from collection below will remove from collection inside test
                var deletedAutoTestResults = new BindingList<AutoTestResult>(autoTestResults.Where(autoTestResult => autoTestResult.IsDeleted).ToList());

                foreach (var deletedAutoTestResult in deletedAutoTestResults)
                {
                    autoTestResults.Remove(deletedAutoTestResult);
                }

                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the AutoTestResults collection.
        /// </summary>
        /// <param name="autoTestResults"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<AutoTestResult> autoTestResults)
        {
            Check.Argument.IsNotNull(() => autoTestResults);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var autoTestResult in autoTestResults)
                {
                    result = autoTestResult.IsDeleted ? Delete(autoTestResult) : Save(autoTestResult);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a autoTestResult.
        /// </summary>
        /// <param name="autoTestResult">The autoTestResult.</param>
        /// <returns></returns>
        public ProcessResult Delete(AutoTestResult autoTestResult)
        {
            Check.Argument.IsNotNull(() => autoTestResult);

            try
            {
                autoTestResult.AutoTestResultProducts = GetAutoTestResultProducts(new AutoTestResultProductFilter { AutoTestResultsId = autoTestResult.Id });
                var processResultAutoTestResultProducts = Delete(autoTestResult.AutoTestResultProducts);

                var processResult = _autoTestDestinationRepository.Delete(autoTestResult);

                if (processResult.IsSucceed &&
                    processResultAutoTestResultProducts.IsSucceed)
                {
                    autoTestResult.ObjectState = DomainEntityState.Deleted;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes  the AutoTestResults collection.
        /// </summary>
        /// <param name="autoTestResults"></param>
        public ProcessResult Delete(BindingList<AutoTestResult> autoTestResults)
        {
            Check.Argument.IsNotNull(() => autoTestResults);

            try
            {
                foreach (var autoTestResult in autoTestResults)
                {
                    var result = Delete(autoTestResult);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region AutoTestResultProducts

        /// <summary>
        /// Gets a autoTestResultProduct.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public AutoTestResultProduct GetAutoTestResultProductById(AutoTestResultProductFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<AutoTestResultProduct> GetAutoTestResultProducts(AutoTestResultProductFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the autoTestResultProduct.
        /// </summary>
        /// <param name="autoTestResultProduct">The autoTestResultProduct.</param>
        /// <returns></returns>
        public ProcessResult Save(AutoTestResultProduct autoTestResultProduct)
        {
            Check.Argument.IsNotNull(() => autoTestResultProduct);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoTestResultProduct.Id };

                if (!autoTestResultProduct.IsChanged) return processResult;

                autoTestResultProduct.SetUserAndDates();

                processResult = _autoTestDestinationRepository.Save(autoTestResultProduct);

                if (processResult.IsSucceed)
                {
                    autoTestResultProduct.ObjectState = DomainEntityState.Unchanged;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the AutoTestResultProducts collection.
        /// </summary>
        /// <param name="autoTestResultProducts"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<AutoTestResultProduct> autoTestResultProducts)
        {
            Check.Argument.IsNotNull(() => autoTestResultProducts);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var autoTestResultProduct in autoTestResultProducts)
                {
                    result = autoTestResultProduct.IsDeleted ? Delete(autoTestResultProduct) : Save(autoTestResultProduct);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a autoTestResultProduct.
        /// </summary>
        /// <param name="autoTestResultProduct">The autoTestResultProduct.</param>
        /// <returns></returns>
        public ProcessResult Delete(AutoTestResultProduct autoTestResultProduct)
        {
            Check.Argument.IsNotNull(() => autoTestResultProduct);

            try
            {
                var processResult = _autoTestDestinationRepository.Delete(autoTestResultProduct);

                if (processResult.IsSucceed)
                {
                    autoTestResultProduct.ObjectState = DomainEntityState.Deleted;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes  the AutoTestResultProducts collection.
        /// </summary>
        /// <param name="autoTestResultProducts"></param>
        public ProcessResult Delete(BindingList<AutoTestResultProduct> autoTestResultProducts)
        {
            Check.Argument.IsNotNull(() => autoTestResultProducts);

            try
            {
                foreach (var autoTestResultProduct in autoTestResultProducts)
                {
                    var result = Delete(autoTestResultProduct);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region ClinicProductPricing

        /// <summary>
        /// Gets a clinicProductPricing.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ClinicProductPricing GetClinicProductPricingById(ClinicProductPricingsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<ClinicProductPricing> GetClinicProductPricing(ClinicProductPricingsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the clinicProductPricing.
        /// </summary>
        /// <param name="clinicProductPricing">The clinicProductPricing.</param>
        /// <returns></returns>
        public ProcessResult Save(ClinicProductPricing clinicProductPricing)
        {
            Check.Argument.IsNotNull(() => clinicProductPricing);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = clinicProductPricing.Id };

                if (!clinicProductPricing.IsChanged) return processResult;

                clinicProductPricing.SetUserAndDates();

                processResult = _autoTestDestinationRepository.Save(clinicProductPricing);

                if (processResult.IsSucceed)
                {
                    clinicProductPricing.ObjectState = DomainEntityState.Unchanged;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }
        /// <summary>
        /// Saves the ClinicProductPricings collection.
        /// </summary>
        /// <param name="clinicProductPricings"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<ClinicProductPricing> clinicProductPricings)
        {
            Check.Argument.IsNotNull(() => clinicProductPricings);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var clinicProductPricing in clinicProductPricings)
                {
                    result = clinicProductPricing.IsDeleted ? Delete(clinicProductPricing) : Save(clinicProductPricing);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a clinicProductPricing.
        /// </summary>
        /// <param name="clinicProductPricing">The clinicProductPricing.</param>
        /// <returns></returns>
        public ProcessResult Delete(ClinicProductPricing clinicProductPricing)
        {
            Check.Argument.IsNotNull(() => clinicProductPricing);

            try
            {
                var processResult = _autoTestDestinationRepository.Delete(clinicProductPricing);

                if (processResult.IsSucceed)
                {
                    clinicProductPricing.ObjectState = DomainEntityState.Deleted;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes  the ClinicProductPricings collection.
        /// </summary>
        /// <param name="clinicProductPricings"></param>
        public ProcessResult Delete(BindingList<ClinicProductPricing> clinicProductPricings)
        {
            Check.Argument.IsNotNull(() => clinicProductPricings);

            try
            {
                foreach (var clinicProductPricing in clinicProductPricings)
                {
                    var result = Delete(clinicProductPricing);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region Products

        /// <summary>
        /// Gets a product.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Product GetProductById(ProductsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<Product> GetProducts(ProductsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public ProcessResult Save(Product product)
        {
            Check.Argument.IsNotNull(() => product);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = product.Id };

                if (!product.IsChanged) return processResult;

                product.SetUserAndDates();

                processResult = _autoTestDestinationRepository.Save(product);

                if (processResult.IsSucceed)
                {
                    product.ObjectState = DomainEntityState.Unchanged;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the product with details
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public ProcessResult SaveWithDetails(Product product)
        {
            Check.Argument.IsNotNull(() => product);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = product.Id };

                if (!product.IsChanged &&
                     product.ProductForms.All(a => !a.IsChanged)) return processResult;

                product.SetUserAndDates();

                processResult = _autoTestDestinationRepository.Save(product);

                //Set the product inside the ProductForms
                if (product.ProductForms != null)
                    product.ProductForms.ToList().ForEach(s => s.Product = product);

                var processResultProductForms = Save(product.ProductForms);

                if (processResult.IsSucceed &&
                    processResultProductForms.IsSucceed)
                {
                    product.ObjectState = DomainEntityState.Unchanged;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the Products collection.
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<Product> products)
        {
            Check.Argument.IsNotNull(() => products);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var product in products)
                {
                    result = product.IsDeleted ? Delete(product) : Save(product);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public ProcessResult Delete(Product product)
        {
            Check.Argument.IsNotNull(() => product);

            try
            {
                var clinicProductPricings = GetClinicProductPricing(new ClinicProductPricingsFilter { ProductsId = product.Id });
                var processResultClinicProductPricings = Delete(clinicProductPricings);

                product.ProductForms = GetProductForms(new ProductFormsFilter { ProductsId = product.Id });
                var processResultProductForms = Delete(product.ProductForms);

                var processResult = _autoTestDestinationRepository.Delete(product);

                if (processResult.IsSucceed &&
                    processResultClinicProductPricings.IsSucceed &&
                    processResultProductForms.IsSucceed)
                {
                    product.ObjectState = DomainEntityState.Deleted;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes  the Products collection.
        /// </summary>
        /// <param name="products"></param>
        public ProcessResult Delete(BindingList<Product> products)
        {
            Check.Argument.IsNotNull(() => products);

            try
            {
                foreach (var product in products)
                {
                    var result = Delete(product);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region ProductForms

        /// <summary>
        /// Gets a productForm.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ProductForm GetProductFormById(ProductFormsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<ProductForm> GetProductForms(ProductFormsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the productForm.
        /// </summary>
        /// <param name="productForm">The productForm.</param>
        /// <returns></returns>
        public ProcessResult Save(ProductForm productForm)
        {
            Check.Argument.IsNotNull(() => productForm);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = productForm.Id };

                if (!productForm.IsChanged) return processResult;

                productForm.SetUserAndDates();

                processResult = _autoTestDestinationRepository.Save(productForm);

                if (processResult.IsSucceed)
                {
                    productForm.ObjectState = DomainEntityState.Unchanged;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the productForm with details
        /// </summary>
        /// <param name="productForm">The productForm.</param>
        /// <returns></returns>
        public ProcessResult SaveWithDetails(ProductForm productForm)
        {
            Check.Argument.IsNotNull(() => productForm);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = productForm.Id };

                if (!productForm.IsChanged &&
                     productForm.DosageOptions.All(a => !a.IsChanged) &&
                     productForm.ProductSizes.All(a => !a.IsChanged)) return processResult;

                productForm.SetUserAndDates();

                processResult = _autoTestDestinationRepository.Save(productForm);

                //Set the productForm inside the DosageOptions
                if (productForm.DosageOptions != null)
                    productForm.DosageOptions.ToList().ForEach(s => s.ProductForm = productForm);

                //Set the productForm inside the ProductSizes
                if (productForm.ProductSizes != null)
                    productForm.ProductSizes.ToList().ForEach(s => s.ProductForm = productForm);

                var processResultDosageOptions = Save(productForm.DosageOptions);
                var processResultProductSizes = Save(productForm.ProductSizes);

                if (processResult.IsSucceed &&
                    processResultDosageOptions.IsSucceed &&
                    processResultProductSizes.IsSucceed)
                {
                    productForm.ObjectState = DomainEntityState.Unchanged;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the ProductForms collection.
        /// </summary>
        /// <param name="productForms"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<ProductForm> productForms)
        {
            Check.Argument.IsNotNull(() => productForms);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var productForm in productForms)
                {
                    result = productForm.IsDeleted ? Delete(productForm) : Save(productForm);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a productForm.
        /// </summary>
        /// <param name="productForm">The productForm.</param>
        /// <returns></returns>
        public ProcessResult Delete(ProductForm productForm)
        {
            Check.Argument.IsNotNull(() => productForm);

            try
            {
                productForm.DosageOptions = GetDosageOptions(new DosageOptionsFilter { ProductFormsId = productForm.Id });
                var processResultDosageOptions = Delete(productForm.DosageOptions);

                productForm.ProductSizes = GetProductSizes(new ProductSizesFilter { ProductFormsId = productForm.Id });
                var processResultProductSizes = Delete(productForm.ProductSizes);

                var processResult = _autoTestDestinationRepository.Delete(productForm);

                if (processResult.IsSucceed &&
                    processResultDosageOptions.IsSucceed &&
                    processResultProductSizes.IsSucceed)
                {
                    productForm.ObjectState = DomainEntityState.Deleted;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes  the ProductForms collection.
        /// </summary>
        /// <param name="productForms"></param>
        public ProcessResult Delete(BindingList<ProductForm> productForms)
        {
            Check.Argument.IsNotNull(() => productForms);

            try
            {
                foreach (var productForm in productForms)
                {
                    var result = Delete(productForm);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region ProductSizes

        /// <summary>
        /// Gets a productSize.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ProductSize GetProductSizeById(ProductSizesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<ProductSize> GetProductSizes(ProductSizesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the productSize.
        /// </summary>
        /// <param name="productSize">The productSize.</param>
        /// <returns></returns>
        public ProcessResult Save(ProductSize productSize)
        {
            Check.Argument.IsNotNull(() => productSize);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = productSize.Id };

                if (!productSize.IsChanged) return processResult;

                productSize.SetUserAndDates();

                processResult = _autoTestDestinationRepository.Save(productSize);

                if (processResult.IsSucceed)
                {
                    productSize.ObjectState = DomainEntityState.Unchanged;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the ProductSizes collection.
        /// </summary>
        /// <param name="productSizes"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<ProductSize> productSizes)
        {
            Check.Argument.IsNotNull(() => productSizes);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var productSize in productSizes)
                {
                    result = productSize.IsDeleted ? Delete(productSize) : Save(productSize);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a productSize.
        /// </summary>
        /// <param name="productSize">The productSize.</param>
        /// <returns></returns>
        public ProcessResult Delete(ProductSize productSize)
        {
            Check.Argument.IsNotNull(() => productSize);

            try
            {
                var processResult = _autoTestDestinationRepository.Delete(productSize);

                if (processResult.IsSucceed)
                {
                    productSize.ObjectState = DomainEntityState.Deleted;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes  the ProductSizes collection.
        /// </summary>
        /// <param name="productSizes"></param>
        public ProcessResult Delete(BindingList<ProductSize> productSizes)
        {
            Check.Argument.IsNotNull(() => productSizes);

            try
            {
                foreach (var productSize in productSizes)
                {
                    var result = Delete(productSize);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region DosageOptions

        /// <summary>
        /// Gets a dosageOption.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public DosageOption GetDosageOptionById(DosageOptionsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<DosageOption> GetDosageOptions(DosageOptionsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestDestinationRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the dosageOption.
        /// </summary>
        /// <param name="dosageOption">The dosageOption.</param>
        /// <returns></returns>
        public ProcessResult Save(DosageOption dosageOption)
        {
            Check.Argument.IsNotNull(() => dosageOption);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = dosageOption.Id };

                if (!dosageOption.IsChanged) return processResult;

                dosageOption.SetUserAndDates();

                processResult = _autoTestDestinationRepository.Save(dosageOption);

                if (processResult.IsSucceed)
                {
                    dosageOption.ObjectState = DomainEntityState.Unchanged;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }
        /// <summary>
        /// Saves the DosageOptions collection.
        /// </summary>
        /// <param name="dosageOptions"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<DosageOption> dosageOptions)
        {
            Check.Argument.IsNotNull(() => dosageOptions);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var dosageOption in dosageOptions)
                {
                    result = dosageOption.IsDeleted ? Delete(dosageOption) : Save(dosageOption);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a dosageOption.
        /// </summary>
        /// <param name="dosageOption">The dosageOption.</param>
        /// <returns></returns>
        public ProcessResult Delete(DosageOption dosageOption)
        {
            Check.Argument.IsNotNull(() => dosageOption);

            try
            {
                var processResult = _autoTestDestinationRepository.Delete(dosageOption);

                if (processResult.IsSucceed)
                {
                    dosageOption.ObjectState = DomainEntityState.Deleted;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes  the DosageOptions collection.
        /// </summary>
        /// <param name="dosageOptions"></param>
        public ProcessResult Delete(BindingList<DosageOption> dosageOptions)
        {
            Check.Argument.IsNotNull(() => dosageOptions);

            try
            {
                foreach (var dosageOption in dosageOptions)
                {
                    var result = Delete(dosageOption);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #endregion
    }
}
