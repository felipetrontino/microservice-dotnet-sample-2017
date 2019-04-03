dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=.\results\coverage\  /p:Exclude="[Framework*]*"

& "../../../tools/report-generator/ReportGenerator.exe" -reports:"./results/coverage/coverage.opencover.xml" -targetdir:"./results/coverage/Reports" -reportTypes:htmlInline

Start-Process "chrome" -Argument "./results/coverage/reports/index.htm"