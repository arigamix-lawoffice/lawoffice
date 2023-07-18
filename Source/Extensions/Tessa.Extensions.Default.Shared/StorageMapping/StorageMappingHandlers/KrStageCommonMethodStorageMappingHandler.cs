using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.Platform.Storage.Mapping;

namespace Tessa.Extensions.Default.Shared.StorageMapping.StorageMappingHandlers
{
    /// <summary>
    /// Параметры сериализации для выгрузки контента карточек во внешние файлы
    /// для типа <see cref="DefaultCardTypes.KrStageCommonMethodTypeID"/>.
    /// </summary>
    public class KrStageCommonMethodStorageMappingHandler : IStorageMappingHandler
    {
        /// <inheritdoc />
        public IList<IStorageContentMapping> GetContentMappings(Card card) => new List<IStorageContentMapping>
        {
            new StorageContentMapping(
                "Sections." + KrConstants.KrStageCommonMethods.Name + ".Fields." +
                KrConstants.KrStageCommonMethods.Source,
                KrConstants.KrStageCommonMethods.Source + ".cs")
        };
    }
}