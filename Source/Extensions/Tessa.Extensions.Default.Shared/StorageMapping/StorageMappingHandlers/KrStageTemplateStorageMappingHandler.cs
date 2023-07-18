using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.StorageMapping.StorageMappingHandlers
{
    /// <summary>
    /// Параметры сериализации для выгрузки контента карточек во внешние файлы
    /// для типа <see cref="DefaultCardTypes.KrStageTemplateTypeID"/>.
    /// </summary>
    public class KrStageTemplateStorageMappingHandler : KrProcessStorageMappingHandlerBase
    {
        /// <inheritdoc />
        public override IList<IStorageContentMapping> GetContentMappings(Card card)
        {
            var pathMappings = base.GetContentMappings(card);

            pathMappings.Add(
                new StorageContentMapping(
                    "Sections." + KrConstants.KrStageTemplates.Name + ".Fields." + KrConstants.KrStageTemplates.SourceBefore,
                    KrConstants.KrStageTemplates.SourceBefore + ".cs"));
            pathMappings.Add(
                new StorageContentMapping(
                    "Sections." + KrConstants.KrStageTemplates.Name + ".Fields." + KrConstants.KrStageTemplates.SourceAfter,
                    KrConstants.KrStageTemplates.SourceAfter + ".cs"));
            pathMappings.Add(
                new StorageContentMapping(
                    "Sections." + KrConstants.KrStageTemplates.Name + ".Fields." + KrConstants.KrStageTemplates.SqlCondition,
                    KrConstants.KrStageTemplates.SqlCondition + ".sql"));
            pathMappings.Add(
                new StorageContentMapping(
                    "Sections." + KrConstants.KrStageTemplates.Name + ".Fields." + KrConstants.KrStageTemplates.SourceCondition,
                    KrConstants.KrStageTemplates.SourceCondition + ".cs"));

            return pathMappings;
        }
    }
}