export declare enum FileTemplateType {
    Card = 0,
    View = 1
}
export declare class FileTemplate {
    constructor(id: guid, name: string, templateType: FileTemplateType, group: string, fileName: string, typesOrViews: any[], convertToPdf: boolean);
    readonly id: guid;
    readonly name: string;
    readonly templateType: FileTemplateType;
    readonly group: string;
    readonly fileName: string;
    readonly types: ReadonlyArray<guid>;
    readonly views: ReadonlyArray<string>;
    readonly convertToPdf: boolean;
}
