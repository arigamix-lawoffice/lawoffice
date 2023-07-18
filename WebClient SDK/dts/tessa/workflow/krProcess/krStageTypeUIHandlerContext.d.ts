import { GridRowAction, GridViewModel } from 'tessa/ui/cards/controls';
import { ICardModel, IFormWithBlocksViewModel } from 'tessa/ui/cards';
import { CardRow } from 'tessa/cards';
import { IValidationResultBuilder } from 'tessa/platform/validation';
/**
 * Контекст расширений {@link IStageTypeUIHandler}.
 */
export interface IKrStageTypeUIHandlerContext {
    /**
     * Идентификатор типа этапа.
     */
    readonly stageTypeId: guid;
    /**
     * Действие со строкой {@link row} или значение null, если выполняется валидация строки с параметрами этапа ({@link IStageTypeUIHandler.validate(IKrStageTypeUIHandlerContext)}).
     */
    readonly action: GridRowAction | null;
    /**
     * Элемент управления, в рамках которого выполняется событие.
     */
    readonly control: GridViewModel;
    /**
     * Строка карточки с параметрами этапа, с которой производится действие.
     */
    readonly row: CardRow;
    /**
     * Модель строки {@link row} с параметрами этапа вместе с формой.
     *
     * В данном объекте содержатся элементы управления из всех зарегистрированных типов карточек настроек этапов. Для обращения к формам, соответствующим типу текущего этапа, используйте свойство {@link settingsForms}.
     *
     * Используйте данное свойство для обращения к общим элементам управления, заданным в типе KrTemplateCard.
     */
    readonly rowModel: ICardModel;
    /**
     * Модель карточки, в которой расположена строка {@link row}.
     */
    readonly cardModel: ICardModel;
    /**
     * {@link IValidationResultBuilder}
     */
    readonly validationResult: IValidationResultBuilder;
    /**
     * Коллекция, содержащая формы из типа карточки настроек текущего этапа.
     *
     * Для обращения к общим элементам управления, заданным в типе KrTemplateCard, используйте свойство {@link rowModel}.
     */
    readonly settingsForms: ReadonlyArray<IFormWithBlocksViewModel>;
}
export declare class KrStageTypeUIHandlerContext implements IKrStageTypeUIHandlerContext {
    constructor(stageTypeId: guid, action: GridRowAction | null, control: GridViewModel, row: CardRow, rowModel: ICardModel, cardModel: ICardModel, validationResult: IValidationResultBuilder);
    readonly stageTypeId: guid;
    readonly action: GridRowAction | null;
    readonly control: GridViewModel;
    readonly row: CardRow;
    readonly rowModel: ICardModel;
    readonly cardModel: ICardModel;
    readonly validationResult: IValidationResultBuilder;
    readonly settingsForms: ReadonlyArray<IFormWithBlocksViewModel>;
    private static getSettingsForms;
}
