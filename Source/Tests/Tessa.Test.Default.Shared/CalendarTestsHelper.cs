using System;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет значения календарей, типов календарей и т.п. информацию необходимую для использования в тестах.
    /// </summary>
    public static class CalendarTestsHelper
    {
        /// <summary>
        /// Идентификатор календаря по умолчанию: {64b9230d-5422-466d-bb19-f9882bb87b78}.
        /// </summary>
        public static readonly Guid DefaultCalendarID =
            new Guid(0x64b9230d, 0x5422, 0x466d, 0xbb, 0x19, 0xf9, 0x88, 0x2b, 0xb8, 0x7b, 0x78);

        /// <summary>
        /// Название типа календаря по умолчанию <see cref="DefaultWorkWeekCalendarTypeID"/>.
        /// </summary>
        public static readonly string DefaultCalendarName = "$Calendars_DefaultCalendar";

        /// <summary>
        /// Идентификатор типа календаря по умолчанию "Тип календаря: Рабочая неделя".
        /// </summary>
        public static readonly Guid DefaultWorkWeekCalendarTypeID =
            new Guid(0xd4e28b83, 0x0ce6, 0x4b11, 0xaf, 0x63, 0xbe, 0x07, 0xd1, 0x58, 0x6a, 0x06);

        /// <summary>
        /// Название типа календаря по умолчанию "Рабочая неделя".
        /// </summary>
        public static readonly string DefaultWorkWeekCalendarTypeCaption = "$Calendars_WorkWeekCalendarType";

        /// <summary>
        /// Название способа расчёта календаря по умолчанию "Рабочая неделя".
        /// </summary>
        public static readonly string DefaultWorkWeekCalendarCalcTypeName = "$Calendars_WorkWeekCalendarCalcType";
    }
}