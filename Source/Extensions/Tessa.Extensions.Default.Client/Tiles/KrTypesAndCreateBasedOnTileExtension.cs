using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Platform.Client.Tiles;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Settings;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Extensions.Default.Client.Tiles
{
    /// <summary>
    /// Расширение, управляющее плиткой "Создать на основании".
    /// </summary>
    public sealed class KrTypesAndCreateBasedOnTileExtension : TileExtension
    {
        #region Constructors

        public KrTypesAndCreateBasedOnTileExtension(
            IKrTypesCache typesCache,
            IUIHost host,
            ICardMetadata cardMetadata,
            ICardRepository cardRepository,
            ISettingsProvider settingsProvider)
        {
            this.typesCache = typesCache;
            this.host = host;
            this.cardMetadata = cardMetadata;
            this.cardRepository = cardRepository;
            this.settingsProvider = settingsProvider;

            this.createBasedOnTypesLazy = new AsyncLazy<HashSet<Guid>>(
                async () =>
                {
                    ISettings settings = await this.settingsProvider.GetSettingsAsync().ConfigureAwait(false);
                    KrSettings krSettings = settings.TryGet<KrSettings>();
                    return krSettings != null ? krSettings.CreateBasedOnTypes : new HashSet<Guid>();
                });
        }

        #endregion

        #region Constants

        public const string InitialCaptionKey = "InitialCaption";

        #endregion

        #region Fields

        private readonly IKrTypesCache typesCache;
        private readonly IUIHost host;
        private readonly ICardMetadata cardMetadata;
        private readonly ICardRepository cardRepository;
        private readonly ISettingsProvider settingsProvider;

        private readonly AsyncLazy<HashSet<Guid>> createBasedOnTypesLazy;

        #endregion

        #region Private Methods

        private async void EnableIfHasDocumentCommonInfoAndNotCreating(object sender, TileEvaluationEventArgs e)
        {
            ICardEditorModel editor = e.CurrentTile.Context.CardEditor;

            var deferral = e.Defer();
            try
            {
                ICardModel model;

                e.SetIsEnabledWithCollapsing(
                    e.CurrentTile,
                    editor != null
                    && (model = editor.CardModel) != null
                    && model.Card.StoreMode != CardStoreMode.Insert
                    && !model.InSpecialMode()
                    && (await this.createBasedOnTypesLazy).Contains(model.CardType.ID));
            }
            catch (Exception ex)
            {
                deferral.SetException(ex);
            }
            finally
            {
                deferral.Dispose();
            }
        }


        private static void EnableIfNotInSpecialModeAndNotCreating(object sender, TileEvaluationEventArgs e)
        {
            ICardEditorModel editor = e.CurrentTile.Context.CardEditor;
            ICardModel model;

            e.SetIsEnabledWithCollapsing(
                e.CurrentTile,
                editor != null
                && (model = editor.CardModel) != null
                && model.Card.StoreMode != CardStoreMode.Insert
                && !model.InSpecialMode());
        }


        /// <summary>
        /// Считает количество доступных для создания типов карточек и документов (опционально) для группы
        /// типов карточек
        /// </summary>
        /// <param name="cache">Кэш типов Kr</param>
        /// <param name="metadata">Метаинформация</param>
        /// <param name="unavailableTypes">Недоступные типы</param>
        /// <param name="forGroup">Опциональный признак группы типов карточек</param>
        /// <param name="checkKrTypesOnly">
        /// Признак того, что проверяются только типы, включённые в типовое решение.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Количество доступных для создания типов</returns>
        private static async ValueTask<int> CountTypesAsync(
            IKrTypesCache cache,
            ICardMetadata metadata,
            IList<Guid> unavailableTypes,
            string forGroup = null,
            bool checkKrTypesOnly = false,
            CancellationToken cancellationToken = default)
        {
            bool hasGroup = !string.IsNullOrWhiteSpace(forGroup);

            int result = 0;
            foreach (CardType cardType in await metadata.GetCardTypesAsync(cancellationToken).ConfigureAwait(false))
            {
                //Скрытые типы карточек не учитываем
                if (cardType.Flags.Has(CardTypeFlags.Hidden))
                {
                    continue;
                }

                //Если нужно посчитать количество типов для группы типов карточек
                if (hasGroup && cardType.Group != forGroup)
                {
                    continue;
                }

                KrComponents components = await KrComponentsHelper.GetKrComponentsAsync(cardType.ID, cache, cancellationToken);
                if (components.Has(KrComponents.DocTypes))
                {
                    //Если тип карточки использует типы документов - считаем все типы документов
                    //для этого типа карточки, которые не указаны как недоступные
                    result += (await cache.GetDocTypesAsync(cancellationToken).ConfigureAwait(false))
                        .Count(x => x.CardTypeID == cardType.ID
                                && unavailableTypes.All(typeID => typeID != x.ID));
                }
                else if (!checkKrTypesOnly || components.Has(KrComponents.Base))
                {
                    //Если тип карточки не использует типы документов - считаем его, если он не
                    //указан как недоступный
                    if (unavailableTypes.All(x => x != cardType.ID))
                    {
                        result++;
                    }
                }
            }

            return result;
        }


        private async Task InitializeKrTilesGlobalAsync(ITileGlobalExtensionContext context, IList<Guid> unavailableTypes)
        {
            ITilePanel panel = context.Workspace.RightPanel;
            ITile createCard = panel.Tiles.TryGet(TileNames.CreateCard);
            if (createCard == null)
            {
                return;
            }

            var tileIndicesToRemove = new List<int>();
            var groupIndicesToRemove = new List<int>();

            ISettings settings = await this.settingsProvider.GetSettingsAsync(context.CancellationToken).ConfigureAwait(false);
            int maxTilesCountInGroupTile = settings.MaxDocTypeTilesWithoutGroupingByCardType;
            IKrType[] types = (await this.typesCache.GetTypesAsync(context.CancellationToken).ConfigureAwait(false))
                .OrderByLocalized(x => x.Caption).ToArray();
            int groupIndex = -1;

            TileCollection groupTileCollection = createCard.Tiles;
            foreach (ITile groupTile in groupTileCollection)
            {
                groupIndex++;
                tileIndicesToRemove.Clear();

                //Если будет отображаться <= maxTilesCountInGroupTile типов карточек/документов,
                //то список будет одноуровневым поэтому сначала подсчитаем итоговое (с учетом прав)
                //число типов карточек/документов
                if (await CountTypesAsync(this.typesCache, this.cardMetadata, unavailableTypes, groupTile.Name, cancellationToken: context.CancellationToken).ConfigureAwait(false)
                    > maxTilesCountInGroupTile)
                {
                    TileCollection tilesInGroup = groupTile.Tiles;
                    bool hasVisibleTypeTile = false;
                    int index = -1;

                    //Группируем по типам
                    foreach (ITile typeTile in tilesInGroup)
                    {
                        index++;

                        typeTile.Caption = TileHelper.SplitCaption(typeTile.Caption);

                        if ((await this.cardMetadata.GetCardTypesAsync(context.CancellationToken).ConfigureAwait(false))
                            .TryGetValue(typeTile.Name, out CardType cardType))
                        {
                            //Если для типа карточки не используются типы документов - не будет генерить под них тайлы
                            if ((await KrComponentsHelper.GetKrComponentsAsync(cardType.ID, this.typesCache, context.CancellationToken))
                                .HasNot(KrComponents.DocTypes))
                            {
                                //Если тип карточки при этом не доступен к созданию - скроем
                                if (unavailableTypes.Any(x => x == cardType.ID))
                                {
                                    tileIndicesToRemove.Add(index);
                                }
                                else
                                {
                                    hasVisibleTypeTile = true;
                                }
                            }
                            //Если для типа карточки используются типы документов
                            else
                            {
                                //Уберем возможность создания карточки по тайлу типа карточки
                                typeTile.Command = DelegateCommand.Empty;

                                //Добавим типы документов в тайл типа карточки
                                this.AddDocTypeTiles(
                                    context,
                                    types,
                                    panel,
                                    typeTile.Tiles,
                                    unavailableTypes,
                                    cardType.ID);

                                //Если используем типы документов и не добавили ни одного типа документа
                                //скроем тип карточки
                                if (typeTile.Tiles.Count == 0)
                                {
                                    tileIndicesToRemove.Add(index);
                                }
                                else
                                {
                                    hasVisibleTypeTile = true;
                                }
                            }
                        }
                    }

                    // если все типы карточек были скрыты, то скрываем группу типов
                    if (!hasVisibleTypeTile)
                    {
                        groupIndicesToRemove.Add(groupIndex);
                    }
                    else if (tileIndicesToRemove.Count > 0)
                    {
                        tileIndicesToRemove.Reverse();

                        foreach (int i in tileIndicesToRemove)
                        {
                            tilesInGroup.RemoveAt(i);
                        }

                        int order = 0;
                        foreach (ITile tile in tilesInGroup)
                        {
                            tile.Order = order++;
                        }
                    }
                }
                else
                {
                    bool hasDocTypes = false;

                    //Не группируем по типам
                    TileCollection groupTiles = groupTile.Tiles;
                    int index = -1;

                    // при добавлении типов документа плитка, создающая тип, удаляется и тут же заменяется на плитку группу,
                    // поэтому итерацию выполняем по коллекции-копии
                    foreach (ITile typeTile in groupTiles.ToArray())
                    {
                        index++;

                        typeTile.Caption = TileHelper.SplitCaption(typeTile.Caption);

                        if ((await this.cardMetadata.GetCardTypesAsync(context.CancellationToken).ConfigureAwait(false))
                            .TryGetValue(typeTile.Name, out CardType cardType))
                        {
                            //Если для типа карточки не используются типы документов - не будет генерить
                            //под них тайлы
                            if ((await KrComponentsHelper.GetKrComponentsAsync(cardType.ID, this.typesCache, context.CancellationToken))
                                    .HasNot(KrComponents.DocTypes))
                            {
                                //Если тип карточки при этом не доступен к созданию - скроем
                                if (unavailableTypes.Any(x => x == cardType.ID))
                                {
                                    tileIndicesToRemove.Add(index);
                                }
                            }
                            else
                            {
                                //Тип карточки использует типы документов, поэтому удалим его тайл из тайла группы
                                hasDocTypes = true;

                                tileIndicesToRemove.Add(index);

                                this.AddDocTypeTiles(
                                    context,
                                    types,
                                    panel,
                                    groupTiles,
                                    unavailableTypes,
                                    cardType.ID);
                            }
                        }
                    }

                    // удаляем типы карточки, которые надо удалить
                    tileIndicesToRemove.Reverse();

                    foreach (int i in tileIndicesToRemove)
                    {
                        groupTiles.RemoveAt(i);
                    }

                    if (groupTiles.Count == 0)
                    {
                        groupIndicesToRemove.Add(groupIndex);
                    }
                    else if (hasDocTypes)
                    {
                        //Упорядочиваем тайлы
                        int order = 0;
                        foreach (ITile typeTile in groupTiles.OrderByLocalized(x => x.Caption))
                        {
                            switch (typeTile.Name)
                            {
                                case TileNames.OtherTypes:
                                    typeTile.Order = int.MaxValue;
                                    break;

                                default:
                                    typeTile.Order = order++;
                                    break;
                            }
                        }
                    }
                }
            }

            // удаляем группы типов карточек, которые надо удалить
            if (groupIndicesToRemove.Count > 0)
            {
                groupIndicesToRemove.Reverse();

                foreach (int i in groupIndicesToRemove)
                {
                    groupTileCollection.RemoveAt(i);
                }

                int order = 0;
                foreach (ITile tile in groupTileCollection)
                {
                    tile.Order = order++;
                }
            }
        }


        private async Task InitializeKrCreateBasedOnTilesAsync(
            ITileGlobalExtensionContext context,
            IList<Guid> unavailableTypes)
        {
            ITileContextSource contextSource = context.Workspace.LeftPanel;

            //Добавляем корневой тайл создать на основании
            ITile createBasedOnTile =
                new Tile(
                    TileNames.CreateCardBasedOn,
                    TileHelper.SplitCaption("$KrTiles_CreateBasedOn"),
                    context.Icons.Get("Thin2"),
                    contextSource,
                    DelegateCommand.Empty,
                    order: 100,
                    toolTip: "$KrTiles_CreateBasedOnTooltip",
                    evaluating: this.EnableIfHasDocumentCommonInfoAndNotCreating
                    );

            //Если будет отображаться <= maxTilesCountInTypeTile типов карточек/документов,
            //то список будет одноуровневым поэтому сначала подсчитаем итоговое (с учетом прав)
            //число типов карточек/документов
            int count = await CountTypesAsync(
                this.typesCache, this.cardMetadata, unavailableTypes, checkKrTypesOnly: true,
                cancellationToken: context.CancellationToken).ConfigureAwait(false);

            ISettings settings = await this.settingsProvider.GetSettingsAsync(context.CancellationToken).ConfigureAwait(false);
            int maxWithoutGroupingCount = settings.MaxDocTypeTilesWithoutGroupingByCardType;

            if (count > maxWithoutGroupingCount)
            {
                await this.CreateBasedOnTilesAndGroupAsync(context, unavailableTypes, contextSource, createBasedOnTile).ConfigureAwait(false);
            }
            else
            {
                await this.CreateBasedOnTilesWithoutGroupingAsync(context, unavailableTypes, contextSource, createBasedOnTile).ConfigureAwait(false);
            }

            if (createBasedOnTile.Tiles.Count > 0)
            {
                context.Workspace.LeftPanel.Tiles[TileNames.CardOthers].Tiles.Add(createBasedOnTile);
            }
        }


        /// <summary>
        /// Добавляет в указанную parentTileCollection тайлы типов документов для указанного typeName
        /// имени типа карточки
        /// </summary>
        /// <param name="context">Контекст расширения</param>
        /// <param name="docTypes">Типы документов</param>
        /// <param name="contextSource">Источник контекста для плиток</param>
        /// <param name="parentTileCollection">Родительская коллекция плиток</param>
        /// <param name="unavailableTypes">Недоступные типы карточек и документов</param>
        /// <param name="cardTypeID">Идентификатор типа карточки</param>
        private void AddDocTypeTiles(
            ITileGlobalExtensionContext context,
            IEnumerable<IKrType> docTypes,
            ITileContextSource contextSource,
            TileCollection parentTileCollection,
            IList<Guid> unavailableTypes,
            Guid cardTypeID)
        {
            KrDocType[] docTypesForCardType =
                docTypes
                    .OfType<KrDocType>()
                    .Where(x => x.CardTypeID == cardTypeID)
                    .ToArray();

            var tiles = new List<ITile>();

            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < docTypesForCardType.Length; i++)
            {
                Guid docTypeID = docTypesForCardType[i].ID;

                //Если тип документа недоступен - не будем генерить под него тайл
                if (unavailableTypes.Any(x => x == docTypeID))
                {
                    continue;
                }

                string docTypeTitle = docTypesForCardType[i].Name;
                string docTypeCaption = TileHelper.SplitCaption(docTypesForCardType[i].Caption);

                //Создаем тайл с именем и отображаемым названием соотв. подтипу
                ITile docTypeTile = new Tile(
                    docTypeTitle,
                    docTypeCaption,
                    Icon.Empty,
                    contextSource,
                    //Создание карточки указаного поддтипа
                    new DelegateCommand(async p =>
                    {
                        var tile = (ITile)p;
                        using ISplash splash = TessaSplash.Create(TessaSplashMessage.CreatingCard);
                        await this.host.CreateCardAsync(
                            cardTypeID,
                            options: new CreateCardOptions
                            {
                                UIContext = tile.Context,
                                Splash = splash,
                                Info = new Dictionary<string, object>
                                {
                                    { "docTypeID", docTypeID },
                                    { "docTypeTitle", docTypeTitle },
                                },
                            });
                    }),
                    size: TileSize.Half);

                tiles.Add(docTypeTile);
            }

            tiles.Sort((x, y) => string.Compare(x.Caption, y.Caption, StringComparison.Ordinal));

            int order = 0;
            TileCollection parentTiles = parentTileCollection;

            foreach (ITile tile in tiles)
            {
                tile.Order = order++;
                parentTiles.Add(tile);
            }
        }


        private async Task CreateBasedOnTilesWithoutGroupingAsync(
            ITileGlobalExtensionContext context,
            IList<Guid> unavailableTypes,
            ITileContextSource contextSource,
            ITile createBasedOnTile)
        {
            // этот быстрый и упрощённый метод добавления типов "единым списком" без группировки, используется в типовой поставке

            int order = 0;
            TileCollection tileCollection = createBasedOnTile.Tiles;

            ISettings settings = await this.settingsProvider.GetSettingsAsync(context.CancellationToken).ConfigureAwait(false);
            int maxTiles = settings.MaxCardTypeTilesBeforeAddingSubgroup;
            CardTypeCollection metadataCardTypes = await this.cardMetadata.GetCardTypesAsync(context.CancellationToken).ConfigureAwait(false);

            foreach (IKrType type in (await this.typesCache.GetTypesAsync().ConfigureAwait(false))
                .OrderByLocalized(x => x.Caption))
            {
                if (type is KrCardType cardType)
                {
                    if (cardType.UseDocTypes
                        || unavailableTypes.Any(x => x == cardType.ID))
                    {
                        continue;
                    }

                    if (!metadataCardTypes.TryGetValue(cardType.ID, out CardType metadataCardType)
                        || metadataCardType.Flags.Has(CardTypeFlags.Hidden))
                    {
                        continue;
                    }

                    ExtensionTileHelper.CreateMoreTileIfNecessary(
                        maxTiles,
                        contextSource,
                        ref order,
                        ref tileCollection);

                    tileCollection.Add(
                        CreateBasedOnCardTypeTile(
                            context,
                            contextSource,
                            ref order,
                            cardType));
                }
                else
                {
                    if (!(type is KrDocType docType)
                        || unavailableTypes.Any(x => x == docType.ID))
                    {
                        continue;
                    }

                    if (!metadataCardTypes.TryGetValue(docType.CardTypeID, out CardType metadataCardType)
                        || metadataCardType.Flags.Has(CardTypeFlags.Hidden))
                    {
                        continue;
                    }

                    ExtensionTileHelper.CreateMoreTileIfNecessary(
                        maxTiles,
                        contextSource,
                        ref order,
                        ref tileCollection);

                    tileCollection.Add(
                        CreateBasedOnDocTypeTile(
                            context,
                            contextSource,
                            ref order,
                            docType));
                }
            }
        }


        private async Task CreateBasedOnTilesAndGroupAsync(
            ITileGlobalExtensionContext context,
            IList<Guid> unavailableTypes,
            ITileContextSource contextSource,
            ITile createBasedOnTile)
        {
            TileCollection createBasedOnTileCollection = createBasedOnTile.Tiles;

            ISettings settings = await this.settingsProvider.GetSettingsAsync(context.CancellationToken).ConfigureAwait(false);
            int maxTiles = settings.MaxCardTypeTilesBeforeAddingSubgroup;

            int groupingTileOrder = 0;
            IReadOnlyList<KrCardType> cardTypes = await this.typesCache.GetCardTypesAsync(context.CancellationToken).ConfigureAwait(false);

            var groupedCardTypes = new List<(KrCardType krCardType, string groupCaption)>(cardTypes.Count);
            foreach (KrCardType type in cardTypes)
            {
                string groupCaption = await this.GetCardTypeGroupLocalizedCaptionAsync(context, type).ConfigureAwait(false);
                groupedCardTypes.Add((type, groupCaption));
            }

            foreach (var grouping in groupedCardTypes
                .GroupBy(x => x.groupCaption)
                .OrderBy(x => x.Key == null)
                .ThenBy(x => x.Key))
            {
                var groupingTile = new Tile(
                    grouping.Key ?? TileNames.OtherTypes,
                    grouping.Key ?? CreateCardTileExtension.UnknownGroupCaption,
                    Icon.Empty,
                    contextSource,
                    DelegateCommand.Empty,
                    null,
                    groupingTileOrder++,
                    TileSize.Half);

                groupingTile.Info[CreateCardTileExtension.IsGroupKey] = BooleanBoxes.True;
                TileCollection groupingTileCollection = groupingTile.Tiles;

                var tilesInGroup = new List<ITile>();

                int tileOrder = 0;
                foreach (KrCardType krCardType in grouping.Select(x => x.krCardType).OrderByLocalized(x => x.Caption))
                {
                    CardTypeCollection metadataCardTypes = await this.cardMetadata.GetCardTypesAsync(context.CancellationToken).ConfigureAwait(false);

                    if (!metadataCardTypes.TryGetValue(krCardType.ID, out CardType metadataCardType)
                        || metadataCardType.Flags.Has(CardTypeFlags.Hidden))
                    {
                        continue;
                    }

                    // чтобы не брать переменную из foreach в замыкания ниже
                    KrCardType krCardTypeClosure = krCardType;

                    if (krCardType.UseDocTypes)
                    {
                        ITile typeTile = new Tile(
                            ".basedOn" + krCardType.Name,
                            TileHelper.SplitCaption(krCardType.Caption),
                            Icon.Empty,
                            contextSource,
                            DelegateCommand.Empty,
                            order: tileOrder++,
                            size: TileSize.Half,
                            evaluating: EnableIfNotInSpecialModeAndNotCreating);

                        typeTile.Info[InitialCaptionKey] = krCardType.Caption;

                        bool hasAvailableDocTypes = true;

                        int innerOrder = 0;
                        TileCollection typeTileCollection = typeTile.Tiles;

                        foreach (KrDocType docType in (await this.typesCache.GetDocTypesAsync(context.CancellationToken).ConfigureAwait(false))
                            .OrderByLocalized(x => x.Caption))
                        {
                            if (docType.CardTypeID != krCardType.ID
                                || unavailableTypes.Any(x => x == docType.ID))
                            {
                                continue;
                            }

                            ITile basedOnDocType = CreateBasedOnDocTypeTile(
                                context,
                                contextSource,
                                ref innerOrder,
                                docType);

                            tilesInGroup.Add(basedOnDocType);
                            typeTileCollection.Add(basedOnDocType);
                            hasAvailableDocTypes = false;
                        }

                        if (!hasAvailableDocTypes)
                        {
                            groupingTileCollection.Add(typeTile);
                        }
                    }
                    else
                    {
                        if (unavailableTypes.Any(u => u == krCardType.ID))
                        {
                            //создание этого типа карточки недоступно
                            continue;
                        }

                        ITile basedOnCardType =
                            CreateBasedOnCardTypeTile(
                                context,
                                contextSource,
                                ref tileOrder,
                                krCardTypeClosure);

                        tilesInGroup.Add(basedOnCardType);
                        groupingTileCollection.Add(basedOnCardType);
                    }
                }

                // если плиток с создаваемыми типами карточек и документов мало в пределах группы типов,
                // то превращаем их в плоский список
                if (tilesInGroup.Count > 0 && tilesInGroup.Count <= maxTiles)
                {
                    groupingTileCollection.Clear();

                    int order = 0;
                    foreach (ITile tile in tilesInGroup.OrderByLocalized(x => x.Info.Get<string>(InitialCaptionKey)))
                    {
                        tile.Order = order++;
                        groupingTileCollection.Add(tile);
                    }
                }

                // если в группе появилась хотя бы одна плитка, то добавляем её
                if (groupingTileCollection.Count > 0)
                {
                    createBasedOnTileCollection.Add(groupingTile);
                }
            }

            // на "верхнем" уровне гарантированно есть группировка, для добавления без группировки есть
            // другой метод CreateBasedOnTilesAndGroup, поэтому выполнять "разгруппировку" верхнеуровневых плиток не будем

            // добавляем плитку "ещё" на любых уровнях вложенности, где это нужно
            ExtensionTileHelper.RegroupAndAddMoreIfNecessary(createBasedOnTileCollection, contextSource, maxTiles);
        }


        private async Task<string> GetCardTypeGroupLocalizedCaptionAsync(
            ITileGlobalExtensionContext context,
            KrCardType krCardType)
        {
            string groupCaption = (await this.cardMetadata.GetCardTypesAsync(context.CancellationToken).ConfigureAwait(false))
                .TryGetValue(krCardType.ID, out CardType cardType)
                ? cardType.Group
                : null;

            return string.IsNullOrWhiteSpace(groupCaption)
                ? null
                : LocalizationManager.Localize(
                    context.Workspace.GetTypeGroupCaption(groupCaption) ?? groupCaption);
        }


        private Tile CreateBasedOnDocTypeTile(
            ITileGlobalExtensionContext context,
            ITileContextSource contextSource,
            ref int order,
            KrDocType docType)
        {
            var result = new Tile(
                ".basedOn" + docType.Name,
                TileHelper.SplitCaption(docType.Caption),
                Icon.Empty,
                contextSource,
                new DelegateCommand(p =>
                {
                    bool copyFiles = TileContext.Current.KeyboardModifiers.Has(ModifierKeys.Shift);
                    var tile = (ITile)p;

                    this.CreateBasedOnTileActionAsync(tile, docType.CardTypeID, docType.CardTypeName, copyFiles,
                        info =>
                        {
                            info["docTypeID"] = docType.ID;
                            info["docTypeTitle"] = docType.Name;
                        });
                }),
                order: order++,
                size: TileSize.Half,
                evaluating: EnableIfNotInSpecialModeAndNotCreating);

            result.Info[InitialCaptionKey] = docType.Caption;
            return result;
        }


        private Tile CreateBasedOnCardTypeTile(
            ITileGlobalExtensionContext context,
            ITileContextSource contextSource,
            ref int order,
            KrCardType cardType)
        {
            var result = new Tile(
                ".basedOn" + cardType.Name,
                TileHelper.SplitCaption(cardType.Caption),
                Icon.Empty,
                contextSource,
                new DelegateCommand(p =>
                {
                    bool copyFiles = TileContext.Current.KeyboardModifiers.Has(ModifierKeys.Shift);
                    var tile = (ITile)p;

                    this.CreateBasedOnTileActionAsync(tile, cardType.ID, cardType.Name, copyFiles);
                }),
                order: order++,
                size: TileSize.Half,
                evaluating: EnableIfNotInSpecialModeAndNotCreating);

            result.Info[InitialCaptionKey] = cardType.Caption;
            return result;
        }


        private async void CreateBasedOnTileActionAsync(
            ITile tile,
            Guid cardTypeID,
            string cardTypeName,
            bool copyFiles,
            Action<Dictionary<string, object>> initializeRequestAction = null)
        {
            IUIContext uiContext = tile.Context;
            ICardEditorModel editor = uiContext.CardEditor;
            ICardModel model = editor?.CardModel;
            if (model == null)
            {
                return;
            }

            bool saveBeforeCreatingCard;
            if (await model.HasChangesAsync())
            {
                bool? dialogResult = TessaDialog.ConfirmWithCancel("$UI_Cards_ConfirmSavingCardBeforeCreatingBasedOn");
                if (!dialogResult.HasValue)
                {
                    return;
                }

                saveBeforeCreatingCard = dialogResult.Value;
            }
            else
            {
                saveBeforeCreatingCard = false;
            }

            var requestInfo = new Dictionary<string, object>();
            initializeRequestAction?.Invoke(requestInfo);

            Func<Card, Task> createActionAsync = async baseCard =>
            {
                KrCreateBasedOnHelper.InitializeRequestInfo(requestInfo, baseCard, copyFiles);

                using ISplash splash = TessaSplash.Create(TessaSplashMessage.CreatingCard);
                await this.host.CreateCardAsync(
                    cardTypeID,
                    cardTypeName,
                    options: new CreateCardOptions
                    {
                        UIContext = tile.Context,
                        Splash = splash,
                        CreationModeDisplayText = "$UI_Tiles_CreateCard_Suffix_BasedOn",
                        Info = requestInfo,
                    });
            };

            if (saveBeforeCreatingCard)
            {
                if (await editor.SaveCardAsync(uiContext))
                {
                    await createActionAsync(editor.CardModel?.Card);
                }
            }
            else
            {
                await createActionAsync(editor.CardModel?.Card);
            }
        }

        #endregion

        #region Base Overrides

        public override async Task InitializingGlobal(ITileGlobalExtensionContext context)
        {
            var (unavailableTypes, result) = await KrTileHelper.GetUnavailableTypesAsync(this.cardRepository, this.typesCache).ConfigureAwait(false);
            await TessaDialog.ShowNotEmptyAsync(result);
            await InitializeKrTilesGlobalAsync(context, unavailableTypes).ConfigureAwait(false);
            await InitializeKrCreateBasedOnTilesAsync(context, unavailableTypes).ConfigureAwait(false);
        }

        #endregion
    }
}
