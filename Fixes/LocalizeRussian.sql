/*
 * Script localizes in Russian cards in standard solution.
 * Such cards are already localized in Russian when system is installed with default settings.
 * You can run script on any supported DBMS.
 */

 /*
 * Скрипт локализует карточки в типовом решении на русском языке.
 * Такие карточки уже локализованы на русском на установленной по умолчанию системе.
 * Вы можете запустить скрипт на любой поддерживаемой СУБД.
 */

BEGIN TRANSACTION;

UPDATE "Roles"
SET "Name" = 'Все сотрудники'
WHERE "ID" = '7ff52dc0-ff6a-4c9d-ba25-b562c370004d';

UPDATE "DynamicRoles"
SET "Name" = 'Все сотрудники'
WHERE "ID" = '7ff52dc0-ff6a-4c9d-ba25-b562c370004d';

UPDATE "Roles"
SET "Name" = 'Автор документа'
WHERE "ID" = 'fd655183-a59e-409a-9a9b-1232087b0cb8';

UPDATE "Roles"
SET "Name" = 'Создатель карточки'
WHERE "ID" = 'b0918beb-94ad-4ec9-a728-749e14f14b28';

UPDATE "Roles"
SET "Name" = 'Инициатор'
WHERE "ID" = '20d9bdeb-db40-45dd-bab4-9555c48840d7';

UPDATE "Roles"
SET "Name" = 'Регистратор документа'
WHERE "ID" = 'f195932b-1978-48f1-bb4e-59e78ffccb5e';

UPDATE "Roles"
SET "Name" = 'Регистраторы'
WHERE "ID" = '0071b103-0ffa-49da-8776-53b9c654d815';

UPDATE "Roles"
SET "Name" = 'Руководитель инициатора'
WHERE "ID" = '69e4ba9b-04bd-468e-a5ec-38bd0de38f66';

UPDATE "Roles"
SET "Name" = 'Ознакомление'
WHERE "ID" = '9837d6cf-f355-4852-bd6c-231011f4464a';

COMMIT;
