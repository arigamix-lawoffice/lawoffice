import React from 'react';
import { EditorMode } from '../types';
declare type State = EditorMode | undefined;
declare type ContextType = [State, React.Dispatch<React.SetStateAction<State>>];
declare function useEditorMode(): ContextType;
declare function EditorModeProvider(props: {
    children: JSX.Element;
    editorMode?: EditorMode;
}): JSX.Element;
export { EditorModeProvider, useEditorMode };
