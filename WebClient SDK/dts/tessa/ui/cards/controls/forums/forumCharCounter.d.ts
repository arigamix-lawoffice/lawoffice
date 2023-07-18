import { IRichTextCharCounter } from 'ui/richTextBox';
export declare class ForumCharCounter implements IRichTextCharCounter {
    private readonly _tags;
    private readonly _rnRegex;
    private readonly _ltgtRegex;
    private constructor();
    private static _instance;
    static get instance(): ForumCharCounter;
    getCount(html: string): Promise<number>;
    private countWithHandleBreaksAsync;
}
