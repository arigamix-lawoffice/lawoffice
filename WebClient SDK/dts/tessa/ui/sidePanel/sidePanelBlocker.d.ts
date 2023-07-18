import * as React from 'react';
export interface SidePanelBlockerProps {
    isPanelOpen: boolean;
}
declare class SidePanelBlocker extends React.Component<SidePanelBlockerProps> {
    shouldComponentUpdate(nextProps: SidePanelBlockerProps): boolean;
    render(): JSX.Element;
}
export default SidePanelBlocker;
