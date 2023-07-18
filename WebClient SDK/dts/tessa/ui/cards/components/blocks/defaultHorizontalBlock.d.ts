/// <reference types="react" />
import { HorizontalBlockViewModel, SingleControlBlockViewModel } from 'tessa/ui/cards/blocks';
export interface DefaultHorizontalBlockBlockProps {
    viewModel: HorizontalBlockViewModel | SingleControlBlockViewModel;
}
export declare const DefaultHorizontalBlock: (props: DefaultHorizontalBlockBlockProps) => JSX.Element | null;
