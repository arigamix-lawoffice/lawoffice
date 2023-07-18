#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(TypeFontsFix))]
    public class TypeFontsFix :
        ServerConsoleScriptBase
    {
        #region Constants

        private const string LegacyFont1 = "Segoe UI";

        private const string LegacyFont2 = "Segoe UI, ./Fonts/#Open Sans";

        #endregion

        #region FixFontsVisitor Private Class

        public sealed class FixFontsVisitor :
            CardTypeVisitor
        {
            public bool HasChanges { get; set; }

            public override ValueTask VisitControlAsync(CardTypeControl control, CardTypeBlock block, CardTypeForm form, CardType type, CancellationToken cancellationToken = default)
            {
                if (control.ControlSettings.TryGet<object?>("TextStyle") is Dictionary<string, object?> textStyle
                    && textStyle.TryGet<object?>("SelectedFontFamily") is string and (LegacyFont1 or LegacyFont2))
                {
                    textStyle["SelectedFontFamily"] = null;
                    this.HasChanges = true;
                }

                return base.VisitControlAsync(control, block, form, type, cancellationToken);
            }
        }

        #endregion

        #region Base Overrides

        protected override async ValueTask ExecuteCoreAsync(CancellationToken cancellationToken)
        {
            string? source = this.TryGetParameter("source");
            if (string.IsNullOrEmpty(source))
            {
                await this.Logger.ErrorAsync(
                    "Pass the path to .jtype file or folders with such files in the" +
                    " \"dir\" parameter, i.e.: -pp:source=C:\\Repository\\Configuration\\Types");
                this.Result = -1;
                return;
            }

            var visitor = new FixFontsVisitor();
            foreach (string filePath in DefaultConsoleHelper.GetSourceFiles(source, "*.jtype"))
            {
                try
                {
                    await this.Logger.InfoAsync("Checking font fixes in file \"{0}\"", filePath);
                    string json = await File.ReadAllTextAsync(filePath, Encoding.UTF8, cancellationToken);

                    var type = new CardType();
                    await type.DeserializeFromJsonAsync(json, cancellationToken);

                    visitor.HasChanges = false;
                    await type.VisitAsync(visitor, cancellationToken);

                    if (visitor.HasChanges)
                    {
                        await this.Logger.InfoAsync("Saving font fixes in file \"{0}\"", filePath);
                        json = await type.SerializeToJsonAsync(indented: true, cancellationToken);
                        await File.WriteAllTextAsync(filePath, json, Encoding.UTF8, cancellationToken);
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    await this.Logger.LogExceptionAsync($"An error occurred during the operation with file \"{filePath}\": ", ex);
                }
            }

            this.Result = 0;
        }

        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Fixes font settings in types files .jtype.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("-pp:source=PathToTypes - Specifies path to a type or to a folder containing types (recursively in subfolders).");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Example:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(TypeFontsFix)}" +
                " -pp:source=C:\\Repository\\Configuration\\Types");
        }

        #endregion
    }
}
