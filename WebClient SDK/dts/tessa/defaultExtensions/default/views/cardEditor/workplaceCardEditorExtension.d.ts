import React from 'react';
import { ApplicationExtension } from 'tessa';
import { IStorage } from 'tessa/platform/storage';
import { ICardEditorModel } from 'tessa/ui/cards';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { BaseContentItem, ContentPlaceArea } from 'tessa/ui/views/content';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
export declare class WorkplaceCardEditorExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    initialize(model: IWorkplaceViewComponent): void;
}
export declare class WorkplaceCardEditorInitializeExtension extends ApplicationExtension {
    initialize(): void;
}
export declare class WorkplaceCardEditorSettings {
    data: IStorage;
    constructor(data: IStorage);
}
export declare class WorkplaceCardEditorViewModel extends BaseContentItem {
    constructor(settings: WorkplaceCardEditorSettings, viewComponent: IWorkplaceViewComponent, area?: ContentPlaceArea, order?: number);
    private _cardEditor;
    readonly settings: WorkplaceCardEditorSettings;
    get cardEditor(): ICardEditorModel;
    get isLoading(): boolean;
    initialize(): void;
    dispose(): void;
    refresh(): Promise<void>;
    save(): Promise<void>;
}
export interface WorkplaceCardEditorProps {
    viewModel: WorkplaceCardEditorViewModel;
}
export declare class WorkplaceCardEditor extends React.Component<WorkplaceCardEditorProps> {
    render(): JSX.Element;
}
