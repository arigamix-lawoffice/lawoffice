import { ICardValidator } from './cardValidator';
import { CardValidationContext } from './cardValidationContext';
import { CardTypeValidatorSealed } from '../types';
export declare class NotNullFieldValidator implements ICardValidator {
    id: string;
    validate(context: CardValidationContext, validator: CardTypeValidatorSealed): void;
    private checkField;
    private checkEntryField;
    private checkTableField;
}
