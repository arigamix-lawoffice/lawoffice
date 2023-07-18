using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tessa.Json;
using Tessa.Json.Bson;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.IO;
using Tessa.Platform.Json;
using Tessa.Platform.Runtime;
using Tessa.Views;
using Tessa.Web;
using Tessa.Web.Services;
using Unity;

namespace Tessa.Extensions.Server.Web
{
    [Route("api/b/Views/GetData", Order = -1), AllowAnonymous]
    public sealed class ViewsController :
        TessaControllerBase
    {
        public ViewsController(ITessaWebScope scope) =>
            this.scope = scope ?? throw new ArgumentNullException(nameof(scope));

        private readonly ITessaWebScope scope;

        private ITessaViewService GetService() =>
            this.scope.UnityContainer.Resolve<ITessaViewService>();

        [HttpPost, SessionMethod]
        public async Task<byte[]> PostGetData()
        {
            var request = (ITessaViewRequest) await TessaSerializerFixed.Instance
                .DeserializeBsonAsync(
                    this.Request.Body,
                    typeof(ITessaViewRequest),
                    withLength: true,
                    fallbackDeserialization: true);

            return this.GetService().GetData(request);
        }
    }

    public class TessaSerializerFixed
    {
        private sealed class SafeguardFromNetCoreSerializationBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName) =>
                !assemblyName.StartsWith("System.ComponentModel.TypeConverter", StringComparison.Ordinal)
                    ? null
                    : typeName == "System.ComponentModel.ListSortDirection"
                        ? typeof(ListSortDirection)
                        : typeof(ListSortDirection).Assembly.GetType(typeName, throwOnError: false);
        }

        private enum BinarySerializationType
        {
            Unknown,
            BinarySerializable,
            BsonSerializable,
            Storage
        }

        private readonly BinaryFormatter binaryFormatter = new BinaryFormatter { Binder = new SafeguardFromNetCoreSerializationBinder() };

        private List<object> DeserializeList(JsonReader reader)
        {
            try
            {
                var result = new List<object>();
                while (reader.Read() && reader.TokenType != JsonToken.EndArray)
                {
                    result.Add(this.DeserializeValue(reader));
                }

                if (reader.TokenType != JsonToken.EndArray)
                {
                    throw new InvalidOperationException(
                        FormatUnexpectedTokenMessage(JsonToken.EndArray, reader.TokenType));
                }

                return result;
            }
            catch (TessaSerializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TessaSerializationException($"Failed to deserialize array: {ex.Message}", ex);
            }
        }

        private Dictionary<string, object> DeserializeDictionary(JsonReader reader)
        {
            try
            {
                var result = new Dictionary<string, object>(StringComparer.Ordinal);
                while (reader.Read() && reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TokenType != JsonToken.PropertyName)
                    {
                        throw new InvalidOperationException(
                            FormatUnexpectedTokenMessage(JsonToken.PropertyName, reader.TokenType));
                    }

                    if (reader.ValueType != typeof(string))
                    {
                        throw new InvalidOperationException($"Invalid dictionary key type: '{reader.ValueType.Name}'.");
                    }

                    var key = (string)reader.Value;
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException(
                            "Unexpected end of stream during reading dictionary value.");
                    }

                    var value = this.DeserializeValue(reader);
                    result.Add(key, value);
                }

                if (reader.TokenType != JsonToken.EndObject)
                {
                    throw new InvalidOperationException(
                        FormatUnexpectedTokenMessage(JsonToken.EndObject, reader.TokenType));
                }

                return result;
            }
            catch (TessaSerializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TessaSerializationException($"Failed to deserialize dictionary: {ex.Message}", ex);
            }
        }

        private object DeserializeValue(JsonReader reader)
        {
            switch (reader.TokenType)
            {
                // case JsonToken.None:
                // break;
                case JsonToken.StartObject:
                    return this.DeserializeDictionary(reader);
                case JsonToken.StartArray:
                    return this.DeserializeList(reader);

                // case JsonToken.PropertyName:
                // break;
                case JsonToken.Integer:
                    return reader.ValueType == typeof(int)
                        ? ReadPrimitiveType<int>(reader)
                        : ReadPrimitiveType<long>(reader);

                case JsonToken.Decimal:
                    return ReadPrimitiveType<decimal>(reader);
                case JsonToken.Float:
                    return ReadPrimitiveType<double>(reader);
                case JsonToken.String:
                    return ReadPrimitiveType<string>(reader);
                case JsonToken.Boolean:
                    return ReadPrimitiveType<bool>(reader);
                case JsonToken.Null:
                case JsonToken.Undefined:
                    return null;
                case JsonToken.Date:
                    return ReadPrimitiveType<DateTime>(reader);
                case JsonToken.Bytes:
                    return ReadPrimitiveType<byte[]>(reader);
                case JsonToken.Guid:
                    return ReadPrimitiveType<Guid>(reader);
                default:
                    throw new InvalidOperationException(
                        $"Unexpected token of type '{Enum.GetName(typeof(JsonToken), reader.TokenType)}' found.");
            }
        }

        private static string FormatUnexpectedTokenMessage(JsonToken expectedToken, JsonToken realToken)
        {
            return $"Unexpected token of type '{Enum.GetName(typeof(JsonToken), realToken)}' found." +
                $" Expect token '{Enum.GetName(typeof(JsonToken), expectedToken)}'.";
        }

        private static object ReadPrimitiveType<T>(JsonReader reader)
        {
            try
            {
                return (T)reader.Value;
            }
            catch (Exception ex)
            {
                throw new TessaSerializationException(
                    $"Failed to read value of type '{typeof(T).Name}'." +
                    $" Actual value type: '{(reader.Value != null ? reader.Value.GetType().Name : "null")}'",
                    ex);
            }
        }

        private void SerializeDictionary(JsonWriter writer, Dictionary<string, object> data)
        {
            writer.WriteStartObject();
            foreach (var key in data.Keys.OrderBy(x => x))
            {
                // сортировка по ключам даже в бинарном виде нужна для стабильности:
                // сериализованный объект не зависит от порядка добавления ключей в хеш-таблицу
                writer.WritePropertyName(key);
                this.SerializeValue(writer, data[key]);
            }

            writer.WriteEndObject();
        }

        private void SerializeEnumerable(JsonWriter writer, IEnumerable values)
        {
            writer.WriteStartArray();
            foreach (var value in values)
            {
                this.SerializeValue(writer, value);
            }

            writer.WriteEndArray();
        }

        private void SerializeValue(JsonWriter writer, object value)
        {
            try
            {
                if (value == null)
                {
                    writer.WriteNull();
                    return;
                }

                var valueType = EnsureNotNullableType(value.GetType());
                if (TessaBsonHelper.IsPrimitiveType(valueType))
                {
                    writer.WriteValue(value);
                }
                else if (valueType == typeof(Dictionary<string, object>))
                {
                    this.SerializeDictionary(writer, (Dictionary<string, object>)value);
                }
                else if (typeof(IEnumerable).IsAssignableFrom(valueType))
                {
                    this.SerializeEnumerable(writer, (IEnumerable)value);
                }
                else
                {
                    throw new InvalidOperationException($"Serialization of type '{value.GetType().FullName}' is not supported.");
                }
            }
            catch (TessaSerializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TessaSerializationException($"Failed to serialize value: {ex.Message}", ex);
            }
        }


        private static Type EnsureNotNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>)
              ? Nullable.GetUnderlyingType(t)
              : t;
        }


        private static BinarySerializationType GetBinarySerializationType(Type type) =>
            type.Implements<IBinarySerializable>()
                ? BinarySerializationType.BinarySerializable
                : type.Implements<IBsonSerializable>()
                    ? BinarySerializationType.BsonSerializable
                    : type == typeof(Dictionary<string, object>)
                        ? BinarySerializationType.Storage
                        : BinarySerializationType.Unknown;

        public Dictionary<string, object> Deserialize(byte[] serializedData)
        {
            if (serializedData == null)
            {
                throw new ArgumentNullException("serializedData");
            }

            try
            {
                using (var stream = new MemoryStream(serializedData))
                using (var bsonReader = new BsonReader(stream, false, DateTimeKind.Utc))
                {
                    if (!bsonReader.Read())
                    {
                        throw new InvalidOperationException(
                            "Unexpected end of stream during deserializing data.");
                    }

                    return (Dictionary<string, object>)this.DeserializeValue(bsonReader);
                }
            }
            catch (TessaSerializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TessaSerializationException($"Failed to deserialize data: {ex.Message}", ex);
            }
        }


        public Dictionary<string, object> Deserialize(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            try
            {
                using (var stream = new StringReader(text))
                using (var reader = new JsonTextReader(stream))
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException(
                            "Unexpected end of stream during deserializing data.");
                    }

                    return (Dictionary<string, object>)this.DeserializeValue(reader);
                }
            }
            catch (TessaSerializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TessaSerializationException($"Failed to deserialize data: {ex.Message}", ex);
            }
        }


        public Dictionary<string, object> Deserialize(BsonReader reader)
        {
            Check.ArgumentNotNull(reader, nameof(reader));

            try
            {
                if (!reader.Read())
                {
                    throw new InvalidOperationException(
                        "Unexpected end of stream during deserializing data.");
                }

                return (Dictionary<string, object>)this.DeserializeValue(reader);
            }
            catch (TessaSerializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TessaSerializationException($"Failed to deserialize data: {ex.Message}", ex);
            }
        }


        public Dictionary<string, object> Deserialize(BinaryReader reader)
        {
            Check.ArgumentNotNull(reader, nameof(reader));

            BsonReader bsonReader = null;

            try
            {
                bsonReader = new BsonReader(reader, false, DateTimeKind.Utc) { CloseInput = false };

                if (!bsonReader.Read())
                {
                    throw new InvalidOperationException(
                        "Unexpected end of stream during deserializing data.");
                }

                return (Dictionary<string, object>)this.DeserializeValue(bsonReader);
            }
            catch (TessaSerializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TessaSerializationException($"Failed to deserialize data: {ex.Message}", ex);
            }
            finally
            {
                bsonReader?.Close();
            }
        }

        public void SerializeJson(JsonWriter writer, Dictionary<string, object> storage) =>
            this.SerializeDictionary(
                writer ?? throw new ArgumentNullException(nameof(writer)),
                storage ?? throw new ArgumentNullException(nameof(storage)));

        public Dictionary<string, object> DeserializeJson(JsonReader reader) =>
            this.DeserializeDictionary(reader ?? throw new ArgumentNullException(nameof(reader)));

        public string SerializeJson(Dictionary<string, object> data, bool indented = true)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            try
            {
                var builder = StringBuilderHelper.Acquire(128);
                StringWriter stringWriter = null;
                JsonTextWriter writer = null;

                try
                {
                    stringWriter = new StringWriter(builder);
                    writer = new JsonTextWriter(stringWriter);

                    if (indented)
                    {
                        writer.Formatting = Formatting.Indented;
                    }

                    this.SerializeDictionary(writer, data);
                }
                finally
                {
                    if (writer != null)
                    {
                        // любой JsonWriter реализует IDisposable
                        ((IDisposable)writer).Dispose();
                    }

                    if (stringWriter != null)
                    {
                        stringWriter.Dispose();
                    }

                    builder.Release();
                }

                return builder.ToString();
            }
            catch (TessaSerializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TessaSerializationException($"Failed to serialize data: {ex.Message}", ex);
            }
        }


        public byte[] SerializeBson(Dictionary<string, object> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            try
            {
                using (var stream = new MemoryStream())
                using (var writer = new BsonWriter(stream))
                {
                    this.SerializeDictionary(writer, data);
                    stream.Seek(0, SeekOrigin.Begin);
                    return stream.ToArray();
                }
            }
            catch (TessaSerializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TessaSerializationException($"Failed to serialize data: {ex.Message}", ex);
            }
        }


        public void SerializeBson(BsonWriter writer, Dictionary<string, object> data)
        {
            Check.ArgumentNotNull(data, nameof(data));

            try
            {
                this.SerializeDictionary(writer, data);
            }
            catch (TessaSerializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TessaSerializationException($"Failed to serialize data: {ex.Message}", ex);
            }
        }


        public void SerializeBson(BinaryWriter writer, Dictionary<string, object> data)
        {
            Check.ArgumentNotNull(writer, nameof(writer));
            Check.ArgumentNotNull(data, nameof(data));

            BsonWriter bsonWriter = null;

            try
            {
                bsonWriter = new BsonWriter(writer) { CloseOutput = false };
                this.SerializeDictionary(bsonWriter, data);
            }
            catch (TessaSerializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TessaSerializationException($"Failed to serialize data: {ex.Message}", ex);
            }
            finally
            {
                bsonWriter?.Close();
            }
        }


        public bool TrySerializeBson(MemoryStream stream, object obj)
        {
            Check.ArgumentNotNull(stream, nameof(stream));
            Check.ArgumentNotNull(obj, nameof(obj));

            BinarySerializationType serializationType = GetBinarySerializationType(obj.GetType());

            switch (serializationType)
            {
                case BinarySerializationType.BinarySerializable:
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true))
                    {
                        ((IBinarySerializable)obj).Serialize(writer);
                    }
                    return true;

                case BinarySerializationType.BsonSerializable:
                    using (var writer = new BsonWriter(stream) { CloseOutput = false })
                    {
                        ((IBsonSerializable)obj).Serialize(writer);
                    }
                    return true;

                case BinarySerializationType.Storage:
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true))
                    {
                        this.SerializeBson(writer, (Dictionary<string, object>)obj);
                    }
                    return true;
            }

            Type objType = obj.GetType();

            bool objTypeIsArray = objType.IsArray;
            if (objTypeIsArray || objType.Implements<IEnumerable<object>>())
            {
                Type itemType = objTypeIsArray ? objType.GetElementType() : objType.TryGetEnumerableElementType();
                if (itemType == null)
                {
                    return false;
                }

                BinarySerializationType itemSerializationType = GetBinarySerializationType(itemType);

                if (itemSerializationType == BinarySerializationType.Unknown
                    || itemSerializationType == BinarySerializationType.Storage)
                {
                    return false;
                }

                var items = obj as IReadOnlyCollection<object> ?? ((IEnumerable<object>)obj).ToArray();

                using (var writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true))
                {
                    writer.Write(items.Count);

                    switch (itemSerializationType)
                    {
                        case BinarySerializationType.BinarySerializable:
                            foreach (object item in items)
                            {
                                writer.Write(item != null);

                                if (item != null)
                                {
                                    var serializable = (IBinarySerializable)item;
                                    serializable.Serialize(writer);
                                }
                            }
                            return true;

                        case BinarySerializationType.BsonSerializable:
                            using (var bsonWriter = new BsonWriter(writer) { CloseOutput = false })
                            {
                                foreach (object item in items)
                                {
                                    writer.Write(item != null);

                                    if (item != null)
                                    {
                                        var serializable = (IBsonSerializable)item;
                                        serializable.Serialize(bsonWriter);
                                    }
                                }
                            }
                            return true;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(BinarySerializationType));
                    }
                }
            }

            return false;
        }


        public bool TryDeserializeBson(MemoryStream stream, Type objType, out object obj)
        {
            Check.ArgumentNotNull(stream, nameof(stream));
            Check.ArgumentNotNull(objType, nameof(objType));

            if (objType.IsGenericType && objType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // Nullable<SerializableStruct> => SerializableStruct
                objType = objType.GenericTypeArguments[0];
            }

            BinarySerializationType serializationType = GetBinarySerializationType(objType);
            switch (serializationType)
            {
                case BinarySerializationType.Unknown:
                    break;

                case BinarySerializationType.BinarySerializable:
                    var binarySerializable = (IBinarySerializable)Activator.CreateInstance(objType);
                    using (var reader = new BinaryReader(stream, Encoding.UTF8))
                    {
                        binarySerializable.Deserialize(reader);
                    }

                    obj = binarySerializable;
                    return true;

                case BinarySerializationType.BsonSerializable:
                    var bsonSerializable = (IBsonSerializable)Activator.CreateInstance(objType);
                    using (var reader = new BsonReader(stream, false, DateTimeKind.Utc))
                    {
                        bsonSerializable.Deserialize(reader);
                    }

                    obj = bsonSerializable;
                    return true;

                case BinarySerializationType.Storage:
                    using (var reader = new BsonReader(stream, false, DateTimeKind.Utc))
                    {
                        obj = this.Deserialize(reader);
                    }
                    return true;

                default:
                    throw new ArgumentOutOfRangeException(nameof(BinarySerializationType));
            }

            bool objTypeIsArray = objType.IsArray;
            if (objTypeIsArray || objType.Implements<IEnumerable<object>>())
            {
                Type itemType = objTypeIsArray ? objType.GetElementType() : objType.TryGetEnumerableElementType();
                if (itemType == null)
                {
                    obj = null;
                    return false;
                }

                BinarySerializationType itemSerializationType = GetBinarySerializationType(itemType);

                if (itemSerializationType == BinarySerializationType.Unknown
                    || itemSerializationType == BinarySerializationType.Storage)
                {
                    obj = null;
                    return false;
                }

                Type listType = typeof(List<>).MakeGenericType(itemType);
                MethodInfo addMethod = listType.GetMethod(nameof(List<object>.Add), new[] { itemType });

                if (itemType.IsGenericType && itemType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // Nullable<SerializableStruct> => SerializableStruct
                    itemType = itemType.GenericTypeArguments[0];
                }

                object list = Activator.CreateInstance(listType);
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    int count = reader.ReadInt32();
                    object[] parameters = new object[1];

                    switch (itemSerializationType)
                    {
                        case BinarySerializationType.BinarySerializable:
                            for (int i = 0; i < count; i++)
                            {
                                bool isNotNull = reader.ReadBoolean();
                                if (isNotNull)
                                {
                                    var binarySerializable = (IBinarySerializable)Activator.CreateInstance(itemType);
                                    binarySerializable.Deserialize(reader);
                                    parameters[0] = binarySerializable;
                                }
                                else
                                {
                                    parameters[0] = null;
                                }

                                addMethod.Invoke(list, parameters);
                            }
                            break;

                        case BinarySerializationType.BsonSerializable:
                            using (var bsonReader = new BsonReader(reader, false, DateTimeKind.Utc))
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    bool isNotNull = reader.ReadBoolean();
                                    if (isNotNull)
                                    {
                                        var bsonSerializable = (IBsonSerializable)Activator.CreateInstance(itemType);
                                        bsonSerializable.Deserialize(bsonReader);
                                        parameters[0] = bsonSerializable;
                                    }
                                    else
                                    {
                                        parameters[0] = null;
                                    }

                                    addMethod.Invoke(list, parameters);
                                }
                            }
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(BinarySerializationType));
                    }
                }

                if (objTypeIsArray)
                {
                    // obj = list.ToArray()
                    MethodInfo toArrayMethod = listType.GetMethod(nameof(List<object>.ToArray));
                    obj = toArrayMethod.Invoke(list, EmptyHolder<object>.Array);
                }
                else
                {
                    obj = list;
                }

                return true;
            }

            obj = null;
            return false;
        }


        public async Task<object> DeserializeBsonAsync(
            Stream stream,
            Type objType,
            bool withLength = false,
            bool fallbackDeserialization = false,
            bool useCompression = false)
        {
            Check.ArgumentNotNull(stream, nameof(stream));
            Check.ArgumentNotNull(objType, nameof(objType));

            byte[] data;
            if (withLength)
            {
                int length = await stream.ReadInt32Async().ConfigureAwait(false);
                if (length < 0)
                {
                    // -1 == null
                    return null;
                }

                data = await stream.ReadBytesExactAsync(length).ConfigureAwait(false);
            }
            else
            {
                data = await stream.ReadAllBytesAsync().ConfigureAwait(false);
            }

            if (objType == typeof(byte[]))
            {
                if (useCompression)
                {
                    using (var deflateStream = new DeflateStream(new MemoryStream(data), CompressionMode.Decompress))
                    {
                        data = await deflateStream.ReadAllBytesAsync().ConfigureAwait(false);
                    }
                }

                return data;
            }

            var memoryStream = new MemoryStream(data);

            if (useCompression)
            {
                using (var deflateStream = new DeflateStream(new MemoryStream(data), CompressionMode.Decompress))
                {
                    data = await deflateStream.ReadAllBytesAsync().ConfigureAwait(false);
                }

                memoryStream = new MemoryStream(data);
            }

            if (this.TryDeserializeBson(memoryStream, objType, out object result))
            {
                return result;
            }

            if (fallbackDeserialization)
            {
                // используем асинхронную версию метода, т.к. чтение выполняется в поток в памяти
                NullableObject<object> primitiveObject =
                    await memoryStream.TryReadPrimitiveTypeAsync(objType).ConfigureAwait(false);

                // если тип не примитивный, то он сериализуется стандартным бинарным форматтером
                return primitiveObject.HasValue
                    ? primitiveObject.Value
                    : this.binaryFormatter.Deserialize(memoryStream);
            }

            throw new NotSupportedException(
                $"Can't deserialize object of type {objType.FullName} from bson stream.");
        }


        public async Task SerializeBsonAsync(
            Stream stream,
            object obj,
            bool withLength = false,
            bool fallbackSerialization = false,
            bool useCompression = false,
            MemoryStream bufferStream = null,
            MemoryStream compressionBufferStream = null)
        {
            Check.ArgumentNotNull(stream, nameof(stream));

            switch (obj)
            {
                case null:
                    if (withLength)
                    {
                        // -1 == null
                        await stream.WriteAsync(-1).ConfigureAwait(false);
                    }
                    return;

                case byte[] bytes:
                    if (useCompression)
                    {
                        var bytesBuffer = compressionBufferStream ?? new MemoryStream();
                        using (var deflateStream = new DeflateStream(bytesBuffer, CompressionLevel.Optimal, leaveOpen: true))
                        {
                            await deflateStream.WriteAsync(bytes, 0, bytes.Length, CancellationToken.None).ConfigureAwait(false);
                        }

                        await CopyMemoryStreamFast(bytesBuffer, stream, withLength);
                    }
                    else
                    {
                        if (withLength)
                        {
                            await stream.WriteAsync(bytes.Length).ConfigureAwait(false);
                        }

                        await stream.WriteAsync(bytes, 0, bytes.Length, CancellationToken.None).ConfigureAwait(false);
                    }
                    return;
            }

            MemoryStream memoryStream = bufferStream ?? new MemoryStream();

            if (!this.TrySerializeBson(memoryStream, obj))
            {
                if (!fallbackSerialization)
                {
                    throw new NotSupportedException(
                        $"Object type \"{obj.GetType().FullName}\" isn't supported to be serialized to bson.");
                }

                // используем синхронную версию метода, т.к. запись выполняется в поток в памяти
                if (!memoryStream.TryWritePrimitiveType(obj))
                {
                    // если тип не примитивный, то он сериализуется стандартным бинарным форматтером
                    this.binaryFormatter.Serialize(memoryStream, obj);
                }
            }

            int length = (int)memoryStream.Length;

            if (useCompression)
            {
                var compressionBuffer = compressionBufferStream ?? new MemoryStream();
                using (var deflateStream = new DeflateStream(compressionBuffer, CompressionLevel.Optimal, leaveOpen: true))
                {
                    await deflateStream.WriteAsync(memoryStream.GetBuffer(), 0, length, CancellationToken.None).ConfigureAwait(false);
                }

                await CopyMemoryStreamFast(compressionBuffer, stream, withLength);
            }
            else
            {
                await CopyMemoryStreamFast(memoryStream, stream, withLength);
            }
        }

        private static async Task CopyMemoryStreamFast(MemoryStream source, Stream target, bool withLength)
        {
            int length = (int)source.Length;

            if (withLength)
            {
                await target.WriteAsync(length).ConfigureAwait(false);
            }

            await target.WriteAsync(source.GetBuffer(), 0, length, CancellationToken.None).ConfigureAwait(false);
        }

        public static TessaSerializerFixed Instance { get; } = new TessaSerializerFixed();

        public static JsonSerializer Json => serializerLocal.Value;

        private static readonly ThreadLocal<JsonSerializer> serializerLocal =
            new ThreadLocal<JsonSerializer>(() => Create());


        public static JsonSerializer JsonTyped => serializerTypedLocal.Value;

        private static readonly ThreadLocal<JsonSerializer> serializerTypedLocal =
            new ThreadLocal<JsonSerializer>(() => CreateTyped());

        public static JsonSerializer Create(JsonSerializerSettings settings = null)
        {
            var serializer = JsonSerializer.Create(settings);
            serializer.Converters.Add(new TessaJsonConverter());

            return serializer;
        }


        public static JsonSerializer CreateTyped(JsonSerializerSettings settings = null)
        {
            var serializer = JsonSerializer.Create(settings);
            serializer.Converters.Add(new TypedJsonConverter());

            return serializer;
        }
    }
}