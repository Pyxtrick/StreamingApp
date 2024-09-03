using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.Core.Commands.Custom;
public class CustomExecuter
{
    //TODO: custom code executer (send in code to be able to be executed)
    public void execute(string code)
    {
        var Compiler = new Microsoft.CSharp.CSharpCodeProvider().CreateCompiler();

        var parms = new CompilerParameters();
        parms.ReferencedAssemblies.Add("System.dll");
        parms.ReferencedAssemblies.Add("System.Core.dll");
        parms.GenerateInMemory = true;

        var classCode = @"
        using System;
        namespace MyApp
        {
            public class Test1
            {
                public string HelloWorld(string name) 
                {
                    return ""Hello "" + name + "" from the classic compiler."";
                }
            }
        }
        ";

        CompilerResults result = Compiler.CompileAssemblyFromSource(parms, classCode);

        if (result.Errors.Count > 0)
        {
            Console.WriteLine("*** Compilation Errors");
            foreach (var error in result.Errors)
            {
                Console.WriteLine("- " + error);

                return;
            }
        }

        var ass = result.CompiledAssembly;

        dynamic inst = ass.CreateInstance("MyApp.Test1");
        string methResult = inst.HelloWorld("Rick") as string;

        Console.WriteLine(methResult);
    }
}
