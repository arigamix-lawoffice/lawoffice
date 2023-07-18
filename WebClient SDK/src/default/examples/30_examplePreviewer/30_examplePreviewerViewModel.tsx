import { IFileVersion } from 'tessa/files';
import { IPreviewerViewModel } from 'tessa/ui/cards/controls/previewer';
import { observable, runInAction } from 'mobx';

export class ExamplePreviewerViewModel implements IPreviewerViewModel {
  public static get type(): string {
    return 'ExamplePreviewerViewModel';
  }

  public readonly type = ExamplePreviewerViewModel.type;
  public readonly showNameHeader: boolean = true;

  @observable.ref
  private _fileVersion: IFileVersion | null = null;

  @observable
  private _isLoading = false;

  @observable
  private _text = '';

  public load = async (version: IFileVersion): Promise<void> => {
    try {
      runInAction(() => {
        this._isLoading = true;
        this._fileVersion = version;
      });

      await version.ensureContentDownloaded();
      const text = await version.content!.text();

      runInAction(() => {
        this._text = text;
      });
    } catch (e) {
      runInAction(() => {
        this._text = 'ERROR: ' + e.message;
      });
    } finally {
      runInAction(() => (this._isLoading = false));
    }
  };

  public unload = (): void => {
    runInAction(() => {
      this._fileVersion = null;
      this._text = '';
      this._isLoading = false;
    });
  };

  public get fileVersion(): IFileVersion | null {
    return this._fileVersion;
  }

  public get text(): string {
    return this._text;
  }

  public get isLoading(): boolean {
    return this._isLoading;
  }
}
