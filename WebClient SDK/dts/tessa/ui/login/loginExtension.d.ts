/// <reference types="react" />
import { IExtension } from 'tessa/extensions';
import { ILoginFormViewModel } from 'tessa/ui/login/loginFormViewModel';
export interface ILoginExtensionContext {
    loginFormViewModelFactory?: () => ILoginFormViewModel;
    loginFormFactory?: (viewModel: ILoginFormViewModel) => JSX.Element;
    viewModel?: ILoginFormViewModel;
}
export interface ILoginExtension extends IExtension<ILoginExtensionContext> {
}
export declare class LoginExtension implements ILoginExtension {
    static readonly type = "LoginExtension";
    shouldExecute(_context: ILoginExtensionContext): boolean;
    initializing(_context: ILoginExtensionContext): void;
    initialized(_context: ILoginExtensionContext): void;
    finalized(_context: ILoginExtensionContext): void;
}
