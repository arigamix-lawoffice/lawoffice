import { MessageModelBase } from './messageModelBase';
import { MessageType, OutsideType } from './enums';
import { AvatarSource } from './avatarSource';
import { ItemModel } from './itemModel';
import { IStorage, IStorageValueFactory, ArrayStorage } from 'tessa/platform/storage';
export declare class MessageModel extends MessageModelBase {
    constructor(storage?: IStorage);
    static readonly imageSourceKey: string;
    static readonly replyKey: string;
    static readonly typeKey: string;
    static readonly lastNameKey: string;
    static readonly firstNameKey: string;
    static readonly outsideKey: string;
    static readonly attachmentsKey: string;
    get imageSource(): AvatarSource;
    set imageSource(value: AvatarSource);
    get reply(): guid | null;
    set reply(value: guid | null);
    get type(): MessageType;
    set type(value: MessageType);
    get lastName(): string | null;
    set lastName(value: string | null);
    get firstName(): string | null;
    set firstName(value: string | null);
    get outside(): OutsideType;
    set outside(value: OutsideType);
    get attachments(): ArrayStorage<ItemModel>;
    set attachments(value: ArrayStorage<ItemModel>);
    tryGetImageSource(): AvatarSource | null | undefined;
    tryGetAttachments(): ArrayStorage<ItemModel> | null | undefined;
    private static readonly _attachmentFactory;
}
export declare class MessageModelFactory implements IStorageValueFactory<MessageModel> {
    getValue(storage: IStorage): MessageModel;
    getValueAndStorage(): {
        value: MessageModel;
        storage: IStorage;
    };
}
