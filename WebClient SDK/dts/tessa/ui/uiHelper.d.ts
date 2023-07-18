/// <reference types="react" />
import { CardModelFlags } from './cards';
import { IBlockViewModel, ICardEditorModel, ICardModel, IControlViewModel, IFormWithBlocksViewModel, IFormViewModelBase } from './cards/interfaces';
import { IUIContext } from './uiContext';
import { Card, CardRow } from 'tessa/cards';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { CardTypeBlock, CardTypeControl, CardTypeForm, CardTypeTableControl } from 'tessa/cards/types';
import { Visibility } from 'tessa/platform/visibility';
import { FileContainer, FileContainerPermissions, IFileSource } from 'tessa/files';
import { IStorage } from 'tessa/platform/storage';
import { CardNewResponse } from 'tessa/cards/service';
import { FormCreationOptions } from 'tessa/ui/formCreationOptions';
export declare function createCardEditorModel(): ICardEditorModel;
export declare type CreateModelFunc = (card: Card, sectionRows: Map<string, CardRow>) => ICardModel;
export declare function createCardModel(card: Card, sectionRows: Map<string, CardRow>): ICardModel;
export declare function createCardModelWithMetadata(card: Card, sectionRows: Map<string, CardRow>, cardMetadata: CardMetadataSealed): ICardModel;
export declare type CreateFormFunc = (cardModel: ICardModel) => IFormViewModelBase;
export declare function createCardForm(cardModel: ICardModel): IFormViewModelBase;
export declare type CreateFileSourceFunc = (cardModel: ICardModel, card?: Card | null) => IFileSource;
export declare function createCardFileSource(cardModel: ICardModel, card?: Card | null): IFileSource;
export declare type CreateCardFileSourceForCardFunc = (card: Card) => IFileSource;
export declare function createCardFileSourceForCard(card: Card, executeInContextFunc?: ((action: (context: IUIContext) => void) => void) | null): IFileSource;
export declare type CreateFileContainerFunc = (fileSource: IFileSource, permissions: FileContainerPermissions | null) => FileContainer;
export declare function createCardFileContainer(fileSource: IFileSource, permissions?: FileContainerPermissions | null): FileContainer;
export declare function createFormViewModel(form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel): IFormWithBlocksViewModel;
export declare function createBlockViewModel(block: CardTypeBlock, form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel): IBlockViewModel;
export declare function createControlViewModel(control: CardTypeControl, block: CardTypeBlock, form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel): IControlViewModel;
export declare function createDialogForm(dialogName: string, tabAlias?: string, formCreationOptions?: FormCreationOptions, modifyResponseAsync?: (response: CardNewResponse) => Promise<void>, modifyModelAsync?: (cardModel: ICardModel) => Promise<void>): Promise<[IFormViewModelBase, ICardModel] | null>;
/**
 * Возвращает признак того, что форма является формой верхнего уровня карточки,
 * т.е. это не форма дочерних элементов управления.
 * @param form Форма, для которой необходимо определить, является ли она формой
 * верхнего уровня карточки.
 * @returns true если заданная форма является формой верхнего уровня карточки;
 * false в противном случае.
 */
export declare function isTopLevelForm(form: CardTypeForm): boolean;
export declare function putNamedBlockViewModels(container: Map<string, IBlockViewModel>, blocks: Iterable<IBlockViewModel>): void;
export declare function putNamedControlViewModels(container: Map<string, IControlViewModel>, controls: Iterable<IControlViewModel>): void;
export declare function tryGetFromSettings<T>(settings: IStorage | null, key: string, defaultValue?: T): T;
export declare function tryGetFromInfo<T>(info: IStorage | null | undefined, key: string, defaultValue?: T): T;
export declare function tryGetFieldsFromInfo<T>(info: IStorage | null | undefined, key: string, defaultValue?: T[], fieldConverter?: (value: unknown) => T): T[];
export interface ColorSettingsObj {
    r: number;
    g: number;
    b: number;
    a: number;
}
export declare function getColorFromSettingsAsObject(color: string): ColorSettingsObj;
export declare function getColorFromSettings(color: string, changeColor?: (color: ColorSettingsObj) => ColorSettingsObj): string;
export declare function getGradientColorFromSettings(startPoint: string, endPoint: string, gradientStops: Array<[string, number]>, changeColor?: (color: ColorSettingsObj) => ColorSettingsObj): string;
export declare function getVisibilityStyle(style: React.CSSProperties, visibility: Visibility, displayValue?: string): any;
export declare function tryGetOrderColumnId(settings: IStorage | null, tableControl: CardTypeTableControl, model: ICardModel): guid | null;
export declare const specialCardModelFlags: CardModelFlags;
export declare const editTemplateCardModelFlags: CardModelFlags;
export declare function cardInSpecialMode(cardModel: ICardModel): boolean;
export declare function modifyTemplateCardEditor(cardEditor: ICardEditorModel): {
    name: string;
    info?: string;
} | undefined;
export declare function createClickWrapper(delay?: number, ctrlAndShiftBlock?: boolean, oneAction?: boolean): (onClick: ((e: React.MouseEvent) => void) | null, onDoubleClick: (e: React.MouseEvent) => void) => (e: React.MouseEvent) => void;
export declare function createClickWrapper(onClick: ((e: React.MouseEvent) => void) | null, onDoubleClick: (e: React.MouseEvent) => void, delay?: number, ctrlAndShiftBlock?: boolean, oneAction?: boolean): (e: React.MouseEvent) => void;
export declare function createMouseDownWrapper(onMouseDown: (e: React.MouseEvent) => void, onDoubleMouseDown: (e: React.MouseEvent) => void, delay?: number): (e: React.MouseEvent) => void;
export declare function getTaskState(roleName: string | null, userName: string | null, optionId: string | null): string;
export declare function getTaskStateDate(created: string | null, inProgress: string | null, completed: string | null): string | null;
export declare const getCSSProps: (value: string, appearances: string[]) => React.CSSProperties;
export declare const hexToRgbaObject: (hex: string) => {
    r: number;
    g: number;
    b: number;
    a?: number;
};
export declare const getRgbaFromDecimal: (num: number | undefined) => string;
export declare const getBasedRgbaFromDecimal: (num: number, lightColor?: string, darkColor?: string) => string;
export declare const getDecimalFromRbga: (rgba?: string | undefined) => number | undefined;
export declare function rgbaToString(rgba: {
    r: number;
    g: number;
    b: number;
    a?: number;
}): string;
export declare function rgbaToHex(c: {
    r: number;
    g: number;
    b: number;
    a?: number;
}): string;
export declare function isColorCloseToWhite(rgba?: {
    r: number;
    g: number;
    b: number;
    a?: number;
}): boolean;
