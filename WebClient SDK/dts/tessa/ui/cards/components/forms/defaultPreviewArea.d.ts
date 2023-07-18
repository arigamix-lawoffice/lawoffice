import React from 'react';
import { DefaultFormMainViewModel } from 'tessa/ui/cards/forms';
export interface DefaultPreviewAreaProps {
    viewModel: DefaultFormMainViewModel;
    innerRef?: React.RefObject<HTMLElement>;
    innerStyle?: React.CSSProperties;
}
export declare class DefaultPreviewArea extends React.Component<DefaultPreviewAreaProps> {
    render(): JSX.Element | null;
}
