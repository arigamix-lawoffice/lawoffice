/**
 * Translate a dotnet datetime format string to a MomentJS format string
 * @param text The dotnet datetime format string to convert
 * @param tolerant If true, some cases where there is not exact equivalent in MomentJs is
 * handled by generating a similar format instead of throwing exception.
 */
export declare function generateMomentJSFormatString(text: string, tolerant?: boolean): string;
