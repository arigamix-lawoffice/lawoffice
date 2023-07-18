using System;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Расширение <see cref="ICardNewExtension"/> для процесса создания и <see cref="ICardGetExtension"/> для процесса загрузки карточки шаблона этапов.
    /// </summary>
    public sealed class KrStageTemplateNewGetExtension : KrTemplateNewGetExtension
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrStageTemplateNewGetExtension"/>.
        /// </summary>
        /// <param name="serializer">Объект предоставляющий методы для сериализации параметров этапов.</param>
        /// <param name="getGuidReplacerFunc">Метод возвращающий объект, выполняющий замещение идентификаторов на сгенерированные идентификаторы.</param>
        public KrStageTemplateNewGetExtension(
            IKrStageSerializer serializer,
            Func<IGuidReplacer> getGuidReplacerFunc)
            : base(
                  serializer,
                  getGuidReplacerFunc)
        {
        }

        #endregion
    }
}