/**
 * Allows to display workplace views in an appropriate for each user way.
 */
export declare enum WorkplaceDisplayOption {
    /**
     * `Collapsed`: workplace views are shown within display sizes according to the scheme from Tessa Admin.
     */
    Collapsed = 0,
    /**
     * `Spread`: workplace views are presented with their actual sizes according to the scheme from Tessa Admin.
     */
    Spread = 1,
    /**
     * `SpreadWithoutMaxHeight`: the same option like _Spread_ one, but CSS-property max-height is excluded for all displayed workplace views.
     */
    SpreadWithoutMaxHeight = 2
}
