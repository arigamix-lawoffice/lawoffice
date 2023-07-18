using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Tiles
{
    public static class KrTileHelper
    {
        #region Fields

        private static volatile ReadOnlyCollection<Guid> unavaiableTypeIDList;

        #endregion

        #region Static Methods

        /// <summary>
        /// Ставит в Info карточки отметку, по которой сработает соответствующее расширение,
        /// и открывает карточку. Перед вызовом открытия карточки можно использовать диалог для
        /// подтверждения/отмены действия и опционального вызова предварительного сохранения карточки.
        /// </summary>
        /// <param name="mark">Отметка, по которой сработает расширение при открытии карточки.</param>
        /// <param name="proceedConfirmation">Функция подтверждения основного действия</param>
        /// <param name="proceedAndSaveCardConfirmation">Функция подтверждения действия и опционального
        /// сохранения карточки. Если функция вернет null - отрытия карточки не будет, если false - будет
        /// открытие карточки без сохранения изменения, если true - открытие карточки с сохраением изменений</param>
        /// <param name="continuationOnSuccessFunc">
        /// Функция, выполняемая в потоке UI при успешном действии с карточкой,
        /// или <c>null</c>, если такая функция отсутствует.
        /// </param>
        /// <param name="getInfo">
        /// Информация, передаваемая в запрос на загрузку карточки <c>Request.Info</c>,
        /// или <c>null</c>, если дополнительная информация не указывается.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Задача, возвращающая <c>true</c>, если сохранение успешно завершено или не выполнялось, и <c>false</c> в противном случае.
        /// </returns>
        public static async Task<bool> OpenMarkedCardAsync(
            string mark,
            Func<bool> proceedConfirmation,
            Func<bool?> proceedAndSaveCardConfirmation,
            Func<Task<bool>> continuationOnSuccessFunc = null,
            Dictionary<string, object> getInfo = null,
            CancellationToken cancellationToken = default)
        {
            IUIContext context = UIContext.Current;
            ICardEditorModel editor = context.CardEditor;
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
                    context,
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
                context,
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
                    context,
                    cancellationToken: cancellationToken);
            }

            if (!sendTaskSucceeded || continuationOnSuccessFunc == null)
            {
                return sendTaskSucceeded;
            }

            await using (UIContext.Create(context))
            {
                return await continuationOnSuccessFunc();
            }
        }


        /// <summary>
        /// Возвращает список недоступных идентификаторов для создания эффективных (типы карточек, не использующие типы документов и типы документов) типов.
        /// </summary>
        /// <param name="cardRepository">Репозиторий карточек</param>
        /// <param name="krTypesCache">Кеш типов</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Возвращает список недоступных идентификаторов для создания эффективных типов.</returns>
        public static async ValueTask<(ReadOnlyCollection<Guid>, ValidationResult)> GetUnavailableTypesAsync(
            ICardRepository cardRepository,
            IKrTypesCache krTypesCache,
            CancellationToken cancellationToken = default)
        {
            if (unavaiableTypeIDList != null)
            {
                return (unavaiableTypeIDList, ValidationResult.Empty);
            }

            ValidationResult result;
            (unavaiableTypeIDList, result) = await KrPermissionsHelper.GetUnavailableTypesAsync(
                cardRepository, krTypesCache, cancellationToken: cancellationToken).ConfigureAwait(false);
            return (unavaiableTypeIDList, result);
        }

        /// <summary>
        /// Устанавливает кэш недоступных идентификаторов, который может быть получен вызовом <see cref="GetUnavailableTypesAsync"/>.
        /// </summary>
        /// <param name="unavailableTypes">Список недоступных идентификаторов для создания эффективных типов.</param>
        public static void SetUnavailableTypes(ReadOnlyCollection<Guid> unavailableTypes) => unavaiableTypeIDList = unavailableTypes;

        #endregion
    }
}
