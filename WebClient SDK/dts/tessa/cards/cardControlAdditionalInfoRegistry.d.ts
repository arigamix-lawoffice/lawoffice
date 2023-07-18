import { CardControlType } from './cardControlType';
import { CardControlSourceInfo } from './cardControlSourceInfo';
import { CardTypeControl } from './types';
export interface CardControlSourceInfoDelegate {
    (control: CardTypeControl): CardControlSourceInfo;
}
export declare class CardControlAdditionalInfoRegistry {
    private constructor();
    private static _instance;
    static get instance(): CardControlAdditionalInfoRegistry;
    private _customSourceFuncs;
    registerSourceInfoFunc(controlType: CardControlType, func: CardControlSourceInfoDelegate): CardControlAdditionalInfoRegistry;
    tryGetSourceInfoFunc(controlType: CardControlType): CardControlSourceInfoDelegate | null;
}
