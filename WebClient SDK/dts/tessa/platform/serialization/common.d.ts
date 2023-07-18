import { SerializationContext } from './serializationContext';
import { DeserializeCallback, SerializeCallback } from './callbacks';
export declare type JSONObject = {
    [key: string]: any;
};
export declare type SerializationInfo = {
    ctor: Function;
    factory: (...params: any[]) => SerializationTransform;
    props: {
        [key: string]: SerializationTransform;
    };
    extender?: SerializationInfo;
    onSerializing?: SerializeCallback;
    onSerialized?: SerializeCallback;
    onDeserializing?: DeserializeCallback;
    onDeserialized?: DeserializeCallback;
};
export declare type SerializationTransform = {
    jsonName?: string;
    propName?: string;
    serializer: (value: any, context: SerializationContext) => any;
    deserializer: (value: any, key: string, context: SerializationContext) => any;
};
export declare function createSerializationInfo(ctor: Function): SerializationInfo;
export declare function getSerializationInfo(target: any): SerializationInfo | null;
export declare function getOrCreateSerializationInfo(ctor: Function): SerializationInfo;
export declare type TagPosition = 'before' | 'after' | 'none' | 'override';
