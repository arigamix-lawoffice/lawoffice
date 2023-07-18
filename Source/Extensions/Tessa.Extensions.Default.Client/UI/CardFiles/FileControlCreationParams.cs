using System.Collections;

namespace Tessa.Extensions.Default.Client.UI.CardFiles
{
    public sealed class FileControlCreationParams
    {
        /// <summary>
        /// Алиас представления с категорями
        /// </summary>
        public string CategoriesViewAlias { get; set; } = "FileCategoriesFiltered";

        /// <summary>
        /// Алиас контрола предпросмотра в карточке
        /// </summary>
        public string PreviewControlName { get; set; }

        /// <summary>
        /// Признак того, что разрешено использование категорий. В этом случае при добавлении файла пользователь может выбрать его категорию
        /// </summary>
        public bool IsCategoriesEnabled { get; set; }

        /// <summary>
        /// Признак того, что при добавлении файла пользователю запрещается вводить имя категории вручную. Настройка имеет смысл только в том случае, если использование категорий разрешено
        /// </summary>
        public bool IsManualCategoriesCreationDisabled { get; set; }

        /// <summary>
        /// Признак того, что при добавлении файла пользователю запрещается выбирать "без категории". Настройка имеет смысл только в том случае, если использование категорий разрешено
        /// </summary>
        public bool IsNullCategoryCreationDisabled { get; set; }

        /// <summary>
        /// Признак того, что существующие в карточке категории файлов по умолчанию недоступны для выбора. Настройка имеет смысл только в том случае, если использование категорий разрешено
        /// </summary>
        public bool IsIgnoreExistingCategories { get; set; }

        /// <summary>
        /// Настройки маппиинга представления категорий
        /// </summary>
        public IList CategoriesViewMapping { get; set; } 
    }
}
