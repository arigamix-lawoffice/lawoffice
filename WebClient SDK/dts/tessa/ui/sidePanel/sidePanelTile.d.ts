import * as React from 'react';
import { ITile } from 'tessa/ui/tiles/interfaces';
import { TileDirection } from 'tessa/ui/tiles/tileDirection';
export interface SidePanelTileProps {
    tile: ITile;
    direction: TileDirection;
    groupTileAction: (tile: ITile) => void;
}
export declare class SidePanelTile extends React.Component<SidePanelTileProps> {
    render(): JSX.Element | null;
    private renderActionTile;
    private renderSelectorTile;
    private renderActionSelectorTile;
    private renderMainPart;
    private renderSelectorPart;
    private renderOpenIcon;
    private tileAction;
    private groupTileAction;
}
