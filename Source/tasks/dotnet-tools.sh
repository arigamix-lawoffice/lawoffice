set -e
dotnet build -c Release Source/Extensions/Tessa.Extensions.Console
dotnet build -c Release Source/Extensions/Tessa.Extensions.Shared
\cp -rf Source/Bin/Tessa.Extensions.Console/* tools/extensions/

echo -e "\033[1;32mDone\033[0m"
