/*
Скрипт устанавливает номер сборки платформы, установленной на сервере. Используется при автоматическом обновлении на новую сборку
в скрипте Upgrade.bat в случае отката преждевременного изменения номера сборки в базе данных при возникновении ошибки.

Требуется указать параметры:
* BuildVersion - номер сборки платформы, которую требуется установить

Не увеличивает версию конфигурации, для этого выполните команду tadmin.exe IncrementVersion

Пример использования в командном файле:

set "build=2.5"
tadmin.exe Sql "Fixes\SetBuildVersion.ms.sql" "/p:BuildVersion=%build%" /nologo
*/

if not exists (select * from sysobjects where name='Configuration' and xtype='U')
begin
	return;
end

update Configuration set BuildVersion = @BuildVersion
