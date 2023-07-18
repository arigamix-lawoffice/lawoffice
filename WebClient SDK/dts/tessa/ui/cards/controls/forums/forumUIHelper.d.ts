import { ItemModel } from 'tessa/forums';
import { ICardModel } from '../../interfaces';
export declare function getMessageBody(htmlText?: string): string;
export declare function mergeWithQuoteBlock(topicId: string, topicTypeId: string, messageId: string, created: string, authorName: string, body: string, currentBody: string): string;
export declare function getQuoteBlock(topicId: string, topicTypeId: string, messageId: string, created: string, authorName: string, body: string): string;
export declare function createServiceMessage(items: ItemModel[]): string;
export declare function isEmptyMessage(message: string): boolean;
export declare function clearMessageBodyFromServiceText(body: string): string;
export declare function getThumbnails(message: string): {
    oldId: string;
    newId: string;
    data: File;
}[];
export declare function data64ToFile(data: string, name: string, type: string): File;
export declare function replaceImageIds(message: string, idPairs: {
    oldId: string;
    newId: string;
}[]): string;
export declare function openTopic(cardModel: ICardModel, messageId: string, topicId: string, topicTypeId: string, openMessageTime?: string | null, _isNeedUpdateLastReadMessageTime?: boolean, forumViewModelId?: string): Promise<void>;
export declare function openCardWithTopic(cardId: string, messageId: string, topicId: string, topicTypeId: string, needDispatch?: boolean, forumViewModelId?: string): Promise<void>;
export declare function tryOpenTopicLink(href: string, needDispatch?: boolean, forumViewModelId?: string): Promise<void>;
export declare function writeToClipboard(text: string, html?: string): Promise<void>;
export declare function execCopyCommand(html: string): void;
