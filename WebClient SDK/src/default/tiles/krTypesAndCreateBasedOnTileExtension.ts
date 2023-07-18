import {
  TileExtension,
  ITileGlobalExtensionContext,
  TileContextSource,
  ITile,
  Tile,
  TileEvaluationEventArgs
} from 'tessa/ui/tiles';
import {
  KrTypesCache,
  getKrComponentsTypesCache,
  KrComponents,
  KrDocType,
  KrCardType,
  KrToken
} from 'tessa/workflow';
import { LocalizationManager } from 'tessa/localization';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { hasFlag, hasNotFlag, createTypedField, DotNetType } from 'tessa/platform';
import { CardTypeFlags } from 'tessa/cards/types';
import { MetadataStorage } from 'tessa';
import { createCard } from 'tessa/ui/uiHost';
import { ICardModel } from 'tessa/ui/cards';
import { CardStoreMode } from 'tessa/cards';
import { tryGetFromInfo, showConfirmWithCancel, LoadingOverlay } from 'tessa/ui';
import { IStorage } from 'tessa/platform/storage';

export class KrTypesAndCreateBasedOnTileExtension extends TileExtension {
  //#region TileExtension

  public initializingGlobal(context: ITileGlobalExtensionContext) {
    const unavailableTypes = KrTypesCache.instance.unavailableTypes;
    KrTypesAndCreateBasedOnTileExtension.initializeKrTilesGlobal(context, unavailableTypes);
    KrTypesAndCreateBasedOnTileExtension.initializeKrCreateBasedOnTiles(context, unavailableTypes);
  }

  //#endregion

  //#region evaluatings

  private static enableIfHasDocumentCommonInfoAndNotCreating(e: TileEvaluationEventArgs) {
    const editor = e.currentTile.context.cardEditor;
    let model: ICardModel;

    e.setIsEnabledWithCollapsing(
      e.currentTile,
      !!editor &&
        !!(model = editor.cardModel!) &&
        model.card.storeMode !== CardStoreMode.Insert &&
        !model.inSpecialMode &&
        MetadataStorage.instance.commonMetadata.createBasedOnTypes.some(
          x => x === model.cardType.id
        )
    );
  }

  private static enableIfNotInSpecialModeAndNotCreating(e: TileEvaluationEventArgs) {
    const editor = e.currentTile.context.cardEditor;
    let model: ICardModel;

    e.setIsEnabledWithCollapsing(
      e.currentTile,
      !!editor &&
        !!(model = editor.cardModel!) &&
        model.card.storeMode !== CardStoreMode.Insert &&
        !model.inSpecialMode
    );
  }

  //#endregion

  //#region methods

  private static initializeKrTilesGlobal(
    context: ITileGlobalExtensionContext,
    unavailableTypes: Set<guid>
  ) {
    const panel = context.workspace.rightPanel;
    const createCard = panel.tryGetTile('CreateCard');
    if (!createCard) {
      return;
    }

    const typesCache = KrTypesCache.instance;
    const cardMetadata = MetadataStorage.instance.cardMetadata;
    const tileIndicesToRemove: number[] = [];
    const groupIndicesToRemove: number[] = [];

    const maxTilesCountInGroupTile = 10; // TODO: получать из settings
    const types = typesCache.types
      .map(x => x)
      .sort((a, b) => LocalizationManager.instance.sortByLocalized(a.caption, b.caption));
    let groupIndex = -1;

    for (const groupTile of createCard.tiles) {
      groupIndex++;
      tileIndicesToRemove.length = 0;

      // Если будет отображаться <= maxTilesCountInGroupTile типов карточек/документов,
      // то список будет одноуровневым поэтому сначала подсчитаем итоговое (с учетом прав)
      // число типов карточек/документов
      const count = KrTypesAndCreateBasedOnTileExtension.countTypes(
        typesCache,
        cardMetadata,
        unavailableTypes,
        groupTile.name
      );
      if (count > maxTilesCountInGroupTile) {
        const tilesInGroup = groupTile.tiles;
        let hasVisibleTypeTile = false;
        let index = -1;

        // Группируем по типам
        for (const typeTile of tilesInGroup) {
          index++;

          const cardType = cardMetadata.getCardTypeByName(typeTile.name);
          if (cardType) {
            // Если для типа карточки не используются типы документов - не будет генерить под них тайлы
            const components = getKrComponentsTypesCache(cardType.id!, typesCache);
            if (hasNotFlag(components, KrComponents.DocTypes)) {
              if (unavailableTypes.has(cardType.id!)) {
                tileIndicesToRemove.push(index);
              } else {
                hasVisibleTypeTile = true;
              }
            } else {
              // Если для типа карточки используются типы документов
              // Уберем возможность создания карточки по тайлу типа карточки
              typeTile.command = null;
              KrTypesAndCreateBasedOnTileExtension.addDocTypeTiles(
                types as KrDocType[],
                panel.contextSource,
                typeTile.tiles,
                unavailableTypes,
                cardType.id!
              );

              // Если используем типы документов и не добавили ни одного типа документа
              // скроем тип карточки
              if (typeTile.tiles.length === 0) {
                tileIndicesToRemove.push(index);
              } else {
                hasVisibleTypeTile = true;
              }
            }
          }
        }

        // если все типы карточек были скрыты, то скрываем группу типов
        if (!hasVisibleTypeTile) {
          groupIndicesToRemove.push(groupIndex);
        } else if (tileIndicesToRemove.length > 0) {
          tileIndicesToRemove.reverse();

          for (const i of tileIndicesToRemove) {
            tilesInGroup.splice(i, 1);
          }

          let order = 0;
          for (const tile of tilesInGroup) {
            tile.order = order++;
          }
        }
      } else {
        let hasDocTypes = false;

        // Не группируем по типам
        const groupTiles = groupTile.tiles;
        let index = -1;

        // при добавлении типов документа плитка, создающая тип, удаляется и тут же заменяется на плитку группу,
        // поэтому итерацию выполняем по коллекции-копии
        for (const typeTile of groupTiles.map(x => x)) {
          index++;

          const cardType = cardMetadata.getCardTypeByName(typeTile.name);
          if (cardType) {
            // Если для типа карточки не используются типы документов - не будет генерить
            // под них тайлы
            const components = getKrComponentsTypesCache(cardType.id!, typesCache);
            if (hasNotFlag(components, KrComponents.DocTypes)) {
              // Если тип карточки при этом не доступен к созданию - скроем
              if (unavailableTypes.has(cardType.id!)) {
                tileIndicesToRemove.push(index);
              }
            } else {
              // Тип карточки использует типы документов, поэтому удалим его тайл из тайла группы
              hasDocTypes = true;

              tileIndicesToRemove.push(index);
              KrTypesAndCreateBasedOnTileExtension.addDocTypeTiles(
                types as KrDocType[],
                panel.contextSource,
                groupTiles,
                unavailableTypes,
                cardType.id!
              );
            }
          }
        }

        // удаляем типы карточки, которые надо удалить
        tileIndicesToRemove.reverse();

        for (const i of tileIndicesToRemove) {
          groupTiles.splice(i, 1);
        }

        if (groupTiles.length === 0) {
          groupIndicesToRemove.push(groupIndex);
        } else if (hasDocTypes) {
          let order = 0;
          const orderedTiles = groupTiles
            .map(x => x)
            .sort((a, b) => LocalizationManager.instance.sortByLocalized(a.caption, b.caption));
          for (const typeTile of orderedTiles) {
            if (typeTile.name === 'Other') {
              typeTile.order = Number.MAX_SAFE_INTEGER;
            } else {
              typeTile.order = order++;
            }
          }
        }
      }
    }

    // удаляем группы типов карточек, которые надо удалить
    if (groupIndicesToRemove.length > 0) {
      groupIndicesToRemove.reverse();

      for (const i of groupIndicesToRemove) {
        createCard.tiles.splice(i, 1);
      }

      let order = 0;
      for (const tile of createCard.tiles) {
        tile.order = order++;
      }
    }
  }

  private static countTypes(
    cache: KrTypesCache,
    metadata: CardMetadataSealed,
    unavailableTypes: Set<guid>,
    forGroup: string | null = null,
    checkKrTypesOnly = false
  ): number {
    const hasGroup = !!forGroup;
    if (hasGroup) {
      forGroup = forGroup!.toLocaleLowerCase();
    }

    let result = 0;
    for (const cardType of metadata.cardTypes) {
      // Скрытые типы карточек не учитываем
      if (hasFlag(cardType.flags, CardTypeFlags.Hidden)) {
        continue;
      }

      // Если нужно посчитать количество типов для группы типов карточек
      const groupName = cardType.group ? cardType.group.toLocaleLowerCase() : '';
      if (hasGroup && groupName !== forGroup) {
        continue;
      }

      const components = getKrComponentsTypesCache(cardType.id!, cache);
      if (hasFlag(components, KrComponents.DocTypes)) {
        // Если тип карточки использует типы документов - считаем все типы документов
        // для этого типа карточки, которые не указаны как недоступные
        result += cache.docTypes.filter(
          x => x.cardTypeId === cardType.id && !unavailableTypes.has(x.id)
        ).length;
      } else if (!checkKrTypesOnly || hasFlag(components, KrComponents.Base)) {
        // Если тип карточки не использует типы документов - считаем его, если он не
        // указан как недоступный
        if (!unavailableTypes.has(cardType.id!)) {
          result++;
        }
      }
    }

    return result;
  }

  private static addDocTypeTiles(
    docTypes: ReadonlyArray<KrDocType>,
    contextSource: TileContextSource,
    parentTiles: ITile[],
    unavailableTypes: Set<guid>,
    typeId: guid
  ) {
    const docTypesForCardType = docTypes.filter(x => x.cardTypeId === typeId);
    const tiles: ITile[] = [];

    for (let i = 0; i < docTypesForCardType.length; i++) {
      const docTypeId = docTypesForCardType[i].id;

      // Если тип документа недоступен - не будем генерить под него тайл
      if (unavailableTypes.has(docTypeId)) {
        continue;
      }

      const docTypeName = docTypesForCardType[i].name;
      const docTypeCaption = docTypesForCardType[i].caption;

      // Создаем тайл с именем и отображаемым названием соотв. подтипу
      const docTypeTile = new Tile({
        name: docTypeName,
        caption: docTypeCaption,
        icon: '',
        contextSource,
        command: async tile => {
          await LoadingOverlay.instance.show(async splashResolve => {
            await createCard({
              cardTypeId: typeId,
              context: tile.context,
              info: {
                docTypeID: createTypedField(docTypeId, DotNetType.Guid),
                docTypeTitle: createTypedField(docTypeName, DotNetType.String)
              },
              splashResolve
            });
          });
        }
      });

      tiles.push(docTypeTile);
    }

    tiles.sort((a, b) => a.caption.localeCompare(b.caption));

    let order = 0;
    for (const tile of tiles) {
      tile.order = order++;
      parentTiles.push(tile);
    }
  }

  private static initializeKrCreateBasedOnTiles(
    context: ITileGlobalExtensionContext,
    unavailableTypes: Set<guid>
  ) {
    const contextSource = context.workspace.leftPanel.contextSource;
    const typesCache = KrTypesCache.instance;
    const cardMetadata = MetadataStorage.instance.cardMetadata;

    // Добавляем корневой тайл создать на основании
    const createBasedOnTile = new Tile({
      name: 'CreateCardBasedOn',
      caption: '$KrTiles_CreateBasedOn',
      icon: 'ta icon-thin-002',
      contextSource,
      order: 100,
      evaluating: KrTypesAndCreateBasedOnTileExtension.enableIfHasDocumentCommonInfoAndNotCreating,
      toolTip: '$KrTiles_Web_CreateBasedOnTooltip'
    });
    const createBasedOnTileWithFiles = new Tile({
      name: 'CreateCardBasedOnWithFiles',
      caption: '$KrTiles_CreateBasedOnWithFiles',
      icon: 'ta icon-thin-002',
      contextSource,
      order: 101,
      evaluating: KrTypesAndCreateBasedOnTileExtension.enableIfHasDocumentCommonInfoAndNotCreating,
      toolTip: '$KrTiles_Web_CreateBasedOnTooltipWithFiles'
    });
    // Если будет отображаться <= maxTilesCountInTypeTile типов карточек/документов,
    // то список будет одноуровневым поэтому сначала подсчитаем итоговое (с учетом прав)
    // число типов карточек/документов
    const count = KrTypesAndCreateBasedOnTileExtension.countTypes(
      typesCache,
      cardMetadata,
      unavailableTypes,
      null,
      true
    );
    const maxTilesCountInGroupTile = 10; // TODO: получать из settings

    if (count > maxTilesCountInGroupTile) {
      KrTypesAndCreateBasedOnTileExtension.createBasedOnTilesAndGroup(
        context,
        unavailableTypes,
        contextSource,
        createBasedOnTile,
        false
      );
      KrTypesAndCreateBasedOnTileExtension.createBasedOnTilesAndGroup(
        context,
        unavailableTypes,
        contextSource,
        createBasedOnTileWithFiles,
        true
      );
    } else {
      KrTypesAndCreateBasedOnTileExtension.createBasedOnTilesWithoutGrouping(
        unavailableTypes,
        contextSource,
        createBasedOnTile,
        false
      );
      KrTypesAndCreateBasedOnTileExtension.createBasedOnTilesWithoutGrouping(
        unavailableTypes,
        contextSource,
        createBasedOnTileWithFiles,
        true
      );
    }

    if (createBasedOnTile.tiles.length > 0) {
      const cardOthers = context.workspace.leftPanel.tryGetTile('CardOthers')!;
      cardOthers.tiles.push(createBasedOnTile, createBasedOnTileWithFiles);
    }
  }

  private static createBasedOnTilesAndGroup(
    context: ITileGlobalExtensionContext,
    unavailableTypes: Set<guid>,
    contextSource: TileContextSource,
    createBasedOnTile: ITile,
    copyFiles: boolean
  ) {
    const typesCache = KrTypesCache.instance;
    const cardMetadata = MetadataStorage.instance.cardMetadata;
    const createBasedOnTileCollection = createBasedOnTile.tiles;

    const types = KrTypesCache.instance.cardTypes.map(x => {
      return {
        krCardType: x,
        groupCaption: KrTypesAndCreateBasedOnTileExtension.getCardTypeGroupLocalizedCaption(
          context,
          x
        )
      };
    });

    const groups: Map<string, { krCardType: KrCardType; groupCaption: string }[]> = new Map();
    for (const type of types) {
      const group = groups.get(type.groupCaption);
      if (!group) {
        groups.set(type.groupCaption, [type]);
      } else {
        group.push(type);
      }
    }

    const orderedGroups: string[] = [];
    groups.forEach((_, key) => orderedGroups.push(key));
    // дефолтная сортировка
    orderedGroups.sort((a, b) => LocalizationManager.instance.sort(a, b));

    const maxTiles = 10; // TODO: получать из settings
    let groupingTileOrder = 0;

    for (const key of orderedGroups) {
      const grouping = groups.get(key)!;
      grouping.sort((a, b) =>
        LocalizationManager.instance.sortByLocalized(a.krCardType.caption, b.krCardType.caption)
      );

      const groupingTile = new Tile({
        name: key || 'Other',
        caption: key || '$UI_Tiles_Other',
        icon: '',
        contextSource,
        order: groupingTileOrder++
      });

      groupingTile.info['.isGroup'] = true;

      const groupingTileCollection = groupingTile.tiles;
      const tilesInGroup: ITile[] = [];
      const tileOrderRef = { order: 0 };

      // уже отсортированно
      for (const krCardType of grouping.map(x => x.krCardType)) {
        const metadataCardType = cardMetadata.getCardTypeById(krCardType.id);
        if (!metadataCardType || hasFlag(metadataCardType.flags, CardTypeFlags.Hidden)) {
          continue;
        }

        if (krCardType.useDocTypes) {
          const typeTile = new Tile({
            name: '.basedOn' + krCardType.name,
            caption: krCardType.caption,
            icon: '',
            contextSource,
            order: tileOrderRef.order++,
            evaluating: KrTypesAndCreateBasedOnTileExtension.enableIfNotInSpecialModeAndNotCreating
          });

          typeTile.info['InitialCaption'] = krCardType.caption;

          let hasAvailableDocTypes = true;
          const innerOrderRef = { order: 0 };
          const docTypes = typesCache.docTypes
            .map(x => x)
            .sort((a, b) => LocalizationManager.instance.sortByLocalized(a.caption, b.caption));

          for (const docType of docTypes) {
            if (docType.cardTypeId !== krCardType.id || unavailableTypes.has(docType.id)) {
              continue;
            }

            const basedOnDocType = KrTypesAndCreateBasedOnTileExtension.createBasedOnDocTypeTile(
              contextSource,
              innerOrderRef,
              docType,
              copyFiles
            );

            tilesInGroup.push(basedOnDocType);
            typeTile.tiles.push(basedOnDocType);
            hasAvailableDocTypes = false;
          }

          if (!hasAvailableDocTypes) {
            groupingTileCollection.push(typeTile);
          }
        } else {
          if (unavailableTypes.has(krCardType.id)) {
            continue;
          }

          const basedOnCardType = KrTypesAndCreateBasedOnTileExtension.createBasedOnCardTypeTile(
            contextSource,
            tileOrderRef,
            krCardType,
            copyFiles
          );

          tilesInGroup.push(basedOnCardType);
          groupingTileCollection.push(basedOnCardType);
        }
      }

      // если плиток с создаваемыми типами карточек и документов мало в пределах группы типов,
      // то превращаем их в плоский список
      if (tilesInGroup.length > 0 && tilesInGroup.length <= maxTiles) {
        groupingTileCollection.length = 0;
        let order = 0;
        for (const tile of tilesInGroup.sort((a, b) =>
          LocalizationManager.instance.sortByLocalized(
            tryGetFromInfo(a.info, 'InitialCaption', ''),
            tryGetFromInfo(b.info, 'InitialCaption', '')
          )
        )) {
          tile.order = order++;
          groupingTileCollection.push(tile);
        }
      }

      // если в группе появилась хотя бы одна плитка, то добавляем её
      if (groupingTileCollection.length > 0) {
        createBasedOnTileCollection.push(groupingTile);
      }
    }
  }

  private static createBasedOnTilesWithoutGrouping(
    unavailableTypes: Set<guid>,
    contextSource: TileContextSource,
    createBasedOnTile: ITile,
    copyFiles: boolean
  ) {
    // этот быстрый и упрощённый метод добавления типов "единым списком" без группировки, используется в типовой поставке

    const typesCache = KrTypesCache.instance;
    const cardMetadata = MetadataStorage.instance.cardMetadata;
    const tileCollection = createBasedOnTile.tiles;
    const orderRef = { order: 0 };
    const types = typesCache.types
      .map(x => x)
      .sort((a, b) => LocalizationManager.instance.sortByLocalized(a.caption, b.caption));

    for (const type of types) {
      if (type instanceof KrCardType) {
        const cardType = type as KrCardType;
        if (cardType.useDocTypes || unavailableTypes.has(cardType.id)) {
          continue;
        }

        const metadataCardType = cardMetadata.getCardTypeById(cardType.id);
        if (!metadataCardType || hasFlag(metadataCardType.flags, CardTypeFlags.Hidden)) {
          continue;
        }

        tileCollection.push(
          KrTypesAndCreateBasedOnTileExtension.createBasedOnCardTypeTile(
            contextSource,
            orderRef,
            cardType,
            copyFiles
          )
        );
      } else {
        const docType = type as KrDocType;
        if (unavailableTypes.has(docType.id)) {
          continue;
        }

        const metadataCardType = cardMetadata.getCardTypeById(docType.cardTypeId);
        if (!metadataCardType || hasFlag(metadataCardType.flags, CardTypeFlags.Hidden)) {
          continue;
        }

        tileCollection.push(
          KrTypesAndCreateBasedOnTileExtension.createBasedOnDocTypeTile(
            contextSource,
            orderRef,
            docType,
            copyFiles
          )
        );
      }
    }
  }

  private static createBasedOnDocTypeTile(
    contextSource: TileContextSource,
    order: { order: number },
    docType: KrDocType,
    copyFiles: boolean
  ): ITile {
    const result = new Tile({
      name: '.basedOn' + docType.name,
      caption: docType.caption,
      icon: '',
      contextSource,
      command: async tile => {
        await KrTypesAndCreateBasedOnTileExtension.createBasedOnTileAction(
          tile,
          docType.cardTypeId,
          docType.cardTypeName,
          copyFiles,
          info => {
            info['docTypeID'] = createTypedField(docType.id, DotNetType.Guid);
            info['docTypeTitle'] = createTypedField(docType.name, DotNetType.String);
          }
        );
      },
      order: order.order++,
      evaluating: KrTypesAndCreateBasedOnTileExtension.enableIfNotInSpecialModeAndNotCreating
    });

    result.info['InitialCaption'] = docType.caption;
    return result;
  }

  private static createBasedOnCardTypeTile(
    contextSource: TileContextSource,
    order: { order: number },
    cardType: KrCardType,
    copyFiles: boolean
  ): ITile {
    const result = new Tile({
      name: '.basedOn' + cardType.name,
      caption: cardType.caption,
      icon: '',
      contextSource,
      command: async tile => {
        await KrTypesAndCreateBasedOnTileExtension.createBasedOnTileAction(
          tile,
          cardType.id,
          cardType.name,
          copyFiles
        );
      },
      order: order.order++,
      evaluating: KrTypesAndCreateBasedOnTileExtension.enableIfNotInSpecialModeAndNotCreating
    });

    result.info['InitialCaption'] = cardType.caption;
    return result;
  }

  private static async createBasedOnTileAction(
    tile: ITile,
    cardTypeId: guid,
    cardTypeName: string,
    copyFiles: boolean,
    initializeRequestAction: ((info: IStorage) => void) | null = null
  ) {
    const uiContext = tile.context;
    const editor = uiContext.cardEditor;
    let model: ICardModel;
    if (!editor || !(model = editor.cardModel!)) {
      return;
    }

    let saveBeforeCreatingCard: boolean;
    if (await model.hasChanges()) {
      const dialogResult = await showConfirmWithCancel(
        '$UI_Cards_ConfirmSavingCardBeforeCreatingBasedOn'
      );
      if (dialogResult == null) {
        return;
      }

      saveBeforeCreatingCard = dialogResult;
    } else {
      saveBeforeCreatingCard = false;
    }

    const requestInfo: IStorage = {};
    if (initializeRequestAction) {
      initializeRequestAction(requestInfo);
    }

    if (saveBeforeCreatingCard) {
      const saved = await editor.saveCard(uiContext);
      if (!saved) {
        return;
      }
    }

    const baseCard = editor.cardModel!.card;
    requestInfo['KrCreateBasedOnCardID'] = createTypedField(baseCard.id, DotNetType.Guid);
    requestInfo['KrCreateBasedOnCopyFiles'] = createTypedField(copyFiles, DotNetType.Boolean);

    const baseCardToken = KrToken.tryGet(baseCard.info);
    if (baseCardToken) {
      const tokenStorage: IStorage = {};
      baseCardToken.setInfo(tokenStorage);

      requestInfo['KrCreateBasedOnToken'] = tokenStorage;
    }

    await LoadingOverlay.instance.show(async splashResolve => {
      await createCard({
        cardTypeId,
        cardTypeName,
        context: tile.context,
        info: requestInfo,
        creationModeDisplayText: '$UI_Tiles_CreateCard_Suffix_BasedOn',
        splashResolve
      });
    });
  }

  private static getCardTypeGroupLocalizedCaption(
    context: ITileGlobalExtensionContext,
    krCardType: KrCardType
  ): string {
    const cardMetadata = MetadataStorage.instance.cardMetadata;
    const cardType = cardMetadata.getCardTypeById(krCardType.id);
    const groupCaption = cardType ? cardType.group : null;

    if (!groupCaption) {
      return '';
    }

    const captions = tryGetFromInfo(context.workspace.info, 'TypeGroupCaption', {});
    return captions[groupCaption] || groupCaption;
  }

  //#endregion
}
