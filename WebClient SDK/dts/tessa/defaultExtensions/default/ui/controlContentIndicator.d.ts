import { ICardFieldContainer } from 'tessa/cards';
import { IControlViewModel, IBlockViewModel } from 'tessa/ui/cards';
import { BreadthFirstControlVisitor } from './breadthFirstControlVisitor';
export declare class ControlContentIndicatorVisitor extends BreadthFirstControlVisitor {
    constructor(fieldSectionMapping: Map<string, number>, fieldIDs: Map<string, string>);
    private _index;
    private _fieldSectionMapping;
    private _fieldIDs;
    get index(): number;
    set index(value: number);
    protected visitControl(control: IControlViewModel): void;
    protected visitBlock(_block: IBlockViewModel): void;
}
export declare abstract class ControlContentIndicator<T> {
    constructor(controls: readonly T[], cardFieldContainer: ICardFieldContainer, fieldIDs: Map<string, string>, parentBlockViewModel?: IBlockViewModel | null, indicatorFormat?: string);
    private _fieldControlsMapping;
    private _controlFieldsMapping;
    private _controls;
    private readonly _cardFieldContainer;
    private readonly _parentBlock;
    private readonly _originalControlNames;
    private readonly _originalParentBlockName;
    private readonly _hasContentControls;
    private readonly _indicatorFormat;
    private _disposedValue;
    protected abstract visitControl(visitor: ControlContentIndicatorVisitor, control: T): any;
    protected abstract getDisplayName(control: T): string;
    protected abstract setDisplayName(control: T, name: string): any;
    private fieldChangeAction;
    private update;
    private updateIndex;
    private updateControlName;
    private checkContent;
    private updateParentBlockCaption;
    private getNewName;
    dispose: () => void;
}
