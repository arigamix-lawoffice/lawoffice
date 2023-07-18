using System;
using Tessa.Platform;
using Tessa.Platform.Storage.Mapping;

namespace Tessa.Extensions.Default.Shared.StorageMapping.StorageMappingHandlers
{
    public static class StorageMappingResolverExtensions
    {
        /// <summary>
        /// Выполняет регистрацию типов для <see cref="IStorageMappingResolver"/>.
        /// </summary>
        /// <param name="storageMappingResolver"><see cref="IStorageMappingResolver"/>.</param>
        /// <returns>Объект <paramref name="storageMappingResolver"/> для цепочки вызовов.</returns>
        public static IResolver<Guid, IStorageMappingHandler> RegisterKrStorageMappingHandlers(
            this IStorageMappingResolver storageMappingResolver)
        {
            Check.ArgumentNotNull(storageMappingResolver, nameof(storageMappingResolver));

            return storageMappingResolver
                    .Register<KrSecondaryProcessStorageMappingHandler>(DefaultCardTypes.KrSecondaryProcessTypeID)
                    .Register<KrStageTemplateStorageMappingHandler>(DefaultCardTypes.KrStageTemplateTypeID)
                    .Register<KrStageCommonMethodStorageMappingHandler>(DefaultCardTypes.KrStageCommonMethodTypeID)
                    .Register<KrStageGroupStorageMappingHandler>(DefaultCardTypes.KrStageGroupTypeID)
                ;
        }
    }
}