CREATE FUNCTION [Localization]
(
	@name nvarchar(256),
	@culture nvarchar(8)
)
RETURNS TABLE
AS
RETURN
(
	SELECT TOP 1 ISNULL(ls.Value, @name) Value
	FROM (SELECT 1 Value) t
	OUTER APPLY (
		SELECT TOP 1 le.ID
		FROM [LocalizationEntries] le WITH(NOLOCK)
		WHERE LEFT(@name, 1) = '$'
		    AND le.Name = RIGHT(@name, CASE WHEN len(@name) > 0 THEN len(@name) - 1 ELSE 0 END)
		    AND le.Overridden = 0
	) le
	OUTER APPLY (
		SELECT TOP 1 ls.Value
		FROM [LocalizationStrings] ls WITH(NOLOCK)
		WHERE ls.EntryID = le.ID
		  and ls.Culture = @culture
	) ls
);