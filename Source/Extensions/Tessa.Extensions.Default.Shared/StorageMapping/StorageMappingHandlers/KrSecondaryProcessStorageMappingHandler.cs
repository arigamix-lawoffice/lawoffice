using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.StorageMapping.StorageMappingHandlers
{
    /// <summary>
    /// Параметры сериализации для выгрузки контента карточек во внешние файлы
    /// для типа <see cref="DefaultCardTypes.KrSecondaryProcessTypeID"/>.
    /// </summary>
    public class KrSecondaryProcessStorageMappingHandler : KrProcessStorageMappingHandlerBase
    {
        /// <inheritdoc />
        public override IList<IStorageContentMapping> GetContentMappings(Card card)
        {
            var pathMappings = base.GetContentMappings(card);

            pathMappings.Add(
                new StorageContentMapping(
                    "Sections." + KrConstants.KrSecondaryProcesses.Name + ".Fields." + KrConstants.KrSecondaryProcesses.ExecutionSourceCondition,
                    KrConstants.KrSecondaryProcesses.ExecutionSourceCondition + ".cs"));
            pathMappings.Add(
                new StorageContentMapping(
                    "Sections." + KrConstants.KrSecondaryProcesses.Name + ".Fields." + KrConstants.KrSecondaryProcesses.ExecutionSqlCondition,
                    KrConstants.KrSecondaryProcesses.ExecutionSqlCondition + ".sql"));
            pathMappings.Add(
                new StorageContentMapping(
                    "Sections." + KrConstants.KrSecondaryProcesses.Name + ".Fields." + KrConstants.KrSecondaryProcesses.VisibilitySourceCondition,
                    KrConstants.KrSecondaryProcesses.VisibilitySourceCondition + ".cs"));
            pathMappings.Add(
                new StorageContentMapping(
                    "Sections." + KrConstants.KrSecondaryProcesses.Name + ".Fields." + KrConstants.KrSecondaryProcesses.VisibilitySqlCondition,
                    KrConstants.KrSecondaryProcesses.VisibilitySqlCondition + ".sql"));

            return pathMappings;
        }
    }
}