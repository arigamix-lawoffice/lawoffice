import React from 'react';
export interface TreeIndentProps {
    level: number;
    hasItems: boolean;
    isLoading: boolean;
    isExpanded: boolean;
    handleOpen: (isExpanded: boolean) => void;
}
export declare const TreeIndent: (props: TreeIndentProps) => JSX.Element;
export interface TreePlusMinusProps {
    hasItems: boolean;
    isLoading: boolean;
    isExpanded: boolean;
    handleOpen: (isExpanded: boolean) => void;
}
export declare const TreePlusMinus: (props: TreePlusMinusProps) => JSX.Element | null;
export declare const TreeSpin: () => JSX.Element;
export declare function levelBackground(level: number): React.CSSProperties;
export declare function setSubsetDropdownStyle(props: any): string;
