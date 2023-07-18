using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Metadata;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow
{
    /// <summary>
    /// Абстрактный базовый класс предоставляющий методы для обхода дочерних секций карточки.
    /// </summary>
    public abstract class DescendantSectionsVisitor
    {
        #region Fields

        protected readonly ICardMetadata CardMetadata;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DescendantSectionsVisitor"/>.
        /// </summary>
        /// <param name="cardMetadata">Метаинформация по карточкам.</param>
        protected DescendantSectionsVisitor(
            ICardMetadata cardMetadata)
        {
            Check.ArgumentNotNull(cardMetadata, nameof(cardMetadata));

            this.CardMetadata = cardMetadata;
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Выполняет посещение строки секции верхнего уровня.
        /// </summary>
        /// <param name="row">Посещаемая строка.</param>
        /// <param name="section">Посещаемая секция.</param>
        /// <param name="stageMapping">Коллекция &lt;ключ-значение&gt;, содержащая соответствие &lt;идентификатор текущей строки - идентификатор строки верхнего уровня&gt;.</param>
        protected abstract void VisitTopLevelSection(
            CardRow row,
            CardSection section,
            IDictionary<Guid, Guid> stageMapping);

        /// <summary>
        /// Выполняет посещение строки дочерней секции по отношению к секции верхнего уровня.
        /// </summary>
        /// <param name="row">Посещаемая строка.</param>
        /// <param name="section">Посещаемая секция.</param>
        /// <param name="parentRowID">Идентификатор родительской строки.</param>
        /// <param name="topLevelRowID">Идентификатор строки верхнего уровня.</param>
        /// <param name="stageMapping">Коллекция &lt;ключ-значение&gt;, содержащая соответствие &lt;идентификатор текущей строки - идентификатор строки верхнего уровня&gt;.</param>
        protected abstract void VisitNestedSection(
            CardRow row,
            CardSection section,
            Guid parentRowID,
            Guid topLevelRowID,
            IDictionary<Guid, Guid> stageMapping);

        #endregion

        #region Public Methods

        /// <summary>
        /// Выполняет обход всех коллекционных секций, для которых предком является строка из секции <paramref name="topLevelSectionName"/>.
        /// </summary>
        /// <param name="cardSections">Секции карточки.</param>
        /// <param name="typeID">Идентификатор типа карточки.</param>
        /// <param name="topLevelSectionName">Имя секции с которой начинается обход.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Асинхронная задача.</returns>
        public async ValueTask VisitAsync(
            StringDictionaryStorage<CardSection> cardSections,
            Guid typeID,
            string topLevelSectionName,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(cardSections, nameof(cardSections));

            var cardTypeMetadata = await this.CardMetadata.GetMetadataForTypeAsync(typeID, cancellationToken);
            var cardType = (await cardTypeMetadata.GetCardTypesAsync(cancellationToken))[typeID];
            var schemeItems = new HashSet<Guid, CardTypeSchemeItem>(
                static p => p.SectionID,
                cardType.SchemeItems);

            var topLevelSecMetadata = (await this.CardMetadata.GetSectionsAsync(cancellationToken))[topLevelSectionName];
            // Связь RowID строки в подчиненной коллекционной секции произвольной вложенности
            // <->
            // RowID строки верхнего уровня.
            var stagesMapping = new Dictionary<Guid, Guid>();
            var topLevelSection = cardSections[topLevelSectionName];
            foreach (var topLevelRow in topLevelSection.Rows)
            {
                stagesMapping[topLevelRow.RowID] = topLevelRow.RowID;
                this.VisitTopLevelSection(topLevelRow, topLevelSection, stagesMapping);
            }

            var previousLayer = new HashSet<Guid> { topLevelSecMetadata.ID };
            var currentLayer = new HashSet<Guid>();

            // Обход зависимостей проводится в "ширину"
            // Вершиной является переданная через параметр секция
            // В первый слой входят все секции, имеющие столбец с указанием на родителя, т.е. на вершину
            // Вторым слоем будут все секции, у которых "ссылка на родителя" указывает на секции первого слоя
            // и т.д. до тех пор, пока очередной слой не станет пустым.
            while (previousLayer.Count != 0)
            {
                foreach (var secMetadata in await cardTypeMetadata.GetSectionsAsync(cancellationToken))
                {
                    // Секция не используется в карточке, а значит в обработке не участвует.
                    if (!schemeItems.TryGetItem(secMetadata.ID, out var schemeItem))
                    {
                        continue;
                    }

                    // Получаем комплексный столбец с ссылкой на родителя.
                    var refSecTuple = GetParentColumnSec(secMetadata, previousLayer);
                    var parentComplexColumn = refSecTuple.Item1;
                    var parentRowIDColumn = refSecTuple.Item2;
                    if (parentComplexColumn is null
                        || parentRowIDColumn is null)
                    {
                        continue;
                    }

                    // Комплексный столбец используется в карточке.
                    if (!schemeItem.ColumnIDList.Contains(parentComplexColumn.ID))
                    {
                        continue;
                    }

                    ListStorage<CardRow> rows;
                    if (cardSections.TryGetValue(secMetadata.Name, out var section)
                        && (rows = section.TryGetRows()) is not null
                        && rows.Count != 0)
                    {
                        currentLayer.Add(secMetadata.ID);
                        // Проставляем каждой строке ссылку на этап, ориентируясь по
                        // ссылке на непосредственного родителя.
                        foreach (var row in rows)
                        {
                            if (row.TryGetValue(parentRowIDColumn.Name, out var parentIDObj)
                                && parentIDObj is Guid parentID
                                && stagesMapping.TryGetValue(parentID, out var topLevelRowID))
                            {
                                stagesMapping[row.RowID] = topLevelRowID;
                                this.VisitNestedSection(row, section, parentID, topLevelRowID, stagesMapping);
                            }
                        }
                    }
                }
                SwapLayers(ref previousLayer, ref currentLayer);
            }
        }

        #endregion

        #region Private Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Tuple<CardMetadataColumn, CardMetadataColumn> GetParentColumnSec(
            CardMetadataSection secMetadata,
            HashSet<Guid> previousLayer)
        {
            CardMetadataColumn complex = null;
            CardMetadataColumn rowID = null;
            foreach (var column in secMetadata.Columns)
            {
                if (column.ParentRowSection is not null
                    && column.ColumnType == CardMetadataColumnType.Complex
                    && previousLayer.Contains(column.ParentRowSection.ID))
                {
                    complex = column;
                }
                else if (complex is not null
                    && column.ColumnType == CardMetadataColumnType.Physical
                    && column.ParentRowSection?.ID == complex.ParentRowSection.ID
                    && column.ComplexColumnIndex == complex.ComplexColumnIndex)
                {
                    rowID = column;
                    break;
                }
            }

            return new Tuple<CardMetadataColumn, CardMetadataColumn>(complex, rowID);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SwapLayers(ref HashSet<Guid> previousLayer, ref HashSet<Guid> currentLayer)
        {
            var tmp = previousLayer;
            tmp.Clear();
            previousLayer = currentLayer;
            currentLayer = tmp;
        }

        #endregion

    }
}
