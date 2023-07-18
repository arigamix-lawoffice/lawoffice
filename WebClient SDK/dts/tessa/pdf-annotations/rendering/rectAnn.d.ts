import React from 'react';
import { AreaAnnotation, HighlightAnnotation } from '../types';
declare const RectAnn: React.MemoExoticComponent<(props: {
    annotation: HighlightAnnotation | AreaAnnotation;
    transformStr: string;
}) => JSX.Element | null>;
export { RectAnn };
