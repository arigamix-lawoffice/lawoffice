export declare type MemoChunk = {
    highlight: boolean;
    text: string;
};
export declare type Chunk = {
    highlight: boolean;
    start: number;
    end: number;
};
export declare type IntoArgs = {
    caseSensitive?: boolean;
    findChunks?: typeof defaultFindChunks;
    sanitize?: typeof defaultSanitize;
    searchWords: Array<string>;
    textToHighlight: string;
};
export declare const getChunkedText: (args: IntoArgs) => MemoChunk[];
/**
 * Creates an array of chunk objects representing both higlightable and non highlightable pieces of text that match each search word.
 * @return Array of "chunks" (where a Chunk is { start:number, end:number, highlight:boolean })
 */
export declare const findAll: ({ caseSensitive, findChunks, sanitize, searchWords, textToHighlight }: IntoArgs) => Array<Chunk>;
/**
 * Takes an array of {start:number, end:number} objects and combines chunks that overlap into single chunks.
 * @return {start:number, end:number}[]
 */
export declare const combineChunks: ({ chunks }: {
    chunks: Array<Chunk>;
}) => Array<Chunk>;
/**
 * Examine text for any matches.
 * If we find matches, add them to the returned array as a "chunk" object ({start:number, end:number}).
 * @return {start:number, end:number}[]
 */
declare const defaultFindChunks: ({ caseSensitive, sanitize, searchWords, textToHighlight }: {
    caseSensitive?: boolean | undefined;
    sanitize?: typeof defaultSanitize | undefined;
    searchWords: Array<string>;
    textToHighlight: string;
}) => Array<Chunk>;
export { defaultFindChunks as findChunks };
/**
 * Given a set of chunks to highlight, create an additional set of chunks
 * to represent the bits of text between the highlighted text.
 * @param chunksToHighlight {start:number, end:number}[]
 * @param totalLength number
 * @return {start:number, end:number, highlight:boolean}[]
 */
export declare const fillInChunks: ({ chunksToHighlight, totalLength }: {
    chunksToHighlight: Array<Chunk>;
    totalLength: number;
}) => Array<Chunk>;
declare function defaultSanitize(str: string): string;
