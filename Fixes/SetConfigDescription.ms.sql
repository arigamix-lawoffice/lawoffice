/*
Скрипт устанавливает текстовое описание для конфигурации платформы, установленной на сервере.

Требуется указать параметры:
* Text - текст описания сборки

Не увеличивает версию конфигурации, для этого выполните команду tadmin.exe IncrementVersion

Пример использования в командном файле (для русскоязычного описания установите кодировку командного файла как OEM866):

set "description=Текстовое описание сборки"
tadmin.exe Sql "Fixes\SetConfigDescription.ms.sql" "/p:Text=%description%" /nologo


Чтобы сбросить описание на "Конфигурация по умолчанию", используйте команду:

tadmin.exe Sql "Fixes\SetConfigDescription.ms.sql" /p:Text /nologo
*/

if not exists (select * from sysobjects where name='Configuration' and xtype='U')
begin
	return;
end

update Configuration set Description = coalesce(@Text, N'$Configuration_DefaultDescription')
