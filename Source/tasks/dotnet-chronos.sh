set -e
dotnet build -c Release Source/Extensions/Tessa.Extensions.Server
dotnet build -c Release Source/Extensions/Tessa.Extensions.Chronos
\cp -rf Source/Bin/extensions/* ~/tessa/chronos/extensions/
\cp -rf Source/Bin/Tessa.Extensions.Chronos/ ~/tessa/chronos/Plugins/

echo -e "\033[1;33mRestarting chronos service\033[0m"
sudo systemctl restart chronos
echo -e "\033[1;32mDone\033[0m"
