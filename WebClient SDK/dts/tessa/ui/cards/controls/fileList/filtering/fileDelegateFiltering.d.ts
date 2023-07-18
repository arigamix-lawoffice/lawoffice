import { FileFiltering } from './fileFiltering';
import { FileViewModel } from '../fileViewModel';
export declare class FileDelegateFiltering extends FileFiltering {
    constructor(name: string, caption: string, func: (file: FileViewModel) => boolean);
    private _func;
    isVisible(viewModel: FileViewModel): boolean;
    equals(other: FileFiltering): boolean;
}
