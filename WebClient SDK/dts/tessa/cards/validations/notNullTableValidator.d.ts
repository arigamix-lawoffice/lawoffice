import { ICardValidator } from './cardValidator';
import { CardValidationContext } from './cardValidationContext';
import { CardTypeValidatorSealed } from '../types';
export declare class NotNullTableValidator implements ICardValidator {
    id: string;
    validate(context: CardValidationContext, validator: CardTypeValidatorSealed): void;
    private checkTable;
    private checkChildSection;
}
