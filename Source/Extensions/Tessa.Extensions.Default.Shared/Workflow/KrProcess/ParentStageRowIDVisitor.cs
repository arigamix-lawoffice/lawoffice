using System;
using System.Collections.Generic;
using Tessa.Cards;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Объект, устанавливающий значение поля <see cref="KrConstants.Keys.ParentStageRowID"/> равным идентификатору строки секции верхнего уровня.
    /// </summary>
    public class ParentStageRowIDVisitor :
        DescendantSectionsVisitor
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ParentStageRowIDVisitor"/>.
        /// </summary>
        /// <param name="cardMetadata">Метаинформация по карточкам.</param>
        public ParentStageRowIDVisitor(
            ICardMetadata cardMetadata)
            : base(cardMetadata)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void VisitTopLevelSection(
            CardRow row,
            CardSection section,
            IDictionary<Guid, Guid> stageMapping) =>
            row[KrConstants.Keys.ParentStageRowID] = row.RowID;

        /// <inheritdoc />
        protected override void VisitNestedSection(
            CardRow row,
            CardSection section,
            Guid parentRowID,
            Guid topLevelRowID,
            IDictionary<Guid, Guid> stageMapping) =>
            row[KrConstants.Keys.ParentStageRowID] = topLevelRowID;

        #endregion
    }
}
