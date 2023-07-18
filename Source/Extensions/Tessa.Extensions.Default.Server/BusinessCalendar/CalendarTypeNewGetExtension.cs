using System.Threading.Tasks;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Server.BusinessCalendar
{
    /// <summary>
    /// Автоматически проставляем дату и время для вновь добавленных строк в таблицу исключений.
    /// Это нужно для удобства.
    /// </summary>
    public sealed class CalendarTypeNewGetExtension :
        CardNewGetExtension
    {
        #region Base Overrides

        public override Task AfterRequest(ICardNewExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return Task.CompletedTask;
            }

            BusinessCalendarExtensionHelper.SetupCalendarTypeResponse(context.Response, context.Session);
            return Task.CompletedTask;
        }

        public override Task AfterRequest(ICardGetExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return Task.CompletedTask;
            }

            BusinessCalendarExtensionHelper.SetupCalendarTypeResponse(context.Response, context.Session);
            return Task.CompletedTask;
        }

        #endregion
    }
}
