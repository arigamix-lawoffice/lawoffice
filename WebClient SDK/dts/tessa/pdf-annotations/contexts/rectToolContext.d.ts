import React from 'react';
import { COLORS } from '../types';
export declare type State = {
    size: number;
    color: COLORS;
};
declare type ContextType = [State, React.Dispatch<React.SetStateAction<State>>];
declare function useRectTool(): ContextType;
declare function RectToolProvier(props: {
    children: JSX.Element;
}): JSX.Element;
export { RectToolProvier, useRectTool };
