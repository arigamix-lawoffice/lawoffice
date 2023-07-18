using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.SourceProviders;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Cards
{
    internal class TestConfigurationCardLibraryImportListener : ICardLibraryImportListener
    {
        private readonly HashSet<Guid> cardsForReimport = new();

        /// <inheritdoc/>
        public ValueTask NotifyCardCanNotImportAsync(
            Card card,
            CancellationToken cancellationToken)
            => ValueTask.CompletedTask;

        /// <inheritdoc/>
        public ValueTask NotifyCardImportFinishedAsync(
            Guid cardID,
            Guid cardTypeID,
            string cardName,
            ValidationResult cardImportResult,
            CancellationToken cancellationToken = default)
        {
            var reimport = this.cardsForReimport.Contains(cardID);

            if (!reimport && cardImportResult.Items.Any(x => x.Type == ValidationResultType.Error && !CardValidationKeys.IsCardExists(x.Key)))
            {
                throw new InvalidOperationException(
                    $"{nameof(TestConfigurationCardLibraryImportListener)} error: can't import card {cardName}:{Environment.NewLine}{cardImportResult.ToString(ValidationLevel.Detailed)}");
            }
            else if (reimport && !cardImportResult.IsSuccessful)
            {
                this.cardsForReimport.Remove(cardID);

                throw new InvalidOperationException(
                    $"{nameof(TestConfigurationCardLibraryImportListener)} error: card exists, but can't delete or import card {cardName}:{Environment.NewLine}{cardImportResult.ToString(ValidationLevel.Detailed)}");
            }

            return ValueTask.CompletedTask;
        }

        /// <inheritdoc/>
        public ValueTask NotifyCardWillBeDeletedAndReimportedAsync(
            Guid cardID,
            CancellationToken cancellationToken)
        {
            this.cardsForReimport.Add(cardID);
            return ValueTask.CompletedTask;
        }

        /// <inheritdoc/>
        public ValueTask NotifyExportedRequestReadedAsync(
            CardStoreRequest cardStoreRequest,
            ValidationResult validationResult,
            CancellationToken cancellationToken)
        {
            ValidationAssert.IsSuccessful(validationResult);
            return ValueTask.CompletedTask;
        }

        /// <inheritdoc/>
        public ValueTask NotifyFilesForImportNotFoundAsync(
            CancellationToken cancellationToken = default)
            => ValueTask.CompletedTask;

        /// <inheritdoc/>
        public ValueTask NotifyFilesImportStartedAsync(
            IReadOnlyList<(ISourceContentProvider cardProvider,
                ISourceContentProvider optionProvider)> providers,
            CancellationToken cancellationToken = default)
            => ValueTask.CompletedTask;

        /// <inheritdoc/>
        public ValueTask NotifyFileWithIgnoredPatternsNotFoundAsync(
            string path,
            CancellationToken cancellationToken = default)
            => ValueTask.CompletedTask;

        /// <inheritdoc/>
        public ValueTask NotifyProgressAsync(
            int totalCount,
            int index,
            string cardName,
            string mergeOptionsPath,
            CancellationToken cancellationToken = default)
            => ValueTask.CompletedTask;
    }
}
