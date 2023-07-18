import { SerializationTransform } from '../common';
export declare function map(): () => SerializationTransform;
export declare function map(transform: SerializationTransform): () => SerializationTransform;
export declare function map(transformAction: () => SerializationTransform): () => SerializationTransform;
