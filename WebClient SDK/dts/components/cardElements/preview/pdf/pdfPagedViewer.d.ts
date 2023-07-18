import React from 'react';
import { PdfPreviewerViewModel } from 'tessa/ui/cards/controls/previewer/pdfPreviewerViewModel';
import 'react-pdf/dist/esm/Page/TextLayer.css';
interface PdfPagedViewerProps {
    viewModel: PdfPreviewerViewModel;
    parentRef?: React.RefObject<HTMLElement> | null;
}
interface PdfPagedViewerState {
    width: number;
    originalWidth: number;
    originalRotate: number;
    searchText: string;
}
export default class PdfPagedViewer extends React.PureComponent<PdfPagedViewerProps, PdfPagedViewerState> {
    private timeout?;
    private isMouseClick;
    private lastClientX;
    private lastClientY;
    private containerPreviewRef;
    private cardNode;
    private mainDiv;
    private pageRef;
    private _disposeCallbacks;
    constructor(props: PdfPagedViewerProps);
    componentDidMount(): void;
    componentDidUpdate(prevProps: Readonly<PdfPagedViewerProps>): void;
    componentWillUnmount(): void;
    render(): JSX.Element;
    private attachViewModelListeners;
    private disposeViewModelListeners;
    private updateWidthByScale;
    private updateAutoScaledWidth;
    private calculateAutoScaledWidth;
    private scrollToPageStart;
    private onResize;
    private onMouseDown;
    private onMouseUp;
    private onMouseMove;
    private getCardNode;
    private onDocumentLoad;
    private onDocumentError;
    private onPageLoad;
    private onWheel;
    private refHandler;
    private calcRotate;
    private searchTextHandler;
}
export {};
