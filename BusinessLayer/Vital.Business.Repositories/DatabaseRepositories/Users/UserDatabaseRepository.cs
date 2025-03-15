using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.Users;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.Users
{
    public class UserDatabaseRepository : BaseRepository,IUserRepository
    {

        #region Private Variables



        #endregion

        #region Public Methods

        /// <summary>
        /// Loads user by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The user</returns>
        public User LoadUserById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.User.Where(c => c.Id == id);

                    var user = src.FirstOrDefault();

                    var userObj = new User();

                    Mapper.Map(user, userObj);

                    return userObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of users.
        /// </summary>
        /// <returns>List of users.</returns>
        public BindingList<User> LoadUsers()
        {
            try
            {
                return LoadUsersWorker();
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }


        /// <summary>
        /// Saves a user.
        /// </summary>
        /// <param name="userToSave">The user.</param>
        /// <returns>The result.</returns>
        public ProcessResult Save(User userToSave)
        {
            Check.Argument.IsNotNull(userToSave, "user to save");

            try
            {
                var userEntity = Mapper.Map<User, UserEntity>(userToSave);

                userEntity.IsNew = userEntity.Id > 0 ? false : true;

                var processResult = CommonRepository.Save(userEntity);

                userToSave.Id = userEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userToDelete">The user.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(User userToDelete)
        {
            Check.Argument.IsNotNull(userToDelete, "user to delete");

            try
            {
                var userEntity = Mapper.Map<User, UserEntity>(userToDelete);

                return CommonRepository.Delete(userEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads a list of users.
        /// </summary>
        /// <returns></returns>
        private static BindingList<User> LoadUsersWorker()
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.User;

                var users = src.ToList();

                var userObjList = new BindingList<User>();

                Mapper.Map(users, userObjList);

                return userObjList;
            }
        }

        #endregion
    }
}
