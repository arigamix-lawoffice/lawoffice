import * as React from 'react';
import { RequestParameter } from 'tessa/views/metadata';
export interface FilterBalloonProps {
    parameter: RequestParameter | null;
    handleRemove: (e: React.SyntheticEvent, p: RequestParameter) => void;
    handleOpenFilter?: (e: React.SyntheticEvent, params?: {
        focusValue?: {
            requestParam: RequestParameter;
            criteriaIndex: number;
        };
    }) => void;
}
export declare class FilterBalloon extends React.Component<FilterBalloonProps> {
    render(): JSX.Element | null;
    private onRemoveClickHandler;
    private onOpenFilter;
}
