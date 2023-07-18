using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Requests
{
    /// <summary>
    /// Устанавливает права доступа на строки этапов.
    /// </summary>
    public sealed class KrStagePermissionsNewGetExtension :
        CardNewGetExtension
    {
        #region Fields

        private readonly IKrTypesCache typesCache;

        private readonly IKrProcessCache processCache;

        #endregion

        #region Constructors

        public KrStagePermissionsNewGetExtension(
            IKrTypesCache typesCache,
            IKrProcessCache processCache)
        {
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
            this.processCache = processCache ?? throw new ArgumentNullException(nameof(processCache));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterRequest(
            ICardNewExtensionContext context)
        {
            Card card;
            if (context.CardType is null
                || context.CardType.InstanceType != CardInstanceType.Card
                || context.CardType.Flags.Has(CardTypeFlags.Singleton)
                || !context.RequestIsSuccessful
                || (card = context.Response.TryGetCard()) is null
                || !await KrProcessHelper.CardSupportsRoutesAsync(
                    context.Response.Card,
                    context.DbScope,
                    this.typesCache,
                    context.CancellationToken))
            {
                return;
            }

            await this.SetStageRowsPermissionsAsync(
                card,
                context.CancellationToken);
        }

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            if (context.CardType is null
                || context.CardType.Flags.Has(CardTypeFlags.Singleton)
                || !context.RequestIsSuccessful
                || (card = context.Response.TryGetCard()) is null
                || !await KrProcessHelper.CardSupportsRoutesAsync(
                        context.Response.Card,
                        context.DbScope,
                        this.typesCache,
                        context.CancellationToken))
            {
                return;
            }

            await this.SetStageRowsPermissionsAsync(
                card,
                context.CancellationToken);
        }

        #endregion

        #region Private Methods

        private async ValueTask SetStageRowsPermissionsAsync(
            Card card,
            CancellationToken cancellationToken = default)
        {
            ListStorage<CardRow> rows;
            if (!card.TryGetStagesSection(out var stagesSection)
                || (rows = stagesSection.TryGetRows()) is null
                || rows.Count == 0)
            {
                return;
            }

            var stagesPermissions =
                card.Permissions.Sections.GetOrAddTable(KrConstants.KrStages.Virtual).Rows;
            var stageTemplates = await this.processCache.GetAllStageTemplatesAsync(cancellationToken);
            var stageGroups = await this.processCache.GetAllStageGroupsAsync(cancellationToken);

            foreach (var row in rows)
            {
                if (row.TryGet<bool>(KrConstants.Keys.RootStage)
                    || row.TryGet<bool>(KrConstants.Keys.NestedStage))
                {
                    // Для форка и его нестед строк все просто - они запрещены к редактированию в любом случае
                    var perm = stagesPermissions.GetOrAdd(row.RowID);
                    perm.SetRowPermissions(CardPermissionFlags.ProhibitDeleteRow);
                    perm.SetRowPermissions(CardPermissionFlags.ProhibitModify);

                    continue;
                }

                var basedOnStageTemplateID = row.TryGet<Guid?>(KrConstants.KrStages.BasedOnStageTemplateID);

                if (!basedOnStageTemplateID.HasValue
                    || !stageTemplates.TryGetValue(basedOnStageTemplateID.Value, out var stageTemplate)
                    || !stageGroups.TryGetValue(stageTemplate.StageGroupID, out var stageGroup))
                {
                    continue;
                }

                SetRowPermissions(
                    stagesPermissions.GetOrAdd(row.RowID),
                    row,
                    stageTemplate,
                    stageGroup);
            }
        }

        /// <summary>
        /// Устанавливает права на строку этапа.
        /// </summary>
        /// <param name="stageRowPermissionsInfo">Права доступа на отдельную строку коллекционной секции - этапа.</param>
        /// <param name="row">Строка этапа для которого выполняется настройка прав.</param>
        /// <param name="stageTemplate">Информация о шаблоне этапов.</param>
        /// <param name="stageGroup">Информация о группе этапов.</param>
        private static void SetRowPermissions(
            CardRowPermissionInfo stageRowPermissionsInfo,
            CardRow row,
            IKrStageTemplate stageTemplate,
            IKrStageGroup stageGroup)
        {
            var hasNotProhibitModifyStageRow = stageRowPermissionsInfo.RowPermissions.HasNot(CardPermissionFlags.ProhibitModify);

            if (hasNotProhibitModifyStageRow)
            {
                stageRowPermissionsInfo.SetRowPermissions(
                    stageRowPermissionsInfo.RowPermissions.Has(CardPermissionFlags.AllowDeleteRow)
                    && KrProcessSharedHelper.CanBeSkipped(row)
                    && !row.TryGet<bool>(KrConstants.KrStages.Skip)
                    ? CardPermissionFlags.AllowDeleteRow
                    : CardPermissionFlags.ProhibitDeleteRow);
            }

            stageRowPermissionsInfo.SetFieldPermissions(KrConstants.KrStages.NameField, CardPermissionFlags.ProhibitModify);

            if (stageGroup.IsGroupReadonly || !stageTemplate.CanChangeOrder)
            {
                stageRowPermissionsInfo.SetFieldPermissions(KrConstants.KrStages.Order, CardPermissionFlags.ProhibitModify);
            }

            if (hasNotProhibitModifyStageRow
                && (stageGroup.IsGroupReadonly || stageTemplate.IsStagesReadonly))
            {
                var fieldPermissions = stageRowPermissionsInfo.TryGetFieldPermissions();
                var prohibitChangeOrder = fieldPermissions is not null
                    && fieldPermissions.TryGetValue(KrConstants.KrStages.Order, out var orderPermissions)
                    && orderPermissions.Has(CardPermissionFlags.ProhibitModify);

                stageRowPermissionsInfo.SetRowPermissions(CardPermissionFlags.ProhibitModify);

                // Если до установки прав на всю строку не было запрета на смену порядка,
                // то запрет на смену порядка через запрет на изменение всей строки идет лесом
                if (!prohibitChangeOrder)
                {
                    stageRowPermissionsInfo.SetFieldPermissions(KrConstants.KrStages.Order, CardPermissionFlags.AllowModify);
                }
            }
        }

        #endregion
    }
}
