import { Card } from 'tessa/cards';
import { ValidationResult } from 'tessa/platform/validation';
export declare class PersonalRoleSatelliteHelper {
    static updateSettings(satelliteId: guid, action: (card: Card) => void): Promise<ValidationResult>;
}
