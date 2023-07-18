import { observer } from 'mobx-react';
import React from 'react';
import { ExamplePreviewerViewModel } from './30_examplePreviewerViewModel';

export type ExamplePreviewerComponentProps = {
  viewModel: ExamplePreviewerViewModel;
};

@observer
export class ExamplePreviewerComponent extends React.PureComponent<ExamplePreviewerComponentProps> {
  public render(): JSX.Element {
    const { viewModel } = this.props;

    return (
      <div
        style={{
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
          alignItems: 'center'
        }}
      >
        <h3>Example Previewer</h3>
        {viewModel.isLoading && <h4>Text is loading...</h4>}
        <span>{viewModel.text}</span>
      </div>
    );
  }
}
