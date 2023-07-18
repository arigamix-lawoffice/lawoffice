import { Card } from './card';
export declare class CardSingletonCache {
    private constructor();
    private static _instance;
    static get instance(): CardSingletonCache;
    readonly cards: ReadonlyMap<string, Card>;
    readonly settings: Map<string, any>;
    readonly existentSingletons: Set<guid>;
    initialize(singletons: any): void;
    invalidate(): void;
}
