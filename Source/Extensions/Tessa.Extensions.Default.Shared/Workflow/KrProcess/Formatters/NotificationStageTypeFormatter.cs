using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.NotificationDescriptor"/>.
    /// </summary>
    public sealed class NotificationStageTypeFormatter : StageTypeFormatterBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override async ValueTask FormatClientAsync(IStageTypeFormatterContext context)
        {
            await base.FormatClientAsync(context);

            var excludeDeputies = context.StageRow.Fields.Get<bool>(KrNotificationSettingVirtual.ExcludeDeputies);
            var excludeSubscribers = context.StageRow.Fields.Get<bool>(KrNotificationSettingVirtual.ExcludeSubscribers);
            var optionalRecipients = context.Card.Sections.TryGet(KrNotificationOptionalRecipientsVirtual.Synthetic)?
                 .Rows
                 .Where(x => x.State != Tessa.Cards.CardRowState.Deleted
                    && x.TryGet<Guid>(KrConstants.StageRowIDReferenceToOwner) == context.StageRow.RowID)
                 .Select(x => x.TryGet<string>(KrNotificationOptionalRecipientsVirtual.RoleName))
                 .ToArray();

            context.DisplayTimeLimit = string.Empty;
            context.DisplaySettings = GetDisplaySettings(excludeDeputies, excludeSubscribers, optionalRecipients);
        }

        /// <inheritdoc/>
        public override async ValueTask FormatServerAsync(IStageTypeFormatterContext context)
        {
            await base.FormatServerAsync(context);

            var excludeDeputies = context.Settings.TryGet<bool>(KrNotificationSettingVirtual.ExcludeDeputies);
            var excludeSubscribers = context.Settings.TryGet<bool>(KrNotificationSettingVirtual.ExcludeSubscribers);
            var optionalRecipients = context.Settings.TryGet<IList>(KrNotificationOptionalRecipientsVirtual.Synthetic)?
                .Cast<Dictionary<string, object>>()
                .Select(x => x.TryGet<string>(KrNotificationOptionalRecipientsVirtual.RoleName))
                .ToArray();

            context.DisplayTimeLimit = string.Empty;
            context.DisplaySettings = GetDisplaySettings(excludeDeputies, excludeSubscribers, optionalRecipients);
        }

        #endregion

        #region Private Methods

        private static string GetDisplaySettings(bool excludeDeputies, bool excludeSubscribers, string[] optionalRecipients)
        {
            var sb = StringBuilderHelper.Acquire();

            if (excludeDeputies)
            {
                sb.Append("{$UI_KrNotification_ExcludeDeputies}");
            }

            if (excludeSubscribers)
            {
                if (sb.Length > 0)
                {
                    sb.AppendLine();
                }

                sb.Append("{$UI_KrNotification_ExcludeSubscribers}");
            }

            if (optionalRecipients is { Length: > 0})
            {
                if (sb.Length > 0)
                {
                    sb.AppendLine();
                }

                sb
                    .Append("{$CardTypes_Controls_OptionalRecipients}: ")
                    .AppendJoin(", ", optionalRecipients);
            }

            return sb.ToStringAndRelease();
        }

        #endregion
    }
}
