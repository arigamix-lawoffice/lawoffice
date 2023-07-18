using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Базовый класс расширений для процесса сохранения карточки содержащей шаблон этапов маршрута документов.
    /// </summary>
    public abstract class KrTemplateStoreExtension : CardStoreExtension
    {
        #region Fields

        private const string HasStageChangesKey =
            nameof(KrStageTemplateStoreExtension) + "." + nameof(HasStageChangesKey);

        protected readonly IKrStageSerializer Serializer;

        protected readonly ICardStoreStrategy CardStoreStrategy;

        protected readonly ICardMetadata CardMetadata;

        protected readonly IKrProcessCache KrProcessCache;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrTemplateStoreExtension"/>.
        /// </summary>
        /// <param name="serializer">Объект предоставляющий методы для сериализации параметров этапов.</param>
        /// <param name="cardStoreStrategy">Стратегия сохранения карточки.</param>
        /// <param name="cardMetadata">Метаинформация, необходимую для использования типов карточек совместно с пакетом карточек.</param>
        /// <param name="krProcessCache">Кэш для данных из карточек шаблонов этапов.</param>
        protected KrTemplateStoreExtension(
            IKrStageSerializer serializer,
            ICardStoreStrategy cardStoreStrategy,
            ICardMetadata cardMetadata,
            IKrProcessCache krProcessCache)
        {
            this.Serializer = serializer;
            this.CardStoreStrategy = cardStoreStrategy;
            this.CardMetadata = cardMetadata;
            this.KrProcessCache = krProcessCache;
        }

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Действие выполняемое перед обработкой карточки при её создании (<see cref="CardStoreMode.Insert"/>).
        /// </summary>
        /// <param name="context">Контекст процесса сохранения карточки.</param>
        protected virtual void OnInsert(ICardStoreExtensionContext context)
        {
        }

        /// <summary>
        /// Возвращает значение, показывающее, что необходимо обновление карточки при её создании (<see cref="CardStoreMode.Insert"/>).
        /// </summary>
        /// <param name="context">Контекст процесса сохранения карточки.</param>
        /// <returns>Значенние <see langword="true"/>, если обновление карточки при её создании необходимо, иначе - <see langword="false"/>.</returns>
        protected virtual bool DoInsert(
            ICardStoreExtensionContext context) => true;

        /// <summary>
        /// Действие выполняемое перед обновлением карточки содержащей данные из сохраняемой карточки.
        /// </summary>
        /// <param name="context">Контекст процесса сохранения карточки.</param>
        /// <param name="innerCard">Обновляемая карточка.</param>
        protected virtual void OnUpdate(ICardStoreExtensionContext context, Card innerCard)
        {
        }

        /// <summary>
        /// Возвращает значение, показывающее, что необходимо принудительное обновление карточки при её изменении (<see cref="CardStoreMode.Update"/>).
        /// </summary>
        /// <param name="context">Контекст процесса сохранения карточки.</param>
        /// <returns>Значенние <see langword="true"/>, если принудительное обновление карточки необходимо, иначе - <see langword="false"/>.</returns>
        protected virtual bool DoUpdate(
            ICardStoreExtensionContext context) => true;

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Возвращает обновляемую карточку в которую должны быть сохранены изменения из сохраняемой карточки.
        /// </summary>
        /// <param name="context">Контекст процесса сохранения карточки.</param>
        /// <returns>Обновляемая карточка в которую должны быть сохранены изменения из сохраняемой карточки.</returns>
        protected abstract Task<Card> GetInnerCardAsync(
            ICardStoreExtensionContext context);

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task BeforeRequest(ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            var card = context.Request.Card;

            var hasStageChanges = this.HasStageChanges(card);

            context.Info[HasStageChangesKey] = BooleanBoxes.Box(hasStageChanges);

            if (!hasStageChanges && !this.DoInsert(context))
            {
                return;
            }

            if (card.StoreMode == CardStoreMode.Insert)
            {
                this.OnInsert(context);

                if (hasStageChanges)
                {
                    await this.UpdateStagesAsync(card, card, context, this.KrProcessCache);
                }
            }
            else
            {
                context.Request.ForceTransaction = true;
            }
        }

        /// <inheritdoc/>
        public override async Task AfterBeginTransaction(ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            var hasStageChanges = context.Info.TryGet<bool>(HasStageChangesKey);
            var outerCard = context.Request.Card;

            if (outerCard.StoreMode == CardStoreMode.Update
                && (hasStageChanges || this.DoUpdate(context)))
            {
                var innerCard = await this.GetInnerCardAsync(context);
                if (!context.ValidationResult.IsSuccessful())
                {
                    return;
                }

                this.OnUpdate(context, innerCard);

                if (hasStageChanges)
                {
                    await this.UpdateStagesAsync(outerCard, innerCard, context, this.KrProcessCache);
                }

                innerCard.RemoveAllButChanged(innerCard.StoreMode);
                var storeContext =
                    await CardStoreContext.CreateAsync(
                        innerCard,
                        DateTime.UtcNow,
                        context.Session,
                        this.CardMetadata,
                        context.TransactionStrategy,
                        context.ValidationResult,
                        context.DbScope.Executor,
                        context.DbScope.BuilderFactory,
                        context.DbScope.Db,
                        context.Request.TryGetRepairSectionsFlag(),
                        cancellationToken: context.CancellationToken);
                var extraSourcesChanged = innerCard.TryGetStagesSection(out var stagesSec)
                        && stagesSec.Rows.Any(p => p.ContainsKey(KrConstants.KrStages.ExtraSources));
                outerCard.Info[KrConstants.Keys.ExtraSourcesChanged] = BooleanBoxes.Box(extraSourcesChanged);

                await this.CardStoreStrategy.StoreAsync(storeContext);
            }
        }

        #endregion

        #region Private Methods

        private bool HasStageChanges(Card card)
        {
            var storeMode = card.StoreMode;
            switch (storeMode)
            {
                case CardStoreMode.Insert:
                    // Интересует исключительно виртуальная версия для main карточек
                    return this.Serializer
                        .SettingsSectionNames
                        .Any(p => card.Sections.TryGetValue(p, out var sec) && sec.HasChanges());
                case CardStoreMode.Update:
                    // Интересует исключительно виртуальная версия для main карточек
                    return this.Serializer
                        .SettingsSectionNames
                        .Any(p => card.Sections.ContainsKey(p));
                default:
                    throw new InvalidOperationException($"Unknown CardStoreMode.{storeMode.ToString()}");
            }
        }

        private async Task UpdateStagesAsync(Card outerCard, Card innerCard, ICardStoreExtensionContext context, IKrProcessCache krProcessCache)
        {
            IDictionary<Guid, IDictionary<string, object>> stageStorages = null;
            StringDictionaryStorage<CardSection> rows;
            if (innerCard.Sections.TryGetValue(KrConstants.KrStages.Name, out var krStagesSec)
                && (rows = outerCard.TryGetSections()) != null)
            {
                stageStorages = await this.Serializer.MergeStageSettingsAsync(krStagesSec, rows, context.CancellationToken);
            }

            new KrProcessSectionMapper(outerCard, innerCard)
                .MapKrStages();

            await this.Serializer.UpdateStageSettingsAsync(innerCard, outerCard, stageStorages, krProcessCache, context, context.CancellationToken);
        }

        #endregion

    }
}
