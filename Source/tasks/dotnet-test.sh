set -e
dotnet test Source/Tests/Tessa.Test.Server -c Release -l "html;LogFileName=dotnet-test-server.html" -r Source/logs
dotnet test Source/Tests/Tessa.Test.Client -c Release -l "html;LogFileName=dotnet-test-client.html" -r Source/logs
firefox Source/logs/dotnet-test-*.html
