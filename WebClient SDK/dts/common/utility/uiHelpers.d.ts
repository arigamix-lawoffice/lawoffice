/**
 * Дополнения числа символами (нулями, если не задано)
 * @param  {number} n     число
 * @param  {number} width нужный размер
 * @param  {string} z     символ заполнения
 * @return {string}
 */
export declare function pad(n: number | string, width: number, z?: string): string;
/**
 * Разбивает строку на меньшие строки, длиной 30 по умолчанию
 * @param  {string} content   строка
 * @param  {number} maxLength длина куска строки
 * @return {string}
 */
export declare function forceBreakLongWord(content: string, maxLength?: number): string;
export declare const globalSkipViewRenderTimer: {
    timer: number | null;
};
export interface IHtmlAbsolutePosition {
    position?: string;
    top?: number | string;
    bottom?: number | string;
    left?: number | string;
    right?: number | string;
    minWidth?: string;
}
export interface IHtmlElementPosition {
    height: number;
    width: number;
    top: number;
    left: number;
    topOffsetFromTopSide: number;
    bottomOffsetFromTopSide: number;
    leftOffsetFromLeftSide: number;
    rightOffsetFromLeftSide: number;
    topOffsetFromBottomSide: number;
    bottomOffsetFromBottomSide: number;
    leftOffsetFromRightSide: number;
    rightOffsetFromRightSide: number;
}
/**
 * Возвращает позицию html-элемента
 * @param  {HTMLElement}           elem
 * @return {IHtmlElementPosition}
 */
export declare function getElementPosition(elem: HTMLElement): IHtmlElementPosition;
export declare function getRectPosition(box: ClientRect | DOMRect): IHtmlElementPosition;
export declare type DropdownOpenDirection = 'left' | 'right';
/**
 * Позиционирует элемент в видимой части окна
 * @param {HTMLElement} element [description]
 * @param {number   =       10}          offset       подстройка алгоритма
 * @param {DropdownOpenDirection   =       'right'}      openDirection в какую сторону открывается
 */
export declare function updateDropdownPosition(element: HTMLElement, offset?: number, openDirection?: DropdownOpenDirection): void;
/**
 * Позиционирует элемент относительно контейнера
 * @param {HTMLElement}           element
 * @param {HTMLElement}           container
 * @param {IHtmlAbsolutePosition} style     стиль, который уже есть у элемента
 * @param {number             =         10}          offsetX подстройка алгоритма по оси X
 * @param {number             =         10}          offsetY подстройка алгоритма по оси Y
 */
export declare function updateDropdownPositionInContainer(element: HTMLElement, container: Element, style?: IHtmlAbsolutePosition, offsetX?: number, offsetY?: number): void;
/**
 * the edge does't support the closest property
 * @param  {Element} el
 * @param  {string}  selector
 * @return {Element}
 */
export declare function closest(el: Element, selector: string): Element | null;
export interface IFileNameExt {
    name?: string;
    ext?: string;
}
/**
 * Вернуть название файла и его расширение
 * @param  {string    | null          | undefined} fileName целое имя
 * @return {IFileNameExt}
 */
export declare function getNameAndExtForFile(fileName: string | null | undefined): IFileNameExt;
export declare function debounce(func: Function, wait: number, immediate?: boolean): () => void;
export declare const launcherIcon64 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAFoAAABaCAYAAAA4qEECAAAAIGNIUk0AAHomAACAhAAA+gAAAIDoAAB1MAAA6mAAADqYAAAXcJy6UTwAAAAGYktHRAD/AP8A/6C9p5MAAABEdEVYdFJhdyBwcm9maWxlIHR5cGUgaXB0YwAKaXB0YwogICAgICAxNQoxYzAyNmUwMDAzNTI0NjQ3MWMwMjAwMDAwMjAwMDQKt4CGFQAADGZJREFUeNrtm3lwXPV9wD+/t29vaVe70kqybBnjC1s2BGowHoNJYii+CQmhZTolaRjSGDfTkHbyD5MQGspAnZC0HXJOU9oMmSSQBppJmIamhLq2cWwL32DFl+57deyhvd+3f6xky7J17iEf7zOzo5nd936/9/u8r77vdz0wMTExMTExMTExMTExMTExMTExMTEpFKcGYzLb13CloBWy8D2dYV4+2S3hZPq6F15Q0euqPbzZPMDOwx00hxPXteyCil7kdaiP1njY1RnmKwda2dMZum5lF1Q0wIbaMirsOufCCZ57r51/a+i5LmUXXPQir0Otn+dBKYimDf69oYen9jdL03WWSgouGmBTbRkBhw6AALs7wnx5fwu72q+fVFIU0Qs9DrV+rudCpUrRHE3y/OF2fvhBt4Sug15JUUQDbJiXzdUjRhUQSxu8crqXFw61czYUv6ZlF030SK4eiwjs7Yrw9IFW3rmGU0nRREM2V1c4LkT1aFqiSV441Ma/fNB1TaaSoope6HGoe2s8l/1NAfGM8MqpIM/Wt3H6Ghu+F1U0XOhXT2Rxf0+Upw+28tvWwWtGdtFFj5erR6OAtkiKrx9p5zvHO2UgcfWnkqKLBtg4Qa4eQSlIZIRXz2ZTScPA1Z1KZkX0Io9Dra/xTPFoxcGeKF892MpvWgauWtmzIhqyUV0+Sa4eQSnoGErxzSMdfOd4p/TFU1ed8FkTvch78WhxMhSQMITXzvbxtfo2TvZfXalk1kQDbJo/ea4eiwCHgkM8fbCF/2q+elKJmujHAyeapKG1l7kVXmorvXhLnAR8JWqqhU+Fbx/vlFfP9jHdQgWwabDlBh+fWlKB32HN63VdjmPBqMQywurK6TuY8IS+waj88Nf7eWPXcTRNUVbqYm6Fh+ryUmoDZSyY46fS56bCW8KcCs+MGnpmMC5f2tdMMJGetuwswi3lbrYvr6TO78q77MZwXOq7o+zpinByII5CeGxZgIcWlk+rrikd/LPfHpbv/udegqEhkOzDSdM0NKWwWy34PS78pS7mVnq5sdpHdbmH2soyqvwluJ12ykqcE9bz0vFOeW0GUX1etUClS+fTSwNsvcGXs+yuoaQcCQ6xpzPMsf4Y/fE0xnC7AWya4pHF5Ty2rHLKdU35wHePNcrOn/yOM2195yscwRAZLkghIugWDYddx2W3UelzU1Pupbq8lIVz/MyrLKPc6yZQ5sY7fAMawwn50u+b6R5KzVg2gFUptt5Qxp8tqSDgnF4qCSXTcnIgzu6OMPW9ETqHUqSNrKGxBQmgK9i2wMeTN8+ZUj3TupjTrb3yzZ/+L3uONU56pgwXLiIYAhZNoWkKDfC4nZR7XVR43dw4x8+8gJc9CY0T2HMSPcJKv5PtdVWsnEIqOdE3JPu6wuzrjtIUTpDIyGXljifvj2vL2FFXSZldV5MdOy36Q0Py7df38sb/HSOZNmYkRmS4s6BU9v9eBKunFPfdd6C5XdnvckCACofOZ266fCppCifkYE+EvV1RGgZjRFLG+cA43yAZdZ0TiBOBdTWl/PXKKiqdNjXZ8dPmx7+pl+//ch8DkRhK5R6HYgjOFUtx1i3JWfSIbKuWnZr99NIAIsKh3gi728Mc7Y3QH09hGAIZA4wMks5+yKQx0hm8DitrVy7AbtPRyMZE9kYoFNl+sWX4zgiwOuBmTXVp/kUD/K7+tLz4s3do6hpAy1W2CJrbRek9d+Ylqke3cH48SvhUI23hGOlUBkmnIZ1BMhkwjOxHJBvBhqABX3x4HZ/afEfeejE5F3SqpUf+4ZW3OdDQmvvViOBYsRTn8vxEdbaFikj9MRJnGtGUdvkWjwmSbXfV8fef3ZTXrmLOI8MltQH13Oc287G7V2DRVM5+kk2tGNGhSxo/UySRxOjrR9M00NRwDhjzGcYQoW5BFTseXJuXukeTlyF4lb9Ufe3xjWrHx9dS4rTNXLZSGJEhEk1t+WmdUqQHQ2Qik984ESHgdfO3j3yYmoA37wOfvM51PL5tjXrmM/czN+DByCG08xnV6d6+bE6eBJtu4S8fWMPty2oLMpTP+6TS/XfepHY+sZVbF9fMrAClMKJDJJpzj2rJZEj3BKfUK3pw3Uoeue+2gs2XFGT27uZFc9TOJ7ayec2yGQdlzlGtFEYkSmYwMmEZIsIfLZ3LZx9YUwgV59ELVfCcCo+KDCVkXqCMV96qJxpPTr2/PZKrm9uyPZCZoCDd248kk+OKFhGqfaX8zZ9+mCp/acGiGQooGqDEZVcAv9rzvnzr1V30DESmNbhJNrZiq63BUuKefnfPEFI9wex549TpsFn5/EN3c/Oiqc1X5EJRJv633lWndj6xhRULqrOjsSkggMtIc6Oa/EF2CUphxOJk+gYmjOZPfuQWHli3ouCSoYgrLKuW1aqdO7Zw36rFWLQJcuawhKW1AZ59fBNfvv9mAg7LtFZhUJDuH8SIxS8rWkRYu/IGHt92Z7GaX9jUMZbaKp8KRWLyr28e4MdvHSKRSl2USgwRXHYr29bW8djW1cwpz64prq/x8tMzwalXJJDuCSKGgdIujiURmF/p4wsP34Pfk/+FgvEoqmgAz/Ac9GtvH5bvvvEuPYNRNKUwRFg4p5ztH1vD+lVLsFkt58/ZWOvl7fZBumNTW4WRZIp0b98lzwMBXHadzz90F8sXVBVNMszi4uzD629VL2zfwtJ5FdgsGg/evZJ/fvJBNq5ZdpFkgAWldtbXeKdWsFJkQmEykeglaUPXFI9uWMXGNcuKKhlmIaJHs7puvnrr/VY52tzLjo/W4bRZxz12Q62X/2kbpCc+SVQrSPX2QSqTndsYxjCEez60kEc33s5fzUJbLbkXMXN+2dgvP2oK8wdDx2LRWOxxYB3nQVlm1+lLZDjeH5tQtGQM4g1nMEbNb4jA4nkVPPPY/VSXz2wROVdmRXRjKC6+h7Y/8+rZIKFkhlTG4EgwSjCeZrnPiVO/NKMpsqsme7vCRMdb2Rke6MQbzmYn9IcP8rjtPPXn67lt6bxZkQyzIPrN5n75x2OdHO2LZccSw98LcCqU4ORAnEUeB+WOS7NamV0nGJ8gqjVFqqOLVEv7Rfn5cw+s4RMfuWXWJEMRH4YtkYQ8/16bfOtoJ63R1GWPUcCR4BB/V9/Ku10RLje22VDrHX93kyGku4MXjSI3rr6JR+69rVjNHJeiRPR/twzKi0c7qO8durAIOg4KCKUMDnRH0DXFkjInllHROZKrT4yNagVGPEH85GkklUKAZfMr+cpf3Jf33VUzoaAR3TGUlG8caZevH2mnOZJEm+qcElnZP/igm++d6KI/kb7ot021XgLOsVGtyAyEMIZiCApfqYsv/sk91FblvqEmHxQsot9pD8mLRzr4fVcUg+nPdiqyebthIM6ZUIJlZU68tuzlltl1+uLpi3O1UiTONZPuCWLVLez4+Fq23FV3RUiGAkR011BS/ulohzz/XhvnwomcF0kE2N8d4asHW9jfHTkfxWPfhZFUdjQIsHXtch7dePsVIxnyLHp3R0ie2t/CLxr7SRiSl11HkH3T9lw4yXOH2vlVUz8pQ7ix1M69I+/CKEUmHCUTivChJXPZXoDF1ZzbkI9CemMpeelYpzx3qJ3ToUTeBI9GAYPJDC8d7+IHH3QRTRtsri0j4Mx2A1M9QfxOG08+vG7GO1sLSc45el9XWF482sGuzjDpPEbx5VBARuD9/hhN4QSrq0rICBztjcDZRr6weRX33XHTFScZcpjrCMZT8vOzfTxb30YkZeRrG8aUEBS7uyJ0xVJsXeCjPJPi9pXz+eT6W69IyTDDnUoHuyPyckMPJ/pjk/aLp0t24j9bplJceNiJoFBYNYXVkv1rARZ5HaxwKrYu8BMom/3+8nhM68IGE2n5j7N9vN7YTyiVmfLJo2/GaJEozm8gBNBQuHQNl1XDrWu4dA2fXcdv1yl36JRaLXhsFnw2Cz67jlPX0DVFeRFeqyia6CO9UXm5oYfDwaFLJI6sf470fREQBE0NR+Dwx27R8Fg1vHYdn82C22rBZ7dQYdfxO3TcugW3VaPEaqHaZbvi5eVV9EAiLa+f6+Pn5/qIJI3z29eE7A57lz4cfVYLLl3Db9epcGQj0K1reGwW/HYdn13HZlHYNG3STdvXIhM2OBhLyS/O9fGHwTheuwWPVcdvt1Dh0PE7rDgtGm6rRqnVMu1XGUzG0J+4+t5SNTExMTExMTExMTExMTExMTExMTExMble+X+Z2uD228LvLgAAACV0RVh0ZGF0ZTpjcmVhdGUAMjAxNy0wMS0xNlQxMjowNzoxMCswMTowMHfsa88AAAAldEVYdGRhdGU6bW9kaWZ5ADIwMTctMDEtMTZUMTI6MDc6MTArMDE6MDAGsdNzAAAAAElFTkSuQmCC";
export declare function getTessaIcon(iconName: string): string;
