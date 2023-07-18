set -e
dotnet build -c Release Source/Extensions/Tessa.Extensions.Server.Web
dotnet build -c Release Source/Extensions/Tessa.Extensions.Chronos
dotnet build -c Release Source/Extensions/Tessa.Extensions.Console
