import { ElementSide, InteractionEventTypes } from './common';
export declare class SidePanelManager {
    private constructor();
    private static _instance;
    static get instance(): SidePanelManager;
    private _openedPanel;
    private _interactionType;
    get openedPanel(): ElementSide | null;
    get isLeftPanelOpened(): boolean;
    get isRigthPanelOpened(): boolean;
    get interactionType(): InteractionEventTypes | null;
    openPanel(side: ElementSide, type: InteractionEventTypes): void;
    closePanel(): void;
}
