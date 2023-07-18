import { IStorage } from 'tessa/platform/storage';
/**
 * Инициализация работы с deep link
 * @param request информация о файле
 * @param operationType тип операции
 * @returns при успешной инициализации:
 *  operationId - идентификатор операции, запущенной для проверки наличия deskiMobile
 *  link - ссылка, по которой deskiMobile должен подтвердить свое наличие на устройстве пользователя
 */
export declare const startOperation: (request: IStorage<any>, operationType: 'sign' | 'verify') => Promise<{
    operationId: string;
    link: string;
} | null>;
/**
 * Ожидание подтверждения от deskiMobile своего наличия на устройстве пользователя и парсинг результата
 * @param operationId идентификатор операции, запущенной для работы с deskiMobile
 * @returns идентификатор операции, запущенной для работы с deskiMobile
 */
export declare const validateTrackingDeskiMobile: (operationId: string) => Promise<guid | null>;
