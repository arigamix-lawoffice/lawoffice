// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CardImageCache.cs" company="Syntellect">
//   Tessa Project
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Tessa.Extensions.Default.Client.Workplaces.Manager
{
    #region

    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;

    using Tessa.Cards;
    using Tessa.Files;
    using Tessa.Platform.Validation;
    using Tessa.Properties.Resharper;
    using Tessa.UI;
    using Tessa.UI.Cards;

    #endregion

    /// <summary>
    ///     Кеш изображений карточки
    /// </summary>
    public class CardImageCache
    {
        [NotNull]
        private readonly Card card;

        [NotNull]
        private readonly ICardFileManager fileManager;

        [NotNull]
        private readonly Func<ICardEditorModel> createEditorFunc;

        /// <summary>
        ///     The images.
        /// </summary>
        [NotNull]
        private readonly ConcurrentDictionary<string, BitmapImage> images =
            new ConcurrentDictionary<string, BitmapImage>(StringComparer.OrdinalIgnoreCase);

        /// <inheritdoc />
        public CardImageCache(
            [NotNull] Card card,
            [NotNull] ICardFileManager fileManager,
            [NotNull] Func<ICardEditorModel> createEditorFunc)
        {
            this.card = card ?? throw new ArgumentNullException(nameof(card));
            this.fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
            this.createEditorFunc = createEditorFunc ?? throw new ArgumentNullException(nameof(createEditorFunc));
        }

        /// <summary>
        /// Асинхронно получает файл из карточки с именем файла <paramref name="fileName"/>
        /// </summary>
        /// <param name="fileName">
        /// Имя файла
        /// </param>
        /// <returns>
        /// Асинхронная задача возвращающая изображение из файла с именем <paramref name="fileName"/>
        ///     из карточки содержащейся в кеше
        /// </returns>
        public async Task<BitmapImage> TryGetImageAsync([NotNull] string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(@"Value cannot be null or whitespace.", nameof(fileName));
            }

            if (this.images.TryGetValue(fileName, out BitmapImage image))
            {
                return image;
            }

            image = await this.LoadFileAsync(fileName);

            this.images.TryAdd(fileName, image);
            return image;
        }

        /// <summary>
        /// Осуществляет загрузку изображения из файла <paramref name="fileName"/>
        ///     содержащегося в карточке привязанной к
        /// </summary>
        /// <param name="fileName">
        /// Имя файла
        /// </param>
        /// <returns>
        /// Асинхронная задача получающая содержимое файла с именем <paramref name="fileName"/>
        ///     и преобразующая содержимое в изображение
        /// </returns>
        private async Task<BitmapImage> LoadFileAsync(string fileName)
        {
            byte[] bytes;
            await using (ICardFileContainer fileContainer = await this.fileManager.CreateContainerAsync(this.card))
            {
                IFile file = fileContainer.FileContainer.Files.FirstOrDefault(
                    x => string.Equals(x.Name, fileName, StringComparison.OrdinalIgnoreCase));

                if (file == null)
                {
                    return null;
                }

                IFileContent objContent = file.Content;

                if (!objContent.HasData)
                {
                    ICardEditorModel editor = this.createEditorFunc();
                    ICardModel model = await editor.CreateCardModelAsync(this.card, CardHelper.CreateSectionRows());
                    await editor.SetCardModelAsync(model);

                    // таким образом будет записан дайджест карточки при загрузке файлов
                    ValidationResult result;
                    await using (UIContext.Create(new UIContext(editor)))
                    {
                        result = await file.EnsureContentDownloadedAsync();
                    }

                    await TessaDialog.ShowNotEmptyAsync(result);

                    if (!result.IsSuccessful)
                    {
                        return null;
                    }
                }

                bytes = await file.ReadAllBytesAsync();
            }

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(bytes);
            image.EndInit();

            if (image.CanFreeze)
            {
                image.Freeze();
            }

            return image;
        }
    }
}