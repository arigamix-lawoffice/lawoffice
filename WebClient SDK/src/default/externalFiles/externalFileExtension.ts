import { ExternalFile } from './externalFile';
import { FileExtension, IFileExtensionContext } from 'tessa/ui/files';
import { UIContext } from 'tessa/ui';
import { ICardModel } from 'tessa/ui/cards';

export class ExternalFileExtension extends FileExtension {

  public openingMenu(context: IFileExtensionContext) {
    // Проверяем тип карточки
    const editor = UIContext.current.cardEditor;
    let model: ICardModel;
    if (editor == null
      || !(model = editor.cardModel!)
      || model.cardType.name !== 'Car'
    ) {
      return;
    }

    // Отключаем пункт "Копировать ссылку"
    const copyLinkAction = context.actions.find(p => p.name === 'CopyLink');
    if (context.file.model instanceof ExternalFile
      && copyLinkAction
    ) {
      copyLinkAction.isCollapsed = true;
    }
  }

}