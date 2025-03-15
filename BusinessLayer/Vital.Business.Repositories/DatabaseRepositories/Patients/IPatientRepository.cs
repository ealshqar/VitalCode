using System.Collections.Generic;
using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.Patients
{
    public interface IPatientRepository
    {
        /// <summary>
        /// Loads patient by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The patient.</returns>
        Patient LoadPatientById(int id);

        /// <summary>
        /// Gets the number of a new patient
        /// </summary>
        /// <returns>The patient</returns>
        int GetNewPatientNumber();

        /// <summary>
        /// Load all patients.
        /// </summary>
        /// <returns>The list of patients.</returns>
        BindingList<Patient> LoadPatients(int number, string name, string city, string email);

        /// <summary>
        /// Saves the patient.
        /// </summary>
        /// <param name="patientToSave">The patient to save.</param>
        /// <returns>The result.</returns>
        ProcessResult Save(Patient patientToSave);

        /// <summary>
        /// Deletes the patient.
        /// </summary>
        /// <param name="patientToDelete">The patient to delete.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(Patient patientToDelete);

        /// <summary>
        /// Loads the patient history by id.
        /// </summary>
        /// <param name="id">The patient history id.</param>
        /// <returns></returns>
        PatientHistory LoadPatientHistoryById(int id);

        /// <summary>
        /// Loads a list of patient history.
        /// </summary>
        /// <param name="patientId">The patient id.</param>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        BindingList<PatientHistory> LoadPatientHistory(int? patientId, int? type, string name);

        /// <summary>
        /// Saves the patient history.
        /// </summary>
        /// <param name="patientHistoryToSave">The patient history</param>
        /// <returns></returns>
        ProcessResult Save(PatientHistory patientHistoryToSave);

        /// <summary>
        /// Deletes the patient history.
        /// </summary>
        /// <param name="patientHistoryToDelete">The patient history</param>
        /// <returns></returns>
        ProcessResult Delete(PatientHistory patientHistoryToDelete);
    }
}
