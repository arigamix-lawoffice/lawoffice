import { CardGetExtension, ICardGetExtensionContext } from 'tessa/cards/extensions';
/**
 * Если загружается та же карточка, которая содержится в текущем контексте,
 * то из карточки в контексте все задания с флагом <see cref="CardTaskFlags.UnlockedForPerformer"/>
 * переносятся в запрос <see cref="CardGetRequest.TasksToUnlockRowIDList"/>.
 */
export declare class FillTasksToUnlockRowIDListGetExtension extends CardGetExtension {
    beforeRequest(context: ICardGetExtensionContext): void;
}
