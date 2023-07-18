import { ICardFieldContainer } from 'tessa/cards';
import { ControlContentIndicator, ControlContentIndicatorVisitor } from './controlContentIndicator';
import { IBlockViewModel } from 'tessa/ui/cards';
export declare class BlockContentIndicator extends ControlContentIndicator<IBlockViewModel> {
    constructor(block: IBlockViewModel, cardFieldContainer: ICardFieldContainer, fieldIDs: Map<string, string>);
    protected visitControl(visitor: ControlContentIndicatorVisitor, control: IBlockViewModel): void;
    protected getDisplayName(control: IBlockViewModel): string;
    protected setDisplayName(control: IBlockViewModel, name: string): void;
}
