/*
Скрипт устанавливает текстовое описание для конфигурации платформы, установленной на сервере.

Требуется указать параметры:
* Text - текст описания сборки

Не увеличивает версию конфигурации, для этого выполните команду tadmin.exe IncrementVersion

Пример использования в командном файле (для русскоязычного описания установите кодировку командного файла как OEM866):

set "description=Текстовое описание сборки"
tadmin.exe Sql "Fixes\SetConfigDescription.pg.sql" "/p:Text=%description%" /nologo


Чтобы сбросить описание на "Конфигурация по умолчанию", используйте команду:

tadmin.exe Sql "Fixes\SetConfigDescription.pg.sql" /p:Text /nologo
*/

UPDATE "Configuration" SET "Description" = COALESCE(@Text, '$Configuration_DefaultDescription')
