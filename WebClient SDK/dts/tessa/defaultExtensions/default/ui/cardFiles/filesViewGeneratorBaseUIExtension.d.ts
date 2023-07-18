import { Lambda } from 'mobx';
import type { FileControlCreationParams } from './fileControlCreationParams';
import { CardUIExtension, ICardModel } from 'tessa/ui/cards';
import { ViewControlViewModel, FileListViewModel, IViewControlInitializationStrategy } from 'tessa/ui/cards/controls';
export declare class FilesViewGeneratorBaseUIExtension extends CardUIExtension {
    readonly _dispose: Array<Function | Lambda | null>;
    finalized(): void;
    protected initializeFileControl(model: ICardModel, viewControlName: string, creationParams: FileControlCreationParams): Promise<void>;
    protected attachViewToFileControl(cardModel: ICardModel, viewControlName: string, initializationStrategy?: IViewControlInitializationStrategy, viewModifierAction?: (viewControl: ViewControlViewModel) => void): Promise<FileListViewModel | null>;
    protected initializeSelection(viewControl: ViewControlViewModel, fileControl: FileListViewModel): void;
    protected initializeGrouping(viewControl: ViewControlViewModel, fileControl: FileListViewModel): void;
    protected initializeFiltering(viewControl: ViewControlViewModel, fileControl: FileListViewModel): void;
    protected initializeClickCommands(viewControl: ViewControlViewModel, _fileControl: FileListViewModel): void;
    protected initializeMenuButton(viewControl: ViewControlViewModel, fileControl: FileListViewModel): void;
    protected initializeContextMenu(viewControl: ViewControlViewModel, fileControl: FileListViewModel): void;
}
