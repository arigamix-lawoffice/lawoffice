using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Platform.Shared.Initialization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Conditions;
using Tessa.Platform.Data;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Unity;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Расширение для процесса сохранения карточки содержащей вторичный процесс маршрута документов.
    /// </summary>
    public sealed class KrSecondaryProcessStoreExtension : KrTemplateStoreExtension
    {
        #region Fields

        private readonly ICardTransactionStrategy transactionStrategyWt;

        private readonly ICardGetStrategy getStrategy;

        private readonly IPlaceholderManager placeholderManager;

        private readonly IUnityContainer unityContainer;

        private readonly IConditionTypesProvider typesProvider;

        private readonly ICardRepairManager cardRepairManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrSecondaryProcessStoreExtension"/>.
        /// </summary>
        /// <param name="serializer">Объект предоставляющий методы для сериализации параметров этапов.</param>
        /// <param name="cardStoreStrategy">Стратегия сохранения карточки.</param>
        /// <param name="cardMetadata">Метаинформация, необходимую для использования типов карточек совместно с пакетом карточек.</param>
        /// <param name="transactionStrategyWt">Стратегия обеспечения блокировок reader/writer при выполнении операций с карточкой.</param>
        /// <param name="getStrategy">Стратегия загрузки карточки.</param>
        /// <param name="krProcessCache">Кэш для данных из карточек шаблонов этапов.</param>
        /// <param name="placeholderManager">Объект, управляющий операциями с плейсхолдерами.</param>
        /// <param name="unityContainer">Unity-контейнер.</param>
        /// <param name="typesProvider">Объект, который производит получение информации о типах условий.</param>
        /// <param name="cardRepairManager">Объект, управляющий исправлением структуры карточки, например, вследствие изменения её типа карточки.</param>
        public KrSecondaryProcessStoreExtension(
            IKrStageSerializer serializer,
            ICardStoreStrategy cardStoreStrategy,
            ICardMetadata cardMetadata,
            [Dependency(CardTransactionStrategyNames.WithoutTransaction)] ICardTransactionStrategy transactionStrategyWt,
            ICardGetStrategy getStrategy,
            IKrProcessCache krProcessCache,
            IPlaceholderManager placeholderManager,
            IUnityContainer unityContainer,
            IConditionTypesProvider typesProvider,
            ICardRepairManager cardRepairManager)
            : base(serializer, cardStoreStrategy, cardMetadata, krProcessCache)
        {
            this.transactionStrategyWt = transactionStrategyWt;
            this.getStrategy = getStrategy;
            this.placeholderManager = placeholderManager;
            this.unityContainer = unityContainer;
            this.typesProvider = typesProvider;
            this.cardRepairManager = cardRepairManager;
        }

        #endregion

        #region Base overrides

        /// <inheritdoc />
        protected override async Task<Card> GetInnerCardAsync(
            ICardStoreExtensionContext context)
        {
            var cardID = context.Request.Card.ID;
            var validationResult = context.ValidationResult;
            Card card = null;

            await this.transactionStrategyWt.ExecuteInReaderLockAsync(
                cardID,
                validationResult,
                async p =>
                {
                    var getContext = await this.getStrategy
                        .TryLoadCardInstanceAsync(
                            cardID,
                            p.DbScope.Db,
                            this.CardMetadata,
                            p.ValidationResult,
                            cancellationToken: p.CancellationToken);

                    if (getContext == null)
                    {
                        p.ReportError = true;
                        return;
                    }

                    getContext.SectionsToExclude.AddRange(
                        await this.GetKrSecondaryProcessSectionsToExcludeAsync(p.CancellationToken));

                    await this.getStrategy.LoadSectionsAsync(getContext, p.CancellationToken);

                    if (getContext.Card != null)
                    {
                        card = getContext.Card;
                    }
                    else
                    {
                        p.ReportError = true;
                    }
                },
                context.CancellationToken);

            if (!validationResult.IsSuccessful())
            {
                return null;
            }

            await this.Serializer.DeserializeSectionsAsync(card, card, cancellationToken: context.CancellationToken);
            return card;
        }

        /// <inheritdoc/>
        public override async Task BeforeRequest(ICardStoreExtensionContext context)
        {
            await base.BeforeRequest(context);

            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            if (context.Request.Card.Sections.TryGetValue(KrSecondaryProcessRoles.Name, out var secondaryProcessRolesSection))
            {
                await using (context.DbScope.Create())
                {
                    await SetIsContextForAllRolesAsync(
                        secondaryProcessRolesSection,
                        context.DbScope,
                        context.CancellationToken);
                }
            }

            await this.UpdateConditionsAsync(context);
        }

        #endregion

        #region Private methods

        private async ValueTask<Guid[]> GetKrSecondaryProcessSectionsToExcludeAsync(CancellationToken cancellationToken = default)
        {
            var stageTemplateSectionsIDs = new HashSet<Guid>(
                (await this.CardMetadata.GetCardTypesAsync(cancellationToken))[DefaultCardTypes.KrSecondaryProcessTypeID]
                    .SchemeItems
                    .Select(x => x.SectionID));

            return (await this.CardMetadata.GetSectionsAsync(cancellationToken))
                .Select(x => x.ID)
                .Where(id => stageTemplateSectionsIDs.Contains(id)
                    && id != DefaultSchemeHelper.KrStages
                    && id != DefaultSchemeHelper.KrSecondaryProcesses)
                .ToArray();
        }

        /// <summary>
        /// Устанавливает значение <see langword="true"/> в поле IsContext, если роль с идентификатором расположенным в поле RoleID является контекстной (<see cref="RoleHelper.ContextRoleTypeID"/>), иначе - <see langword="false"/>.
        /// </summary>
        /// <param name="section">Табличная секция, в которой находятся ссылочные колонки
        /// ролей, для каждой из которых нужно проставить признак IsContext.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private static async Task SetIsContextForAllRolesAsync(
            CardSection section,
            IDbScope dbScope,
            CancellationToken cancellationToken = default)
        {
            ListStorage<CardRow> rows;
            if ((rows = section.TryGetRows()) is null
                || rows.Count == 0)
            {
                return;
            }

            Dictionary<Guid, bool> isContextByRoleID = default;
            foreach (var row in rows)
            {
                if (row.State == CardRowState.Inserted
                    || row.State == CardRowState.Modified)
                {
                    if (isContextByRoleID is null)
                    {
                        isContextByRoleID = new Dictionary<Guid, bool>();
                    }

                    var roleID = row.TryGet<Guid?>("RoleID");
                    if (roleID.HasValue)
                    {
                        if (!isContextByRoleID.TryGetValue(roleID.Value, out var roleIsContext))
                        {
                            var db = dbScope.Db;
                            var builderFactory = dbScope.BuilderFactory;

                            var roleTypeID = await db
                                .SetCommand(
                                    builderFactory
                                        .Select().C("TypeID")
                                        .From("Instances").NoLock()
                                        .Where().C("ID").Equals().P("RoleID")
                                        .Build(),
                                    db.Parameter("RoleID", roleID.Value))
                                .LogCommand()
                                .ExecuteAsync<Guid>(cancellationToken);

                            roleIsContext = roleTypeID == RoleHelper.ContextRoleTypeID;
                            isContextByRoleID.Add(roleID.Value, roleIsContext);
                        }

                        row["IsContext"] = BooleanBoxes.Box(roleIsContext);
                    }
                }
            }
        }

        private async Task UpdateConditionsAsync(ICardStoreExtensionContext context)
        {
            // Алгоритм сохранения
            // 1. Проверяем наличие изменений секций с условиями. Если есть, продолжаем
            // 2. Загружаем текущие настройки и десериализуем
            // 3. Мержим изменения
            // 4. Сериализуем настройки и записываем в поле карточки
            var mainCard = context.Request.Card;
            var checkSections = new HashSet<string>() { ConditionHelper.ConditionSectionName };

            var conditionBaseType = await this.CardMetadata.GetMetadataForTypeAsync(
                ConditionHelper.ConditionsBaseTypeID,
                context.CancellationToken);
            var sections = await conditionBaseType.GetSectionsAsync(context.CancellationToken);
            checkSections.AddRange(sections.Select(x => x.Name));

            if (context.Method == CardStoreMethod.Default
                && !mainCard.Sections.Any(x => checkSections.Contains(x.Key)))
            {
                return;
            }

            string oldSettings;
            if (context.Method == CardStoreMethod.Import)
            {
                switch (mainCard.StoreMode)
                {
                    case CardStoreMode.Update:
                        if (!TryGetConditions(mainCard.Sections, out oldSettings))
                        {
                            oldSettings = await GetSettingsAsync(
                                context.DbScope,
                                mainCard.ID,
                                context.CancellationToken);
                        }
                        break;
                    case CardStoreMode.Insert:
                        _ = TryGetConditions(mainCard.Sections, out oldSettings);
                        break;
                    default:
                        throw new InvalidOperationException($"Value {mainCard.StoreMode} is not supported.");
                }
            }
            else
            {
                oldSettings = await GetSettingsAsync(
                    context.DbScope,
                    mainCard.ID,
                    context.CancellationToken);
            }

            var oldCard = new Card();
            oldCard.Sections.GetOrAdd(KrSecondaryProcesses.Name).RawFields[KrSecondaryProcesses.Conditions] = oldSettings;
            await ConditionHelper.DeserializeConditionsToEntrySectionAsync(
                oldCard,
                this.CardMetadata,
                this.typesProvider,
                KrSecondaryProcesses.Name,
                KrSecondaryProcesses.Conditions,
                false,
                context.CancellationToken);

            foreach (var section in mainCard.Sections.Values)
            {
                if (checkSections.Contains(section.Name))
                {
                    var oldSection = oldCard.Sections.GetOrAdd(section.Name);
                    oldSection.Type = section.Type;

                    CardHelper.MergeSection(section, oldSection);
                    mainCard.Sections.Remove(section.Name);
                }
            }

            var conditionsSection = oldCard.Sections.GetOrAddTable(ConditionHelper.ConditionSectionName);

            foreach (var conditionRow in conditionsSection.Rows)
            {
                await this.UpdateConditionDescriptionAsync(
                    conditionRow,
                    oldCard,
                    context.Session,
                    context.ValidationResult,
                    context.CancellationToken);

                await ConditionHelper.SerializeConditionRowAsync(
                    conditionRow,
                    oldCard,
                    DefaultCardTypes.KrSecondaryProcessTypeID,
                    this.CardMetadata,
                    this.typesProvider,
                    true,
                    context.CancellationToken);
            }

            mainCard.Sections.GetOrAdd(KrSecondaryProcesses.Name).RawFields[KrSecondaryProcesses.Conditions] =
                StorageHelper.SerializeToTypedJson((List<object>) conditionsSection.Rows.GetStorage(), false);
        }

        private static bool TryGetConditions(
            IDictionary<string, CardSection> cardSections,
            out string conditions)
        {
            if (cardSections.TryGetValue(KrSecondaryProcesses.Name, out var section)
                && section.Fields.TryGetValue(KrSecondaryProcesses.Conditions, out var conditionsObj))
            {
                conditions = (string) conditionsObj;
                return true;
            }

            conditions = default;
            return false;
        }

        private static async Task<string> GetSettingsAsync(
            IDbScope dbScope,
            Guid cardID,
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                return await db.SetCommand(
                        dbScope.BuilderFactory
                            .Select().Top(1).C(KrSecondaryProcesses.Conditions)
                            .From(KrSecondaryProcesses.Name).NoLock()
                            .Where().C(KrSecondaryProcesses.ID).Equals().P("CardID")
                            .Limit(1)
                            .Build(),
                        db.Parameter("CardID", cardID))
                        .LogCommand()
                        .ExecuteAsync<string>(cancellationToken);
            }
        }

        private async Task UpdateConditionDescriptionAsync(
            CardRow conditionRow,
            Card mainCard,
            ISession session,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            // 0. Обновляем описание только если оно не задано.
            if (conditionRow.Fields.TryGet<string>("Description") is not null)
            {
                return;
            }

            // 1. Создаем фейковую карточку
            var conditionCard = new Card { ID = Guid.NewGuid() };
            var conditionTypeID = conditionRow.TryGet<Guid?>("ConditionTypeID");

            if (!conditionTypeID.HasValue)
            {
                return;
            }

            // 2. Заполняем ее настройками текущего условия
            var conditionSettingsStorage = await ConditionHelper.RowConditionToSettingsAsync(
                conditionRow,
                mainCard,
                this.CardMetadata,
                this.typesProvider,
                cancellationToken);

            await InitializationExtensionHelper.DeserializeSettingsToSectionsByTypeAsync(
                conditionTypeID.Value,
                conditionSettingsStorage,
                conditionCard,
                this.CardMetadata,
                this.cardRepairManager,
                false,
                false,
                cancellationToken);


            // 3. Находим нужный нам тип
            var conditionType = await this.typesProvider.GetAsync(conditionTypeID.Value);

            if (conditionType is null)
            {
                return;
            }

            // 4. Создаем контекста для замены плейсхолдеров и документа
            var placeholderInfo = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                { PlaceholderHelper.SessionKey, session },
                { PlaceholderHelper.UnityContainerKey, this.unityContainer },
                { PlaceholderHelper.CardKey, conditionCard },
                { PlaceholderHelper.NoCardInDbKey, BooleanBoxes.True },

                // плейсхолдеры локализации {$Name} не заменяются, т.к. они будут фактически заменены на клиенте при отображении,
                // в зависимости от языка сотрудника, а в базе данных имя группы сохраняется вместе с таким плейсхолдером
                { PlaceholderHelper.NoLocalizationKey, BooleanBoxes.True },
            };
            var document = new StringPlaceholderDocument(conditionType.ConditionText);

            // 5. Производим расчет с помощью плейсхолдеров
            var result = await this.placeholderManager.FindAndReplaceAsync(document, placeholderInfo);
            validationResult.Add(result);

            if (result.HasErrors)
            {
                return;
            }

            // 6. Установка результата в соответствующее поле. Считаем, что обновление описание не должно помечать строку как измененную, т.к. оно всегда генерируется
            conditionRow.Fields["Description"] = document.Text;
        }

        #endregion
    }
}
