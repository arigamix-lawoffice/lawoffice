import userSession from 'common/utility/userSession';
import { runInAction } from 'mobx';
import moment from 'moment';
import { CardStoreMode } from 'tessa/cards/cardStoreMode';
import { Tag } from 'tessa/tags/tag';
import { TagInfo } from 'tessa/tags/tagInfo';
import { CardUIExtension } from 'tessa/ui/cards/cardUIExtension';
import { ICardUIExtensionContext } from 'tessa/ui/cards/cardUIExtensionContext';
import { ICardEditorModel } from 'tessa/ui/cards/interfaces';
import { CardTagViewModel } from 'tessa/ui/tags/cardTagViewModel';
import { selectOrCreateTag } from 'tessa/ui/tags/tagHelper';
import { TagInfoModel } from 'tessa/ui/tags/tagInfoModel';
import { TagManager } from 'tessa/ui/tags/tagManager';
import { showConfirm } from 'tessa/ui/tessaDialog/show';
import { showNotEmpty } from 'tessa/ui/tessaDialog/showNotEmpty';
import { UIContext } from 'tessa/ui/uiContext';

export class TagDemoCardActionExtension extends CardUIExtension {
  public async initialized(context: ICardUIExtensionContext): Promise<void> {
    const cardEditor = context.uiContext.cardEditor!;
    cardEditor.toolbar.canHaveTags = true;
    const tags = await TagManager.instance.getTags(context.card.id);
    cardEditor.toolbar.canHaveTags = true;
    runInAction(() => {
      cardEditor.toolbar.tags.length = 0;
      for (const tagInfo of tags) {
        this.addTagToToolbar(context.card.id, cardEditor, tagInfo);
      }
    });
    cardEditor.toolbar.clickAddTag = async () => {
      const cardEditor = UIContext.current.cardEditor;
      if (!cardEditor?.cardModel) {
        return;
      }

      const cardId = cardEditor.cardModel.card.id;

      if (
        cardEditor.cardModel.card.storeMode === CardStoreMode.Insert ||
        (await cardEditor.cardModel.hasChanges())
      ) {
        if (await showConfirm('$Tags_SaveCardToAddTag')) {
          const saveResult = await cardEditor.cardModel.saveAsync();
          if (!saveResult.isSuccessful) {
            return;
          }
        } else {
          return;
        }
      }

      const tagInfo = await selectOrCreateTag(cardId);

      if (tagInfo && !cardEditor.toolbar.tags.some(x => x.id === tagInfo.id && !x.isDeleted)) {
        const tag = new Tag();
        tag.tagId = tagInfo.id;
        tag.cardId = context.card.id;
        tag.userId = userSession.UserID;
        tag.setAt = moment.utc().format();
        const result = await TagManager.instance.storeTag(tag);
        const tagStored = result.isSuccessful;
        await showNotEmpty(result);
        if (tagStored) {
          this.addTagToToolbar(cardId, cardEditor, tagInfo);
        }
      }
    };
  }

  private addTagToToolbar(cardId: guid, cardEditor: ICardEditorModel, tag: TagInfo) {
    // Проверяем, не был ли тег удален перед этим.
    // Если был, то восстанавливаем его, а не добавляем новый.
    const deletedTag = cardEditor.toolbar.tags.find(x => x.id == tag.id && x.isDeleted);
    if (deletedTag) {
      runInAction(() => {
        deletedTag.isDeleted = false;
      });
    } else {
      const tagInfoModel = new TagInfoModel(tag, cardId);
      const viewTagInfoModel = new CardTagViewModel(tagInfoModel);
      runInAction(() => {
        cardEditor.toolbar.tags.push(viewTagInfoModel);
      });
    }
  }

  public shouldExecute(_context: ICardUIExtensionContext): boolean {
    return _context.card.typeId === 'd0006e40-a342-4797-8d77-6501c4b7c4ac'; // Карточка автомобиля.
  }
}
