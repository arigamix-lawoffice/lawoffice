#nullable enable
using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Cards;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.UI.CardFiles
{
    /// <summary>
    /// Card files extensions class.
    /// </summary>
    public static class CardFilesExtensions
    {
        #region Private Methods

        private static List<TryGetControlInitializationStrategyAsync>?
            GetFileViewExtensionInitializationStrategyHandlersCore(
                ICardModel cardModel,
                bool tryGet)
        {
            var list = cardModel.Info.TryGet<List<TryGetControlInitializationStrategyAsync>>(
                nameof(DefaultCardTypeExtensionTypes.InitializeFilesView));
            if (list is null && !tryGet)
            {
                list = new();
                cardModel.Info[nameof(DefaultCardTypeExtensionTypes.InitializeFilesView)] = list;
            }

            return list;
        }

        #endregion

        #region ICardModel Extensions

        /// <summary>
        /// Get not null list of strategy selection handlers for files in view extension for adding a new one for given card model.
        /// </summary>
        /// <param name="cardModel">Model of the card.</param>
        /// <returns>Not null list of strategy selection handlers for files in view extension.</returns>
        public static List<TryGetControlInitializationStrategyAsync> GetFileViewExtensionInitializationStrategyHandlers(
            this ICardModel cardModel) =>
            GetFileViewExtensionInitializationStrategyHandlersCore(cardModel, tryGet: false)!;

        /// <summary>
        /// Get nullable list of strategy selection handlers for files in view extension for given card model.
        /// </summary>
        /// <param name="cardModel">Model of the card.</param>
        /// <returns>Nullable list of strategy selection handlers for files in view extension.</returns>
        public static List<TryGetControlInitializationStrategyAsync>?
            TryGetFileViewExtensionInitializationStrategyHandlers(this ICardModel cardModel) =>
            GetFileViewExtensionInitializationStrategyHandlersCore(cardModel, tryGet: true);

        #endregion
    }
}
