import * as React from 'react';
import classNames from 'classnames';
import Moment from 'moment';
import TextField from 'ui/textField';
import { CardHelpMode } from 'tessa/ui/cards';
import { ControlCaption, ControlProps, StyledTextField } from 'tessa/ui/cards/components/controls';
import { getVisibilityStyle } from 'tessa/ui/uiHelper';
import { HelpSection } from 'tessa/ui/cards/components/controls/helpSection';
import { localize } from 'tessa/localization';
import { MediaStyle } from 'ui/mediaStyle';
import { observer } from 'mobx-react';
import { OcrDateTimeViewModel } from './ocrDateTimeViewModel';
import { OcrInputCalendar } from './ocrInputCalendar';
import { OcrInputViewModel } from './ocrInputViewModel';
import { OcrReferenceViewModel } from './ocrReferenceViewModel';
import { OcrTextBoxViewModel } from './ocrTextBoxViewModel';
import { OcrValidationContainer } from '../validation/ocrValidationContainer';
import { TextBoxMode } from 'ui';
import { ToolbarWithButtons } from 'ui/toolbar/toolbarWithButtons';
import { UIButton } from 'tessa/ui';
import { ValidationResult } from 'tessa/platform/validation';
import { Visibility } from 'tessa/platform/visibility';
import {
  withTextChanged,
  WithTextChangedProps
} from 'tessa/ui/cards/components/controls/withTextChanged';

/** Пропсы для контрола редактируемого поля. */
interface OcrInputProps extends ControlProps<OcrInputViewModel> {}

@observer
/** Контрол для редактируемого поля. */
export class OcrInput extends React.Component<OcrInputProps> {
  //#region fields

  private _mainRef: React.RefObject<OcrInputInternal> = React.createRef();

  //#endregion

  //#region react

  public componentDidMount(): void {
    this.props.viewModel.bindReactComponentRef(this._mainRef);
  }

  public componentWillUnmount(): void {
    this.props.viewModel.unbindReactComponentRef();
  }

  public componentDidUpdate(prevProps: OcrInputProps): void {
    if (prevProps.viewModel !== this.props.viewModel) {
      this.props.viewModel.bindReactComponentRef(this._mainRef);
    }
  }

  public render(): JSX.Element {
    const { viewModel } = this.props;
    const error = viewModel.error;
    const validationResult = viewModel.validationResult;

    return (
      <OcrInputInternalEnhanced
        ref={this._mainRef}
        text={viewModel.text}
        caption={viewModel.caption}
        captionStyle={viewModel.captionStyle}
        captionVisibility={viewModel.captionVisibility}
        controlStyle={viewModel.controlStyle}
        controlVisibility={viewModel.controlVisibility}
        helpMode={viewModel.helpMode}
        helpValue={viewModel.helpValue}
        isReadOnly={viewModel.isReadOnly}
        isRequired={viewModel.isRequired}
        isInvalid={validationResult?.hasErrors || !!error}
        toolTip={validationResult?.toString() || error || viewModel.tooltip}
        buttons={viewModel.buttonsContainer.getButtons()}
        onBlur={this.handleBlur}
        onChange={this.handleChange}
        onFocus={this.handleFocus}
        onKeyDown={this.handleKeyDown}
        // onTextInternalValidation={handleInternalValidation}
        validationResult={validationResult}
        textBoxViewModel={viewModel.textBoxViewModel}
        dateTimeViewModel={viewModel.dateTimeViewModel}
        referenceViewModel={viewModel.referenceViewModel}
      />
    );
  }

  //#endregion

  //#region methods

  public focus(opt?: FocusOptions): void {
    if (this._mainRef.current) {
      this._mainRef.current.focus(opt);
    }
  }

  //#endregion

  //#region handlers

  private handleChange = (value: string | null): void => {
    const { viewModel } = this.props;

    value ??= '';

    if (viewModel.isReadOnly || viewModel.text === value) {
      return;
    }

    try {
      viewModel.text = value;
    } catch (error) {
      console.error(error);
    }
  };

  private handleFocus = () => {
    this.props.viewModel.isFocused = true;
  };

  private handleBlur = () => {
    this.props.viewModel.isFocused = false;
  };

  private handleKeyDown = (event: React.KeyboardEvent) => {
    const { viewModel } = this.props;

    if (viewModel.isReadOnly) {
      return;
    }

    try {
      viewModel.keyDown.invoke({ control: viewModel, event });
    } catch (error) {
      console.error(error);
    }
  };

  // private handleInternalValidation = (value: string): string | null => {
  //   const { viewModel } = this.props;
  //   return viewModel.getInternalValidation(value);
  // };

  //#endregion
}

interface OcrInputInternalProps extends WithTextChangedProps {
  caption: string;
  captionStyle: MediaStyle | null;
  captionVisibility: Visibility;
  controlStyle: MediaStyle | null;
  controlVisibility: Visibility;
  helpMode: CardHelpMode;
  helpValue: string;
  isReadOnly: boolean;
  isRequired: boolean;
  toolTip: string;
  buttons?: UIButton[];
  onBlur: () => void;
  onChange: (text: string | null) => void;
  onFocus: () => void;
  onKeyDown: (e: React.KeyboardEvent) => void;
  validationResult: ValidationResult | null;
  textBoxViewModel: OcrTextBoxViewModel | null;
  dateTimeViewModel: OcrDateTimeViewModel | null;
  referenceViewModel: OcrReferenceViewModel | null;
}

interface OcrInputInternalState {
  isCalendarOpened: boolean;
}

class OcrInputInternal extends React.Component<OcrInputInternalProps, OcrInputInternalState> {
  //#region fields

  private _inputRef: React.RefObject<TextField>;
  private _calendarButton: UIButton | null | undefined;

  //#endregion

  //#region constructors

  constructor(props: OcrInputInternalProps) {
    super(props);
    this.state = { isCalendarOpened: false };
    this._inputRef = React.createRef();

    if (this.props.dateTimeViewModel) {
      this._calendarButton = this.props.buttons?.find(button => button.name === 'CalendarButton');
    }
  }

  //#endregion

  //#region react

  public render() {
    const { buttons, controlVisibility, toolTip } = this.props;
    const { textBoxViewModel, dateTimeViewModel, referenceViewModel } = this.props;
    const { textBoxMode, minRows, maxRows, spellcheck } = textBoxViewModel || {};

    if (controlVisibility === Visibility.Collapsed) {
      return null;
    }

    let captionVisibility = this.props.captionVisibility;
    if (controlVisibility === Visibility.Hidden) {
      captionVisibility = Visibility.Hidden;
    }

    return (
      <>
        <ControlCaption
          mediaStyle={this.props.captionStyle}
          style={getVisibilityStyle({}, captionVisibility)}
        >
          {localize(this.props.caption)}
          <span className="req-star">{this.props.isRequired ? ' * ' : null}</span>
          <HelpSection
            helpMode={this.props.helpMode}
            helpValue={this.props.helpValue}
            tooltip={toolTip}
          />
        </ControlCaption>
        <div className="control-wrapper" style={getVisibilityStyle({}, controlVisibility)}>
          <OcrValidationContainer
            isLoadingEnabled={referenceViewModel?.isDataLoading}
            validationResult={this.props.validationResult}
          >
            <StyledTextField
              ref={this._inputRef}
              avalonFontType={this.props.textBoxViewModel?.avalonFontType}
              avalonShowLineNumbers={this.props.textBoxViewModel?.avalonShowLineNumbers}
              avalonSyntaxType={this.props.textBoxViewModel?.avalonSyntaxType}
              className={classNames({ invalid: this.props.isInvalid, last: !buttons?.length })}
              disabled={this.props.isReadOnly}
              mediaStyle={this.props.controlStyle}
              multiLine={(maxRows ?? 1) > 1}
              onBlur={this.handleBlur}
              onChange={this.handleChange}
              onFocus={this.handleFocus}
              onKeyDown={this.props.onKeyDown}
              // onValidate={this.handleValidate}
              rows={minRows}
              rowsMax={maxRows}
              spellCheck={spellcheck}
              textBoxMode={textBoxMode}
              title={toolTip}
              type={textBoxMode === TextBoxMode.Password ? 'password' : 'text'}
              value={this.props.text}
            />
            {buttons && <ToolbarWithButtons buttons={buttons} />}
            {dateTimeViewModel?.isDateFormat && (
              <OcrInputCalendar
                rootRef={() =>
                  this._calendarButton?.tryGetReactComponentRef()?.current ??
                  this._inputRef?.current
                }
                isCalendarOpened={this.state.isCalendarOpened}
                viewModel={dateTimeViewModel}
                onCalendarButtonClick={this.handleCalendarButtonClick}
                onDaySelect={this.handleDaySelect}
              />
            )}
          </OcrValidationContainer>
        </div>
      </>
    );
  }

  //#endregion

  //#region methods

  public focus(opt?: FocusOptions) {
    if (this._inputRef.current) {
      this._inputRef.current.focus(opt);
    }
  }

  public selectAll() {
    if (this._inputRef.current) {
      this._inputRef.current.select();
    }
  }

  //#endregion

  //#region handlers

  private handleFocus = () => {
    this.props.onFocus();
  };

  private handleBlur = () => {
    this.props.onBlur();
    this.props.onChange(this.props.text);
  };

  private handleCalendarButtonClick = (): void => {
    this.setState({ isCalendarOpened: !this.state.isCalendarOpened });
  };

  private handleChange = (e: React.SyntheticEvent<HTMLInputElement>) => {
    if (!this.props.isReadOnly) {
      this.props.onTextChanged(e.currentTarget.value);
    }
  };

  private handleDaySelect = (date: Moment.Moment | null, formattedDate: string | null) => {
    this.props.dateTimeViewModel!.selectedDate = date;
    this.props.onChange(formattedDate);
    this._inputRef.current?.focus();
  };

  //#endregion
}

const OcrInputInternalEnhanced = withTextChanged(OcrInputInternal);
