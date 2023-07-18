using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.CopyCulture
{
    public sealed class Operation :
        ConsoleOperation<OperationContext>
    {
        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger)
            : base(logger, sessionManager)
        {
        }

        #endregion

        #region Base Overrides

        public override async Task<int> ExecuteAsync(
            OperationContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await this.Logger.InfoAsync(
                    "Copying localization from {0} to {1} for: \"{2}\"",
                    context.FromCulture.TwoLetterISOLanguageName,
                    context.ToCulture.TwoLetterISOLanguageName,
                    context.Source);

                if (context.FromCulture.Equals(context.ToCulture))
                {
                    // nonsense
                    await this.Logger.InfoAsync("Given cultures are equal. No actions required.");
                    return -2;
                }

                
                bool defaultTarget = string.IsNullOrEmpty(context.Target);
                bool targetPathIsDirectory = !defaultTarget && (File.GetAttributes(context.Target) & FileAttributes.Directory) == FileAttributes.Directory;
                bool sourcePathIsDirectory = (File.GetAttributes(context.Source) & FileAttributes.Directory) ==
                    FileAttributes.Directory;
                if (sourcePathIsDirectory && !defaultTarget && !targetPathIsDirectory)
                {
                    await this.Logger.ErrorAsync(
                        "Invalid command. Source path is a directory but target path \"{0}\" is a file. Potential multiple overrides.",
                        context.Target);
                    return -2;
                }

                foreach (string sourceFilePath in DefaultConsoleHelper
                             .GetSourceFiles(
                                 context.Source,
                                 JsonFileLocalizationService.FileSearchPattern,
                                 throwIfNotFound: false))
                {
                    var localizationService = new JsonFileLocalizationService(new[] { sourceFilePath });

                    LocalizationLibrary library =
                        (await localizationService.GetLibrariesAsync(returnComments: true, cancellationToken: cancellationToken))
                        .First();

                    bool hasChanges = false;
                    if (context.ForceDetached && !library.DetachedCultures.Contains(context.ToCulture))
                    {
                        library.DetachedCultures.Add(context.ToCulture);
                        hasChanges = true;
                    }

                    foreach (LocalizationEntry entry in library.Entries)
                    {
                        if (!entry.Strings.TryGetItem(context.FromCulture, out LocalizationString fromString))
                        {
                            continue;
                        }

                        if (!entry.Strings.TryGetItem(context.ToCulture, out LocalizationString toString))
                        {
                            toString = new LocalizationString(context.ToCulture);
                            entry.Strings.Add(toString);
                        }

                        if (!context.EmptyOnly || string.IsNullOrEmpty(toString.Value))
                        {
                            hasChanges |= !string.Equals(toString.Value, fromString.Value, StringComparison.Ordinal);
                            toString.Value = fromString.Value;
                        }
                    }

                    if (!hasChanges)
                    {
                        await this.Logger.InfoAsync("Library is skipped due to no changes: \"{0}\"", sourceFilePath);
                        continue;
                    }

                    string target = sourceFilePath;
                    if (defaultTarget)
                    {
                        await localizationService.SaveLibraryAsync(library, cancellationToken);
                    }
                    else
                    {
                        target = targetPathIsDirectory
                            ? Path.Combine(context.Target, library.Name + JsonFileLocalizationService.FileNameExtension)
                            : context.Target;

                        await localizationService.SaveLibraryAsync(library, target, cancellationToken);
                    }

                    await this.Logger.InfoAsync("Library is processed: \"{0}\"", target);
                }

                return 0;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error copying localization", e);
                return -1;
            }
        }

        #endregion
    }
}
