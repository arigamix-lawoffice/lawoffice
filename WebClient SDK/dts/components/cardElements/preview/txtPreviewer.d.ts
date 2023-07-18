import React from 'react';
import { TxtPreviewerViewModel } from 'tessa/ui/cards/controls/previewer/txtPreviewerViewModel';
export interface ITxtPreviewerProps {
    viewModel: TxtPreviewerViewModel;
}
export default class TxtPreviewer extends React.PureComponent<ITxtPreviewerProps> {
    render(): JSX.Element;
}
