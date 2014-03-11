## Sample app with embedded MSBuild script ##

#### Instructions ####

1. Run `Install-Package Underscore.Templates.MsBuild` command from NuGet Package manager console.

2. Open `.csproj` file in edit mode and add necessary build code to `<Target Name="BeforeBuild"></Target>` element.

3. Run build.