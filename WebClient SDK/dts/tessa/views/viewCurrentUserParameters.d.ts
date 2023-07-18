import { RequestParameter } from './metadata/requestParameter';
import { IViewParameterMetadata } from './metadata/viewParameterMetadata';
export declare class ViewCurrentUserParameters {
    getCurrentUserParameter(hidden?: boolean, readOnly?: boolean): RequestParameter;
    provideCurrentUserIdParameter(parameters: RequestParameter[]): void;
    getLocaleParameter(hidden?: boolean, readOnly?: boolean): RequestParameter;
    provideLocaleParameter(parameters: RequestParameter[]): void;
    getFormatSettingsParameter(hidden?: boolean, readOnly?: boolean): RequestParameter;
    provideFormatSettingsParameter(parameters: RequestParameter[]): void;
    readonly currentUserId: string;
    readonly currentUserName: string;
    getCurrentUserParameterMetadata(hidden?: boolean): IViewParameterMetadata;
    readonly locale: string;
    readonly localeName: string;
    getLocaleParameterMetadata(hidden?: boolean): IViewParameterMetadata;
    readonly formatSettings: string;
    readonly formatSettingsName: string;
    getFormatSettingsParameterMetadata(hidden?: boolean): IViewParameterMetadata;
}
