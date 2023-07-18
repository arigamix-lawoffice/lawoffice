import React, { SyntheticEvent } from 'react';
export interface EmojiPickerHeaderProps {
    groupPairs: [string, string][];
    onHeaderPartClicked: (e: SyntheticEvent) => void;
}
export declare class EmojiPickerHeader extends React.Component<EmojiPickerHeaderProps> {
    render(): JSX.Element | null;
}
