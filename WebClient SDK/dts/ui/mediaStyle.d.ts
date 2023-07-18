import { CSSObject } from 'styled-components';
export interface MediaStyle {
    default?: CSSObject;
    xs?: CSSObject;
    sm?: CSSObject;
    md?: CSSObject;
    lg?: CSSObject;
    xl?: CSSObject;
    asString?: () => string;
}
export declare const MediaSizes: {
    xs: number;
    sm: number;
    md: number;
    lg: number;
    xl: number;
};
/**
 * Конвертим стили в css
 */
export declare function mediaStyleToString(style?: MediaStyle | null): string;
/**
 * Безопасно добавляем стили
 */
export declare function addToMediaStyle(mediaStyle: MediaStyle | null, size: 'default' | 'xs' | 'sm' | 'md' | 'lg' | 'xl', style: CSSObject): MediaStyle;
export declare function addToMediaStyleIfNotExists(mediaStyle: MediaStyle | null, size: 'default' | 'sm' | 'md' | 'lg' | 'xl', style: CSSObject): MediaStyle;
/**
 * Мерджим два стиля
 */
export declare function mergeMediaStyles(target: MediaStyle | null, source: MediaStyle | null): MediaStyle | null;
/**
 * Вытаскиваем только нужные проперти
 */
export declare function getFromMediaStyle(mediaStyle: MediaStyle | null, props: string[]): MediaStyle | null;
