import { IFileVersion } from 'tessa/files';
import { OnlyOfficeApi } from './onlyOfficeApi';
import { IPreviewerViewModel } from 'tessa/ui/cards/controls/previewer';
import { observable, runInAction } from 'mobx';
import { EventHandler } from 'tessa/platform';

/**
 * Представляет собой модель редактора, используемого для предпросмотра файлов.
 */
export class OnlyOfficeEditorPreviewViewModel implements IPreviewerViewModel {
  public static get type(): string {
    return 'OnlyOfficeEditorPreviewViewModel';
  }

  public readonly showNameHeader: boolean = false;
  public readonly type: string = OnlyOfficeEditorPreviewViewModel.type;
  public readonly resizerMouseDown: EventHandler<() => void>;
  public readonly resizerMouseUp: EventHandler<() => void>;

  @observable.ref
  private _fileVersion: IFileVersion | null;

  private readonly _api: OnlyOfficeApi;
  private readonly _errorHandler: (e: Error) => void;
  private _closeEditorCallback: (() => void) | null = null;
  private _editor: Record<string, unknown> | null = null;

  public constructor(api: OnlyOfficeApi, errorHandler: (e: Error) => void) {
    this._api = api;
    this._errorHandler = errorHandler;

    this.resizerMouseDown = new EventHandler<() => void>();
    this.resizerMouseUp = new EventHandler<() => void>();
  }

  public load = (version: IFileVersion): void => {
    runInAction(() => (this._fileVersion = version));
  };

  public setUpEditorSafety = async (placeholder: string): Promise<void> => {
    if (!this._fileVersion) return;
    const version = this._fileVersion;

    try {
      OnlyOfficeApi.throwIfApiScriptIsNotLoaded(window);
      await this._api.openFile(
        version,
        ({id, accessToken}) => this.openEditorFunc(id, version, placeholder, accessToken),
        this.unload,
        false,
        null,
        false,
        false
      );
    } catch (e) {
      this._errorHandler(e);
    }
  };

  public unload = (): void => {
    if (this._editor) {
      if ('destroyEditor' in this._editor) {
        const destroyEditorFunc = this._editor['destroyEditor'] as () => void;
        destroyEditorFunc();
      }
      this._editor = null;
    }

    if (this._fileVersion) {
      runInAction(() => (this._fileVersion = null));
    }

    if (this._closeEditorCallback) {
      this._closeEditorCallback();
      this._closeEditorCallback = null;
    }
  };

  private openEditorFunc = async (
    id: guid,
    version: IFileVersion,
    placeholder: string,
    accessToken: string
  ): Promise<void> => {
    const config = this._api.createDefaultDocEditorConfig(id, version, 'preview', accessToken);
    this._editor = this._api.createDocEditorFrame(placeholder, config);

    const closePromise = new Promise<void>(resolve => {
      this._closeEditorCallback = resolve;
    });
    await closePromise;
  };

  public get fileVersion(): IFileVersion | null {
    return this._fileVersion;
  }
}
