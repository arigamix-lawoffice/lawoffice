import * as React from 'react';
import { IFormViewModelBase, IFormWithBlocksViewModel } from 'tessa/ui/cards/interfaces';
export interface CardFormProps {
    viewModel: IFormWithBlocksViewModel | IFormViewModelBase;
}
export declare const CardForm: React.FC<CardFormProps>;
