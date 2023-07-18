import * as React from 'react';
export default class DialogContent extends React.Component<IDialogContentProps, {}> {
    render(): JSX.Element | null;
}
export interface IDialogContentProps {
    children?: any;
    className?: string;
    [key: string]: any;
}
