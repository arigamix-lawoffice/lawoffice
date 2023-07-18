import { ICardModel } from '../interfaces';
import { LabelViewModel } from '../controls';
export declare class TaskLinkViewModel {
    constructor(text: string, command: Function | null, model: ICardModel);
    readonly text: string;
    readonly command: Function | null;
    readonly linkViewModel: LabelViewModel;
}
