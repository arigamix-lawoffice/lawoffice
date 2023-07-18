export declare class ImageCache {
    private static cache;
    static loadImage(cardId: guid, names: string[], callback: (image: (string | null)[]) => void): Promise<void>;
}
