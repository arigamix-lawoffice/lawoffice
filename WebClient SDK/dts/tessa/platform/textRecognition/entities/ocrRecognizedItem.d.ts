import { IOcrRecognizedBox, OcrRecognizedBox } from './ocrRecognizedBox';
import { OcrRecognizedLayout } from './ocrRecognizedLayout';
/** Информация о коллекции распознанных элементов с одинаковым способом отображения. */
export interface IOcrRecognizedItem {
    /** Набор распознанных элементов. */
    readonly boxes: IOcrRecognizedBox[];
    /** Способ отображения распознанного элемента. */
    readonly layout: OcrRecognizedLayout;
}
/** Информация о коллекции распознанных элементов с одинаковым способом отображения. */
export declare class OcrRecognizedItem implements IOcrRecognizedItem {
    readonly boxes: OcrRecognizedBox[];
    readonly layout: OcrRecognizedLayout;
    constructor(item: IOcrRecognizedItem);
    /**
     * Выполняет пересчет координат элементов.
     * @param angle Угол поворота страницы.
     * @param scale Масштаб страницы.
     * @param offset Смещение страницы.
     * @param width Ширина страницы.
     * @param height Высота страницы.
     */
    invalidate(angle: number, scale: number, offset: number, width: number, height: number): void;
}
