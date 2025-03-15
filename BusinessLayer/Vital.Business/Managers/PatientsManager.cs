using System;
using System.ComponentModel;
using System.Linq;
using Vital.Business.Repositories.DatabaseRepositories.Patients;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class PatientsManager : BaseManager
    {
        #region Patients

        #region Private Variables

        private readonly IPatientRepository _patientsRepository;
        private readonly TestsManager _testsManager;
        private readonly SpotCheckManager _spotCheckManager;
        private readonly VitalForceSheetManager _vfsManager;
        private readonly FrequencyTestsManager _frequencyTestsManager;

        #endregion

        #region Constructors
            
        /// <summary>
        /// The Constructor.
        /// </summary>
        public PatientsManager()
        {
            _patientsRepository = new PatientDatabaseRepository();
            _testsManager = new TestsManager();
            _spotCheckManager = new SpotCheckManager();
            _vfsManager = new VitalForceSheetManager();
            _frequencyTestsManager = new FrequencyTestsManager();
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Gets a patient.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Patient GetPatientById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId,"item id");

            try
            {
                return _patientsRepository.LoadPatientById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets the number of a new patient
        /// </summary>
        /// <returns></returns>
        public int GetNewPatientNumber()
        {
            try
            {
                return _patientsRepository.GetNewPatientNumber();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }


        /// <summary>
        /// Gets a list of patients.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<Patient> GetPatients(PatientsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _patientsRepository.LoadPatients(filter.Number, filter.Name, filter.City, filter.Email);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <returns></returns>
        public ProcessResult SavePatient(Patient patient)
        {
            Check.Argument.IsNotNull(() => patient);

            if (!patient.IsChanged) return ProcessResult.Succeed;

            try
            { 
                patient.SetUserAndDates();

                var processResult = _patientsRepository.Save(patient);

                var processResultHistory = SavePatientHistory(patient.PatientHistory);

                if (processResult.IsSucceed && processResultHistory.IsSucceed) { patient.ObjectState = DomainEntityState.Unchanged; }

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
        /// Deletes a patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <returns></returns>
        public ProcessResult DeletePatient(Patient patient)
        {
            Check.Argument.IsNotNull(() => patient);

            try
            {
                patient.FrequencyTests = _frequencyTestsManager.GetFrequencyTests(new FrequencyTestsFilter() { PatientId = patient.Id });

                foreach (var frequencytest in patient.FrequencyTests)
                {
                    var result = _frequencyTestsManager.DeleteFrequencyTest(frequencytest);
                    if (!result.IsSucceed) return result;
                }

                patient.SpotChecks = _spotCheckManager.GetSpotChecks(new SpotChecksFilter {PatientId = patient.Id});

                foreach (var spotCheck in patient.SpotChecks)
                {
                    var result = _spotCheckManager.DeleteSpotCheck(spotCheck);
                    if (!result.IsSucceed) return result;
                }

                patient.VFSRecords = _vfsManager.GetVFSs(new VFSsFilter() { PatientId = patient.Id });

                foreach (var vfs in patient.VFSRecords)
                {
                    var result = _vfsManager.DeleteVFS(vfs);
                    if (!result.IsSucceed) return result;
                }

                patient.Tests = _testsManager.GetTests(new TestsFilter {PatientId = patient.Id});
                
                foreach (var test in patient.Tests)
                {
                    var result =  _testsManager.DeleteTest(test);
                    if (!result.IsSucceed) return result;
                }

                var  processResult = _patientsRepository.Delete(patient);

                if (processResult.IsSucceed) { patient.ObjectState = DomainEntityState.Deleted; }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        #endregion

        #endregion

        #region Patients History

        /// <summary>
        /// Deletes a patient history.
        /// </summary>
        /// <param name="patientHistory">The patient history.</param>
        /// <returns></returns>
        public ProcessResult DeletePatientHistory(PatientHistory patientHistory)
        {
            Check.Argument.IsNotNull(() => patientHistory);

            try
            {
                var processResult =  _patientsRepository.Delete(patientHistory);

                if (processResult.IsSucceed)
                {
                    patientHistory.ObjectState = DomainEntityState.Deleted;
                }

                return processResult;
            }
            catch(Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a patient history.
        /// </summary>
        /// <param name="patientHistoryList">The patient history.</param>
        /// <returns></returns>
        public ProcessResult DeletePatientHistory(BindingList<PatientHistory> patientHistoryList)
        {
            Check.Argument.IsNotNull(() => patientHistoryList);

            var result = new ProcessResult {IsSucceed = true};

            try
            {
                foreach (var patientHistory in patientHistoryList)
                {
                    if(patientHistory.Id > 0)
                    {
                        result = _patientsRepository.Delete(patientHistory);

                        patientHistory.ObjectState = DomainEntityState.Deleted;

                        if (!result.IsSucceed) return result;
                    }
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
        /// Saves the patient history.
        /// </summary>
        /// <param name="patientHistory">The patient history.</param>
        /// <returns></returns>
        public ProcessResult SavePatientHistory(PatientHistory patientHistory)
        {
            Check.Argument.IsNotNull(() => patientHistory);

            if (!patientHistory.IsChanged) return ProcessResult.Succeed;

            try
            {
                patientHistory.SetUserAndDates();

                var processResult = _patientsRepository.Save(patientHistory);

                if (processResult.IsSucceed)
                {
                    patientHistory.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the patient history.
        /// </summary>
        /// <param name="patientHistory">The patient history.</param>
        /// <returns></returns>
        private ProcessResult SavePatientHistory(BindingList<PatientHistory> patientHistory)
        {
            Check.Argument.IsNotNull(() => patientHistory);

            var patientList = patientHistory.ToList();

            try
            {
                var processResult = new ProcessResult { IsSucceed = true};
                var deletionProcessResult = new ProcessResult { IsSucceed = true };

                foreach (var history in patientList)
                {
                    history.SetUserAndDates();

                    if(history.ObjectState ==DomainEntityState.Deleted)
                    {
                        deletionProcessResult = _patientsRepository.Delete(history);

                        if(!deletionProcessResult.IsSucceed)
                        {
                            break;
                        }
                    }
                    else
                    {
                        processResult = _patientsRepository.Save(history);

                        if (processResult.IsSucceed)
                        {
                            history.ObjectState = DomainEntityState.Unchanged;
                        }
                        else
                        {
                            processResult.IsSucceed = false;
                            break;
                        }
                    }
                    
                }

                processResult.IsSucceed = (processResult.IsSucceed && deletionProcessResult.IsSucceed);

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
        /// Gets a list of patient history.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<PatientHistory> GetPatientsHistory(PatientHistoryFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _patientsRepository.LoadPatientHistory(filter.PatientId, filter.Type, filter.Name);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Loads patient history by id.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public PatientHistory GetPatientHistoryById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId , "patient history id.");

            try
            {
                return _patientsRepository.LoadPatientHistoryById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        #endregion
    }
}
