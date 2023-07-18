set -e
rm -f ~/tessa/web/wwwroot/extensions/*
cp -r ~/project/Source/web/wwwroot/extensions/ ~/tessa/web/wwwroot/

echo -e "\033[1;32mDone\033[0m"
