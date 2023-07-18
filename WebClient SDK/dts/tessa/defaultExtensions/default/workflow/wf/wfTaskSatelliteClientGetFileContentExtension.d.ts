import { CardGetFileContentExtension, ICardGetFileContentExtensionContext } from 'tessa/cards/extensions';
export declare class WfTaskSatelliteClientGetFileContentExtension extends CardGetFileContentExtension {
    beforeRequest(context: ICardGetFileContentExtensionContext): Promise<void>;
}
