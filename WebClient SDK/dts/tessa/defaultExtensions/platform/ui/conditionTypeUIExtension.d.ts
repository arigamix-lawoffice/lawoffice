import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class ConditionTypeUIExtension extends CardUIExtension {
    private _dispose;
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
    private updateCanCompile;
    private updateCanRepair;
    private repairConditions;
}
