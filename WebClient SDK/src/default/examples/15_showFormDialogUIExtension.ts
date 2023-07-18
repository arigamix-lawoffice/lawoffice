import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { Guid } from 'tessa/platform';
import { ButtonViewModel } from 'tessa/ui/cards/controls';
import { MetadataStorage } from 'tessa';
import { CardNewRequest, CardService } from 'tessa/cards/service';
import { showNotEmpty, createCardModel, UIButton } from 'tessa/ui';
import { showFormDialog } from 'tessa/ui/uiHost';
import { TestCardTypeID } from './common';

/**
 * При клике на контрол кнопки показываем диалог с выбранной формой.
 *
 * Результат работы расширения:
 * При клике на кнопку "Показать диалог" показываем диалоговое окно "Создать несколько карточек".
 */
export class ShowFormDialogUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext): void {
    // если карточка не для тестов, то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // пытаемся найти кнопку "Показать диалог"
    const button = context.model.controls.get('ShowDialogTypeForm') as ButtonViewModel;
    if (!button) {
      return;
    }

    // будем открывать диалог с формой по клику
    button.onClick = ShowFormDialogUIExtension.showFormDialog;
  }

  private static async showFormDialog() {
    // Ищем тип карточки с диалогами
    const dialogsType = MetadataStorage.instance.cardMetadata.getCardTypeByName('Dialogs');
    if (!dialogsType) {
      return;
    }

    // для примера ищем форму "Создать несколько карточек"
    const dialogForm = dialogsType.forms.find(x => x.name === 'CreateMultipleCards');
    if (!dialogForm) {
      return;
    }

    // получаем новую карточку
    const request = new CardNewRequest();
    request.cardTypeId = dialogsType.id;
    const response = await CardService.instance.new(request);
    response.card.id = Guid.newGuid();
    await showNotEmpty(response.validationResult.build());
    if (!response.validationResult.isSuccessful) {
      return;
    }

    // создаем модель карточки
    const windowCardModel = createCardModel(response.card, response.sectionRows);

    // показываем диалог
    await showFormDialog(
      dialogForm,
      windowCardModel,
      _form => {
        // тут можно управлять и расширить все контролы формы
      },
      [
        new UIButton('$UI_Common_OK', async btn => {
          console.log('OK');
          btn.close();
        }),
        new UIButton('$UI_Common_Cancel', btn => {
          console.log('Cancel');
          btn.close();
        })
      ]
    );
  }
}
