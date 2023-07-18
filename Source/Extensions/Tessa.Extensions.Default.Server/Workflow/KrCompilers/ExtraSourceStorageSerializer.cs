using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Platform;
using Tessa.Platform.Json;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <inheritdoc cref="IExtraSourceSerializer"/>
    public sealed class ExtraSourceStorageSerializer :
        IExtraSourceSerializer
    {
        #region Fields

        private readonly JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new TypedJsonConverter() },
            MaxDepth = TessaSerializer.JsonMaxDepth,
        });

        #endregion

        #region IExtraSourceSerializer Members

        /// <inheritdoc />
        public string Serialize(
            IList<IExtraSource> list)
        {
            if (list is null)
            {
                return null;
            }

            var toSerialize = new List<object>(list.Count);
            foreach (var item in list)
            {
                if (item is ExtraSource storageItem)
                {
                    toSerialize.Add(storageItem.GetStorage());
                }
                else
                {
                    throw new InvalidOperationException(
                        $"{this.GetType().FullName} supports only {typeof(ExtraSource).FullName} serialization. Actual item type: {item?.GetType().FullName ?? "<Item is null>"}.");
                }
            }

            var sb = StringBuilderHelper.Acquire(256);
            var sw = new StringWriter(sb, CultureInfo.InvariantCulture);
            using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.Formatting = this.jsonSerializer.Formatting;
                this.jsonSerializer.Serialize(jsonWriter, toSerialize);
            }

            return sb.ToStringAndRelease();
        }

        /// <inheritdoc />
        public IList<IExtraSource> Deserialize(
            string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<IExtraSource>();
            }

            var list = StorageHelper.DeserializeListFromTypedJson(json);
            var resultList = new List<IExtraSource>(list.Count);
            foreach (var item in list)
            {
                if (!(item is IDictionary<string, object> itemDict))
                {
                    throw new InvalidOperationException();
                }

                resultList.Add(new ExtraSource(itemDict.ToDictionaryStorage()));
            }

            return resultList;
        }

        #endregion
    }
}
