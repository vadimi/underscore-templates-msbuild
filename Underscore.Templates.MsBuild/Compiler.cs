using System;
using System.IO;
using System.Reflection;

namespace Underscore.Templates.MsBuild
{
    public class Compiler : IDisposable
    {
        private readonly ScriptEngine engine;
        private readonly ParsedScript vm;

        public Compiler()
        {
            engine = new ScriptEngine("jscript");

            var underscore = LoadResource("underscore.js") + LoadResource("underscore.compile.js");
            vm = engine.Parse(underscore);
        }

        private ParsedScript Vm
        {
            get { return vm; }
        }

        private static string LoadResource(string name)
        {
            var asm = Assembly.GetCallingAssembly();
            using (var stream = asm.GetManifestResourceStream(string.Format("Underscore.Templates.MsBuild.{0}", name)))
            {
                if (stream == null)
                    return null;

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public string Compile(string template)
        {
            return (string)Vm.CallMethod("compile", template);
        }

        public void SetTemplateSettings(TemplateSettings settings)
        {
            Vm.CallMethod("setTemplateSettings", settings);
        }

        public void Dispose()
        {
            ((IDisposable)vm).Dispose();
            ((IDisposable)engine).Dispose();
        }
    }
}
