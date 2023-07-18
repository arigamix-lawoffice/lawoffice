CREATE FUNCTION [FormatAmount]
(
	@amount decimal,
	@formatting nvarchar(32)
)
RETURNS nvarchar(40)
WITH RETURNS NULL ON NULL INPUT
AS
BEGIN
	DECLARE @group_separator nchar(1), @decimal_separator nchar(1);
	SELECT
		@group_separator = [NumberGroupSeparator],
		@decimal_separator = [NumberDecimalSeparator]
	FROM [FormatSettings] WITH (NOLOCK)
	WHERE [Name] = @formatting;

	SET @group_separator = COALESCE(@group_separator, ',');
	SET @decimal_separator = COALESCE(@decimal_separator, '.');

	DECLARE @result nvarchar(40);

	-- 1 000 000,00 (we need culture with spaces as group separator, so use French)
	SET @result = FORMAT(@amount, N'N', N'fr');
	SET @result = REPLACE(@result, N',', @decimal_separator);
	SET @result = REPLACE(@result, N' ', @group_separator);
	SET @result = REPLACE(@result, N' ', char(160));

	RETURN @result;
END;