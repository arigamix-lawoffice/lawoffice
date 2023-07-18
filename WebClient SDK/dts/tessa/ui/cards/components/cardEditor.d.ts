import * as React from 'react';
import { ICardEditorModel } from 'tessa/ui/cards/interfaces';
export interface CardEditorProps {
    cardEditorModel: ICardEditorModel;
    isHidden: boolean;
}
export declare class CardEditor extends React.Component<CardEditorProps> {
    constructor(props: CardEditorProps);
    private _wasSuccessRender;
    shouldComponentUpdate(nextProps: CardEditorProps): boolean;
    render(): JSX.Element | null;
}
export interface CardEditorInternalProps {
    cardEditorModel: ICardEditorModel;
}
