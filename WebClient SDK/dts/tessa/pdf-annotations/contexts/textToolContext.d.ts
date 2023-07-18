import React from 'react';
import { COLORS, TextSizes } from '../types';
export declare type State = {
    size: typeof TextSizes[number];
    color: COLORS;
};
declare type ContextType = [State, React.Dispatch<React.SetStateAction<State>>];
declare function useTextTool(): ContextType;
declare function TextToolProvier(props: {
    children: JSX.Element;
}): JSX.Element;
export { TextToolProvier, useTextTool };
