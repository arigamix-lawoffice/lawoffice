set -e
\cp -rf Source/Bin/Tessa.Extensions.Console/* tools/extensions/
\cp -rf Source/Bin/Tessa.Extensions.Server/* ~/tessa/web/extensions/
\cp -rf Source/Bin/extensions/* ~/tessa/chronos/extensions/
\cp -rf Source/Bin/Tessa.Extensions.Chronos/ ~/tessa/chronos/Plugins/

echo -e "\033[1;33mRestarting web and chronos services\033[0m"
sudo systemctl restart tessa chronos
echo -e "\033[1;32mDone\033[0m"
