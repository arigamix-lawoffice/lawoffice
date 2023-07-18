import { IExtension } from 'tessa/extensions';
import { IKrStageTypeUIHandlerContext } from './krStageTypeUIHandlerContext';
import { StageTypeHandlerDescriptor } from './stageTypeHandlerDescriptor';
/**
 * UI обработчик типа этапа.
 */
export interface IKrStageTypeUIHandler extends IExtension {
    /**
     * Выполняется при создании и открытии на редактирование существующей строки с параметрами этапа.
     * @param {IKrStageTypeUIHandlerContext} context {@link IKrStageTypeUIHandlerContext}
     */
    initialize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    /**
     * Выполняется при валидации строки с параметрами этапа перед сохранением или закрытием её окна редактирования.
     * @param {IKrStageTypeUIHandlerContext} context {@link IKrStageTypeUIHandlerContext}
     */
    validate(context: IKrStageTypeUIHandlerContext): Promise<void>;
    /**
     * Выполняется при закрытии строки с параметрами этапа. Вызывается как при закрытии с сохранением строки, так и при отмене.
     * @param {IKrStageTypeUIHandlerContext} context {@link IKrStageTypeUIHandlerContext}
     */
    finalize(context: IKrStageTypeUIHandlerContext): Promise<void>;
}
export declare class KrStageTypeUIHandler implements IKrStageTypeUIHandler {
    shouldExecute(context: IKrStageTypeUIHandlerContext): boolean;
    /**
     * Возвращает список дескрипторов этапов для которых должен выполняться обработчик.
     * @returns {StageTypeHandlerDescriptor[]} Список дескрипторов этапов.
     */
    descriptors(): StageTypeHandlerDescriptor[];
    initialize(_context: IKrStageTypeUIHandlerContext): Promise<void>;
    validate(_context: IKrStageTypeUIHandlerContext): Promise<void>;
    finalize(_context: IKrStageTypeUIHandlerContext): Promise<void>;
}
