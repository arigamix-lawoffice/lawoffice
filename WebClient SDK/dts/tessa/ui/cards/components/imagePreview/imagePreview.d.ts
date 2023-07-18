import React from 'react';
import { ImagePreviewViewModel } from './imagePreviewViewModel';
export interface ImagePreviewProps {
    viewModel: ImagePreviewViewModel;
    onClose: () => void;
}
export declare class ImagePreview extends React.Component<ImagePreviewProps> {
    private handleCloseForm;
    render(): JSX.Element;
}
export declare function openImagePreview(src: string, showSaveButton?: boolean): Promise<void>;
