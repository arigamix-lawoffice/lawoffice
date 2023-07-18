import * as React from 'react';
import { MediaStyle } from 'ui/mediaStyle';
import { TextFieldProps } from 'ui/textField/textField';
import { RaisedButtonProps } from 'ui/buttons/raisedButton/raisedButton';
import { CheckboxProps } from 'ui/checkbox/checkbox';
import type { CustomControlStyle } from 'tessa/ui/cards';
export interface ControlCaptionProps {
    mediaStyle?: MediaStyle | null;
    customStyle?: CustomControlStyle | null;
}
export declare const ControlCaption: import("styled-components").StyledComponent<"label", any, {
    className: "control-caption";
} & ControlCaptionProps, "className">;
export interface StyledControlProps {
    mediaStyle?: MediaStyle | null;
    customStyle?: CustomControlStyle | null;
}
export declare function createStyledControl<T>(component: any): import("styled-components").StyledComponent<React.ForwardRefExoticComponent<React.PropsWithoutRef<T & StyledControlProps> & React.RefAttributes<any>>, any, T & StyledControlProps, never>;
export declare const StyledTextField: import("styled-components").StyledComponent<React.ForwardRefExoticComponent<TextFieldProps & StyledControlProps & React.RefAttributes<any>>, any, TextFieldProps & StyledControlProps, never>;
export declare const StyledRaisedButton: import("styled-components").StyledComponent<React.ForwardRefExoticComponent<Pick<RaisedButtonProps & StyledControlProps, string | number> & React.RefAttributes<any>>, any, RaisedButtonProps & StyledControlProps, never>;
export declare const StyledCheckbox: import("styled-components").StyledComponent<React.ForwardRefExoticComponent<CheckboxProps & StyledControlProps & React.RefAttributes<any>>, any, CheckboxProps & StyledControlProps, never>;
export declare const StyledDiv: import("styled-components").StyledComponent<React.ForwardRefExoticComponent<React.HTMLAttributes<HTMLDivElement> & StyledControlProps & React.RefAttributes<any>>, any, React.HTMLAttributes<HTMLDivElement> & StyledControlProps, never>;
