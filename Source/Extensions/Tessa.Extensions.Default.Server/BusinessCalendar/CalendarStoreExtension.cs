using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Localization;
using Tessa.Platform.Data;
using Tessa.Platform.Operations;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.BusinessCalendar
{
    public sealed class CalendarStoreExtension :
        CardStoreExtension
    {
        #region Fields

        private readonly IBusinessCalendarService businessCalendarService;
        private readonly ICalendarSettingsCache calendarSettingsCache;
        private readonly IOperationLockingStrategy operationLockingStrategy;

        #endregion

        #region Constructors

        public CalendarStoreExtension(
            IBusinessCalendarService businessCalendarService,
            ICalendarSettingsCache calendarSettingsCache,
            IOperationLockingStrategy operationLockingStrategy)
        {
            this.businessCalendarService = businessCalendarService;
            this.calendarSettingsCache = calendarSettingsCache;
            this.operationLockingStrategy = operationLockingStrategy;
        }

        #endregion

        #region Base Overrides

        public override Task BeforeRequest(ICardStoreExtensionContext context)
        {
            Card card;
            StringDictionaryStorage<CardSection> sections;
            if (!context.ValidationResult.IsSuccessful()
                || (card = context.Request.TryGetCard()) == null
                || (sections = card.TryGetSections()) == null
                || !sections.TryGetValue(BusinessCalendarHelper.CalendarSettingsSection, out CardSection settings))
            {
                return Task.CompletedTask;
            }

            Dictionary<string, object> settingsFields = settings.RawFields;

            // в полях задаётся дата и время, но поля редактируются как даты
            // поэтому для даты начала всегда указываем время 00:00:00, а для даты окончания - 23:59:59

            if (settingsFields.ContainsKey("CalendarStart"))
            {
                settingsFields["CalendarStart"] = settingsFields.Get<DateTime>("CalendarStart")
                    .Date;
            }

            if (settingsFields.ContainsKey("CalendarEnd"))
            {
                settingsFields["CalendarEnd"] = settingsFields.Get<DateTime>("CalendarEnd")
                    .Date
                    .AddDays(1.0)
                    .AddSeconds(-1.0);
            }

            return Task.CompletedTask;
        }

        public override async Task AfterRequest(ICardStoreExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return;
            }

            Dictionary<string, object> info;
            if ((info = context.Request.TryGetInfo()) != null &&
                info.TryGet<bool?>(BusinessCalendarHelper.RebuildMarkKey) == true)
            {
                var cardID = context.Request.Card.ID;

                var result = await this.operationLockingStrategy.ExecuteInLockAsync(
                    async (operationID, ct) =>
                    {
                        await this.businessCalendarService.RebuildCalendarAsync(
                            operationID,
                            cardID,
                            true,
                            ct);
                    },
                    BusinessCalendarHelper.LockOptions,
                    LogManager.GetCurrentClassLogger(),
                    true,
                    default);

                if (!result.HasValue)
                {
                    context.Response.ValidationResult.AddError(this, "Failed to create calendar recalculation operation");
                    return;
                }

                context.Response.Info[BusinessCalendarHelper.RebuildOperationIDKey] = result.Value;
            }
        }

        public override async Task BeforeCommitTransaction(ICardStoreExtensionContext context)
        {
            if (context.Request.Card.Sections.TryGetValue(BusinessCalendarHelper.CalendarExclusionsSection, out CardSection exclusionSection))
            {
                if (exclusionSection.Rows.Count > 0)
                {
                    var rowIDList = exclusionSection.Rows.Select(x => x.RowID).ToArray();
                    await using (context.DbScope.Create())
                    {
                        DbManager db = context.DbScope.Db;
                        db
                            .SetCommand(context.DbScope.BuilderFactory
                                .Select()
                                    .C(null, "StartTime", "EndTime")
                                .From(BusinessCalendarHelper.CalendarExclusionsSection).NoLock()
                                .Where()
                                    .C("RowID").InArray(rowIDList, "IDs", out var dpIDs)
                                .Build(), DataParameters.Get(dpIDs))
                            .LogCommand();

                        await using var reader = await db.ExecuteReaderAsync(context.CancellationToken);
                        while (await reader.ReadAsync(context.CancellationToken))
                        {
                            var startTime = reader.GetDateTimeUtc(0);
                            var endTime = reader.GetDateTimeUtc(1);
                            if (startTime > endTime)
                            {
                                context.ValidationResult.AddError(this,
                                    await LocalizationManager.LocalizeAndEscapeFormatAsync("$BusinessCalendar_Exclusions_EndDateMustBeGEThenStartDate", context.CancellationToken),
                                    FormatDateTime(startTime, convertToLocal: false), 
                                    FormatDateTime(endTime, convertToLocal: false));

                                return;
                            }
                        }
                    }
                }
            }

            if (context.Request.Card.Sections.TryGetValue(BusinessCalendarHelper.CalendarNamedRangesSection, out CardSection namedRangesSection))
            {
                if (namedRangesSection.Rows.Count > 0)
                {
                    var rowIDList = namedRangesSection.Rows.Select(x => x.RowID).ToArray();
                    await using (context.DbScope.Create())
                    {
                        DbManager db = context.DbScope.Db;
                        db
                            .SetCommand(context.DbScope.BuilderFactory
                                .Select()
                                    .C(null, "StartTime", "EndTime")
                                .From(BusinessCalendarHelper.CalendarNamedRangesSection).NoLock()
                                .Where()
                                    .C("RowID").InArray(rowIDList, "IDs", out var dpIDs)
                                .Build(), DataParameters.Get(dpIDs))
                            .LogCommand();

                        await using var reader = await db.ExecuteReaderAsync(context.CancellationToken);
                        while (await reader.ReadAsync(context.CancellationToken))
                        {
                            var startTime = reader.GetDateTimeUtc(0);
                            var endTime = reader.GetDateTimeUtc(1);
                            if (startTime > endTime)
                            {
                                context.ValidationResult.AddError(this,
                                    await LocalizationManager.LocalizeAndEscapeFormatAsync("$BusinessCalendar_NamedRanges_EndDateMustBeGEThenStartDate", context.CancellationToken),
                                    FormatDateTime(startTime, convertToLocal: false), 
                                    FormatDateTime(endTime, convertToLocal: false));

                                return;
                            }
                        }
                    }
                }
            }

            if (context.Request.Card.Sections.TryGetValue(BusinessCalendarHelper.CalendarSettingsSection, out var settingsSection))
            {
                await using (context.DbScope.Create())
                {
                    DbManager db = context.DbScope.Db;

                    db
                        .SetCommand(context.DbScope.BuilderFactory
                            .Select()
                                .C(null, "CalendarStart", "CalendarEnd")
                            .From(BusinessCalendarHelper.CalendarSettingsSection).NoLock()
                            .Where()
                                .C("ID").Equals().P("ID")
                            .Build(),
                            db.Parameter("ID", context.Request.Card.ID))
                        .LogCommand();

                    await using var reader = await db.ExecuteReaderAsync(context.CancellationToken);
                    if (await reader.ReadAsync(context.CancellationToken))
                    {
                        var calendarStart = reader.GetDateTimeUtc(0);
                        var calendarEnd = reader.GetDateTimeUtc(1);

                        if (calendarStart >= calendarEnd)
                        {
                            context.ValidationResult.AddError(this,
                                await LocalizationManager.LocalizeAndEscapeFormatAsync("$BusinessCalendar_EndDateMustBeGEThenStartDate", context.CancellationToken),
                                FormatDateTime(calendarStart, convertToLocal: false),
                                FormatDateTime(calendarEnd, convertToLocal: false));
                        }
                    }
                }

                if (settingsSection.Fields.ContainsKey("CalendarTypeID"))
                {
                    await this.calendarSettingsCache.InvalidateAsync(context.Request.Card.ID, context.CancellationToken);
                }
            }
        }

        #endregion
    }
}
