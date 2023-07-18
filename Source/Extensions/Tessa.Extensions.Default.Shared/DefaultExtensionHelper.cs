using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Общие вспомогательные методы для расширений типового решения.
    /// </summary>
    public static class DefaultExtensionHelper
    {
        #region Constants

        /// <summary>
        /// Константа для установки местоположения контента файлов в запросах <see cref="CardRequest"/>.
        /// </summary>
        public const string SourceIDKey = "SourceID";

        /// <summary>
        /// Ключ, по которому в <c>Info</c> карточки или запроса передаётся идентификатор состояния
        /// для виртуальной карточки состояния документа.
        /// </summary>
        public const string StateIDKey = "StateID";

        #endregion

        #region Static Methods

        /// <summary>
        /// Устанавливает местоположение контента файлов в запросе <paramref name="request"/>.
        /// </summary>
        /// <param name="request">Запрос, требующий известного местоположения.</param>
        /// <param name="sourceType">Местоположение контента файлов, задаваемое в запросе.</param>
        public static void SetSourceID(CardRequest request, CardFileSourceType sourceType)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            request.Info[SourceIDKey] = (int)sourceType;
        }


        /// <summary>
        /// Переносит все файлы или заданный файл <paramref name="fileID"/> для карточки <paramref name="cardID"/>
        /// на местоположение контента файлов <paramref name="sourceType"/>.
        ///
        /// Перенос файла включает перенос всех его версий. Если версия уже располагалась в заданном местоположении, то действий не производится.
        /// Метод корректно выполняется только в том случае, если пользователь является администратором.
        ///
        /// Возвращает результат выполнения метода, в котором, как правило, содержатся ошибки в случае неудачного выполнения.
        /// Возвращаемый объект никогда не равен <c>null</c>.
        /// </summary>
        /// <param name="sourceType">Местоположение контента файлов, на которое требуется перенести файлы.</param>
        /// <param name="extendedRepository">Репозиторий для управления карточки с расширениями.</param>
        /// <param name="cardID">Идентификатор карточки, файл или файлы которой должны быть перенесены.</param>
        /// <param name="fileID">Идентификатор файла, который переносится, или <c>null</c>, если переносятся все файлы карточки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат выполнения метода. Не равен <c>null</c>.</returns>
        public static async Task<ValidationResult> MoveFilesToAsync(
            CardFileSourceType sourceType,
            ICardRepository extendedRepository,
            Guid cardID,
            Guid? fileID = null,
            CancellationToken cancellationToken = default)
        {
            var request = new CardRequest { RequestType = DefaultRequestTypes.MoveFiles, CardID = cardID, FileID = fileID };
            SetSourceID(request, sourceType);

            CardResponse response = await extendedRepository.RequestAsync(request, cancellationToken).ConfigureAwait(false);
            return response.ValidationResult.Build();
        }


        public const string CardTypeIDResponseKey = "cardTypeID";

        public const string DocTypeIDResponseKey = "docTypeID";

        public const string DocTypeTitleResponseKey = "docTypeTitle";

        /// <summary>
        /// <para>Возвращает информацию по типу карточки и типу документа (если он присутствует) для карточки с заданными идентификатором.
        /// Возвращает результат выполнения запроса. Поскольку запрос расширяемый, то даже при успешном запросе (т.е. при отсутствии расширений)
        /// возвращаемые out-параметры могут быть равны <c>null</c>.</para>
        ///
        /// <para>Возвращает <c>cardTypeID</c> - идентификатор типа карточки, полученный по идентификатору карточки <paramref name="cardID"/>,
        /// или <c>null</c>, если карточка с таким идентификатором не существует</para>
        ///
        /// <para>Возвращает <c>docTypeID</c> - идентификатор типа документа, полученный по идентификатору карточки <paramref name="cardID"/>,
        /// или <c>null</c>, если либо карточка с таким идентификатором не существует,
        /// либо тип карточки не добавлен в типовое решение, либо тип карточки не поддерживает типы документов.</para>
        ///
        /// <para>Возвращает <c>docTypeTitle</c> - отображаемое название типа документа, полученное по идентификатору карточки <paramref name="cardID"/>,
        /// или <c>null</c>, если либо карточка с таким идентификатором не существует,
        /// либо тип карточки не добавлен в типовое решение, либо тип карточки не поддерживает типы документов</para>
        /// </summary>
        /// <param name="extendedRepository">Репозиторий для управления карточки с расширениями.</param>
        /// <param name="cardID">Идентификатор карточки, для которой возвращается информация.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат выполнения запроса.</returns>
        public static async Task<(ValidationResult result, Guid? cardTypeID, Guid? docTypeID, string docTypeTitle)> GetDocTypeInfoAsync(
            ICardRepository extendedRepository,
            Guid cardID,
            CancellationToken cancellationToken = default)
        {
            var request = new CardRequest { RequestType = DefaultRequestTypes.GetDocTypeInfo, CardID = cardID };
            CardResponse response = await extendedRepository.RequestAsync(request, cancellationToken).ConfigureAwait(false);

            ValidationResult result = response.ValidationResult.Build();

            Guid? cardTypeID = result.IsSuccessful
                ? response.Info.TryGet<Guid?>(CardTypeIDResponseKey)
                : null;

            if (cardTypeID.HasValue)
            {
                Guid? docTypeID = response.Info.TryGet<Guid?>(DocTypeIDResponseKey);
                string docTypeTitle = docTypeID.HasValue ? response.Info.TryGet<string>(DocTypeTitleResponseKey) : null;
                return (result, cardTypeID, docTypeID, docTypeTitle);
            }

            return (result, null, null, null);
        }

        /// <summary>
        /// Возвращает уникальный идентификатор виртуальной карточки состояния документа.
        /// </summary>
        /// <param name="stateID">Числовой идентификатор состояния документа.</param>
        /// <returns>Уникальный идентификатор виртуальной карточки состояния документа.</returns>
        public static Guid GetKrDocStateCardID(int stateID) =>
            (stateID.ToString(CultureInfo.InvariantCulture) + "." + DefaultCardTypes.KrDocStateTypeID.ToString("N")).ToGuid();

        #endregion
    }
}
