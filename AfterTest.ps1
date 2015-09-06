# Only create a NuGet package for projects with a .nuspec file:
$nuspecFiles = Get-ChildItem -Filter *.nuspec -Recurse

foreach($nuspecFile in $nuspecFiles)
{
    nuget pack $nuspecFile.FullName.Replace(".nuspec", ".csproj") -Symbol
}