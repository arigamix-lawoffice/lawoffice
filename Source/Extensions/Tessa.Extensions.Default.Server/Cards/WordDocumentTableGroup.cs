using DocumentFormat.OpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using Tessa.Platform;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class WordDocumentTableGroup : StorageObject
    {
        #region Fields

        private List<OpenXmlElement> baseElements;
        private List<IPlaceholder> tablePlaceholders;

        #endregion

        #region Constructors

        public WordDocumentTableGroup(Dictionary<string, object> storage = null)
            : base(storage ?? new Dictionary<string, object>(StringComparer.Ordinal))
        {
            this.Init(nameof(this.ID), string.Empty);
            this.Init(nameof(this.Name), string.Empty);
            this.Init(nameof(this.StartIndex), Int32Boxes.Zero);
            this.Init(nameof(this.EndIndex), Int32Boxes.Zero);
            this.Init(nameof(this.StartPosition), null);
            this.Init(nameof(this.EndPosition), null);
            this.Init(nameof(this.GroupType), 0);

            this.InnerTableGroups = new List<WordDocumentTableGroup>();
        }

        #endregion

        #region Storage Properties

        /// <summary>
        /// Идентификатор закладки в Word
        /// </summary>
        public string ID
        {
            get { return this.Get<string>(nameof(this.ID)); }
            set { this.Set(nameof(this.ID), value); }
        }

        /// <summary>
        /// Имя закладки в Word
        /// </summary>
        public string Name
        {
            get { return this.Get<string>(nameof(this.Name)); }
            set { this.Set(nameof(this.Name), value); }
        }

        /// <summary>
        /// Индекс метки начала закладки в тексте ее родительского элемента
        /// </summary>
        public int StartIndex
        {
            get { return this.Get<int>(nameof(this.StartIndex)); }
            set { this.Set(nameof(this.StartIndex), value); }
        }

        /// <summary>
        /// Индекс метки конца закладки в тексте ее родительского элемента
        /// </summary>
        public int EndIndex
        {
            get { return this.Get<int>(nameof(this.EndIndex)); }
            set { this.Set(nameof(this.EndIndex), value); }
        }

        /// <summary>
        /// Позиция метки начала закладки в структуре документа
        /// </summary>
        public IList StartPosition
        {
            get { return this.Get<IList>(nameof(this.StartPosition)); }
            set { this.Set(nameof(this.StartPosition), value); }
        }

        /// <summary>
        /// Позиция метки конца закладки в структуре документа
        /// </summary>
        public IList EndPosition
        {
            get { return this.Get<IList>(nameof(this.EndPosition)); }
            set { this.Set(nameof(this.EndPosition), value); }
        }

        /// <summary>
        /// Тип группы
        /// </summary>
        public WordDocumentTableGroupType GroupType
        {
            get { return (WordDocumentTableGroupType)this.Get<int>(nameof(this.GroupType)); }
            set { this.Set(nameof(this.GroupType), value.GetHashCode()); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Внутренняя таблица
        /// </summary>
        public List<WordDocumentTableGroup> InnerTableGroups { get; }

        /// <summary>
        /// Определяет, что вся закладка находится внутри параграфа
        /// </summary>
        public bool InParagraph { get; set; }

        /// <summary>
        /// Родительский элемент по отношению ко всем <see cref="BaseElements"/>
        /// </summary>
        public OpenXmlElement TableElement { get; set; }

        /// <summary>
        /// Позиция начала элемента таблицы
        /// </summary>
        public List<object> TableStartPosition { get; set; }

        /// <summary>
        /// Позиция окончания элемента таблицы
        /// </summary>
        public List<object> TableEndPosition { get; set; }

        /// <summary>
        /// Те элементы, что копируются, в которых потом производится замена плейсхолдеров
        /// </summary>
        public List<OpenXmlElement> BaseElements => this.baseElements ??= new List<OpenXmlElement>();

        /// <summary>
        /// Табличные плейсхолдеры
        /// </summary>
        public List<IPlaceholder> TablePlaceholders => this.tablePlaceholders ??= new List<IPlaceholder>();

        /// <summary>
        /// Определяет, подготовлена ли группа для обработки связей.
        /// </summary>
        public bool IsPrepared => this.TableElement != null && this.TableStartPosition != null && this.TableEndPosition != null;

        #endregion

        #region Public Methods

        public bool Contains(WordDocumentTableGroup info)
        {
            return
                (!info.InParagraph || !this.InParagraph || (this.StartIndex <= info.StartIndex && this.EndIndex >= info.EndIndex))
                && OpenXmlHelper.IsLessOrEquals(this.TableStartPosition, info.TableStartPosition)
                && OpenXmlHelper.IsLessOrEquals(info.TableEndPosition, this.TableEndPosition, true);
        }

        public bool Contains(IList position)
        {
            return
                OpenXmlHelper.IsLessOrEquals(this.TableStartPosition, position)
                && OpenXmlHelper.IsLessOrEquals(position, this.TableEndPosition, true);
        }

        public bool ContainsPlaceholder(IPlaceholder placeholder)
        {
            var placeholderIndex = placeholder.Info.Get<int>(OpenXmlHelper.IndexField);
            return placeholderIndex >= this.StartIndex && placeholderIndex < this.EndIndex;
        }

        #endregion
    }
}
