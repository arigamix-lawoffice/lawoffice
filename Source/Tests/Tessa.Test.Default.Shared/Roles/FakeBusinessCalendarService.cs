using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Platform.Data;
using Tessa.Platform.Validation;
using Tessa.Roles;
using TimeZoneInfo = Tessa.BusinessCalendar.TimeZoneInfo;

namespace Tessa.Test.Default.Shared.Roles
{
    /// <summary>
    /// Объект <see cref="IBusinessCalendarService"/> для использования в конструкторе <see cref="TestRoleRepository"/> в тестах.
    /// Реализует только методы <see cref="GetDefaultTimeZoneOffsetAsync"/> и <see cref="GetDefaultCalendarInfoAsync"/>.
    /// </summary>
    public class FakeBusinessCalendarService : IBusinessCalendarService
    {
        #region Fields

        private readonly IDbScope dbScope;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FakeBusinessCalendarService"/>.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        public FakeBusinessCalendarService(IDbScope dbScope)
        {
            this.dbScope = dbScope ?? throw new ArgumentNullException(nameof(dbScope));
        }

        #endregion

        #region IBusinessCalendarService Members

        /// <inheritdoc/>
        public async Task<int?> GetDefaultTimeZoneOffsetAsync(CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                return await db
                    .SetCommand(
                        this.dbScope.BuilderFactory
                            .Select().Top(1)
                            .C("dz", "UtcOffsetMinutes")
                            .From(TimeZonesHelper.DefaultTimeZoneSection, "dz").NoLock()
                            .Limit(1)
                            .Build())
                    .LogCommand()
                    .ExecuteAsync<int?>(cancellationToken);
            }
        }

        /// <inheritdoc/>
        public async Task<CalendarInfo> GetDefaultCalendarInfoAsync(CancellationToken cancellationToken = default)
        {
            Guid? calendarID = null;
            string calendarName = null;
            int? calendarIntID = null;

            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                var builder =
                    this.dbScope.BuilderFactory
                        .Select().Top(1)
                            .C("s", "DefaultCalendarID", "DefaultCalendarName")
                            .C("cs", "CalendarID")
                        .From("ServerInstances", "s").NoLock()
                        .InnerJoin("CalendarSettings", "cs")
                            .On().C("cs", "ID").Equals().C("s", "DefaultCalendarID")
                        .Limit(1);

                await using var reader = await db
                    .SetCommand(
                        builder.Build())
                    .LogCommand()
                    .ExecuteReaderAsync(cancellationToken);

                if (await reader.ReadAsync(cancellationToken))
                {
                    calendarID = reader.GetValue<Guid?>(0);
                    calendarName = reader.GetValue<string>(1);
                    calendarIntID = reader.GetValue<int?>(2);
                }
            }

            return
                calendarID.HasValue
                    ? new CalendarInfo(calendarID.Value, calendarName, calendarIntID)
                    : null;
        }

        /// <summary>
        /// Не реализовано.
        /// </summary>
        public Task<BusinessCalendarTimeType> IsWorkTimeAsync(
            DateTime dateTime,
            Guid calendarCardID,
            TimeSpan? zoneOffset = null,
            CancellationToken cancellationToken = default) => throw new NotImplementedException();

        /// <summary>
        /// Не реализовано.
        /// </summary>
        public Task<long> GetDateDiffAsync(
            DateTime dateTimeStart,
            DateTime dateTimeEnd,
            Guid calendarCardID,
            TimeSpan? zoneOffset = null,
            CancellationToken cancellationToken = default) => throw new NotImplementedException();

        /// <summary>
        /// Не реализовано.
        /// </summary>
        public Task<DateTime> AddWorkingQuantsToDateAsync(
            DateTime dateTime,
            long quants,
            Guid calendarCardID,
            TimeSpan? zoneOffset = null,
            CancellationToken cancellationToken = default) => throw new NotImplementedException();

        /// <summary>
        /// Не реализовано.
        /// </summary>
        public Task<DateTime> GetFirstQuantStartAsync(
            DateTime dateTime,
            int daysOffset,
            Guid calendarCardID,
            TimeSpan? zoneOffset = null,
            CancellationToken cancellationToken = default) => throw new NotImplementedException();

        /// <summary>
        /// Не реализовано.
        /// </summary>
        public Task<DateTime> GetLastQuantEndAsync(
            DateTime dateTime,
            int daysOffset,
            Guid calendarCardID,
            TimeSpan? zoneOffset = null,
            CancellationToken cancellationToken = default) => throw new NotImplementedException();

        /// <summary>
        /// Не реализовано.
        /// </summary>
        public Task<DateTime> AddWorkingDaysToDateAsync(
            DateTime dateTime,
            double daysOffset,
            Guid calendarCardID,
            TimeSpan? zoneOffset = null,
            CancellationToken cancellationToken = default) => throw new NotImplementedException();

        /// <summary>
        /// Не реализовано.
        /// </summary>
        public Task<DateTime> CalendarAddWorkingDaysToDateExactAsync(
            DateTime dateTime,
            int interval,
            Guid calendarCardID,
            TimeSpan? zoneOffset = null,
            CancellationToken cancellationToken = default) => throw new NotImplementedException();

        /// <summary>
        /// Не реализовано.
        /// </summary>
        public Task<TimeZoneInfo> GetRoleTimeZoneInfoAsync(
            Guid roleID,
            CancellationToken cancellationToken = default) => throw new NotImplementedException();

        /// <summary>
        /// Не реализовано.
        /// </summary>
        public Task<CalendarInfo> GetRoleCalendarInfoAsync(
            Guid roleID,
            CancellationToken cancellationToken = default) => throw new NotImplementedException();

        /// <summary>
        /// Не реализовано.
        /// </summary>
        public Task<CalendarInfo> GetCalendarInfoAsync(
            int calendarIntID,
            CancellationToken cancellationToken = default) => throw new NotImplementedException();

        /// <summary>
        /// Не реализовано.
        /// </summary>
        public Task<List<CalendarInfo>> GetAllCalendarInfosAsync(
            CancellationToken cancellationToken = default) => throw new NotImplementedException();

        /// <inheritdoc/>
        public Task RebuildCalendarAsync(
            Guid operationGuid,
            Guid calendarCardID,
            bool rebuildIndexes = false,
            CancellationToken cancellationToken = default) => throw new NotImplementedException();

        /// <summary>
        /// Не реализовано.
        /// </summary>
        public Task<ValidationResult> ValidateCalendarAsync(
            Guid calendarCardID,
            CancellationToken cancellationToken = default) => throw new NotImplementedException();

        #endregion
    }
}
