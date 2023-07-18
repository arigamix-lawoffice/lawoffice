import { Duration } from 'moment';
/**
 * Представляет собой класс, содержащий информацию по временной зоне.
 */
export declare class TimeZoneInfo {
    readonly TimeZoneId: number;
    readonly TimeZoneUtcOffset: Duration;
    constructor(timeZoneId: number, timeZoneUtcOffsetMinutes: number);
}
