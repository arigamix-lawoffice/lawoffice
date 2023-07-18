using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    public sealed class KrNotificationSettingsStoreExtension : CardStoreExtension
    {
        #region Base Overrides

        public override Task BeforeRequest(ICardStoreExtensionContext context)
        {
            var card = context.Request.Card;

            if (card.Sections.TryGetValue(KrConstants.KrStages.Virtual, out var stageSection))
            {
                foreach (var row in stageSection.Rows)
                {
                    if (row.State != CardRowState.Deleted
                        && row.TryGetValue(KrConstants.KrNotificationSettingVirtual.NotificationID, out var notificationID)
                        && notificationID == null
                        && (row.State == CardRowState.Modified
                            || row.TryGet<Guid?>(KrConstants.KrStages.StageTypeID) == StageTypeDescriptors.NotificationDescriptor.ID))
                    {
                        context.ValidationResult.AddError(
                            this,
                            LocalizationManager.Format(
                                "$KrProcess_NotificationIsEmpty",
                                row.TryGet<string>(KrConstants.KrStages.NameField) ?? StageTypeDescriptors.NotificationDescriptor.Caption));
                        return Task.CompletedTask;
                    }
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
