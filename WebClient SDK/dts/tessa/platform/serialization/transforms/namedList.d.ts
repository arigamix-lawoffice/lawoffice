import { SerializationTransform } from '../common';
export declare function namedList(transform: SerializationTransform): () => SerializationTransform;
export declare function namedList(transformAction: () => SerializationTransform): () => SerializationTransform;
export declare function namedList(nameProp: string, transform: SerializationTransform): () => SerializationTransform;
export declare function namedList(nameProp: string, transformAction: () => SerializationTransform): () => SerializationTransform;
