set -e
dotnet build -c Release Source/Extensions/Tessa.Extensions.Server.Web
\cp -rf Source/Bin/Tessa.Extensions.Server/* ~/tessa/web/extensions/

echo -e "\033[1;33mRestarting web service\033[0m"
sudo systemctl restart tessa
echo -e "\033[1;32mDone\033[0m"
