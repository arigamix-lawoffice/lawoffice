import { ControlViewModelBase } from './controlViewModelBase';
import { CardTypeControl } from 'tessa/cards/types';
import { IFilePreviewControl, IFileControl, IFileControlManager } from 'tessa/ui/files';
import { MediaStyle } from 'ui';
/**
 * Элемент управления предпросмотром в карточке.
 */
export declare class FilePreviewViewModel extends ControlViewModelBase implements IFilePreviewControl {
    constructor(control: CardTypeControl, minHeight?: string, maxHeight?: string);
    private _minHeight?;
    private _maxHeight?;
    /**
     * Минимальная высота элемента управления.
     * Пример: 10px
     */
    get minHeight(): string | undefined;
    set minHeight(value: string | undefined);
    /**
     * Максимальная высота элемента управления.
     * Пример: 10px
     */
    get maxHeight(): string | undefined;
    set maxHeight(value: string | undefined);
    /**
     * Объект, управляющий доступностью предпросмотра.
     */
    fileControlManager: IFileControlManager;
    attach(fileControl: IFileControl): void;
    getControlStyle(): MediaStyle | null;
}
