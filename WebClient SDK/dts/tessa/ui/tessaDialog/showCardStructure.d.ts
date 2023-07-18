import * as React from 'react';
import { Card } from 'tessa/cards';
export interface CardStructureProps {
    card: Card;
    removeAllButChanged: boolean;
    canRemoveAllButChanged: boolean;
    onClose: () => void;
    onSaveToFile: () => void;
}
export interface CardStructureState {
    beforeSave: boolean;
}
export declare class CardStructure extends React.Component<CardStructureProps, CardStructureState> {
    constructor(props: CardStructureProps);
    private _checkBoxRef;
    componentDidMount(): void;
    render(): JSX.Element;
    private printCard;
    private printObject;
    private printArray;
    private printTypedValue;
    private printProp;
    private getIndent;
    private getSortPropsFunc;
    private handleBeforeSaveToggle;
}
export declare function showCardStructure(card: Card, removeAllButChanged?: boolean, canRemoveAllButChanged?: boolean, digest?: string): Promise<boolean>;
