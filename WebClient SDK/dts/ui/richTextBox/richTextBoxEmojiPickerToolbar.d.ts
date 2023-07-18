import { IEmoji, IEmojiPickerViewModel } from 'ui';
import { FC } from 'react';
export interface RichTextBoxColorPickerToolbarProps {
    icon: string;
    dropDownDirectionUp: boolean;
    onEmojiPicked: (emoji: IEmoji) => void;
    emojiPickerViewModel: IEmojiPickerViewModel;
    title?: string;
    quickSearchPlaceholder: string;
}
export declare const RichTextBoxEmojiPickerToolbar: FC<RichTextBoxColorPickerToolbarProps>;
