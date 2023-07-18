import React from 'react';
import { OcrRecognizedBox } from 'tessa/platform/textRecognition/entities/ocrRecognizedBox';
/** Пропсы для компонента распознанного элемента. */
interface PdfViewerBoxProps {
    /** Порядковый номер элемента в коллекции. */
    index: number;
    /** Информация о распознанном элементе. */
    box: OcrRecognizedBox;
    /** Признак того, что элемент был выбран. */
    isSelected: boolean;
    /** Обработчик события перетаскивания элемента. */
    onDrag: (box: OcrRecognizedBox, selected: boolean, event: React.DragEvent) => void;
    /** Обработчик события выбора или отмены выбора элемента. */
    onSelection: (box: OcrRecognizedBox, selected: boolean, multiple: boolean, index: number) => void;
}
/** Компонент распознанного элемента. */
export declare const PdfViewerBox: React.FC<PdfViewerBoxProps>;
export {};
