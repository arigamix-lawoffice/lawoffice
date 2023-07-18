import { FunctionComponent, SyntheticEvent } from 'react';
export interface RichTextBoxFontSizeToolbarProps {
    fontSize?: string;
    onChange: (event: SyntheticEvent, value: string) => void;
    dropDownDirectionUp: boolean;
    title?: string;
}
export declare const RichTextBoxFontSizeToolbar: FunctionComponent<RichTextBoxFontSizeToolbarProps>;
