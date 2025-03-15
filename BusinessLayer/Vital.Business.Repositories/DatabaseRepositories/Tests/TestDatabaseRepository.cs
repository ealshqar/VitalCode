using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.Tests
{
    public class TestDatabaseRepository : BaseRepository,ITestRepository
    {
        #region Path Edges

        /*IMPORTANT:
        When fetching second or third level properties in the path edge for the same object typs that is being fetched
        in different nodes of path, make sure that the fetch statment for that second or third level object is the same
        since this object is being fetched using the first fetch statment that the LLBLGen encounters so if this statment
        doesn't include your nested fetch, then the object needed won't be fetched since the second statment that fetches
        it is not being called since the first level object was being fetched already.*/

        /*Sample:
        Fetch:
        p => p
        .A
          .C
        .B
          .C
            .E
        
        We are trying to fetch C with Object property E in it, but if A has object of type C that is the same object
        that B has, then A will fetch C without E since the statment for A doesn't include E and the statment for B will
        not fetch C since it is already there and E will be missed so the correct path will be like this:
        
        Fetch:
        p => p
        .A
          .C
            .E
        .B
          .C
            .E
        
        */
        private readonly Func<IPathEdgeRootParser<TestEntity>, IPathEdgeRootParser<TestEntity>> _pathEdgesTest =
            p => p
                /*1*/.Prefetch<PatientEntity>(cc => cc.Patient)
                     /*1.1*/.SubPath(tp => tp.Prefetch(tpg => tpg.Lookup))
                /*2*/.Prefetch<ItemEntity>(k => k.Item)
                /*3*/.Prefetch<TestScheduleEntity>(ts => ts.TestSchedule)
                       /*L*/.SubPath(l => 
                                    l.Prefetch(evl => evl.EvalPeriodType)
                                     .Prefetch(sda => sda.DiscountApply))
                      /*L*/.SubPath(tss => tss
                          /*3.1*/.Prefetch<ScheduleLineEntity>(sl => sl.ScheduleLines)
                                /*L*/.SubPath(sll => sll
                                    /*3.1.1*/.Prefetch<ItemEntity>(slt => slt.Item)
                                    .SubPath(a => a.Prefetch<ItemPropertyEntity>(aa => aa.Properties)
                                        .SubPath(aaa => aaa.Prefetch<PropertyEntity>(aaaa => aaaa.Property).SubPath(pr => pr.Prefetch(pat => pat.ApplicableTypeLookup).Prefetch(pvt => pvt.ValueTypeLookup))))
                                            /*L*/.SubPath(a => a
                                                /*3.1.2*/.Prefetch<LookupEntity>(aa => aa.TypeLookup)
                                                                .Prefetch<LookupEntity>(aa => aa.ListTypeLookup)
                                                                .Prefetch<LookupEntity>(aa => aa.ItemSourceLookup))))
                /*4*/.Prefetch<TestProtocolEntity>(pp => pp.TestProtocol)
                      /*L*/.SubPath(ps => ps
                          /*4.1*/.Prefetch<ProtocolStepEntity>(pss => pss.ProtocolSteps)
                                /*L*/.SubPath(pot => pot
                                    /*4.1.1*/.Prefetch(pott => pott.Type)))
                     .Prefetch<TestServiceEntity>(ss=>ss.TestServices)
                         .SubPath(sst => sst
                               .Prefetch(sstt=>sstt.TypeLookup)
                               .Prefetch(ssttt=>ssttt.Service))
                /*5*/.Prefetch<TestIssueEntity>(ccc => ccc.TestIssues)
                     .FilterOn(f=>f.IsMainIssue == false)
                    /*L*/.SubPath(ts => ts
                        /*5.1*/.Prefetch<ProtocolStepEntity>(tsp => tsp.ProtocolStep)
                              /*L*/.SubPath(po => po
                                  /*5.1.1*/.Prefetch(poo => poo.Type)))
                    /*L*/.SubPath(q => q
                        /*5.2*/.Prefetch<ItemEntity>(qq => qq.Item)
                        .SubPath(a => a.Prefetch<ItemPropertyEntity>(aa => aa.Properties)
                                .SubPath(aaa => aaa.Prefetch<PropertyEntity>(aaaa => aaaa.Property).SubPath(pr => pr.Prefetch(pat => pat.ApplicableTypeLookup).Prefetch(pvt => pvt.ValueTypeLookup))))
                        .SubPath(a => a.Prefetch<LookupEntity>(aa => aa.TypeLookup)
                                                            .Prefetch<LookupEntity>(aa => aa.ListTypeLookup)
                                                            .Prefetch<LookupEntity>(aa => aa.ItemSourceLookup))
                              /*L*/.SubPath(m => m
                                  /*5.2.1*/.Prefetch<ItemDetailsEntity>(zzz => zzz.ItemDetail)
                                          /*L*/.SubPath(e => e
                                              /*5.2.1.1*/.Prefetch<ImageEntity>(ee => ee.Image).Exclude(kk => kk.Data))))
                    /*L*/.SubPath(ns => ns
                        /*5.3*/.Prefetch<IssueNavigationStepEntity>(nss => nss.IssueNavigationSteps)
                              /*L*/.SubPath(si => si
                                  /*5.3.1*/.Prefetch<ItemEntity>(sit => sit.Item)
                                  /*L*/.SubPath(m => m
                                      /*5.2.1*/.Prefetch<ItemDetailsEntity>(zzz => zzz.ItemDetail)
                                      /*L*/.SubPath(e => e
                                          /*5.2.1.1*/.Prefetch<ImageEntity>(ee => ee.Image).Exclude(kk => kk.Data)))
                                                .SubPath(a => a.Prefetch<ItemPropertyEntity>(aa => aa.Properties)
                                                    .SubPath(aaa => aaa.Prefetch<PropertyEntity>(aaaa => aaaa.Property).SubPath(pr => pr.Prefetch(pat => pat.ApplicableTypeLookup).Prefetch(pvt => pvt.ValueTypeLookup))))
                                          /*L*/.SubPath(a => a
                                              /*5.3.1*/.Prefetch<LookupEntity>(aa => aa.TypeLookup)
                                                                                .Prefetch<LookupEntity>(aa => aa.ListTypeLookup)
                                                                                .Prefetch<LookupEntity>(aa => aa.ItemSourceLookup))        
                                  /*5.3.2*/.Prefetch(parent => parent.ParentStep)))
                    /*L*/.SubPath(tr => tr
                        /*5.4*/.Prefetch<TestResultEntity>(trr => trr.TestResults)
                              /*L*/.SubPath(tre => tre
                                  /*5.4.1*/.Prefetch(trv => trv.VitalForce)
                                  /*5.4.3*/.Prefetch(trr => trr.RatioItem)
                                  /*5.4.2*/.Prefetch<ItemEntity>(tri => tri.Item)
                                  /*L*/.SubPath(m => m
                                      /*5.2.1*/.Prefetch<ItemDetailsEntity>(zzz => zzz.ItemDetail)
                                      /*L*/.SubPath(e => e
                                          /*5.2.1.1*/.Prefetch<ImageEntity>(ee => ee.Image).Exclude(kk => kk.Data)))
                                                .SubPath(a => a.Prefetch<ItemPropertyEntity>(aa => aa.Properties)
                                                    .SubPath(aaa => aaa.Prefetch<PropertyEntity>(aaaa => aaaa.Property).SubPath(pr => pr.Prefetch(pat => pat.ApplicableTypeLookup).Prefetch(pvt => pvt.ValueTypeLookup))))
                                                .SubPath(tip => tip
                                                    .Prefetch(tipp => tipp.Parents))
                                           /*L*/.SubPath(tt => tt
                                               /*5.4.2.1*/.Prefetch(ttt => ttt.TypeLookup)
                                               .Prefetch<LookupEntity>(aa => aa.ListTypeLookup)
                                               .Prefetch<LookupEntity>(aa => aa.ItemSourceLookup))
                                  /*5.4.3*/.Prefetch(tri => tri.StepType)
                                  /*5.4.4*/.Prefetch(tri => tri.TestProtocol)
                                  /*5.4.5*/.Prefetch<TestResultEntity>(trp => trp.Parent)
                                          /*L*/.SubPath(trpi => trpi
                                              /*5.4.6*/.Prefetch<ItemEntity>(tri => tri.Item)
                                                      /*L*/.SubPath(tt => tt
                                                          /*5.4.6.1*/.Prefetch(ttt => ttt.TypeLookup)
                                                          .Prefetch<LookupEntity>(aa => aa.ListTypeLookup)
                                                          .Prefetch<LookupEntity>(aa => aa.ItemSourceLookup))
                                                          /*5.4.6.2*/.Prefetch(trpis => trpis.TestIssue))
                                  /*5.4.7*/.Prefetch<TestResultEntity>(sp => sp.SelectedParent)
                                          /*L*/.SubPath(trpi => trpi
                                              /*5.4.8*/.Prefetch<ItemEntity>(trpii => trpii.Item)
                                                      /*L*/.SubPath(item => item
                                                          /*5.4.8.1*/.Prefetch(itemType => itemType.TypeLookup)
                                                          .Prefetch<LookupEntity>(aa => aa.ListTypeLookup)
                                                          .Prefetch<LookupEntity>(aa => aa.ItemSourceLookup))
                                                          /*5.4.8.2*/.Prefetch(trpis => trpis.TestIssue))
                        /*5.4.9*/.Prefetch<TestResultFactorsEntity>(trp => trp.TestResultFactors)
                                /*L*/.SubPath(trfe => trfe
                                    /*5.4.9.1*/.Prefetch(trfee => trfee.Factor)
                                    /*5.4.9.2*/.Prefetch(trfee => trfee.Potency))))
                    /*5.5*/.Prefetch<ReadingEntity>(ccc => ccc.Readings)
                          /*L*/.SubPath(q => q
                              /*5.5.1*/.Prefetch<ItemEntity>(qq => qq.Item)
                                      /*L*/.SubPath(a => a
                                          /*5.5.1.1*/.Prefetch<LookupEntity>(aa => aa.TypeLookup)
                                          .Prefetch<LookupEntity>(aa => aa.ListTypeLookup)
                                          .Prefetch<LookupEntity>(aa => aa.ItemSourceLookup))
                                      /*L*/.SubPath(m => m
                                          /*5.5.1.2*/.Prefetch<ItemDetailsEntity>(zzz => zzz.ItemDetail)
                                                    /*L*/.SubPath(e => e
                                                        /*5.5.1.2.1*/.Prefetch<ImageEntity>(ee => ee.Image).Exclude(kk => kk.Data)))
                                           .SubPath(ee=>ee.Prefetch<ItemRelationEntity>(rr=>rr.Parents)
                                                    .SubPath(pp=>pp.Prefetch(ppp=>ppp.Child))))
                    /*L*/.SubPath(lp => lp
                        /*5.6*/.Prefetch(lpl => lpl.ListPointLookup))
                /*6*/.Prefetch(d => d.StateLookup)
                /*7*/.Prefetch(lp => lp.ListPointLookup)
                /*8*/.Prefetch(dd => dd.TypeLookup)
                /*9*/.Prefetch(o => o.ShippingOrders)
                /*10*/.Prefetch<TestImprintableItemEntity>(o => o.TestImprintableItems)
                            .SubPath(q=>q
                                .Prefetch<ItemEntity>(qq => qq.Item)
                                    .SubPath(ii => ii.Prefetch<ItemPropertyEntity>(it => it.Properties)
                                    .SubPath(ip => ip.Prefetch(ppp=>ppp.Property)))
                                .Prefetch(qq => qq.TestResult));

        private readonly Func<IPathEdgeRootParser<TestEntity>, IPathEdgeRootParser<TestEntity>> _pathEdgesTestLight =
            p => p.Prefetch(zz => zz.TypeLookup).Prefetch(cc => cc.StateLookup).Prefetch(cc => cc.Invoices);

        private readonly Func<IPathEdgeRootParser<TestIssueEntity>, IPathEdgeRootParser<TestIssueEntity>> _pathEdgesTestIssue =
                p => p.Prefetch<IssueNavigationStepEntity>(c => c.IssueNavigationSteps)
                            .SubPath(a => a.Prefetch<ItemEntity>(aa => aa.Item)
                                .SubPath(aa => aa.Prefetch<ItemPropertyEntity>(aaa => aaa.Properties)
                                       .SubPath(aaa => aaa.Prefetch<PropertyEntity>(aaaa => aaaa.Property).SubPath(pr => pr.Prefetch(pat => pat.ApplicableTypeLookup).Prefetch(pvt => pvt.ValueTypeLookup))))
                                .SubPath(c => c.Prefetch(cc => cc.TypeLookup)
                                    .Prefetch<LookupEntity>(aa => aa.ListTypeLookup)
                                    .Prefetch<LookupEntity>(aa => aa.ItemSourceLookup)))
                      .Prefetch<ProtocolStepEntity>(tsp => tsp.ProtocolStep)
                            .SubPath(po => po.Prefetch(poo => poo.Type))
                      .Prefetch<TestEntity>(cc => cc.Test)
                            .SubPath(te => te.Prefetch<TestProtocolEntity>(tp => tp.TestProtocol)
                                                   .SubPath(tps => tps.Prefetch<ProtocolStepEntity>(steps => steps.ProtocolSteps)
                                                        .SubPath(ll => ll.Prefetch(ls => ls.Type))))
                      .Prefetch<ItemEntity>(ti => ti.Item)
                      .SubPath(a => a.Prefetch<ItemPropertyEntity>(aa => aa.Properties)
                                        .SubPath(aaa => aaa.Prefetch<PropertyEntity>(aaaa => aaaa.Property).SubPath(pr => pr.Prefetch(pat => pat.ApplicableTypeLookup).Prefetch(pvt => pvt.ValueTypeLookup))))
                        .SubPath(a => a.Prefetch(aa => aa.TypeLookup)
                            .Prefetch<LookupEntity>(aa => aa.ListTypeLookup)
                            .Prefetch<LookupEntity>(aa => aa.ItemSourceLookup))
                      .Prefetch<TestResultEntity>(trr => trr.TestResults)
                            .SubPath(tre => tre.Prefetch(trv => trv.VitalForce)
                                                .Prefetch(trr => trr.RatioItem)
                                                .Prefetch<ItemEntity>(tri => tri.Item)
                                                .SubPath(a => a.Prefetch<ItemPropertyEntity>(aa => aa.Properties)
                                                    .SubPath(aaa => aaa.Prefetch(aaaa => aaaa.Property)))
                                                    .SubPath(c => c.Prefetch(cc => cc.TypeLookup)
                                                        .Prefetch<LookupEntity>(aa => aa.ListTypeLookup)
                                                        .Prefetch<LookupEntity>(aa => aa.ItemSourceLookup)
                                                                 .Prefetch(q => q.Parents))
                                                .Prefetch(tri => tri.StepType)                                                
                                                .Prefetch(tri => tri.TestProtocol)                                                
                                                .Prefetch<TestResultEntity>(trp => trp.Parent)
                                                         .SubPath(trpi => trpi.Prefetch<ItemEntity>(trpii => trpii.Item)
                                                             .SubPath(a => a.Prefetch<ItemPropertyEntity>(aa => aa.Properties)
                                                                .SubPath(aaa => aaa.Prefetch<PropertyEntity>(aaaa => aaaa.Property).SubPath(pr => pr.Prefetch(pat => pat.ApplicableTypeLookup).Prefetch(pvt => pvt.ValueTypeLookup))))
                                                             .SubPath(c => c.Prefetch(cc => cc.TypeLookup)
                                                                 .Prefetch<LookupEntity>(aa => aa.ListTypeLookup)
                                                                 .Prefetch<LookupEntity>(aa => aa.ItemSourceLookup))
                                                              .Prefetch(trpis => trpis.TestIssue))
                                                .Prefetch<TestResultEntity>(sp => sp.SelectedParent)
                                                        .SubPath(trpi => trpi.Prefetch<ItemEntity>(trpii => trpii.Item)
                                                            .SubPath(a => a.Prefetch<ItemPropertyEntity>(aa => aa.Properties)
                                                                .SubPath(aaa => aaa.Prefetch<PropertyEntity>(aaaa => aaaa.Property).SubPath(pr => pr.Prefetch(pat => pat.ApplicableTypeLookup).Prefetch(pvt => pvt.ValueTypeLookup))))
                                                            .SubPath(c => c.Prefetch(cc => cc.TypeLookup)
                                                                .Prefetch<LookupEntity>(aa => aa.ListTypeLookup)
                                                                .Prefetch<LookupEntity>(aa => aa.ItemSourceLookup))
                                                            .Prefetch(trpis => trpis.TestIssue))
                                                .Prefetch<TestResultFactorsEntity>(trp => trp.TestResultFactors)
                                                        .SubPath(trfe => trfe.Prefetch(trfee => trfee.Factor)
                                                            .Prefetch(trfee => trfee.Potency))
                                                            );


        private readonly Func<IPathEdgeRootParser<TestEntity>, IPathEdgeRootParser<TestEntity>>
            _pathEdgesTestMajorIssues =
                p => p.Prefetch<TestIssueEntity>(t => t.TestIssues)
                    .FilterOn(f => f.IsMainIssue == false)    
                    .SubPath(ti =>ti
                        .Prefetch(tii => tii.Item)
                        .Prefetch<TestResultEntity>(tii=>tii.TestResults).SubPath(tr=>tr
                            .Prefetch(trr=>trr.Item)
                            .Prefetch<TestResultFactorsEntity>(trr=>trr.TestResultFactors).SubPath(trf=>trf
                                .Prefetch(trff=>trff.Factor))));

        private readonly Func<IPathEdgeRootParser<IssueNavigationStepEntity>, IPathEdgeRootParser<IssueNavigationStepEntity>> _pathEdgesIssueNavigationStep = 
            p => p.Prefetch(c => c.TestIssue)
                  .Prefetch < IssueNavigationStepEntity>(cc => cc.ParentStep)
                       .SubPath(pi => pi.Prefetch(i=>i.Item))
                  .Prefetch(cc => cc.ChildSteps)
                  .Prefetch(i => i.Item)
                  .Prefetch(parent => parent.ParentStep);


        private readonly Func<IPathEdgeRootParser<TestResultEntity>, IPathEdgeRootParser<TestResultEntity>> _pathEdgesTestResult = 
            p => p.Prefetch(tr => tr.VitalForce)
                .Prefetch(trr => trr.RatioItem)
                .Prefetch(tr => tr.Parent)
                .Prefetch(tri => tri.StepType)
                .Prefetch(tri => tri.TestProtocol)
                .Prefetch(tr => tr.SelectedParent)
                .Prefetch<TestResultFactorsEntity>(tr => tr.TestResultFactors)
                    .SubPath(trf => trf.Prefetch(rf => rf.Factor))
                    .SubPath(trf => trf.Prefetch(rf => rf.Potency))
                .Prefetch<ItemEntity>(i => i.Item).SubPath(a => a.Prefetch(aa => aa.TypeLookup)
                    .Prefetch<LookupEntity>(aa => aa.ListTypeLookup)
                    .Prefetch<LookupEntity>(aa => aa.ItemSourceLookup))
                .Prefetch(ti => ti.TestIssue);



        private readonly Func<IPathEdgeRootParser<TestResultFactorsEntity>, IPathEdgeRootParser<TestResultFactorsEntity>> _pathEdgesTestResultFactor = 
            p => p.Prefetch(f => f.Factor)
                  .Prefetch(f => f.Potency);

        private readonly Func<IPathEdgeRootParser<TestServiceEntity>, IPathEdgeRootParser<TestServiceEntity>>
            _pathEdgesTestService =
                s => s.Prefetch(f => f.TypeLookup);

        private readonly Func<IPathEdgeRootParser<TestImprintableItemEntity>, IPathEdgeRootParser<TestImprintableItemEntity>> _pathEdgesTestImprintableItem =
            p => p.Prefetch<ItemEntity>(qq => qq.Item)
                        .SubPath(ii => ii.Prefetch<ItemPropertyEntity>(it => it.Properties)
                        .SubPath(ip => ip.Prefetch(ppp => ppp.Property)))
                  .Prefetch(zz => zz.Test)
                  .Prefetch(cc => cc.TestResult);

        #endregion

        #region Public Methods

        #region Tests

        /// <summary>
        /// Loads test by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The test</returns>
        public Test LoadTestById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.Test.Where(c => c.Id == id).WithPath(_pathEdgesTest);

                    var test = src.FirstOrDefault();

                    var testObj = new Test();

                    Mapper.Map(test, testObj);

                    //This force to map to solve the issue of loading the ItemDetails in side the issue item, issue item some times map to null.
                    if (test != null && test.TestIssues != null)
                    {
                        for (int index = 0; index < test.TestIssues.Count; index++)
                        {
                            var testIssueEntity = test.TestIssues[index];
                            Mapper.Map(testIssueEntity.Item, testObj.TestIssues[index].Item);
                        }
                    }

                    //This code is needed to fill the parent in each single imprintable item regardless of how many levels we have in the tree of items
                    //this is happening because we create the path edge in a way that is dynamic so each item load its parent and the parent loads its parent and
                    //so on until the top most parent is loaded, so what we do below is that we mape the ParentImprintableId property from the entity and then 
                    //use it in the TestImprintableItem to get its parent from the DB so the structure and logic of the tree works correctly.
                    if (test != null && test.TestImprintableItems != null)
                    {
                        for (int index = 0; index < test.TestImprintableItems.Count; index++)
                        {
                            var testImprintableItemEntity = test.TestImprintableItems[index];
                            Mapper.Map(testImprintableItemEntity.ParentImprintableId, testObj.TestImprintableItems[index].ParentImprintableId);
                        }

                        foreach (var testImprintableItem in testObj.TestImprintableItems)
                        {
                            if (testImprintableItem.ParentImprintableId.HasValue)
                            {
                                testImprintableItem.Parent = LoadTestImprintableItemById(testImprintableItem.ParentImprintableId.Value);
                                testImprintableItem.TempParentAfterDelete = testImprintableItem.Parent;//Store the parent that is coming from DB to allow deleting handling later if needed
                            }
                        }
                    }

                    return testObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads test by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The test</returns>
        public Test LoadTestAndMajorIssuesAndFactorsById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.Test.Where(c => c.Id == id).WithPath(_pathEdgesTestMajorIssues);

                    var test = src.FirstOrDefault();

                    var testObj = new Test();

                    Mapper.Map(test, testObj);

                    //This force to map to solve the issue of loading the ItemDetails in side the issue item, issue item some times map to null.
                    if (test != null && test.TestIssues != null)
                    {
                        for (int index = 0; index < test.TestIssues.Count; index++)
                        {
                            var testIssueEntity = test.TestIssues[index];
                            Mapper.Map(testIssueEntity.Item, testObj.TestIssues[index].Item);
                        }
                    }

                    return testObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of tests.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="patientId">The patient id.</param>
        /// <param name="itemId">The points group id.</param>
        /// <param name="testProtocolId">Test protocol id.</param>
        /// <param name="dateTime">The date time.</param>
        /// <param name="loadingType">The loading type.</param>
        /// <returns></returns>
        public BindingList<Test> LoadTests(string name, int patientId, int itemId, int testProtocolId, DateTime? dateTime, LoadingTypeEnum loadingType)
        {
            try
            {
                return LoadTestsWorker(name, patientId, itemId, testProtocolId, dateTime, loadingType == LoadingTypeEnum.All ? _pathEdgesTest : (loadingType == LoadingTypeEnum.Light ? _pathEdgesTestLight : null));
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }
        
        /// <summary>
        /// Saves a test.
        /// </summary>
        /// <param name="testToSave">The test.</param>
        /// <returns>The result.</returns>
        public ProcessResult Save(Test testToSave)
        {
            Check.Argument.IsNotNull(testToSave, "test to save");

            try
            {
                var testEntity = Mapper.Map<Test, TestEntity>(testToSave);

                testEntity.IsNew = testEntity.Id <= 0;

                var processResult = CommonRepository.Save(testEntity);

                testToSave.Id = testEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a test.
        /// </summary>
        /// <param name="testToDelete">The test.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(Test testToDelete)
        {
            Check.Argument.IsNotNull(testToDelete, "test to delete");

            try
            {
                var testEntity = Mapper.Map<Test, TestEntity>(testToDelete);

                return CommonRepository.Delete(testEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }
        
        #endregion

        #region Test Results

        /// <summary>
        /// Loads test result by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The test result</returns>
        public TestResult LoadTestResultById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.TestResult.Where(c => c.Id == id).WithPath(_pathEdgesTestResult);

                    var testResult = src.FirstOrDefault();

                    var testResultObj = new TestResult();

                    Mapper.Map(testResult, testResultObj);

                    return testResultObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of test results.
        /// </summary>
        /// <param name="testIssueId">Issue Id.</param>
        /// <param name="itemId">Item Id.</param>
        /// <param name="parentId">Parent Id.</param>
        /// <param name="vitalForceId">VF Id.</param>
        /// <param name="isSelected">Is selected. </param>
        /// <returns></returns>
        public BindingList<TestResult> LoadTestResults(int testIssueId, int itemId, int parentId, int vitalForceId , bool isSelected)
        {
            try
            {
                return LoadTestResultsWorker(testIssueId, itemId, parentId, vitalForceId, isSelected, _pathEdgesTestResult);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Saves a test result.
        /// </summary>
        /// <param name="testResultToSave">The test result.</param>
        /// <returns>The result.</returns>
        public ProcessResult Save(TestResult testResultToSave)
        {
            Check.Argument.IsNotNull(testResultToSave, "test result to save");

            try
            {
                var testResultEntity = Mapper.Map<TestResult, TestResultEntity>(testResultToSave);

                testResultEntity.IsNew = testResultEntity.Id <= 0;

                var processResult = CommonRepository.Save(testResultEntity);

                testResultToSave.Id = testResultEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a test result.
        /// </summary>
        /// <param name="testResultToDelete">The test result.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(TestResult testResultToDelete)
        {
            Check.Argument.IsNotNull(testResultToDelete, "test result to delete");

            try
            {
                var testResultEntity = Mapper.Map<TestResult, TestResultEntity>(testResultToDelete);

                return CommonRepository.Delete(testResultEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region TestIssues
        
        /// <summary>
        /// Loads TestIssue by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The TestIssue</returns>
        public TestIssue LoadTestIssueById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.TestIssue.Where(c => c.Id == id).WithPath(_pathEdgesTestIssue);

                    var testIssue = src.FirstOrDefault();

                    var testIssueObj = new TestIssue();

                    Mapper.Map(testIssue, testIssueObj);

                    return testIssueObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of TestIssues.
        /// </summary>
        /// <returns>List of TestIssues.</returns>
        public BindingList<TestIssue> LoadTestIssues(string name, int testId, int protocolStepId, int itemId, TestIssuesLoadingType issuesLoadingType)
        {
            try
            {
                return LoadTestIssuesWorker(name, testId, protocolStepId, itemId, issuesLoadingType, _pathEdgesTestIssue);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of TestIssues.
        /// </summary>
        /// <returns>List of TestIssues.</returns>
        public BindingList<TestIssue> LoadLightTestIssues(string name, int testId, int protocolStepId, int itemId)
        {
            try
            {
                return LoadTestIssuesWorker(name, testId, protocolStepId, itemId, TestIssuesLoadingType.NormalIssuesOnly, null);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves a testIssue.
        /// </summary>
        /// <param name="testIssueToSave">The testIssue.</param>
        /// <returns>The testIssue.</returns>
        public ProcessResult Save(TestIssue testIssueToSave)
        {
            Check.Argument.IsNotNull(testIssueToSave, "testIssue to save");

            try
            {
                var testIssueEntity = Mapper.Map<TestIssue, TestIssueEntity>(testIssueToSave);

                testIssueEntity.IsNew = testIssueEntity.Id <= 0;

                var processResult = CommonRepository.Save(testIssueEntity);

                testIssueToSave.Id = testIssueEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a testIssue.
        /// </summary>
        /// <param name="testIssueToDelete">The testIssue.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(TestIssue testIssueToDelete)
        {
            Check.Argument.IsNotNull(testIssueToDelete, "testIssue to delete");

            try
            {
                var testIssueEntity = Mapper.Map<TestIssue, TestIssueEntity>(testIssueToDelete);

                return CommonRepository.Delete(testIssueEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region Test Imprintable Items

        /// <summary>
        /// Loads Test Imprintable Item by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The TestImprintableItem</returns>
        public TestImprintableItem LoadTestImprintableItemById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.TestImprintableItem.Where(c => c.Id == id).WithPath(_pathEdgesTestImprintableItem);

                    var testImprintableItem = src.FirstOrDefault();

                    var testImprintableItemObj = new TestImprintableItem();

                    Mapper.Map(testImprintableItem, testImprintableItemObj);

                    return testImprintableItemObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of Test Imprintable Items.
        /// </summary>
        /// <returns>List of TestImprintableItems.</returns>
        public BindingList<TestImprintableItem> LoadTestImprintableItems(int testId, int itemId)
        {
            try
            {
                var testImprintableItems = LoadTestImprintableItemsWorker(testId, itemId, _pathEdgesTestImprintableItem);

                //This code is needed to fill the parent in each single imprintable item regardless of how many levels we have in the tree of items
                //this is happening because we create the path edge in a way that is dynamic so each item load its parent and the parent loads its parent and
                //so on until the top most parent is loaded, so what we do below is that we mape the ParentImprintableId property from the entity and then 
                //use it in the TestImprintableItem to get its parent from the DB so the structure and logic of the tree works correctly.
                foreach (var testImprintableItem in testImprintableItems)
                {
                    if (testImprintableItem.ParentImprintableId.HasValue)
                    {
                        testImprintableItem.Parent = LoadTestImprintableItemById(testImprintableItem.ParentImprintableId.Value);
                        testImprintableItem.TempParentAfterDelete = testImprintableItem.Parent;//Store the parent that is coming from DB to allow deleting handling later if needed
                    }
                }

                return testImprintableItems;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves a TestImprintableItem.
        /// </summary>
        /// <param name="TestImprintableItemToSave">The TestImprintableItem.</param>
        /// <returns>The TestImprintableItem.</returns>
        public ProcessResult Save(TestImprintableItem testImprintableItemToSave)
        {
            Check.Argument.IsNotNull(testImprintableItemToSave, "TestImprintableItem to save");

            try
            {
                var testImprintableItemEntity = Mapper.Map<TestImprintableItem, TestImprintableItemEntity>(testImprintableItemToSave);

                testImprintableItemEntity.IsNew = testImprintableItemEntity.Id <= 0;

                var processResult = CommonRepository.Save(testImprintableItemEntity);

                testImprintableItemToSave.Id = testImprintableItemEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a TestImprintableItem.
        /// </summary>
        /// <param name="TestImprintableItemToDelete">The TestImprintableItem.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(TestImprintableItem testImprintableItemToDelete)
        {
            Check.Argument.IsNotNull(testImprintableItemToDelete, "TestImprintableItem to delete");

            try
            {
                var testImprintableItemEntity = Mapper.Map<TestImprintableItem, TestImprintableItemEntity>(testImprintableItemToDelete);

                return CommonRepository.Delete(testImprintableItemEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region Test Services

        /// <summary>
        /// Loads TestService by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The TestService</returns>
        public TestService LoadTestServiceById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.TestService.Where(c => c.Id == id).WithPath(_pathEdgesTestService);

                    var testService = src.FirstOrDefault();

                    var testServiceObj = new TestService();

                    Mapper.Map(testService, testServiceObj);

                    return testServiceObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of TestServices.
        /// </summary>
        /// <returns>List of TestServices.</returns>
        public BindingList<TestService> LoadTestServices(string key, string name, int typeLookupId, int testId, int serviceId)
        {
            try
            {
                return LoadTestServicesWorker(key, name, typeLookupId, testId, serviceId, _pathEdgesTestService);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }
       
        /// <summary>
        /// Saves a TestService.
        /// </summary>
        /// <param name="testServiceToSave">The TestService.</param>
        /// <returns>The TestService.</returns>
        public ProcessResult Save(TestService testServiceToSave)
        {
            Check.Argument.IsNotNull(testServiceToSave, "TestService to save");

            try
            {
                var testServiceEntity = Mapper.Map<TestService, TestServiceEntity>(testServiceToSave);

                testServiceEntity.IsNew = testServiceEntity.Id <= 0;

                var processResult = CommonRepository.Save(testServiceEntity);

                testServiceToSave.Id = testServiceEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a TestService.
        /// </summary>
        /// <param name="testServiceToDelete">The TestService.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(TestService testServiceToDelete)
        {
            Check.Argument.IsNotNull(testServiceToDelete, "TestService to delete");

            try
            {
                var testServiceEntity = Mapper.Map<TestService, TestServiceEntity>(testServiceToDelete);

                return CommonRepository.Delete(testServiceEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region IssueNavigationSteps
        
        /// <summary>
        /// Loads IssueNavigationStep by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IssueNavigationStep</returns>
        public IssueNavigationStep LoadIssueNavigationStepById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.IssueNavigationStep.Where(c => c.Id == id).WithPath(_pathEdgesIssueNavigationStep);

                    var issueNavigationStep = src.FirstOrDefault();

                    var issueNavigationStepObj = new IssueNavigationStep();

                    Mapper.Map(issueNavigationStep, issueNavigationStepObj);

                    return issueNavigationStepObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of IssueNavigationSteps.
        /// </summary>
        /// <returns>List of IssueNavigationSteps.</returns>
        public BindingList<IssueNavigationStep> LoadIssueNavigationSteps(int order, int issueId)
        {
            try
            {
                return LoadIssueNavigationStepsWorker(order, issueId, _pathEdgesIssueNavigationStep);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }
        
        /// <summary>
        /// Saves a issueNavigationStep.
        /// </summary>
        /// <param name="issueNavigationStepToSave">The issueNavigationStep.</param>
        /// <returns>The issueNavigationStep.</returns>
        public ProcessResult Save(IssueNavigationStep issueNavigationStepToSave)
        {
            Check.Argument.IsNotNull(issueNavigationStepToSave, "issueNavigationStep to save");

            try
            {
                var issueNavigationStepEntity = Mapper.Map<IssueNavigationStep, IssueNavigationStepEntity>(issueNavigationStepToSave);

                issueNavigationStepEntity.IsNew = issueNavigationStepEntity.Id <= 0;

                var processResult = CommonRepository.Save(issueNavigationStepEntity);

                issueNavigationStepToSave.Id = issueNavigationStepEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a issueNavigationStep.
        /// </summary>
        /// <param name="issueNavigationStepToDelete">The issueNavigationStep.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(IssueNavigationStep issueNavigationStepToDelete)
        {
            Check.Argument.IsNotNull(issueNavigationStepToDelete, "issueNavigationStep to delete");

            try
            {
                var issueNavigationStepEntity = Mapper.Map<IssueNavigationStep, IssueNavigationStepEntity>(issueNavigationStepToDelete);

                return CommonRepository.Delete(issueNavigationStepEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region TestResultFactor

        /// <summary>
        /// Loads TestResultFactor by id.
        /// </summary>
        /// <param name="id">The id.</param>
        public TestResultFactor LoadTestResultFactorById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.TestResultFactors.Where(c => c.Id == id).WithPath(_pathEdgesTestResultFactor);

                    var testResultFactor = src.FirstOrDefault();

                    var testResultFactorObj = new TestResultFactor();

                    Mapper.Map(testResultFactor, testResultFactorObj);

                    return testResultFactorObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of TestResultFactors.
        /// </summary>
        /// <returns>List of TestResultFactor.</returns>
        public BindingList<TestResultFactor> LoadTestResultFactors(int factorId, int potencyId, int testResultId)
        {
            try
            {
                return LoadTestResultFactorsWorker(factorId, potencyId, testResultId, _pathEdgesTestResultFactor);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        
        /// <summary>
        /// Saves a TestResultFactor.
        /// </summary>
        /// <param name="testResultFactorToSave">The TestResultFactor.</param>
        /// <returns>The result.</returns>
        public ProcessResult Save(TestResultFactor testResultFactorToSave)
        {
            Check.Argument.IsNotNull(testResultFactorToSave, "TestResultFactor to save");

            try
            {
                var testResultFactorEntity = Mapper.Map<TestResultFactor, TestResultFactorsEntity>(testResultFactorToSave);

                testResultFactorEntity.IsNew = testResultFactorEntity.Id <= 0;

                var processResult = CommonRepository.Save(testResultFactorEntity);

                testResultFactorToSave.Id = testResultFactorEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a TestResultFactor.
        /// </summary>
        /// <param name="testResultFactorToDelete">The TestResultFactor.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(TestResultFactor testResultFactorToDelete)
        {
            Check.Argument.IsNotNull(testResultFactorToDelete, "testResultFactor to delete");

            try
            {
                var testResultFactorEntity = Mapper.Map<TestResultFactor, TestResultFactorsEntity>(testResultFactorToDelete);

                return CommonRepository.Delete(testResultFactorEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        ///  Loads a list of test .
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="patientId">The patient id.</param>
        /// <param name="pointsGroupId">The points group id.</param>
        /// <param name="testProtocolId">The test protocol Id.</param>
        /// <param name="dateTime">The date time.</param>
        /// <param name="pathEdges">The path edges.</param>
        /// <returns>List of tests.</returns>
        private static BindingList<Test> LoadTestsWorker(string name, int patientId, int pointsGroupId, int testProtocolId, DateTime? dateTime, Func<IPathEdgeRootParser<TestEntity>, IPathEdgeRootParser<TestEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                IQueryable<TestEntity> src = data.Test;
                
                if(pathEdges != null)
                {
                    src = src.WithPath(pathEdges);
                }
                
                if (!string.IsNullOrEmpty(name))
                    src = src.Where(c => c.Name.ToLower().Contains(name.ToLower()));

                if (pointsGroupId > 0)
                    src = src.Where(c => c.PointsGroupId == pointsGroupId);

                if (patientId > 0)
                    src = src.Where(c => c.PatientId == patientId);

                if (dateTime.HasValue)
                    src = src.Where(c => c.DateTime.HasValue && c.DateTime.Value.Day == dateTime.Value.Day && c.DateTime.Value.Month == dateTime.Value.Month
                                   && c.DateTime.Value.Year == dateTime.Value.Year);

                if (testProtocolId > 0)
                    src = src.Where(c => c.TestProtocolId == testProtocolId);

                var tests = src.ToList();

                var testObjList = new BindingList<Test>();

                Mapper.Map(tests, testObjList);

                return testObjList;
            }
        }

        /// <summary>
        /// Loads a list of test results depend on the passed filters.
        /// </summary>
        /// <returns>List of test results.</returns>
        private static BindingList<TestResult> LoadTestResultsWorker(int testIssueId, int itemId, int parentId, int vitalForceId, bool isSelected , Func<IPathEdgeRootParser<TestResultEntity>, IPathEdgeRootParser<TestResultEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.TestResult.AsQueryable();

                if (pathEdges != null)
                    src = src.WithPath(pathEdges);

                if (testIssueId > 0)
                    src = src.Where(c => c.IssueId == testIssueId);

                if (parentId > 0)
                    src = src.Where(c => c.ParentId == parentId);

                if (vitalForceId > 0 && itemId > 0)
                    src = src.Where(c => c.VitalForceId == vitalForceId || c.ItemId == itemId);
                else if(itemId > 0)
                    src = src.Where(c => c.ItemId == itemId);
                else if(vitalForceId > 0)
                    src = src.Where(c => c.VitalForceId == vitalForceId);

                if (isSelected)
                    src = src.Where(c => c.IsSelected == isSelected);

                var testResults = src.ToList();

                var testResultsObjList = new BindingList<TestResult>();

                Mapper.Map(testResults, testResultsObjList);

                return testResultsObjList;
            }
        }

        /// <summary>
        /// Loads a list of TestIssues.
        /// </summary>
        /// <returns></returns>
        private static BindingList<TestIssue> LoadTestIssuesWorker(string name, int testId, int testProtocolStepId, int itemId, TestIssuesLoadingType issuesLoadingType, Func<IPathEdgeRootParser<TestIssueEntity>, IPathEdgeRootParser<TestIssueEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                IQueryable<TestIssueEntity> src = data.TestIssue;

                if (pathEdges != null)
                    src = src.WithPath(pathEdges);
                
                //Filter test issues based on type Main or Normal
                switch (issuesLoadingType)
                {
                    case TestIssuesLoadingType.MainIssueOnly:
                        src = src.Where(c => c.IsMainIssue == true);
                        break;
                    case TestIssuesLoadingType.NormalIssuesOnly:
                        src = src.Where(c => c.IsMainIssue == false);
                        break;
                }
                
                if (!string.IsNullOrEmpty(name))
                    src = src.Where(cc => cc.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));

                if (testId > 0)
                    src = src.Where(c => c.TestId == testId);

                if (itemId > 0)
                    src = src.Where(c => c.ItemId == itemId);

                if (testProtocolStepId > 0)
                    src = src.Where(cc => cc.ProtocolStepId == testProtocolStepId);

                var testIssues = src.ToList();

                var testIssuesObjList = new BindingList<TestIssue>();

                Mapper.Map(testIssues, testIssuesObjList);

                return testIssuesObjList;
            }
        }
        
        /// <summary>
        /// Loads a list of TestImprintableItems.
        /// </summary>
        /// <returns></returns>
        private static BindingList<TestImprintableItem> LoadTestImprintableItemsWorker(int testId, int itemId, Func<IPathEdgeRootParser<TestImprintableItemEntity>, IPathEdgeRootParser<TestImprintableItemEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                IQueryable<TestImprintableItemEntity> src = data.TestImprintableItem;

                if (pathEdges != null)
                    src = src.WithPath(pathEdges);

                if (testId > 0)
                    src = src.Where(c => c.TestId == testId);

                if (itemId > 0)
                    src = src.Where(c => c.ItemId == itemId);

                var TestImprintableItems = src.ToList();

                var TestImprintableItemsObjList = new BindingList<TestImprintableItem>();

                Mapper.Map(TestImprintableItems, TestImprintableItemsObjList);

                //This code is needed to fill the parent in each single imprintable item regardless of how many levels we have in the tree of items
                //this is happening because we create the path edge in a way that is dynamic so each item load its parent and the parent loads its parent and
                //so on until the top most parent is loaded, so what we do below is that we mape the ParentImprintableId property from the entity and then 
                //use it in the TestImprintableItem to get its parent from the DB so the structure and logic of the tree works correctly.
                if (TestImprintableItems.Count != 0)
                {
                    for (int index = 0; index < TestImprintableItems.Count; index++)
                    {
                        var testImprintableItemEntity = TestImprintableItemsObjList[index];
                        Mapper.Map(testImprintableItemEntity.ParentImprintableId, TestImprintableItemsObjList[index].ParentImprintableId);
                    }
                }

                return TestImprintableItemsObjList;
            }
        }

        /// <summary>
        /// Loads a list of TestServices.
        /// </summary>
        /// <returns></returns>
        private static BindingList<TestService> LoadTestServicesWorker(string key, string name, int typeLookupId, int testId, int serviceId, Func<IPathEdgeRootParser<TestServiceEntity>, IPathEdgeRootParser<TestServiceEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                IQueryable<TestServiceEntity> src = data.TestService;

                if (pathEdges != null)
                    src = src.WithPath(pathEdges);

                if (!string.IsNullOrEmpty(name))
                    src = src.Where(cc => cc.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));

                if (testId > 0)
                    src = src.Where(c => c.TestId == testId);

                if (serviceId > 0)
                    src = src.Where(c => c.ServiceId == serviceId);

                if (!string.IsNullOrEmpty(key))
                    src = src.Where(s => s.Key.Equals(key));

                if (!string.IsNullOrEmpty(name))
                    src = src.Where(s => s.Name.Equals(name));
                
                if (typeLookupId > 0)
                    src = src.Where(s => s.TypeLookupId == typeLookupId);

                var testServices = src.ToList();

                var testServicesObjList = new BindingList<TestService>();

                Mapper.Map(testServices, testServicesObjList);

                return testServicesObjList;
            }
        }
        
        /// <summary>
        /// Loads a list of IssueNavigationSteps.
        /// </summary>
        /// <returns></returns>
        private static BindingList<IssueNavigationStep> LoadIssueNavigationStepsWorker(int order, int issueId, Func<IPathEdgeRootParser<IssueNavigationStepEntity>, IPathEdgeRootParser<IssueNavigationStepEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.IssueNavigationStep.WithPath(pathEdges);

                if (order > 0)
                    src = src.Where(c => c.Order == order);

                if (issueId > 0)
                    src = src.Where(c => c.TestIssueId == issueId);
                
                var issueNavigationSteps = src.ToList();

                var issueNavigationStepsObjList = new BindingList<IssueNavigationStep>();

                Mapper.Map(issueNavigationSteps, issueNavigationStepsObjList);

                return issueNavigationStepsObjList;
            }
        }

        /// <summary>
        /// Load Test Result Factors Worker
        /// </summary>
        /// <returns></returns>
        private static BindingList<TestResultFactor> LoadTestResultFactorsWorker(int factorId, int potencyId, int testResultId, Func<IPathEdgeRootParser<TestResultFactorsEntity>, IPathEdgeRootParser<TestResultFactorsEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.TestResultFactors.WithPath(pathEdges);

                if(factorId > 0 && potencyId > 0 && testResultId > 0)
                    src = src.Where(c => c.FactorItemId == factorId || c.PotencyItemId == potencyId || c.TestResultId == testResultId);
                
                else if (factorId > 0)
                    src = src.Where(c => c.FactorItemId == factorId);

                else if (potencyId > 0)
                    src = src.Where(c => c.PotencyItemId == potencyId);

                else if (testResultId > 0)
                    src = src.Where(c => c.TestResultId == testResultId);

                var issueNavigationSteps = src.ToList();

                var testResultFactorObjList = new BindingList<TestResultFactor>();

                Mapper.Map(issueNavigationSteps, testResultFactorObjList);

                return testResultFactorObjList;
            }
        }

        #endregion
    }
}
