using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Files.VirtualFiles;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Caching
{
    /// <summary>
    /// Расширение, выполняющее сброс платформенных кэшей <see cref="DefaultCacheNames"/>
    /// в рамках запроса <see cref="CardRequestTypes.InvalidateCache"/>.
    /// </summary>
    public sealed class DefaultInvalidateCacheRequestExtension
        : CardRequestExtension
    {
        #region Constructors

        public DefaultInvalidateCacheRequestExtension(
            IKrProcessCache krProcessCache,
            IKrTypesCache krTypesCache,
            IKrVirtualFileCache krVirtualFileCache)
        {
            if (krProcessCache is null)
            {
                throw new ArgumentNullException(nameof(krProcessCache));
            }
            if (krTypesCache is null)
            {
                throw new ArgumentNullException(nameof(krTypesCache));
            }
            if (krVirtualFileCache is null)
            {
                throw new ArgumentNullException(nameof(krVirtualFileCache));
            }

            this.invalidateActions = new Dictionary<string, Func<ICardRequestExtensionContext, Task>>
            {
                { DefaultCacheNames.KrProcess, ctx =>
                    {
                        // кэш зависит от cardCache.Settings и не имеет своих полей, поэтому можно сбросить его целиком
                        return ctx.ShouldInvalidateCache(PlatformCacheNames.Cards)
                            ? Task.CompletedTask
                            : krProcessCache.InvalidateAsync(ctx.CancellationToken); }
                },

                { DefaultCacheNames.KrTypes, ctx =>
                    // кэш имеет поле "types", которое лучше сбросить отдельным вызовом
                    krTypesCache.InvalidateAsync(true, true, ctx.CancellationToken) },

                { DefaultCacheNames.KrVirtualFiles, ctx =>
                    // кэш зависит от cardCache.Settings и не имеет своих полей, поэтому можно сбросить его целиком
                    ctx.ShouldInvalidateCache(PlatformCacheNames.Cards)
                        ? Task.CompletedTask
                        : krVirtualFileCache.InvalidateAsync(ctx.CancellationToken) },
            };
        }

        #endregion

        #region Fields

        private readonly Dictionary<string, Func<ICardRequestExtensionContext, Task>> invalidateActions;

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardRequestExtensionContext context)
        {
            if (!context.RequestIsSuccessful
                || !context.ValidationResult.IsSuccessful())
            {
                return;
            }

            foreach (string cacheName in DefaultCacheNames.All)
            {
                if (context.ShouldInvalidateCache(cacheName)
                    && this.invalidateActions.TryGetValue(cacheName, out Func<ICardRequestExtensionContext, Task> actionAsync))
                {
                    await actionAsync(context);
                    context.AddInvalidateCompletedCacheNames(cacheName);
                }
            }
        }

        #endregion
    }
}