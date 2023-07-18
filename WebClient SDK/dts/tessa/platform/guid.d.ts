export declare const Guid: {
    empty: string;
    equals: (a: guid | null | undefined, b: guid | null | undefined) => boolean;
    newGuid: () => guid;
    isValid: (id: guid | null | undefined) => boolean;
    isEmpty: (id: guid | null | undefined) => boolean;
};
