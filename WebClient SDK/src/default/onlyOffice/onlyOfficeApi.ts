import { userSession } from 'common/utility';
import {
  ValidationError,
  ValidationKeys,
  ValidationResult,
  ValidationResultBuilder,
  ValidationResultType
} from 'tessa/platform/validation';
import { FileContainer, IFile, IFileVersion } from 'tessa/files';
import { Guid } from 'tessa/platform';
import Platform from 'common/platform';
import moment from 'moment';
import { LocalizationManager } from 'tessa/localization';
import { CardGetFileContentResponse } from 'tessa/cards/service';
import { showLoadingOverlay } from 'tessa/ui';
import { OnlyOfficeEditorConfig } from './onlyOfficeEditorConfig';
import { OnlyOfficeSettings } from './onlyOfficeSettings';
import { ApiService } from 'tessa';
import { renameFile, selectFileCategoriesWithValidation } from 'tessa/ui/cards/controls';
import { OnlyOfficeOpenFileInfo } from './onlyOfficeOpenFileInfo';
import { FormattingManager } from 'tessa/platform/formatting';
import { FileInfo, IFileControl } from 'tessa/ui/files';

/**
 * Предоставляет API редактора документов OnlyOffice и его хранилища.
 */
export class OnlyOfficeApi {
  public static readonly apiScriptId = 'onlyOfficeApiScript';
  public readonly settings: OnlyOfficeSettings;

  private _openFiles: OnlyOfficeOpenFileInfo[] = [];

  public constructor(settings: OnlyOfficeSettings) {
    this.settings = settings;
  }

  //#region Public Methods

  /**
   * Открывает указанный файл, выполняя кэширование, а затем, после закрытия редактора, сохранение изменений и удаление из кэша.
   * @param version Версия файла.
   * @param openEditorAsyncCallback
   * Ассинхронная функция обратного вызова, в которой необходимо открыть редактор.
   * Ассинхронная функция должна завершаться после закрытия редактора.
   * @param forceCloseEditorCallback
   * Функция, которая вызывается, когда необходимо незамедлительно закрыть редактор
   * и закончить выполнение функции работы с редактором.
   * @param forEdit Признак того, что необходимо отследить изменения в файле, выполнив сохранение.
   * @param cardId Идентификатор карточки. Может быть null. Может служить дополнительным признаком для отслеживания открытых файлов в карточке.
   * @param loadingOverlay Признак того, что необходимо показать оверлей загрузки.
   * @param coedit Признак того, что будем работать с кем-то плотно.
   * @param onCoeditChanged Действие при завершении плотной работы с кем-то, когда был результат
   * @throws ValidationError Ошибка, возникающая в случае неудачной работы с файлом.
   */
  public async openFile(
    version: IFileVersion,
    openEditorAsyncCallback: (p: {
      id: guid;
      accessToken: string;
      documentKey?: string;
    }) => Promise<void>,
    forceCloseEditorCallback: () => void,
    forEdit: boolean,
    cardId: guid | null,
    loadingOverlay: boolean,
    coedit: boolean,
    onCoeditChanged?: () => Promise<unknown>
  ): Promise<void> {
    OnlyOfficeApi.throwIfFormatUnsupported(version.getExtension());

    let id: guid = Guid.newGuid();
    let accessToken: string | undefined;

    let forceCloseCalled = false;
    const forceCloseCallbackWrapper = () => {
      if (!forceCloseCalled) {
        forceCloseCalled = true;
        forceCloseEditorCallback();
      }
    };

    let coeditKey: string | undefined = undefined;

    const beforeEditorOpenFunc = async () => {
      let isCached = false;
      if (coedit) {
        const res = await OnlyOfficeApi.initiateCoedit(version.id, version.file.name);
        if (res.validation.hasErrors) throw new ValidationError(res.validation);
        const coeditParams = res.coeditParams!;
        isCached = !coeditParams.isNew;
        coeditKey = coeditParams.coeditKey;
        id = coeditParams.id;
        accessToken = coeditParams.accessToken;
      }

      if (!isCached) {
        const downloadRes = await version.ensureContentDownloaded();
        if (downloadRes.hasErrors) throw new ValidationError(downloadRes);

        const cacheRes = await OnlyOfficeApi.cache(
          id,
          version.id,
          version.file.name,
          version.content!
        );
        if (cacheRes.validation.hasErrors) throw new ValidationError(cacheRes.validation);
        const cacheParams = cacheRes.cacheParams!;
        accessToken = cacheParams.accessToken;
      }
    };

    let errorWhenOpening: Error | null = null;
    try {
      if (loadingOverlay) {
        await showLoadingOverlay(
          beforeEditorOpenFunc,
          LocalizationManager.instance.localize('$OnlyOffice_OpeningEditor')
        );
      } else {
        await beforeEditorOpenFunc();
      }

      this._openFiles.push({
        cacheId: id,
        forceCloseCallback: forceCloseCallbackWrapper,
        version: version,
        cardId: cardId,
        forEdit: forEdit
      });

      await openEditorAsyncCallback({
        id,
        documentKey: coeditKey,
        accessToken: accessToken!
      });
    } catch (e) {
      errorWhenOpening = e;
    }

    const afterEditorClosedFunc = async () => {
      let isCoeditChanged = false;
      let handleEnd = async () => {
        if (forceCloseCalled) {
          OnlyOfficeApi.deleteSynchronously(id);
        } else {
          await OnlyOfficeApi.delete(id);
        }
      };
      try {
        // здесь мы можем бросить ошибку открытия редактора, если она есть, для того,
        // чтобы предотвратить попытку получения отредактированного файла, удалив исходный кэш.
        if (errorWhenOpening) throw errorWhenOpening;

        if (forEdit && !forceCloseCalled) {
          if (!coedit) {
            const newContent = await OnlyOfficeApi.waitUntilEditorInfoPresentAndGetFinalFile(
              id,
              false
            );
            if (newContent) {
              version.file.replace(newContent, true);
            }
          } else {
            await OnlyOfficeApi.waitUntilEditorInfoPresent(
              id,
              async () => {
                isCoeditChanged = true;
              },
              10000
            );
          }
        }
      } catch (ex) {
        if (coedit && ex === NOT_CLOSE_EXCEPTION) {
          handleEnd = async () => {
            // onlyoffice editor did not signaled close event in time, so we need to 'close' it ourselves
            await OnlyOfficeApi.closeEditor(id);
          };
        }
        throw ex;
      } finally {
        const openedIndex = this._openFiles.findIndex(f => f.cacheId === id);
        if (openedIndex >= 0) this._openFiles.splice(openedIndex, 1);

        if (isCoeditChanged) {
          await onCoeditChanged?.();
        }

        await handleEnd();
      }
    };

    if (loadingOverlay && !forceCloseCalled) {
      await showLoadingOverlay(
        afterEditorClosedFunc,
        LocalizationManager.instance.localize('$OnlyOffice_ClosingEditor')
      );
    } else {
      await afterEditorClosedFunc();
    }
  }

  /**
   * Объединение/сравнение файлов
   * @param version Версия файла.
   * @param otherVersion Версия другого файла
   * @param openEditorAsyncCallback
   * Ассинхронная функция обратного вызова, в которой необходимо открыть редактор.
   * Ассинхронная функция должна завершаться после закрытия редактора.
   * @param forceCloseEditorCallback
   * Функция, которая вызывается, когда необходимо незамедлительно закрыть редактор
   * и закончить выполнение функции работы с редактором.
   * @param forEdit Признак того, что необходимо отследить изменения в файле, выполнив сохранение.
   * @param cardId Идентификатор карточки. Может быть null. Может служить дополнительным признаком для отслеживания открытых файлов в карточке.
   * @param loadingOverlay Признак того, что необходимо показать оверлей загрузки.
   * @throws ValidationError Ошибка, возникающая в случае неудачной работы с файлом.
   */
  public async mergeFiles(
    version: IFileVersion,
    otherVersion: IFileVersion,
    openEditorAsyncCallback: (p: {
      id: guid;
      accessToken: string;
      otherId: guid;
      otherAccessToken: string;
    }) => Promise<void>,
    forceCloseEditorCallback: () => void,
    forEdit: boolean,
    cardId: guid | null,
    loadingOverlay: boolean
  ): Promise<void> {
    OnlyOfficeApi.throwIfFormatUnsupported(version.getExtension());

    const id = Guid.newGuid();
    const otherId = Guid.newGuid();
    let accessToken = '';
    let otheraccessToken = '';

    let forceCloseCalled = false;
    const forceCloseCallbackWrapper = () => {
      if (!forceCloseCalled) {
        forceCloseCalled = true;
        forceCloseEditorCallback();
      }
    };

    const beforeEditorOpenFunc = async () => {
      for (const pair of [
        { id, version },
        { id: otherId, version: otherVersion }
      ] as { id: string; version: IFileVersion }[]) {
        const downloadRes = await pair.version.ensureContentDownloaded();
        if (downloadRes.hasErrors) throw new ValidationError(downloadRes);

        const cacheRes = await OnlyOfficeApi.cache(
          pair.id,
          pair.version.id,
          pair.version.file.name,
          pair.version.content!
        );
        if (cacheRes.validation.hasErrors) throw new ValidationError(cacheRes.validation);
        const cacheParams = cacheRes.cacheParams!;
        if (pair.id === id) {
          accessToken = cacheParams.accessToken;
        } else if (pair.id === otherId) {
          otheraccessToken = cacheParams.accessToken;
        }
      }
    };

    let errorWhenOpening: Error | null = null;
    try {
      if (loadingOverlay) {
        await showLoadingOverlay(
          beforeEditorOpenFunc,
          LocalizationManager.instance.localize('$OnlyOffice_OpeningEditor')
        );
      } else {
        await beforeEditorOpenFunc();
      }

      this._openFiles.push({
        cacheId: id,
        forceCloseCallback: forceCloseCallbackWrapper,
        version: version,
        cardId: cardId,
        forEdit: forEdit
      });

      await openEditorAsyncCallback({
        id,
        accessToken,
        otherId,
        otherAccessToken: otheraccessToken
      });
    } catch (e) {
      errorWhenOpening = e;
    }

    const afterEditorClosedFunc = async () => {
      try {
        // здесь мы можем бросить ошибку открытия редактора, если она есть, для того,
        // чтобы предотвратить попытку получения отредактированного файла, удалив исходный кэш.
        if (errorWhenOpening) throw errorWhenOpening;

        if (forEdit && !forceCloseCalled) {
          const newContent = await OnlyOfficeApi.waitUntilEditorInfoPresentAndGetFinalFile(
            id,
            false
          );
          if (newContent) {
            version.file.replace(newContent, true);
          }
        }
      } finally {
        const openedIndex = this._openFiles.findIndex(f => f.cacheId === id);
        if (openedIndex >= 0) this._openFiles.splice(openedIndex, 1);

        for (const i of [id, otherId]) {
          if (forceCloseCalled) {
            OnlyOfficeApi.deleteSynchronously(i);
          } else {
            await OnlyOfficeApi.delete(i);
          }
        }
      }
    };

    if (loadingOverlay && !forceCloseCalled) {
      await showLoadingOverlay(
        afterEditorClosedFunc,
        LocalizationManager.instance.localize('$OnlyOffice_ClosingEditor')
      );
    } else {
      await afterEditorClosedFunc();
    }
  }

  /**
   * Добавляет скрипт редактора в указанный документ, если его не имеется.
   */
  public ensureApiScriptAdded(d: Document): void {
    if (!!d.getElementById(OnlyOfficeApi.apiScriptId)) return;

    const script = d.createElement('script');
    script.id = OnlyOfficeApi.apiScriptId;
    script.src = this.settings.apiScriptUrl;
    d.head.insertBefore(script, d.head.firstChild);
  }

  /**
   * Создаёт новый файл в указанном контейнере, с помощью указанного шаблона.
   * @param container Контейнер файлов.
   * @param templateName Полное имя шаблона.
   * @param nameAfterCreation Новое имя после создания.
   */
  public async createTemplateFile(
    control: IFileControl,
    container: FileContainer,
    templateName: string,
    nameAfterCreation: string
  ): Promise<{ file: IFile | null; validation: ValidationResult }> {
    const templateData = await OnlyOfficeApi.getDocumentTemplate(templateName);
    if (templateData.validation.hasErrors)
      return { file: null, validation: templateData.validation };

    try {
      const file = container.createFile(new File([templateData.blob!], nameAfterCreation));

      const renameResult = await renameFile(file.name);
      if (renameResult.cancel) {
        return { file: null, validation: ValidationResult.empty };
      }

      file.changeName(renameResult.name);

      if (control.isCategoriesEnabled) {
        const categoryResult = await selectFileCategoriesWithValidation(
          control,
          [new FileInfo(null, file)],
          true,
          true
        );

        if (categoryResult.cancel) {
          return { file: null, validation: ValidationResult.empty };
        }

        file.category = categoryResult.categories[0];
      }

      await container.addFile(file, false, true);
      return { file: file, validation: ValidationResult.empty };
    } catch (e) {
      return { file: null, validation: ValidationResult.fromError(e) };
    }
  }

  /**
   * Создаёт редактор документов в указанном элементе и с указанным конфигом.
   */
  public createDocEditorFrame(
    placeholder: string,
    config: OnlyOfficeEditorConfig
  ): Record<string, unknown> {
    const api = window.DocsAPI;
    const editorConstructor = api['DocEditor'];

    return new editorConstructor(placeholder, config);
  }

  /**
   * Создаёт стандартный конфиг для указанной версии файла.
   */
  public createDefaultDocEditorConfig(
    id: guid,
    version: IFileVersion,
    mode: 'preview' | 'view' | 'edit',
    accessToken: string,
    documentKey?: string
  ): OnlyOfficeEditorConfig {
    const file = version.file;
    const fileExtension = version.getExtension();
    //  const documentType = getDocumentTypeByFileExtension(fileExtension);

    const forPreview = mode === 'preview';
    const forEdit = mode === 'edit';

    return {
      type: forPreview ? 'embedded' : Platform.isMobile() ? 'mobile' : 'desktop',
      document: {
        title: file.name,
        fileType: fileExtension,
        key: documentKey,
        url: OnlyOfficeApi.getFileForDocumentServerUrl(id, accessToken),
        info: {
          owner: version.createdByName,
          uploaded: moment(version.created).format(
            FormattingManager.instance.settings.dateTimeWithoutSecondsPattern
          )
        },
        permissions: {
          edit: forPreview ? false : file.permissions.canEdit,
          download: !forPreview,
          reader: !forPreview,
          review: !forPreview,
          print: !forPreview,
          comment: false,
          copy: !forPreview,
          fillForms: !forPreview,
          modifyContentControl: !forPreview,
          modifyFilter: !forPreview,
          rename: !forPreview
        }
      },
      // documentType: documentType,
      editorConfig: {
        mode: forEdit && file.permissions.canEdit ? 'edit' : 'view',
        lang: LocalizationManager.instance.currentLocaleCode,
        callbackUrl: OnlyOfficeApi.getCallbackUrl(id, accessToken),
        user: {
          id: userSession.UserID,
          name: userSession.UserName
        },
        customization: {
          chat: false,
          comments: false
        }
      }
    };
  }

  /**
   * Получает список открытых файлов в редакторе.
   */
  public get openFiles(): ReadonlyArray<OnlyOfficeOpenFileInfo> {
    return this._openFiles;
  }

  /**
   * Возвращает признак того, что указанный формат поддерживается редактором.
   */
  public static isSupportedFormat(ext: string): boolean {
    const type =
      /^(?:(xls|xlsx|ods|csv|xlst|xlsy|gsheet|xlsm|xlt|xltm|xltx|fods|ots)|(pps|ppsx|ppt|pptx|odp|pptt|ppty|gslides|pot|potm|potx|ppsm|pptm|fodp|otp)|(doc|docx|doct|odt|gdoc|txt|rtf|pdf|mht|htm|html|epub|djvu|xps|docm|dot|dotm|dotx|fodt|ott))$/.exec(
        ext
      );

    return !!type;
  }

  /**
   * Выбрасывает исключение, если указанное расширение не поддерживается.
   * @throws ValidationResult
   */
  public static throwIfFormatUnsupported(ext: string): void {
    if (!OnlyOfficeApi.isSupportedFormat(ext)) {
      throw new ValidationError(
        ValidationResult.fromText(`Format ${ext} is unsupported`, ValidationResultType.Error)
      );
    }
  }

  /**
   * Выбрасывает исключение, если в указанном окне не содержится загруженного API-скрипта.
   * @throws ValidationResult
   */
  public static throwIfApiScriptIsNotLoaded(w: Window): void {
    const notLoaded = !w['DocsAPI'];
    if (notLoaded) {
      throw new ValidationError(
        ValidationResult.fromText(
          LocalizationManager.instance.localize('$OnlyOffice_Error_ApiScriptNotAvailable'),
          ValidationResultType.Error
        )
      );
    }
  }

  //#endregion Public Methods

  //#region SERVER API

  private static get basePath(): string {
    return (
      window.location.protocol +
      '//' +
      window.location.host +
      ApiService.instance.getURL('api/v1/onlyoffice/')
    );
  }

  private static getUrlWithParams(
    actionName: string,
    id: guid | undefined,
    actionSuffix = '',
    token: string | undefined,
    ...additionalParams: { name: string; value: string }[]
  ) {
    let urlText = OnlyOfficeApi.basePath + actionName + (!!id ? '/' + id : '');
    if (actionSuffix.length > 0) {
      urlText = urlText + '/' + actionSuffix;
    }

    const url = new URL(urlText);

    if (token) {
      // const token = userSession.serializeToXml();
      url.searchParams.append('token', token);
    }

    for (const p of additionalParams) url.searchParams.append(p.name, p.value);

    return url.href;
  }

  private static async checkForMaintenance(response: Response): Promise<{
    maintenance: boolean;
    result: ValidationResult | null;
  }> {
    const contentType = response.headers.get('Content-Type');
    if (response.status == 503 && contentType?.startsWith('text/html') === true) {
      const data = await response.text();
      const builder = new ValidationResultBuilder();
      builder.add(
        ValidationResultType.Error,
        'System is in the maintenance mode',
        undefined,
        undefined,
        undefined,
        data,
        ValidationKeys.Maintenance
      );
      return {
        maintenance: true,
        result: builder.build()
      };
    }
    return { maintenance: false, result: null };
  }

  private static async getDocumentTemplate(
    name: string
  ): Promise<{ blob: Blob | null; validation: ValidationResult }> {
    const url = new URL(OnlyOfficeApi.basePath + 'templates' + '/' + encodeURI(name));

    try {
      const options: RequestInit = {
        method: 'GET',
        headers: { 'Accept-Language': LocalizationManager.instance.currentLocaleCode }
      };
      const response = await fetch(url.href, options);
      if (response.ok) {
        const blob = await response.blob();
        return { blob: blob, validation: ValidationResult.empty };
      } else {
        const { maintenance, result } = await this.checkForMaintenance(response);
        if (maintenance) {
          return { blob: null, validation: result! };
        }
        const error = await response.text();
        return {
          blob: null,
          validation: ValidationResult.fromText(error, ValidationResultType.Error)
        };
      }
    } catch (e) {
      return { blob: null, validation: ValidationResult.fromError(e) };
    }
  }

  private static getCallbackUrl(id: guid, token: string): string {
    return OnlyOfficeApi.getUrlWithParams('files', id, 'callback', token);
  }

  public static getFileForDocumentServerUrl(id: guid, token: string): string {
    return OnlyOfficeApi.getUrlWithParams('files', id, 'editor', token);
  }

  public static async getCurrentCoedit(sourceFileVersionId: guid): Promise<{
    data?: CurrentCoedit;
    validation: ValidationResult;
  }> {
    const url = OnlyOfficeApi.getUrlWithParams('files', undefined, 'coeditstatus', undefined, {
      name: 'versionId',
      value: sourceFileVersionId
    });

    try {
      const options: RequestInit = {
        mode: 'cors',
        method: 'GET',
        headers: { 'Accept-Language': LocalizationManager.instance.currentLocaleCode }
      };
      const response = await fetch(url, options);

      if (response.ok) {
        const data = await response.json();
        return { data, validation: ValidationResult.empty };
      } else {
        const { maintenance, result } = await this.checkForMaintenance(response);
        if (maintenance) {
          return { validation: result! };
        }
        const error = await response.text();
        return { validation: ValidationResult.fromText(error, ValidationResultType.Error) };
      }
    } catch (e) {
      return { validation: ValidationResult.fromError(e) };
    }
  }

  private static async cache(
    id: guid,
    sourceFileVersionId: guid,
    sourceFileName: string,
    content: File
  ): Promise<{
    cacheParams?: CacheParams;
    validation: ValidationResult;
  }> {
    const url = OnlyOfficeApi.getUrlWithParams(
      'files',
      id,
      'create',
      undefined,
      {
        name: 'versionId',
        value: sourceFileVersionId
      },
      {
        name: 'name',
        value: sourceFileName
      }
    );

    try {
      const options: RequestInit = {
        mode: 'cors',
        method: 'POST',
        body: content,
        headers: {
          'Content-Type': 'application/octet-stream',
          'Accept-Language': LocalizationManager.instance.currentLocaleCode
        }
      };
      const response = await fetch(url, options);

      if (response.ok) {
        const cacheParams = await response.json();
        return { cacheParams, validation: ValidationResult.empty };
      } else {
        const { maintenance, result } = await this.checkForMaintenance(response);
        if (maintenance) {
          return { validation: result! };
        }
        const error = await response.text();
        return { validation: ValidationResult.fromText(error, ValidationResultType.Error) };
      }
    } catch (e) {
      return { validation: ValidationResult.fromError(e) };
    }
  }

  /**
   * Выражаем желание совместно редактировать
   * @param sourceFileVersionId ид версии
   * @param sourceFileName название версии
   * @returns параметры совместного редактирования и данные по валидации
   */
  private static async initiateCoedit(
    sourceFileVersionId: guid,
    sourceFileName: string
  ): Promise<{
    coeditParams?: CoeditParams;
    validation: ValidationResult;
  }> {
    const url = OnlyOfficeApi.getUrlWithParams(
      'files',
      undefined,
      'coedit',
      undefined,
      {
        name: 'versionId',
        value: sourceFileVersionId
      },
      {
        name: 'name',
        value: sourceFileName
      }
    );

    try {
      const options: RequestInit = {
        mode: 'cors',
        method: 'GET',
        headers: {
          'Accept-Language': LocalizationManager.instance.currentLocaleCode
        }
      };
      const response = await fetch(url, options);

      if (response.ok) {
        const coeditParams = await response.json();
        return { coeditParams, validation: ValidationResult.empty };
      } else {
        const { maintenance, result } = await this.checkForMaintenance(response);
        if (maintenance) {
          return { validation: result! };
        }
        const error = await response.text();
        return { validation: ValidationResult.fromText(error, ValidationResultType.Error) };
      }
    } catch (e) {
      return { validation: ValidationResult.fromError(e) };
    }
  }

  private static async checkFinalFile(id: guid): Promise<{
    hasChangesAfterClose: boolean | null;
    validation: ValidationResult;
  }> {
    const url = OnlyOfficeApi.getUrlWithParams('files', id, 'state', undefined);

    try {
      const options: RequestInit = {
        mode: 'cors',
        method: 'GET',
        headers: {
          'Accept-Language': LocalizationManager.instance.currentLocaleCode
        }
      };
      const response = await fetch(url, options);

      if (response.ok) {
        const data = await response.json();
        return {
          hasChangesAfterClose: data.hasChangesAfterClose,
          validation: ValidationResult.empty
        };
      } else {
        const { maintenance, result } = await this.checkForMaintenance(response);
        if (maintenance) {
          return { hasChangesAfterClose: null, validation: result! };
        }

        const error = await response.text();

        return {
          hasChangesAfterClose: null,
          validation: ValidationResult.fromText(error, ValidationResultType.Error)
        };
      }
    } catch (e) {
      return { hasChangesAfterClose: null, validation: ValidationResult.fromError(e) };
    }
  }

  private static async getFinalFile(
    id: guid,
    convertToSourceExtension: boolean
  ): Promise<{ content?: File; validation: ValidationResult }> {
    const url = OnlyOfficeApi.getUrlWithParams('files', id, '', undefined, {
      name: 'original-format',
      value: JSON.stringify(convertToSourceExtension)
    });

    try {
      const options: RequestInit = {
        mode: 'cors',
        method: 'GET',
        headers: {
          'Accept-Language': LocalizationManager.instance.currentLocaleCode
        }
      };
      const response = await fetch(url, options);

      if (response.ok) {
        const blob = await response.blob();
        const fileName =
          CardGetFileContentResponse.tryGetFileNameFromContentDisposition(
            response.headers.get('Content-Disposition')
          ) || '[ERROR]';

        return { content: new File([blob], fileName), validation: ValidationResult.empty };
      } else {
        const { maintenance, result } = await this.checkForMaintenance(response);
        if (maintenance) {
          return { validation: result! };
        }
        const error = await response.text();
        return { validation: ValidationResult.fromText(error, ValidationResultType.Error) };
      }
    } catch (e) {
      return { validation: ValidationResult.fromError(e) };
    }
  }

  /**
   * @throws ValidationError
   */
  private static async waitUntilEditorInfoPresentAndGetFinalFile(
    id: guid,
    convertToSourceExtension: boolean
  ): Promise<File | null> {
    return await OnlyOfficeApi.waitUntilEditorInfoPresent(id, async () => {
      const fileResult = await OnlyOfficeApi.getFinalFile(id, convertToSourceExtension);
      if (fileResult.validation.isSuccessful) {
        return fileResult.content!;
      } else {
        throw new ValidationError(fileResult.validation);
      }
    });
  }

  /**
   * @throws ValidationError
   */
  private static async waitUntilEditorInfoPresent<T>(
    id: guid,
    onChange?: () => Promise<T>,
    timeoutMs?: number
  ): Promise<T | null> {
    let pastTime = 0;
    const TIMEOUT = 100;
    while (true) {
      const checkInfo = await OnlyOfficeApi.checkFinalFile(id);
      if (checkInfo.validation.hasErrors) throw new ValidationError(checkInfo.validation);

      if (checkInfo.hasChangesAfterClose === true) {
        return (await onChange?.()) || null;
      } else if (checkInfo.hasChangesAfterClose === false) {
        return null;
      } else {
        if (timeoutMs === undefined || (timeoutMs && timeoutMs > (pastTime += TIMEOUT))) {
          await new Promise(resolve => setTimeout(resolve, TIMEOUT));
        } else {
          break;
        }
      }
    }

    return Promise.reject(NOT_CLOSE_EXCEPTION);
  }

  private static async delete(id: guid): Promise<ValidationResult> {
    const url = OnlyOfficeApi.getUrlWithParams('files', id, '', undefined);

    try {
      const options: RequestInit = {
        mode: 'cors',
        method: 'DELETE',
        headers: {
          'Accept-Language': LocalizationManager.instance.currentLocaleCode
        }
      };
      const response = await fetch(url, options);

      if (response.ok) {
        return ValidationResult.empty;
      } else {
        const { maintenance, result } = await this.checkForMaintenance(response);
        if (maintenance) {
          return result!;
        }
        const error = await response.text();
        return ValidationResult.fromText(error, ValidationResultType.Error);
      }
    } catch (e) {
      return ValidationResult.fromError(e);
    }
  }

  private static async closeEditor(id: guid): Promise<ValidationResult> {
    const url = OnlyOfficeApi.getUrlWithParams('files', id, 'close', undefined);

    try {
      const options: RequestInit = { mode: 'cors', method: 'POST' };
      const response = await fetch(url, options);

      if (response.ok) {
        return ValidationResult.empty;
      } else {
        const error = await response.text();
        return ValidationResult.fromText(error, ValidationResultType.Error);
      }
    } catch (e) {
      return ValidationResult.fromError(e);
    }
  }

  private static deleteSynchronously(id: guid) {
    try {
      if ('sendBeacon' in navigator) {
        const url = OnlyOfficeApi.getUrlWithParams('files', id, 'delete', undefined);
        navigator.sendBeacon(url);
      } else {
        const url = OnlyOfficeApi.getUrlWithParams('files', id, '', undefined);
        const request = new XMLHttpRequest();
        request.open('DELETE', url, false);
        request.send();
      }
    } catch (e) {
      // ignore
      console.log(e);
    }
  }

  //#endregion SERVER API
}

export interface CoeditParams {
  id: string;
  accessToken?: string;
  coeditKey: string;
  isNew: boolean;
}

export interface CacheParams {
  accessToken: string;
}

export interface CurrentCoedit {
  /**
   * Coedit session participants with separator ','
   */
  names: string | null;
  /**
   * Coedit session date
   */
  date: string | null;
  /**
   * Current last version id. For purpose of warning a user about working in a past
   */
  lastVersionId: string;
}

const NOT_CLOSE_EXCEPTION = 'NOT_CLOSE_EXCEPTION';
