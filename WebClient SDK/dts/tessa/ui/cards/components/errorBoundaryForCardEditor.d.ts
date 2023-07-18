import React, { ErrorInfo } from 'react';
import { UIErrorPresenterViewModel } from '../uiErrorPresenterViewModel';
interface IErrorBoundaryForCardEditorProps {
    notifyUIErrorOccurred: (error: Error, errorInfo: ErrorInfo, close: () => void) => UIErrorPresenterViewModel | undefined;
}
interface IErrorBoundaryForCardEditorState {
    vm: UIErrorPresenterViewModel | null;
}
export declare class ErrorBoundaryForCardEditor extends React.Component<IErrorBoundaryForCardEditorProps, IErrorBoundaryForCardEditorState> {
    constructor(props: IErrorBoundaryForCardEditorProps);
    componentDidCatch(error: Error, errorInfo: ErrorInfo): void;
    close: () => void;
    renderButtons(): JSX.Element[] | undefined;
    render(): React.ReactNode;
}
export {};
