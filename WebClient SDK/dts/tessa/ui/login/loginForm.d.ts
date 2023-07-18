import React from 'react';
import { ILoginFormViewModel } from './loginFormViewModel';
export interface LoginFormProps {
    viewModel: ILoginFormViewModel;
}
export declare class LoginForm extends React.Component<LoginFormProps> {
    componentDidMount(): void;
    render(): JSX.Element;
    protected onKeyDownHandler: (e: React.KeyboardEvent) => void;
}
export interface LoginLogoProps {
    viewModel: ILoginFormViewModel;
}
export declare type LoginLogoRef = HTMLImageElement;
export declare const LoginLogo: React.ForwardRefExoticComponent<LoginLogoProps & React.RefAttributes<HTMLImageElement>>;
export interface LoginFieldsProps {
    viewModel: ILoginFormViewModel;
}
export declare const LoginFields: (props: LoginFieldsProps) => JSX.Element;
export interface LoginMessageProps {
    viewModel: ILoginFormViewModel;
}
export declare type LoginMessageRef = HTMLDivElement;
export declare const LoginMessage: React.ForwardRefExoticComponent<LoginMessageProps & React.RefAttributes<HTMLDivElement>>;
export interface LoginButtonsProps {
    viewModel: ILoginFormViewModel;
}
export declare const LoginButtons: (props: LoginButtonsProps) => JSX.Element;
export interface DefaultLoginFormProps {
    viewModel: ILoginFormViewModel;
}
export declare const DefaultLoginForm: (props: DefaultLoginFormProps) => JSX.Element;
