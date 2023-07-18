import {
  TileExtension,
  ITileGlobalExtensionContext,
  Tile,
  TileGroups,
  TileEvaluationEventArgs
} from 'tessa/ui/tiles';
import { UIContext, showMessage, MessageBoxResult } from 'tessa/ui';
import { ICardModel } from 'tessa/ui/cards';
import { Guid } from 'tessa/platform';
import { CardSection } from 'tessa/cards';
import { TestCardTypeID } from './common';

/**
 * Добавлять\cкрывать\показывать тайл в левой панели для карточек в зависимости от:
 * - типа карточки.
 * - данных карточки.
 *
 * Результат работы расширения:
 * Добавляет на левую панель тайл, который по нажатию открывает модальное окно.
 * Тайл виден только в карточке типа "Автомобиль" и при значении контрола пробег 100000 км.
 */
export class SimpleCardTileExtension extends TileExtension {
  public initializingGlobal(context: ITileGlobalExtensionContext): void {
    // получаем доступ к левой панели тайлов
    const panel = context.workspace.leftPanel;

    // создаем новый тайл
    const tile = new Tile({
      name: 'SimpleCardTile',
      caption: 'SimpleCardTile',
      icon: 'ta icon-thin-002',
      contextSource: panel.contextSource,
      group: TileGroups.Cards,
      order: 100,
      command: SimpleCardTileExtension.showMessageBoxCommand,
      evaluating: SimpleCardTileExtension.enableIfCardWithSubject
    });

    // добавляем тайл в левую боковую панель
    panel.tiles.push(tile);
  }

  private static async showMessageBoxCommand() {
    // пытаемся получить текущий СardEditor
    const editor = UIContext.current.cardEditor;
    let cardModel: ICardModel;
    if (!editor || !(cardModel = editor.cardModel!)) {
      return;
    }

    // находим текущую карточку
    const card = cardModel.card;

    // пытаемся найти секцию "Основная информация"
    const section = card.sections.tryGet('TEST_CarMainInfo');
    if (!section) {
      return;
    }

    const joke =
      '<b>Анекдот:</b> The past, the present, and the future walked into a bar. It was tense.';
    // находим поле пробег
    const running = section.fields.tryGet('Running') || '';
    const result = await showMessage(`${joke}<br/><b>Пробег, км:</b> ${running}`);
    if (result === MessageBoxResult.OK) {
      console.log('Окно закрыто по кнопке ОК. Шутка понравилась.');
    }
  }

  private static enableIfCardWithSubject(e: TileEvaluationEventArgs) {
    // пытаемся получить текущий СardEditor
    const editor = UIContext.current.cardEditor;
    let section: CardSection;
    e.setIsEnabledWithCollapsing(
      e.currentTile,
      !!editor &&
        !!editor.cardModel &&
        Guid.equals(editor.cardModel.cardType.id, TestCardTypeID) &&
        !!(section = editor.cardModel.card.sections.tryGet('TEST_CarMainInfo')!) &&
        section.fields.tryGet('Running') === 100000
    );
  }
}
