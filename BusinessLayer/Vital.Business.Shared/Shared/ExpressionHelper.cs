using System;
using System.Linq;
using System.Linq.Expressions;

namespace Vital.Business.Shared.Shared
{
    public class ExpressionHelper
    {
        #region Public Methods

        /// <summary>
        /// Gets the parameter name.
        /// </summary>
        /// <typeparam name="TParameter">The generic type.</typeparam>
        /// <param name="parameterToCheck">The parameter to check.</param>
        /// <returns></returns>
        public static string GetParameterName<TParameter>(Expression<Func<TParameter>> parameterToCheck)
        {
            var memberExpression = parameterToCheck.Body as MemberExpression;

            if (memberExpression != null)
            {
                string parameterName = memberExpression.Member.Name;

                return parameterName;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the passed property value from the passed source.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="propName">Property Name.</param>
        /// <returns></returns>
        public static object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        /// <summary>
        /// Sets the passed value for the passed property.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="propName">The property name.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetPropertyValue(object src, string propName, object value)
        {
            var propertyInfo = src.GetType().GetProperty(propName);
            propertyInfo.SetValue(src, value, null);
        }

        /// <summary>
        /// Gets the member name.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string GetMemberName(Expression expression)
        {
            if (expression is LambdaExpression)
                expression = ((LambdaExpression)expression).Body;

            if (expression is MemberExpression)
            {
                var memberExpression = (MemberExpression)expression;

                if (memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    return GetMemberName(memberExpression.Expression)
                        + "."
                        + memberExpression.Member.Name;
                }
                return memberExpression.Member.Name;
            }

            if (expression is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception(string.Format("Cannot interpret member from {0}", expression));

                return GetMemberName(unaryExpression.Operand);
            }

            throw new Exception(string.Format("Could not determine member from {0}", expression));
        }

        /// <summary>
        /// Gets the parameter value.
        /// </summary>
        /// <typeparam name="TParameter">The generic type.</typeparam>
        /// <param name="parameterToCheck">The parameter to check.</param>
        /// <returns></returns>
        public static TParameter GetParameterValue<TParameter>(Expression<Func<TParameter>> parameterToCheck)
        {
            var parameterValue = parameterToCheck.Compile().Invoke();

            return parameterValue;
        }

        /// <summary>
        /// Dynamically get the name of the property
        /// </summary>
        /// <param name="expression"> The property we are using</param>        
        public static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            return GetName(expression);
        }

        /// <summary>
        /// Check if the passed obj has the passed property name.
        /// </summary>
        public static bool HasProperty(object objectToCheck, string memberName)
        {
            var type = objectToCheck.GetType();
            return type.GetProperty(memberName) != null;
        }

        /// <summary>
        /// Check if the passed type has the passed property name.
        /// </summary>
        public static bool HasProperty(Type type, string memberName)
        {
            return type.GetProperty(memberName) != null;
        }

        /// <summary>
        /// Dynamically get the full name of the property.
        /// </summary>
        /// <typeparam name="T">Property parent.</typeparam>
        /// <typeparam name="TS">property.</typeparam>
        /// <param name="expression">The property parent we are using</param>
        /// <param name="subExpression">The property we are using</param>
        /// <returns></returns>
        public static string GetPropertyName<T, TS>(Expression<Func<T>> expression, Expression<Func<TS>> subExpression)
        {
            return GetName(expression) + "." + GetName(subExpression);
        }

        private static string GetName(Expression method)
        {
            var lambda = method as LambdaExpression;
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            return memberExpr == null ? null : memberExpr.Member.Name;
        }

        public static string GetNameOf<T>(Expression<Func<T>> property)
        {
            return (property.Body as MemberExpression).Member.Name;
        }

        /// <summary>
        /// Generates an lambda expression for a member in an object.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="memberPathName">The member name or path.</param>
        /// <returns></returns>
        public static Expression<Func<T, object>> GenerateMemberExpression<T>(string memberPathName)
        {
            var param = Expression.Parameter(typeof(T), "p");

            var property = memberPathName.Split('.').Aggregate<string, Expression>(param, Expression.Property);

            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), param);

            return lambda;
        } 

        #endregion
    }
}
