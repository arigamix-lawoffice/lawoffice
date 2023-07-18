import { IGridRowTagViewModel } from 'components/cardElements/grid/interfaces';
import { SchemeDbType } from 'tessa/platform';
import { ViewControlViewComponent } from 'tessa/ui/cards/controls';
import { ISelectionState } from 'tessa/ui/views';
export declare class CardTableViewComponent extends ViewControlViewComponent {
    private _savedSelectionState;
    initColumns(columns: ReadonlyMap<string, SchemeDbType>): void;
    protected resetData(): void;
    protected updateData(columns: ReadonlyMap<string, SchemeDbType>, rows: ReadonlyArray<ReadonlyMap<string, any>>, rowCount: number, tags: Map<guid, IGridRowTagViewModel[]>, selectionState?: ISelectionState | null): void;
}
