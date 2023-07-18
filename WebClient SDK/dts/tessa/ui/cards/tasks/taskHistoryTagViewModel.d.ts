import React from 'react';
import { ClassNameList } from 'tessa/ui/classNameList';
export declare class TaskHistoryTagViewModel {
    text: string;
    icon: string;
    tooltip: string;
    handler: Function | undefined;
    private _style?;
    get style(): React.CSSProperties | undefined;
    set style(value: React.CSSProperties | undefined);
    readonly className: ClassNameList;
    constructor(text: string, icon?: string, tooltip?: string, handler?: Function);
}
