import { ICloneable } from 'tessa/platform';
/** Информация о координатах распознанного элемента. */
export interface IOcrRectangle {
    /** Координата по оси X левого верхнего угла прямоугольника. */
    readonly x: number;
    /** Координата по оси Y левого верхнего угла прямоугольника. */
    readonly y: number;
    /** Ширина прямоугольника. */
    readonly width: number;
    /** Высота прямоугольника. */
    readonly height: number;
    /**
     * Выполняет проверку, что прямоугольник содержит точку с координатами ({@link x}, {@link y}).
     * @param x Значение координаты точки на оси X.
     * @param y Значение координаты точки на оси Y.
     * @returns `true` - если точка содержится в прямоугольнике, иначе - `false`.
     */
    containsPoint(x: number, y: number): boolean;
}
/** Информация о координатах распознанного элемента. */
export declare class OcrRectangle implements IOcrRectangle, ICloneable<OcrRectangle> {
    x: number;
    y: number;
    width: number;
    height: number;
    constructor(x: number, y: number, width: number, height: number);
    containsPoint(x: number, y: number): boolean;
    clone(): OcrRectangle;
}
