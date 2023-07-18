import React from 'react';
import { PdfPreviewerViewModel } from 'tessa/ui/cards/controls/previewer/pdfPreviewerViewModel';
export default function PdfViewer(props: {
    viewModel: PdfPreviewerViewModel;
    parentRef?: React.RefObject<HTMLElement> | null;
}): JSX.Element;
