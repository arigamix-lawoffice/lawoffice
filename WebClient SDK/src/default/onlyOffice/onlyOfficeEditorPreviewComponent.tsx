import React, { RefObject } from 'react';
import { OnlyOfficeEditorPreviewViewModel } from './onlyOfficeEditorPreviewViewModel';
import { reaction } from 'mobx';

interface OnlyOfficeEditorPreviewComponentProps {
  viewModel: OnlyOfficeEditorPreviewViewModel;
}

export class OnlyOfficeEditorPreviewComponent extends React.PureComponent<OnlyOfficeEditorPreviewComponentProps> {
  private static _placeholderId = 'onlyoffice-placeholder';

  private readonly _placeholderRef: RefObject<HTMLDivElement>;
  private readonly _placeholderContainerRef: RefObject<HTMLDivElement>;
  private readonly _disposers: (() => void)[] = [];

  private checkExistsIntervalId: number | undefined;

  public constructor(props: OnlyOfficeEditorPreviewComponentProps) {
    super(props);

    this._placeholderRef = React.createRef<HTMLDivElement>();
    this._placeholderContainerRef = React.createRef<HTMLDivElement>();
  }

  public async componentDidMount(): Promise<void> {
    const { viewModel } = this.props;

    this.attachViewModelListeners();

    if (this._placeholderRef.current) {
      await viewModel.setUpEditorSafety(OnlyOfficeEditorPreviewComponent._placeholderId);
    } else {
      // из-за того, что компонент может не сразу появляться в DOM (например, в диалоге),
      // а componentDidMount вызывается, дожидаемся пока элемент появится.
      this.checkExistsIntervalId = setInterval(async () => {
        if (this._placeholderRef.current) {
          clearInterval(this.checkExistsIntervalId);
          this.checkExistsIntervalId = undefined;
          await viewModel.setUpEditorSafety(OnlyOfficeEditorPreviewComponent._placeholderId);
        }
      }, 50);
    }
  }

  public async componentDidUpdate(
    prevProps: Readonly<OnlyOfficeEditorPreviewComponentProps>
  ): Promise<void> {
    const { viewModel } = this.props;

    if (prevProps.viewModel !== viewModel) {
      this.disposeViewModelListeners();
      this.attachViewModelListeners();
    }

    await viewModel.setUpEditorSafety(OnlyOfficeEditorPreviewComponent._placeholderId);
  }

  public componentWillUnmount(): void {
    clearInterval(this.checkExistsIntervalId);
    this.disposeViewModelListeners();
  }

  public render(): JSX.Element {
    /*
     * NOTE: Очень важно здесь держать элемент-обёртку над элементом-плейсхолдером.
     * Поскольку элемент-плейсхолдер превращается в что-то другое, при закрытии редактора - он уничтожается из DOM.
     * Реакту нужно понять, какой компонент нужно пересоздать.
     * Если оставить только элемент-плейсхолдер, то при открытии нового редактора в этом же месте,
     * реакту придётся пересоздавать все компоненты по иерархии сверху, пока не встретит ту самую обёртку,
     * что может привести к неожидаемым последствиям.
     */
    return (
      <div style={{ width: '100%', height: '100%' }} ref={this._placeholderContainerRef}>
        <div id={OnlyOfficeEditorPreviewComponent._placeholderId} ref={this._placeholderRef} />
      </div>
    );
  }

  private attachViewModelListeners = () => {
    const { viewModel } = this.props;

    // когда сменился файл, вызываем ререндер, чтобы компонент восстановил элемент плейсхолдер, а после этого в componentDidUpdate загружаем редактор.
    this._disposers.push(reaction(() => viewModel.fileVersion, this.rerenderComponent));

    const startDisposer = viewModel.resizerMouseDown.addWithDispose(this.resizerStartedHandler);
    if (startDisposer) this._disposers.push(startDisposer);

    const endDisposer = viewModel.resizerMouseUp.addWithDispose(this.resizerFinishedHandler);
    if (endDisposer) this._disposers.push(endDisposer);
  };

  private disposeViewModelListeners = () => {
    for (const d of this._disposers) {
      d();
    }
    this._disposers.length = 0;
  };

  private rerenderComponent = () => {
    this.forceUpdate();
  };

  private get actualPlaceholderElement(): HTMLElement | null {
    // не можем получить напрямую через ref, так как элемент заменяется на iframe при загрузке редактора
    return (
      (this._placeholderContainerRef.current?.firstElementChild as
        | HTMLElement
        | null
        | undefined) || null
    );
  }

  private resizerStartedHandler = () => {
    OnlyOfficeEditorPreviewComponent.setPointerEvents(this.actualPlaceholderElement, false);
  };

  private resizerFinishedHandler = () => {
    OnlyOfficeEditorPreviewComponent.setPointerEvents(this.actualPlaceholderElement, true);
  };

  /**
   * Включает или отключает события мыши в элементе.
   * Необходим для правильной работы ресайза панели предпросмотра,
   * так как внутри кастомного предпросмотра может находиться iframe с другого домена, который перехватывает события.
   */
  private static setPointerEvents(iframeElement: HTMLElement | null, enabled: boolean) {
    if (iframeElement) {
      if (enabled) {
        iframeElement.style.removeProperty('pointer-events');
      } else {
        iframeElement.style['pointer-events'] = 'none';
      }
    }
  }
}
