# Only create a NuGet package for projects with a .nuspec file:
Write-Host "Creating NuGet packages..."
$nuspecFiles = Get-ChildItem -Filter *.nuspec -Recurse

foreach($nuspecFile in $nuspecFiles)
{
    NuGet.exe pack $nuspecFile.FullName.Replace(".nuspec", ".csproj") -Symbols
}