import { TypeExtensionContext } from 'tessa/cards';
import { IViewControlInitializationStrategy } from 'tessa/ui/cards/controls/viewControl/viewControlInitializationStrategy';
import { ICardModel } from 'tessa/ui/cards/interfaces';
declare type TryGetControlInitializationStrategy = (context: TypeExtensionContext, cardModel: ICardModel) => IViewControlInitializationStrategy | null;
export declare function getFileViewExtensionInitializationStrategyHandlers(cardModel: ICardModel): TryGetControlInitializationStrategy[];
export declare function tryGetFileViewExtensionInitializationStrategyHandlers(cardModel: ICardModel): TryGetControlInitializationStrategy[];
export {};
