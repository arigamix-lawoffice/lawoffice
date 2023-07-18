import * as React from 'react';
import { ValidationResult, ValidationResultItem } from 'tessa/platform/validation';
export interface ValidationResultMaintenanceProps {
    onlyMaintenance?: boolean;
    maintenanceItems?: boolean[] | null;
}
export interface ValidationResultBodyProps extends ValidationResultMaintenanceProps {
    result: ValidationResult;
    caption?: string;
    handleClose?: () => void;
}
export interface ValidationResultBodyState {
    currentItem: ValidationResultItem | null;
}
export declare class ValidationResultBody extends React.Component<ValidationResultBodyProps, ValidationResultBodyState> {
    constructor(props: ValidationResultBodyProps);
    private _clipboardRef;
    private _clipboard;
    private _locManager;
    componentWillUnmount(): void;
    render(): JSX.Element;
    private checkMaintenance;
    private renderMaintenance;
    private renderCommon;
    private renderDetails;
    closeDetails(): boolean;
    private getCaption;
    private localize;
    private handleCloseDetails;
    private handleShowDetails;
    private handleCopy;
}
