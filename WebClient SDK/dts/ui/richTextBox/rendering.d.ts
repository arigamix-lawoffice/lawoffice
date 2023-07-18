import React, { SyntheticEvent } from 'react';
import { RenderTessaElementProps, RenderTessaLeafProps } from './common';
export declare function renderElement(props: RenderTessaElementProps, onInnerItemClick?: (e: React.MouseEvent, id: string) => void, onQuoteDelete?: (id: string) => void, onLinkClick?: (e: SyntheticEvent, href: string) => void): JSX.Element;
export declare function renderLeaf(props: RenderTessaLeafProps): JSX.Element;
