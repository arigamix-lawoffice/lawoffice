import { ICardModel, IControlViewModel, IFormWithBlocksViewModel, IBlockViewModel } from 'tessa/ui/cards';
export declare abstract class BreadthFirstControlVisitor {
    protected abstract visitControl(control: IControlViewModel): any;
    protected abstract visitBlock(block: IBlockViewModel): any;
    visitByCard(cardModel: ICardModel): void;
    visitByRootControl(rootControl: IControlViewModel): void;
    visitByForm(form: IFormWithBlocksViewModel): void;
    visitByBlock(block: IBlockViewModel): void;
    private visitInternal;
    private enqueueTabs;
    private enqueueForm;
    private enqueueBlock;
}
