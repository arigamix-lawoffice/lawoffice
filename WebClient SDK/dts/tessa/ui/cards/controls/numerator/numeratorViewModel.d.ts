import { ControlViewModelBase } from '../controlViewModelBase';
import { ICardModel, ControlKeyDownEventArgs } from '../../interfaces';
import { CardTypeCustomControl } from 'tessa/cards/types';
import { NumberType } from 'tessa/cards/numbers';
import { EventHandler } from 'tessa/platform';
import { MenuAction } from 'tessa/ui';
import { ValidationResultBuilder } from 'tessa/platform/validation';
import { ControlButtonsContainer } from '../controlButtonsContainer';
/**
 * Модель представления для элемента управления, выполняющего ввод и управление номерами.
 */
export declare class NumeratorViewModel extends ControlViewModelBase {
    constructor(control: CardTypeCustomControl, model: ICardModel);
    private readonly _buttonsContainer;
    private _specialToolTip;
    private readonly _numbersToRelease;
    private readonly _cardModel;
    private readonly _fields;
    private readonly _stateProvider;
    private readonly _sectionName;
    private readonly _fullNumberFieldName;
    private readonly _numberFieldName;
    private readonly _sequenceFieldName;
    private _fullNumberMaxLength;
    private _numberType;
    private _numberIsSequential;
    private _numberBuilder;
    private _numberDirector;
    private _isReserved;
    get numberType(): NumberType;
    set numberType(value: NumberType);
    get computedToolTip(): string | null;
    get fullNumber(): string | null;
    set fullNumber(value: string | null);
    get number(): string | null;
    set number(value: string | null);
    get sequence(): string | null;
    set sequence(value: string | null);
    get fullNumberMaxLength(): number;
    set fullNumberMaxLength(value: number);
    get numberIsSequential(): boolean;
    get inPlaceEditorIsReadOnly(): boolean;
    get error(): string | null;
    get hasEmptyValue(): boolean;
    get buttonsContainer(): ControlButtonsContainer;
    readonly contextMenuGenerators: ((ctx: NumeratorMenuContext) => void)[];
    private getToolTip;
    private computeToolTip;
    private getNumberContext;
    private defaultContextMenuGenerator;
    private getNumberIsSequential;
    private reserveNumberAction;
    private reserveNumber;
    private releaseNumberAction;
    private releaseNumber;
    private setManualNumberAction;
    private editNumberAction;
    private editNumber;
    private fieldChangedHandler;
    getContextMenu(): MenuAction[];
    onUnloading(validationResult: ValidationResultBuilder): void;
    readonly keyDown: EventHandler<(args: ControlKeyDownEventArgs) => void>;
    initializeButtons(): void;
}
export interface NumeratorMenuContext {
    readonly control: NumeratorViewModel;
    readonly menuActions: MenuAction[];
}
