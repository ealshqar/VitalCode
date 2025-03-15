using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.FactoryClasses;
using Vital.DataLayer.HelperClasses;
using SD.LLBLGen.Pro.LinqSupportClasses;
using SD.LLBLGen.Pro.LinqSupportClasses.PrefetchPathAPI;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using Expression = System.Linq.Expressions.Expression;

namespace Vital.Business.Repositories.Shared
{
    public static class RepositoryExtensions
    {
        #region General

        /// <summary>
        /// Gets lookup Id from a LookupEnumInfo list
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <param name="target"></param>
        /// <param name="lookupEnum"></param>
        /// <returns></returns>
        public static int GetLookupId<TEntityType>(this List<LookupEnumInfo<TEntityType>> target, TEntityType lookupEnum)
        {
            return target.FirstOrDefault(s => s.LookupEnum.Equals(lookupEnum)).LookupId;
        }

        /// <summary>
        /// Combine two expressions with OrElse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> OrElse<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            // need to detect whether they use the same
            // parameter instance; if not, they need fixing

            var param = expr1.Parameters[0];

            if (ReferenceEquals(param, expr2.Parameters[0]))
            {
                // simple version
                return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, expr2.Body), param);
            }

            // otherwise, keep expr1 "as is" and invoke expr2
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, Expression.Invoke(expr2, param)), param);
        }

        /// <summary>
        /// Get property name directly from type without creating an instance
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string GetPropertyName<TClass>(Expression<Func<TClass, object>> exp)
        {
            var body = exp.Body as MemberExpression;

            if (body == null)
            {
                var ubody = (UnaryExpression)exp.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }

        /// <summary>
        /// Analyzes a property sent and if it is a nested property then it will deconstruct it to make sure it is fully used as nested property and not just the last part
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <param name="destinationProperty"></param>
        /// <param name="expressionParameter"></param>
        /// <returns></returns>
        public static MemberExpression GetDeconstructedExpression<TEntityType>(Expression<Func<TEntityType, object>> destinationProperty,
                                                                               ParameterExpression expressionParameter)
        {
            //Generate a string from the property path sent
            var deconstructedProperties = GetPropertyPath(destinationProperty);

            MemberExpression destinationMember = null;// "(x => x.DestinationProperty ..."

            if (deconstructedProperties.Contains(".")) //x=>x.Owner.FirstName
            {
                var firstSet = true;

                foreach (var property in deconstructedProperties.Split('.').ToList())// We use loop as there might be multiple nested properties like x=>x.Prop1.Prop2.Prop3...
                {
                    if (firstSet)
                    {
                        destinationMember = Expression.Property(expressionParameter, property);
                        firstSet = false;
                    }
                    else
                    {
                        destinationMember = Expression.Property(destinationMember, property);
                    }

                }
            }
            else
            {
                destinationMember = Expression.Property(expressionParameter, deconstructedProperties);// x=>x.Name
            }

            return destinationMember;
        }

        #region Logic to Deconstruct a nested property path

        /// <summary>
        /// GetMemberExpression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MemberExpression GetMemberExpression(Expression expression)
        {
            if (expression is MemberExpression)
            {
                return (MemberExpression)expression;
            }
            else if (expression is LambdaExpression)
            {
                var lambdaExpression = expression as LambdaExpression;
                if (lambdaExpression.Body is MemberExpression)
                {
                    return (MemberExpression)lambdaExpression.Body;
                }
                else if (lambdaExpression.Body is UnaryExpression)
                {
                    return ((MemberExpression)((UnaryExpression)lambdaExpression.Body).Operand);
                }
            }
            return null;
        }

        /// <summary>
        /// GetPropertyPath
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetPropertyPath(Expression expr)
        {
            var path = new StringBuilder();
            MemberExpression memberExpression = GetMemberExpression(expr);
            do
            {
                if (path.Length > 0)
                {
                    path.Insert(0, ".");
                }
                path.Insert(0, memberExpression.Member.Name);
                memberExpression = GetMemberExpression(memberExpression.Expression);
            }
            while (memberExpression != null);
            return path.ToString();
        }

        /// <summary>
        /// GetPropertyPath
        /// </summary>
        /// <typeparam name="TObj"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="obj"></param>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetPropertyPath<TObj, TRet>(this TObj obj, Expression<Func<TObj, TRet>> expr)
        {
            return GetPropertyPath(expr);
        }

        #endregion

        #endregion

        #region SRC Extensions

        #region Filtration On SRC

        /// <summary>
        /// Filter on SRC
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <typeparam name="TFilterPropertyType"></typeparam>
        /// <param name="target"></param>
        /// <param name="filterPropertyValue"></param>
        /// <param name="destinationProperty"></param>
        /// <param name="filterType"></param>
        /// <returns></returns>
        public static IQueryable<TEntityType> ApplyFilter<TEntityType, TFilterPropertyType>(this IQueryable<TEntityType> target,
                                                                                            TFilterPropertyType filterPropertyValue,
                                                                                            Expression<Func<TEntityType, object>> destinationProperty,
                                                                                            FilterType filterType)
        {
            target = ApplyFilter(filterPropertyValue, destinationProperty, target, filterType);

            return target;
        }

        /// <summary>
        /// Apply filter to LLBLGen when loading data in worker
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <typeparam name="TFilterPropertyType"></typeparam>
        /// <param name="filterPropertyValue"></param>
        /// <param name="destinationProperty"></param>
        /// <param name="src"></param>
        /// <param name="filterType"></param>
        /// <returns></returns>
        public static IQueryable<TEntityType> ApplyFilter<TEntityType, TFilterPropertyType>(TFilterPropertyType filterPropertyValue,
                                                                                            Expression<Func<TEntityType, object>> destinationProperty,
                                                                                            IQueryable<TEntityType> src,
                                                                                            FilterType filterType)
        {
            if (filterPropertyValue == null) return src;

            var performFilter = true;

            var filterPropertyValueString = filterPropertyValue.ToString();

            if (filterPropertyValue is int)
            {
                performFilter = int.Parse(filterPropertyValueString) > 0;
            }
            else if (filterPropertyValue is decimal)
            {
                performFilter = decimal.Parse(filterPropertyValueString) > 0;
            }
            else if (filterPropertyValue is long)
            {
                performFilter = long.Parse(filterPropertyValueString) > 0;
            }
            else if (filterPropertyValue is string)
            {
                performFilter = !string.IsNullOrEmpty(filterPropertyValueString);
            }
            else if (filterPropertyValue is DateTime?)
            {
                var value = string.IsNullOrEmpty(filterPropertyValueString) ? (DateTime?)null : DateTime.Parse(filterPropertyValueString);
                performFilter = value.HasValue;
            }
            else if (typeof(TFilterPropertyType) == typeof(bool?))
            {
                //It is nullable
                var value = string.IsNullOrEmpty(filterPropertyValueString) ? (bool?)null : bool.Parse(filterPropertyValueString);
                performFilter = value.HasValue;
            }
            else if (typeof(TFilterPropertyType) == typeof(bool))
            {
                //It is not nullable 

                //If the filter property passed is not bool? then the check shouldn't be performed because the filtration will be applied even if not value was set in the bool
                //property and the filter will be applied with the value as "False"
                //If this is debug mode then return an error so developer would know about it
                if (Debugger.IsAttached)
                {
                    throw new Exception("Vital Error: A filter property of type bool must be nullable to avoid logical issues in filtration - Change the passed filter property type from bool to bool?.");
                }

                //If this is release mode then just ignore the property and don't show an error to the users, this might cause logical errors if happened
                //but it is better than showing error to user.
                return src;
            }

            if (performFilter)
            {
                //Generic parameter to use in expression
                var expressionParameter = Expression.Parameter(typeof(TEntityType), "x");                 // "(x => x. ..."
                var destinationMember = GetDeconstructedExpression(destinationProperty, expressionParameter);// "(x => x.DestinationProperty ..."

                var filterValue = Expression.Constant(filterPropertyValue);//Get the filter property value as constant for use in expressions

                Expression<Func<TEntityType, bool>> filterExpression = null;//Define the final expression to be used in Where caluse

                if (filterType == FilterType.StringEqualOrContains ||
                    filterType == FilterType.StringContains ||
                    filterType == FilterType.StringEqual)
                {
                    filterValue = Expression.Constant(filterPropertyValue.ToString().ToLower());

                    var toLowerExpressionBody = Expression.Call(destinationMember, typeof(string).GetMethod("ToLower", new Type[] { }));
                    var containsConditionBody = Expression.Call(toLowerExpressionBody, typeof(string).GetMethod("Contains"), filterValue);
                    var equalityExpressionBody = Expression.Equal(toLowerExpressionBody, filterValue);

                    //Contains filter case requires special handling
                    if (filterType == FilterType.StringEqualOrContains)
                    {
                        filterExpression = Expression.Lambda<Func<TEntityType, bool>>(containsConditionBody, expressionParameter);
                        var equalityExpression = Expression.Lambda<Func<TEntityType, bool>>(equalityExpressionBody, expressionParameter);

                        filterExpression = OrElse(filterExpression, equalityExpression);
                    }
                    else if (filterType == FilterType.StringContains)
                    {
                        filterExpression = Expression.Lambda<Func<TEntityType, bool>>(containsConditionBody, expressionParameter);
                    }
                    else if (filterType == FilterType.StringEqual)
                    {
                        filterExpression = Expression.Lambda<Func<TEntityType, bool>>(equalityExpressionBody, expressionParameter);
                    }
                }
                else
                {
                    //Get the expression in non nullable format since the Expression.Method doesn't allow nullable types.
                    var nonNullableType = Nullable.GetUnderlyingType(typeof(TFilterPropertyType));
                    var nonNullableExpression = Expression.Convert(destinationMember, nonNullableType ?? typeof(TFilterPropertyType));

                    BinaryExpression expressionBody = null;

                    //Select the expression operation based on type
                    switch (filterType)
                    {
                        case FilterType.Equal:
                            expressionBody = Expression.Equal(nonNullableExpression, filterValue);
                            break;
                        case FilterType.NotEqual:
                            expressionBody = Expression.NotEqual(nonNullableExpression, filterValue);
                            break;
                    }

                    //Set the filter expression value
                    filterExpression = Expression.Lambda<Func<TEntityType, bool>>(expressionBody, expressionParameter);
                }

                //Apply the filter
                src = src.Where(filterExpression);
            }

            return src;
        }

        #endregion

        #region Applying SearchKey On SRC

        /// <summary>
        /// Apply Search Key on SRC
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <param name="target"></param>
        /// <param name="searchKey"></param>
        /// <param name="destinationProperties"></param>
        /// <returns></returns>
        public static IQueryable<TEntityType> ApplySearchKey<TEntityType>(this IQueryable<TEntityType> target,
                                                                        string searchKey,
                                                                        params Expression<Func<TEntityType, object>>[] destinationProperties)
        {
            target = ApplySearchKey(searchKey, target, destinationProperties);

            return target;
        }

        /// <summary>
        /// Apply search key filter to LLBLGen when loading data in worker
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <param name="searchKey"></param>
        /// <param name="src"></param>
        /// <param name="destinationProperties"></param>
        /// <returns></returns>
        public static IQueryable<TEntityType> ApplySearchKey<TEntityType>(string searchKey,
                                                                          IQueryable<TEntityType> src,
                                                                          params Expression<Func<TEntityType, object>>[] destinationProperties)
        {
            var performFilter = !string.IsNullOrEmpty(searchKey);

            if (performFilter)
            {
                var filterValue = Expression.Constant(searchKey);//Get the filter property value as constant for use in expressions

                Expression<Func<TEntityType, bool>> finalFilterExpression = type => type != null;//Define the final expression to be used in Where caluse

                //Generic parameter to use in expression
                var expressionParameter = Expression.Parameter(typeof(TEntityType), "x");// "(x => x. ..."
                var isFirstExpression = true;

                foreach (var destinationProperty in destinationProperties)
                {
                    if (destinationProperty != null)
                    {
                        var destinationMember = GetDeconstructedExpression(destinationProperty, expressionParameter);// "(x => x.DestinationProperty ..."

                        var containsCondition = Expression.Call(destinationMember, typeof(string).GetMethod("Contains"), filterValue);
                        var currentfilterExpression = Expression.Lambda<Func<TEntityType, bool>>(containsCondition, expressionParameter);

                        if (isFirstExpression)
                        {
                            isFirstExpression = false;

                            finalFilterExpression = currentfilterExpression;
                        }
                        else
                        {
                            finalFilterExpression = OrElse(finalFilterExpression, currentfilterExpression);
                        }
                    }
                }

                //Apply the filter
                src = src.Where(finalFilterExpression);
            }

            return src;
        }
        #endregion

        #region Mapping To Page List For SRC

        /// <summary>
        /// Get mapped list from an src with filtration & sorting
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <typeparam name="TBOType"></typeparam>
        /// <param name="target"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static BindingList<TBOType> GetMappedPageList<TEntityType, TBOType>(this IQueryable<TEntityType> target, BaseFilter<TBOType> filter) where TBOType : DomainEntity
        {
            return GetMappedPageList(filter, target);
        }

        /// <summary>
        /// Returns LLBLGen source as mapped list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        public static BindingList<TBOType> GetMappedPageList<TEntityType, TBOType>(BaseFilter<TBOType> filter, IQueryable<TEntityType> src) where TBOType : DomainEntity
        {
            if (filter.SortByKeys != null && filter.SortByKeys.Count > 0)
                src = src.SortBy(filter.SortByKeys);

            if (filter.PageNumber > 0 && filter.PageSize > 0)
                src = src.TakePage(filter.PageNumber, filter.PageSize);

            var domainEntities = new BindingList<TBOType>();

            if (filter.PageNumber != -1) Mapper.Map(src.ToList(), domainEntities);//This check is used to prevent loading list if only we need to check if there are any records to show for UI purposes

            return new BindingList<TBOType>(domainEntities);
        }

        #endregion

        #endregion

        #region Filter Bucket Extensions

        #region Filtration on Filter Bucket

        /// <summary>
        /// Add filter conditions to a RelationPredicateBucket
        /// </summary>
        /// <typeparam name="TFilterPropertyType"></typeparam>
        /// <param name="target"></param>
        /// <param name="filterPropertyValue"></param>
        /// <param name="destinationProperty"></param>
        /// <param name="adapter"></param>
        /// <param name="filterType"></param>
        /// <returns></returns>
        public static RelationPredicateBucket ApplyFilter<TFilterPropertyType>(this RelationPredicateBucket target,
                                                                                TFilterPropertyType filterPropertyValue,
                                                                                EntityField2 destinationProperty,
                                                                                DataAccessAdapter adapter,
                                                                                FilterType filterType)
        {
            target = ApplyFilter(filterPropertyValue, destinationProperty, target, adapter, filterType);

            return target;
        }

        /// <summary>
        /// Add filter conditions to a RelationPredicateBucket
        /// </summary>
        /// <typeparam name="TFilterPropertyType"></typeparam>
        /// <param name="filterPropertyValue"></param>
        /// <param name="destinationProperty"></param>
        /// <param name="filter"></param>
        /// <param name="adapter"></param>
        /// <param name="filterType"></param>
        /// <returns></returns>
        public static RelationPredicateBucket ApplyFilter<TFilterPropertyType>(TFilterPropertyType filterPropertyValue,
                                                            EntityField2 destinationProperty,
                                                            RelationPredicateBucket filter,
                                                            DataAccessAdapter adapter,
                                                            FilterType filterType)
        {
            if (filterPropertyValue == null) return filter;

            var performFilter = true;

            var filterPropertyValueString = filterPropertyValue.ToString();

            if (filterPropertyValue is int)
            {
                performFilter = int.Parse(filterPropertyValueString) > 0;
            }
            else if (filterPropertyValue is decimal)
            {
                performFilter = decimal.Parse(filterPropertyValueString) > 0;
            }
            else if (filterPropertyValue is long)
            {
                performFilter = long.Parse(filterPropertyValueString) > 0;
            }
            else if (filterPropertyValue is string)
            {
                performFilter = !string.IsNullOrEmpty(filterPropertyValueString);
            }
            else if (filterPropertyValue is DateTime?)
            {
                var value = string.IsNullOrEmpty(filterPropertyValueString) ? (DateTime?)null : DateTime.Parse(filterPropertyValueString);
                performFilter = value.HasValue;
            }
            else if (typeof(TFilterPropertyType) == typeof(bool?))
            {
                //It is nullable
                var value = string.IsNullOrEmpty(filterPropertyValueString) ? (bool?)null : bool.Parse(filterPropertyValueString);
                performFilter = value.HasValue;
            }
            else if (typeof(TFilterPropertyType) == typeof(bool))
            {
                //It is not nullable 

                //If the filter property passed is not bool? then the check shouldn't be performed because the filtration will be applied even if not value was set in the bool
                //property and the filter will be applied with the value as "False"
                //If this is debug mode then return an error so developer would know about it
                if (Debugger.IsAttached)
                {
                    throw new Exception("Vital Error: A filter property of type bool must be nullable to avoid logical issues in filtration - Change the passed filter property type from bool to bool?.");
                }

                //If this is release mode then just ignore the property and don't show an error to the users, this might cause logical errors if happened
                //but it is better than showing error to user.
                return filter;
            }

            if (performFilter)
            {
                if (filterType == FilterType.StringEqualOrContains ||
                    filterType == FilterType.StringContains ||
                    filterType == FilterType.StringEqual)
                {
                    var stringValue = filterPropertyValue.ToString();
                    var stringValueUpper = stringValue.ToUpper();//We use upper value to allow for more accurate search by using upper case on both sides of equality check function

                    var fieldInfo = adapter.GetEntityFieldPersistenceInfo(destinationProperty);

                    var equalPredicate = new FieldCompareValuePredicate(destinationProperty, fieldInfo, ComparisonOperator.Equal, stringValueUpper)
                    {
                        //This causes the query to use UPPER(ColumnName) which allows us to use case sensitive search which is important to make sure we get result regardless of the
                        //string entered by the user, this is needed for the Equal case only becuase the "Like" function doesnt' care about letter casing.
                        CaseSensitiveCollation = true
                    };

                    var likePredicate = new FieldLikePredicate(destinationProperty, fieldInfo, GetLikeFormatForString(stringValue));//Like uses normal string because letters casing doesn't affect it

                    if (filterType == FilterType.StringEqualOrContains)
                    {
                        //Here we use .Or inside of .Add( .... Or .... ) to make sure the generated query has the condition inside paranthesis to prevent failures or incorrect results
                        //Because of we used filter.PredicateExpression.AddWithOr then this would add an Or condition on the whole where statement and this is wrong.
                        filter.PredicateExpression.Add(equalPredicate.Or(likePredicate));
                    }
                    else if (filterType == FilterType.StringContains)
                    {

                        filter.PredicateExpression.Add(likePredicate);
                    }
                    else if (filterType == FilterType.StringEqual)
                    {
                        filter.PredicateExpression.Add(equalPredicate);
                    }
                }
                else
                {
                    //Select the logic operation based on type
                    switch (filterType)
                    {
                        case FilterType.Equal:
                            filter.PredicateExpression.Add(destinationProperty == filterPropertyValue);
                            break;
                        case FilterType.NotEqual:
                            filter.PredicateExpression.Add(destinationProperty != filterPropertyValue);
                            break;
                    }
                }
            }

            return filter;
        }
        #endregion

        #region Applying SearchKey On Filter Bucket

        /// <summary>
        /// Apply Search Key on SRC
        /// </summary>
        /// <param name="target"></param>
        /// <param name="searchKey"></param>
        /// <param name="adapter"></param>
        /// <param name="destinationProperties"></param>
        /// <returns></returns>
        public static RelationPredicateBucket ApplySearchKey(this RelationPredicateBucket target,
                                                            string searchKey,
                                                            DataAccessAdapter adapter,
                                                            params EntityField2[] destinationProperties)
        {
            target = ApplySearchKey(searchKey, target, adapter, destinationProperties);

            return target;
        }

        /// <summary>
        /// Apply search key filter to LLBLGen when loading data in worker
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="filter"></param>
        /// <param name="adapter"></param>
        /// <param name="destinationProperties"></param>
        /// <returns></returns>
        public static RelationPredicateBucket ApplySearchKey(string searchKey,
                                                            RelationPredicateBucket filter,
                                                            DataAccessAdapter adapter,
                                                            params EntityField2[] destinationProperties)
        {
            var performFilter = !string.IsNullOrEmpty(searchKey);

            if (performFilter)
            {
                var isFirstExpression = true;

                //Notice here that we create a filter expression to merge all conditions with or statements and then we add them all inside one AND condition
                //with the main filter.
                var finalFilter = new PredicateExpression();

                foreach (var destinationProperty in destinationProperties)
                {
                    var currentFilter = destinationProperty.Like(GetLikeFormatForString(searchKey));

                    if (isFirstExpression)
                    {
                        isFirstExpression = false;

                        finalFilter.Add(currentFilter);
                    }
                    else
                    {
                        //Here we use .Or inside of .Add( .... Or .... ) to make sure the generated query has the condition inside paranthesis to prevent failures or incorrect results
                        //Because of we used filter.PredicateExpression.AddWithOr then this would add an Or condition on the whole where statement and this is wrong.
                        finalFilter.Or(currentFilter);
                    }
                }

                filter.PredicateExpression.Add(finalFilter);
            }

            return filter;
        }
        #endregion

        #region Mapping To Page List For Filter Bucket

        /// <summary>
        /// Fetches an entity collection and return it as mapped list
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <typeparam name="TBOType"></typeparam>
        /// <param name="filter"></param>
        /// <param name="adapter"></param>
        /// <param name="prefetchPath2"></param>
        /// <param name="target"></param>
        /// <param name="filterBucket"></param>
        /// <returns></returns>
        public static BindingList<TBOType> GetMappedPageList<TEntityType, TBOType>(this EntityCollection<TEntityType> target,
                                                                                BaseFilter<TBOType> filter,
                                                                                DataAccessAdapter adapter,
                                                                                PrefetchPath2 prefetchPath2,
                                                                                RelationPredicateBucket filterBucket)
            where TBOType : DomainEntity
            where TEntityType : EntityBase2
        {
            return GetMappedPageList(filter, adapter, prefetchPath2, target, filterBucket);
        }

        /// <summary>
        /// Fetches an entity collection and return it as mapped list
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <typeparam name="TBOType"></typeparam>
        /// <param name="filter"></param>
        /// <param name="adapter"></param>
        /// <param name="prefetchPath2"></param>
        /// <param name="entityCollection"></param>
        /// <param name="filterBucket"></param>
        /// <returns></returns>
        public static BindingList<TBOType> GetMappedPageList<TEntityType, TBOType>(BaseFilter<TBOType> filter,
                                                                                DataAccessAdapter adapter,
                                                                                PrefetchPath2 prefetchPath2,
                                                                                EntityCollection<TEntityType> entityCollection,
                                                                                RelationPredicateBucket filterBucket)
            where TBOType : DomainEntity
            where TEntityType : EntityBase2
        {
            var domainEntities = new BindingList<TBOType>();

            //Check if we need to prefetch the data or if we just want to count.
            if (filter.PageNumber != -1)
            {
                adapter.FetchEntityCollection(entityCollection, filterBucket, filter.PageSize, null, prefetchPath2, filter.PageNumber, filter.PageSize);

                var list = entityCollection.ToList().AsQueryable();

                if (filter.SortByKeys != null && filter.SortByKeys.Count > 0)
                {
                    list = list.SortBy(filter.SortByKeys);
                }

                Mapper.Map(list, domainEntities);

            }

            //Always get the count using method adapter.GetDbCount() because using list.Count() will return only the count for current page.
            //but GetDbCount() will get the count of the all the list items as if it was all loaded at once.
            var recordsCount = adapter.GetDbCount(entityCollection, filterBucket);

            return new BindingList<TBOType>(domainEntities) ;
        }

        #endregion

        #region Filter Bucket & FetchEntityCollection Helper Methods

        /// <summary>
        /// Gets a string with a like format for SQL Like operation
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetLikeFormatForString(string value)
        {
            return string.Format(StaticKeys.LikeFormatForString, value);
        }

        /// <summary>
        /// Convert a path edge of type "Func<IPathEdgeRootParser<TEntityType>, IPathEdgeRootParser<TEntityType>>"  to a PrefetchPath of type "PrefetchPath2"
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <param name="pathEdge"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static PrefetchPath2 GetPathEdgeAsPrefetchPath2<TEntityType>(Func<IPathEdgeRootParser<TEntityType>, IPathEdgeRootParser<TEntityType>> pathEdge, DataLayer.EntityType entityType) where TEntityType : IEntityCore
        {
            //This logic allows converting a pathedge of type "Func<IPathEdgeRootParser<TEntityType>, IPathEdgeRootParser<TEntityType>>" to a PrefetchPath of type "PrefetchPath2"
            //This allows us to fetch data from database using "adapter.FetchEntityCollection" instead of data.Entity.WithPath when needed.

            var pathEdgeRootParser = new PathEdgeRootParser<TEntityType>(new ElementCreator());
            var invoke = pathEdge.Invoke(pathEdgeRootParser);
            var rootEdges = ((PathEdgeRootParser<TEntityType>)invoke).RootEdges;
            var prefetchPath2 = new PrefetchPath2(entityType);

            //Loop on all main root edges, check their childs and then add them to the new prefetch path
            foreach (var rootEdge in rootEdges)
            {
                var prefetchPathElement = (PrefetchPathElement2)rootEdge.PathElement;

                //-------------------------------------------------------------------------------------------------------------------------------------------------
                //IMPORTANT:
                //Call this method before the line below "prefetchPath2.Add(prefetchPathElement);" to make sure when adding the prefetchPathElement that it contains
                //the details of all of its childs and their childs before addition.
                AddRootEdgeChilds(prefetchPathElement, rootEdge.ChildEdges.ToList());
                //-------------------------------------------------------------------------------------------------------------------------------------------------

                //Add the current prefetchPathElement
                prefetchPath2.Add(prefetchPathElement);
            }

            return prefetchPath2;
        }

        /// <summary>
        /// Check for all child edges and add them to the parent prefetch path element then call logic again for their childs
        /// </summary>
        /// <param name="parentPrefetchPathElement"></param>
        /// <param name="childEdges"></param>
        private static void AddRootEdgeChilds(PrefetchPathElement2 parentPrefetchPathElement, List<IPathEdge> childEdges)
        {
            //Use recursion to loop over all pass child edges, and call the same logic for their childs if any and then add them to their parent parentPrefetchPathElement
            foreach (var childEdge in childEdges)
            {
                var prefetchPathElement = (PrefetchPathElement2)childEdge.PathElement;

                AddRootEdgeChilds(prefetchPathElement, childEdge.ChildEdges.ToList());

                parentPrefetchPathElement.SubPath.Add(prefetchPathElement);
            }
        }

        /// <summary>
        /// Generates a predicate of type FieldCompareRangePredicate
        /// </summary>
        /// <param name="destinationProperty"></param>
        /// <param name="target"></param>
        /// <param name="negate"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static FieldCompareRangePredicate GetFieldRangeFilter(this DataAccessAdapter target,
                                                                     EntityField2 destinationProperty,
                                                                     bool negate,
                                                                     List<int> values)
        {
            return GetFieldRangeFilter(destinationProperty, target, negate, values);
        }

        /// <summary>
        /// Generates a predicate of type FieldCompareRangePredicate
        /// </summary>
        /// <param name="destinationProperty"></param>
        /// <param name="adapter"></param>
        /// <param name="negate"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static FieldCompareRangePredicate GetFieldRangeFilter(EntityField2 destinationProperty,
                                                                     DataAccessAdapter adapter,
                                                                     bool negate,
                                                                     List<int> values)
        {
            return new FieldCompareRangePredicate(destinationProperty,
                                                  adapter.GetEntityFieldPersistenceInfo(destinationProperty),
                                                  negate,
                                                  values.ToArray());
        }

        /// <summary>
        /// Generates a predicate of type FieldCompareValuePredicate
        /// </summary>
        /// <param name="destinationProperty"></param>
        /// <param name="target"></param>
        /// <param name="comparisonOperator"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldCompareValuePredicate GetFieldCompareFilter(this DataAccessAdapter target,
                                                                        EntityField2 destinationProperty,
                                                                        ComparisonOperator comparisonOperator,
                                                                        object value)
        {
            return GetFieldCompareFilter(destinationProperty, target, comparisonOperator, value);
        }

        /// <summary>
        /// Generates a predicate of type FieldCompareValuePredicate
        /// </summary>
        /// <param name="destinationProperty"></param>
        /// <param name="adapter"></param>
        /// <param name="comparisonOperator"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldCompareValuePredicate GetFieldCompareFilter(EntityField2 destinationProperty,
                                                                       DataAccessAdapter adapter,
                                                                       ComparisonOperator comparisonOperator,
                                                                       object value)
        {
            return new FieldCompareValuePredicate(destinationProperty,
                                                  adapter.GetEntityFieldPersistenceInfo(destinationProperty),
                                                  comparisonOperator,
                                                  value);
        }

        /// <summary>
        /// Generates a predicate of type FieldCompareNullPredicate
        /// </summary>
        /// <param name="destinationProperty"></param>
        /// <param name="target"></param>
        /// <param name="negate"></param>
        /// <returns></returns>
        public static FieldCompareNullPredicate GetFieldIsNullFilter(this DataAccessAdapter target,
                                                                     EntityField2 destinationProperty,
                                                                     bool negate)
        {
            return GetFieldIsNullFilter(destinationProperty, target, negate);
        }

        /// <summary>
        /// Generates a predicate of type FieldCompareNullPredicate
        /// </summary>
        /// <param name="destinationProperty"></param>
        /// <param name="adapter"></param>
        /// <param name="negate"></param>
        /// <returns></returns>
        public static FieldCompareNullPredicate GetFieldIsNullFilter(EntityField2 destinationProperty,
                                                                     DataAccessAdapter adapter,
                                                                     bool negate)
        {
            return new FieldCompareNullPredicate(destinationProperty,
                                                  adapter.GetEntityFieldPersistenceInfo(destinationProperty),
                                                  negate);
        }
        #endregion

        #endregion
    }
}
