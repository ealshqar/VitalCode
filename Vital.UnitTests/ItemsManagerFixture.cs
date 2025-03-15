using System.IO;
using Vital.Business.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vital.Business.Repositories.DatabaseRepositories.Images;
using Vital.Business.Shared.DomainObjects.Images;
using Vital.Business.Shared.DomainObjects.Items;
using System.Collections.Generic;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Properties;
using Vital.Business.Shared.DomainObjects.Users;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.EntityClasses;

namespace Vital.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ItemsManagerTest and is intended
    ///to contain all ItemsManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ItemsManagerFixture
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetItemById
        ///</summary>
        [TestMethod()]
        public void GetItemByIdTest()
        {
            var target = new ItemsManager();
            var filter = new SingleItemFilter() {ItemId = 2529};
            var actual = target.GetItemById(filter);
            //var hasIt = actual.Properties.HasProperty(PropertiesEnum.TestProperty, "ssadas132");
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetItemChildren
        ///</summary>
        [TestMethod()]
        public void GetItemChildrenTest()
        {
            var target = new ItemsManager();
            var filter = new SingleItemFilter() { ItemId = 1 };
            var actual = target.GetItemChildren(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetItemChildren
        ///</summary>
        [TestMethod()]
        public void GetItemAsReadingsTest()
        {
            var target = new ItemsManager();
            var filter = new SingleItemFilter() { ItemId = 1 };
            var actual = target.GetItemsAsReadings(filter);
            Assert.IsNotNull(actual);
        }

       

        /// <summary>
        ///A test for GetItemRelations
        ///</summary>
        [TestMethod()]
        public void GetItemRelationsTest()
        {
            var target = new ItemsManager();
            var filter = new ItemRelationsFilter() { ParentId = 1 };
            var actual = target.GetItemRelations(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetItemRelations
        ///</summary>
        [TestMethod()]
        public void GetItemRelationByIdTest()
        {
            var target = new ItemsManager();
            var filter = new SingleItemFilter() { ItemId = 1 };
            var actual = target.GetItemRelationById(filter);
            Assert.IsNotNull(actual);
        }


        /// <summary>
        ///A test for GetItems
        ///</summary>
        [TestMethod()]
        public void GetItemsTest()
        {
            var target = new ItemsManager();
            var filter = new ItemsFilter() { TargetTypeLookupId = 14};
            var actual = target.GetItems(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for LoadItemParents
        ///</summary>
        [TestMethod()]
        public void LoadItemParentsTest()
        {
            var target = new ItemsManager();
            var filter = new SingleItemFilter() { ItemId = 3};
            var actual = target.GetItemParents(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for SaveItem
        ///</summary>
        [TestMethod()]
        public void SaveItemTest()
        {
            var target = new ItemsManager();
            var item = new Item()
                           {
                               Name = "Test Unit",
                               Description = "Test unit added this one.",
                               CreationDateTime = DateTime.Now,
                               UpdatedDateTime = DateTime.Now,
                               User = new Business.Shared.DomainObjects.Users.User() { Id = 6 },
                               
                           };

            var actual = target.SaveItem(item);
            Assert.IsNotNull(actual);
        }

        

        

        /// <summary>
        ///A test for DeleteItem
        ///</summary>
        [TestMethod()]
        public void DeleteItemTest()
        {
            var target = new ItemsManager();
            var filter = new SingleItemFilter() { ItemId = 5 };
            var item = target.GetItemById(filter);

            if (item != null)
            {
                var result = target.DeleteItem(item);

                Assert.IsTrue(result.IsSucceed);
            }


        }

        [TestMethod()]
        public void PropertyTest()
        {
            var manager = new PropertiesManager();

            var prop = manager.GetProperties(new PropertiesFilter() {Key = "TestProperty"});

            var prop2 = manager.GetProperties(new PropertiesFilter());

            var prop3 = manager.GetPropertyById(new SingleItemFilter() {ItemId = 1});

            prop3.Name = "This is a test property";

            var result = manager.SaveProperty(prop3).IsSucceed;

        }

        [TestMethod()]
        public void MigrateImages()
        {
            var imagesManager = new ImagesDatabaseRepository();

            foreach (string fileName in Directory.GetFiles(@"D:\Images"))
                {
                    var file = new FileInfo(fileName);

                    byte[] b = File.ReadAllBytes(fileName);

                    using (var sr = new MemoryStream())
                    {
                        var image = new ImageEntity()
                                        {
                                            CreationDateTime = DateTime.Now,
                                            Data = b,
                                            UserId = 1,
                                            Path = file.Name,
                                            Extension = file.Extension,
                                            Size = file.Length,
                                            UpdatedDateTime = DateTime.Now
                                        };

                        imagesManager.Save(image);
                    }     
                }
        }

        

    }
}
