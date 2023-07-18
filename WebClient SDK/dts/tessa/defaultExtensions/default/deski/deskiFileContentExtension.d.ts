import { CardGetFileContentExtension, ICardGetFileContentExtensionContext } from 'tessa/cards/extensions';
export declare class DeskiFileContentExtension extends CardGetFileContentExtension {
    beforeRequest(context: ICardGetFileContentExtensionContext): Promise<void>;
    private isVirtualFile;
    private requestForEditableFile;
    private requestForReadonlyFile;
}
