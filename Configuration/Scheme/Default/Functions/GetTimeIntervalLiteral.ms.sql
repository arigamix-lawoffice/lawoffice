create function dbo.GetTimeIntervalLiteral(@quants int) returns nvarchar(200)
as
begin

declare @prefix nvarchar(28);
if @quants < 0 
    set @prefix = N'{$Format_DateDiff_Overdue}: ';  -- просрочено:
else
    set @prefix = N'';

declare @quants_diff int; set @quants_diff = abs(@quants);

-- меньше часа
if @quants_diff <= 3
    return @prefix + N'{$Format_DateDiff_LessThanHour}';  -- меньше часа
	
-- один час
if @quants_diff <= 5
    return @prefix + N'1 {$Format_DateDiff_Hour_Single}';  -- 1 час

-- переводим кванты в часы с округлением до целого часа
declare @qdiff_hours int; set @qdiff_hours = floor(round(@quants_diff / 4.0, 0));

-- меньше четырёх часов
if @qdiff_hours <= 4 
    return @prefix + cast(@qdiff_hours as nvarchar) + N' {$Format_DateDiff_Hour_Several}';  -- 4 часа

-- меньше восьми часов
if @qdiff_hours < 8
    return @prefix + cast(@qdiff_hours as nvarchar) + N' {$Format_DateDiff_Hour_Many}';  -- 5 часов


-- два нижних поля нужны для формирования округленного до 0.5 количества дней: 2 дня, 3.4 дня и т.п.
-- метод округления прост - умножаем на 2, округляем и делим на 2
declare @n int; set @n = cast(round(@quants_diff / 32.0 * 2, 0) as int);

-- 1.5 дня
if @n = 3
    return @prefix + N'1.5 {$Format_DateDiff_Day_Several}';  -- 1.5 дня

-- целая часть от деления на 2
declare @qdiff_days int; set @qdiff_days = @n / 2;

-- нужно или ненужно дописать 0.5; если остаток от деления на 2 после умножения на 2 равен 1 - значит, нужно
declare @qdiff_05 nvarchar(2); set @qdiff_05 = case @n % 2 when 1 then N'.5' else N'' end;

-- остаток от деления количества дней на 10; нужен для склонения "день, дней" и т.п.
declare @qdiff_10_remainder int; set @qdiff_10_remainder = @qdiff_days % 10;

-- остаток от деления количества дней на 100; нужен для обработки исключений вида 111-119, 1011-1019 и т.п.
declare @qdiff_100_remainder int; set @qdiff_100_remainder = @qdiff_days % 100;

-- 1 день
if @qdiff_10_remainder = 1 and @qdiff_100_remainder <> 11
    return @prefix + cast(@qdiff_days as nvarchar) + @qdiff_05 + N' {$Format_DateDiff_Day_Single}';  -- 21.5 день

-- 2-4 дня
if @qdiff_10_remainder >= 2 and @qdiff_10_remainder <= 4 and (@qdiff_100_remainder < 12 or @qdiff_100_remainder > 14)
    return @prefix + cast(@qdiff_days as nvarchar) + @qdiff_05 + N' {$Format_DateDiff_Day_Several}';  -- 2.5 дня

-- 5-9 и 0,  а также 11-19, 1011-1019 дней исключения
return @prefix + cast(@qdiff_days as nvarchar) + @qdiff_05 + N' {$Format_DateDiff_Day_Many}';  -- 12 дней

end;