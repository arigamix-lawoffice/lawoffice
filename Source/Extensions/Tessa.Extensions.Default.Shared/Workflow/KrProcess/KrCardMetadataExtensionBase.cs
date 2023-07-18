using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Metadata;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Базовый класс расширений на метаинформацию типового решения.
    /// Расширяет типы карточек KrCard, KrStageTemplate, KrSecondaryProcess.
    /// И добавляет формы, валидаторы и расширения из KrCard во все карточки типового решения.
    /// </summary>
    public abstract class KrCardMetadataExtensionBase :
        CardTypeMetadataExtension
    {
        #region Nested Types

        /// <summary>
        /// Помощник добавления элементов управления DocStateControl и DocStateChangedControl из KrCard
        /// в блок KrBlockForDocStatus целевого типа карточки.
        /// </summary>
        private sealed class KrFillBlockForDocStatusVisitor : CardTypeVisitor
        {
            /// <summary>
            /// Элемент управления DocStateControl из KrCard.
            /// </summary>
            private readonly CardTypeControl docStateControl;

            /// <summary>
            /// Элемент управления DocStateChangedControl из KrCard.
            /// </summary>
            private readonly CardTypeControl docStateChangedControl;

            /// <summary>
            /// Создаёт экземпляр класса для вставки элементов управления DocStateControl и DocStateChangedControl из KrCard
            /// в блок KrBlockForDocStatus целевого типа карточки.
            /// </summary>
            /// <param name="validationResult">Результат для добавления сведений об ошибках в процессе добавления элементов управления и обхода типа карточки.</param>
            /// <param name="docStateControl">Элемент управления DocStateControl из KrCard.</param>
            /// <param name="docStateChangedControl">Элемент управления DocStateChangedControl из KrCard.</param>
            public KrFillBlockForDocStatusVisitor(
                CardTypeControl docStateControl,
                CardTypeControl docStateChangedControl,
                IValidationResultBuilder validationResult = null)
                : base(validationResult)
            {
                this.docStateControl = docStateControl;
                this.docStateChangedControl = docStateChangedControl;
            }

            /// <inheritdoc/>
            public override ValueTask VisitBlockAsync(
                CardTypeBlock block,
                CardTypeForm form,
                CardType type,
                CancellationToken cancellationToken = default)
            {
                if (block.Name == KrConstants.Ui.KrBlockForDocStatusAlias)
                {
                    block.Controls.Add(this.docStateControl);
                    block.Controls.Add(this.docStateChangedControl);
                }

                return new ValueTask();
            }
        }

        #endregion

        #region Constructors

        /// <inheritdoc />
        protected KrCardMetadataExtensionBase(ICardMetadata clientCardMetadata)
            : base(clientCardMetadata)
        {
        }

        /// <inheritdoc />
        protected KrCardMetadataExtensionBase()
            : base()
        {
        }

        #endregion

        #region Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Расширить и настроить указанные типы карточек.
        /// </summary>
        /// <param name="krTypes">Типы карточек для расширения.</param>
        /// <param name="context">Контекст выполнения операции, предоставляющий доступ к метаданным.</param>
        /// <returns>Асинхронная задача.</returns>
        protected abstract Task ExtendKrTypesAsync(
            IList<CardType> krTypes,
            ICardMetadataExtensionContext context);

        /// <summary>
        /// Получить все секции метаданных о таблицах.
        /// </summary>
        /// <param name="context">Контекст выполнения операции, предоставляющий доступ к метаданным.</param>
        /// <returns>Коллекция секций метаданных о таблицах.</returns>
        protected abstract ValueTask<CardMetadataSectionCollection> GetAllSectionsAsync(
            ICardMetadataExtensionContext context);

        /// <summary>
        /// Получить идентификаторы типов карточек, входящих в типовое решение.
        /// </summary>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>Идентификаторы типов карточек, входящих в типовое решение.</returns>
        protected abstract Task<List<Guid>> GetCardTypeIDsAsync(CancellationToken cancellationToken = default);

        #endregion

        #region Private Methods

        private static bool CheckCardType(CardType type, List<Guid> allowedCardTypeIDs) =>
            type.InstanceType == CardInstanceType.Card && allowedCardTypeIDs.Contains(type.ID);

        /// <summary>
        /// Подготовить карточку (KrCard) к экспорту её компонентов.
        /// </summary>
        /// <param name="cardType">Тип карточки.</param>
        /// <param name="context">Контекст выполнения операции, предоставляющий доступ к метаданным.</param>
        /// <returns>Подготовленный тип карточки (KrCard) и визитор для вставки контролов состояния документа (если есть).</returns>
        private static (CardType krCard, ICardTypeVisitor visitor)
            PrepareSourceCardType(CardType cardType, ICardMetadataExtensionContext context)
        {
            using var ctx = new CardGlobalReferencesContext(context, cardType);
            // добавляем в глобальные ссылки все элементы, которые используются потом в EnhanceAsync.
            // добавляем формы.
            cardType.Forms.MakeGlobal(ctx);
            
            // получаем данные о контролах DocStateControl и DocStateChangedControl.
            var docStateInfo = GetDocStateControls(cardType);
            ICardTypeVisitor visitor = null;
            // если контролы нашлись, то их нужно также сделать глобальными.
            if (docStateInfo is not null)
            {
                var (docStateControl, docStateChangedControl) = docStateInfo.Value;
                docStateControl.MakeGlobal(ctx);
                docStateChangedControl.MakeGlobal(ctx);
                visitor = new KrFillBlockForDocStatusVisitor(
                    docStateControl,
                    docStateChangedControl);
            }
            // добавляем валидаторы.
            cardType.Validators.MakeGlobal(ctx);
            
            // добавляем расширения.
            cardType.Extensions.MakeGlobal(ctx);
            
            return (cardType, visitor);
        }

        /// <summary>
        /// Получить контролы DocStateControl и DocStateChangedControl.
        /// </summary>
        /// <param name="cardType">Тип карточки KrCard.</param>
        /// <returns>Информация о контролах DocStateControl и DocStateChangedControl, или <c>null</c> если их не удалось найти.</returns>
        private static (CardTypeControl docStateControl, CardTypeControl docStateChangedControl)?
            GetDocStateControls(CardType cardType)
        {
            CardTypeControl docState = null, docStateChanged = null;

            foreach (CardTypeNamedForm form in cardType.Forms)
            {
                foreach (CardTypeBlock block in form.Blocks)
                {
                    foreach (CardTypeControl control in block.Controls)
                    {
                        switch (control.Name)
                        {
                            case "DocStateControl":
                                docState = control;
                                if (docStateChanged is not null)
                                {
                                    return (docState, docStateChanged);
                                }
                                break;
                            case "DocStateChangedControl":
                                docStateChanged = control;
                                if (docState is not null)
                                {
                                    return (docState, docStateChanged);
                                }
                                break;
                        }
                    }
                }
            }

            return null;
        }
        
        private static async ValueTask EnhanceAsync(
            CardType krCardType,
            ICardTypeVisitor visitor,
            CardType targetCardType,
            CardMetadataSectionCollection enhanceableSections,
            CardMetadataSectionCollection allSections,
            CancellationToken cancellationToken = default)
        {
            // быстрее скопировать весь тип карточки, чем отдельные его части
            // теперь клон нужен только для копирования SchemeItems.
            CardType krCardTypeClone = await krCardType.DeepCloneAsync(cancellationToken).ConfigureAwait(false);

            targetCardType.SchemeItems.AddRange(krCardTypeClone.SchemeItems);
            targetCardType.Validators.AddRange(krCardType.Validators);
            targetCardType.Extensions.AddRange(krCardType.Extensions);

            targetCardType.Forms.AddRange(krCardType.Forms);
            // копируем в блок KrBlockForDocStatus элементов управления DocStateControl и DocStateChangedControl из KrCard.
            if (visitor is not null)
            {
                await targetCardType.VisitAsync(visitor, cancellationToken).ConfigureAwait(false);
            }

            CardMetadataSectionCollection sectionsToClone = null;
            foreach (CardTypeSchemeItem schemeItem in krCardTypeClone.SchemeItems)
            {
                Guid sectionID = schemeItem.SectionID;
                if (enhanceableSections.TryGetValue(sectionID, out CardMetadataSection metadataSection))
                {
                    metadataSection.CardTypeIDList.Add(targetCardType.ID);
                    foreach (CardMetadataColumn column in metadataSection.Columns)
                    {
                        column.CardTypeIDList.Add(targetCardType.ID);
                    }
                }
                else
                {
                    // мы на клиенте, там не все секции в текущей мете
                    CardMetadataSection section = allSections[sectionID];
                    sectionsToClone ??= new CardMetadataSectionCollection();
                    sectionsToClone.Add(section);
                }
            }

            if (sectionsToClone != null)
            {
                // клонируем все секции за раз для оптимизации, используется на клиенте в предпросмотре типов
                foreach (CardMetadataSection section in await sectionsToClone.DeepCloneAsync(cancellationToken).ConfigureAwait(false))
                {
                    section.CardTypeIDList.Add(targetCardType.ID);
                    foreach (CardMetadataColumn column in section.Columns)
                    {
                        column.CardTypeIDList.Add(targetCardType.ID);
                    }

                    enhanceableSections.Add(section);
                }
            }
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task ModifyMetadata(ICardMetadataExtensionContext context)
        {
            try
            {
                // получим типы для расширения и использования в операции модификации метаданных.
                CardType krCardType = await this.TryGetCardTypeAsync(context, DefaultCardTypes.KrCardTypeID).ConfigureAwait(false);
                if (krCardType is null)
                {
                    return;
                }
                
                CardType krStageTemplateCardType = await this.TryGetCardTypeAsync(context, DefaultCardTypes.KrStageTemplateTypeID).ConfigureAwait(false);
                if (krStageTemplateCardType is null)
                {
                    return;
                }
                
                CardType secondaryProcessCardType = await this.TryGetCardTypeAsync(context, DefaultCardTypes.KrSecondaryProcessTypeID).ConfigureAwait(false);
                if (secondaryProcessCardType is null)
                {
                    return;
                }
                
                // расширим типы и метаинформацию.
                await this.ExtendKrTypesAsync(new List<CardType> { krCardType, krStageTemplateCardType, secondaryProcessCardType, }, context).ConfigureAwait(false);
               
                // получим перечень идентификаторов типов карточек типового решения.
                var allowedCardTypeIDs = await this.GetCardTypeIDsAsync(context.CancellationToken).ConfigureAwait(false);
                if (allowedCardTypeIDs is null)
                {
                    return;
                }
                
                // получаем все типы карточек типового решения.
                var targetCardTypes = (await context.CardMetadata!.GetCardTypesAsync(context.CancellationToken).ConfigureAwait(false))
                    .Where(x => CheckCardType(x, allowedCardTypeIDs));
                
                // расширяемые секции.
                var enhanceableSections = await context.CardMetadata.GetSectionsAsync(context.CancellationToken).ConfigureAwait(false);
                // все секции.
                var allSections = await this.GetAllSectionsAsync(context).ConfigureAwait(false);

                // кэшированный тип карточки KrCard
                var cachedInfo = PrepareSourceCardType(krCardType, context);

                // расширяем все типы карточек типового решения.
                foreach (CardType targetCardType in targetCardTypes)
                {
                    if (!targetCardType.IsSealed)
                    {
                        await EnhanceAsync(cachedInfo.krCard, cachedInfo.visitor, targetCardType, enhanceableSections, allSections, context.CancellationToken).ConfigureAwait(false);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogException(ex, LogLevel.Error);
            }
        }

        #endregion
    }
}
