import { SerializationInfo, JSONObject } from './common';
import { SerializationContext } from './serializationContext';
export declare function deserialize(type: Object, json: JSONObject, model: Object, context?: SerializationContext | null): void;
export declare function deserializeProps(info: SerializationInfo, json: JSONObject, model: Object, context: SerializationContext): void;
