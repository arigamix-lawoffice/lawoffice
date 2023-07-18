import { FC } from 'react';
import { UIButton } from 'tessa/ui';
interface IToolbarWithButtons {
    buttons: UIButton[];
}
/**
 * Хук для получения тулбара с кнопками. При переполнении появляется скрол и тень,
 * которая показывает, что можно скролить дальше
 */
export declare const ToolbarWithButtons: FC<IToolbarWithButtons>;
export {};
