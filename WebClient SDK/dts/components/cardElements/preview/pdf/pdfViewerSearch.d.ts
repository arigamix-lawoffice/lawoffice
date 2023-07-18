import React from 'react';
/** Пропсы для компонента поиска текста в средстве предпросмотра PDF. */
interface PdfViewerSearchProps {
    /** Исходное значение в строке поиска. */
    initialValue: string;
    /** Обработчик события поиска текста. */
    onTextSearch: (value: string) => void;
}
/** Компонент поиска текста в средстве предпросмотра PDF. */
export declare const PdfViewerSearch: React.FC<PdfViewerSearchProps>;
export {};
