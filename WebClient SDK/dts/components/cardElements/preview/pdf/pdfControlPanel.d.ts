import React from 'react';
import { PdfPreviewerViewModel } from 'tessa/ui/cards/controls/previewer/pdfPreviewerViewModel';
interface ControlPanelProps {
    viewModel: PdfPreviewerViewModel;
}
export declare class ControlPanel extends React.PureComponent<ControlPanelProps> {
    private _containerRef;
    render(): JSX.Element;
    getContainerRect(): DOMRect | null;
    private onScaleChange;
    private onRecognizedLayoutChange;
}
export {};
