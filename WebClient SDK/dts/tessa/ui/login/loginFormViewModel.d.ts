/// <reference types="react" />
import type { Location } from 'history';
import { EventHandler, Visibility } from 'tessa/platform';
import { ValidationResult } from 'tessa/platform/validation';
import { UIButton } from 'tessa/ui/uiButton';
import { TextFieldProps } from 'ui/textField/textField';
interface SimpleTextField extends TextFieldProps {
    visibility: Visibility;
}
export interface ILoginFormViewModel {
    locos: {
        [key: string]: string;
    };
    redirectTo: string;
    autoLogin: boolean;
    loginField: SimpleTextField;
    passwordField: SimpleTextField;
    readonly buttons: UIButton[];
    message: string | ValidationResult | null;
    logo: React.DetailedHTMLProps<React.ImgHTMLAttributes<HTMLImageElement>, HTMLImageElement>;
    onKeyDown: EventHandler<(args: {
        event: React.KeyboardEvent;
    }) => void>;
    initialize(location: Location): Promise<void>;
    dispose(): Promise<void>;
    autoLoginIfNeeded(): any;
    localize(alias: string, defaultStr?: string): string;
}
export declare class LoginFormViewModel implements ILoginFormViewModel {
    constructor();
    protected _winAuthPath: string | null;
    protected _hasSaml: boolean;
    protected _message: string | ValidationResult | null;
    locos: {
        [key: string]: string;
    };
    redirectTo: string;
    autoLogin: boolean;
    loginField: SimpleTextField;
    passwordField: SimpleTextField;
    readonly buttons: UIButton[];
    get message(): string | ValidationResult | null;
    set message(value: string | ValidationResult | null);
    logo: React.DetailedHTMLProps<React.ImgHTMLAttributes<HTMLImageElement>, HTMLImageElement>;
    readonly onKeyDown: EventHandler<(args: {
        event: React.KeyboardEvent;
    }) => void>;
    initialize(location: Location): Promise<void>;
    dispose(): Promise<void>;
    autoLoginIfNeeded(): void;
    localize(alias: string, defaultStr?: string): string;
    protected onLoginFieldChanged(e: React.SyntheticEvent<HTMLInputElement>): void;
    protected onPasswordFieldChanged(e: React.SyntheticEvent<HTMLInputElement>): void;
    protected onLogin(): void;
    protected onWinLogin(): void;
    protected onSAMLLogin(): void;
    protected defaultKeyDownHandler(e: {
        event: React.KeyboardEvent;
    }): void;
}
export {};
