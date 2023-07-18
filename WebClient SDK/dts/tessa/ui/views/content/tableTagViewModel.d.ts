import { CellTag, CellTagCtor } from 'components/cardElements/grid/cellTag';
import { Command, CommandFunc } from 'tessa/platform';
export declare class TableTagViewModel extends CellTag {
    constructor(args: {
        handler?: CommandFunc;
    } & CellTagCtor);
    protected _command: Command;
    get command(): Command;
}
