using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Requests
{
    /// <summary>
    /// Расширение на загрузку карточки, имеющей основной сателлит <see cref="DefaultCardTypes.KrSatelliteTypeID"/>.
    /// Переносит информацию из сателлита в соответствующие виртуальные секции.
    /// </summary>
    public sealed class KrCardGetExtension :
        CardGetExtension
    {
        #region Fields

        private readonly IKrTypesCache typesCache;
        private readonly IKrStageSerializer stageSerializer;
        private readonly IKrPermissionsManager permissionsManager;
        private readonly IKrScope krScope;

        #endregion

        #region Constructors

        public KrCardGetExtension(
            IKrTypesCache typesCache,
            IKrStageSerializer stageSerializer,
            IKrPermissionsManager permissionsManager,
            IKrScope krScope)
        {
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
            this.stageSerializer = stageSerializer ?? throw new ArgumentNullException(nameof(stageSerializer));
            this.permissionsManager = permissionsManager ?? throw new ArgumentNullException(nameof(permissionsManager));
            this.krScope = krScope ?? throw new ArgumentNullException(nameof(krScope));
        }

        #endregion

        #region Private Methods

        private Task FillSectionsAsync(
            Card main,
            Card satellite,
            KrProcessSerializerHiddenStageMode hiddenStageMode,
            ICardGetExtensionContext context)
        {
            new KrProcessSectionMapper(satellite, main)
                .Map(KrApprovalCommonInfo.Name, KrApprovalCommonInfo.Virtual, modifyAction: RemoveRedundantKeysAci)
                .Map(KrActiveTasks.Name, KrActiveTasks.Virtual)
                .Map(KrApprovalHistory.Name, KrApprovalHistory.Virtual)
                ;

            return this.stageSerializer.DeserializeSectionsAsync(
                satellite,
                main,
                hiddenStageMode: hiddenStageMode,
                cardContext: context,
                cancellationToken: context.CancellationToken);
        }

        private static void RemoveRedundantKeysAci(
            CardSection sec,
            IDictionary<string, object> storage)
        {
            storage.Remove(KrApprovalCommonInfo.Info);
            storage.Remove(KrApprovalCommonInfo.CurrentHistoryGroup);
            storage.Remove(KrProcessCommonInfo.NestedWorkflowProcesses);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            if (context.Request.IsKrSatelliteIgnored()
                || context.CardType is null
                || context.CardType.Flags.Has(CardTypeFlags.Singleton)
                || !context.ValidationResult.IsSuccessful()
                || (card = context.Response.TryGetCard()) is null)
            {
                return;
            }

            var components = await KrComponentsHelper.GetKrComponentsAsync(
                card,
                this.typesCache,
                context.CancellationToken);

            // Тип карточки не включён в типовое решение?
            if (components == KrComponents.None)
            {
                return;
            }

            if (components.Has(KrComponents.Routes))
            {
                KrProcessHelper.SetStageDefaultValues(context.Response);
            }

            var satellite = await this.krScope.TryGetKrSatelliteAsync(
                card.ID,
                context.ValidationResult,
                context.CancellationToken);

            if (satellite is null)
            {
                return;
            }

            KrProcessSerializerHiddenStageMode hiddenStageMode;

            if (context.Request.ConsiderSkippedStages())
            {
                var permContext = await this.permissionsManager.TryCreateContextAsync(
                    new KrPermissionsCreateContextParams
                    {
                        Card = context.Response.Card,
                        WithRequiredPermissions = false,
                        WithExtendedPermissions = false,
                        ValidationResult = context.ValidationResult,
                        AdditionalInfo = context.Info,
                        PrevToken = KrToken.TryGet(context.Request.Info),
                        ExtensionContext = context,
                        ServerToken = context.Info.TryGetServerToken(),
                    },
                    cancellationToken: context.CancellationToken);

                if (permContext is null)
                {
                    return;
                }

                hiddenStageMode =
                    await this.permissionsManager.CheckRequiredPermissionsAsync(
                        permContext,
                        KrPermissionFlagDescriptors.EditRoute)
                    ? KrProcessSerializerHiddenStageMode.ConsiderOnlySkippedStages
                    : KrProcessSerializerHiddenStageMode.Consider;
            }
            else
            {
                hiddenStageMode =
                    context.Request.ConsiderHiddenStages()
                    ? KrProcessSerializerHiddenStageMode.Consider
                    : KrProcessSerializerHiddenStageMode.Ignore;
            }

            await this.FillSectionsAsync(card, satellite, hiddenStageMode, context);
        }

        #endregion
    }
}
