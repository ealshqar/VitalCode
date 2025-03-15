using Vital.Business.Shared.DomainObjects.Lookups;

namespace Vital.Business.Shared.Shared
{
    public class LookupEnumInfo<T>
    {
        /// <summary>
        /// Id of the lookup
        /// </summary>
        public int LookupId
        {
            get
            {
                return LookupBO.Id;
            }
        }

        /// <summary>
        /// Lookup Business Object
        /// </summary>
        public Lookup LookupBO
        {
            get;
            set;
        }

        /// <summary>
        /// Lookup As Enum
        /// </summary>
        public T LookupEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<T>(LookupBO);
            }
        }
    }
}
