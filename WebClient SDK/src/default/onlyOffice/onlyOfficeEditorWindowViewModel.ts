import { IFileVersion } from 'tessa/files';
import { OnlyOfficeApi } from './onlyOfficeApi';
import { LocalizationManager } from 'tessa/localization';
import { ValidationError, ValidationResult, ValidationResultType } from 'tessa/platform/validation';
import { showNotEmpty } from 'tessa/ui';

/**
 * Представляет собой модель редактора, используемого для чтения и редактирования файлов в отдельной вкладке браузера.
 */
export class OnlyOfficeEditorWindowViewModel {
  public readonly placeholder: string;
  public readonly version: IFileVersion;
  public readonly otherVersion: IFileVersion | undefined;
  public readonly forEdit: boolean;

  private readonly _cardId: guid;
  private readonly _api: OnlyOfficeApi;
  private _window: Window | null = null;

  /**
   *
   * @param api
   * @param version main file
   * @param forEdit
   * @param cardId
   * @param otherVersion file for a merge/compare operation
   */
  public constructor(
    api: OnlyOfficeApi,
    version: IFileVersion,
    forEdit: boolean,
    cardId: guid,
    otherVersion: IFileVersion | undefined = undefined
  ) {
    this._api = api;
    this.placeholder = 'onlyoffice-editor-placeholder';
    this.version = version;
    this.otherVersion = otherVersion;
    this.forEdit = forEdit;
    this._cardId = cardId;
  }

  public load = async (coedit: boolean = false, onCoeditChanged?: () => Promise<unknown>): Promise<void> => {
    try {
      await this._api.openFile(
        this.version,
        this.openEditorWindow,
        this.unload,
        this.forEdit,
        this._cardId,
        true,
        this.forEdit && coedit,
        onCoeditChanged
      );
    } catch (e) {
      if (e instanceof ValidationError) await showNotEmpty(e.Result);
      else await showNotEmpty(ValidationResult.fromError(e));
    }
  };

  public mergeOrCompare = async (): Promise<void> => {
    try {
      if (!this.otherVersion) {
        // only for developers
        throw 'Other version is not set';
      }

      await this._api.mergeFiles(
        this.version,
        this.otherVersion,
        this.openEditorWindow,
        this.unload,
        this.forEdit,
        this._cardId,
        true
      );
    } catch (e) {
      if (e instanceof ValidationError) await showNotEmpty(e.Result);
      else await showNotEmpty(ValidationResult.fromError(e));
    }
  };

  public unload = async (): Promise<void> => {
    if (this._window) {
      this._window.close();
      this._window = null;
    }
  };

  /**
   * Open editor window
   * @param id main file id
   * @param otherId for merge/conmpare operations
   */
  private openEditorWindow = async (params: {
    id: string;
    accessToken: string;
    otherId: string;
    otherAccessToken: string;
    documentKey?: string;
  }): Promise<void> => {
    const { id, otherId, documentKey, accessToken, otherAccessToken } = params;

    const config = this._api.createDefaultDocEditorConfig(
      id,
      this.version,
      this.forEdit ? 'edit' : 'view',
      accessToken,
      documentKey
    );

    let mergeComparePart = '';

    if (!!otherId) {
      mergeComparePart = `
config.events = config.events || { onDocumentReady: () => {}};
config.events.onDocumentReady = () => {
  editor.setRevisedFile({
    fileType: "docx",
    url: "${OnlyOfficeApi.getFileForDocumentServerUrl(otherId, otherAccessToken)}",
  });
};
    `;
    }

    // noinspection JSUnresolvedFunction,JSUnresolvedVariable
    const html = `
<html lang="${LocalizationManager.instance.currentLocaleCode}">
<head>
<title>ARIGAMIX DOCUMENT - ${this.version.name}</title>
<script
  id="${OnlyOfficeApi.apiScriptId}"
  onerror="window.checkScript()"
  onload="window.checkScript()"
  src="${this._api.settings.apiScriptUrl}">
</script>
</head>
<body>
<div id="${this.placeholder}"></div>
<script>
const config = ${JSON.stringify(config)};
${mergeComparePart}
const editor = new DocsAPI.DocEditor('${this.placeholder}', config);
</script>
</body>
</html>`;

    this._window = window.open();
    // tslint:disable-next-line:triple-equals
    if (!this._window || this._window.closed || typeof this._window.closed == 'undefined') {
      throw new ValidationError(
        ValidationResult.fromText(
          LocalizationManager.instance.localize('$OnlyOffice_Error_NewWindowBlocked'),
          ValidationResultType.Error
        )
      );
    }

    // устанавливаем функцию проверки загрузки скрипта
    let resolveApiAvailableChecking: () => void;
    let rejectApiAvailableChecking: (e: Error) => void;
    const apiAvailableCheckingPromise = new Promise<void>((resolve, reject) => {
      resolveApiAvailableChecking = resolve;
      rejectApiAvailableChecking = reject;
    });

    this._window.checkScript = () => {
      try {
        OnlyOfficeApi.throwIfApiScriptIsNotLoaded(this._window!);
        resolveApiAvailableChecking!();
      } catch (e) {
        rejectApiAvailableChecking!(e);
        this._window?.close();
      }
    };

    // загружаем документ
    this._window.document.open();
    this._window.document.write(html);
    this._window.document.close();

    const onClose = () =>
      rejectApiAvailableChecking(
        new Error(LocalizationManager.instance.localize('$OnlyOffice_Error_TabClosed'))
      );

    // а что будет, если вкладку закроют до запуска твоих маленьких скриптов? а? А? из-за этих строчек можешь продолжать об этом не думать
    this._window!.onbeforeunload = onClose;
    this._window!.onunload = onClose;

    // дожидаемся результатов проверки скрипта
    await apiAvailableCheckingPromise;

    // подписываемся на события закрытия окна и дожидаемся закрытия
    await new Promise<void>(resolve => {
      let closeFired = false;
      const afterWindowClosedFunc = () => {
        if (!closeFired) {
          closeFired = true;
          resolve();
        }
      };

      // навсякий случай подписываемся на оба события
      this._window!.onbeforeunload = afterWindowClosedFunc;
      this._window!.onunload = afterWindowClosedFunc;
    });
  };
}
