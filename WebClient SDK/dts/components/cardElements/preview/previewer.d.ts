import React, { Component } from 'react';
import { IFileControlManager } from 'tessa/ui/files';
export interface PreviewerProps {
    manager: IFileControlManager;
    parentRef?: React.RefObject<HTMLElement> | null;
    hiddenHeaderNameInDialog?: boolean;
}
export interface PreviewerState {
    error: Error | null;
}
/**
 * Помимо основных пропсов может получать Ref, внутри которого выводится
 * предпросмотр файла
 */
declare class Previewer extends Component<PreviewerProps, PreviewerState> {
    constructor(props: PreviewerProps);
    static getDerivedStateFromError(error: Error): Record<string, unknown>;
    componentDidUpdate(_prevProps: Readonly<PreviewerProps>, prevState: Readonly<PreviewerState>): void;
    render(): JSX.Element;
}
export default Previewer;
