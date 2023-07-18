import { ViewControlInitializationContext, ViewControlInitializationStrategy } from 'tessa/ui/cards/controls';
export declare class CardTableViewInitializationStrategy extends ViewControlInitializationStrategy {
    initializeMetadata(context: ViewControlInitializationContext): void;
    initializeDataProvider(_context: ViewControlInitializationContext): void;
    initializeSorting(context: ViewControlInitializationContext): void;
    initializeContent(context: ViewControlInitializationContext): void;
    initializePaging(context: ViewControlInitializationContext): void;
    initializeTable(context: ViewControlInitializationContext): void;
}
