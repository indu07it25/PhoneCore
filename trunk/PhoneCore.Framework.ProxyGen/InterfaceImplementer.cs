using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mono.Cecil;

namespace PhoneCore.Framework.ProxyGen
{
    /// <summary>
    /// TODO TOTAL REFACTORING REQUIRED!
    /// </summary>
    public class InterfaceImplementer
    {
        public static void Generate(string assemblyPath, string interfaceTypeName, string proxyName, string outputDir)
        {
            StringWriter writer = new StringWriter();
            //var module = ModuleDefinition.ReadModule(assemblyPath);
            AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(assemblyPath);


            var type = assembly.MainModule.GetType(interfaceTypeName);
            var properties = new LinkedList<MethodDefinition>();
            writer.WriteLine("using System.Reflection;");
            writer.WriteLine("using PhoneCore.Framework.IoC.Interception.Proxies;");
            writer.WriteLine("namespace {0}{{", type.Namespace);
            writer.WriteLine("public class {0}:ProxyBase, {1}", proxyName, type.FullName);
            writer.WriteLine("{");

            //generate methods););
            foreach (var methodDefinition in type.Methods)
            {
                if (methodDefinition.IsGetter || methodDefinition.IsSetter)
                    properties.AddLast(methodDefinition);
                else
                    writer.WriteLine(GenerateMethodString(methodDefinition));
            }
            //System.Diagnostics.Debugger.Launch();
            //generates properties
            Func<string, IEnumerable<MethodDefinition>> look = (s) =>
            {
                return properties.Where(p => p.Name.Substring(4) == s);
            };
            while (properties.Count != 0)
            {
                var property = properties.First.Value;
                var fullProperties = look(property.Name.Substring(4)).ToList();
                MethodDefinition getter = null;
                MethodDefinition setter = null;
                if (fullProperties.Count == 2)
                {
                    getter = fullProperties[0].IsGetter ? fullProperties[0] : fullProperties[1];
                    setter = fullProperties[0].IsSetter ? fullProperties[0] : fullProperties[1];
                }
                else
                {
                    getter = fullProperties[0].IsGetter ? fullProperties[0] : null;
                    setter = fullProperties[0].IsSetter ? fullProperties[0] : null;
                }
                writer.WriteLine(GeneratePropertyString(getter, setter));
                fullProperties.ForEach(p => properties.Remove(p));
            }
            writer.WriteLine("}}");

            File.WriteAllText(String.Format(@"{0}\{1}.cs", outputDir, proxyName),writer.ToString());
        }

        private static string GeneratePropertyString(MethodDefinition getter, MethodDefinition setter)
        {
            MethodDefinition method;
            if (getter != null)
                method = getter;
            else
                method = setter;

            StringBuilder builder = new StringBuilder(64);
            builder.AppendFormat("public {0} {1}", method.ReturnType.FullName.Replace("`1", ""), method.Name.Substring(4));
            builder.AppendLine("{");

            //generate getter
            if (getter != null)
            {
                builder.AppendLine("get{ return RunBehaviors(BuildMethodInvocation(MethodBase.GetCurrentMethod()))");
                builder.AppendFormat(".GetReturnValue<{0}>();", getter.ReturnType.FullName.Replace("`1", ""));
                builder.Append("}");
            }

            //generate setter
            if (setter != null)
            {
                builder.AppendLine("set{ RunBehaviors(BuildMethodInvocation(MethodBase.GetCurrentMethod(), value));}");
            }

            builder.AppendLine("}");
            return builder.ToString();
        }


        private static string GenerateMethodString(MethodDefinition method)
        {
            StringBuilder builder = new StringBuilder(128);
            builder.AppendFormat("public {0} {1}{2}", 
                method.ReturnType.FullName == "System.Void" ? "void" : method.ReturnType.FullName.Replace("`1", ""),
                method.Name,
                method.HasGenericParameters ? String.Format("<{0}>", method.ReturnType.FullName.Replace("`1","")) : "");
            var parametersCount = method.Parameters.Count;
            builder.Append("(");
            for (int i = 0; i < parametersCount; i++)
            {
                builder.AppendFormat("{0} {1}", method.Parameters[i].ParameterType.FullName.Replace("`1", ""), method.Parameters[i].Name);
                if (i != (parametersCount - 1))
                    builder.Append(",");
            }
            builder.Append("){");

            //only one generic param is support currently
            
 


            builder.AppendLine("var methodInvocation = BuildMethodInvocation(MethodBase.GetCurrentMethod()");
         
            //parameter list
            for (int i = 0; i < parametersCount; i++)
                builder.AppendFormat(",{0}", method.Parameters[i].Name);
            builder.Append(");");

            if (method.HasGenericParameters)
            {
                builder.AppendLine(String.Format("methodInvocation.GenericTypes.Add(typeof({0}));", method.ReturnType.FullName));
            }

            //method body
            if (method.ReturnType.FullName != "System.Void")
                builder.AppendLine("return ");

            builder.AppendLine("RunBehaviors(methodInvocation)");
            if (method.ReturnType.FullName != "System.Void")
                builder.AppendFormat(".GetReturnValue<{0}>()", method.ReturnType.FullName.Replace("`1", ""));
            builder.Append(";");
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
