using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Users;


namespace Vital.Business.Shared.Shared
{
    public static class DomainEntityExtensions
    {
        #region Extensions

        /// <summary>
        /// Sets the user id and the correct needed dates.
        /// </summary>
        /// <param name="domainObject">The domain object.</param>
        public static void SetUserAndDates(this DomainEntity domainObject )
        {
            if(domainObject.Id == 0)
            {
                domainObject.CreationDateTime = DateTime.Now;
            }

            domainObject.UpdatedDateTime = DateTime.Now;
            domainObject.User = new User() { Id = 1 , Name = "Test" };
        }

        #endregion
    }
}
