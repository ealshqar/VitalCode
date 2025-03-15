using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.IO;

namespace Vital.Business.Shared.Shared
{
    public class VitalLogicExecuter
    {
        /// <summary>
        /// Executes logic in the passed method, the logic will be executed in memory when user has permissions and logic will be executed in a separate on fly generated
        /// exe file that will be run with admin permissions to allow performing the required logic when permissions are needed
        /// </summary>
        /// <param name="methodName">The name of the method to be executed</param>
        /// <param name="methodImplementation">The implementation of the method to be executed</param>
        /// <param name="referencesAssemblies">The list of assemblies needed for executing the method</param>
        /// <param name="referencesAssembliesDLLs">The list of DLL's that need to be referenced</param>
        /// <param name="userCancelledPermission">Indicates if user cancelled permission when user confirmation is needed</param>
        /// <returns></returns>
        public static ProcessResult ExecuteMethod(string methodName,
                                                  string methodImplementation,
                                                  List<string> referencesAssemblies,
                                                  List<string> referencesAssembliesDLLs,
                                                  out bool userCancelledPermission)
        {
            var processResult = ProcessResult.Succeed;

            var executionResultPath = Application.StartupPath + @"\" + StaticKeys.VitalExecutionResult;
            var executionLogPath = Application.StartupPath + @"\" + StaticKeys.VitalExecutionLog;
            var version = "v4.0";
            var assemblyAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(System.Runtime.Versioning.TargetFrameworkAttribute), false);

            if (assemblyAttributes.Any())
            {
                var frameWorkAttribute = assemblyAttributes[0] as TargetFrameworkAttribute;

                if (frameWorkAttribute != null)
                {
                    version = frameWorkAttribute.FrameworkName.Replace(".NETFramework,Version=", string.Empty);
                }
            }

            var providerOptions = new Dictionary<string, string> { { "CompilerVersion", version } };
            var codeProvider = new CSharpCodeProvider(providerOptions);
            var compilerParameters = new CompilerParameters();

            var assembliesString = string.Empty;

            foreach (var assemblyDll in referencesAssembliesDLLs)
            {
                var assemblyDLLName = assemblyDll + ".dll";

                if (!compilerParameters.ReferencedAssemblies.Contains(assemblyDLLName))
                {
                    compilerParameters.ReferencedAssemblies.Add(assemblyDLLName);
                }
            }

            foreach (var assembly in referencesAssemblies)
            {
                var assemblyName = "using " + assembly + ";";

                if (!assembliesString.Contains(assemblyName))
                {
                    assembliesString += assemblyName + Environment.NewLine;
                }
            }

            assembliesString += "using System.IO;" + Environment.NewLine;

            var sourceToExecute = @"
            " + assembliesString + @"
            
            namespace VitalCodeExecuter
            {
                public class Program
                {
                    public static void Main()
                    {
                        var executionResultPath = @""" + executionResultPath + @""";
                        var executionLogPath = @""" + executionLogPath + @""";

                        var executionResult = string.Empty;
                        var executionLog = string.Empty;

                        try
                        {
                            " + methodName + @"(out executionResult, out executionLog);
                            File.WriteAllText(executionResultPath, executionResult);
                            File.WriteAllText(executionLogPath, executionLog);
                        }
                        catch (Exception exception)
                        {
                            File.WriteAllText(executionResultPath, """ + StaticKeys.VITALSCRIPTFAIL + @""");
                            File.WriteAllText(executionLogPath, exception.ToString());
                        }
                    }

                    " + methodImplementation + @"
                }
            }
            ";

            var hasAdminPermissions = IsRunAsAdministrator();
            userCancelledPermission = false;

            var fileToExecute = Application.StartupPath + @"\" + "Vital Operation - " +  methodName + ".exe";

            if (hasAdminPermissions)
            {
                compilerParameters.GenerateInMemory = true;
            }
            else
            {
                compilerParameters.GenerateExecutable = true;
                compilerParameters.OutputAssembly = fileToExecute;
            }

            var complilerResults = codeProvider.CompileAssemblyFromSource(compilerParameters, sourceToExecute);

            if (complilerResults.Errors.HasErrors)
            {
                File.WriteAllText(executionResultPath, StaticKeys.VITALSCRIPTFAIL);

                foreach (CompilerError error in complilerResults.Errors)
                {
                    File.WriteAllText(executionLogPath, error.ErrorText + Environment.NewLine);
                }

                processResult.IsSucceed = false;
                processResult.Message = StaticKeys.VitalCodeExecuterError;
            }
            else
            {
                if (hasAdminPermissions)
                {
                    try
                    {
                        var assembly = complilerResults.CompiledAssembly;
                        var program = assembly.GetType("VitalCodeExecuter.Program");
                        var main = program.GetMethod("Main");
                        main.Invoke(null, null);
                    }
                    catch (Exception exception)
                    {
                        File.WriteAllText(executionResultPath, StaticKeys.VITALSCRIPTFAIL);
                        File.WriteAllText(executionLogPath, exception.Message + Environment.NewLine + exception.InnerException);
                    }
                }
                else
                {
                    var processInfo = new ProcessStartInfo(fileToExecute)
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        UseShellExecute = true, 
                        Verb = "runas"
                    };

                    try
                    {
                        try
                        {
                            var processRunResult = Process.Start(processInfo);
                            processRunResult.WaitForExit();
                        }
                        catch (Exception exception)
                        {
                            userCancelledPermission = true;
                            File.WriteAllText(executionResultPath, StaticKeys.VITALSCRIPTFAIL);
                            File.WriteAllText(executionLogPath, exception.Message + Environment.NewLine + exception.InnerException);
                        }
                        File.Delete(fileToExecute);
                    }
                    catch (Exception exception)
                    {
                        File.WriteAllText(executionResultPath, StaticKeys.VITALSCRIPTFAIL);
                        File.WriteAllText(executionLogPath, exception.Message + Environment.NewLine + exception.InnerException);
                    }
                }

                var executionResult = File.ReadAllText(executionResultPath);
                var executionLog = File.ReadAllText(executionLogPath);

                File.Delete(executionResultPath);
                File.Delete(executionLogPath);

                processResult.IsSucceed = executionResult.Contains(StaticKeys.VITALSCRIPTSUCCESS);
                processResult.Message = executionLog;
            }
            return processResult;
        }

        /// <summary>
        /// Checks if the current application is run in admin mode
        /// </summary>
        /// <returns></returns>
        public static bool IsRunAsAdministrator()
        {
            try
            {
                var windowsIdentity = WindowsIdentity.GetCurrent();

                if (windowsIdentity != null)
                {
                    var windowsPrincipal = new WindowsPrincipal(windowsIdentity);

                    return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
                }
            }
            catch (Exception exception)
            {
                //
            }

            return false;
        }
    }
}
