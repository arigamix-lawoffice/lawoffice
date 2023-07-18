import { SerializationTransform } from '../common';
export declare function alias(jsonName: string): () => SerializationTransform;
export declare function alias(jsonName: string, propName: string): () => SerializationTransform;
export declare function alias(jsonName: string, transform: SerializationTransform): () => SerializationTransform;
export declare function alias(jsonName: string, transform: () => SerializationTransform): () => SerializationTransform;
export declare function alias(jsonName: string, propName: string, transform: SerializationTransform): () => SerializationTransform;
export declare function alias(jsonName: string, propName: string, transform: () => SerializationTransform): () => SerializationTransform;
