import * as React from 'react';
import { ICardEditorModel } from '../interfaces';
export interface CardWatchdogViewerProps {
    cardEditorModel: ICardEditorModel;
}
export declare class CardWatchdogViewer extends React.Component<CardWatchdogViewerProps> {
    render(): JSX.Element | null;
}
