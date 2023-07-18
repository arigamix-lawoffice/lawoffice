import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
export declare class OpenForumContextMenuViewExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    initialize(model: IWorkplaceViewComponent): void;
    private getParticipantsMenuAction;
    private static changeParticipantsCommand;
    private static removeParticipantsCommand;
}
