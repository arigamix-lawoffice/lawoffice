import { AdvancedDialogCommandHandler } from '../krProcess/commandInterpreter/advancedDialogCommandHandler';
import { CardTaskCompletionOptionSettings, CardTaskDialogActionResult } from 'tessa/cards';
import { IClientCommandHandlerContext } from 'tessa/workflow/krProcess';
import { ICardEditorModel } from 'tessa/ui/cards';
/**
 * Обработчик клиентской команды DefaultCommandTypes.WeShowAdvancedDialog.
 */
export declare class WeAdvancedDialogCommandHandler extends AdvancedDialogCommandHandler {
    protected prepareDialogCommand(context: IClientCommandHandlerContext): CardTaskCompletionOptionSettings | null;
    protected completeDialogCore(actionResult: CardTaskDialogActionResult, context: IClientCommandHandlerContext, _dialogCardEditor: ICardEditorModel | null, parentCardEditor: ICardEditorModel | null): Promise<boolean>;
    private getProcessRequest;
}
