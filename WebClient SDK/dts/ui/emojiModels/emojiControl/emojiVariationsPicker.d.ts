import { IEmoji } from 'ui';
import React, { SyntheticEvent } from 'react';
export interface EmojiVariationsPickerProps {
    emojis: IEmoji[];
    onEmojiPicked: (e: SyntheticEvent, emoji: IEmoji) => void;
}
export declare class EmojiVariationsPicker extends React.Component<EmojiVariationsPickerProps> {
    render(): JSX.Element | null;
}
