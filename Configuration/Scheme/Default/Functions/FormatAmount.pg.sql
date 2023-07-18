CREATE FUNCTION "FormatAmount"
(
	amount numeric,
	formatting text
)
RETURNS text
RETURNS NULL ON NULL INPUT
AS $$
DECLARE
	"group_separator" char;
	"decimal_separator" char;
	"result" text;
BEGIN
	SELECT "NumberGroupSeparator"::char, "NumberDecimalSeparator"::char
	INTO "group_separator", "decimal_separator"
	FROM "FormatSettings"
	WHERE "Name" = "formatting";

	"group_separator" = COALESCE("group_separator", ',');
	"decimal_separator" = COALESCE("decimal_separator", '.');

	-- 1 000 000,00 (we need culture with spaces as group separator)
	"result" = TO_CHAR(amount, '999 999 999 999 999 999 990.00');
	"result" = TRIM(LEADING ' ' FROM "result");
	"result" = REPLACE("result", '.', "decimal_separator");
	"result" = REPLACE("result", ' ', "group_separator");
	"result" = REPLACE("result", ' ', chr(160));

	RETURN "result";
END; $$
LANGUAGE PLPGSQL;