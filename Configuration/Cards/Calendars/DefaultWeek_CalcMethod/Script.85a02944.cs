var calendarSettings = new Tessa.BusinessCalendar.WorkWeekCalendarSettings();

foreach (var row in context.TypeCardObject.Sections["CalendarTypeWeekDays"].Rows)
{
	var dayNumber = row.Fields.Get<int>("Number");
	var isNotWorkingDay = row.Fields.Get<bool>("IsNotWorkingDay");

	var currentDay = isNotWorkingDay
		? new Tessa.BusinessCalendar.CalendarDay()
		: new Tessa.BusinessCalendar.CalendarDay(
			row.Fields.Get<DateTime?>("WorkingDayStart"),
			row.Fields.Get<DateTime?>("WorkingDayEnd"),
			row.Fields.Get<DateTime?>("LunchStart"),
			row.Fields.Get<DateTime?>("LunchEnd"));

	switch (dayNumber)
	{
		case 0:
			calendarSettings.Days[DayOfWeek.Monday] = currentDay;
			break;
		case 1:
			calendarSettings.Days[DayOfWeek.Tuesday] = currentDay;
			break;
		case 2:
			calendarSettings.Days[DayOfWeek.Wednesday] = currentDay;
			break;
		case 3:
			calendarSettings.Days[DayOfWeek.Thursday] = currentDay;
			break;
		case 4:
			calendarSettings.Days[DayOfWeek.Friday] = currentDay;
			break;
		case 5:
			calendarSettings.Days[DayOfWeek.Saturday] = currentDay;
			break;
		case 6:
			calendarSettings.Days[DayOfWeek.Sunday] = currentDay;
			break;
	}
}

var currentDate = context.StartDate;

while (currentDate.Date != context.EndDate.Date)
{
	var daySettings = calendarSettings.Days[currentDate.Date.DayOfWeek];

	if (daySettings.IsNotWorkingDay)
	{
		context.Intervals.Add(
			new BusinessCalendarInterval(
				true,
				currentDate.Date,
				currentDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59)));
	}
	else
	{
		// День начинается с обеда или обед до начала рабочего дня. Или обеда вообще нет.
		if (daySettings.LunchStart is null || 
			daySettings.LunchEnd is null ||
			daySettings.LunchStart <= daySettings.WorkingDayStart)
		{
			// Обед заканчивается до начала дня или обед предваряет день. Или обеда вообще нет.
			if (daySettings.LunchStart is null ||
				daySettings.LunchEnd is null || 
				daySettings.LunchEnd.Value.TimeOfDay <= daySettings.WorkingDayStart.Value.TimeOfDay)
			{
				// Добавим нерабочий интервал от начала дня до начала рабочего времени
				context.Intervals.Add(
					new BusinessCalendarInterval(
						true,
						currentDate.Date,
						currentDate.Date.AddHours(daySettings.WorkingDayStart.Value.Hour).AddMinutes(daySettings.WorkingDayStart.Value.Minute)));
				// Добавим рабочий интервал на весь день 
				context.Intervals.Add(
					new BusinessCalendarInterval(
						false,
						currentDate.Date.AddHours(daySettings.WorkingDayStart.Value.Hour).AddMinutes(daySettings.WorkingDayStart.Value.Minute),
						currentDate.Date.AddHours(daySettings.WorkingDayEnd.Value.Hour).AddMinutes(daySettings.WorkingDayEnd.Value.Minute)));

			}
			// Обед залезает на рабочий день
			else
			{
				context.Intervals.Add(
					new BusinessCalendarInterval(
						true,
						currentDate.Date,
						currentDate.Date.AddHours(daySettings.LunchEnd.Value.Hour).AddMinutes(daySettings.LunchEnd.Value.Minute)));

				// Добавим рабочий интервал на весь день 
				context.Intervals.Add(
					new BusinessCalendarInterval(
						false,
						currentDate.Date.AddHours(daySettings.LunchEnd.Value.Hour).AddMinutes(daySettings.LunchEnd.Value.Minute),
						currentDate.Date.AddHours(daySettings.WorkingDayEnd.Value.Hour).AddMinutes(daySettings.WorkingDayEnd.Value.Minute)));
			}

			// Добавим нерабочий интервал до конца дня
			context.Intervals.Add(
				new BusinessCalendarInterval(
					true,
					currentDate.Date.AddHours(daySettings.WorkingDayEnd.Value.Hour).AddMinutes(daySettings.WorkingDayEnd.Value.Minute),
					currentDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59)));
		}
		// Обед внутри рабочего дня или после него
		else
		{
			// Обед начинаетя до конца дня
			if (daySettings.LunchStart.Value.TimeOfDay <= daySettings.WorkingDayEnd.Value.TimeOfDay)
			{
				// Добавим нерабочий интервал от начала дня до начала рабочего времени
				context.Intervals.Add(
					new BusinessCalendarInterval(
						true,
						currentDate.Date,
						currentDate.Date.AddHours(daySettings.WorkingDayStart.Value.Hour).AddMinutes(daySettings.WorkingDayStart.Value.Minute)));
				// Добавим рабочий интервал на весь день до обеда
				context.Intervals.Add(
					new BusinessCalendarInterval(
						false,
						currentDate.Date.AddHours(daySettings.WorkingDayStart.Value.Hour).AddMinutes(daySettings.WorkingDayStart.Value.Minute),
						currentDate.Date.AddHours(daySettings.LunchStart.Value.Hour).AddMinutes(daySettings.LunchStart.Value.Minute)));

				// Обед внутри рабочего дня полностью
				if (daySettings.LunchEnd.Value.TimeOfDay >= daySettings.WorkingDayEnd.Value.TimeOfDay)
				{
					// Добавим нерабочий интервал до конца дня
					context.Intervals.Add(
						new BusinessCalendarInterval(
							true,
							currentDate.Date.AddHours(daySettings.LunchStart.Value.Hour).AddMinutes(daySettings.LunchStart.Value.Minute),
							currentDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59)));
				}
				// После обеда ещё работаем
				else
				{
					// Добавим нерабочий интервал на время обеда
					context.Intervals.Add(
						new BusinessCalendarInterval(
							true,
							currentDate.Date.AddHours(daySettings.LunchStart.Value.Hour).AddMinutes(daySettings.LunchStart.Value.Minute),
							currentDate.Date.AddHours(daySettings.LunchEnd.Value.Hour).AddMinutes(daySettings.LunchEnd.Value.Minute)));
					// Добавим рабочий интервал после обеда
					context.Intervals.Add(
						new BusinessCalendarInterval(
							false,
							currentDate.Date.AddHours(daySettings.LunchEnd.Value.Hour).AddMinutes(daySettings.LunchEnd.Value.Minute),
							currentDate.Date.AddHours(daySettings.WorkingDayEnd.Value.Hour).AddMinutes(daySettings.WorkingDayEnd.Value.Minute)));
					// Добавим нерабочий интервал до конца рабочего дня
					context.Intervals.Add(
						new BusinessCalendarInterval(
							true,
							currentDate.Date.AddHours(daySettings.WorkingDayEnd.Value.Hour).AddMinutes(daySettings.WorkingDayEnd.Value.Minute),
							currentDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59)));
				}
			}
		}
	}
	
	currentDate = currentDate.AddDays(1);
}

// Объединим однотипные исключения
context.IntervalsProcessor.ArrangeIntervals(context.Intervals);

// Исключения из карточки Типа календаря
IEnumerable<IBusinessCalendarInterval> calendarTypeExclusions = null;

var calendarTypeExclusionsRows = context.TypeCardObject.Sections["CalendarTypeExclusions"].Rows;

if (calendarTypeExclusionsRows.Count > 0)
{
	calendarTypeExclusions =
		calendarTypeExclusionsRows.Select(p =>
			new BusinessCalendarInterval(
				p.Fields.Get<bool>("IsNotWorkingTime"),
				p.Fields.Get<DateTime>("StartTime"),
				p.Fields.Get<DateTime>("EndTime"),
				p.Fields.Get<string>("Caption")));
}

// Перенесём исключения из типа календаря с интервалами из скрипта
if (calendarTypeExclusions != null)
{
	context.IntervalsProcessor.MergeIntervals(context.Intervals, calendarTypeExclusions);
}