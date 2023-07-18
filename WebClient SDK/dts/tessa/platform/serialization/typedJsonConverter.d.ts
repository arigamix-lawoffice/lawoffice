import { IStorage, IStorageArray } from 'tessa/platform/storage/storage';
/**
 * Тип, представляющий корень сериализации.
 */
export declare type SerializationRootType = IStorage | IStorageArray;
/**
 * Десериализует указанную JSON-строку Typed-вида ("id::uid":"value") в Pure объектную модель без метаинформации о типах ("id":"value").
 * @throws SyntaxError Ошибка, возникающая в случае некорректного формата typedJson.
 */
export declare function deserializeFromTypedToPureJson(typedJson: string): SerializationRootType;
/**
 * Преобразует модель Typed-вида ("id::uid":"value") в модель без метаинформации о типах ("id":"value"). Меняется указанная исходная модель.
 */
export declare function transformFromTypedToPureJson(typedModel: SerializationRootType): SerializationRootType;
/**
 * Десериализует указанную JSON-строку Typed-вида ("id::uid":"value") в объектную модель Plain-вида ($type, $value).
 * @throws SyntaxError Ошибка, возникающая в случае некорректного формата typedJson.
 */
export declare function deserializeFromTypedToPlain(typedJson: string): SerializationRootType;
/**
 * Преобразует модель Typed-вида ("id::uid":"value") в модель Plain-вида ($type, $value). Меняется указанная исходная модель.
 */
export declare function transformFromTypedToPlain(typedModel: SerializationRootType): SerializationRootType;
/**
 * Сериализует указанную модель Plain-вида ($type, $value) в строку Typed-вида ("id::uid":"value").
 * Исходная указанная модель при этом не меняется.
 */
export declare function serializeFromPlainToTyped(root: SerializationRootType, pretty?: boolean): string;
