CREATE FUNCTION [Localize]
(
	@name nvarchar(256),
	@culture nvarchar(8)
)
RETURNS nvarchar(1024)
WITH RETURNS NULL ON NULL INPUT
AS
BEGIN
	IF LEFT(@Name, 1) <> '$' RETURN @name

	DECLARE @result nvarchar(1024)

	SELECT TOP 1 @result = ls.Value
	FROM [LocalizationEntries] le WITH(NOLOCK)
	cross apply (
		SELECT TOP 1 ls.Value
		FROM [LocalizationStrings] ls WITH(NOLOCK)
		WHERE ls.EntryID = le.ID
		  AND ls.Culture = @culture
	) ls
	WHERE le.Name = RIGHT(@name, len(@name) - 1)
	    AND le.Overridden = 0

	RETURN ISNULL(@result, @name)
END;