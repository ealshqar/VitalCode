using Microsoft.Practices.EnterpriseLibrary.Caching;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.Caching
{
    public class CachingManager
    {
        #region Properties

        /// <summary>
        /// Cache manager property.
        /// </summary>
        public static ICacheManager CacheManager
        {
            get
            {
                return CacheFactory.GetCacheManager("VitalCacheManager");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Puts the passed data into the cache.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="cacheKey">The cache key.</param>
        public static void PutInCache(object data , string cacheKey)
        {
            CacheManager.Add(cacheKey, data);
        }

        /// <summary>
        /// Gets the data from the cache in the specified key.
        /// </summary>
        /// <param name="cacheKey">The cahce key.</param>
        /// <returns></returns>
        public static CacheResult GetFromCache(string cacheKey)
        {
            if (CacheManager.Contains(cacheKey))
            {
                return new CacheResult
                           {
                               Data = CacheManager.GetData(cacheKey),
                               IsSucceed = true
                           };
            }

            return new CacheResult
            {
                IsSucceed = false,
                Message = string.Format("Cache key '{0}' could not be found.", cacheKey)
            };
        }

        /// <summary>
        /// Removes all the items from cache
        /// </summary>
        public static void RemoveAllItemsFromCache()
        {
            RemoveFromCache(CachableDataEnum.AllProducts.ToString());
            RemoveFromCache(CachableDataEnum.AllSubstance.ToString());
            RemoveFromCache(CachableDataEnum.AllBacteria.ToString());
            RemoveFromCache(CachableDataEnum.AllPotency.ToString());
            RemoveFromCache(CachableDataEnum.AllItems.ToString());
        }

        /// <summary>
        /// Removes the cache entry that has the specified key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        public static CacheResult RemoveFromCache(string cacheKey)
        {
            if (CacheManager.Contains(cacheKey))
            {
                CacheManager.Remove(cacheKey);

                return new CacheResult
                {
                    IsSucceed = true
                };
            }

            return new CacheResult
            {
                IsSucceed = false,
                Message = string.Format("Cache key '{0}' could not be found.", cacheKey)
            };
        }

        /// <summary>
        /// Checks if the cache entry with the specified key is empty.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        public static CacheResult IsNotEmpty(string cacheKey)
        {

            if (CacheManager.Contains(cacheKey))
            {
                return new CacheResult
                               {
                                   IsSucceed = CacheManager.GetData(cacheKey) != null
                               };
            }

            return new CacheResult
            {
                IsSucceed = false,
                Message = string.Format("Cache key '{0}' could not be found.", cacheKey)
            };
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public static void ClearCache()
        {
            CacheManager.Flush();
        }

        /// <summary>
        /// Clears the cache entry with the specified key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        public static CacheResult ClearCache(string cacheKey)
        {
            if(CacheManager.Contains(cacheKey))
            {
                CacheManager.Remove(cacheKey);

                return new CacheResult
                {
                    IsSucceed = true
                };
            }

            return new CacheResult
            {
                IsSucceed = false,
                Message = string.Format("Cache key '{0}' could not be found.", cacheKey)
            };

        }

        #endregion
    }
}
