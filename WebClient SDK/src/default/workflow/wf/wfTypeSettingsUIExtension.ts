import { CardUIExtension, ICardUIExtensionContext, IBlockViewModel } from 'tessa/ui/cards';
import { GridViewModel, GridRowAction } from 'tessa/ui/cards/controls';
import { DotNetType } from 'tessa/platform';
import { CardRow } from 'tessa/cards';
import { WfUiHelper } from './wfUiHelper';

export class WfTypeSettingsUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext) {
    const model = context.model;
    if (model.inSpecialMode) {
      return;
    }

    const cardTypeId = model.cardType.id;
    if (cardTypeId === '35a03878-57b6-4263-ae36-92eb59032132') {
      // KrSettingsTypeID
      const sections = model.card.tryGetSections();
      if (!sections) {
        return;
      }

      const cardTypesSection = sections.tryGet('KrSettingsCardTypes');
      const cardTypesControl = model.controls.get('CardTypeControl');
      if (!cardTypesSection || !cardTypesControl) {
        return;
      }

      // скрываем / отображаем контролы, связанные с использованием резолюции
      const cardTypesGrid = cardTypesControl as GridViewModel;
      cardTypesGrid.rowInvoked.add(e => {
        let resolutionsBlock: IBlockViewModel;
        if (
          (e.action === GridRowAction.Inserted || e.action === GridRowAction.Opening) &&
          e.rowModel &&
          (resolutionsBlock = e.rowModel.blocks.get('UseResolutionsBlock')!)
        ) {
          const useResolutionsAtFirst = e.row.get('UseResolutions');
          WfUiHelper.setControlVisibility(
            resolutionsBlock,
            '_UseResolutions',
            useResolutionsAtFirst
          );

          const dispose = e.row.fieldChanged.addWithDispose(e => {
            if (e.fieldName === 'UseResolutions') {
              const useResolutions = e.fieldValue;
              WfUiHelper.setControlVisibility(
                resolutionsBlock,
                '_UseResolutions',
                !!useResolutions
              );
            }
          });

          resolutionsBlock.form.closed.add(() => {
            if (dispose) {
              dispose();
            }
          });
        }
      });

      // сбрасываем состояние полей, когда галка "использовать резолюции" очищается
      const cardTypeRows = cardTypesSection.rows;
      for (let cardTypeRow of cardTypeRows) {
        WfTypeSettingsUIExtension.attachHandlersToCardTypeRow(cardTypeRow);
      }

      cardTypeRows.collectionChanged.add(e => {
        if (e.added) {
          for (let addedRow of e.added) {
            WfTypeSettingsUIExtension.attachHandlersToCardTypeRow(addedRow);
          }
        }
      });
    } else if (cardTypeId === 'b17f4f35-17e1-4509-994b-ebd576f2c95e') {
      // KrDocTypeTypeID
      const sections = model.card.tryGetSections();
      if (!sections) {
        return;
      }

      const docTypeSection = sections.tryGet('KrDocType');
      const resolutionsBlock = model.blocks.get('UseResolutionsBlock');
      if (!docTypeSection || !resolutionsBlock) {
        return;
      }

      // скрываем / отображаем контролы, связанные с использованием резолюции
      const useResolutionsAtFirst = docTypeSection.fields.get('UseResolutions');
      WfUiHelper.setControlVisibility(resolutionsBlock, '_UseResolutions', useResolutionsAtFirst);

      docTypeSection.fields.fieldChanged.add(e => {
        if (e.fieldName === 'UseResolutions') {
          // сбрасываем состояние полей, когда галка "использовать резолюции" очищается
          const useResolutions = e.fieldValue;
          if (!useResolutions) {
            e.storage.set('DisableChildResolutionDateCheck', false, DotNetType.Boolean);
          }

          // скрываем / отображаем контролы, связанные с использованием резолюции
          WfUiHelper.setControlVisibility(resolutionsBlock, '_UseResolutions', !!useResolutions);
        }
      });
    }
  }

  private static attachHandlersToCardTypeRow(cardTypeRow: CardRow) {
    cardTypeRow.fieldChanged.add(e => {
      if (e.fieldName === 'UseResolutions' && !e.fieldValue) {
        e.storage.set('DisableChildResolutionDateCheck', false, DotNetType.Boolean);
      }
    });
  }
}
