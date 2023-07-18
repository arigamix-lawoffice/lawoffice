import React from 'react';
import { ElementSide, InteractionEventTypes } from './common';
declare const SidePanelWrapperProxy: React.FC;
export interface SidePanelWrapperProps {
    openedElement: ElementSide | null;
    openEventType: InteractionEventTypes.EVENT_TYPE_MOUSE | InteractionEventTypes.EVENT_TYPE_TOUCH;
    isLeftPanelOnClickOnly: boolean;
    isRightPanelOnClickOnly: boolean;
}
export default SidePanelWrapperProxy;
