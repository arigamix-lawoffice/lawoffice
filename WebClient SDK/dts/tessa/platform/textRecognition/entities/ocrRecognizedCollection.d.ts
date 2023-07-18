import { EventHandler } from 'tessa/platform';
import { IOcrRecognizedBox } from './ocrRecognizedBox';
import { IOcrRecognizedPage, OcrRecognizedPage } from './ocrRecognizedPage';
/** Информация обо всех распознанных элементах в документе. */
export interface IOcrRecognizedCollection {
    /** Набор распознанных страниц, содержащих распознанные элементы. */
    readonly pages: ReadonlyArray<IOcrRecognizedPage>;
    /** Обработчик события выбора распознанного элемента в области предпросмотра. */
    readonly recognizedBoxSelected: EventHandler<(args: IOcrRecognizedBox) => void>;
}
/** Информация обо всех распознанных элементах в документе. */
export declare class OcrRecognizedCollection implements IOcrRecognizedCollection {
    readonly pages: ReadonlyArray<OcrRecognizedPage>;
    readonly recognizedBoxSelected: EventHandler<(args: IOcrRecognizedBox) => void>;
    constructor(pages: IOcrRecognizedPage[]);
    /**
     * Выполняет пересчет координат элементов на странице (-ах).
     * @param angle Угол поворота страницы.
     * @param scale Масштаб страницы.
     * @param pageIndex Индекс страницы. Если не задан, то пересчет выполняется для всех страниц.
     */
    invalidate(angle: number, scale: number, pageIndex?: number): void;
}
