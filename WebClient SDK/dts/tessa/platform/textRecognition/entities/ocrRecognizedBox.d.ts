/** Информация о распознанном элементе. */
export interface IOcrRecognizedBox {
    /** Координата по оси X левого верхнего угла элемента. */
    readonly x: number;
    /** Координата по оси Y левого верхнего угла элемента. */
    readonly y: number;
    /** Ширина элемента. */
    readonly width: number;
    /** Высота элемента. */
    readonly height: number;
    /** Текст, который содержит элемент. */
    readonly text: string;
    /** Пороговый коэффициент уверенности. */
    readonly confidence: number;
    /** Угол поворота элемента. */
    readonly rotation: number;
    /** Язык, которым написан {@link text} элемента. */
    readonly language: string;
    /** Цвет элемента. */
    readonly color: ReadonlyArray<string>;
    /**
     * Выполняет проверку, что элемент содержит точку с координатами ({@link x}, {@link y}).
     * @param x Значение координаты точки на оси X.
     * @param y Значение координаты точки на оси Y.
     * @returns `true` - если точка содержится в элементе, иначе - `false`.
     */
    containsPoint(x: number, y: number): boolean;
}
/** Информация о распознанном элементе. */
export declare class OcrRecognizedBox implements IOcrRecognizedBox {
    /** Элемент с исходными координатами. */
    private readonly source;
    /** Элемент с вычисленными координатами. */
    private readonly target;
    readonly text: string;
    readonly confidence: number;
    readonly rotation: number;
    readonly language: string;
    readonly color: ReadonlyArray<string>;
    /** Координата по оси X левого верхнего угла прямоугольника. */
    get x(): number;
    /** Координата по оси Y левого верхнего угла прямоугольника. */
    get y(): number;
    /** Ширина прямоугольника. */
    get width(): number;
    /** Высота прямоугольника. */
    get height(): number;
    /** Набор доступных цветов для элемента. */
    private static readonly colors;
    constructor(box: IOcrRecognizedBox, index: number);
    /**
     * Выполняет пересчет координат элемента.
     * @param angle Угол поворота страницы.
     * @param scale Масштаб страницы.
     * @param offset Смещение страницы.
     * @param width Ширина страницы.
     * @param height Высота страницы.
     */
    invalidate(angle: number, scale: number, offset: number, width: number, height: number): void;
    containsPoint(x: number, y: number): boolean;
}
