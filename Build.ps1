# Add/Remove build configurations as needed
$buildConfigurations = @(
    "Release 4.6",
    "Release 4.5",
    "Release 4.0",
    "Release 3.5"
)

# Find all .sln files
$solutionFiles = Get-ChildItem -Filter *.sln -Recurse

# Force MSBuild 14 to support C# 6 features
$MSBuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe"

foreach($solutionFile in $solutionFiles)
{
    foreach($buildConfiguration in $buildConfigurations)
    {
        . $MSBuild $solutionFile.FullName /p:Configuration=$buildConfiguration
    }
}