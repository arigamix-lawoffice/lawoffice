import { IStorage } from 'tessa/platform/storage';
import { showError, showNotEmpty, tryGetFromInfo } from 'tessa/ui';
import { DeskiMobileService } from './deskiMobileService';
import { InitOperationRequest } from './deskiMobileInitOperationRequest';

/**
 * Инициализация работы с deep link
 * @param request информация о файле
 * @param operationType тип операции
 * @returns при успешной инициализации:
 *  operationId - идентификатор операции, запущенной для проверки наличия deskiMobile
 *  link - ссылка, по которой deskiMobile должен подтвердить свое наличие на устройстве пользователя
 */
export const startOperation = async (
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  request: IStorage<any>,
  operationType: 'sign' | 'verify'
): Promise<{ operationId: string; link: string } | null> => {
  const requestBody = new InitOperationRequest(request);
  const [signOperationResponse, signOperationResult] =
    await DeskiMobileService.instance.initOperation(requestBody, operationType);
  if (!signOperationResult.isSuccessful) {
    await showNotEmpty(signOperationResult);
    return null;
  }

  const operationId = tryGetFromInfo<guid>(signOperationResponse, 'OperationID');
  if (!operationId) {
    await showError('initial OperationID is null.');
    return null;
  }

  const link = tryGetFromInfo<string>(signOperationResponse, 'Link');
  if (!link) {
    await showError('Link is null.');
    return null;
  }

  return { operationId, link };
};

/**
 * Ожидание подтверждения от deskiMobile своего наличия на устройстве пользователя и парсинг результата
 * @param operationId идентификатор операции, запущенной для работы с deskiMobile
 * @returns идентификатор операции, запущенной для работы с deskiMobile
 */
export const validateTrackingDeskiMobile = async (operationId: string): Promise<guid | null> => {
  const validationResult = await DeskiMobileService.instance.trackingDeskiMobile(
    operationId,
    10000
  );
  if (!validationResult.isSuccessful) {
    await showNotEmpty(validationResult);
    return null;
  }

  return operationId;
};
