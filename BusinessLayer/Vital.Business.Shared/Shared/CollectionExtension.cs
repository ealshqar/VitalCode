using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Properties;
using Vital.Business.Shared.DomainObjects.Tests;

namespace Vital.Business.Shared.Shared
{
    public static class CollectionExtension
    {
        #region Extensions

        /// <summary>
        /// Apply action on all items in the collection based on condition or without condition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="act"></param>
        /// <returns></returns>
        public static IEnumerable<T> DoAction<T>(this IEnumerable<T> source,Action<T> act)
        {
            foreach (T element in source) act(element);
            return source;
        }

        /// <summary>
        /// Gets next item in a string list with circulation support
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T NextOf<T>(this IList<T> list, T item)
        {
            return list.IndexOf(item) == list.Count - 1? list[ 0]: list[list.IndexOf(item) + 1];
        }

        /// <summary>
        /// Takes the correct page depending on the page number and the page size.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns></returns>
        public static List<T> TakePage<T>(this List<T> list, int pageNumber, int pageSize)
        {
             var result=new List<T>();

             for (int i = (pageNumber - 1) * (pageSize); i < list.Count && i <= (pageSize * pageNumber) - 1; i++)
             {
                 result.Add(list[i]);
             }

            return result;
        }

        /// <summary>
        /// Gets the children of the items from the item relation.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static BindingList<Item> GetChildren(this BindingList<ItemRelation> list)
        {
            var childrenList = new BindingList<Item>();

            if(list != null)
            {
                foreach (var itemRelation in list)
                {
                    var item = itemRelation.Child;
                    item.Step = itemRelation.Step;

                    childrenList.Add(item);
                }
            }

            return childrenList;
        }

        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> list)
        {
            var bindingList = new BindingList<T>();

            foreach (var item in list)
            {
                bindingList.Add(item);
            }

            return bindingList;
        }

        /// <summary>
        /// DistinctBy Extension.
        /// </summary>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey> (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            //Returns a distinct depends on the pass key.
            // depends on the HashSet object and the Add method inside it (this Add method will return false if the key is had been added to the list before).
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }

        /// <summary>
        /// Sort the list.
        /// </summary>
        public static IOrderedQueryable<TRSource> SortBy<TRSource, TSource>(this IQueryable<TRSource> sources, Expression<Func<TSource, object>> sortKey, SortByTypesEnum sortByType)
        {
            var propertyName = ExpressionHelper.GetMemberName(sortKey);

            switch (sortByType)
            {
                case SortByTypesEnum.Ascending:
                    return sources.OrderBy(propertyName);

                default:
                    return sources.OrderByDescending(propertyName);
            }
        }

        /// <summary>
        /// Then sort list by.
        /// </summary>
        public static IOrderedQueryable<TRSource> ThenSortBy<TRSource, TSource>(this IOrderedQueryable<TRSource> sources, Expression<Func<TSource, object>> sortKey, SortByTypesEnum sortByType)
        {
            var propertyName = ExpressionHelper.GetMemberName(sortKey);

            return ThenSortBy(sources, propertyName, sortByType);
        }

        /// <summary>
        /// Then sort list by.
        /// </summary>
        public static IOrderedQueryable<TRSource> ThenSortBy<TRSource>(this IOrderedQueryable<TRSource> sources, string propertyName, SortByTypesEnum sortByType)
        {
            switch (sortByType)
            {
                case SortByTypesEnum.Ascending:
                    return sources.ThenBy(propertyName);

                default:
                    return sources.ThenByDescending(propertyName);
            }
        }

        /// <summary>
        /// Sort the list.
        /// </summary>
        public static IOrderedQueryable<TRSource> SortBy<TRSource, TSource>(this IQueryable<TRSource> sources, IList<SortKey<TSource>> sortKeys)
        {
            var sortedList = sources.SortBy(sortKeys[0].Key, sortKeys[0].Type);

            sortKeys.RemoveAt(0);

            sortedList = sortKeys.Aggregate(sortedList, (current, sortKey) => current.ThenSortBy(sortKey.Key, sortKey.Type));

            if (ExpressionHelper.HasProperty(typeof(TSource), "Id"))
                sortedList = sortedList.ThenSortBy("Id", SortByTypesEnum.Ascending);

            return sortedList;
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (var prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            var lambda = Expression.Lambda(delegateType, expr, arg);

            var result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }


        public static IEnumerable<IEnumerable<T>> SplitByCapacity<T>(this IEnumerable<T> sourceList, int capacity)
        {
            var result = new List<List<T>>();

            if (sourceList == null)
                return null;

            if (!sourceList.Any())
                return result;

            // Determine how many lists are required.
            var numberOfLists = (sourceList.Count() / capacity) + 1;

            for (int i = 0; i < numberOfLists; i++)
            {
                var newList = sourceList.Skip(i * capacity).Take(capacity).ToList();
                result.Add(newList);
            }

            return result;
        }

        public static IEnumerable<IEnumerable<T>> SplitByParts<T>(this IEnumerable<T> list, int parts)
        {
            var i = 0;
            var splits = from item in list
                         group item by i++ % parts into part
                         select part.AsEnumerable();
            return splits;
        }

        /// <summary>
        /// Create a single item list of T contains the source.
        /// </summary>
        public static List<T> ToSingleList<T>(this T source)
        {
            return new List<T> { source };
        }

        #endregion

        #region Properties Collection Extensions

        /// <summary>
        /// Check if the current collection contains a specific property.
        /// </summary>
        /// <param name="propertiesList"></param>
        /// <param name="propertiesEnum">The propertiesEnum.</param>
        /// <returns>Is passed property exists.</returns>
        public static bool HasProperty(this IEnumerable<Property> propertiesList, PropertiesEnum propertiesEnum)
        {
            return propertiesList.FirstOrDefault(p => p.Key.Equals(EnumNameResolver.Resolve(propertiesEnum))) != null;
        }

        /// <summary>
        /// Check if the current collection contains a specific property.
        /// </summary>
        /// <param name="propertiesList"></param>
        /// <param name="propertiesEnum">The propertiesEnum.</param>
        /// <param name="checkForNullValue">If the value of properly == null then consider it as not exists.</param>
        /// <returns>Is passed property exists.</returns>
        public static bool HasProperty(this IEnumerable<DomainEntityPropertyRelational> propertiesList, PropertiesEnum propertiesEnum, bool checkForNullValue = false)
        {
            var propertyRelational = propertiesList.FirstOrDefault(ip => ip.Property.Key.Equals(EnumNameResolver.Resolve(propertiesEnum)));

            return propertyRelational != null && (!checkForNullValue || propertyRelational.Value != null);
        }

        /// <summary>
        /// Check if the current collection contains a specific property with the specific value.
        /// </summary>
        /// <param name="propertiesList"></param>
        /// <param name="propertiesEnum">The propertiesEnum.</param>
        /// <param name="value">The value.</param>
        /// <returns>Is passed property exists.</returns>
        public static bool HasProperty(this IEnumerable<DomainEntityPropertyRelational> propertiesList, PropertiesEnum propertiesEnum, string value)
        {
            var enumString = EnumNameResolver.Resolve(propertiesEnum);
            return propertiesList.FirstOrDefault(ip => ip.Property.Key == enumString && ip.Value.ToString()== value) != null;
        }

        /// <summary>
        /// Returns a first property match the condition in the current collection.
        /// </summary>
        /// <param name="propertiesList"></param>
        /// <param name="propertiesEnum">The propertiesEnum.</param>
        public static DomainEntityPropertyRelational FirstProperty(this IEnumerable<DomainEntityPropertyRelational> propertiesList, PropertiesEnum propertiesEnum)
        {
            return propertiesList.FirstOrDefault(ip => ip.Property.Key.Equals(EnumNameResolver.Resolve(propertiesEnum)));
        }

        /// <summary>
        /// Returns the value for a property of a specific type if that property exists
        /// </summary>
        /// <param name="propertiesList"></param>
        /// <param name="propertiesEnum"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this IEnumerable<DomainEntityPropertyRelational> propertiesList, PropertiesEnum propertiesEnum)
        {
            var property = propertiesList.FirstOrDefault(ip => ip.Property.Key.Equals(EnumNameResolver.Resolve(propertiesEnum)));
            return property == null ? null : property.Value;
        }

        /// <summary>
        /// Returns the value for a property of a specific type if that property exists as string.
        /// </summary>
        /// <param name="propertiesList"></param>
        /// <param name="propertiesEnum"></param>
        /// <returns></returns>
        public static string GetPropertyValueAsString(this IEnumerable<DomainEntityPropertyRelational> propertiesList, PropertiesEnum propertiesEnum)
        {
            var property = propertiesList.FirstOrDefault(ip => ip.Property.Key.Equals(EnumNameResolver.Resolve(propertiesEnum)));
            return property == null ? null : property.Value == null ? null : property.Value.ToString();
        }

        #endregion

        #region TestResults Collection Extensions

        /// <summary>
        /// Gets the testResults that have not empty four factors.
        /// </summary>
        /// <param name="testResults">The test results.</param>
        /// <returns></returns>
        public static IEnumerable<TestResult> GetResultsHaveFourFactors(this IEnumerable<TestResult> testResults)
        {
            return testResults == null ? null : testResults.Where(r => r.TestResultFactors != null && r.TestResultFactors.Count > 0  && r.TestResultFactors.Any(f => f.Reading > 0));
        }

        #endregion
    }
}
