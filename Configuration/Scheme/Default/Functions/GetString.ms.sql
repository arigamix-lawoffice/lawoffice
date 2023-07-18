CREATE FUNCTION [GetString]
(
	@name nvarchar(256),
	@culture nvarchar(8)
)
RETURNS nvarchar(1024)
WITH RETURNS NULL ON NULL INPUT
AS
BEGIN
	declare @result nvarchar(1024)

	select top 1 @result = ls.Value
	from LocalizationEntries le with(nolock)
	cross apply (
		select top 1 ls.Value
		from LocalizationStrings ls with(nolock)
		where ls.EntryID = le.ID
		  and ls.Culture = @culture
	) ls
	where le.Name = @name
	    and le.Overridden = 0

	return isnull(@result, N'$'+@name)
END;