import { FileSignatureEventType, FileSignatureState } from 'tessa/files';
export declare enum FileVersionDialogColumns {
    index = "Number",
    name = "FileName",
    date = "ChangedDate",
    author = "ChangedBy"
}
export declare enum FileSignsDialogColumns {
    state = "state",
    status = "status",
    signer = "signer",
    company = "company",
    signDate = "signDate",
    action = "action",
    profile = "profile",
    userName = "userName",
    comment = "comment",
    errorText = "errorText"
}
export declare const getCheckClass: (check: FileSignatureState) => string;
export declare const getEventText: (eventType: FileSignatureEventType) => string;
