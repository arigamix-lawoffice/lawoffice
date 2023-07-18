import { FC, CSSProperties } from 'react';
import { IntoArgs } from './common';
export interface HighlighterProps extends Omit<IntoArgs, 'searchWords'> {
    searchWords: string;
    highlightTag?: string;
    highlightClassName?: string;
    highlightStyle?: CSSProperties;
}
export declare const Highlighter: FC<HighlighterProps>;
