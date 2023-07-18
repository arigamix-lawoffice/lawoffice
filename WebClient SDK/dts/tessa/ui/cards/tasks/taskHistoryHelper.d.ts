export interface TaskHistoryItemInfo {
    leftItems: ReadonlyArray<{
        caption: string;
        data: string | null;
    }>;
    rightItems: ReadonlyArray<{
        caption: string;
        data: string | null;
    }>;
    bottomItems: ReadonlyArray<{
        caption: string;
        data: string | null;
    }>;
}
export declare function getTaskHistoryTooltip(info: TaskHistoryItemInfo): string;
