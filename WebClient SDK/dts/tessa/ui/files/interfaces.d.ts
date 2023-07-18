import { IExtension } from 'tessa/extensions';
import { MenuAction } from 'tessa/ui/menuAction';
import { FileContainer, IFileVersion, IFile, FileCategory, FileType } from 'tessa/files';
import { FileViewModel, FileSortingDirection, FileListCategoryFilter, FileListTypeFilter, FileControlCancelEventArgs, FileControlEventArgs, FileListGroupSorting, FileListVersionsDialogViewModel, ReadonlyFileGroupViewModel, FileListSelect, FileListValidateContent, FileListValidateCategory } from 'tessa/ui/cards/controls';
import { FileGrouping } from 'tessa/ui/cards/controls/fileList/grouping';
import { FileSorting } from 'tessa/ui/cards/controls/fileList/sorting';
import { FileFiltering } from 'tessa/ui/cards/controls/fileList/filtering';
import { IControlState, ICardModel, IControlViewModel } from 'tessa/ui/cards';
import { EventHandler, Result } from 'tessa/platform';
import { IStorage } from 'tessa/platform/storage';
import { IPreviewerViewModel } from 'tessa/ui/cards/controls/previewer/previewerViewModel';
import { PreviewFileDialogOpts, PreviewFileDialogViewModel } from 'tessa/ui/tessaDialog';
import { IValidationResultBuilder } from 'tessa/platform/validation';
import { UIButton } from '../uiButton';
import { FileInfo } from 'tessa/ui/files';
export interface IFileControl extends IControlViewModel {
    readonly model: ICardModel;
    fileContainer: FileContainer;
    readonly files: ReadonlyArray<FileViewModel>;
    readonly filteredFiles: ReadonlyArray<FileViewModel>;
    readonly groupings: ReadonlyArray<FileGrouping>;
    readonly sortings: ReadonlyArray<FileSorting>;
    readonly actions: ReadonlyArray<MenuAction>;
    readonly fileActions: ReadonlyArray<MenuAction>;
    readonly versionActions: ReadonlyArray<MenuAction>;
    readonly groups: ReadonlyMap<string, ReadonlyFileGroupViewModel> | null;
    readonly isFileExists: boolean;
    selectedSorting: FileSorting | null;
    selectedSortDirection: FileSortingDirection;
    selectedGrouping: FileGrouping | null;
    groupsExpanded: boolean;
    selectedFiltering: FileFiltering | null;
    isCategoriesEnabled: boolean;
    isManualCategoriesCreationDisabled: boolean;
    isNullCategoryCreationDisabled: boolean;
    isPreservingCategoriesOrder: boolean;
    isIgnoreExistingCategories: boolean;
    multiSelectionMode: boolean;
    categoryFilter: FileListCategoryFilter | null;
    typeFilter: FileListTypeFilter | null;
    modifyFileSelect: FileListSelect | null;
    validateFileContent: FileListValidateContent | null;
    validateFileCategory: FileListValidateCategory | null;
    groupSorting: FileListGroupSorting | null;
    readonly cryptoProPluginEnabled: boolean;
    getState(): IControlState;
    setState(state: IControlState): boolean;
    handleDropFiles(contents: ReadonlyArray<File>): Promise<void>;
    readonly containerFileAdding: EventHandler<(args: FileControlCancelEventArgs) => void>;
    readonly containerFileAdded: EventHandler<(args: FileControlEventArgs) => void>;
    readonly containerFileRemoving: EventHandler<(args: FileControlCancelEventArgs) => void>;
    readonly containerFileRemoved: EventHandler<(args: FileControlEventArgs) => void>;
    manager: IFileControlManager | null;
    info: IStorage;
    addFile(file: IFile): FileViewModel;
    addFiles(files: ReadonlyArray<IFile>): ReadonlyArray<FileViewModel>;
    removeFile(file: IFile | FileViewModel): any;
    removeFiles(files: ReadonlyArray<IFile | FileViewModel>): void;
    removeFiles(filter: (f: FileViewModel) => boolean): void;
    readonly fileControlButtons: UIButton[];
    readonly visibleFileControlButtons: readonly UIButton[];
}
export interface IFileControlExtension extends IExtension {
    initializing(context: IFileControlExtensionContext): any;
    openingMenu(context: IFileControlExtensionContext): any;
}
export interface IFileExtension extends IExtension {
    openingMenu(context: IFileExtensionContext): any;
}
export interface IFileVersionExtension extends IExtension {
    openingMenu(context: IFileVersionExtensionContext): any;
}
export interface IFileExtensionContextBase {
    readonly control: IFileControl;
    readonly actions: MenuAction[];
    readonly info: IStorage;
}
export interface IFileControlExtensionContext extends IFileExtensionContextBase {
    readonly groupings: FileGrouping[];
    readonly sortings: FileSorting[];
}
export interface IFileExtensionContext extends IFileExtensionContextBase {
    readonly file: FileViewModel;
    readonly files: ReadonlyArray<FileViewModel>;
}
export interface IFileVersionExtensionContext extends IFileExtensionContextBase {
    readonly file: FileViewModel;
    readonly version: IFileVersion;
    readonly versions: ReadonlyArray<IFileVersion>;
    readonly dialog: FileListVersionsDialogViewModel;
}
export declare type HandleFileContentLoadingFuncType = (version: IFileVersion, callback: (result: Result<File>) => Promise<void>, handleParams?: {
    showLoadingMessage?: boolean;
    showErrorMessage?: boolean;
}) => Promise<void>;
/**
 * Предоставляет возможность управления предпросмотром файлов.
 */
export interface IFileControlManager {
    /**
     * Модель представления области предпросмотра.
     */
    readonly previewToolViewModel: IPreviewerViewModel | null;
    /**
     * Свойство отвечает за предпросмотр файла в диалоговом окне.
     */
    previewInDialog: boolean;
    /**
     * Фабрика, на основе которой происходит определение модели представления области предпросмотра.
     * Может быть переопределена.
     * Если тип текущей модели previewToolViewModel совпадает с типом, созданным фабрикой, создание новой модели происходить не будет.
     * @param version Версия файла.
     * @returns Объект, представляющий тип вью-модели и функцию создания вью-модели.
     * Или null, если отображение указанного файла невозможно.
     */
    previewToolFactory: (version: IFileVersion) => {
        type: string;
        createViewModelFunc: () => IPreviewerViewModel;
    } | null;
    /**
     * Отображает указанный файл в предпросмотре.
     */
    showPreview: (file: IFile, previewFileDialogOpts?: PreviewFileDialogOpts) => Promise<PreviewFileDialogViewModel | void>;
    showDefaultPreview: (file: IFile) => void;
    showPreviewInDialog: (file: IFile, previewFileDialogOpts?: PreviewFileDialogOpts) => Promise<PreviewFileDialogViewModel | void>;
    /**
     * Признак того, что предпросмотр через конвертацию в PDF включен.
     */
    readonly previewPdfEnabled: boolean;
    /**
     * Выполняет загрузку указанного файла, предварительно определяя необходимость конвертации в PDF-формат.
     * @param version Версия файла.
     * @param callback Функция обратного вызова, вызываемая при окончании загрузки.
     */
    handleFileContentLoading: HandleFileContentLoadingFuncType;
    reset: () => void;
    /**
     * Сбрасывает текущий файл предпросмотра.
     */
    resetPreview: () => void;
    /**
     * Сбрасывает сообщение и файл предпросмотра, если указанный файл является текущим.
     */
    resetIfInPreview(file: IFile): any;
    /**
     * Сбрасывает сообщение и файл предпросмотра, если указанный идентификатор версии файла является текущим.
     */
    resetIfInPreview(versionId: guid): any;
    /**
     * Устанавливает указанный угол поворота страницы для файла.
     */
    setFilePageAngle(fileVersionId: guid, pageIndex: number, angle: number): any;
    /**
     * Получает угол поворота указанной страницы для указанного файла.
     */
    getFilePageAngle(fileVersionId: guid, pageIndex: number): number | null;
    /**
     * Текстовая информация, выводимая в области предпросмотра.
     * Имеет приоритет над моделью previewToolViewModel, поэтому сообщение будет выводиться всегда, если отлично от null.
     */
    message: {
        main: string;
        additional?: string | null;
    } | null;
    /**
     * Признак того, что в данный момент производится загрузка файла.
     */
    readonly inProgress: boolean;
}
/**
 * Элемент управления для области предпросмотра.
 */
export interface IFilePreviewControl {
    attach: (fileControl: IFileControl) => void;
}
/**
 * Контекст фильтрации категории файлов.
 */
export interface IFileCategoryFilterContext {
    /**
     * Коллекция доступных категорий по умолчанию.
     */
    readonly categories: (FileCategory | null)[];
    /**
     * Коллекция с информацией о файлах, для которых выбирается категория.
     */
    readonly fileInfos: FileInfo[];
    /**
     * Признак того, что при добавлении файла пользователю запрещается вводить имя категории вручную.
     * Изначальное определяется из настроек контрола <see cref="IFileControl.IsManualCategoriesCreationDisabled"/> и может быть пеоеопределено в рамках фильтрации категорий.
     */
    isManualCategoriesCreationDisabled: boolean;
}
/**
 * Контекст модификации способа выбора файла.
 */
export interface IFileSelectContext {
    /**
     * Список доступных расширений файлов, используемый при выборе файла.
     */
    selectFileDialogAccept?: string;
    /**
     * Заменяемый файл, для которого выбирается контент файла, или <c>null</c>, если идёт добавление нового файла.
     */
    readonly replaceFile: IFile | null;
}
/**
 * Контекст фильтрации типов файлов.
 */
export interface IFileTypeFilterContext {
    /**
     * Коллекция доступных типов файлов по умолчанию.
     */
    readonly types: FileType[];
    /**
     * Список с информацией о файлах, для которых выбирается тип файла.
     */
    readonly fileInfos: FileInfo[];
    /**
     * Список категорий, выбранных для файлов.
     */
    readonly categories: (FileCategory | null)[];
}
/**
 * Контекст валидации смены категории файла.
 */
export interface IFileValidateCategoryContext {
    /**
     * Объект с информацией о файле.
     */
    readonly fileInfo: FileInfo;
    /**
     * Билдер результата валидации.
     */
    readonly validationResult: IValidationResultBuilder;
    /**
     * Выбранная категория файла.
     */
    readonly category: FileCategory | null;
}
/**
 * Контекст валидации контента файла.
 */
export interface IFileValidateContentContext {
    /**
     * Объект с информацией о файле.
     */
    readonly fileInfo: FileInfo;
    /**
     * Билдер результата валидации.
     */
    readonly validationResult: IValidationResultBuilder;
}
export declare enum ScaleOption {
    auto = 0,
    custom = 2,
    _50 = 50,
    _100 = 100,
    _200 = 200,
    _400 = 400,
    _800 = 800
}
