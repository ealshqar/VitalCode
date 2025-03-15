using System.Collections.Generic;
using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.Users;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.Users
{
    public interface IUserRepository
    {
        /// <summary>
        /// Loads user by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The user.</returns>
        User LoadUserById(int id);

        /// <summary>
        /// Loads all users.
        /// </summary>
        /// <returns>The list of users.</returns>
        BindingList<User> LoadUsers();

        /// <summary>
        /// Saves the user.
        /// </summary>
        /// <param name="userToSave">The user to save.</param>
        /// <returns>The result.</returns>
        ProcessResult Save(User userToSave);

        /// <summary>
        /// Deletes the user. 
        /// </summary>
        /// <param name="userToDelete">The user to delete.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(User userToDelete);


    }
}
