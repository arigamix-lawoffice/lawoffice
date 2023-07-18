using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.Wf
{
    public class WfResolutionCheckSafeLimitStoreTaskExtension : CardStoreTaskExtension
    {
        #region Base Overrides

        public override async Task StoreTaskBeforeCommitTransaction(ICardStoreTaskExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            if (context.Task.Info.TryGetValue(WfHelper.CheckSafeLimitKey, out var objSafeChildResolutionTimeLimit) &&
                context.Task.Info.TryGetValue(WfHelper.ParentPlannedKey, out var objParentPlanned) &&
                context.Task.Info.TryGetValue(WfHelper.StoreDateTimeKey, out var objStoreDateTime))
            {
                var safeChildResolutionTimeLimit = (double)objSafeChildResolutionTimeLimit;
                var parentPlanned = (DateTime?)objParentPlanned;
                var storeDateTime = (DateTime)objStoreDateTime;

                TimeSpan clientUtcOffset = context.Session.ClientUtcOffset;

                DateTime utcParentPlanned = parentPlanned.Value.ToUniversalTime();
                DateTime utcParentPlannedOrCurrent = utcParentPlanned >= storeDateTime
                    ? utcParentPlanned
                    : storeDateTime;

                int? taskRoleUtcOffset = null;
                int? calendarIntID = null;
                double? hoursInDay = null;

                var mainRole = await CardComponentHelper.TryGetMasterTaskAssignedRoleAsync(
                    context.Task,
                    context.ValidationResult,
                    typeof(WfResolutionCheckSafeLimitStoreTaskExtension),
                    context.DbScope.Db,
                    context.DbScope.BuilderFactory,
                    cancellationToken: context.CancellationToken);

                if (mainRole is not null)
                {
                    // Получаем смещение временной зоны роли, на которую назначено задание
                    await using (context.DbScope.Create())
                    {
                        var db = context.DbScope.Db;

                        await using (DbDataReader reader = await db
                             .SetCommand(
                                 context.DbScope.BuilderFactory
                                     .Select().Top(1)
                                         .Coalesce(q =>
                                             q.C("r", "TimeZoneUtcOffsetMinutes").C("d", "UtcOffsetMinutes"))
                                         .As("TimeZoneUtcOffsetMinutes")
                                         .C("cs", "CalendarID").As("CalendarIntID")
                                         .C("ct", "HoursInDay").As("HoursInDay")
                                     .From("Roles", "r").NoLock()
                                     .InnerJoin("CalendarSettings", "cs").NoLock()
                                        .On().C("r", "CalendarID").Equals().C("cs", "ID")
                                     .InnerJoin("CalendarTypes", "ct").NoLock()
                                        .On().C("ct", "ID").Equals().C("cs", "CalendarTypeID")
                                     .LeftJoinLateral(d => d
                                         .Select().Top(1)
                                            .C("dz", "UtcOffsetMinutes")
                                         .From(TimeZonesHelper.DefaultTimeZoneSection, "dz").NoLock()
                                         .Limit(1), "d")
                                     .Where()
                                        .C("r", "ID").Equals().P("TaskRoleID")
                                     .Limit(1)
                                     .Build(),
                                 db.Parameter("TaskRoleID", mainRole.RoleID, DataType.Guid))
                             .LogCommand().ExecuteReaderAsync(context.CancellationToken))
                        {
                            while (await reader.ReadAsync(context.CancellationToken))
                            {
                                taskRoleUtcOffset = reader.GetNullableInt32(0);
                                calendarIntID = reader.GetNullableInt32(1);
                                hoursInDay = reader.GetNullableDouble(2);
                            }
                        }
                    }
                }

                var offset = TimeSpan.FromMinutes(taskRoleUtcOffset ?? 0);

                DateTime? safeLimit = await this.TryGetDateTimeFromCalendarAsync(
                    context.DbScope,
                    utcParentPlannedOrCurrent,
                    offset,
                    calendarIntID,
                    hoursInDay,
                    safeChildResolutionTimeLimit,
                    context.ValidationResult,
                    clientUtcOffset,
                    cancellationToken: context.CancellationToken);

                if (!safeLimit.HasValue)
                {
                    context.ValidationResult.AddError(this, "$WfResolution_Error_ChildResolutionSafeLimitIsNull");
                }
                else if (context.Task.Planned > safeLimit.Value)
                {
                    context.ValidationResult.AddError(this,
                        "$WfResolution_Error_ChildResolutionCantBePlannedAfterParent",
                        FormattingHelper.FormatDateTimeWithoutSeconds(
                            context.Task.Planned + clientUtcOffset,
                            convertToLocal: false),
                        FormattingHelper.FormatDateTimeWithoutSeconds(
                            safeLimit.Value + clientUtcOffset,
                            convertToLocal: false),
                        FormattingHelper.FormatDateTimeWithoutSeconds(
                            utcParentPlanned + clientUtcOffset,
                            convertToLocal: false));
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Возвращает дату/время в UTC, полученные по бизнес-календарю, если к <paramref name="initialDateTime"/>
        /// прибавить количество бизнес-дней, указанных в <paramref name="duration"/>,
        /// или <c>null</c>, если календарь не рассчитан на этом диапазоне.
        /// </summary>
        /// <param name="dbScope">IDbScope</param>
        /// <param name="initialDateTime">Дата/время отсчёта. Переводится в UTC, если задана как локальное время.</param>
        /// <param name="offset">Смещение роли задания.</param>
        /// <param name="calendarIntID">Целочисленный идентификатор календаря.</param>
        /// <param name="hoursInDay">Колличество рабочих часов в дне у календаря задания.</param>
        /// <param name="plannedWorkingDays">
        /// Длительность в бизнес-днях, которую надо прибавить к дате/времени <paramref name="initialDateTime"/>.
        /// Может быть отрицательным числом или нулём, только если параметр <paramref name="positiveDurationOnly"/>
        /// равен <c>false</c>.
        /// </param>
        /// <param name="validationResult">IValidationResultBuilder</param>
        /// <param name="clientUtcOffset">Клиентское смещение времени</param>
        /// <param name="positiveDurationOnly">Признак</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Дата/время в UTC, полученная по бизнес-календарю в результате расчётов,
        /// или <c>null</c>, если календарь не рассчитан на этом диапазоне.
        /// </returns>
        protected async Task<DateTime?> TryGetDateTimeFromCalendarAsync(
            IDbScope dbScope,
            DateTime initialDateTime,
            TimeSpan offset,
            int? calendarIntID,
            double? hoursInDay,
            double plannedWorkingDays,
            IValidationResultBuilder validationResult,
            TimeSpan clientUtcOffset,
            bool positiveDurationOnly = false,
            CancellationToken cancellationToken = default)
        {
            if (positiveDurationOnly && plannedWorkingDays <= 0)
            {
                validationResult.AddError(this,
                    "$WfResolution_Error_TaskDurationCantBeZeroOrNegative",
                    FormattingHelper.FormatDoubleAsDecimal(plannedWorkingDays, 1));

                return null;
            }

            if (plannedWorkingDays.Equals(0.0))
            {
                return initialDateTime.ToUniversalTime();
            }

            await using (dbScope.Create())
            {
                var db = dbScope.Db;

                DateTime? result = await db
                    .SetCommand(
                        dbScope.BuilderFactory
                            .Select()
                                .Function("CalendarGetPlannedByWorkingDays", p => p.P("CalendarID", "HoursInDay", "InitialDateTime", "PlannedWorkingDays"))
                            .Build(),
                        db.Parameter("InitialDateTime", initialDateTime.ToUniversalTime() + offset, DataType.DateTime),
                        db.Parameter("CalendarID", calendarIntID, DataType.Int32),
                        db.Parameter("HoursInDay", hoursInDay, DataType.Double),
                        db.Parameter("PlannedWorkingDays", plannedWorkingDays, DataType.Double))
                    .LogCommand()
                    .ExecuteAsync<DateTime?>(cancellationToken);

                if (!result.HasValue)
                {
                    validationResult.AddError(this,
                        "$WfResolution_Error_CantGetDateTimeFromCalendar",
                        FormattingHelper.FormatDateTimeWithoutSeconds(
                            initialDateTime + clientUtcOffset,
                            convertToLocal: false),
                        FormattingHelper.FormatDateTimeWithoutSeconds(initialDateTime, convertToLocal: false),
                        FormattingHelper.FormatDoubleAsDecimal(plannedWorkingDays, 1));
                }

                return result - offset;
            }
        }

        #endregion
    }
}
