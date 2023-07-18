using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Client.Cards
{
    public sealed class CalendarTypeStoreExtension :
        CardStoreExtension
    {
        #region Constructors

        public CalendarTypeStoreExtension(ICalendarTypeSettingsCache calendarTypeSettingsCache)
        {
            this.calendarTypeSettingsCache = calendarTypeSettingsCache;
        }

        #endregion

        #region Fields
        
        private readonly ICalendarTypeSettingsCache calendarTypeSettingsCache;

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardStoreExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return;
            }

            if (context.Request.Card.Sections.TryGetValue(BusinessCalendarHelper.CalendarTypesSection, out var calendarTypeSection) &&
                (calendarTypeSection.Fields.ContainsKey("HoursInDay")
                    || calendarTypeSection.Fields.ContainsKey("WorkDaysInWeek")))
            {
                // Если мы поменяли настройки календаря, то стоит сбросить кэш на клиенте.
                // Если поменял настройки другой пользолватель, то мы об этом не узнаем до перезапуска клиента.
                await this.calendarTypeSettingsCache.InvalidateAsync(context.Request.Card.ID, context.CancellationToken);
            }
        }

        #endregion
    }
}
