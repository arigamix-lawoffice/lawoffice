import { IGridCellViewModel, IGridRowViewModel } from 'components/cardElements/grid';
import { StageGroup } from './stageGroup';
import { StageSelectorViewModel } from './stageSelectorViewModel';
import { StageType } from './stageType';
export declare class StageDialogRowAdapter implements IGridRowViewModel {
    constructor(model: StageType | StageGroup, viewModel: StageSelectorViewModel, id: string);
    private _viewModel;
    private _id;
    private _type;
    private _cells;
    get id(): string;
    get cells(): IGridCellViewModel[];
    get showChildren(): boolean;
    get showOverflow(): boolean;
    get isSelected(): boolean;
    set isSelected(value: boolean);
    get isLastSelected(): boolean;
}
