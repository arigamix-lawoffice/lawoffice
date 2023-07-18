using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Client.Cards
{
    public sealed class CalendarStoreExtension :
        CardStoreExtension
    {
        #region Constructors

        public CalendarStoreExtension(ICalendarSettingsCache calendarSettingsCache)
        {
            this.calendarSettingsCache = calendarSettingsCache;
        }

        #endregion

        #region Fields
        
        private readonly ICalendarSettingsCache calendarSettingsCache;

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardStoreExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return;
            }

            if (context.Request.Card.Sections.TryGetValue(BusinessCalendarHelper.CalendarSettingsSection, out var settingsSection) &&
                settingsSection.Fields.ContainsKey("CalendarTypeID"))
            {
                // Если мы поменяли настройки календаря, то стоит сбросить кэш на клиенте.
                // Если поменял настройки другой пользолватель, то мы об этом не узнаем до перезапуска клиента.
                await this.calendarSettingsCache.InvalidateAsync(context.Request.Card.ID, context.CancellationToken);
            }
        }

        #endregion
    }
}
