$Version = "1.0.0"

dotnet pack Framework.csproj /p:Configuration=Release /p:Version=$Version

Remove-Item  ..\..\..\nuget-packages\framework\$Version -Recurse -Force -ErrorAction Ignore

nuget add bin\Release\Framework.$Version.nupkg -source ..\..\..\nuget-packages
