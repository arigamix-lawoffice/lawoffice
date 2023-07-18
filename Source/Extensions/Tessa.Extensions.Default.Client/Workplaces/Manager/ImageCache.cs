// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageCache.cs" company="Syntellect">
//   Tessa Project
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Tessa.Extensions.Default.Client.Workplaces.Manager
{
    #region

    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;

    using Tessa.Cards;
    using Tessa.Platform.Validation;
    using Tessa.Properties.Resharper;
    using Tessa.UI;
    using Tessa.UI.Cards;

    #endregion

    /// <summary>
    /// Кеш изображений получаемых из карточек
    /// </summary>
    public sealed class ImageCache
    {
        /// <summary>
        /// The cache.
        /// </summary>
        [NotNull]
        private readonly ConcurrentDictionary<Guid, CardImageCache> cache =
            new ConcurrentDictionary<Guid, CardImageCache>();

        /// <summary>
        /// The card repository.
        /// </summary>
        [NotNull]
        private readonly ICardRepository cardRepository;

        /// <summary>
        /// The file manager.
        /// </summary>
        [NotNull]
        private readonly ICardFileManager fileManager;

        /// <summary>
        /// The create editor func.
        /// </summary>
        [NotNull]
        private readonly Func<ICardEditorModel> createEditorFunc;

        /// <inheritdoc />
        public ImageCache(
            [NotNull] ICardRepository cardRepository,
            [NotNull] ICardFileManager fileManager,
            [NotNull] Func<ICardEditorModel> createEditorFunc)
        {
            this.cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            this.fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
            this.createEditorFunc = createEditorFunc ?? throw new ArgumentNullException(nameof(createEditorFunc));
        }

        /// <summary>
        /// Асинхронно получает и возвращает изображение из карточки
        ///     с идентификатором <paramref name="cardId"/> и именем файла <paramref name="fileName"/>
        /// </summary>
        /// <param name="cardId">
        /// Идентификатор карточки
        /// </param>
        /// <param name="fileName">
        /// Имя файла
        /// </param>
        /// <returns>
        /// Асинхронная задача получения изображения
        /// </returns>
        public async Task<BitmapImage> TryGetImageAsync(Guid cardId, [NotNull] string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(fileName));
            }

            if (!this.cache.TryGetValue(cardId, out CardImageCache imageCache))
            {
                CardImageCache loadedImageCache = await CreateImageCacheAsync(cardId);
                imageCache = this.cache.GetOrAdd(cardId, x => loadedImageCache);
            }

            return await imageCache.TryGetImageAsync(fileName);
        }

        /// <summary>
        /// Создает кеш изображений карточки с идентификатором <paramref name="cardId"/>
        /// </summary>
        /// <param name="cardId">
        /// Идентификатор карточки
        /// </param>
        /// <returns>
        /// Кеш изображений карточки
        /// </returns>z
        [NotNull]
        private async Task<CardImageCache> CreateImageCacheAsync(Guid cardId)
        {
            var request = new CardGetRequest { CardID = cardId, CompressionMode = CardCompressionMode.Full };
            CardGetResponse response = await this.cardRepository.GetAsync(request);

            var result = response.ValidationResult.Build();
            TessaDialog.ShowNotEmpty(result);

            if (!result.IsSuccessful)
            {
                throw new InvalidOperationException(result.ToString(ValidationLevel.Detailed));
            }

            Card card = response.Card;
            card.EnsureCacheResolved();

            return new CardImageCache(card, this.fileManager, this.createEditorFunc);
        }
    }
}