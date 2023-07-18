/**
 * Allows to choose the convenient way of the resizer movement.
 */
export declare enum WorkplaceResizeOption {
    /**
     * `Smooth`: workplace views are scalling according to resizer movement simultaneously.
     */
    Smooth = 0,
    /**
     * `Sharp`: workplace views are scalling only in case the movement of resizer is finished and "mouseup" event is happened.
     */
    Sharp = 1
}
