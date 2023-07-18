import React from 'react';
import { IRegistryItem, Registry } from 'tessa/platform';
/** Информация о выделяемом элементе. */
interface ISelectableRegistryItem extends IRegistryItem<number> {
    /** Ссылка на выделяемый элемент. */
    ref: HTMLElement;
}
/** Коллекция элементов, зарегистрированных для возможности выделения. */
export declare class SelectableRegistry extends Registry<ISelectableRegistryItem, number> {
    getSelected: (bounds: DOMRect) => number[];
    pointInsideRectangle(x: number, y: number): boolean;
    private elementsCollide;
}
/** Коллекция элементов, зарегистрированных для возможности выделения. */
export declare const RegistryContext: React.Context<SelectableRegistry | null>;
export {};
