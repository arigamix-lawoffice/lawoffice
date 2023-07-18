using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Cards
{
    public class KrCardTaskAssignedRolesAccessProvider : CardTaskAssignedRolesAccessProvider
    {
        #region Fields

        private bool onceReLoaded = false;

        #endregion

        #region Private Methods

        public static async Task<bool> ReopenMainCardWithMarkAsync(
            IUIContext mainCardContext,
            string mark,
            Func<bool> proceedConfirmation,
            Func<bool?> proceedAndSaveCardConfirmation,
            Func<Task<bool>> continuationOnSuccessFunc = null,
            Dictionary<string, object> getInfo = null,
            CancellationToken cancellationToken = default)
        {
            ICardEditorModel editor = mainCardContext.CardEditor;
            ICardModel model;

            if (editor == null || editor.OperationInProgress || (model = editor.CardModel) == null)
            {
                return true;
            }

            bool cardIsNew = model.Card.StoreMode == CardStoreMode.Insert;
            bool hasChanges = cardIsNew || await model.HasChangesAsync(cancellationToken: cancellationToken);
            bool? saveCardBeforeOpening;

            if (hasChanges && proceedAndSaveCardConfirmation != null)
            {
                saveCardBeforeOpening = proceedAndSaveCardConfirmation();
            }
            //Если не указана функция подтверждения с вариантом отмены - сохраняем карточку
            //если есть подтверждение основного действия
            else if (proceedConfirmation != null && hasChanges)
            {
                saveCardBeforeOpening = proceedConfirmation() ? (bool?)true : null;
            }
            //Если в карточке не было изменений - не вызываем сохранения
            else if (proceedConfirmation != null)
            {
                saveCardBeforeOpening = proceedConfirmation() ? (bool?)false : null;
            }
            //Если не указана функция подтверждения и нет изменений - вызываем основное действие
            //без подтверждения и сохранения
            else
            {
                saveCardBeforeOpening = false;
            }

            if (getInfo == null)
            {
                getInfo = new Dictionary<string, object>(StringComparer.Ordinal);
            }

            if (!string.IsNullOrWhiteSpace(mark))
            {
                getInfo[mark] = BooleanBoxes.True;
            }

            if (!saveCardBeforeOpening.HasValue)
            {
                return false;
            }

            if (saveCardBeforeOpening.Value)
            {
                KrToken token = KrToken.TryGet(editor.Info);
                KrToken.Remove(editor.Info);

                if (!await editor.SaveCardAsync(
                    mainCardContext,
                    info:
                    new Dictionary<string, object>
                    {
                        {KrPermissionsHelper.SaveWithPermissionsCalcFlag, true}
                    },
                    request: new CardSavingRequest(CardSavingMode.KeepPreviousCard),
                    cancellationToken: cancellationToken))
                {
                    return false;
                }

                token?.Set(getInfo);
            }

            Guid cardID = model.Card.ID;
            CardType cardType = model.CardType;

            bool sendTaskSucceeded = await editor.OpenCardAsync(
                cardID,
                cardType.ID,
                cardType.Name,
                mainCardContext,
                getInfo,
                cancellationToken: cancellationToken);

            if (sendTaskSucceeded)
            {
                editor.IsUpdatedServer = true;
            }
            else if (cardIsNew || saveCardBeforeOpening.Value)
            {
                // если карточка новая или была сохранена, а также не удалось выполнить mark-действие при открытии,
                // то у нас будет "висеть" карточка с некорректной версией;
                // её надо обновить, на этот раз без mark'и

                await editor.OpenCardAsync(
                    cardID,
                    cardType.ID,
                    cardType.Name,
                    mainCardContext,
                    cancellationToken: cancellationToken);
            }

            if (!sendTaskSucceeded || continuationOnSuccessFunc == null)
            {
                return sendTaskSucceeded;
            }

            await using (UIContext.Create(mainCardContext))
            {
                return await continuationOnSuccessFunc();
            }
        }

        #endregion

        #region ICardTaskAssignedRolesAccessProvider Members

        /// <inheritdoc />
        public override async ValueTask<bool> CheckAccessAsync(Guid taskRowID, IUIContext mainCardUIContext, CancellationToken cancellationToken = default)
        {
            if (await base.CheckAccessAsync(taskRowID, mainCardUIContext, cancellationToken))
            {
                return true;
            }

            if (!this.onceReLoaded)
            {
                await ReopenMainCardWithMarkAsync(mainCardUIContext,
                    KrPermissionsHelper.CalculateTaskAssignedRolesPermissionsMark,
                    null, //Не требуем подтверждения действия, если не было изменений
                    () => TessaDialog.ConfirmWithCancel("$KrMesages_EditModeConfirmation"),
                    cancellationToken: cancellationToken);
                this.onceReLoaded = true;
            }

            return await base.CheckAccessAsync(taskRowID, mainCardUIContext, cancellationToken);
        }

        #endregion
    }
}
