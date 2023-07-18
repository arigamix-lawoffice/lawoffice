using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Collections;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Расширение прав доступа, которое добавляет в список запрашиваемых прав доступа все права, если состояние карточки <see cref="KrState.Draft"/>.
    /// </summary>
    public sealed class KrRequestCalculateFullCardPermissionExtension : ICardPermissionsExtension
    {
        ///<inheritdoc/>
        public Task ExtendPermissionsAsync(IKrPermissionsManagerContext context)
        {
            if (context.Mode == KrPermissionsCheckMode.WithCard
                && context.Method == nameof(IKrPermissionsManager.GetEffectivePermissionsAsync)
                && context.DocState == KrState.Draft)
            {
                context.Card.Info[KrPermissionsHelper.PermissionsCalculatedMark] = true;

                // Добавляем в список запрашиваемых прав доступа все права доступа
                context.Descriptor.StillRequired.AddRange(
                    KrPermissionFlagDescriptors.FullCardPermissionsGroup.IncludedPermissions);

                // Убираем из списка уже добавленные права доступа
                context.Descriptor.StillRequired.ExceptWith(context.Descriptor.Permissions);
            }
            return Task.CompletedTask;
        }

        ///<inheritdoc/>
        public Task IsPermissionsRecalcRequired(IKrPermissionsRecalcContext context)
        {
            return Task.CompletedTask;
        }
    }
}
