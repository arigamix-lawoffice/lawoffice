using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Platform.Storage;
using Tessa.Platform.Storage.Mapping;

namespace Tessa.Extensions.Default.Shared.StorageMapping.StorageMappingHandlers
{
    /// <summary>
    /// Параметры сериализации для выгрузки контента карточек во внешние файлы
    /// для типа <see cref="DefaultCardTypes.KrStageGroupTypeID"/>.
    /// </summary>
    public class KrStageGroupStorageMappingHandler : IStorageMappingHandler
    {
        /// <inheritdoc />
        public IList<IStorageContentMapping> GetContentMappings(Card card) => new List<IStorageContentMapping>
        {
            new StorageContentMapping(
                "Sections.KrStageGroups.Fields.RuntimeSourceAfter",
                "RuntimeSourceAfter.cs"),
            new StorageContentMapping(
                "Sections.KrStageGroups.Fields.RuntimeSourceBefore",
                "RuntimeSourceBefore.cs"),
            new StorageContentMapping(
                "Sections.KrStageGroups.Fields.RuntimeSourceCondition",
                "RuntimeSourceCondition.cs"),
            new StorageContentMapping(
                "Sections.KrStageGroups.Fields.RuntimeSqlCondition",
                "RuntimeSqlCondition.sql"),
            new StorageContentMapping(
                "Sections.KrStageGroups.Fields.SourceAfter",
                "SourceAfter.cs"),
            new StorageContentMapping(
                "Sections.KrStageGroups.Fields.SourceBefore",
                "SourceBefore.cs"),
            new StorageContentMapping(
                "Sections.KrStageGroups.Fields.SourceCondition",
                "SourceCondition.cs"),
            new StorageContentMapping(
                "Sections.KrStageGroups.Fields.SqlCondition",
                "SqlCondition.sql")
        };
    }
}