import { IKrStageTypeUIHandlerContext, KrStageTypeUIHandler } from 'tessa/workflow/krProcess';
/**
 * UI обработчик этапов, настраивающий заголовки стандартных вкладок на форме с настройками этапа: Условие, Инициализация, Постобработка.
 */
export declare class TabCaptionUIHandler extends KrStageTypeUIHandler {
    private _dispose;
    initialize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    finalize(): Promise<void>;
}
