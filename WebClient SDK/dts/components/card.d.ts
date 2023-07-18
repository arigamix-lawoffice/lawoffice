import * as React from 'react';
import { ICardEditorModel } from 'tessa/ui/cards';
export interface CardProps {
    cardEditor?: ICardEditorModel;
}
declare class Card extends React.Component<CardProps> {
    render(): JSX.Element | null;
    private defaultRender;
    private specificRender;
}
export default Card;
