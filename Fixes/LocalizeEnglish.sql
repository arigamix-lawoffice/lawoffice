/*
 * Script localizes in English cards in standard solution.
 * You can run script on any supported DBMS.
 */

 /*
 * Скрипт локализует карточки в типовом решении на английском языке.
 * Вы можете запустить скрипт на любой поддерживаемой СУБД.
 */

BEGIN TRANSACTION;

UPDATE "Roles"
SET "Name" = 'All employees'
WHERE "ID" = '7ff52dc0-ff6a-4c9d-ba25-b562c370004d';

UPDATE "DynamicRoles"
SET "Name" = 'All employees'
WHERE "ID" = '7ff52dc0-ff6a-4c9d-ba25-b562c370004d';

UPDATE "Roles"
SET "Name" = 'Author of document'
WHERE "ID" = 'fd655183-a59e-409a-9a9b-1232087b0cb8';

UPDATE "Roles"
SET "Name" = 'Creator of card'
WHERE "ID" = 'b0918beb-94ad-4ec9-a728-749e14f14b28';

UPDATE "Roles"
SET "Name" = 'Initiator'
WHERE "ID" = '20d9bdeb-db40-45dd-bab4-9555c48840d7';

UPDATE "Roles"
SET "Name" = 'Registrator of document'
WHERE "ID" = 'f195932b-1978-48f1-bb4e-59e78ffccb5e';

UPDATE "Roles"
SET "Name" = 'Registrators'
WHERE "ID" = '0071b103-0ffa-49da-8776-53b9c654d815';

UPDATE "Roles"
SET "Name" = 'Supervisor of initiator'
WHERE "ID" = '69e4ba9b-04bd-468e-a5ec-38bd0de38f66';

UPDATE "Roles"
SET "Name" = 'Acquaintance'
WHERE "ID" = '9837d6cf-f355-4852-bd6c-231011f4464a';

COMMIT;
