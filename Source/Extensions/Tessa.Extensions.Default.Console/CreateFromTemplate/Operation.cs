using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Files;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.SourceProviders;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Console.CreateFromTemplate
{
    public sealed class Operation :
        ConsoleOperation<OperationContext>
    {
        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            ICardManager cardManager,
            ICardFileManager cardFileManager)
            : base(logger, sessionManager, extendedInitialization: true)
        {
            this.cardManager = cardManager;
            this.cardFileManager = cardFileManager;
        }

        #endregion

        #region Fields

        private readonly ICardManager cardManager;

        private readonly ICardFileManager cardFileManager;

        #endregion

        #region Private Methods

        private async Task<bool> CreateFromTemplateAsync(
            string fileName,
            CardFileFormat format,
            bool ignoreRepairMessages,
            CancellationToken cancellationToken = default)
        {
            ValidationResult result;

            try
            {
                CardStoreResponse response = await this.CreateFromTemplateCoreAsync(fileName, format, cancellationToken);

                result = response.ValidationResult.Build();
                if (ignoreRepairMessages && result.IsSuccessful)
                {
                    ValidationResultItem[] newItems = result.Items
                        .Where(x => !CardValidationKeys.IsCardRepair(x.Key))
                        .ToArray();

                    if (newItems.Length == 0)
                    {
                        result = ValidationResult.Empty;
                    }
                    else if (newItems.Length < result.Items.Count)
                    {
                        result = new ValidationResult(newItems);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                result = ValidationResult.FromException(this, ex);
            }

            await this.Logger.LogResultAsync(result);

            return result.IsSuccessful;
        }


        private async Task<CardStoreResponse> CreateFromTemplateCoreAsync(string fileName, CardFileFormat format, CancellationToken cancellationToken = default)
        {
            CardStoreResponse storeResponse;
            ICardFileContainer[] container = { null };

            try
            {
                ISourceContentProvider sourceContentProvider = new FileSourceContentProvider(fileName);
                (_, CardNewResponse newResponse) = await this.cardManager.CreateFromTemplateAsync(
                    sourceContentProvider,
                    async p =>
                    {
                        container[0] ??=
                            await this.cardFileManager.CreateContainerAsync(p.Card,
                                cancellationToken: cancellationToken);

                        foreach (IFile file in container[0].FileContainer.Files)
                        {
                            if (file.ID == p.FileID)
                            {
                                IFileContent fileContent = file.Content;
                                using (await fileContent.EnterLockAsync(p.CancellationToken))
                                {
                                    await fileContent.SetAsync(p.Content, p.CancellationToken);
                                }

                                file.Versions.Added = file.Versions.Last;
                                break;
                            }
                        }
                    },
                    format: format,
                    cancellationToken: cancellationToken);
                

                ValidationResult result = newResponse.ValidationResult.Build();

                if (!result.IsSuccessful || newResponse.CancelOpening)
                {
                    storeResponse = new CardStoreResponse();
                    storeResponse.ValidationResult.Add(result);

                    return storeResponse;
                }

                container[0] ??= await this.cardFileManager.CreateContainerAsync(newResponse.Card, cancellationToken: cancellationToken);

                storeResponse = await container[0].StoreAsync(cancellationToken: cancellationToken);
                storeResponse.ValidationResult.Add(result);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                storeResponse = new CardStoreResponse();
                storeResponse.ValidationResult.AddException(this, ex);
            }
            finally
            {
                if (container[0] != null)
                {
                    await container[0].DisposeAsync();
                }
            }

            return storeResponse;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            int repeatCount = Math.Max(1, context.RepeatCount);

            try
            {
                bool foundFiles = false;
                foreach (string source in DefaultConsoleHelper.GetSourceFiles(context.Source, "*.*", throwIfNotFound: false))
                {
                    string extension = Path.GetExtension(source);
                    CardFileFormat? format = CardHelper.TryParseCardFileFormatFromExtension(extension);

                    if (format.HasValue)
                    {
                        foundFiles = true;

                        await this.Logger.InfoAsync("Creating from template{0}: \"{1}\"", repeatCount > 1 ? $" ({repeatCount})" : null, source);

                        for (int i = 0; i < repeatCount; i++)
                        {
                            if (!await this.CreateFromTemplateAsync(source, format.Value, context.IgnoreRepairMessages, cancellationToken))
                            {
                                return -1;
                            }
                        }
                    }
                }

                if (!foundFiles)
                {
                    throw new FileNotFoundException($"Couldn't locate *.jcard or *.card files in \"{context.Source}\"", context.Source);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error creating cards from templates", e);
                return -1;
            }

            await this.Logger.InfoAsync("Cards are created successfully");
            return 0;
        }

        #endregion
    }
}
