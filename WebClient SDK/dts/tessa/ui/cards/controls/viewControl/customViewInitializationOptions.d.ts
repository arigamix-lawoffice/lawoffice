import { ITessaView } from 'tessa/views';
import { IViewMetadata } from 'tessa/views/metadata';
import { ViewMappingSettings } from '..';
import { IViewControlDataProvider } from './viewControlDataProvider';
export interface CustomViewInitializationOptions {
    tessaView?: ITessaView | null;
    viewAlias?: string | null;
    dataProvider?: IViewControlDataProvider | null;
    viewMetadata?: IViewMetadata | null;
    viewMappingSettings?: ViewMappingSettings[] | null;
}
