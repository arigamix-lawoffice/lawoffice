import { CardRequestExtension, ICardRequestExtensionContext } from 'tessa/cards/extensions';
import { tryGetFromInfo } from 'tessa/ui';
import { IStorage } from 'tessa/platform/storage';
import { KrProcessClientCommand } from 'tessa/workflow/krProcess';
import { ClientCommandInterpreter } from 'tessa/workflow/krProcess/clientCommandInterpreter';

export class KrClientCommandCustomExtension extends CardRequestExtension {

  public async afterRequest(context: ICardRequestExtensionContext) {
    if (context.requestIsSuccessful && context.response) {
      const commands: KrProcessClientCommand[] = [];
      const commandsStorage = tryGetFromInfo<IStorage>(context.response.info, 'KrProcessClientCommandInfoMark', []);
      if (Array.isArray(commandsStorage)) {
        for (let tile of commandsStorage) {
          commands.push(new KrProcessClientCommand(tile));
        }
      } else {
        const names = Object.getOwnPropertyNames(commandsStorage);
        for (let name of names) {
          commands.push(new KrProcessClientCommand(commandsStorage[name]));
        }
      }

      await ClientCommandInterpreter.instance.interpret(commands, context);
    }
  }

}