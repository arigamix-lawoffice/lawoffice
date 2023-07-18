import * as React from 'react';
import { HTMLAttributes } from 'react';
import { IRichTextCharCounter } from './iRichTextCharCounter';
export interface RichTextCharCountProps extends HTMLAttributes<HTMLSpanElement> {
    maxCharCount: number | null | undefined;
    charCounter: IRichTextCharCounter | undefined;
}
export interface RichTextCharCountState {
    charCount: number;
}
export declare class RichTextCharCount extends React.Component<RichTextCharCountProps, RichTextCharCountState> {
    constructor(props: RichTextCharCountProps);
    render(): JSX.Element | null;
    countCharacters(html: string): Promise<void>;
}
