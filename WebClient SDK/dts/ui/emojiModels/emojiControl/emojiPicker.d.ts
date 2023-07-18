import React from 'react';
import { IEmojiPickerViewModel, IEmoji } from 'ui/emojiModels/interfaces';
export interface EmojiPickerProps {
    viewModel: IEmojiPickerViewModel;
    onEmojiPicked: (emoji: IEmoji) => void;
    quickSearchPlaceholder: string;
}
export declare class EmojiPicker extends React.Component<EmojiPickerProps> {
    private _emojiPickerListRef;
    constructor(props: EmojiPickerProps);
    componentWillUnmount(): Promise<void>;
    render(): JSX.Element | null;
    private pickEmoji;
    private headerPartClickedHandler;
    private quickSearchTextChange;
    private emojiPickedHandler;
    private quickSearchKeyDown;
}
