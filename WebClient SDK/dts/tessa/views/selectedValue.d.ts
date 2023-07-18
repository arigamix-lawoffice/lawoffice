import { ViewReferenceMetadataSealed } from './metadata/viewReferenceMetadata';
import { ViewMetadataSealed } from './metadata/viewMetadata';
import { IViewContext } from 'tessa/ui/views/viewContext';
export declare class SelectedValue {
    constructor(displayText: string, value: any, metadata?: ViewReferenceMetadataSealed | null, selectedRow?: ReadonlyMap<string, any> | null, viewMetadata?: ViewMetadataSealed | null);
    get cardTypeAlias(): string;
    displayText: string;
    value: any;
    metadata: ViewReferenceMetadataSealed | null;
    selectedRow: ReadonlyMap<string, any> | null;
    viewMetadata: ViewMetadataSealed | null;
    viewContext: IViewContext | null;
}
