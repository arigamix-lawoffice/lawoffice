import { CardStoreExtension, ICardStoreExtensionContext } from 'tessa/cards/extensions';
import { hasNotFlag, DotNetType } from 'tessa/platform';
import { getKrComponentsByCard, KrTypesCache, KrComponents } from 'tessa/workflow';
import { MetadataStorage } from 'tessa';
import { MapStorage, ArrayStorage } from 'tessa/platform/storage';
import { CardSection, CardRow } from 'tessa/cards';
import {
  CardMetadataSectionSealed,
  CardMetadataColumnType,
  CardMetadataColumnSealed
} from 'tessa/cards/metadata';
import { CardTypeSchemeItemSealed } from 'tessa/cards/types';
import { designTimeCard } from '../krUIHelper';

/**
 * Расширение на сохранение карточки, содержащей маршрут.
 */
export class KrCardStoreExtension extends CardStoreExtension {
  public beforeRequest(context: ICardStoreExtensionContext): void {
    if (!context.validationResult.isSuccessful) {
      return;
    }

    const card = context.request.tryGetCard();

    if (
      !card ||
      (!designTimeCard(card.typeId) &&
        hasNotFlag(getKrComponentsByCard(card, KrTypesCache.instance), KrComponents.Routes))
    ) {
      return;
    }

    const info = card.tryGetInfo();
    if (info) {
      delete info['LocalTilesInfoMark'];
    }

    const stagesSection = card.sections.tryGet('KrStagesVirtual');
    if (!stagesSection || stagesSection.rows.length === 0) {
      return;
    }

    KrCardStoreExtension.visitSections(card.sections);

    for (const row of stagesSection.rows) {
      row.setChanged('DisplayTimeLimit', false);
      row.setChanged('DisplayParticipants', false);
      row.setChanged('DisplaySettings', false);
    }
  }

  private static visitSections(cardSections: MapStorage<CardSection>) {
    const cardMetadata = MetadataStorage.instance.cardMetadata;
    const cardType = cardMetadata.getCardTypeById('21bca3fc-f75f-413b-b5c8-49538cbfc761'); // KrCardTypeID
    if (!cardType) {
      return;
    }

    const schemeItems = new Map<guid, CardTypeSchemeItemSealed>();
    cardType.schemeItems.forEach(x => schemeItems.set(x.sectionId!, x));
    const topLevelSecMetadata = cardMetadata.getSectionByName('KrStagesVirtual');
    if (!topLevelSecMetadata) {
      return;
    }

    const stagesMapping = new Map<guid, guid>();
    const topLevelSection = cardSections.tryGet('KrStagesVirtual');
    if (!topLevelSection) {
      return;
    }

    for (const topLevelRow of topLevelSection.rows) {
      stagesMapping.set(topLevelRow.rowId, topLevelRow.rowId);
      topLevelRow.set('__ParentStageRowID', topLevelRow.rowId, DotNetType.Guid);
    }

    let previousLayer = new Set<guid>([topLevelSecMetadata.id!]);
    let currentLayer = new Set<guid>();

    // Обход зависимостей проводится в "ширину"
    // Вершиной является переданная через параметр секция
    // В первый слой входят все секции, имеющие столбец с указанием на родителя KrStages
    // Вторым слоем будут все секции, у которых "ссылка на родителя" указывает на секции первого слоя
    // и т.д. до тех пор, пока очередной слой не станет пустым.
    while (previousLayer.size !== 0) {
      for (const secMetadata of cardMetadata.sections) {
        // Секция не используется в карточке, а значит в обработке не участвует.
        const schemeItem = schemeItems.get(secMetadata.id!);
        if (!schemeItem) {
          continue;
        }

        // Получаем комплексный столбец с ссылкой на родителя.
        const refSecTuple = KrCardStoreExtension.getParentColumnSec(secMetadata, previousLayer);
        const parentComplexColumn = refSecTuple[0];
        const parentRowIdColumn = refSecTuple[1];
        if (!parentComplexColumn || !parentRowIdColumn) {
          continue;
        }

        // Комплексный столбец используется в карточке.
        if (!schemeItem.columnIdList.some(x => x === parentComplexColumn.id)) {
          continue;
        }

        let section: CardSection, rows: ArrayStorage<CardRow>;
        if (
          (section = cardSections.tryGet(secMetadata.name!)!) &&
          (rows = section.tryGetRows()!) &&
          rows.length > 0
        ) {
          currentLayer.add(secMetadata.id!);
          // Проставляем каждой строке ссылку на этап, ориентируясь по
          // ссылке на непосредственного родителя.
          for (const row of rows) {
            const parentId = row.tryGet(parentRowIdColumn.name!);
            let topLevelRowId: guid | null = null;
            if (parentId && (topLevelRowId = stagesMapping.get(parentId)!)) {
              stagesMapping.set(row.rowId, topLevelRowId);
              row.set('__ParentStageRowID', topLevelRowId, DotNetType.Guid);
            }
          }
        }
      }

      const layers = KrCardStoreExtension.swapLayers(previousLayer, currentLayer);
      previousLayer = layers[0];
      currentLayer = layers[1];
    }
  }

  private static getParentColumnSec(
    secMetadata: CardMetadataSectionSealed,
    previousLayer: Set<guid>
  ): [CardMetadataColumnSealed | null, CardMetadataColumnSealed | null] {
    let complex: CardMetadataColumnSealed | null = null;
    let rowId: CardMetadataColumnSealed | null = null;
    for (const column of secMetadata.columns) {
      if (
        column.parentRowSection &&
        column.columnType === CardMetadataColumnType.Complex &&
        previousLayer.has(column.parentRowSection.id!)
      ) {
        complex = column;
      } else if (
        complex &&
        column.columnType === CardMetadataColumnType.Physical &&
        column.parentRowSection &&
        complex.parentRowSection &&
        column.parentRowSection.id === complex.parentRowSection.id &&
        column.complexColumnIndex === complex.complexColumnIndex
      ) {
        rowId = column;
        break;
      }
    }

    return [complex, rowId];
  }

  private static swapLayers(
    previousLayer: Set<guid>,
    currentLayer: Set<guid>
  ): [Set<guid>, Set<guid>] {
    previousLayer.clear();
    return [currentLayer, previousLayer];
  }
}
