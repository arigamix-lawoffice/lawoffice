import React from 'react';
export declare type ImageStateItem = {
    fileName: string;
    bytes: ArrayBuffer;
    dataUrl: string;
};
declare type State = ImageStateItem[];
declare type ContextType = [State, React.Dispatch<React.SetStateAction<State>>];
declare function useImageStamps(): ContextType;
declare function ImageStampsProvider(props: {
    children: JSX.Element;
    images?: File[];
}): JSX.Element;
export { ImageStampsProvider, useImageStamps };
