using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Server.BusinessCalendar
{
    public sealed class CalendarTypeStoreExtension :
        CardStoreExtension
    {
        #region Fields

        private readonly ICalendarTypeSettingsCache calendarTypeSettingsCache;

        #endregion

        #region Constructors

        public CalendarTypeStoreExtension(ICalendarTypeSettingsCache calendarTypeSettingsCache)
        {
            this.calendarTypeSettingsCache = calendarTypeSettingsCache;
        }

        #endregion

        #region Base Overrides

        public override async Task BeforeCommitTransaction(ICardStoreExtensionContext context)
        {
            if (context.Request.Card.Sections.TryGetValue(BusinessCalendarHelper.CalendarTypesSection, out var typeSection))
            {
                if (typeSection.Fields.ContainsKey("HoursInDay")
                    || typeSection.Fields.ContainsKey("WorkDaysInWeek"))
                {
                    await this.calendarTypeSettingsCache.InvalidateAsync(context.Request.Card.ID, context.CancellationToken);
                }
            }
        }

        #endregion
    }
}
