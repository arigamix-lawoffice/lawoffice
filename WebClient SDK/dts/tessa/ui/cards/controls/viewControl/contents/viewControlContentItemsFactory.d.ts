import type { ViewControlInitializationContext } from '../viewControlInitializationContext';
import type { BaseViewControlItem } from './baseViewControlItem';
export interface IViewControlContentItemsFactory {
    createTableContent(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createRefreshButton(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createFilterButton(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createFilterText(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createFilterResetButton(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createPaging(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createQuickSearch(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createSortButton(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createMultiSelectButton(context: ViewControlInitializationContext): BaseViewControlItem | null;
}
export declare class StandardViewControlContentItemsFactory implements IViewControlContentItemsFactory {
    createTableContent(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createRefreshButton(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createFilterButton(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createFilterText(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createFilterResetButton(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createPaging(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createQuickSearch(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createSortButton(context: ViewControlInitializationContext): BaseViewControlItem | null;
    createMultiSelectButton(context: ViewControlInitializationContext): BaseViewControlItem | null;
}
