import React from 'react';
import { PdfPreviewerViewModel } from 'tessa/ui/cards/controls/previewer/pdfPreviewerViewModel';
import 'react-pdf/dist/esm/Page/TextLayer.css';
interface PdfInfiniteViewerProps {
    viewModel: PdfPreviewerViewModel;
    parentRef?: React.RefObject<HTMLElement> | null;
}
interface PdfInfiniteViewerState {
    height: number;
    width: number;
    searchText: string;
}
export default class PdfInfiniteViewer extends React.PureComponent<PdfInfiniteViewerProps, PdfInfiniteViewerState> {
    constructor(props: PdfInfiniteViewerProps);
    private _containerRef;
    private _controlPanelRef;
    componentDidMount(): void;
    componentDidUpdate(prevProps: Readonly<PdfInfiniteViewerProps>): void;
    render(): JSX.Element;
    private updateAutoScaledSize;
    private onContainerResize;
    private onDocumentLoadSuccess;
    private onDocumentLoadError;
    private onPageChanged;
    private searchTextHandler;
}
export {};
