import { FileContainer, IFile } from 'tessa/files';
export declare type VerifyProps = {
    file: IFile;
    fileContainer: FileContainer;
    onClose?: () => void;
};
/**
 * Класс синглтон, для проверки подписи в DeskiMobile
 */
export declare class VerifySignaturesHelpers {
    private constructor();
    private static _instance;
    static get instance(): VerifySignaturesHelpers;
    /**
     * Проверка подписи в DeskiMobile, переопределяется в DeskiMobileVerifyExtension
     * @default null
     */
    verifySignatures: ((props: VerifyProps) => Promise<void>) | null;
}
