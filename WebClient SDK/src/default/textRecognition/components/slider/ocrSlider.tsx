import * as React from 'react';
import {
  ControlCaption,
  ControlProps,
  createStyledControl
} from 'tessa/ui/cards/components/controls';
import { getVisibilityStyle } from 'tessa/ui';
import { HelpSection } from 'tessa/ui/cards/components/controls/helpSection';
import { localize } from 'tessa/localization';
import { observer } from 'mobx-react';
import { OcrSliderViewModel as OcrSliderViewModel } from './ocrSliderViewModel';
import { Visibility } from 'tessa/platform';
import './ocrSliderStyle.css';

/** Контрол ползунка для ввода вещественных значений. */
@observer
export class OcrSlider extends React.Component<ControlProps<OcrSliderViewModel>> {
  //#region fields

  private readonly _inputRef = React.createRef<HTMLInputElement>();

  //#endregion

  //#region react

  public componentDidMount(): void {
    this.props.viewModel.bindReactComponentRef(this._inputRef);
  }

  public componentWillUnmount(): void {
    this.props.viewModel.unbindReactComponentRef();
  }

  public componentDidUpdate(prevProps: ControlProps<OcrSliderViewModel>): void {
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

    const localizedCaption = localize(viewModel.caption);

    return (
      <>
        <ControlCaption
          mediaStyle={viewModel.captionStyle}
          style={getVisibilityStyle({}, actualCaptionVisibility)}
        >
          {`${localizedCaption} (${viewModel.value})`}
          <HelpSection
            helpMode={viewModel.helpMode}
            helpValue={viewModel.helpValue}
            tooltip={viewModel.tooltip}
          />
        </ControlCaption>
        <div className="range" style={getVisibilityStyle({}, controlVisibility)}>
          <span className="value min">{viewModel.minValue}</span>
          <StyledInput
            ref={this._inputRef}
            type="range"
            style={{ width: '100%' }}
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
          <span className="value max">{viewModel.maxValue}</span>
        </div>
      </>
    );
  }

  //#endregion

  //#region methods

  /**
   * Выполняет установку фокуса на элементе.
   * @param options Параметры установки фокуса.
   */
  public focus(options?: FocusOptions): void {
    if (this._inputRef.current) {
      this._inputRef.current.focus(options);
    }
  }

  //#endregion

  //#region handlers

  private handleChange = (e: React.SyntheticEvent<HTMLInputElement>) => {
    const { viewModel } = this.props;

    const target = e.target as HTMLInputElement;
    const value = target.value;

    viewModel.value = Number.parseFloat(value);
  };

  private handleFocus = () => {
    const { viewModel } = this.props;
    viewModel.isFocused = true;
  };

  private handleBlur = () => {
    const { viewModel } = this.props;
    viewModel.isFocused = false;
  };

  //#endregion
}

export const StyledInput = createStyledControl<React.AllHTMLAttributes<HTMLInputElement>>('input');
