import React from 'react';
import { ImagePreviewerViewModel } from 'tessa/ui/cards/controls/previewer/imagePreviewerViewModel';
export interface IImagePreviewerProps {
    viewModel: ImagePreviewerViewModel;
}
declare const ImagePreviewer: React.FC<IImagePreviewerProps>;
export default ImagePreviewer;
