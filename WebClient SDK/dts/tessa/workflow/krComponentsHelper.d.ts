import { KrComponents } from './krComponents';
import { CardSingletonCache, Card } from 'tessa/cards';
import { KrTypesCache } from 'tessa/workflow';
export declare function hasBaseTypesCache(cardTypeId: guid, typesCache: KrTypesCache): boolean;
export declare function hasBaseCardCache(cardTypeId: guid, cardCache: CardSingletonCache): boolean;
export declare function getKrComponentsTypesCache(cardTypeId: guid, typesCache: KrTypesCache): KrComponents;
export declare function getKrComponentsCardCache(cardTypeId: guid, cardCache: CardSingletonCache): KrComponents;
export declare function getKrComponents(cardTypeId: guid, docTypeId: guid | null, typesCache: KrTypesCache): KrComponents;
export declare function getKrComponentsByCard(card: Card, typesCache: KrTypesCache): KrComponents;
