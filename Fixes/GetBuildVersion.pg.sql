/*
Скрипт возвращает версию сборки платформы, которая в текущий момент установлена на сервере.

Команда возвращает актуальное значение до того, как будет выполнено любое изменение конфигурации, в т.ч. посредством утилиты tadmin.exe, т.к. это перезапишет версию сборки.

Пример использования в скрипте .bat (Windows):

set build=
for /f "tokens=* usebackq" %%f in (`tadmin.exe Select "Fixes\GetBuildVersion.pg.sql" /q`) do set build=%%f
echo build="%build%"

Пример использования в скрипте .sh (Linux):

command=(./tadmin Select "Fixes/GetBuildVersion.pg.sql" -q)
OldBuild="$(${command[@]})"
*/

SELECT "BuildVersion" FROM "Configuration" LIMIT(1);
