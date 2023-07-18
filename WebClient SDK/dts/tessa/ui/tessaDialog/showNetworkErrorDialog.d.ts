import { ButtonProps } from '../loadingOverlay';
/**
 * Отображение лоадера, в случае ошибок связанных с проблемами сети;
 * @param _error - ошибка при выполнении полезной нагрузки;
 * @param networkListener - служит для прослушивания сети, если сеть появилась, то ресолвится;
 * @param controller - представляет объект контроллера, который позволяет при необходимости обрывать один и более DOM запросов;
 * @param text - текст лоадера;
 * @param button - UI кнопка лоадера;
 */
export declare const showLoadingNetworkError: (_error: Error, networkListener: (signal?: AbortSignal | undefined) => Promise<void>, controller?: AbortController | undefined, text?: string | undefined, button?: ButtonProps | undefined) => Promise<void>;
