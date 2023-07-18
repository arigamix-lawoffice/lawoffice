import React from 'react';
import { Tool } from '../types';
declare type State = Tool;
declare type ContextType = [State, React.Dispatch<React.SetStateAction<State>>];
declare function useTool(): ContextType;
declare function ToolProvider(props: {
    children: JSX.Element;
}): JSX.Element;
export { ToolProvider, useTool };
