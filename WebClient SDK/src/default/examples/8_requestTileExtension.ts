import { TileExtension, ITileGlobalExtensionContext, Tile, TileGroups } from 'tessa/ui/tiles';
import { CardGetRequest, CardService } from 'tessa/cards/service';
import { showNotEmpty, showMessage } from 'tessa/ui';
import {
  RequestParameterBuilder,
  ViewService,
  TessaViewRequest,
  ITessaViewResult,
  convertRowsToMap
} from 'tessa/views';
import { isNotNullCriteriaOperator } from 'tessa/views/metadata';
import { ValidationResult } from 'tessa/platform/validation';

/**
 * В этом расширении демонстрируется:
 * - Тайл-группа на левой панели.
 * - Запрос карточки с сервера.
 * - Запрос данных представления с сервера.
 * - Заполнение параметров поиска в запросе к представлению.
 * - Доступ к результату запроса представления.
 *
 * Результат работы расширения:
 * На левую панель добавляет групповой тайл - “Запросы”. По клику показываются дочерние тайлы - “Запрос карточки” и “Запрос представления”.
 * При клике на “Запрос карточки” происходит вызов сервера, получается карточка прав, и необработанное содержимое показывается в модальном окне.
 * Аналогично, с помощью CardService можно делать и другие реквесты (request, get, store и т.д.).
 * При клике на “Запрос представления” происходит вызов сервера, получаются данные списка контрагентов, и необработанное содержимое показывается в модальном окне.
 */

export class RequestTileExtension extends TileExtension {
  public initializingGlobal(context: ITileGlobalExtensionContext): void {
    // получаем доступ к левой боковой панели
    const panel = context.workspace.leftPanel;

    // создаем тайл-группу
    const groupTile = new Tile({
      name: 'TestGroupRequestTileExtension',
      caption: 'Запросы',
      icon: 'ta icon-thin-100',
      contextSource: panel.contextSource,
      group: TileGroups.Cards,
      order: 100
    });

    // создаем содержимое тайл-группы
    const cardRequestTile = new Tile({
      name: 'TestCardRequestTileExtension',
      caption: 'Запрос карточки',
      icon: 'ta icon-thin-100',
      contextSource: panel.contextSource,
      group: TileGroups.Cards,
      order: 1,
      command: RequestTileExtension.cardRequestCommand
    });
    const viewRequestTile = new Tile({
      name: 'TestViewRequestTileExtension',
      caption: 'Запрос представления',
      icon: 'ta icon-thin-100',
      contextSource: panel.contextSource,
      group: TileGroups.Cards,
      order: 2,
      command: RequestTileExtension.viewRequestCommand
    });

    // добавляем созданные тайлы в группу
    groupTile.tiles.push(viewRequestTile, cardRequestTile);

    // добавляем тайл-группу в левую боковую панель
    panel.tiles.push(groupTile);
  }

  private static async cardRequestCommand() {
    const cardGetRequest = new CardGetRequest();

    // ID карточки типа "Правила доступа" с названием "Default access rules"
    cardGetRequest.cardId = '9fac55ba-afab-4a40-8789-79c97d96ace2';
    cardGetRequest.cardTypeId = 'fa9dbdac-8708-41df-bd72-900f69655dfa';
    cardGetRequest.cardTypeName = 'KrPermissions';

    // при запросе все расширения CardGetExtension будут вызываны
    const cardGetResponse = await CardService.instance.get(cardGetRequest);
    if (!cardGetResponse.validationResult.isSuccessful) {
      // если в validationResult есть ошибки, то показываем их
      await showNotEmpty(cardGetResponse.validationResult.build());
      return;
    }

    // берем storage карточки как строку
    const cardStr = JSON.stringify(cardGetResponse.card.getStorage());
    await showMessage(cardStr.slice(0, 100));
  }

  private static async viewRequestCommand() {
    // пытаемся найти представление "Контрагенты"
    const partnersView = ViewService.instance.getByName('Partners');
    if (!partnersView) {
      return;
    }

    const request = new TessaViewRequest(partnersView.metadata);

    // добавляем параметр фильтрации по имени контрагента (для примера, что имя не равно null)
    const nameParam = new RequestParameterBuilder()
      .withMetadata(partnersView.metadata.parameters.get('Name')!)
      .addCriteria(isNotNullCriteriaOperator())
      .asRequestParameter();
    request.values.push(nameParam);

    let result: ITessaViewResult;
    try {
      // в getData будут добавлены параметры currentUserId и locale
      result = await partnersView.getData(request);
    } catch (err) {
      await showNotEmpty(ValidationResult.fromError(err));
      return;
    }

    // конвертируем строки в Map<string, any>[] для удобства
    const rows = convertRowsToMap(result.columns, result.rows);

    const text: string[] = [];
    rows.forEach(row => {
      const rowText: string[] = [];
      row.forEach((v, k) => {
        rowText.push(`${k}: ${v}`);
      });
      text.push(rowText.join(';'));
    });

    await showMessage(text.join('\n'));
  }
}
