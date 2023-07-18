import IMask, { AnyMaskedOptions, InputMask } from 'imask';
import React from 'react';
import Moment from 'moment';
import { IMaskInputProps } from 'react-imask/dist/mixin';
import { EventHandler } from 'tessa/platform/eventHandler';
import { DateTimeTypeFormat } from './dateTimeTypeFormat';
interface IBlock {
    nameBlock: string;
    selectionStart: number;
    selectionEnd: number;
    min: number | string;
    max: number | string;
    maxLength: number;
    selected?: boolean;
}
interface DeselectBlockEventArgs {
    block: IBlock;
    maskRef: InputMask<AnyMaskedOptions>;
    replaceValue?: string;
}
interface DatePickerFormatterProps {
    dateTimeFormat: DateTimeTypeFormat;
}
export declare class DatePickerFormatter extends React.Component<DatePickerFormatterProps> {
    constructor(props: DatePickerFormatterProps);
    private _blocks;
    private _dateFocusPattern;
    private _timeFocusPattern;
    private _dateTimeFocusPattern;
    private _dateFocusMaskPattern;
    private _timeFocusMaskPattern;
    private _dateTimeFocusMaskPattern;
    private _dateMaskPattern;
    private _timeMaskPattern;
    private _dateTimeMaskPattern;
    private _datePattern;
    private _timePattern;
    private _time24Pattern;
    private _dateTimePattern;
    private _dateSeparator;
    private _timeSeparator;
    private _timeAmDesignator;
    private _timePmDesignator;
    time24Hour: boolean;
    selectedBlock: IBlock | null;
    selectedIndex: number | null;
    isNeedCalculateCursorPosition: boolean;
    mask: IMaskInputProps<IMask.MaskedPatternOptions>;
    maskRef: React.RefObject<IMask.InputMask<IMask.AnyMaskedOptions>>;
    defaultBlocks: ({
        nameBlock: string;
        min: number;
        max: number;
        maxLength: number;
    } | {
        nameBlock: string;
        min: string;
        max: string;
        maxLength: number;
    })[];
    readonly deselectBlock: EventHandler<(args: DeselectBlockEventArgs) => void>;
    handleKeyDownMask: (type: string, key: string, ctrlKey?: boolean, preventDefault?: (() => void) | undefined) => void;
    handleInputSelect: (event: React.FocusEvent<HTMLInputElement>, date: string | null) => void;
    fillBlockValue: (e: DeselectBlockEventArgs) => void;
    getDateFormat(date?: Moment.Moment | null): string | undefined;
    getPattern(): string;
    getFocusPattern(): string;
    getFocusMaskPattern(): string;
    getMaskPattern(): string;
    private _getDifferencePosition;
    private _replacer;
    private _replaceMaskPattern;
    private _replacePattern;
    private _initBlocks;
    private _initMaskBlocks;
}
export {};
