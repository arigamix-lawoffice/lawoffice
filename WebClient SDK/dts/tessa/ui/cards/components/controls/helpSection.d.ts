/// <reference types="react" />
import { CardHelpMode } from '../../cardHelpMode';
interface HelpSectionProps {
    helpMode: CardHelpMode;
    helpValue: string;
    tooltip: string | null;
}
export declare const HelpSection: ({ helpMode, helpValue, tooltip }: HelpSectionProps) => JSX.Element | null;
export {};
