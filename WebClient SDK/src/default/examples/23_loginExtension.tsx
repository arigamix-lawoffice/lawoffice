import React from 'react';
import { Observer } from 'mobx-react';
import { observable, runInAction } from 'mobx';
import type { Location } from 'history';
import { showMessage, UIButton } from 'tessa/ui';
import {
  ILoginExtensionContext,
  LoginExtension,
  LoginFormViewModel,
  LoginForm,
  LoginLogo,
  LoginFields,
  LoginMessage,
  LoginButtons
} from 'tessa/ui/login';
import { TextField } from 'ui';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';
import { captchaExampleImg, captchaExampleValue } from './captchaExample';

/**
 * Позволяет добавлять дополнительные параметры и элементы управления в форму логинизации.
 *
 * Результат работы расширения:
 * В форму логинизации добавлен параметр безопасности в виде капчи и тестовая кнопка, при нажатии на которую
 * появляется сообщение в диалоговом окне.
 */
export class ExampleLoginExtension extends LoginExtension {
  initializing(context: ILoginExtensionContext): void {
    context.loginFormViewModelFactory = () => new ExampleLoginFormViewModel();
    context.loginFormFactory = function LoginFormFactory(viewModel) {
      return <ExampleLoginForm viewModel={viewModel as ExampleLoginFormViewModel} />;
    };
  }

  initialized(context: ILoginExtensionContext): void {
    const { viewModel } = context;
    if (!viewModel) {
      return;
    }

    // добавляем тестовую кнопку в форму логинизации
    viewModel.buttons.push(
      UIButton.create({
        name: 'TestButton',
        caption: 'TestButton',
        icon: 'icon-thin-025',
        buttonAction: () => {
          showMessage('TestButton click!', 'Test', { OKButtonText: 'OK!' });
        }
      })
    );
  }
}

class ExampleLoginFormViewModel extends LoginFormViewModel {
  //#region props

  @observable.ref
  captchaValue = '';

  @observable.ref
  captchaImg = '';

  captchaValidator: (value: string) => boolean;

  onCaptchaFieldChanged = (e: React.SyntheticEvent<HTMLInputElement>) => {
    runInAction(() => (this.captchaValue = e.currentTarget.value));
  };

  //#endregion

  //#region methods

  private initializeCaptcha() {
    runInAction(() => {
      this.captchaImg = captchaExampleImg;
    });
    this.captchaValidator = (value: string) =>
      !!value && value.toLowerCase() === captchaExampleValue;
  }

  private captchaLoginCheck(): boolean {
    return runInAction(() => {
      this.message = null;
      if (!this.captchaValidator(this.captchaValue)) {
        this.message = ValidationResult.fromText(
          'Captcha value is wrong.',
          ValidationResultType.Error
        );
        this.captchaValue = '';
        return false;
      }

      return true;
    });
  }

  //#endregion

  //#region overrides

  async initialize(location: Location) {
    await super.initialize(location);
    this.initializeCaptcha();
  }

  protected onLogin() {
    if (this.captchaLoginCheck()) {
      super.onLogin();
    }
  }

  protected onWinLogin() {
    if (this.captchaLoginCheck()) {
      super.onWinLogin();
    }
  }

  protected onSAMLLogin() {
    if (this.captchaLoginCheck()) {
      super.onSAMLLogin();
    }
  }

  //#endregion
}

interface ExampleLoginFormProps {
  viewModel: ExampleLoginFormViewModel;
}

const ExampleLoginForm = (props: ExampleLoginFormProps) => {
  const { viewModel } = props;
  return (
    <LoginForm viewModel={viewModel}>
      <LoginLogo viewModel={viewModel} />
      <LoginFields viewModel={viewModel} />
      <Observer>
        {() => {
          return (
            <>
              <div style={{ marginBottom: '10px' }}>
                <img src={viewModel.captchaImg} />
              </div>
              <TextField
                type={'text'}
                placeholder={'captcha'}
                value={viewModel.captchaValue}
                onChange={viewModel.onCaptchaFieldChanged}
              />
            </>
          );
        }}
      </Observer>
      <LoginMessage viewModel={viewModel} />
      <LoginButtons viewModel={viewModel} />
    </LoginForm>
  );
};
