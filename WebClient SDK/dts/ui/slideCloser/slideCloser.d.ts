import * as React from 'react';
import { GesturesInstance } from 'common/gestures';
declare class SlideCloser extends React.Component<ISlideCloserProps> {
    isDragging: boolean;
    currentRef: any;
    closeAmountValue: number | null;
    startX: number | null;
    lastX: number | null;
    nativeAnimation: any;
    dragGesture: GesturesInstance | null;
    releaseGesture: GesturesInstance | null;
    static defaultProps: {
        animationTime: number;
        disabled: boolean;
    };
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: ISlideCloserProps): void;
    init(): void;
    release(): void;
    snapToRest(event: any): void;
    getCloseRatio(): number;
    getCloseAmount(): number;
    closePercentage(percentage: any): void;
    closeAmount(amount: any): void;
    enableAnimation(): void;
    disableAnimation(): void;
    reset(): void;
    close(): void;
    handleDrag: (event: any) => void;
    handleEndDrag: (event: any) => void;
    render(): React.FunctionComponentElement<{
        ref: string;
    }>;
}
export interface ISlideCloserProps {
    children?: any;
    onCloseRequest: any;
    animationTime?: number;
    disabled?: boolean;
}
export default SlideCloser;
