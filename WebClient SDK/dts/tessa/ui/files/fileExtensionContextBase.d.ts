import { IFileExtensionContextBase, IFileControl } from './interfaces';
import { IStorage } from 'tessa/platform/storage';
import { MenuAction } from 'tessa/ui/menuAction';
export declare abstract class FileExtensionContextBase implements IFileExtensionContextBase {
    constructor(control: IFileControl, getActionsFunc: (c: IFileControl) => MenuAction[], cloneCollections: boolean);
    readonly control: IFileControl;
    readonly actions: MenuAction[];
    readonly info: IStorage;
}
