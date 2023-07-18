CREATE FUNCTION "GetTimeIntervalLiteral"
(
	quants bigint
)
RETURNS text
AS $$
DECLARE
	prefix text;
	quants_diff bigint;
	hours_diff bigint;
	day_halfs bigint;
	days_diff bigint;
	half_suffix text;
	div_10_remainder bigint;
	div_100_remainder bigint;
BEGIN
	IF quants < 0 THEN
		prefix = '{$Format_DateDiff_Overdue}: ';  -- просрочено:
	ELSE
		prefix = '';
	END IF;

	quants_diff = abs(quants);

	-- меньше часа
	IF quants_diff <= 3 THEN
		RETURN prefix || '{$Format_DateDiff_LessThanHour}';  -- меньше часа
	END IF;
	
	-- один час
	IF quants_diff <= 5 THEN
		RETURN prefix || '1 {$Format_DateDiff_Hour_Single}';  -- 1 час
	END IF;

	-- переводим кванты в часы с округлением до целого часа
	hours_diff = floor(round(quants_diff / 4.0, 0));

	-- меньше четырёх часов
	IF hours_diff <= 4 THEN
		RETURN prefix || CAST(hours_diff AS varchar) || ' {$Format_DateDiff_Hour_Several}';  -- 4 часа
	END IF;

	-- меньше восьми часов
	IF hours_diff < 8 THEN
		RETURN prefix || CAST(hours_diff AS varchar) || ' {$Format_DateDiff_Hour_Many}';  -- 5 часов
	END IF;

	-- два нижних поля нужны для формирования округленного до 0.5 количества дней: 2 дня, 3.4 дня и т.п.
	-- метод округления прост - умножаем на 2, округляем и делим на 2
	day_halfs = CAST(round(quants_diff / 32.0 * 2, 0) AS int);

	-- 1.5 дня
	IF day_halfs = 3 THEN
		RETURN prefix || '1.5 {$Format_DateDiff_Day_Several}';  -- 1.5 дня
	END IF;

	-- целая часть от деления на 2
	days_diff = day_halfs / 2;

	-- нужно или ненужно дописать 0.5; если остаток от деления на 2 после умножения на 2 равен 1 - значит, нужно
	half_suffix = CASE day_halfs % 2 WHEN 1 THEN '.5' ELSE '' END;

	-- остаток от деления количества дней на 10; нужен для склонения "день, дней" и т.п.
	div_10_remainder = days_diff % 10;

	-- остаток от деления количества дней на 100; нужен для обработки исключений вида 111-119, 1011-1019 и т.п.
	div_100_remainder = days_diff % 100;

	-- 1 день
	IF div_10_remainder = 1 AND div_100_remainder <> 11 THEN
		RETURN prefix || CAST(days_diff AS varchar) || half_suffix || ' {$Format_DateDiff_Day_Single}';  -- 21.5 день
	END IF;

	-- 2-4 дня
	IF div_10_remainder >= 2 AND div_10_remainder <= 4 AND (div_100_remainder < 12 or div_100_remainder > 14) THEN
		RETURN prefix || CAST(days_diff AS varchar) || half_suffix || ' {$Format_DateDiff_Day_Several}';  -- 2.5 дня
	END IF;

	-- 5-9 и 0,  а также 11-19, 1011-1019 дней исключения
	RETURN prefix || CAST(days_diff AS varchar) || half_suffix || ' {$Format_DateDiff_Day_Many}';  -- 12 дней
END; $$
LANGUAGE PLPGSQL;