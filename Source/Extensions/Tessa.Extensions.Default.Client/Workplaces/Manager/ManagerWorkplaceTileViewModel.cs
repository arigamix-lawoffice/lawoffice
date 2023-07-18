// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagerWorkplaceTileViewModel.cs" company="Syntellect">
//   Tessa Project
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Tessa.Platform.Formatting;

namespace Tessa.Extensions.Default.Client.Workplaces.Manager
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using Tessa.Platform;
    using Tessa.Properties.Resharper;
    using Tessa.UI;
    using Tessa.Views.Metadata;
    using Tessa.Extensions.Default.Shared.Workplaces;

    #endregion

    /// <summary>
    ///     Модель-представление фильтра
    /// </summary>
    public sealed class ManagerWorkplaceTileViewModel : SelectableViewModel<IDictionary<string, object>>
    {
        /// <summary>
        ///     The active image.
        /// </summary>
        private ImageSource activeImage;

        /// <summary>
        ///     The caption.
        /// </summary>
        private string caption;

        /// <summary>
        ///     The count.
        /// </summary>
        private string count;

        /// <summary>
        /// The hover image.
        /// </summary>
        private ImageSource hoverImage;

        /// <summary>
        ///     The inactive image.
        /// </summary>
        private ImageSource inactiveImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerWorkplaceTileViewModel"/> class.
        /// </summary>
        /// <param name="row">
        /// Модель плитки
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// row is null
        /// </exception>
        public ManagerWorkplaceTileViewModel([NotNull] IDictionary<string, object> row)
            : base(row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }
        }

        /// <summary>
        ///     Gets or sets Изображение активного элемента
        /// </summary>
        public ImageSource ActiveImage
        {
            get
            {
                return this.activeImage;
            }
            set
            {
                if (!ReferenceEquals(value, this.activeImage))
                {
                    this.activeImage = value;
                    this.OnPropertyChanged(nameof(this.ActiveImage));
                }
            }
        }

        /// <summary>
        ///     Gets or sets Заголовок плитки
        /// </summary>
        [CanBeNull]
        public string Caption
        {
            get
            {
                return this.caption;
            }

            set
            {
                if (value != this.caption)
                {
                    this.caption = value;
                    this.OnPropertyChanged(nameof(this.Caption));
                }
            }
        }

        /// <summary>
        ///     Gets or sets Количество элементов
        /// </summary>
        public string Count
        {
            get
            {
                return this.count;
            }

            set
            {
                if (value != this.count)
                {
                    this.count = value;
                    this.OnPropertyChanged(nameof(this.Count));
                }
            }
        }

        /// <summary>
        ///     Gets or sets Изображение если курсор находится над плиткой
        /// </summary>
        public ImageSource HoverImage
        {
            get
            {
                return this.hoverImage;
            }

            set
            {
                if (!ReferenceEquals(value, this.hoverImage))
                {
                    this.hoverImage = value;
                    this.OnPropertyChanged(nameof(this.HoverImage));
                }
            }
        }

        /// <summary>
        ///     Gets or sets Изображение не активного элемента
        /// </summary>
        [CanBeNull]
        public ImageSource InactiveImage
        {
            get
            {
                return this.inactiveImage;
            }

            set
            {
                if (!ReferenceEquals(value, this.inactiveImage))
                {
                    this.inactiveImage = value;
                    this.OnPropertyChanged(nameof(this.InactiveImage));
                }
            }
        }

        /// <summary>
        /// Создает модель-представление плитки
        /// </summary>
        /// <param name="row">
        /// Строка представления привязанная к плитке
        /// </param>
        /// <param name="viewMetadata">
        /// Метаданные представления
        /// </param>
        /// <param name="settings">
        /// Настройки
        /// </param>
        /// <param name="imageCache">
        /// Кеш изображений
        /// </param>
        /// <returns>
        /// Модель-представление плитки
        /// </returns>
        public static async Task<ManagerWorkplaceTileViewModel> Create(
            [NotNull] IDictionary<string, object> row,
            [NotNull] IViewMetadata viewMetadata,
            [NotNull] ManagerWorkplaceSettings settings,
            [NotNull] ImageCache imageCache)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            if (viewMetadata == null)
            {
                throw new ArgumentNullException("viewMetadata");
            }

            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            if (imageCache == null)
            {
                throw new ArgumentNullException(nameof(imageCache));
            }

            var result = new ManagerWorkplaceTileViewModel(row);
            var captionColumn = settings.TileColumnName;
            if (!string.IsNullOrWhiteSpace(captionColumn))
            {
                result.Caption = FormattingHelper.FormatToString(
                    TryGetValue(row, captionColumn),
                    RequireLocalization(viewMetadata, captionColumn));
            }

            var countColumn = settings.CountColumnName;
            result.Count = !string.IsNullOrWhiteSpace(countColumn)
                               ? FormattingHelper.FormatToString(TryGetValue(row, countColumn) ?? 0)
                               : "0";

            Task<BitmapImage>[] imageTasks = new[]
                {
                    settings.ActiveImageColumnName,
                    settings.InactiveImageColumnName,
                    settings.HoverImageColumnName,
                }
                .Select(columnName =>
                    imageCache.TryGetImageAsync(
                        settings.CardId,
                        (string)GetValueOrThrow(row, columnName)))
                .ToArray();

            BitmapImage[] images = await Task.WhenAll(imageTasks);

            result.ActiveImage = images[0];
            result.InactiveImage = images[1];
            result.HoverImage = images[2];

            return result;
        }

        /// <summary>
        /// Проверяет требуется ли производить локализацию значения столбца <paramref name="columnName"/>
        /// </summary>
        /// <param name="metadata">
        /// Метаданные представления
        /// </param>
        /// <param name="columnName">
        /// Имя столбца
        /// </param>
        /// <returns>
        /// Результат проверки
        /// </returns>
        private static bool RequireLocalization([NotNull] IViewMetadata metadata, [NotNull] string columnName)
        {
            if (metadata == null)
            {
                throw new ArgumentNullException("metadata");
            }

            if (columnName == null)
            {
                throw new ArgumentNullException("columnName");
            }

            var columnMetadata = metadata.Columns.FindByName(columnName);
            return columnMetadata != null && columnMetadata.Localizable;
        }

        /// <summary>
        /// Возвращает или значение столбца с именем <paramref name="columnName"/> или <c>null</c>
        ///     если столбец отсутствует в стороке привязанной к данному элементу
        /// </summary>
        /// <param name="row">
        /// Строка данных
        /// </param>
        /// <param name="columnName">
        /// Имя столбца
        /// </param>
        /// <returns>
        /// Значение столбца или <c>null</c>
        /// </returns>
        [CanBeNull]
        private static object TryGetValue([NotNull] IDictionary<string, object> row, [NotNull] string columnName)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            if (columnName == null)
            {
                throw new ArgumentNullException("columnName");
            }

            return row.TryGetValue(columnName, out object result) ? result : null;
        }

        [NotNull]
        private static object GetValueOrThrow([NotNull] IDictionary<string, object> row, [NotNull] string columnName)
        {
            object value = TryGetValue(row, columnName);
            if (value == null)
            {
                throw new InvalidOperationException($"Invalid NULL value in column '{columnName}'.");
            }

            return value;
        }
    }
}