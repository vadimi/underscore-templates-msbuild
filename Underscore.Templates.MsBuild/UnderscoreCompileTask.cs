using System;
using System.IO;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Underscore.Templates.MsBuild
{
    /// <summary>
    /// Precompile underscore templates and store them into a separate file
    /// </summary>
    public class UnderscoreCompileTask : Task
    {
        private const string Module = "(function(global) {{\n{0}\n}})(this);";

        [Required]
        public ITaskItem[] SourceFiles { get; set; }

        [Required]
        public string OutputFile { get; set; }

        /// <summary>
        /// The default one is JST
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// _.templateSettings.interpolate
        /// </summary>
        public string Interpolate { get; set; }

        /// <summary>
        /// _.templateSettings.evaluate
        /// </summary>
        public string Evaluate { get; set; }

        /// <summary>
        /// _.templateSettings.escape
        /// </summary>
        public string Escape { get; set; }

        readonly StringBuilder compiledTemplates;

        public UnderscoreCompileTask()
        {
            compiledTemplates = new StringBuilder();
        }

        public override bool Execute()
        {
            try
            {
                if (!Validate())
                    return false;
                CompileFiles();
            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex);
                return false;
            }

            return true;
        }

        void CompileFiles()
        {
            AppendHeader();
            using (var compiler = new Compiler())
            {
                compiler.SetTemplateSettings(new TemplateSettings
                    {
                        interpolate = Interpolate,
                        evaluate = Evaluate,
                        escape = Escape
                    });
                var ns = GetNamespace();
                foreach (var sourceFile in SourceFiles)
                {
                    var templateName = Path.GetFileNameWithoutExtension(sourceFile.ItemSpec);
                    var template = File.ReadAllText(sourceFile.ItemSpec);
                    compiledTemplates.AppendFormat("global.{0}[\"{1}\"] = {2};\n", ns, templateName, compiler.Compile(template));
                }

                File.WriteAllText(OutputFile, string.Format(Module, compiledTemplates));
                Log.LogMessage("Compiled {0} templates to {1}", SourceFiles.Length, OutputFile);
            }
        }

        void AppendHeader()
        {
            var nsParts = GetNamespace().Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            var nsPartObject = string.Empty;
            foreach (var nsPart in nsParts)
            {
                nsPartObject += nsPart;
                compiledTemplates.AppendFormat("global.{0} = global.{0} || {{}};\n", nsPartObject);
                nsPartObject += ".";
            }
        }

        string GetNamespace()
        {
            return string.IsNullOrEmpty(Namespace) ? "JST" : Namespace;
        }

        bool Validate()
        {
            if (SourceFiles == null || SourceFiles.Length == 0)
            {
                Log.LogError("At least one file is required to be compiled.", new object[0]);
                return false;
            }
            if (string.IsNullOrEmpty(OutputFile))
            {
                Log.LogError("The OutputFile is required if one or more input files have been defined.", new object[0]);
                return false;
            }
            var sourceFiles = SourceFiles;
            for (int i = 0; i < sourceFiles.Length; i++)
            {
                var fileSpec = sourceFiles[i];
                if (string.Compare(fileSpec.ItemSpec, OutputFile, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    Log.LogError("Output file cannot be the same as source file(s).", new object[0]);
                    return false;
                }
            }

            return true;
        }
    }
}