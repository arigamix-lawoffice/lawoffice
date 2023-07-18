import React from 'react';
import { OnlyOfficeEditorPreviewViewModel } from './onlyOfficeEditorPreviewViewModel';
interface OnlyOfficeEditorPreviewComponentProps {
    viewModel: OnlyOfficeEditorPreviewViewModel;
}
export declare class OnlyOfficeEditorPreviewComponent extends React.PureComponent<OnlyOfficeEditorPreviewComponentProps> {
    private static _placeholderId;
    private readonly _placeholderRef;
    private readonly _placeholderContainerRef;
    private readonly _disposers;
    private checkExistsIntervalId;
    constructor(props: OnlyOfficeEditorPreviewComponentProps);
    componentDidMount(): Promise<void>;
    componentDidUpdate(prevProps: Readonly<OnlyOfficeEditorPreviewComponentProps>): Promise<void>;
    componentWillUnmount(): void;
    render(): JSX.Element;
    private attachViewModelListeners;
    private disposeViewModelListeners;
    private rerenderComponent;
    private get actualPlaceholderElement();
    private resizerStartedHandler;
    private resizerFinishedHandler;
    /**
     * Включает или отключает события мыши в элементе.
     * Необходим для правильной работы ресайза панели предпросмотра,
     * так как внутри кастомного предпросмотра может находиться iframe с другого домена, который перехватывает события.
     */
    private static setPointerEvents;
}
export {};
