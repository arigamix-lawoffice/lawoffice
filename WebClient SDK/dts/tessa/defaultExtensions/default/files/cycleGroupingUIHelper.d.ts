import { IFileControl } from 'tessa/ui/files';
import { CycleFilesMode } from './cycleFilesMode';
import { Card } from 'tessa/cards';
export declare function switchFilesVisibility(control: IFileControl, card: Card, currentCycle: number | null, mode: CycleFilesMode): void;
export declare function modifyFileList(control: IFileControl, card: Card, currentMode: CycleFilesMode, mode: CycleFilesMode): void;
export declare function restoreFilesList(control: IFileControl, card: Card): void;
