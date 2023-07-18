import { JSONObject } from './common';
import { SerializationContext } from './serializationContext';
export interface SerializeCallback {
    (model: any, jObject: JSONObject, context: SerializationContext): void;
}
export declare function onSerializing(func: SerializeCallback): (...params: any[]) => void;
export declare function onSerialized(func: SerializeCallback): (...params: any[]) => void;
export interface DeserializeCallback {
    (model: any, jObject: JSONObject, context: SerializationContext): void;
}
export declare function onDeserializing(func: DeserializeCallback): (...params: any[]) => void;
export declare function onDeserialized(func: DeserializeCallback): (...params: any[]) => void;
