using System;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.SqlConfig
{
    public interface ISqlConfigRepository
    {
        /// <summary>
        /// Performs a sql DB task
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        void CreateDatabase(string filePath);

        /// <summary>
        /// Runs an update script on DB
        /// </summary>
        /// <param name="updateScript"></param>
        bool RunUpdateScript(string updateScript);

        /// <summary>
        /// Calls logic for the backup
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="branch"></param>
        /// <param name="vitalVersion"></param>
        /// <param name="dbVersion"></param>
        ProcessResult Backup(string destination, string branch, string vitalVersion, string dbVersion);

        /// <summary>
        /// Calls logic for the verification of a backup for restore
        /// </summary>
        /// <param name="source"></param>
        /// <param name="strDBInfo"></param>
        DatabaseVerificationObject VerifyBackup(string source, ref string strDBInfo);

        /// <summary>
        /// Calls logic for the restore
        /// </summary>
        /// <param name="source"></param>
        void Restore(string source);

        /// <summary>
        /// This function checks if the DB exists or not
        /// </summary>
        /// <returns>True if the DB exists</returns>
        DBCheckState CheckDbExists(string dbName);
    }
}
