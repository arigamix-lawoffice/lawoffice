import { SerializationContext } from './serializationContext';
export declare function serializeToObject(object: Object, context?: SerializationContext): Object;
export declare function serializeToString(object: Object, context?: SerializationContext): string;
export declare function serializeToFormData(object: Object, context?: SerializationContext): FormData;
export declare function deserializeFromString(type: Object, text: string, context?: SerializationContext): Object;
export declare function deserializeFromObject(type: Object, object: Object, context?: SerializationContext): Object;
