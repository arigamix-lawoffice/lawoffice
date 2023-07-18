import { Card, CardRow } from 'tessa/cards';
/**
 * Контекст для обработки {@link IWorkflowEngineTileManagerUIExtension}.
 */
export interface IWorkflowEngineTileManagerUIExtensionContext {
    /**
     * Карточка.
     */
    readonly card: Card;
    /**
     * Основная строка из секции BusinessProcessButtons.
     */
    readonly mainRow: CardRow;
    /**
     * Набор строк, дочерних по отношению к {@link IWorkflowEngineTileManagerUIExtension.mainRow}, разделенных по секциям, к которым они относятся.
     */
    readonly allButtonRows: Map<string, CardRow[]>;
    /**
     * Id расширений, относящихся к {@link WorkflowEngineTileManagerUIExtension}.
     */
    readonly extensionIds: Set<guid>;
    /**
     * Строка для построения результата, который будет отображаться в колонке "Настройки".
     */
    result: string;
}
/**
 * @inheritDoc
 */
export declare class WorkflowEngineTileManagerUIExtensionContext implements IWorkflowEngineTileManagerUIExtensionContext {
    /**
     * @inheritDoc
     */
    readonly card: Card;
    /**
     * @inheritDoc
     */
    readonly mainRow: CardRow;
    /**
     * @inheritDoc
     */
    readonly allButtonRows: Map<string, CardRow[]>;
    /**
     * @inheritDoc
     */
    readonly extensionIds: Set<string>;
    constructor(
    /**
     * @inheritDoc
     */
    card: Card, 
    /**
     * @inheritDoc
     */
    mainRow: CardRow, 
    /**
     * @inheritDoc
     */
    allButtonRows: Map<string, CardRow[]>, 
    /**
     * @inheritDoc
     */
    extensionIds: Set<string>);
    result: string;
}
