import { FieldMapStorage } from 'tessa/cards/fieldMapStorage';
import { NumeratorViewModel } from 'tessa/ui/cards/controls';
export declare class NumeratorDialogViewModel {
    constructor(numeratorViewModel: NumeratorViewModel, fields: FieldMapStorage, fullNumberFieldName: string, numberFieldName: string, sequenceFieldName: string | null, fullNumberMaxLength: number, isReadOnly: boolean, sequenceIsVisible: boolean);
    private readonly _numeratorViewModel;
    private readonly _fields;
    private readonly _fullNumberFieldName;
    private readonly _numberFieldName;
    private readonly _sequenceFieldName;
    readonly fullNumberMaxLength: number;
    readonly isReadOnly: boolean;
    readonly sequenceIsVisible: boolean;
    readonly sequenceWarningIsVisible: boolean;
    readonly serialNumberWarningIsVisible: boolean;
    get fullNumber(): string | null;
    set fullNumber(value: string | null);
    get number(): number | null;
    set number(value: number | null);
    get sequence(): string | null;
    set sequence(value: string | null);
    get computedTooltip(): string | null;
    get error(): string | null;
}
