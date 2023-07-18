import { ITileLocalExtensionContext, TileExtension } from 'tessa/ui/tiles';
import { TypeInfo } from '../info/typesInfo';
import { showLoadingOverlay } from 'tessa/ui';
import { createCard } from 'tessa/ui/uiHost';
import { CreateCardArg } from 'tessa/ui/uiHost/common';
import { DotNetType, TileNames, createTypedField } from 'tessa/platform';
import { IStorage } from 'tessa/platform/storage';
import { InfoMarks } from '../infoMarks';
import { ICardModel } from 'tessa/ui/cards';

/**
 * Тайлы для карточки "Дело"
 */
export class LawCaseTileExtension extends TileExtension {
  public initializingLocal(context: ITileLocalExtensionContext): void {
    const panel = context.workspace.leftPanel;

    const miscTile = panel.tiles.find(x => x.name === TileNames.CardOthers);
    if (!miscTile) {
      return;
    }

    const editor = miscTile.context.cardEditor;
    let cardModel: ICardModel;
    if (!editor || !(cardModel = editor.cardModel!) || cardModel.card.typeId !== TypeInfo.LawCase.ID) {
      return;
    }

    // Скрыть тайл "Создать шаблон"
    const createTemplateTile = miscTile.tiles.find(x => x.name === TileNames.CreateCardTemplate);
    if (createTemplateTile) {
      createTemplateTile.evaluating.add(e => e.setIsEnabledWithCollapsing(e.currentTile, false));
    }

    // Скрыть тайл "Удалить"
    const deleteCardTile = miscTile.tiles.find(x => x.name === 'DeleteCard');
    if (deleteCardTile) {
      deleteCardTile.evaluating.add(e => e.setIsEnabledWithCollapsing(e.currentTile, false));
    }

    // Переопределить действие копирования карточки, т.к. стандартное не работает с виртуальными карточками
    const createCardCopy = miscTile.tiles.find(x => x.name === TileNames.CreateCardCopy);
    if (createCardCopy) {
      let requestInfo: IStorage = {};
      requestInfo[InfoMarks.SourceCardID] = createTypedField(cardModel.card.id, DotNetType.Guid);

      createCardCopy.command = async () => {
        await showLoadingOverlay(async splashResolve => {
          let createArgs: CreateCardArg = {
            cardTypeId: TypeInfo.LawCase.ID,
            cardTypeName: TypeInfo.LawCase.Alias,
            context: createCardCopy.context,
            splashResolve: splashResolve,
            saveCreationRequest: false,
            info: requestInfo
          };
          await createCard(createArgs);
        });
      };
    }
  }
}
