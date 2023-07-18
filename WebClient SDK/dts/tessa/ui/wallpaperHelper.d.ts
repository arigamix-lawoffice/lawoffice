export declare enum SetWallpaperMode {
    None = 0,
    Init = 1,
    Default = 2
}
export declare function setWallpaper(wallpaper: string | FormData, mode?: SetWallpaperMode): Promise<void>;
