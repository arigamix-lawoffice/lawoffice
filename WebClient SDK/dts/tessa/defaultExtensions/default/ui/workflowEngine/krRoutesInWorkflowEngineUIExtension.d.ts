import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export default class KrRoutesInWorkflowEngineUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): Promise<void>;
}
