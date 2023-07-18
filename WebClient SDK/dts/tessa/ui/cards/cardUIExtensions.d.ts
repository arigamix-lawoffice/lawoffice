/**
 * Возвращает список кэпшнов функциональных ролей с признаком замещения, относящихся к варианту завершения.
 * @param model
 * @param completionOption Вариант завершения
 * @returns Cписок кэпшнов функциональных ролей с признаком замещения
 */
import { ICardModel } from './interfaces';
import { CardTypeCompletionOptionSealed } from 'tessa/cards/types';
import { CardMetadataFunctionRole } from 'tessa/cards/metadata';
export declare const getFunctionRolesNamesWithDeputyInfoForCompletionOption: (model: ICardModel, completionOption: CardTypeCompletionOptionSealed) => {
    caption: string;
    isDeputy: boolean;
}[];
/**
 * Определяем характерные для варианта завершения функциональные роли из доступных для <see cref="CardTask"/> в <see cref="ICardModel"/>.
 * @param model
 * @param completionOption Вариант завершения
 * @returns Характерные для варианта завершения функциональные роли из доступных для <see cref="CardTask"/> в <see cref="ICardModel"/>
 */
export declare const getFunctionRolesForCompletionOption: (model: ICardModel, completionOption: CardTypeCompletionOptionSealed) => CardMetadataFunctionRole[];
