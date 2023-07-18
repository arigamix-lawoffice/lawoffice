using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tessa.Cards;
using Tessa.Platform.Json;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Console.ConfigureGenerator.Generators
{
    public sealed class NotificationGenerator : IGenerator
    {
        public async Task<string> GenerateAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendLine("namespace Tessa.Extensions.Shared.Info");
            builder.AppendLine("{// ReSharper disable InconsistentNaming");
            builder.AppendLine("    public static class NotificationInfo");
            builder.AppendLine("    {");

            foreach (var file in Directory.GetFiles(path, "*.jcard", SearchOption.TopDirectoryOnly).OrderBy(Path.GetFileName))
            {
                using var stream = new StringReader(await File.ReadAllTextAsync(file, cancellationToken));
                using var jsonReader = new JsonTextReader(stream);
                var storage = TessaSerializer.JsonTyped.Deserialize<List<object>>(jsonReader);
                if (storage.Count == 0
                    || !(storage[0] is Dictionary<string, object> requestStorage))
                {
                    // это такая же критичная ошибка, как невозможность прочитать из стрима, поэтому кидаем исключение
                    throw new InvalidOperationException($"Can't read card as {CardFileFormat.Json} format from specified stream.");
                }

                var storeRequest = new CardStoreRequest(requestStorage);
                var card = storeRequest.Card;
                var caption = Translate.ToName(Transliteration.Front(Path.GetFileNameWithoutExtension(file)));

                builder.AppendLine("         /// <summary>");
                builder.AppendLine($"         ///     Notification identifier for \"{caption}\": {card.ID:B}.");
                builder.AppendLine("         /// </summary>");
                builder.AppendLine($"         public static readonly Guid {caption}ID = new Guid({card.ID.ToString("X").Replace("{", "", StringComparison.Ordinal).Replace("}", "", StringComparison.Ordinal)});");
                builder.AppendLine();
            }

            builder.AppendLine("    }");
            builder.Append('}');

            return builder.ToString();
        }

        public async Task<string> GenerateWebAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("// noinspection JSUnusedGlobalSymbols,SpellCheckingInspection");
            builder.AppendLine();
            builder.AppendLine("export class NotificationInfo {");
            builder.AppendLine("  //#region Notifications");
            foreach (var file in Directory.GetFiles(path, "*.jcard", SearchOption.TopDirectoryOnly).OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase))
            {
                using var stream = new StringReader(await File.ReadAllTextAsync(file, cancellationToken));
                using var jsonReader = new JsonTextReader(stream);
                var storage = TessaSerializer.JsonTyped.Deserialize<List<object>>(jsonReader);
                if (storage.Count == 0 || storage[0] is not Dictionary<string, object> requestStorage)
                {
                    // это такая же критичная ошибка, как невозможность прочитать из стрима, поэтому кидаем исключение
                    throw new InvalidOperationException($"Can't read card as {CardFileFormat.Json} format from specified stream.");
                }

                var storeRequest = new CardStoreRequest(requestStorage);
                var card = storeRequest.Card;
                var caption = Translate.AliasName(card.Sections["Notifications"].Fields.Get<string>("Name"));

                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Notification identifier for \"{caption}\": {card.ID:B}.");
                builder.AppendLine("   */");
                builder.AppendLine($"  static readonly {caption}ID: guid = '{card.ID}';");
            }

            builder.AppendLine();
            builder.AppendLine("  //#endregion");
            builder.Append('}');

            return builder.ToString();
        }
    }
}