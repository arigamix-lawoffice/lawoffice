import React from 'react';
import { Annotation } from '../types';
declare type State = Annotation | undefined;
declare type ContextType = [State, React.Dispatch<React.SetStateAction<State>>];
declare function useSelectedAnnotation(): ContextType;
declare function SelectedAnnotationProvider(props: {
    children: JSX.Element;
}): JSX.Element;
export { SelectedAnnotationProvider, useSelectedAnnotation };
