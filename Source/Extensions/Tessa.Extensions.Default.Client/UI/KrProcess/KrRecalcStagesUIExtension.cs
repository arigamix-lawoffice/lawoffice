using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.UI.KrProcess
{
    /// <summary>
    /// Расширение для модели представления карточки, добавляющее кнопку для пересчёта маршрута ("Пересчитать").
    /// </summary>
    public sealed class KrRecalcStagesUIExtension :
        CardUIExtension
    {
        #region Fields

        private readonly IKrTypesCache typesCache;

        #endregion

        #region Constructors

        public KrRecalcStagesUIExtension(IKrTypesCache typesCache) =>
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task Initialized(ICardUIExtensionContext context)
        {
            // выходим, если не включены процессы маршрутов
            var cardModel = context.Model;

            if (cardModel.Flags.HasAny(CardModelFlags.Disabled)
                || cardModel.Controls.TryGet(KrConstants.Ui.KrApprovalStagesControlAlias) is not GridViewModel approvalStagesTable
                || (await KrComponentsHelper.GetKrComponentsAsync(
                        cardModel.Card,
                        this.typesCache,
                        context.CancellationToken)).HasNot(KrComponents.Routes)
                || (KrToken.TryGet(context.Card.Info)?.HasPermission(KrPermissionFlagDescriptors.CanFullRecalcRoute)) != true)
            {
                return;
            }

            var uiContext = context.UIContext;

            approvalStagesTable.LeftButtons.Add(
                new UIButton("$CardTypes_Buttons_RecalcApprovalStages",
                    async _ =>
                    {
                        var editor = uiContext.CardEditor;
                        if (editor?.OperationInProgress != false)
                        {
                            return;
                        }

                        var info = new Dictionary<string, object>(StringComparer.Ordinal);
                        info.SetRecalcFlag();

                        await editor.SaveCardAsync(uiContext, info);
                    }));
        }

        #endregion
    }
}
