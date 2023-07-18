using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Formatting;
using Tessa.Platform.Storage;
using Tessa.Roles;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Предоставляет базовую реализацию форматтера типа этапа.
    /// </summary>
    public abstract class StageTypeFormatterBase : IStageTypeFormatter
    {
        #region Constants And Static Fields

        /// <summary>
        /// Максимальная длина значения параметра выводимая при форматировании на клиенте.
        /// </summary>
        public const int DefaultSettingMax = 30;

        #endregion

        #region IStageTypeFormatter Members

        /// <inheritdoc />
        public virtual ValueTask FormatClientAsync(
            IStageTypeFormatterContext context)
        {
            var containsTimeLimit = context.StageRow.TryGetValue(KrStages.TimeLimit, out var timeLimit);
            var containsPlanned = context.StageRow.TryGetValue(KrStages.Planned, out var planned);
            if (containsTimeLimit
                || containsPlanned)
            {
                DefaultDateFormatting(planned as DateTime?, timeLimit as double?, context);
            }

            if (context.StageRow.TryGetValue(KrSinglePerformerVirtual.PerformerName,
                    out var performerNameObj)
                && performerNameObj is string performerName
                && !string.IsNullOrWhiteSpace(performerName))
            {
                DefaultSinglePerformerFormatting(performerName, context);
            }
            else if (context.Card.Sections.TryGetValue(KrPerformersVirtual.Synthetic, out var appSec))
            {
                IEnumerable<string> FormatPerformers(
                    ListStorage<CardRow> rows)
                {
                    foreach (var row in rows)
                    {
                        if (row.State != CardRowState.Deleted
                            && row.TryGet<Guid?>(KrPerformersVirtual.StageRowID)?.Equals(context.StageRow.RowID) == true
                            && row.TryGetValue(KrPerformersVirtual.PerformerName, out var pnObj)
                            && pnObj is string name)
                        {
                            yield return RoleHelper.EscapeRoleNameForLocalization(name);
                        }
                    }
                }

                context.DisplayParticipants = string.Join(
                    Environment.NewLine,
                    FormatPerformers(appSec.Rows));
            }

            return new ValueTask();
        }

        /// <inheritdoc />
        public virtual ValueTask FormatServerAsync(
            IStageTypeFormatterContext context)
        {
            var containsTimeLimit = context.StageRow.TryGetValue(KrStages.TimeLimit, out var timeLimit);
            var containsPlanned = context.StageRow.TryGetValue(KrStages.Planned, out var planned);
            if (containsTimeLimit
                || containsPlanned)
            {
                DefaultDateFormatting(planned as DateTime?, timeLimit as double?, context);
            }

            if (context.Settings.TryGetValue(KrSinglePerformerVirtual.PerformerName, out var performerNameObj)
                && performerNameObj is string performerName
                && !string.IsNullOrWhiteSpace(performerName))
            {
                DefaultSinglePerformerFormatting(performerName, context);
            }
            else if (context.Settings.TryGetValue(KrPerformersVirtual.Synthetic, out var performers)
                && performers is IList performersList)
            {
                static IEnumerable<string> FormatPerformers(
                    IList rows)
                {
                    foreach (var row in rows)
                    {
                        if (row is IDictionary<string, object> rowStorage
                            && rowStorage.TryGetValue(KrPerformersVirtual.PerformerName, out var pnObj)
                            && pnObj is string name)
                        {
                            yield return RoleHelper.EscapeRoleNameForLocalization(name);
                        }
                    }
                }

                context.DisplayParticipants = string.Join(
                    Environment.NewLine,
                    FormatPerformers(performersList));
            }

            return new ValueTask();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Задаёт отображаемый строк исполнения.
        /// </summary>
        /// <param name="planned">Дата выполнения.</param>
        /// <param name="timeLimit">Срок, рабочие дни.</param>
        /// <param name="context">Контекст форматтера этапа.</param>
        protected static void DefaultDateFormatting(
            DateTime? planned,
            double? timeLimit,
            IStageTypeFormatterContext context)
        {
            if (planned.HasValue)
            {
                context.DisplayTimeLimit =
                    FormattingHelper.FormatDateTimeWithoutSeconds(planned.Value.ToUniversalTime() + context.Session.ClientUtcOffset, convertToLocal: false)
                    + " UTC" + FormattingHelper.FormatUtcOffset(context.Session.ClientUtcOffset);
            }
            else if (timeLimit.HasValue)
            {
                context.DisplayTimeLimit = timeLimit.Value.ToString(CultureInfo.InvariantCulture)
                    + LocalizationManager.GetString("KrProcess_WorkingDaysSuffix");
            }
            else
            {
                context.DisplayTimeLimit = string.Empty;
            }
        }

        /// <summary>
        /// Задаёт отображаемый список участников состоящий из одного указанного исполнителя.
        /// </summary>
        /// <param name="performerName">Имя исполнителя.</param>
        /// <param name="context">Контекст форматтера этапа.</param>
        protected static void DefaultSinglePerformerFormatting(
            string performerName,
            IStageTypeFormatterContext context)
            => context.DisplayParticipants = performerName;

        /// <summary>
        /// Добавляет форматированную строку с описанием настройки этапа в <paramref name="builder"/>.
        /// </summary>
        /// <param name="builder">Конструктор строки.</param>
        /// <param name="value">Значение параметра.</param>
        /// <param name="caption">Заголовок (название) добавляемой колонки. Может быть строкой локализации вида "$LocalizationString". Может быть не задано.</param>
        /// <param name="localizable">Необходимо локализовать значение настройки.</param>
        /// <param name="canBeWithoutValue">Значение <see langword="true"/>, если требуется добавить заголовок (название) параметра, если значение параметра равно <see langword="null"/>, <see cref="string.Empty"/> или состоит из одних символов пробела.</param>
        /// <param name="limit">
        /// Максимальная длина строки со значением параметра.
        /// <c>-1</c>, если значение не ограничено.
        /// </param>
        /// <param name="appendNewLine">Значение <see langword="true"/>, если перед добавляемой строкой должен быть добавлен знак завершения строки по умолчанию, иначе - <see langword="false"/>. Перевод строки добавляется только, если <paramref name="builder"/> содержит данные.</param>
        protected static void AppendString(
            StringBuilder builder,
            string value,
            string caption,
            bool localizable = false,
            bool canBeWithoutValue = false,
            int limit = -1,
            bool appendNewLine = true)
        {
            value = value?.Trim();
            var valueIsNullOrEmpty = string.IsNullOrEmpty(value);
            if (!canBeWithoutValue
                && valueIsNullOrEmpty)
            {
                return;
            }

            var builderLength = builder.Length;
            var addedNewLine = false;

            if (caption is not null)
            {
                caption = caption.Trim();

                if (caption.Length > 0)
                {
                    if (appendNewLine && builderLength > 0)
                    {
                        builder.AppendLine();
                        addedNewLine = true;
                    }

                    AppendExtendedLocalization(builder, caption);

                    if (!valueIsNullOrEmpty)
                    {
                        builder.Append(": ");
                    }
                }
            }

            if (!valueIsNullOrEmpty)
            {
                if (appendNewLine && !addedNewLine && builderLength > 0)
                {
                    builder.AppendLine();
                }

                if (localizable)
                {
                    localizable = value.StartsWith('$');
                }

                var isLimitedValue = false;

                if (limit > -1 && value.Length > limit)
                {
                    if (localizable)
                    {
                        value = LocalizationManager.Localize(value);
                    }

                    value = value.Limit(limit);
                    isLimitedValue = true;
                }

                if (localizable && !isLimitedValue)
                {
                    builder.Append('{');
                }

                builder.Append(value);

                if (localizable && !isLimitedValue)
                {
                    builder.Append('}');
                }
            }
        }

        /// <summary>
        /// Преобразует указанную строку в формат расширенной локализации, если она является строкой локализации.
        /// </summary>
        /// <param name="str">Строка, которая должна быть преобразована в формат расширенной локализации, если она является строкой локализации. Может иметь значение <see langword="null"/>.</param>
        /// <returns>Строка преобразована в формат расширенной локализации, если она является строкой локализации, иначе исходная строка без изменений.</returns>
        protected static string ToExtendedLocalization(
            string str)
        {
            return !string.IsNullOrEmpty(str) && str.StartsWith('$')
                ? "{" + str + "}"
                : str;
        }

        /// <summary>
        /// Добавляет указанную строку в формате расширенной локализации, если она является строкой локализации.
        /// </summary>
        /// <param name="sb">Объект, используемый для построения строки.</param>
        /// <param name="str">Строка, которая должна быть добавлена в формате расширенной локализации, если она является строкой локализации. Если строка не является строкой локализации, то она добавляется без изменений. Может иметь значение <see langword="null"/>.</param>
        protected static void AppendExtendedLocalization(
            StringBuilder sb,
            string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.StartsWith('$'))
                {
                    sb
                        .Append('{')
                        .Append(str)
                        .Append('}');
                }
                else
                {
                    sb.Append(str);
                }
            }
        }

        #endregion
    }
}