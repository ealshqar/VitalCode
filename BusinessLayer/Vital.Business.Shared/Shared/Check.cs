using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Vital.Business.Shared.Shared
{
    public class Check
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        internal Check()
        {
        }

        #endregion

        #region Argument


        /// <summary>
        /// Inner Class Argument.
        /// </summary>
        public class Argument
        {
            internal Argument()
            {
            }
            
            /// <summary>
            /// Check if Guid is not empy.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotEmpty(Guid argument, string argumentName)
            {
                if (argument == Guid.Empty)
                {
                    throw new ArgumentException("\"{0}\" cannot be an empty guid.".FormatWith(argumentName), argumentName);
                }
            }

            /// <summary>
            /// Check if the string is not empty.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotEmpty(string argument, string argumentName)
            {
                if (string.IsNullOrEmpty((argument ?? string.Empty).Trim()))
                {
                    throw new ArgumentException("\"{0}\" cannot be blank.".FormatWith(argumentName), argumentName);
                }
            }

            /// <summary>
            /// Check if the parameter is not empty.
            /// </summary>
            /// <typeparam name="TParameter">The generic parameter.</typeparam>
            /// <param name="parameter">The parameter.</param>
            [DebuggerStepThrough]
            public static void IsNotEmpty<TParameter>(Expression<Func<TParameter>> parameter) where TParameter : class
            {
                string parameterName = ExpressionHelper.GetParameterName(parameter);
                var parameterValue = ExpressionHelper.GetParameterValue(parameter) as string;

                if (string.IsNullOrEmpty((parameterValue ?? string.Empty).Trim()))
                {
                    throw new ArgumentException("\"{0}\" cannot be blank.".FormatWith(parameterName), parameterName);
                }
            }

            /// <summary>
            /// Check if its not out of length.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="length">The length.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotOutOfLength(string argument, int length, string argumentName)
            {
                if (argument.Trim().Length > length)
                {
                    throw new ArgumentException("\"{0}\" cannot be more than {1} character(s).".FormatWith(argumentName, length), argumentName);
                }
            }

            /// <summary>
            /// Check if its not null.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotNull(object argument, string argumentName)
            {
                if (argument == null)
                {
                    throw new ArgumentNullException(argumentName);
                }
            }

            /// <summary>
            /// Check if its not null for generic type.
            /// </summary>
            /// <typeparam name="TParameter">The generic parameter.</typeparam>
            /// <param name="parameter">The parameter.</param>
            [DebuggerStepThrough]
            public static void IsNotNull<TParameter>(Expression<Func<TParameter>> parameter)
            {
                string parameterName = ExpressionHelper.GetParameterName(parameter);
                var parameterValue = ExpressionHelper.GetParameterValue(parameter);

                if (parameterValue == null)
                {
                    throw new ArgumentNullException(string.Format("The parameter named ‘{0}’ cannot be null", parameterName));
                }
            }

            /// <summary>
            /// Check if the number is negative.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotNegative(int argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Check if the number is not negative or zero.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(int argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Check if the number is not negative.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotNegative(long argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Check if the number is not negative or zero.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(long argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Check if its not negative.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name/</param>
            [DebuggerStepThrough]
            public static void IsNotNegative(float argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Check if the number is not negative or zero.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(float argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Check if the number is not negative.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotNegative(decimal argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Check if the number is not negative or zero.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(decimal argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Check if the date time is not valid.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotInvalidDate(DateTime argument, string argumentName)
            {
                if (!argument.IsValid())
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Check if the date time is not in the past.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotInPast(DateTime argument, string argumentName)
            {
                if (argument < DateTime.Now)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Check if the date is not in the future.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotInFuture(DateTime argument, string argumentName)
            {
                if (argument > DateTime.Now)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Check if the time span is not negative.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotNegative(TimeSpan argument, string argumentName)
            {
                if (argument < TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Check if the time span is not negative or zero.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(TimeSpan argument, string argumentName)
            {
                if (argument <= TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Check if the collection is not empty.
            /// </summary>
            /// <typeparam name="T">The generic type.</typeparam>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotEmpty<T>(ICollection<T> argument, string argumentName)
            {
                IsNotNull(argument, argumentName);

                if (argument.Count == 0)
                {
                    throw new ArgumentException("Collection cannot be empty.", argumentName);
                }
            }

            /// <summary>
            /// Check if the collection is null or empty.
            /// </summary>
            /// <typeparam name="T">The generic type.</typeparam>
            /// <param name="collection">The collection.</param>
            [DebuggerStepThrough]
            public static bool IsNullOrEmpty<T>(ICollection<T> collection)
            {
                return (collection == null) || (collection.Count == 0);
            }

            /// <summary>
            /// Check if the argument is not out of range.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="min">The min.</param>
            /// <param name="max">The max.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotOutOfRange(int argument, int min, int max, string argumentName)
            {
                if ((argument < min) || (argument > max))
                {
                    throw new ArgumentOutOfRangeException(argumentName, "{0} must be between \"{1}\"-\"{2}\".".FormatWith(argumentName, min, max));
                }
            }

            /// <summary>
            /// Check if the email is not invalid email.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotInvalidEmail(string argument, string argumentName)
            {
                IsNotEmpty(argument, argumentName);

                if (!argument.IsEmail())
                {
                    throw new ArgumentException("\"{0}\" is not a valid email address.".FormatWith(argumentName), argumentName);
                }
            }

            /// <summary>
            /// Check if the url is not invalid.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">The argument name.</param>
            [DebuggerStepThrough]
            public static void IsNotInvalidWebUrl(string argument, string argumentName)
            {
                IsNotEmpty(argument, argumentName);

                if (!argument.IsWebUrl())
                {
                    throw new ArgumentException("\"{0}\" is not a valid web url.".FormatWith(argumentName), argumentName);
                }
            }
        }

        #endregion
    }
}
