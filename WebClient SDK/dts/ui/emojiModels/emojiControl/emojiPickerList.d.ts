import { IEmoji, IEmojiPickerViewModel } from 'ui';
import React, { SyntheticEvent } from 'react';
export interface EmojiPickerListProps {
    viewModel: IEmojiPickerViewModel;
    onEmojiPicked: (e: SyntheticEvent, emoji: IEmoji) => void;
}
export interface EmojiPickerListState {
    isVariationsPopupOpen: boolean;
    rootElement: React.ReactInstance | null;
    popupContent: JSX.Element | null;
}
export declare class EmojiPickerList extends React.Component<EmojiPickerListProps, EmojiPickerListState> {
    private _virtuoso;
    constructor(props: EmojiPickerListProps);
    render(): JSX.Element | null;
    scrollToGroup(groupName: string): void;
    private renderGroup;
    private outsideClickHandler;
    private emojiClickHandler;
    private showVariationsPopup;
}
