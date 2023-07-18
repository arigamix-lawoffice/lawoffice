export declare class ForumAvatarViewModel {
    constructor(firstName: string | null, lastName: string | null);
    text: string;
    color: string;
    background: string;
    private generate;
    private static getHashFromString;
    private static takeNDigitsFromHash;
}
