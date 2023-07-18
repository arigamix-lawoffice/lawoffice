import { IForumData } from 'tessa/forums';
export interface IForumCachedData {
    getForumData(): IForumData;
    setForumData(data: IForumData): any;
}
export declare class ForumCachedData implements IForumCachedData {
    constructor(forumData: IForumData);
    private _data;
    getForumData: () => IForumData;
    setForumData: (data: IForumData) => void;
}
