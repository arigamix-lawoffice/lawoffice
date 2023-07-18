import { Node } from 'slate';
import { IHtmlConversionConfig } from './config';
export declare function fromArbitraryHtml(html: string, dataTypes?: readonly string[]): Node[] | null;
export declare function convertWithConfig(html: string, config: IHtmlConversionConfig): Node[] | null;
