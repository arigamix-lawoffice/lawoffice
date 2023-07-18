import * as React from 'react';
import { observer } from 'mobx-react';
import { SliderViewModel } from './29_sliderViewModel';
import { getVisibilityStyle } from 'tessa/ui';
import { Visibility } from 'tessa/platform';
import { LocalizationManager } from 'tessa/localization';
import {
  ControlProps,
  ControlCaption,
  createStyledControl
} from 'tessa/ui/cards/components/controls';

@observer
export class SliderControl extends React.Component<ControlProps<SliderViewModel>> {
  private readonly _inputRef = React.createRef<HTMLInputElement>();

  //#region react

  public componentDidMount(): void {
    this.props.viewModel.bindReactComponentRef(this._inputRef);
  }

  public componentWillUnmount(): void {
    this.props.viewModel.unbindReactComponentRef();
  }

  public componentDidUpdate(prevProps: ControlProps<SliderViewModel>): void {
    if (prevProps.viewModel !== this.props.viewModel) {
      this.props.viewModel.bindReactComponentRef(this._inputRef);
    }
  }

  public render(): JSX.Element | null {
    const { viewModel } = this.props;

    const controlVisibility = viewModel.controlVisibility;
    if (controlVisibility === Visibility.Collapsed) {
      return null;
    }

    let actualCaptionVisibility = viewModel.captionVisibility;
    if (controlVisibility === Visibility.Hidden) {
      actualCaptionVisibility = Visibility.Hidden;
    }

    const caption = LocalizationManager.instance.localize(viewModel.caption);

    return (
      <>
        <ControlCaption
          mediaStyle={viewModel.captionStyle}
          style={getVisibilityStyle({}, actualCaptionVisibility)}
        >
          {caption}
        </ControlCaption>
        <div style={getVisibilityStyle({}, controlVisibility)}>
          <StyledInput
            ref={this._inputRef}
            type="range"
            step={viewModel.step}
            min={viewModel.minValue}
            max={viewModel.maxValue}
            value={viewModel.value}
            mediaStyle={viewModel.controlStyle}
            onChange={this.handleChange}
            onFocus={this.handleFocus}
            onBlur={this.handleBlur}
            title={viewModel.error || viewModel.tooltip}
          />
        </div>
      </>
    );
  }

  //#endregion

  //#region methods

  public focus(opt?: FocusOptions): void {
    if (this._inputRef.current) {
      this._inputRef.current.focus(opt);
    }
  }

  //#endregion

  //#region handlers

  private handleChange = (e: React.SyntheticEvent<HTMLInputElement>) => {
    const { viewModel } = this.props;

    const target = e.target as HTMLInputElement;
    const value = target.value;

    viewModel.value = Number.parseInt(value);
  };

  // получаем фокус
  private handleFocus = () => {
    const { viewModel } = this.props;
    viewModel.isFocused = true;
  };

  // теряем фокус
  private handleBlur = () => {
    const { viewModel } = this.props;
    viewModel.isFocused = false;
  };

  //#endregion
}

export const StyledInput = createStyledControl<React.AllHTMLAttributes<HTMLInputElement>>('input');
