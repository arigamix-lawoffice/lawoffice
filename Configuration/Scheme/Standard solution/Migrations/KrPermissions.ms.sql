IF NOT EXISTS (SELECT TOP(1) 1 FROM [KrPermissionsSystem])
BEGIN
	INSERT INTO [KrPermissionsSystem] ([Version])
	SELECT 0
END;