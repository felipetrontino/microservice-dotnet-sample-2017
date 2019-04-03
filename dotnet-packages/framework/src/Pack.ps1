$Version = "1.0.0"

dotnet pack LiteFramework.csproj /p:Configuration=Release /p:Version=$Version

Remove-Item  ..\..\..\nuget-packages\liteframework\$Version -Recurse -Force -ErrorAction Ignore

nuget add bin\Release\LiteFramework.$Version.nupkg -source ..\..\..\nuget-packages
