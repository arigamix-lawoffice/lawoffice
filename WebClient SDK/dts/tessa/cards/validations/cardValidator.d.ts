import { CardValidationContext } from './cardValidationContext';
import { CardTypeValidatorSealed } from '../types';
export interface ICardValidator {
    id: guid;
    validate(context: CardValidationContext, validator: CardTypeValidatorSealed): any;
}
