import { IControlViewModel } from 'tessa/ui/cards';
export interface ControlProps<T extends IControlViewModel> {
    viewModel: T;
}
