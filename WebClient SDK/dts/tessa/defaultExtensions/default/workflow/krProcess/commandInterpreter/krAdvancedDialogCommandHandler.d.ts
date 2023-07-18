import { AdvancedDialogCommandHandler } from './advancedDialogCommandHandler';
import { IClientCommandHandlerContext } from 'tessa/workflow/krProcess';
import { CardTaskCompletionOptionSettings, CardTaskDialogActionResult } from 'tessa/cards';
import { ICardEditorModel } from 'tessa/ui/cards';
/**
 * Обработчик клиентской команды DefaultCommandTypes.ShowAdvancedDialog.
 */
export declare class KrAdvancedDialogCommandHandler extends AdvancedDialogCommandHandler {
    protected prepareDialogCommand(context: IClientCommandHandlerContext): CardTaskCompletionOptionSettings | null;
    protected completeDialogCore(actionResult: CardTaskDialogActionResult, context: IClientCommandHandlerContext, _cardEditor: ICardEditorModel, parentCardEditor: ICardEditorModel | null): Promise<boolean>;
}
