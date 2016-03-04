# Remove the revision number for versioning the NuGet packages:
$version = [version]$env:APPVEYOR_BUILD_VERSION
$semanticVersion = "$($version.Major).$($version.Minor).$($version.Build)"

# Only create a NuGet package for projects with a .nuspec file:
$nuspecFiles = Get-ChildItem -Filter *.nuspec -Recurse

foreach($nuspecFile in $nuspecFiles)
{
    NuGet.exe pack $nuspecFile.FullName.Replace(".nuspec", ".csproj") -Version $semanticVersion -Symbols -Prop Configuration="Release 4.6"
}