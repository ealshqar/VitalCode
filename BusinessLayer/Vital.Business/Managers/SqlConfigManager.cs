using Vital.Business.Repositories.DatabaseRepositories.SqlConfig;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class SqlConfigManager
    {
        #region Private Variables

        private readonly ISqlConfigRepository _sqlConfigRepository;

        #endregion

        #region Constructor
        
        /// <summary>
        /// The Constructor.
        /// </summary>
        public SqlConfigManager()
        {
            _sqlConfigRepository = new SqlConfigDatabaseRepository();
        }        

        #endregion

        #region Methods

        /// <summary>
        /// Performs a sql DB task
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public void CreateDatabase(string filePath)
        {
            _sqlConfigRepository.CreateDatabase(filePath);
        }

        /// <summary>
        /// Runs an update script on DB
        /// </summary>
        /// <param name="updateScript"></param>
        public bool RunUpdateScript(string updateScript)
        {
            return _sqlConfigRepository.RunUpdateScript(updateScript);
        }

        /// <summary>
        /// Calls logic for the backup
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="branch"></param>
        /// <param name="vitalVersion"></param>
        /// <param name="dbVersion"></param>
        public ProcessResult Backup(string destination, string branch, string vitalVersion, string dbVersion)
        {
            return _sqlConfigRepository.Backup(destination, branch,vitalVersion,dbVersion);
        }

        /// <summary>
        /// Calls logic for the verification of a backup for restore
        /// </summary>
        /// <param name="source"></param>
        /// <param name="strDBInfo"></param>
        public DatabaseVerificationObject VerifyBackup(string source, ref string strDBInfo)
        {
            return _sqlConfigRepository.VerifyBackup(source, ref strDBInfo);
        }

        /// <summary>
        /// Calls logic for the restore
        /// </summary>
        /// <param name="source"></param>
        public void Restore(string source)
        {
            _sqlConfigRepository.Restore(source);
        }

        /// <summary>
        /// This function checks if the DB exists or not
        /// </summary>
        /// <returns>True if the DB exists</returns>
        public DBCheckState CheckDbExists(string dbName)
        {
            return _sqlConfigRepository.CheckDbExists(dbName);
        }
        
        #endregion
    }
}
