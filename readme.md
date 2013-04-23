Underscore.js precompiler
===============================================

MsBuild task to precompile underscore.js templates and store them into a separate js file.

## Example
First of all it's necessary to include task:

```
<UsingTask TaskName="UnderscoreCompileTask"
    AssemblyFile="package\location\Underscore.Templates.MsBuild.dll" />
```

Run this task:

```xml
<TemplateFiles Include="$(ScriptsDir)\Templates\*.html" />

<UnderscoreCompileTask
    SourceFiles="@(TemplateFiles)"
    OutputFile="%(TemplateFiles.RelativeDir)compiledTemplates.js"
    Namespace="myApp.Templates"
    Interpolate="\{\{(.+?)\}\}"
    Evaluate="\{%([\s\S]+?)%\}"
    Escape="\{%-([\s\S]+?)%\}" />
```

#### Parameters
```SourceFiles``` - **required**, location of templates. Every templates needs to be stored in a separate file. file name without extension is template name.

```OutputFile``` - **required**, location of compiled templates js file.

```Namespace``` - **optional**, by default all templates will be stored in JST object like ```JST['myTemplate']```. It's possible to override this behavior by using this parameter.

```Interpolate, Evaluate, Escape``` - **optional**, see ```_.templateSettings``` description in [underscorejs](http://underscorejs.org/#template) documentation.