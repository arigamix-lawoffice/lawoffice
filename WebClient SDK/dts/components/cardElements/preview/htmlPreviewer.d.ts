import React from 'react';
import { HtmlPreviewerViewModel } from 'tessa/ui/cards/controls/previewer/htmlPreviewerViewModel';
export interface IHtmlPreviewerProps {
    viewModel: HtmlPreviewerViewModel;
}
export default class HtmlPreviewer extends React.PureComponent<IHtmlPreviewerProps> {
    private _disposers;
    private _iframeRef;
    private refHandler;
    private _previousPositionReactionDisposer?;
    private _previousPosition;
    componentDidMount(): void;
    componentDidUpdate(prevProps: Readonly<IHtmlPreviewerProps>): void;
    componentWillUnmount(): void;
    render(): JSX.Element;
    private writeHtmlToIframe;
    private attachViewModelListeners;
    private disposeViewModelListeners;
    private resizerStartedHandler;
    /**
     * Нужно для получения событий движения мышки над iframe при превью html
     */
    private static bubbleIframeMouseMove;
    private static iFrameMouseEventHandler;
}
