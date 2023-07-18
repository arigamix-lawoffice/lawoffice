import { FileControlObject } from '../fileControlObject';
import { FileViewModel } from '../fileViewModel';
export declare abstract class FileFiltering extends FileControlObject {
    constructor(name: string, caption: string, isCollapsed?: boolean);
    abstract isVisible(viewModel: FileViewModel): boolean;
    abstract equals(other: FileFiltering): boolean;
}
