import { ElementSide } from './common';
export declare const OPENED_STATE = "OPENED_STATE";
export declare const CLOSED_STATE = "CLOSED_STATE";
export declare const CHANGING_STATE = "CHANGING_STATE";
export declare function openPanel(side: ElementSide, panelRef: HTMLDivElement): void;
export declare function closePanel(side: ElementSide, panelRef: HTMLDivElement): void;
export declare function setTranslate(amount: number, side: ElementSide, panelWidth: number, panelRef: HTMLDivElement): void;
export declare function enableAnimation(panelRef: HTMLDivElement): void;
export declare function disableAnimation(panelRef: HTMLDivElement): void;
export interface ISidePanelHolder {
    init: (panelRef: HTMLDivElement) => void;
    enableOrDisableIfNeeded: (isOnClickOnly: boolean, panelRef: HTMLDivElement) => void;
}
