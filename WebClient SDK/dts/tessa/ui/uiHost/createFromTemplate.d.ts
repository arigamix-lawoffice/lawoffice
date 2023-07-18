import { ICardEditorModel } from 'tessa/ui/cards';
import { CreateCardArg } from 'tessa/ui/uiHost/common';
/**
 * Создаёт карточку по шаблону и открывает её в новой вкладке.
 * При создании по шаблону используются и клиентские, и серверные расширения.
 *
 * @param {guid} templateId Идентификатор шаблона, по которому создаётся карточка.
 * @param {CreateCardArg} args Настройки создания карточки.
 * @returns {Promise<ICardEditorModel | null>} {@link ICardEditorModel} карточки, созданной по шаблону, или null, если произошла ошибка.
 */
export declare function createFromTemplate(templateId: guid, args: CreateCardArg): Promise<ICardEditorModel | null>;
