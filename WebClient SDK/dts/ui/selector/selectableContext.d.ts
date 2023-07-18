import React from 'react';
/** Пропсы для контекста выделяемых элементов. */
interface SelectableContextProps extends React.HTMLAttributes<HTMLDivElement> {
    /** Допустимое смещение при начале выделения. */
    offset?: number;
    /** Обработчик события, выполняемый при выделении элементов. */
    onSelection?: (items: unknown[]) => void;
    /** Обработчик события, выполняемый перед началом выделения элементов. */
    onSelectionStart?: () => void;
    /** Обработчик события, выполняемый после окончания выделения элементов. */
    onSelectionEnd?: () => void;
}
/** Контекст выделяемых элементов. */
export declare class SelectableContext extends React.PureComponent<SelectableContextProps> {
    private _registry;
    private _mainRef;
    private _boxRef;
    private _isSelectionActive;
    private _isBoxVisible;
    private _startBoxPosition;
    private _currentBoxPosition;
    private get currentPosition();
    componentWillUnmount(): void;
    private removeListeners;
    private resizeBox;
    private setBoxVisibility;
    private onMouseUp;
    private onMouseMove;
    private onMouseDown;
    private onScroll;
    render(): JSX.Element;
}
export {};
