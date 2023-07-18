import { SerializationInfo, JSONObject } from './common';
import { SerializationContext } from './serializationContext';
export declare function serialize(model: Object, context?: SerializationContext | null): JSONObject;
export declare function serializeModel(info: SerializationInfo, model: Object, context: SerializationContext): JSONObject;
