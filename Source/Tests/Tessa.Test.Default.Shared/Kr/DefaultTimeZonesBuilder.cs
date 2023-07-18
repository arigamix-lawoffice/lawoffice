using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Класс помощник, для установки временных зон по умолчанию.
    /// </summary>
    public class DefaultTimeZonesBuilder :
        CardLifecycleCompanion<DefaultTimeZonesBuilder>
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DefaultTimeZonesBuilder"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        public DefaultTimeZonesBuilder(
            ICardLifecycleCompanionDependencies deps)
            : base(CardHelper.TimeZonesTypeID, CardHelper.TimeZonesTypeName, deps)
        { }

        #endregion

        #region Public Methods

        /// <summary>
        /// Добавляет в БД информацию о указанных временных зонах.
        /// </summary>
        /// <param name="timeZones">Временные зоны для добавления.</param>
        /// <returns>Объект <see cref="DefaultTimeZonesBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public DefaultTimeZonesBuilder WithTimeZones(
            IReadOnlyCollection<(short ZoneID, int OffsetMinutes)> timeZones)
        {
            Check.ArgumentNotNull(timeZones, nameof(timeZones));

            this.FillTimeZonesEnum(timeZones);

            return this;
        }

        /// <summary>
        /// Добавляет в БД информацию о часовом поясе по умолчанию.
        /// </summary>
        /// <returns>Объект <see cref="DefaultTimeZonesBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public DefaultTimeZonesBuilder WithDefaultZone()
        {
            this.AddPendingAction(
                new PendingAction(
                    "Default zone settings",
                    (_, ct) =>
                    {
                        var card = this.GetCardOrThrow();

                        // основная секция
                        var defaultTimeZoneFields = card.Sections[TimeZonesHelper.DefaultTimeZoneSection].Fields;
                        defaultTimeZoneFields["CodeName"] = TimeZonesHelper.DefaultCodeName;
                        defaultTimeZoneFields["UtcOffsetMinutes"] = TimeZonesHelper.DefaultUtcOffsetMinutes;
                        defaultTimeZoneFields["DisplayName"] = TimeZonesHelper.DefaultDisplayName;
                        defaultTimeZoneFields["ShortName"] = TimeZonesHelper.DefaultShortName;
                        defaultTimeZoneFields["IsNegativeOffsetDirection"] = BooleanBoxes.Box(TimeZonesHelper.DefaultIsNegativeOffsetDirection);
                        defaultTimeZoneFields["OffsetTime"] = TimeZonesHelper.DefaultOffsetTime;
                        defaultTimeZoneFields["ZoneID"] = Int32Boxes.Box(TimeZonesHelper.DefaultZoneID);

                        // дополнительная секция
                        var timeZonesSettingsFields = card.Sections[TimeZonesHelper.TimeZonesSettingsSection].Fields;
                        timeZonesSettingsFields["AllowToModify"] = BooleanBoxes.True;

                        return new ValueTask<ValidationResult>(ValidationResult.Empty);
                    }));

            return this;
        }

        #endregion

        #region Private methods

        private void FillTimeZonesEnum(IReadOnlyCollection<(short ZoneID, int OffsetMinutes)> timeZones)
        {
            if (timeZones.Count == 0)
            {
                return;
            }

            this.AddPendingAction(
                new PendingAction(
                    "Fill TimeZones enum.",
                    async (_, ct) =>
                    {
                        var dbScope = this.Dependencies.DbScope;
                        await using (dbScope.Create())
                        {
                            var db = dbScope.Db;

                            var insertIntoTimeZonesCommand =
                                dbScope.BuilderFactory
                                    .InsertInto(TimeZonesHelper.TimeZonesEnumSection,
                                        "ID", "CodeName", "UtcOffsetMinutes", "DisplayName", "ShortName", "IsNegativeOffsetDirection", "OffsetTime")
                                    .Values(p => p.P(
                                        "ID", "CodeName", "UtcOffsetMinutes", "DisplayName", "ShortName", "IsNegativeOffsetDirection", "OffsetTime"))
                                    .Build();

                            foreach ((var zoneID, var offsetMinutes) in timeZones)
                            {
                                var offsetHours = offsetMinutes / 60;
                                var offsetHoursStr = offsetHours.ToString();
                                var isNegativeOffsetDirection = offsetMinutes < 0;
                                var shortName = "UTC";

                                if (!isNegativeOffsetDirection)
                                {
                                    shortName += "+";
                                }

                                shortName += offsetHoursStr;

                                await db.SetCommand(insertIntoTimeZonesCommand,
                                    db.Parameter("ID", zoneID),
                                    db.Parameter("CodeName", $"Test {offsetMinutes} m"),
                                    db.Parameter("UtcOffsetMinutes", offsetMinutes),
                                    db.Parameter("DisplayName", $"Test {offsetHoursStr} Display name"),
                                    db.Parameter("ShortName", shortName),
                                    db.Parameter("IsNegativeOffsetDirection", isNegativeOffsetDirection),
                                    db.Parameter("OffsetTime", CardHelper.DefaultDateTime.Date.Add(new TimeSpan(0, offsetHours, 0, 0))))
                                .LogCommand()
                                .ExecuteNonQueryAsync(ct);
                            }
                        }
                        return ValidationResult.Empty;
                    }));
        }

        #endregion
    }
}
