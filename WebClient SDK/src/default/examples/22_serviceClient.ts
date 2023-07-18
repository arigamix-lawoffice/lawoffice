import { processDefaultThen } from 'common/utility';
import { CardGetRequest, CardGetResponse } from 'tessa/cards/service';
import {
  deserializeFromTypedToPlain,
  serializeFromPlainToTyped
} from 'tessa/platform/serialization';

/*
Пример данного расширения представляет собой клиент для обращения
к методам контроллера Tessa.Extensions.Server.Web/Controllers/ServiceController.

Пример использования:
const client = new ServiceClient();
try {
  const token = await client.login('admin', 'admin');
  console.log(token);
  let data = await client.getData('hello!');
  console.log(data);
  data = await client.getDataWhenTokenInParameter(token, 'hello!');
  console.log(data);
  data = await client.getDataWithoutCheckingToken('hello!');
  console.log(data);
  const cardId = '11111111-1111-1111-1111-111111111111';
  const cardRequest = new CardGetRequest();
  cardRequest.cardId = cardId;
  let cardResponse = await client.getCard(cardRequest);
  console.log(cardResponse);
  cardResponse = await client.getCardById(cardId);
  console.log(cardResponse);
  await client.logout();
} catch (err) {
  console.error(ValidationResult.fromError(err));
}
*/
export class ServiceClient {
  private _servicePath: string;
  private _token: string | null;

  constructor() {
    this._servicePath = `${window.__INSTANCE_PATH__}/service`;
    this._token = null;
  }

  // Возвращает значение URL с учетом this._servicePath
  private getURL(url?: string): string {
    return `${this._servicePath}${url ? `/${url}` : ''}`;
  }

  // Возвращает описание запроса
  private getDefaultOptions(): RequestInit {
    return {
      mode: 'cors',
      credentials: 'same-origin'
    };
  }

  // Возвращает обработанный ответ с сервера
  private async send<T>({
    url,
    init,
    ignoreSession,
    transformResponse
  }: {
    url: string;
    init?: RequestInit;
    ignoreSession?: boolean;
    transformResponse?: (reponse: Response) => Promise<T>;
  }): Promise<T> {
    init = Object.assign({}, this.getDefaultOptions(), init);
    if (!ignoreSession && !!this._token) {
      const headers = init.headers ?? (init.headers = {});
      headers['Arigamix-Session'] = this._token;
    }
    return await processDefaultThen(fetch(this.getURL(url), init), transformResponse);
  }

  // Выполняет вход в систему для интеграционного взаимодействия с веб-сервисом.
  // Возвращает строку с токеном сессии, которую можно использовать для передачи в другие методы для авторизации.
  async login(login: string, password: string): Promise<string> {
    const fullToken = await this.send<string>({
      url: 'login',
      init: {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ login, password })
      },
      ignoreSession: true,
      transformResponse: resp => {
        try {
          this._token = resp.headers.get('Tessa-Session');
        } catch (err) {}
        return resp.text();
      }
    });

    if (!this._token) {
      this._token = fullToken;
    }

    return this._token;
  }

  // Выходим из системы, закрывая сессию, которая описывается указанным токеном.
  async logout(token?: string): Promise<void> {
    const effectiveToken = token ?? this._token!;
    if (!effectiveToken) {
      return;
    }
    await this.send<void>({
      url: `logout?token=${encodeURIComponent(effectiveToken)}`,
      init: {
        method: 'POST'
      },
      ignoreSession: true
    });
    if (!token) {
      this._token = null;
    }
  }

  // Метод сервиса. Может принимать и возвращать произвольные данные.
  async getData(parameter: string): Promise<string> {
    return await this.send<string>({
      url: `data?p=${encodeURIComponent(parameter)}`,
      init: {
        method: 'GET'
      }
    });
  }

  // Метод сервиса. Может принимать и возвращать произвольные данные.
  async getDataWhenTokenInParameter(token: string, parameter: string): Promise<string> {
    return await this.send<string>({
      url: `data?p=${encodeURIComponent(parameter)}&token=${encodeURIComponent(token)}`,
      init: {
        method: 'GET'
      },
      ignoreSession: true
    });
  }

  // Метод сервиса. Может принимать и возвращать произвольные данные.
  // При вызове метода не выполняется авторизация.
  async getDataWithoutCheckingToken(parameter: string): Promise<string> {
    return await this.send<string>({
      url: `data-without-login?p=${encodeURIComponent(parameter)}`,
      init: {
        method: 'GET'
      },
      ignoreSession: true
    });
  }

  // Открывает карточку и возвращает CardGetResponse по заданному request.
  async getCard(request: CardGetRequest): Promise<CardGetResponse> {
    return await this.send<CardGetResponse>({
      url: 'cards/get',
      init: {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: serializeFromPlainToTyped(request.getStorage())
      },
      transformResponse: async response => {
        const responseText = await response.text();
        return new CardGetResponse(deserializeFromTypedToPlain(responseText));
      }
    });
  }

  // Открывает карточку и возвращает CardGetResponse по указанному ID.
  async getCardById(cardId: guid, cardTypeName?: string): Promise<CardGetResponse> {
    return await this.send<CardGetResponse>({
      url: `cards/${cardId}${cardTypeName ? `?type=${encodeURIComponent(cardTypeName)}` : ''}`,
      init: {
        method: 'GET'
      },
      transformResponse: async response => {
        const responseText = await response.text();
        return new CardGetResponse(deserializeFromTypedToPlain(responseText));
      }
    });
  }
}
