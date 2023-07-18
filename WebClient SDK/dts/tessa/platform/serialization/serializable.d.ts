import { SerializationTransform } from './common';
export declare function serializable(transform?: ((...params: any[]) => SerializationTransform) | SerializationTransform): (...params: any[]) => void;
