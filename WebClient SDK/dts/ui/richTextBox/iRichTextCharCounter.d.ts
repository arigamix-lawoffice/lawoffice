export interface IRichTextCharCounter {
    getCount(html: string): Promise<number>;
}
