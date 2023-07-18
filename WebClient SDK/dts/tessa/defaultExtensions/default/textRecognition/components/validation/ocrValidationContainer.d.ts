import React from 'react';
import { ValidationResult } from 'tessa/platform/validation';
import './ocrValidationContainerStyle.css';
/** Пропсы контрола-контейнера для отображения результата проверки введенных значений. */
interface IOcrValidationContainerProps {
    /** Признак того, что выполняется загрузка данных. */
    isLoadingEnabled?: boolean;
    /** Результат валидации. */
    validationResult: ValidationResult | null;
}
/** Контрол-контейнер для отображения результата проверки введенных значений. */
export declare const OcrValidationContainer: React.FC<IOcrValidationContainerProps>;
export {};
