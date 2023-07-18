import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class KrSecondaryProcessUIExtension extends CardUIExtension {
    private _model;
    private _pureProcess;
    private _button;
    private _action;
    private _dispose;
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
    private updateVisibility;
    private updateVisibiltyForPureProcessMode;
    private updateCheckRestrictions;
    private getCurrentMode;
    private initializeConditions;
}
