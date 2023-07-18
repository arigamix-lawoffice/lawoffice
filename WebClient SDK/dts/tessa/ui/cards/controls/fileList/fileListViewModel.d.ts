import { FileSortingDirection } from './fileListCommon';
import { FileViewModel } from './fileViewModel';
import { FileGrouping } from './grouping';
import { FileSorting } from './sorting';
import { FileGroupViewModel, ReadonlyFileGroupViewModel } from './fileGroupViewModel';
import { FileFiltering } from './filtering';
import { ControlViewModelBase } from '../controlViewModelBase';
import { ICardModel, IControlState } from '../../interfaces';
import { CardTypeControl } from 'tessa/cards/types';
import { ITessaView } from 'tessa/views';
import { FileContainer, IFile, FileCategory, FileType, IFileVersion } from 'tessa/files';
import { ValidationResultBuilder } from 'tessa/platform/validation';
import { MenuAction } from 'tessa/ui/menuAction';
import { CardFileType } from 'tessa/cards';
import { EventHandler } from 'tessa/platform';
import { FileControlExtensionContext } from 'tessa/ui/files';
import { IFileCategoryFilterContext, IFileControl, IFileControlManager, IFileSelectContext, IFileTypeFilterContext, IFileValidateCategoryContext, IFileValidateContentContext } from 'tessa/ui/files/interfaces';
import { IStorage } from 'tessa/platform/storage';
import { UIButton } from 'tessa/ui/uiButton';
export interface FileControlCancelEventArgs {
    fileContainer: FileContainer;
    control: FileListViewModel;
    file: IFile;
    cancel: boolean;
}
export interface FileControlEventArgs {
    fileContainer: FileContainer;
    control: FileListViewModel;
    file: IFile;
}
export declare type FileListCategoryFilter = (context: IFileCategoryFilterContext) => Promise<ReadonlyArray<FileCategory | null>>;
export declare type FileListTypeFilter = (context: IFileTypeFilterContext) => Promise<ReadonlyArray<FileType>>;
export declare type FileListSelect = (context: IFileSelectContext) => Promise<void>;
export declare type FileListValidateContent = (context: IFileValidateContentContext) => Promise<void>;
export declare type FileListValidateCategory = (context: IFileValidateCategoryContext) => Promise<void>;
export declare type FileListGroupSorting = (groups: ReadonlyMap<string, FileGroupViewModel>) => void;
/**
 * Элемент управления файлами.
 */
export declare class FileListViewModel extends ControlViewModelBase implements IFileControl {
    constructor(control: CardTypeControl, model: ICardModel, defaultFileSorting: string | null, sortDirection: FileSortingDirection, defaultFileGrouping: string | null, defaultGroupsExpanded: boolean, isCategoriesEnabled: boolean, isManualCategoriesCreationDisabled: boolean, isNullCategoryCreationDisabled: boolean, isPreservingCategoriesOrder: boolean, isIgnoreExistingCategories: boolean, categoriesView: ITessaView | null, categoriesViewMapping: any[] | null, previewControlName: string, fileTypes: CardFileType[] | null);
    private _files;
    private _filesAtom;
    private _model;
    private _categoriesView;
    private _categoriesViewMapping;
    private _groupings;
    private _sortings;
    private _actions;
    private _fileActions;
    private _versionActions;
    private _selectedSorting;
    private _selectedSortDirection;
    private _cachedGroups;
    private _selectedGrouping;
    private _groupsExpanded;
    private _selectedFiltering;
    private _isCategoriesEnabled;
    private _isManualCategoriesCreationDisabled;
    private _isNullCategoryCreationDisabled;
    private _isPreservingCategoriesOrder;
    private _isIgnoreExistingCategories;
    private _previewControlName;
    private _categoryFilter;
    private _typeFilter;
    private _modifyFileSelect;
    private _validateFileContent;
    private _validateFileCategory;
    private _cryptoProPluginEnabled;
    private _denyMobileFileDownload;
    private _manager;
    private _fileContainer;
    private _multiSelectionMode;
    private _selectionPreviewMessageDisposer;
    private _fileTypes;
    private _fileControlButtons;
    protected _initializeDefaultSettings: () => void;
    protected _disposes: Array<() => void>;
    get model(): ICardModel;
    get fileControlButtons(): UIButton[];
    get visibleFileControlButtons(): readonly UIButton[];
    get fileContainer(): FileContainer;
    set fileContainer(value: FileContainer);
    get files(): readonly FileViewModel[];
    get filteredFiles(): readonly FileViewModel[];
    private get sortedFiles();
    get groupings(): ReadonlyArray<FileGrouping>;
    get sortings(): ReadonlyArray<FileSorting>;
    get actions(): ReadonlyArray<MenuAction>;
    get fileActions(): ReadonlyArray<MenuAction>;
    get versionActions(): ReadonlyArray<MenuAction>;
    get groups(): ReadonlyMap<string, ReadonlyFileGroupViewModel> | null;
    get isFileExists(): boolean;
    get selectedSorting(): FileSorting | null;
    set selectedSorting(value: FileSorting | null);
    get selectedSortDirection(): FileSortingDirection;
    set selectedSortDirection(value: FileSortingDirection);
    get selectedGrouping(): FileGrouping | null;
    set selectedGrouping(value: FileGrouping | null);
    get groupsExpanded(): boolean;
    set groupsExpanded(value: boolean);
    get selectedFiltering(): FileFiltering | null;
    set selectedFiltering(value: FileFiltering | null);
    get isCategoriesEnabled(): boolean;
    set isCategoriesEnabled(value: boolean);
    get isManualCategoriesCreationDisabled(): boolean;
    set isManualCategoriesCreationDisabled(value: boolean);
    get isNullCategoryCreationDisabled(): boolean;
    set isNullCategoryCreationDisabled(value: boolean);
    get isPreservingCategoriesOrder(): boolean;
    set isPreservingCategoriesOrder(value: boolean);
    get previewControlName(): string;
    set previewControlName(value: string);
    get isIgnoreExistingCategories(): boolean;
    set isIgnoreExistingCategories(value: boolean);
    get categoryFilter(): FileListCategoryFilter | null;
    set categoryFilter(value: FileListCategoryFilter | null);
    get typeFilter(): FileListTypeFilter | null;
    set typeFilter(value: FileListTypeFilter | null);
    get modifyFileSelect(): FileListSelect | null;
    set modifyFileSelect(value: FileListSelect | null);
    get validateFileContent(): FileListValidateContent | null;
    set validateFileContent(value: FileListValidateContent | null);
    get validateFileCategory(): FileListValidateCategory | null;
    set validateFileCategory(value: FileListValidateCategory | null);
    groupSorting: FileListGroupSorting | null;
    get cryptoProPluginEnabled(): boolean;
    get manager(): IFileControlManager | null;
    set manager(value: IFileControlManager | null);
    readonly info: IStorage;
    get multiSelectionMode(): boolean;
    set multiSelectionMode(value: boolean);
    private _controlExtensionExecutor;
    private get controlExtensionExecutor();
    private executeControlExtensions;
    private _fileExtensionExecutor;
    private get fileExtensionExecutor();
    private executeFileExtensions;
    private _fileVersionExtensionExecutor;
    private get fileVersionExtensionExecutor();
    private executeFileVersionExtensions;
    protected initializeCore(): void;
    private initializeInternal;
    finalizeControlInitialization(modifyContext?: (ctx: FileControlExtensionContext) => void): void;
    getState(): IControlState;
    setState(state: IControlState): boolean;
    handleDropFiles: (contents: ReadonlyArray<File>) => Promise<void>;
    private tryGetDenyMobileFileDownload;
    addFile(file: IFile): FileViewModel;
    addFiles(files: ReadonlyArray<IFile>): ReadonlyArray<FileViewModel>;
    removeFile(file: IFile | FileViewModel): void;
    removeFiles(files: ReadonlyArray<IFile | FileViewModel>): void;
    removeFiles(filter: (f: FileViewModel) => boolean): void;
    getControlActions: () => ReadonlyArray<MenuAction>;
    getFileActions: (file: FileViewModel, withSelectedFiles?: boolean) => ReadonlyArray<MenuAction>;
    getFileVersionActions: (file: FileViewModel, sourceFileVersion: IFileVersion, fileVersions: IFileVersion[], dialog: any) => ReadonlyArray<MenuAction>;
    fileDoubleClickAction: ((file: FileViewModel) => Promise<void>) | null;
    resetSelectionOfAnotherControls(): void;
    private getDefaultFileTypes;
    private onFileContainerChanged;
    readonly containerFileAdding: EventHandler<(args: FileControlCancelEventArgs) => void>;
    readonly containerFileAdded: EventHandler<(args: FileControlEventArgs) => void>;
    readonly containerFileRemoving: EventHandler<(args: FileControlCancelEventArgs) => void>;
    readonly containerFileRemoved: EventHandler<(args: FileControlEventArgs) => void>;
    onUnloading(validationResult: ValidationResultBuilder): void;
}
export declare class FileListViewModelState {
    constructor(control: FileListViewModel);
    readonly groupingName: string | null;
    readonly sortingName: string | null;
    readonly sortDirection: FileSortingDirection;
    apply(control: FileListViewModel): boolean;
}
