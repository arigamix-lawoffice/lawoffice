import React from 'react';
declare type State = number;
declare type ContextType = [State, React.Dispatch<React.SetStateAction<State>>];
declare function useScale(): ContextType;
declare function ScaleProvider(props: {
    children: JSX.Element;
}): JSX.Element;
export { ScaleProvider, useScale };
