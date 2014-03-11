## Sample app with external MSBuild script ##

#### Instructions ####

1. In order to run external build script from Visual Studio open project properties, go to `Build Events` tab and add the following code to `Pre-build event command-line` property:

	`"$(MSBuildBinPath)\msbuild.exe" "$(ProjectDir)BuildScript.xml"`

2. Run `Install-Package Underscore.Templates.MsBuild` command.

3. Run build.