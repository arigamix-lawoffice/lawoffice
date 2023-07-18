import * as React from 'react';
import { FileSignatureState } from 'tessa/files';
import { ISignatureValidationInfo, ICAdESLoggerEntry, ICertDataAndVerification, ITimestamp, OcspInfo, CrlInfo } from 'tessa/cards';
export interface SignatureValidationInfoDialogProps {
    viewModel: SignatureValidationInfoDialogViewModel;
    onClose: () => void;
}
export declare class SignatureValidationInfoDialog extends React.Component<SignatureValidationInfoDialogProps> {
    render(): JSX.Element;
    private handleCloseForm;
}
export declare class SignatureValidationInfoDialogViewModel {
    readonly state: FileSignatureState;
    readonly validationInfos: ISignatureValidationInfo[];
    constructor(state: FileSignatureState, validationInfos: ISignatureValidationInfo[]);
}
export declare class SignatureValidationOCSPsDialogViewModel {
    readonly ocsps: Partial<OcspInfo>[];
    readonly allCerts: Partial<ICertDataAndVerification>[];
    constructor(ocsps: Partial<OcspInfo>[], allCerts: Partial<ICertDataAndVerification>[]);
}
export interface SignatureValidationOCSPsDialogProps {
    viewModel: SignatureValidationOCSPsDialogViewModel;
    onClose: () => void;
}
export declare class SignatureValidationOCSPsDialog extends React.Component<SignatureValidationOCSPsDialogProps> {
    render(): JSX.Element;
    private handleCloseForm;
}
export declare class SignatureValidationCRLsDialogViewModel {
    readonly crls: Partial<CrlInfo>[];
    readonly allCerts: Partial<ICertDataAndVerification>[];
    constructor(crls: Partial<CrlInfo>[], allCerts: Partial<ICertDataAndVerification>[]);
}
export interface SignatureValidationCRLsDialogProps {
    viewModel: SignatureValidationCRLsDialogViewModel;
    onClose: () => void;
}
export declare class SignatureValidationCRLsDialog extends React.Component<SignatureValidationCRLsDialogProps> {
    render(): JSX.Element;
    private handleCloseForm;
}
export declare class SignatureValidationLogDialogViewModel {
    readonly log: ICAdESLoggerEntry[];
    constructor(log: ICAdESLoggerEntry[]);
}
export interface SignatureValidationLogDialogProps {
    viewModel: SignatureValidationLogDialogViewModel;
    onClose: () => void;
}
export declare class SignatureValidationLogDialog extends React.Component<SignatureValidationLogDialogProps> {
    render(): JSX.Element;
    private handleCloseForm;
}
export declare class SignatureValidationDescriptionDialogViewModel {
    readonly descs: string[][];
    constructor(descs: string[][]);
}
export interface SignatureValidationDescriptionDialogProps {
    viewModel: SignatureValidationDescriptionDialogViewModel;
    onClose: () => void;
}
export declare class SignatureValidationDescriptionDialog extends React.Component<SignatureValidationDescriptionDialogProps> {
    render(): JSX.Element;
    private handleCloseForm;
}
export declare class SignatureValidationCertChainDialogViewModel {
    readonly chain: Partial<ICertDataAndVerification>[];
    readonly allCerts: Partial<ICertDataAndVerification>[];
    constructor(chain: Partial<ICertDataAndVerification>[], allCerts: Partial<ICertDataAndVerification>[]);
}
export interface SignatureValidationCertChainDialogProps {
    viewModel: SignatureValidationCertChainDialogViewModel;
    onClose: () => void;
}
export declare class SignatureValidationCertChainDialog extends React.Component<SignatureValidationCertChainDialogProps> {
    render(): JSX.Element;
    private handleCloseForm;
}
export declare class SignatureValidationTimestampsDialogViewModel {
    readonly items: ITimestamp[];
    readonly data: {
        [key: string]: ICertDataAndVerification;
    };
    constructor(items: ITimestamp[], data: {
        [key: string]: ICertDataAndVerification;
    });
}
export interface SignatureValidationTimestampsDialogProps {
    viewModel: SignatureValidationTimestampsDialogViewModel;
    onClose: () => void;
}
export declare class SignatureValidationTimestampsDialog extends React.Component<SignatureValidationTimestampsDialogProps> {
    render(): JSX.Element;
    private handleCloseForm;
}
