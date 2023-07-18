#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Roles.Acl;
using Tessa.Roles.Triggers;

namespace Tessa.Extensions.Default.Server.Acl
{
    /// <summary>
    /// Обработчик стандартных правил расчёта ACL, который учитывает обработку типов документов.
    /// </summary>
    public class KrAclGenerationRuleExecutor : AclGenerationRuleExecutor
    {
        #region Fields

        private readonly IKrTypesCache krTypesCache;

        #endregion

        #region Constructors

        public KrAclGenerationRuleExecutor(
            IDbScope dbScope,
            IUpdateTriggersExecutor triggersExecutor,
            ICardGetStrategy cardGetStrategy,
            IKrTypesCache krTypesCache)
            : base(dbScope, triggersExecutor, cardGetStrategy)
        {
            this.krTypesCache = NotNullOrThrow(krTypesCache);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async ValueTask<bool> AllowedForCardCoreAsync(
            IAclGenerationRuleData rule,
            Guid cardID,
            Dictionary<string, object?>? additionalData,
            CancellationToken cancellationToken = default)
        {
            if (rule is not KrAclGenerationRuleData krRule)
            {
                return await base.AllowedForCardCoreAsync(
                    rule,
                    cardID,
                    additionalData,
                    cancellationToken);
            }

            bool result;
            Guid? typeID = null;
            var docTypes = await this.krTypesCache.GetDocTypesAsync(cancellationToken);
            if (additionalData is not null
                && additionalData.TryGetValue(AclHelper.TypeIDKey, out var typeIDObj))
            {
                typeID = typeIDObj as Guid?;
            }

            if (typeID is not null)
            {
                if (docTypes.TryFirst(x => x.ID == typeID.Value, out var docType))
                {
                    result = krRule.DocTypes?.Contains(docType.ID) == true
                        || krRule.CardTypes?.Contains(docType.CardTypeID) == true;
                }
                else
                {
                    result = krRule.CardTypes?.Contains(typeID.Value) == true;
                }
            }
            else
            {
                var docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(cardID, this.DbScope, cancellationToken);
                if (docTypeID.HasValue
                    && docTypes.TryFirst(x => x.ID == docTypeID.Value, out var docType))
                {
                    result = krRule.DocTypes?.Contains(docType.ID) == true
                        || krRule.CardTypes?.Contains(docType.CardTypeID) == true;
                    
                    if (additionalData is not null)
                    {
                        additionalData[AclHelper.TypeIDKey] = docTypeID;
                    }
                }
                else
                {
                    var cardTypeID = await this.CardGetStrategy.GetTypeIDAsync(cardID, cancellationToken: cancellationToken);

                    result = cardTypeID is not null
                        && krRule.CardTypes?.Contains(cardTypeID.Value) == true;

                    if (additionalData is not null)
                    {
                        additionalData[AclHelper.TypeIDKey] = cardTypeID;
                    }
                }
            }

            if (!result)
            {
                return false;
            }

            if (rule.Extensions is { Count: > 0 })
            {
                foreach (var extension in rule.Extensions)
                {
                    if (!await extension.AllowedForCardAsync(cardID, additionalData, rule.ExtensionsData, cancellationToken))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion
    }
}
