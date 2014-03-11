Underscore.js precompiler
===============================================

MsBuild task to precompile underscore.js templates and store them into a separate js file.

![NuGet Install](https://raw.github.com/vadimi/underscore-templates-msbuild/master/nugetinstall.png)

## Example
First of all it's necessary to include task:

```
<UsingTask TaskName="UnderscoreCompileTask"
    AssemblyFile="..\packages\Underscore.Templates.MsBuild.1.0.1\Underscore.Templates.MsBuild.dll" />
```

or

```
<Import Project="..\packages\Underscore.Templates.MsBuild.1.0.1\build\Underscore.Templates.MsBuild.targets" />
```

Run this task:

```xml
<ItemGroup>
	<TemplateFiles Include="$(MSBuildProjectDirectory)\Scripts\Templates\*.html" />
</ItemGroup>

<UnderscoreCompileTask
    SourceFiles="@(TemplateFiles)"
    OutputFile="%(TemplateFiles.RelativeDir)compiledTemplates.js"
    Namespace="myApp.Templates"
    Interpolate="\{\{(.+?)\}\}"
    Evaluate="\{%([\s\S]+?)%\}"
    Escape="\{%-([\s\S]+?)%\}" />
```

Please check few more examples in [examples](https://github.com/vadimi/underscore-templates-msbuild/tree/master/examples) folder.

#### Parameters
```SourceFiles``` - **required**, location of templates. Every templates needs to be stored in a separate file. file name without extension is template name.

```OutputFile``` - **required**, location of compiled templates js file.

```Namespace``` - **optional**, by default all templates will be stored in JST object like ```JST['myTemplate']```. It's possible to override this behavior by using this parameter.

```Interpolate, Evaluate, Escape``` - **optional**, see ```_.templateSettings``` description in [underscorejs](http://underscorejs.org/#template) documentation.

## License
License: MIT [http://www.opensource.org/licenses/mit-license.php](http://www.opensource.org/licenses/mit-license.php)