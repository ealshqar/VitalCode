using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.AutoTestDestination;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.AutoTestDestination
{
    public class AutoTestDestinationDatabaseRepository : BaseRepository, IAutoTestDestinationRepository
    {
        #region Path Edges

        #region AutoTests

        private readonly Func<IPathEdgeRootParser<AutoTestEntity>, IPathEdgeRootParser<AutoTestEntity>> _pathEdgesAutoTestLight =
                p => p
                    .Prefetch(pa => pa.Patient)
                    .Prefetch(cr => cr.User);

        private readonly Func<IPathEdgeRootParser<AutoTestEntity>, IPathEdgeRootParser<AutoTestEntity>>
            _pathEdgesAutoTest =
                p => p
                    .Prefetch<PatientEntity>(pa => pa.Patient).SubPath(gn => gn
                                        .Prefetch(i => i.Lookup))
                    .Prefetch<AutoTestResultEntity>(tr => tr.AutoTestResults).SubPath(tr => tr
                        .Prefetch<AutoItemEntity>(ai => ai.AutoItem).SubPath(ai => ai
                                        .Prefetch<ProductEntity>(i => i.Products).SubPath(pro => pro
                                            .Prefetch<ProductFormEntity>(i => i.ProductForms).SubPath(pfo => pfo
                                                .Prefetch(i => i.ProductSizes)
                                                .Prefetch(i => i.DosageOptions)))
                                        .Prefetch(i => i.ChildsOrderType)
                                        .Prefetch(i => i.ChildsScanningType)
                                        .Prefetch(i => i.ScanningMethod)
                                        .Prefetch(i => i.Status)
                                        .Prefetch(i => i.StructureType)
                                        .Prefetch(i => i.TestingPoint)
                                        .Prefetch(i => i.Type)
                                        .Prefetch(i => i.Gender)
                                        .Prefetch(i => i.Parents)
                                        .Prefetch(i => i.Image))
                        .Prefetch(r => r.AutoProtocolStageRevision)
                        .Prefetch<AutoTestResultProductEntity>(r => r.AutoTestResultProducts).SubPath(atrp => atrp
                            .Prefetch(i => i.ProductForm)
                            .Prefetch(i => i.ProductSize)))
                    .Prefetch<AutoProtocolRevisionEntity>(au => au.AutoProtocolRevision).SubPath(sr => sr
                        .Prefetch(au => au.AutoProtocol)
                        .Prefetch<AutoProtocolStageRevisionEntity>(au => au.AutoProtocolStageRevisions).SubPath(au => au
                            .Prefetch(ts => ts.AutoTestStage)
                            .Prefetch<AutoProtocolStageEntity>(pr => pr.AutoProtocolStage).SubPath(pr => pr
                                .Prefetch(ts => ts.StageItemsOrderType)
                                .Prefetch<StageAutoItemEntity>(ts => ts.StageAutoItems).SubPath(ts => ts
                                        .Prefetch(i => i.TestingPoint)
                                        .Prefetch(i => i.ChildsOrderType)
                                        .Prefetch(i => i.ChildsScanningType)
                                        .Prefetch(i => i.ScanningMethod)
                                    .Prefetch<AutoItemEntity>(ai => ai.AutoItem).SubPath(ai => ai
                                        .Prefetch<ProductEntity>(i => i.Products).SubPath(pro => pro
                                            .Prefetch<ProductFormEntity>(i => i.ProductForms).SubPath(pfo => pfo
                                                .Prefetch(i => i.ProductSizes)
                                                .Prefetch(i => i.DosageOptions)))
                                        .Prefetch(i => i.ChildsOrderType)
                                        .Prefetch(i => i.ChildsScanningType)
                                        .Prefetch(i => i.ScanningMethod)
                                        .Prefetch(i => i.Status)
                                        .Prefetch(i => i.StructureType)
                                        .Prefetch(i => i.TestingPoint)
                                        .Prefetch(i => i.Type)
                                        .Prefetch(i => i.Gender)
                                        .Prefetch(i => i.Parents)
                                        .Prefetch(i => i.Image))
                                    .Prefetch<StageAutoItemEntity>(ai => ai.StageAutoItems).SubPath(si => si
                                        .Prefetch(i => i.TestingPoint)
                                        .Prefetch(i => i.ChildsOrderType)
                                        .Prefetch(i => i.ChildsScanningType)
                                        .Prefetch(i => i.ScanningMethod)
                                        .Prefetch<AutoItemEntity>(ai => ai.AutoItem).SubPath(ai => ai
                                            .Prefetch<ProductEntity>(i => i.Products).SubPath(pro => pro
                                            .Prefetch<ProductFormEntity>(i => i.ProductForms).SubPath(pfo => pfo
                                                .Prefetch(i => i.ProductSizes)
                                                .Prefetch(i => i.DosageOptions)))
                                            .Prefetch(i => i.ChildsOrderType)
                                            .Prefetch(i => i.ChildsScanningType)
                                            .Prefetch(i => i.ScanningMethod)
                                            .Prefetch(i => i.Status)
                                            .Prefetch(i => i.StructureType)
                                            .Prefetch(i => i.TestingPoint)
                                            .Prefetch(i => i.Type)
                                            .Prefetch(i => i.Gender)
                                            .Prefetch(i => i.Parents)
                                            .Prefetch(i => i.Image))
                                        .Prefetch<StageAutoItemEntity>(sai => sai.StageAutoItems).SubPath(ssi => ssi
                                            .Prefetch(i => i.TestingPoint)
                                            .Prefetch(i => i.ChildsOrderType)
                                            .Prefetch(i => i.ChildsScanningType)
                                            .Prefetch(i => i.ScanningMethod)
                                            .Prefetch<AutoItemEntity>(ai => ai.AutoItem).SubPath(sai => sai
                                                .Prefetch<ProductEntity>(i => i.Products).SubPath(pro => pro
                                                    .Prefetch<ProductFormEntity>(i => i.ProductForms).SubPath(pfo => pfo
                                                        .Prefetch(i => i.ProductSizes)
                                                        .Prefetch(i => i.DosageOptions)))
                                                .Prefetch(i => i.ChildsOrderType)
                                                .Prefetch(i => i.ChildsScanningType)
                                                .Prefetch(i => i.ScanningMethod)
                                                .Prefetch(i => i.Status)
                                                .Prefetch(i => i.StructureType)
                                                .Prefetch(i => i.TestingPoint)
                                                .Prefetch(i => i.Type)
                                                .Prefetch(i => i.Gender)
                                                .Prefetch(i => i.Parents)
                                                .Prefetch(i => i.Image))))))))
                    .Prefetch(cr => cr.User);

        #endregion

        #region AutoTestResults

        private readonly Func<IPathEdgeRootParser<AutoTestResultEntity>, IPathEdgeRootParser<AutoTestResultEntity>> _pathEdgesAutoTestResult =
            p => p
                .Prefetch(au => au.AutoTest)
                .Prefetch(au => au.AutoItem)
                .Prefetch(au => au.AutoProtocolStageRevision)
                .Prefetch(au => au.AutoTestResultParent)
                .Prefetch(au => au.AutoTestResultProducts)
                .Prefetch(au => au.AutoTestResultChildes)
                .Prefetch(cr => cr.User);

        #endregion

        #region AutoTestResultProduct

        private readonly Func<IPathEdgeRootParser<AutoTestResultProductEntity>, IPathEdgeRootParser<AutoTestResultProductEntity>> _pathEdgesAutoTestResultProduct =
            p=> p
                .Prefetch(au => au.AutoTestResult)
                .Prefetch(pr => pr.ProductForm)
                .Prefetch(pr => pr.ProductSize)
                .Prefetch(cr => cr.User);
                
        #endregion

        #region Products

        private readonly Func<IPathEdgeRootParser<ProductEntity>, IPathEdgeRootParser<ProductEntity>> _pathEdgesProduct =
            p => p
                .Prefetch(au => au.AutoItem)
                .Prefetch(cl => cl.ClinicProductPricings)
                .Prefetch(pr => pr.ProductForms)
                .Prefetch(cr => cr.User);

        #endregion

        #region ProductForms

        private readonly Func<IPathEdgeRootParser<ProductFormEntity>, IPathEdgeRootParser<ProductFormEntity>> _pathEdgesProductForm =
            p=> p
                .Prefetch(pr => pr.Product)
                .Prefetch(st => st.Status)
                .Prefetch(ds => ds.DosageOptions)
                .Prefetch(pr => pr.ProductSizes)
                .Prefetch(cr => cr.User);
                
        #endregion

        #region ClinicProductPricings

        private readonly Func<IPathEdgeRootParser<ClinicProductPricingEntity>, IPathEdgeRootParser<ClinicProductPricingEntity>> _pathEdgesClinicProductPricing =
            p => p
                .Prefetch(pr => pr.Product)
                .Prefetch(cr => cr.User);

        #endregion

        #region ProductSizes

        private readonly Func<IPathEdgeRootParser<ProductSizeEntity>, IPathEdgeRootParser<ProductSizeEntity>> _pathEdgesProductSize =
            p => p
                .Prefetch(pr => pr.ProductForm)
                .Prefetch(st => st.Status)
                .Prefetch(cr => cr.User);

        #endregion

        #region DosageOptions

        private readonly Func<IPathEdgeRootParser<DosageOptionEntity>, IPathEdgeRootParser<DosageOptionEntity>> _pathEdgesDosageOption =
            p => p
                .Prefetch<ProductFormEntity>(pr => pr.ProductForm).SubPath(pfs => pfs
                    .Prefetch(pz => pz.ProductSizes))
                .Prefetch(cr => cr.User);

        #endregion

        #endregion

        #region Public Methods

        #region AutoTests

        /// <summary>
        /// Gets AutoTest pathedge based on loading type
        /// </summary>
        /// <param name="loadingType"></param>
        /// <returns></returns>
        public Func<IPathEdgeRootParser<AutoTestEntity>, IPathEdgeRootParser<AutoTestEntity>> GetAutoTestPathEdge(LoadingTypeEnum loadingType)
        {
            switch (loadingType)
            {
                case LoadingTypeEnum.Light:
                    return _pathEdgesAutoTestLight;
                default:
                    return _pathEdgesAutoTest;
            }
        }

        /// <summary>
        /// Loads AutoTest by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoTest</returns>
        public AutoTest LoadEntityById(AutoTestsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.AutoTest.WithPath(GetAutoTestPathEdge(filter.LoadingType)).FirstOrDefault(c => c.Id == filter.AutoTestId);
                    return entity == null ? null : Mapper.Map(entity, new AutoTest());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<AutoTest> LoadEntities(AutoTestsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }
        
        /// <summary>
        /// Saves a autoTest.
        /// </summary>
        /// <param name="autoTestToSave">The autoTest.</param>
        /// <returns>The autoTest.</returns>
        public ProcessResult Save(AutoTest autoTestToSave)
        {
            Check.Argument.IsNotNull(autoTestToSave, "autoTest to save");

            try
            {
                var entity = Mapper.Map<AutoTest, AutoTestEntity>(autoTestToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                autoTestToSave.Id = entity.Id;
                autoTestToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a autoTest.
        /// </summary>
        /// <param name="autoTestToDelete">The autoTest.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(AutoTest autoTestToDelete)
        {
            Check.Argument.IsNotNull(autoTestToDelete, "autoTest to delete");

            try
            {
                var entity = Mapper.Map<AutoTest, AutoTestEntity>(autoTestToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region AutoTestResults
        
        /// <summary>
        /// Loads AutoTestResult by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoTestResult</returns>
        public AutoTestResult LoadEntityById(AutoTestResultsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.AutoTestResult.WithPath(_pathEdgesAutoTestResult).FirstOrDefault(c => c.Id == filter.AutoTestResultId);
                    return entity == null ? null : Mapper.Map(entity, new AutoTestResult());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<AutoTestResult> LoadEntities(AutoTestResultsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a autoTestResult.
        /// </summary>
        /// <param name="autoTestResultToSave">The autoTestResult.</param>
        /// <returns>The autoTestResult.</returns>
        public ProcessResult Save(AutoTestResult autoTestResultToSave)
        {
            Check.Argument.IsNotNull(autoTestResultToSave, "autoTestResult to save");

            try
            {
                var entity = Mapper.Map<AutoTestResult, AutoTestResultEntity>(autoTestResultToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                autoTestResultToSave.Id = entity.Id;
                autoTestResultToSave.ResetStatus();
                
                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a autoTestResult.
        /// </summary>
        /// <param name="autoTestResultToDelete">The autoTestResult.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(AutoTestResult autoTestResultToDelete)
        {
            Check.Argument.IsNotNull(autoTestResultToDelete, "autoTestResult to delete");

            try
            {
                var entity = Mapper.Map<AutoTestResult, AutoTestResultEntity>(autoTestResultToDelete);
                
                var processResult = DeleteEntity(entity);
                
                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region AutoTestResultProduct

        /// <summary>
        /// Loads AutoTestResultProduct by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoTestResultProduct</returns>
        public AutoTestResultProduct LoadEntityById(AutoTestResultProductFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.AutoTestResultProduct.WithPath(_pathEdgesAutoTestResultProduct).FirstOrDefault(c => c.Id == filter.AutoTestResultProductId);
                    return entity == null ? null : Mapper.Map(entity, new AutoTestResultProduct());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<AutoTestResultProduct> LoadEntities(AutoTestResultProductFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a autoTestResultProduct.
        /// </summary>
        /// <param name="autoTestResultProductToSave">The autoTestResultProduct.</param>
        /// <returns>The autoTestResultProduct.</returns>
        public ProcessResult Save(AutoTestResultProduct autoTestResultProductToSave)
        {
            Check.Argument.IsNotNull(autoTestResultProductToSave, "autoTestResultProduct to save");

            try
            {
                var entity = Mapper.Map<AutoTestResultProduct, AutoTestResultProductEntity>(autoTestResultProductToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                autoTestResultProductToSave.Id = entity.Id;
                autoTestResultProductToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a autoTestResultProduct.
        /// </summary>
        /// <param name="autoTestResultProductToDelete">The autoTestResultProduct.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(AutoTestResultProduct autoTestResultProductToDelete)
        {
            Check.Argument.IsNotNull(autoTestResultProductToDelete, "autoTestResultProduct to delete");

            try
            {
                var entity = Mapper.Map<AutoTestResultProduct, AutoTestResultProductEntity>(autoTestResultProductToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region Products

        /// <summary>
        /// Loads Product by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The Product</returns>
        public Product LoadEntityById(ProductsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.Product.WithPath(_pathEdgesProduct).FirstOrDefault(c => c.Id == filter.ProductId);
                    return entity == null ? null : Mapper.Map(entity, new Product());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<Product> LoadEntities(ProductsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a product.
        /// </summary>
        /// <param name="productToSave">The product.</param>
        /// <returns>The product.</returns>
        public ProcessResult Save(Product productToSave)
        {
            Check.Argument.IsNotNull(productToSave, "product to save");

            try
            {
                var entity = Mapper.Map<Product, ProductEntity>(productToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                productToSave.Id = entity.Id;
                productToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="productToDelete">The product.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(Product productToDelete)
        {
            Check.Argument.IsNotNull(productToDelete, "product to delete");

            try
            {
                var entity = Mapper.Map<Product, ProductEntity>(productToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region ProductForms

        /// <summary>
        /// Loads ProductForm by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The ProductForm</returns>
        public ProductForm LoadEntityById(ProductFormsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.ProductForm.WithPath(_pathEdgesProductForm).FirstOrDefault(c => c.Id == filter.ProductFormId);
                    return entity == null ? null : Mapper.Map(entity, new ProductForm());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<ProductForm> LoadEntities(ProductFormsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a productForm.
        /// </summary>
        /// <param name="productFormToSave">The productForm.</param>
        /// <returns>The productForm.</returns>
        public ProcessResult Save(ProductForm productFormToSave)
        {
            Check.Argument.IsNotNull(productFormToSave, "productForm to save");

            try
            {
                var entity = Mapper.Map<ProductForm, ProductFormEntity>(productFormToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                productFormToSave.Id = entity.Id;
                productFormToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a productForm.
        /// </summary>
        /// <param name="productFormToDelete">The productForm.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(ProductForm productFormToDelete)
        {
            Check.Argument.IsNotNull(productFormToDelete, "productForm to delete");

            try
            {
                var entity = Mapper.Map<ProductForm, ProductFormEntity>(productFormToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region ProductSizes

        /// <summary>
        /// Loads ProductSize by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The ProductSize</returns>
        public ProductSize LoadEntityById(ProductSizesFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.ProductSize.WithPath(_pathEdgesProductSize).FirstOrDefault(c => c.Id == filter.ProductSizeId);
                    return entity == null ? null : Mapper.Map(entity, new ProductSize());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<ProductSize> LoadEntities(ProductSizesFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a productSize.
        /// </summary>
        /// <param name="productSizeToSave">The productSize.</param>
        /// <returns>The productSize.</returns>
        public ProcessResult Save(ProductSize productSizeToSave)
        {
            Check.Argument.IsNotNull(productSizeToSave, "productSize to save");

            try
            {
                var entity = Mapper.Map<ProductSize, ProductSizeEntity>(productSizeToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                productSizeToSave.Id = entity.Id;
                productSizeToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a productSize.
        /// </summary>
        /// <param name="productSizeToDelete">The productSize.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(ProductSize productSizeToDelete)
        {
            Check.Argument.IsNotNull(productSizeToDelete, "productSize to delete");

            try
            {
                var entity = Mapper.Map<ProductSize, ProductSizeEntity>(productSizeToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region ClinicProductPricings

        /// <summary>
        /// Loads ClinicProductPricing by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The ClinicProductPricing</returns>
        public ClinicProductPricing LoadEntityById(ClinicProductPricingsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.ClinicProductPricing.WithPath(_pathEdgesClinicProductPricing).FirstOrDefault(c => c.Id == filter.ClinicProductPricingId);
                    return entity == null ? null : Mapper.Map(entity, new ClinicProductPricing());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<ClinicProductPricing> LoadEntities(ClinicProductPricingsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a clinicProductPricing.
        /// </summary>
        /// <param name="clinicProductPricingToSave">The clinicProductPricing.</param>
        /// <returns>The clinicProductPricing.</returns>
        public ProcessResult Save(ClinicProductPricing clinicProductPricingToSave)
        {
            Check.Argument.IsNotNull(clinicProductPricingToSave, "clinicProductPricing to save");

            try
            {
                var entity = Mapper.Map<ClinicProductPricing, ClinicProductPricingEntity>(clinicProductPricingToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                clinicProductPricingToSave.Id = entity.Id;
                clinicProductPricingToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a clinicProductPricing.
        /// </summary>
        /// <param name="clinicProductPricingToDelete">The clinicProductPricing.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(ClinicProductPricing clinicProductPricingToDelete)
        {
            Check.Argument.IsNotNull(clinicProductPricingToDelete, "clinicProductPricing to delete");

            try
            {
                var entity = Mapper.Map<ClinicProductPricing, ClinicProductPricingEntity>(clinicProductPricingToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region DosageOptions

        /// <summary>
        /// Loads DosageOption by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The DosageOption</returns>
        public DosageOption LoadEntityById(DosageOptionsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.DosageOption.WithPath(_pathEdgesDosageOption).FirstOrDefault(c => c.Id == filter.DosageOptionId);
                    return entity == null ? null : Mapper.Map(entity, new DosageOption());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<DosageOption> LoadEntities(DosageOptionsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a dosageOption.
        /// </summary>
        /// <param name="dosageOptionToSave">The dosageOption.</param>
        /// <returns>The dosageOption.</returns>
        public ProcessResult Save(DosageOption dosageOptionToSave)
        {
            Check.Argument.IsNotNull(dosageOptionToSave, "dosageOption to save");

            try
            {
                var entity = Mapper.Map<DosageOption, DosageOptionEntity>(dosageOptionToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                dosageOptionToSave.Id = entity.Id;
                dosageOptionToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a dosageOption.
        /// </summary>
        /// <param name="dosageOptionToDelete">The dosageOption.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(DosageOption dosageOptionToDelete)
        {
            Check.Argument.IsNotNull(dosageOptionToDelete, "dosageOption to delete");

            try
            {
                var entity = Mapper.Map<DosageOption, DosageOptionEntity>(dosageOptionToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #endregion

        #region Private Methods

        #region AutoTests

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<AutoTest> LoadEntitiesWorker(AutoTestsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.AutoTest : data.AutoTest.WithPath(GetAutoTestPathEdge(filter.LoadingType));

                src = src.ApplyFilter(filter.PatientId, x => x.PatientId, FilterType.Equal);
                src = src.ApplyFilter(filter.AutoProtocolRevisionsId, x => x.AutoProtocolRevisionsId, FilterType.Equal);
                src = src.ApplyFilter(filter.Name, x => x.Name, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Description, x => x.Description, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Notes, x => x.Notes, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.TestDate, x => x.TestDate, FilterType.Equal);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey, 
                                    x => x.Name, 
                                    x => x.Description, 
                                    x => x.Notes);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region AutoTestResults

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<AutoTestResult> LoadEntitiesWorker(AutoTestResultsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.AutoTestResult : data.AutoTestResult.WithPath(_pathEdgesAutoTestResult);

                src = src.ApplyFilter(filter.AutoTestsId, x => x.AutoTestsId, FilterType.Equal);
                src = src.ApplyFilter(filter.AutoItemsId, x => x.AutoItemsId, FilterType.Equal);
                src = src.ApplyFilter(filter.AutoProtocolStageRevisionsId, x => x.AutoProtocolStageRevisionsId, FilterType.Equal);
                src = src.ApplyFilter(filter.AutoTestResultParentId, x => x.AutoTestResultsParentId, FilterType.Equal);
                src = src.ApplyFilter(filter.PreliminaryReading, x => x.PreliminaryReading, FilterType.Equal);
                src = src.ApplyFilter(filter.SummaryReading, x => x.SummaryReading, FilterType.Equal);
                src = src.ApplyFilter(filter.IsAddedManually, x => x.IsAddedManually, FilterType.Equal);
                src = src.ApplyFilter(filter.Notes, x => x.Notes, FilterType.StringEqualOrContains);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey, 
                                    x => x.Notes);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region AutoTestResultProduct

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<AutoTestResultProduct> LoadEntitiesWorker(AutoTestResultProductFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.AutoTestResultProduct : data.AutoTestResultProduct.WithPath(_pathEdgesAutoTestResultProduct);

                src = src.ApplyFilter(filter.AutoTestResultsId, x => x.AutoTestResultsId, FilterType.Equal);
                src = src.ApplyFilter(filter.ProductFormsId, x => x.ProductFormsId, FilterType.Equal);
                src = src.ApplyFilter(filter.ProductSizesId, x => x.ProductSizesId, FilterType.Equal);
                src = src.ApplyFilter(filter.Quantity, x => x.Quantity, FilterType.Equal);
                src = src.ApplyFilter(filter.Price, x => x.Price, FilterType.Equal);
                src = src.ApplyFilter(filter.IsChecked, x => x.IsChecked, FilterType.Equal);
                src = src.ApplyFilter(filter.Duration, x => x.Duration, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Schedule, x => x.Schedule, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.SuggestedUsage, x => x.SuggestedUsage, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Comments, x => x.Comments, FilterType.StringEqualOrContains);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey, 
                                    x => x.Form, 
                                    x => x.Size, 
                                    x => x.Duration, 
                                    x => x.Schedule, 
                                    x => x.SuggestedUsage, 
                                    x => x.Comments);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region Products

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<Product> LoadEntitiesWorker(ProductsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.Product : data.Product.WithPath(_pathEdgesProduct);

                src = src.ApplyFilter(filter.AutoItemsId, x => x.AutoItemsId, FilterType.Equal);
                src = src.ApplyFilter(filter.Supplier, x => x.Supplier, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.IngredientsString, x => x.IngredientsString, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Supports, x => x.Supports, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.UsefulFor, x => x.UsefulFor, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Price, x => x.Price, FilterType.Equal);
                src = src.ApplyFilter(filter.DiscountPercentage, x => x.DiscountPercentage, FilterType.Equal);
                src = src.ApplyFilter(filter.HasDiscount, x => x.HasDiscount, FilterType.Equal);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey, 
                                    x => x.Supplier, 
                                    x => x.IngredientsString, 
                                    x => x.Supports, 
                                    x => x.UsefulFor);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region ProductForms

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<ProductForm> LoadEntitiesWorker(ProductFormsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.ProductForm : data.ProductForm.WithPath(_pathEdgesProductForm);

                src = src.ApplyFilter(filter.ProductsId, x => x.ProductsId, FilterType.Equal);
                src = src.ApplyFilter(filter.StatusLookupId, x => x.StatusLookupId, FilterType.Equal);
                src = src.ApplyFilter(filter.Form, x => x.Form, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.SuggestedUsage, x => x.SuggestedUsage, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.UsageSchedule, x => x.UsageSchedule, FilterType.StringEqualOrContains);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey, 
                                    x => x.Form, 
                                    x => x.SuggestedUsage, 
                                    x => x.UsageSchedule);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region ProductSizes

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<ProductSize> LoadEntitiesWorker(ProductSizesFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.ProductSize : data.ProductSize.WithPath(_pathEdgesProductSize);

                src = src.ApplyFilter(filter.ProductFormsId, x => x.ProductFormsId, FilterType.Equal);
                src = src.ApplyFilter(filter.StatusLookupId, x => x.StatusLookupsId, FilterType.Equal);
                src = src.ApplyFilter(filter.Size, x => x.Size, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Price, x => x.Price, FilterType.Equal);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey, 
                                    x => x.Size);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region ClinicProductPricings

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<ClinicProductPricing> LoadEntitiesWorker(ClinicProductPricingsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.ClinicProductPricing : data.ClinicProductPricing.WithPath(_pathEdgesClinicProductPricing);

                src = src.ApplyFilter(filter.ProductsId, x => x.ProductsId, FilterType.Equal);
                src = src.ApplyFilter(filter.Form, x => x.Form, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Size, x => x.Size, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Price, x => x.Price, FilterType.Equal);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey, 
                                    x => x.Form, 
                                    x => x.Size);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region DosageOptions

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<DosageOption> LoadEntitiesWorker(DosageOptionsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.DosageOption : data.DosageOption.WithPath(_pathEdgesDosageOption);

                src = src.ApplyFilter(filter.ProductsId, x => x.ProductForm.ProductsId, FilterType.Equal);
                src = src.ApplyFilter(filter.ProductFormsId, x => x.ProductFormsId, FilterType.Equal);
                src = src.ApplyFilter(filter.Order, x => x.Order, FilterType.Equal);
                src = src.ApplyFilter(filter.Name, x => x.Name, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.UsageSchedule, x => x.UsageSchedule, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.SuggestedUsage, x => x.SuggestedUsage, FilterType.StringEqualOrContains);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey,
                                    x => x.Name,
                                    x => x.UsageSchedule);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #endregion
    }
}
