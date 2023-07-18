/* eslint-disable @typescript-eslint/no-explicit-any */
import * as React from 'react';
import { observable, computed, action } from 'mobx';
import { observer } from 'mobx-react';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { ButtonViewModel } from 'tessa/ui/cards/controls';
import { Guid } from 'tessa/platform';
import { Dialog, DialogContainer, DialogContent, RaisedButton } from 'ui';
import { LocalizationManager } from 'tessa/localization';
import { showViewModelDialog } from 'tessa/ui';
import { TestCardTypeID } from './common';

/**
 * При клике на контрол кнопки показываем кастомный диалог.
 *
 * Результат работы расширения:
 * При клике на кнопку "Показать диалог" показываем кастомный диалог,
 * содержащий текстовое поле, а также кнопки "ОК" и "Закрыть". При нажатии на кнопку "OK"
 * в консоли отображается содержимое текстового поля.
 */
export class ShowCustomDialogUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext): void {
    // если карточка не для тестов, то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // пытаемся получить контрол "Показать диалог"
    const button = context.model.controls.get('ShowDialogTypeForm') as ButtonViewModel;
    if (!button) {
      return;
    }

    // будем открывать кастомный диалог
    button.onClick = ShowCustomDialogUIExtension.showCustomDialog;
  }

  private static async showCustomDialog() {
    // делаем viewModel для нашего диалога
    const viewModel = new CustomDialogViewModel('Hello World!');
    // вызываем диалоговое окно
    await showViewModelDialog(viewModel, CustomDialog);
    // все изменения сохранены во viewModel
    console.log(viewModel.text);
  }
}

class CustomDialogViewModel {
  constructor(text: string) {
    this._text = text || '';
  }

  @observable
  private _text: string;

  @computed
  public get text(): string {
    return this._text;
  }

  @action.bound
  public changeText(newText: string) {
    this._text = newText || '';
  }
}

interface CustomDialogProps {
  viewModel: CustomDialogViewModel;
  onClose: () => void;
}

@observer
class CustomDialog extends React.Component<CustomDialogProps> {
  //#region fields

  private _inputRef = React.createRef<HTMLInputElement>();

  //#endregion

  //#region react

  public render() {
    const { viewModel } = this.props;

    return (
      <Dialog
        isOpened={true}
        noPortal={true}
        isAutoSize={false}
        onCloseRequest={this.handleCloseForm}
        style={{
          minHeight: 'auto',
          minWidth: '300px',
          borderRadius: '4px',
          outline: 'none',
          padding: '0.5rem',
          border: '1px solid #ccc',
          background: '#fff'
        }}
      >
        <DialogContainer>
          <DialogContent>
            <div>
              <div>
                <input
                  type="text"
                  className="form-control"
                  ref={this._inputRef}
                  defaultValue={viewModel.text}
                />
              </div>
              <div>
                <RaisedButton
                  className="btn card-button"
                  style={{
                    display: 'inline-block',
                    margin: '15px 0px 0px 10px',
                    padding: '3px 15px'
                  }}
                  onClick={this.handleChange}
                >
                  {LocalizationManager.instance.localize('$UI_Common_OK')}
                </RaisedButton>
                <RaisedButton
                  className="btn card-button"
                  style={{
                    display: 'inline-block',
                    margin: '15px 0px 0px 10px',
                    padding: '3px 15px'
                  }}
                  onClick={this.handleCloseForm}
                >
                  {LocalizationManager.instance.localize('$UI_Common_Close')}
                </RaisedButton>
              </div>
            </div>
          </DialogContent>
        </DialogContainer>
      </Dialog>
    );
  }

  //#endregion

  //#region handlers

  private handleChange = () => {
    const { viewModel } = this.props;
    if (this._inputRef && this._inputRef.current) {
      const name = this._inputRef.current.value;
      viewModel.changeText(name.trim());
    }
    this.props.onClose();
  };

  private handleCloseForm = () => {
    this.props.onClose();
  };

  //#endregion
}
