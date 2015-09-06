# Determine the 

$buildVersion = $env:APPVEYOR_BUILD_VERSION
Write-Host $buildVersion.GetType()
Write-Host $buildVersion

# Only create a NuGet package for projects with a .nuspec file:
$nuspecFiles = Get-ChildItem -Filter *.nuspec -Recurse

foreach($nuspecFile in $nuspecFiles)
{
    NuGet.exe pack $nuspecFile.FullName.Replace(".nuspec", ".csproj") -Symbols
}