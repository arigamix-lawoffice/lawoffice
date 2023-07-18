import { ICardValidator } from './cardValidator';
import { CardValidationContext } from './cardValidationContext';
import { CardTypeValidatorSealed } from '../types';
export declare class UniqueValidator implements ICardValidator {
    id: string;
    validate(_context: CardValidationContext, _validator: CardTypeValidatorSealed): void;
}
