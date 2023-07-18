DO $$
BEGIN
	IF NOT EXISTS (SELECT NULL FROM "KrPermissionsSystem") 
	THEN
		INSERT INTO "KrPermissionsSystem" ("Version")
		SELECT 0;
	END IF;
END; $$
LANGUAGE PLPGSQL;