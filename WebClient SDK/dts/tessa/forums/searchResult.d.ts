import { MessageModel } from '.';
export declare class SearchResult {
    constructor();
    private _messages;
    private _messageCount;
    private _currentMessageNumber;
    private _page;
    get messages(): MessageModel[];
    get messageCount(): number;
    set messageCount(value: number);
    get currentMessageNumber(): number;
    set currentMessageNumber(value: number);
    get page(): number;
    set page(value: number);
}
