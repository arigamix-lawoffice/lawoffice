import { SerializationTransform } from '../common';
export declare function list(): () => SerializationTransform;
export declare function list(transform: SerializationTransform): () => SerializationTransform;
export declare function list(transformAction: () => SerializationTransform): () => SerializationTransform;
