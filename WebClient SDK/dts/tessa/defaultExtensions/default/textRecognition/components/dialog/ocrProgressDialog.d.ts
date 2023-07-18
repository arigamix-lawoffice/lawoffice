import React from 'react';
import { OcrProgressDialogOptions } from './ocrProgressDialogOptions';
import { OcrProgressDialogViewModel } from './ocrProgressDialogViewModel';
import './ocrProgressDialogStyle.css';
/** Пропсы диалога прогресса OCR. */
interface OcrProgressDialogProps {
    /** Модель представления диалога прогресса OCR. */
    viewModel: OcrProgressDialogViewModel;
    /** Действие, выполняемое при закрытии диалога прогресса OCR.*/
    onClose: (option: OcrProgressDialogOptions) => void;
}
/** Диалог отслеживания прогресса операции. */
export declare const OcrProgressDialog: React.FC<OcrProgressDialogProps>;
export {};
