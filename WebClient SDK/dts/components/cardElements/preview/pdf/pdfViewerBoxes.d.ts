import React from 'react';
import { OcrRecognizedCollection } from 'tessa/platform/textRecognition/entities/ocrRecognizedCollection';
import { OcrRecognizedLayout } from 'tessa/platform/textRecognition/entities/ocrRecognizedLayout';
/** Пропсы для компонента коллекции распознанных элементов. */
interface PdfViewerBoxesProps {
    /** Номер страницы. */
    pageIndex: number;
    /** Наклон страницы. */
    pageRotate: number;
    /** Масштаб страницы. */
    pageScale: number;
    /** Способ отображения распознанных элементов. */
    recognizedLayout: OcrRecognizedLayout;
    /** Коллекция распознанных элементов. */
    collection: OcrRecognizedCollection;
    /** Коллекция индексов выделенных распознанных элементов. */
    selected: number[];
}
/** Компонент коллекции распознанных элементов. */
export declare const PdfViewerBoxes: React.FC<PdfViewerBoxesProps>;
export {};
