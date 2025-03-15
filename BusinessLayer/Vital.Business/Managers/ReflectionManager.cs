using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Vital.Business.Shared;
using Vital.Business.Shared.Exceptions;

namespace Vital.Business.Managers
{
    public class ReflectionManager : BaseManager
    {
        #region Private Clasess

        /// <summary>
        /// Contains the source that needs to be called.
        /// </summary>
        private class Source
        {
            public string Assembly;
            public string Calss;
            public string Method;
            public List<string> Parameters;
        }

        /// <summary>
        /// Contains the members that needs to be set.
        /// </summary>
        private class Member
        {
            public string Name { get; set; }
            public string Value;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes the passed reflection calling string and return the result.
        /// </summary>
        /// <param name="exucutingConfig">Calling string should be as "Assembly.Calss.Method(Parameter, Parameters, ..)"</param>
        /// <returns>The calling results as Object.</returns>
        public object Exucute(string exucutingConfig)
        {
            try
            {
                var sourceOject = GetSourceObjectHelper(exucutingConfig);

                var assembly = Assembly.Load(sourceOject.Assembly);

                var classType = assembly.GetType(sourceOject.Calss);

                var classInstance = Activator.CreateInstance(classType);

                var methodInfo = classType.GetMethod(sourceOject.Method);

                var methodParameters = methodInfo.GetParameters();

                //Convert the parameters to string match the types of each one.
                var parametersAsTypes = methodParameters.Select(
                        par => ConvertToString(sourceOject.Parameters[par.Position], par.ParameterType)).ToList();

                //Convert the strings that results from the previous step to objects match that type for each one. 
                var parameters =
                    methodParameters.Select(
                        par => ConvertFromString(parametersAsTypes[par.Position], par.ParameterType));

                var result = classType.InvokeMember(sourceOject.Method,
                                                    BindingFlags.InvokeMethod | BindingFlags.Instance |
                                                    BindingFlags.Public,
                                                    null, classInstance, parameters.ToArray());

                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Convert the parameter[String] to object[base on the passed type]
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="type">The object type.</param>
        /// <returns></returns>
        public static object ConvertFromString(string parameter, Type type)
        {
            var tc = TypeDescriptor.GetConverter(type);

            return tc.ConvertFrom(null, CultureInfo.InvariantCulture, parameter);
        }

        /// <summary>
        /// Convert the passed parameter[String] to string based on the passed type. 
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="type">The object type.</param>
        /// <returns></returns>
        public static string ConvertToString(object parameter, Type type)
        {
            var tc = TypeDescriptor.GetConverter(type);

            return tc.ConvertToString(null, CultureInfo.InvariantCulture, parameter);
        }

        /// <summary>
        /// Sets The passed members for the passed object.
        /// </summary>
        /// <param name="desObj">The Object to set its parameters.</param>
        /// <param name="membersConfig">The parameters config. string should be as "Property1 = Value1, Property2 = Value2 .."</param>
        /// <param name="isRegEx"></param>
        public void SetMembers(object desObj, string membersConfig, bool isRegEx = false)
        {
            try
            {
                var memberObject = GetObjectMembersHelper(membersConfig, isRegEx);

                foreach (var member in memberObject)
                {
                    var memberInfo = desObj.GetType().GetMember(member.Name);

                    if (memberInfo == null) continue;

                    if (memberInfo.Length < 1) continue;

                    var memberType = memberInfo[0].MemberType;

                    if (memberType == MemberTypes.Field)
                    {
                        var fieldInfo = desObj.GetType().GetField(member.Name);

                        var value = Convert.ChangeType(member.Value, fieldInfo.FieldType);

                        fieldInfo.SetValue(desObj, value);
                    }
                    else if (memberType == MemberTypes.Property)
                    {
                        var propertyInfo = desObj.GetType().GetProperty(member.Name);

                        var value = Convert.ChangeType(member.Value, propertyInfo.PropertyType);

                        propertyInfo.SetValue(desObj, value, null);
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalLogicalException(exception);
            }

        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Converts the passed membersConfig to list of Member objects.
        /// </summary>
        /// <param name="membersConfig">The membersConfig string.</param>
        /// <param name="isRegEx"></param>
        /// <returns></returns>
        private static IEnumerable<Member> GetObjectMembersHelper(string membersConfig, bool isRegEx = false)
        {
            //Example : Property1 = Value1, Property2 = Value2 ..
            try
            {
                var membersParts = membersConfig.Trim().Split(isRegEx ? ';' : ',');

                var members = membersParts.Select(membersPart => membersPart.Trim().Split('='))
                    .Select(memberPart => new Member() {Name = memberPart[0].Trim(), Value = memberPart[1].Trim()}).
                    ToList();

                return members;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Converts the passed source sting to Source objects.
        /// </summary>
        /// <param name="source">The source</param>
        /// <returns></returns>
        private static Source GetSourceObjectHelper(string source)
        {
            //Example : Assembly.Calss.Method(Parameter, Parameters, ..)

            try
            {
                var sourceResult = new Source();

                var mainSourcePath = source.Split('@');

                var sourceParts = mainSourcePath[0].Trim(')').Split('(');

                var callEdges = sourceParts[0].Split('.');

                var parameters = sourceParts[1].Split(',');

                sourceResult.Assembly = mainSourcePath[1];

                sourceResult.Calss = callEdges[0];

                for (var i = 1; i < callEdges.Length - 1; i++)
                {
                    sourceResult.Calss += "." + callEdges[i];
                }

                sourceResult.Method = callEdges[callEdges.Length - 1];

                sourceResult.Parameters = parameters.Select(par => par.Trim()).ToList();

                return sourceResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalLogicalException(exception);
            }
        }

        #endregion
    }
}
