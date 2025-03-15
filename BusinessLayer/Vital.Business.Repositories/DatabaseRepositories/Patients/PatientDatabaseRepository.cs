using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.Patients
{
    public class PatientDatabaseRepository : BaseRepository,IPatientRepository
    {
        #region Path Edges

        private readonly Func<IPathEdgeRootParser<PatientEntity>, IPathEdgeRootParser<PatientEntity>> _pathEdgesPatient =
            p => p.Prefetch<PatientHistoryEntity>(o => o.PatientHistory)
                    .SubPath(z => z.Prefetch(zz => zz.Type))
                  .Prefetch(cc => cc.Lookup)
                  .Prefetch<TestEntity>(c => c.Tests)
                    .SubPath(rr=>rr.Prefetch<ReadingEntity>(cc=>cc.Readings)
                    .SubPath(readingSub=>readingSub.Prefetch(reading=>reading.Item))
                    .SubPath(rrr=>rrr.Prefetch(reading=>reading.ListPointLookup)))
                    .SubPath(a => a.Prefetch(aa => aa.TypeLookup))
                    .SubPath(w => w.Prefetch(ww => ww.StateLookup))
                    .Prefetch(c => c.SpotChecks)
                    .Prefetch(c => c.FrequencyTests)
                    .Prefetch(c => c.VFSRecords)
                    .Prefetch<AutoTestEntity>(u => u.AutoTests)
                        .SubPath(at => at.Prefetch(pr => pr.AutoProtocolRevision));

        private readonly Func<IPathEdgeRootParser<PatientEntity>, IPathEdgeRootParser<PatientEntity>> _pathEdgesPatientLight =
            p => p.Prefetch<PatientHistoryEntity>(o => o.PatientHistory)
                    .SubPath(z => z.Prefetch(zz => zz.Type))
                  .Prefetch(cc => cc.Lookup);

        private readonly Func<IPathEdgeRootParser<PatientHistoryEntity>, IPathEdgeRootParser<PatientHistoryEntity>> _pathEdgesPatientHistory = 
            p => p.Prefetch(c => c.Type)
                  .Prefetch(cc => cc.Patient);

        #endregion

        #region Public Methods

        #region Patients
        
        /// <summary>
        /// Loads patient by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The patient</returns>
        public Patient LoadPatientById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.Patient.Where(c => c.Id == id).WithPath(_pathEdgesPatient);

                    var patient = src.FirstOrDefault();

                    var patientObj = new Patient();

                    Mapper.Map(patient, patientObj);

                    return patientObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Gets the number of a new patient
        /// </summary>
        /// <returns>The patient</returns>
        public int GetNewPatientNumber()
        {
            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.Patient.OrderByDescending(p=>p.Number);

                    var patient = src.FirstOrDefault();

                    var patientObj = new Patient();

                    if (patient != null)
                    {
                        Mapper.Map(patient, patientObj);    
                    }
                    
                    return patientObj.Number + 1;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of patients.
        /// </summary>
        /// <returns>List of patients.</returns>
        public BindingList<Patient> LoadPatients(int number, string name, string city, string email)
        {
            try
            {
                return LoadPatientsWorker(number, name, city, email, _pathEdgesPatientLight);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves a patient.
        /// </summary>
        /// <param name="patientToSave">The patient.</param>
        /// <returns>The patient.</returns>
        public ProcessResult Save(Patient patientToSave)
        {
            Check.Argument.IsNotNull(patientToSave, "patient to save");

            try
            {
                var patientEntity = Mapper.Map<Patient, PatientEntity>(patientToSave);

                patientEntity.IsNew = patientEntity.Id <= 0;

                var processResult = CommonRepository.Save(patientEntity);

                patientToSave.Id = patientEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a patient.
        /// </summary>
        /// <param name="patientToDelete">The patient.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(Patient patientToDelete)
        {
            Check.Argument.IsNotNull(patientToDelete, "patient to delete");

            try
            {
                var patientEntity = Mapper.Map<Patient, PatientEntity>(patientToDelete);

                return CommonRepository.Delete(patientEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region Patients History

        /// <summary>
        /// Deletes a patient history.
        /// </summary>
        /// <param name="patientHistoryToDelete">The patient history.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(PatientHistory patientHistoryToDelete)
        {
            Check.Argument.IsNotNull(patientHistoryToDelete, "patient to delete");

            try
            {
                var patientHistoryEntity = Mapper.Map<PatientHistory, PatientHistoryEntity>(patientHistoryToDelete);

                return CommonRepository.Delete(patientHistoryEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }


        /// <summary>
        /// Saves a patient history.
        /// </summary>
        /// <param name="patientHistoryToSave">The patient history.</param>
        /// <returns>The patient history.</returns>
        public ProcessResult Save(PatientHistory patientHistoryToSave)
        {
            Check.Argument.IsNotNull(patientHistoryToSave, "patient to save");

            try
            {
                var patientHistoryEntity = Mapper.Map<PatientHistory,PatientHistoryEntity>(patientHistoryToSave);

                patientHistoryEntity.IsNew = patientHistoryEntity.Id <= 0;

                var processResult = CommonRepository.Save(patientHistoryEntity);

                patientHistoryToSave.Id = patientHistoryEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a patient history by id.
        /// </summary>
        /// <param name="id">The patient history id.</param>
        /// <returns></returns>
        public PatientHistory LoadPatientHistoryById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.PatientHistory.AsQueryable();

                    src = src.Where(c => c.Id == id);

                    var patientHistoryEntity = src.FirstOrDefault();

                    var patientHistory = new PatientHistory();

                    Mapper.Map(patientHistoryEntity, patientHistory);

                    return patientHistory;
                }
            }
            catch(Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of patients history.
        /// </summary>
        /// <returns>List of patients history.</returns>
        public BindingList<PatientHistory> LoadPatientHistory(int ? patientId , int ? type, string name)
        {
            try
            {
                return LoadPatientHistoryWorker(patientId , type,name, _pathEdgesPatientHistory);
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
        /// Loads a list of patients.
        /// </summary>
        /// <returns></returns>
        private static BindingList<Patient> LoadPatientsWorker(int number, string name, string city, string email, Func<IPathEdgeRootParser<PatientEntity>, IPathEdgeRootParser<PatientEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.Patient.WithPath(pathEdges);

                if (number > 0)
                    src = src.Where(c => c.Number == number);

                if (!string.IsNullOrEmpty(name))
                    src =
                        src.Where(
                            c =>
                            c.FirstName.ToLower().Contains(name.ToLower()) ||
                            c.LastName.ToLower().Contains(name.ToLower()));

                if (!string.IsNullOrEmpty(city))
                    src = src.Where(c => c.City.ToLower().Contains(city.ToLower()));

                if (!string.IsNullOrEmpty(email))
                    src = src.Where(c => c.Email.ToLower().Contains(email.ToLower()));

                var patients = src.ToList();

                var patientsObjList = new BindingList<Patient>();

                Mapper.Map(patients, patientsObjList);

                return patientsObjList;
            }
        }

        /// <summary>
        /// Loads patient history.
        /// </summary>
        /// <param name="patientId">The patient id.</param>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <param name="pathEdges">The path edges.</param>
        /// <returns></returns>
        private static BindingList<PatientHistory> LoadPatientHistoryWorker(int? patientId, int? type, string name, Func<IPathEdgeRootParser<PatientHistoryEntity>, IPathEdgeRootParser<PatientHistoryEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.PatientHistory.WithPath(pathEdges).AsQueryable();

                if (patientId.HasValue && patientId > 0)
                    src = src.Where(c => c.PatientId == patientId);

                if (!string.IsNullOrEmpty(name))
                    src = src.Where(c => c.Name.ToLower().Contains(name.ToLower()));

                if (type.HasValue && type > 0)
                    src = src.Where(c => c.TypeLookupId == type);

                var patientHistoryEntities = src.ToList();

                var patientHistoryList = new BindingList<PatientHistory>();

                Mapper.Map(patientHistoryEntities, patientHistoryList);

                return patientHistoryList;
            }
        }

        #endregion
    }
}
