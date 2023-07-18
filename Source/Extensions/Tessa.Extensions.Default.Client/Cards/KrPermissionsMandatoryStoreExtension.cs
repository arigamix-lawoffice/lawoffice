using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Localization;
using Tessa.Platform.Collections;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Tasks;

namespace Tessa.Extensions.Default.Client.Cards
{
    public sealed class KrPermissionsMandatoryStoreExtension : CardStoreExtension
    {
        #region Constructors

        public KrPermissionsMandatoryStoreExtension(ICardControlAdditionalInfoRegistry controlRegistry) =>
            this.controlRegistry = NotNullOrThrow(controlRegistry);

        #endregion

        #region Fields

        private readonly ICardControlAdditionalInfoRegistry controlRegistry;

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardStoreExtensionContext context)
        {
            if (!context.RequestIsSuccessful
                && UIContext.Current.CardEditor != null
                && context.Response.Info.TryGetValue(KrPermissionsHelper.FailedMandatoryRulesKey, out var rulesObj)
                && rulesObj is IList rules)
            {
                var cardModel = UIContext.Current.CardEditor.CardModel;
                var failRules = rules.Cast<Dictionary<string, object>>().Select(x => new KrPermissionMandatoryRuleStorage(x)).ToList();

                ValidateFromRules(cardModel, failRules);

                var requestTasks = context.Request.Card.Tasks;
                var taskItems = cardModel.TryGetTaskItems();
                if (requestTasks.Count > 0
                    && taskItems is { Count: > 0 })
                {
                    foreach (var task in requestTasks)
                    {
                        if (task.Action == CardTaskAction.Complete
                            && taskItems.TryFirst(x => (x as TaskViewModel)?.TaskModel.CardTask.RowID == task.RowID, out var taskItem))
                        {
                            ValidateFromRules(((TaskViewModel) taskItem).TaskModel, failRules);
                        }
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private void ValidateFromRules(ICardModel cardModel, List<KrPermissionMandatoryRuleStorage> failRules)
        {
            foreach (var controlViewModel in cardModel.ControlBag)
            {
                var sourceInfo = this.controlRegistry.GetSourceInfo(controlViewModel.CardTypeControl);
                if (sourceInfo != null
                    && failRules.Any(x =>
                        x.SectionID == sourceInfo.SectionID
                        && (x.ColumnIDs.Count == 0
                            || sourceInfo.ColumnIDs.Any(y => x.ColumnIDs.Contains(y)))))
                {
                    controlViewModel.ValidationFunc = c => c.HasEmptyValue()
                        ? LocalizationManager.Format("$KrPermissions_MandatoryControlTemplate", controlViewModel.Caption)
                        : null;
                    controlViewModel.NotifyUpdateValidation();
                }
            }
        }

        #endregion
    }
}
