using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vital.Business.Integration.Firestore;
using Vital.Business.Integration.Firestore.Dtos.Customer;
using Vital.Business.Integration.Firestore.Services;
using Vital.Business.Integration.Firestore.Shared;
using Vital.Business.Integration.Shared;
using Vital.Business.Managers;
using Vital.UI.Logic_Classes;

namespace Vital.UnitTests
{
    [TestClass]
    public class FirestoreServiceFixture
    {
        [TestMethod()]
        public void CustomerOperations()
        {
            var customer = CreateSampleCustomerObject();

            var service = new CustomerFirestoreService();

            var createResult = service.CreateCustomer(customer);

            Assert.IsNotNull(createResult);

            var getResult = service.GetCustomer(createResult.Id);

            Assert.IsNotNull(getResult);

            var updateVersionInfoResult = service.UpdateVersionInfo(createResult.Id, new VersionInfoFsSubDocuemnt
            {
                Branch = "Changed Branch",
                DbVersion = "Changed DbVersion",
                Version = "Changed Version",
                DataVersion = "Changed DataVersion"
            });

            Assert.IsNotNull(updateVersionInfoResult);

            var updateSeatsInfoResult = service.UpdateSeatsInfo(createResult.Id, 400);

            Assert.IsNotNull(updateSeatsInfoResult);

            var updateStatisticsoResult = service.UpdateStatistics(createResult.Id, new CustomerStatisticsFsSubDocuemnt
            {
                NumOfClients = 700,
                NumOfOrders = 200
            });

            Assert.IsNotNull(updateStatisticsoResult);

            var updateLastShownAnnouncementNum = service.UpdateLastShownAnnouncementNumber(createResult.Id, 2);

            Assert.IsNotNull(updateLastShownAnnouncementNum);

            var updateExecutionInfooResult = service.UpdateExecutionInfo(createResult.Id, new ExecutionInfoFsSubDocument
            {
               Performed = true,
               RunExecutable = true
            });

            Assert.IsNotNull(updateExecutionInfooResult);

            var updateDocuemntResult = service.UpdateCustomer(new CustomerFsDocument
            {
                Id = createResult.Id,
                TechnicianInfo = new CustomerTechnicianInfoFsSubDocuemnt
                {
                    ClinicName = "Changed ClinicName"
                },
                Statistics = new CustomerStatisticsFsSubDocuemnt
                {
                    NumOfClients = 2000
                },
                VersionInfo = new VersionInfoFsSubDocuemnt
                {
                    Version = "Changed Changed Version"
                },
                SeatsInfo = new SeatsInfoFsSubDocuemnt
                {
                    NumOfSeats = 10
                }
            });

            Assert.IsNotNull(updateDocuemntResult);
        }

        [TestMethod()]
        public void AnnouncementOperations()
        {
            var service = new AnnouncementFirestoreService();

            var allResults = service.GetAnnouncements();

            Assert.IsNotNull(allResults);

            var singleResult = service.GetAnnouncement("1");

            Assert.IsNotNull(singleResult);

            var unshownResult = service.GetUnshownAnnouncement(2);

            Assert.IsNotNull(unshownResult);
        }

        [TestMethod()]
        public void AppSettingsOperations()
        {
            var service = new AppSettingsFirestoreService();

            var testAppSettings = service.Get("TEST");

            Assert.IsNotNull(testAppSettings);

            var uatAppSettings = service.Get("UAT");

            Assert.IsNotNull(uatAppSettings);

            var prodAppSettings = service.Get("PROD");

            Assert.IsNotNull(prodAppSettings);
        }

        [TestMethod()]
        public void MigrateProdToDev()
        {
            var result = FirestoreMigrator.MigrateAll(new ProdFirestoreConfig(), new DevFirestoreConfig());

            Assert.IsTrue(result);
        }

        private CustomerFsDocument CreateSampleCustomerObject()
        {
            var vitalKey = Guid.NewGuid().ToString();

            return new CustomerFsDocument
            {
                Id = string.Format("{0}_{1}", "Test Clinic Name".GenerateSlug(), vitalKey),
                Key = vitalKey,
                CheckDongle = true,
                KeyIssueDate = DateTime.Now,
                RegistrationDate = DateTime.Now,
                TechnicianInfo = new CustomerTechnicianInfoFsSubDocuemnt
                {
                    City = "Test",
                    Address = "Address Test",
                    ClinicName = "Test Clinic Name",
                    TechnicianName = "TechnicianName Test",
                    State = "State Test",
                    Phone = "Phone Test",
                    Website = "Website Test",
                    Email = "Email Test",
                    ZipCode = "ZipCode Test"
                },
                VersionInfo = new VersionInfoFsSubDocuemnt
                {
                    Branch = "TEST",
                    Version = "1.0.0.60",
                    DbVersion = "1.0.0.33",
                    DataVersion = "1.0.0.55"
                },
                AutoShutDownConfig = new ActivateActionCustomerSettingsFsSubDocuemnt
                {
                    Activate = true,
                    ActivateDateTime = DateTime.Now,
                    Message = "SD Message ...",
                    OverrideBranch = false
                },
                SeatsInfo = new SeatsInfoFsSubDocuemnt
                {
                    NumOfSeats = 100,
                    NewAddedSeats = 50
                },
                Statistics = new CustomerStatisticsFsSubDocuemnt
                {
                    NumOfClients = 236,
                    NumOfOrders = 200
                },
                ExecutionInfo = new ExecutionInfoFsSubDocument
                {
                    Performed = false,
                    RunExecutable = false,
                    OverrideBranch = false
                },
                VitalActivationInfo = new VitalActivationInfoFsSubDocument
                {
                    IsActivated = true,
                    Message = "A Message ..."
                },
                RunOnlineOnlySettings = new ActivateActionCustomerSettingsFsSubDocuemnt
                {
                    Activate = true,
                    Message = "RO Message ..",
                    ActivateDateTime = DateTime.Now,
                    OverrideBranch = false
                },
                LastShownAnnouncementNumber = 1,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

        }

        [TestMethod()]
        public void GenerateCustomerObject()
        {
            var appInfoManager = new AppInfoManager();
            UiHelperClass.BackupTechInfo();
            
            var customer =  new CustomerFsDocument
            {
                Id = UiHelperClass.GenerateCustomerId(UiHelperClass.TechnicianClinicName, UiHelperClass.VitalKey),
                Key = UiHelperClass.VitalKey,
                CheckDongle = UiHelperClass.CheckForDongle,
                KeyIssueDate = DateTime.Now,
                RegistrationDate = DateTime.Now,
                TechnicianInfo = new CustomerTechnicianInfoFsSubDocuemnt
                {
                    City = UiHelperClass.TechnicianCity,
                    Address = UiHelperClass.TechnicianAddress,
                    ClinicName = UiHelperClass.TechnicianClinicName,
                    TechnicianName = UiHelperClass.TechnicianName,
                    State = UiHelperClass.TechnicianState,
                    Phone = UiHelperClass.TechnicianPhone,
                    Website = UiHelperClass.TechnicianClinicWebsite,
                    Email = UiHelperClass.TechnicianEmail,
                    ZipCode = UiHelperClass.TechnicianZip
                },
                VersionInfo = new VersionInfoFsSubDocuemnt
                {
                    Branch = UiHelperClass.AppBranch,
                    Version = UiHelperClass.GetVitalStoredVersion(appInfoManager).ToString(),
                    DbVersion = UiHelperClass.GetVitalStoredVersion(appInfoManager).ToString(),
                    DataVersion = UiHelperClass.GetDbVersion(appInfoManager).ToString()
                },
                AutoShutDownConfig = new ActivateActionCustomerSettingsFsSubDocuemnt
                {
                    Activate = false,
                    ActivateDateTime = DateTime.Now,
                    Message = "SD Message ...",
                    OverrideBranch = false
                },
                SeatsInfo = new SeatsInfoFsSubDocuemnt
                {
                    NumOfSeats = int.Parse(UiHelperClass.SeatsCount),
                    NewAddedSeats = 0
                },
                Statistics = new CustomerStatisticsFsSubDocuemnt
                {
                    NumOfClients = 0,
                    NumOfOrders = 0
                },
                ExecutionInfo = new ExecutionInfoFsSubDocument
                {
                    Performed = false,
                    RunExecutable = false,
                    OverrideBranch = false
                },
                VitalActivationInfo = new VitalActivationInfoFsSubDocument
                {
                    IsActivated = true,
                    Message = "A Message ..."
                },
                RunOnlineOnlySettings = new ActivateActionCustomerSettingsFsSubDocuemnt
                {
                    Activate = false,
                    Message = "RO Message ..",
                    ActivateDateTime = DateTime.Now,
                    OverrideBranch = false
                },
                LastShownAnnouncementNumber = 1,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            var service = new CustomerFirestoreService();

            var createResult = service.CreateCustomer(customer);

            Assert.IsNotNull(createResult);
        }

    }
}
