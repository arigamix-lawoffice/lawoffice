/**
 * Выражает настройки редактора OnlyOffice.
 */
export interface OnlyOfficeEditorConfig {
    type?: 'desktop' | 'mobile' | 'embedded';
    width?: string;
    height?: string;
    documentType?: 'text' | 'spreadsheet' | 'presentation' | string;
    document: {
        title?: string;
        url: string;
        fileType?: string;
        options?: string;
        key?: string;
        info?: {
            owner?: string;
            folder?: string;
            uploaded?: string;
            sharingSettings?: Array<{
                user: string;
                permissions: string;
                isLink: boolean;
            }>;
        };
        permissions?: {
            edit?: boolean;
            download?: boolean;
            reader?: boolean;
            review?: boolean;
            print?: boolean;
            rename?: boolean;
            comment?: boolean;
            modifyFilter?: boolean;
            modifyContentControl?: boolean;
            fillForms?: boolean;
            copy?: boolean;
        };
    };
    editorConfig: {
        mode: 'view' | 'edit';
        lang?: string;
        location?: string;
        canCoAuthoring?: boolean;
        createUrl?: string;
        sharingSettingsUrl?: string;
        fileChoiceUrl?: string;
        callbackUrl?: string;
        saveAsUrl?: string;
        licenseUrl?: string;
        customerId?: string;
        region?: string;
        user?: {
            id: string;
            name: string;
        };
        recent?: Array<{
            title: string;
            url: string;
            folder: string;
        }>;
        templates?: Array<{
            title?: string;
            image?: string;
            url?: string;
        }>;
        customization?: {
            logo?: {
                image?: string;
                imageEmbedded?: string;
                url?: string;
            };
            customer?: {
                name?: string;
                address?: string;
                mail?: string;
                www?: string;
                info?: string;
                logo?: string;
            };
            about?: boolean;
            feedback?: {
                visible: boolean;
                url: string;
            };
            goback?: {
                url: string;
                text: string;
                blank: boolean;
                requestClose: boolean;
            };
            chat?: boolean;
            comments?: boolean;
            zoom?: number;
            compactToolbar?: boolean;
            leftMenu?: boolean;
            rightMenu?: boolean;
            hideRightMenu?: boolean;
            toolbar?: boolean;
            statusBar?: boolean;
            autosave?: boolean;
            forcesave?: boolean;
            commentAuthorOnly?: boolean;
            showReviewChanges?: boolean;
            help?: boolean;
            compactHeader?: boolean;
            toolbarNoTabs?: boolean;
            toolbarHideFileName?: boolean;
            reviewDisplay?: string;
            spellcheck?: boolean;
            compatibleFeatures?: boolean;
            unit?: 'cm' | 'pt' | 'inch';
            mentionShare?: boolean;
            macros?: boolean;
            plugins?: boolean;
            macrosMode?: 'warn' | 'enable' | 'disable';
        };
        plugins?: {
            autostart: Array<string>;
            pluginsData: Array<string>;
        };
    };
    events?: {
        onAppReady?: () => void;
        onCollaborativeChanges?: () => void;
        onDocumentReady?: () => void;
        onDocumentStateChange?: () => void;
        onDownloadAs?: () => void;
        onError?: (e: Record<string, unknown>) => void;
        onInfo?: (e: Record<string, unknown>) => void;
        onMetaChange?: () => void;
        onMakeActionLink?: () => void;
        onOutdatedVersion?: () => void;
        onReady?: () => void;
        onRequestClose?: () => void;
        onRequestCompareFile?: () => void;
        onRequestCreateNew?: () => void;
        onRequestEditRights?: () => void;
        onRequestHistory?: () => void;
        onRequestHistoryClose?: () => void;
        onRequestHistoryData?: () => void;
        onRequestInsertImage?: () => void;
        onRequestMailMergeRecipients?: () => void;
        onRequestRename?: () => void;
        onRequestRestore?: () => void;
        onRequestSaveAs?: () => void;
        onRequestSendNotify?: () => void;
        onRequestSharingSettings?: () => void;
        onRequestUsers?: () => void;
        onWarning?: (e: Record<string, unknown>) => void;
    };
}
