import { IOcrRecognizedItem, OcrRecognizedItem } from './ocrRecognizedItem';
/** Информация обо всех распознанных элементах на странице. */
export interface IOcrRecognizedPage {
    /** Набор распознанных элементов на странице. */
    readonly items: ReadonlyArray<IOcrRecognizedItem>;
    /** Индекс страницы. */
    readonly index: number;
    /** Ширина страницы. */
    readonly width: number;
    /** Высота страницы. */
    readonly height: number;
}
/** Информация обо всех распознанных элементах на странице. */
export declare class OcrRecognizedPage implements IOcrRecognizedPage {
    readonly items: ReadonlyArray<OcrRecognizedItem>;
    readonly index: number;
    readonly width: number;
    readonly height: number;
    private _angle;
    private _scale;
    constructor(page: IOcrRecognizedPage);
    /**
     * Выполняет пересчет координат элементов.
     * @param angle Угол поворота страницы.
     * @param scale Масштаб страницы.
     */
    invalidate(angle: number, scale: number): void;
}
