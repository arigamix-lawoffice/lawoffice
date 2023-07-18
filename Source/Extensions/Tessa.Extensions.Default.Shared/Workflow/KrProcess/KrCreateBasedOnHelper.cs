using System;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Константы и вспомогательные методы для создания карточек на основании других карточек.
    /// </summary>
    public static class KrCreateBasedOnHelper
    {
        #region Constants

        /// <summary>
        /// Ключ, по которому в <see cref="CardNewRequest"/><c>.Info</c> содержится идентификатор карточки,
        /// для которой выполняется создание на основании вместо обычного создания.
        /// Если не указан этот ключ и <see cref="CardKey"/>, то не выполняется создание на основании.
        /// </summary>
        public const string CardIDKey = "KrCreateBasedOnCardID";

        /// <summary>
        /// Ключ, по которому в <see cref="CardNewRequest"/><c>.Info</c> содержится объект загруженной карточки <see cref="Card"/>,
        /// для которой выполняется создание на основании вместо обычного создания.
        /// Если не указан этот ключ и <see cref="CardIDKey"/>, то не выполняется создание на основании.
        /// </summary>
        public const string CardKey = "KrCreateBasedOnCard";

        /// <summary>
        /// Ключ, по которому в <see cref="CardNewRequest"/><c>.Info</c> доступен токен безопасности <see cref="KrToken"/> для карточки,
        /// на основании которой создаётся другая карточка, или <c>null</c>, если токен отсутствует
        /// и права на чтение будут вычислены в процессе создания.
        /// </summary>
        public const string TokenKey = "KrCreateBasedOnToken";

        /// <summary>
        /// Ключ, по которому в <see cref="CardNewRequest"/><c>.Info</c> содержится признак того,
        /// что в создаваемой карточке должны быть скопированы файлы из базовой карточки.
        /// </summary>
        public const string CopyFilesKey = "KrCreateBasedOnCopyFiles";

        /// <summary>
        /// Ключ, по которому в расширениях на создание карточки в <c>context.Info</c> доступен объект базовой карточки.
        /// В такой карточке загружено всё, кроме истории заданий и указаний по просрочке загруженных заданий по бизнес-календарю.
        /// </summary>
        public const string CardInfoKey = "KrCreateBasedOnCard";

        #endregion

        #region Methods

        /// <summary>
        /// Инициализирует информацию по запросу на создание карточки <see cref="CardNewRequest"/><c>.Info</c>
        /// на основании заданной карточки с идентификатором <paramref name="baseCardID"/>.
        /// </summary>
        /// <param name="requestInfo">
        /// Информация по запросу на создание карточки <see cref="CardNewRequest"/><c>.Info</c>
        /// на основании заданной карточки с идентификатором <paramref name="baseCardID"/>.
        /// </param>
        /// <param name="baseCardID">Идентификатор карточки, на основании которой создаётся другая карточка.</param>
        /// <param name="copyFiles">Признак того, что должны быть скопированы файлы из основной карточки.</param>
        /// <param name="baseCardToken">
        /// Токен с правами на чтение базовой карточки <paramref name="baseCardID"/>
        /// или <c>null</c>, если права будут вычислены на сервере.
        /// </param>
        public static void InitializeRequestInfo(
            Dictionary<string, object> requestInfo,
            Guid baseCardID,
            bool copyFiles = false,
            KrToken baseCardToken = null)
        {
            Check.ArgumentNotNull(requestInfo, nameof(requestInfo));

            requestInfo[CardIDKey] = baseCardID;
            requestInfo[CopyFilesKey] = BooleanBoxes.Box(copyFiles);

            if (baseCardToken != null)
            {
                var tokenStorage = new Dictionary<string, object>();
                baseCardToken.Set(tokenStorage);

                requestInfo[TokenKey] = tokenStorage;
            }
        }


        /// <summary>
        /// Инициализирует информацию по запросу на создание карточки <see cref="CardNewRequest"/><c>.Info</c>
        /// на основании заданной карточки <paramref name="baseCard"/>.
        /// </summary>
        /// <param name="requestInfo">
        /// Информация по запросу на создание карточки <see cref="CardNewRequest"/><c>.Info</c>
        /// на основании заданной карточки <paramref name="baseCard"/>.
        /// </param>
        /// <param name="baseCard">Карточка, на основании которой создаётся другая карточка.</param>
        /// <param name="copyFiles">Признак того, что должны быть скопированы файлы из основной карточки.</param>
        public static void InitializeRequestInfo(
            Dictionary<string, object> requestInfo,
            Card baseCard,
            bool copyFiles = false)
        {
            Check.ArgumentNotNull(baseCard, nameof(baseCard));

            // остальные параметры проверяются в вызываемом методе
            InitializeRequestInfo(requestInfo, baseCard.ID, copyFiles, KrToken.TryGet(baseCard.Info));
        }


        /// <summary>
        /// Возвращает поле <c>DocDescription</c> для описания ссылки на документ,
        /// которая обычно возвращается типовым представлением RefDocumentsLookup,
        /// или <c>null</c>, если в заданной карточке отсутствуют соответствующие секции.
        /// </summary>
        /// <param name="card">Карточка, для которой требуется получить текстовое описание для ссылки на эту карточку.</param>
        /// <returns>
        /// Поле <c>DocDescription</c> для описания ссылки на документ,
        /// которая обычно возвращается типовым представлением RefDocumentsLookup,
        /// или <c>null</c>, если в заданной карточке отсутствуют соответствующие секции.
        /// </returns>
        public static string TryGetDocDescription(Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            CardSection section = null;
            if (card.TryGetSections()?.TryGetValue("DocumentCommonInfo", out section) != true
                || section == null)
            {
                return null;
            }

            Dictionary<string, object> fields = section.RawFields;
            return fields.TryGet<string>("FullNumber") + ", " + fields.TryGet<string>("Subject");
        }

        #endregion
    }
}
