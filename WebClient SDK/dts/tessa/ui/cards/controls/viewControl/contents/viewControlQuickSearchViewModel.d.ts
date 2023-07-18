import type { ViewControlViewModel } from 'tessa/ui/cards/controls';
import { QuickSearchViewModel } from 'tessa/ui/views/content';
import { ViewControlFilterButtonViewModel } from 'tessa/ui/cards/controls';
export declare class ViewControlQuickSearchViewModel extends QuickSearchViewModel<ViewControlViewModel> {
    readonly filterButtonViewModel: ViewControlFilterButtonViewModel;
    constructor(viewControl: ViewControlViewModel);
    search(): void;
}
