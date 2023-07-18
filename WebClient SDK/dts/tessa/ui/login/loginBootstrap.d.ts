import React from 'react';
import { Location } from 'history';
import { ILoginFormViewModel } from './loginFormViewModel';
export interface LoginBootstrapProps {
    location: Location;
}
export interface LoginBootstrapState {
    viewModel: ILoginFormViewModel | null;
    formFactory: ((viewModel: ILoginFormViewModel) => JSX.Element) | null;
}
export declare class LoginBootstrap extends React.Component<LoginBootstrapProps, LoginBootstrapState> {
    constructor(props: LoginBootstrapProps);
    private _extensionExecutor;
    componentDidMount(): Promise<void>;
    componentWillUnmount(): void;
    render(): JSX.Element | null;
}
