import { IViewContext } from './viewContext';
import { SelectedValue } from 'tessa/views/selectedValue';
export interface SelectAction {
    (context: ISelectFromViewContext): Promise<void>;
}
export interface ISelectFromViewContext {
    viewContext: IViewContext;
    selected: SelectedValue[];
}
