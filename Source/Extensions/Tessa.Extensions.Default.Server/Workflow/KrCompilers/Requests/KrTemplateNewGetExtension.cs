using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants.KrApprovalSettingsVirtual;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Базовый класс расширений <see cref="ICardNewExtension"/> для процесса создания и <see cref="ICardGetExtension"/> для процесса загрузки карточки содержащей этапы маршрута документа.
    /// </summary>
    public abstract class KrTemplateNewGetExtension : CardNewGetExtension
    {
        #region Fields

        private readonly IKrStageSerializer serializer;

        private readonly Func<IGuidReplacer> getGuidReplacerFunc;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrTemplateNewGetExtension"/>.
        /// </summary>
        /// <param name="serializer">Объект предоставляющий методы для сериализации параметров этапов.</param>
        /// <param name="getGuidReplacerFunc">Метод возвращающий объект, выполняющий замещение идентификаторов на сгенерированные идентификаторы.</param>
        protected KrTemplateNewGetExtension(
            IKrStageSerializer serializer,
            Func<IGuidReplacer> getGuidReplacerFunc)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.getGuidReplacerFunc = getGuidReplacerFunc ?? throw new ArgumentNullException(nameof(getGuidReplacerFunc));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return;
            }

            var response = context.Response;

            if (context.Method == CardNewMethod.Template)
            {
                var card = response.Card;

                foreach (var row in card.GetStagesSection().Rows)
                {
                    row[FirstIsResponsible] = BooleanBoxes.False;
                }

                await StageRowMigrationHelper.MigrateAsync(
                    card,
                    card,
                    KrProcessSerializerHiddenStageMode.Ignore,
                    this.serializer,
                    context.CardMetadata,
                    this.getGuidReplacerFunc(),
                    cancellationToken: context.CancellationToken);

                KrProcessHelper.SetInactiveStateToStages(card);
                KrCompilersHelper.ClearPhysicalSections(card);
            }

            KrProcessHelper.SetStageDefaultValues(response);
        }

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            if (!context.RequestIsSuccessful
                || (card = context.Response.TryGetCard()) is null)
            {
                return;
            }

            await this.serializer.DeserializeSectionsAsync(
                card,
                card,
                cardContext: context,
                cancellationToken: context.CancellationToken);
            KrProcessHelper.SetStageDefaultValues(context.Response);
            KrCompilersHelper.ClearPhysicalSections(card);
        }

        #endregion

    }
}