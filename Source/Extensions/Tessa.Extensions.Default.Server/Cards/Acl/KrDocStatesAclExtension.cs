#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions.Templates;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Roles.Acl;
using Tessa.Roles.Acl.Extensions;
using Tessa.Roles.Queries;
using Tessa.Roles.Queries.Parts;
using Tessa.Roles.Triggers;

namespace Tessa.Extensions.Default.Server.Cards.Acl
{
    /// <summary>
    /// Расширение, дополнительно проверяющее состояние карточки при расчёте ACL.
    /// </summary>
    public sealed class KrDocStatesAclExtension : AclGenerationRuleExtensionBase
    {
        #region Fields

        private const string statesKey = CardHelper.SystemKeyPrefix + "states";

        private readonly IDbScope dbScope;

        #endregion

        #region Constructors

        public KrDocStatesAclExtension(IDbScope dbScope)
        {
            this.dbScope = NotNullOrThrow(dbScope);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override AclGenerationRuleExtensionDescriptor Descriptor => AclExtensionDescriptors.KrDocStates;

        /// <inheritdoc/>
        public override async ValueTask ModifyGenerationRuleDataAsync(
            AclGenerationRuleDataSource ruleData,
            CancellationToken cancellationToken = default)
        {
            if (!ruleData.ExtensionsData.TryGetValue("AclExtensions_States", out var statesDataObj)
                || statesDataObj is not Dictionary<string, object?> statesData)
            {
                return;
            }
            var statesSection = new CardSection("AclExtensions_States", statesData);
            var states = statesSection.Rows.Select(x => x.TryGet<int>("StateID")).ToArray();
            if (states.Length == 0)
            {
                return;
            }
            ruleData.ExtensionsData[statesKey] = states;
            ruleData.TriggerOnTypes.Add(DefaultCardTypes.KrSatelliteTypeID);
        }

        /// <inheritdoc/>
        public override ValueTask ModifyGetCardsQueryAsync(
            IComplexQueryBuilder queryBuilder,
            AclGenerationRuleDataSource ruleData,
            CancellationToken cancellationToken = default)
        {
            ThrowIfNull(queryBuilder);
            ThrowIfNull(ruleData);

            if (!ruleData.ExtensionsData.TryGetValue(statesKey, out var statesObj)
                || statesObj is not int[] states)
            {
                return ValueTask.CompletedTask;
            }

            queryBuilder.AddTableJoin(
                nameof(KrDocStatesAclExtension),
                KrConstants.KrApprovalCommonInfo.Name,
                KrConstants.KrApprovalCommonInfo.MainCardID,
                true,
                new FuncQueryPart((builder, dataParameters) =>
                {
                    builder.Coalesce(b => b.C(nameof(KrDocStatesAclExtension), KrConstants.KrApprovalCommonInfo.StateID).V(KrState.Draft.ID)).InArray(states, "States", out var parameter);
                    dataParameters.Add(parameter);
                }),
                new FuncQueryPart((builder, _) => builder.Coalesce(b => b.C(nameof(KrDocStatesAclExtension), KrConstants.KrApprovalCommonInfo.StateID).V(KrState.Draft.ID))));

            return ValueTask.CompletedTask;
        }

        /// <inheritdoc/>
        public override async ValueTask ModifyGetUpdateCardsByTriggersAsync(
            CheckTriggersResult result,
            CheckTriggersRequest request,
            Dictionary<string, object?> extensionsData,
            CancellationToken cancellationToken = default)
        {
            if (request.TriggerCard is not null
                && request.TriggerCard.TypeID == DefaultCardTypes.KrSatelliteTypeID
                && request.TriggerCard.Sections.TryGetValue("KrApprovalCommonInfo", out var krSection)
                && krSection.RawFields.TryGetValue("StateID", out var stateObj)
                && stateObj is int stateID)
            {
                var mainCardID = await CardSatelliteHelper.TryGetMainCardIDFromSatelliteIDAsync(
                    this.dbScope,
                    request.TriggerCard.ID,
                    "KrApprovalCommonInfo",
                    "MainCardID",
                    cancellationToken);

                if (mainCardID.HasValue)
                {
                    result.CardIDsWithData[mainCardID.Value] = new Dictionary<string, object?> { ["StateID"] = stateID };
                }
            }
        }

        /// <inheritdoc/>
        public override async ValueTask<bool> AllowedForCardAsync(
            Guid cardID,
            Dictionary<string, object?>? additionalData,
            Dictionary<string, object?> extensionsData,
            CancellationToken cancellationToken = default)
        {
            if (!extensionsData.TryGetValue(statesKey, out var statesObj)
                || statesObj is not int[] states)
            {
                return true;
            }

            int stateID;
            if (additionalData is not null
                && additionalData.TryGetValue("StateID", out var stateIDObj))
            {
                stateID = stateIDObj is null
                    ? KrState.Draft.ID
                    : (int) stateIDObj;
            }
            else
            {
                await using var s = this.dbScope.Create();
                var db = this.dbScope.Db;
                var builder = this.dbScope.BuilderFactory;

                stateID = await db.SetCommand(
                    builder
                        .Select().C("StateID")
                        .From("KrApprovalCommonInfo").NoLock()
                        .Where().C("MainCardID").Equals().P("CardID")
                        .Build(),
                    db.Parameter("CardID", cardID))
                    .LogCommand()
                    .ExecuteAsync<int?>(cancellationToken) ?? KrState.Draft.ID;
            }

            return states.Contains(stateID);
        }

        #endregion
    }
}
