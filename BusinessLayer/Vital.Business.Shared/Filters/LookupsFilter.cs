using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.Filters
{
    public class LookupsFilter : BaseFilter<Lookup>
    {

        /// <summary>
        /// Get or set the Name.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Get or set the Key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Get or set the Value.
        /// </summary>
        public string Value { get; set; }

        #region Helpers

        /// <summary>
        /// Gets new object of the LookupsFilter that filled by the passed enums.
        /// </summary>
        /// <typeparam name="T">The Type enum type.</typeparam>
        /// <typeparam name="TV">The Value enum type.</typeparam>
        /// <param name="type">The Type.</param>
        /// <param name="value">The Value</param>
        /// <param name="isMultiWordType">Is the Type contains multi words and need to split.</param>
        /// <param name="isMultiWordValue">Is the Value contains multi words and need to split.</param>
        /// <returns></returns>
        public static LookupsFilter As<T, TV>(T type, TV value, bool isMultiWordType = false, bool isMultiWordValue = false)
        {
            return new LookupsFilter
            {
                Type = EnumNameResolver.Resolve(type, isMultiWordType),
                Value = EnumNameResolver.Resolve(value, isMultiWordValue)
            };

        }

        /// <summary>
        /// Gets new object of the LookupsFilter that filled by the passed enums.
        /// </summary>
        /// <typeparam name="T">The Type enum type.</typeparam>
        /// <returns></returns>
        public static LookupsFilter AsKey(string key)
        {
            return new LookupsFilter
            {
                Key = key
            };
        }

        /// <summary>
        /// Gets new object of the LookupsFilter that filled by the passed enum as member Type.
        /// </summary>
        /// <typeparam name="T">The Type enum type.</typeparam>
        /// <param name="type">The Type.</param>
        /// <param name="isMultiWordType">Is the Type contains multi words and need to split.</param>
        public static LookupsFilter As<T>(T type, bool isMultiWordType = false)
        {
            return new LookupsFilter
            {
                Type = EnumNameResolver.Resolve(type, isMultiWordType)
            };
        }


        #endregion
        
    }
}
