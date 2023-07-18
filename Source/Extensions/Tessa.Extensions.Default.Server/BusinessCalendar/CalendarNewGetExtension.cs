using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards.Extensions;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Server.BusinessCalendar
{
    /// <summary>
    /// Автоматически проставляем дату и время для вновь добавленных строк в таблицу исключений.
    /// Это нужно для удобства.
    /// </summary>
    public sealed class CalendarNewGetExtension :
        CardNewGetExtension
    {
        #region Base Overrides

        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return;
            }

            BusinessCalendarExtensionHelper.SetupCalendarResponse(context.Response, context.Session);

            await using (context.DbScope.Create())
            {
                var db = context.DbScope.Db;

                var builderFactory = context.DbScope.BuilderFactory;

                var newCalendarNumericID = await db
                    .SetCommand(
                        builderFactory
                            .Select()
                                .Coalesce(q => q.Max("cs", "CalendarID").Add().V(1).V(0))
                            .From(BusinessCalendarHelper.CalendarSettingsSection, "cs").NoLock()
                            .Build())
                    .LogCommand()
                    .ExecuteAsync<int>(context.CancellationToken);

                context.Response.Card.Sections[BusinessCalendarHelper.CalendarSettingsSection].Fields["CalendarID"] =
                    newCalendarNumericID;
            }
        }

        public override Task AfterRequest(ICardGetExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return Task.CompletedTask;
            }

            BusinessCalendarExtensionHelper.SetupCalendarResponse(context.Response, context.Session);
            return Task.CompletedTask;
        }

        #endregion
    }
}
