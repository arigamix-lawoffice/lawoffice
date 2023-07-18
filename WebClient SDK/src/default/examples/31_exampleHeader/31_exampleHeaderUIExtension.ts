import { Guid } from 'tessa/platform';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { TestCardTypeID } from '../common';
import { ExampleHeaderViewModel } from './31_exampleHeaderViewModel';

export class ExampleHeaderUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext): void {
    // если карточка не для тестов, то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // создаем вью-модель для хэдера и устанавливаем ее в соответствующее свойство модели карточки
    context.model.header = new ExampleHeaderViewModel('Добро пожаловать в карточку автомобиля.');
  }
}
