#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Roles.Acl;

namespace Tessa.Extensions.Default.Server.Acl
{
    /// <summary>
    /// Объект экземпляра кеша правил расчёта ACL, который учитывает типы документов.
    /// </summary>
    public class KrAclGenerationRuleCacheObject : AclGenerationRuleCacheObject
    {
        #region Fields

        private readonly IDbScope dbScope;
        private readonly IKrTypesCache typesCache;

        #endregion

        #region Properties

        /// <summary>
        /// Правила расчёта ACL, разбитые по типам документов, на которые у них есть триггер.
        /// </summary>
        protected Dictionary<Guid, IList<IAclGenerationRule>> RulesByTriggerDocType { get; set; } = new();

        #endregion

        #region Constructors

        public KrAclGenerationRuleCacheObject(
            IDbScope dbScope,
            IKrTypesCache typesCache)
        {
            this.dbScope = NotNullOrThrow(dbScope);
            this.typesCache = NotNullOrThrow(typesCache);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async ValueTask<IList<IAclGenerationRule>> GetRulesByTriggerCardAsync(
            Card triggerCard,
            CancellationToken cancellationToken = default)
        {
            var docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(triggerCard, this.dbScope, cancellationToken);
            if (docTypeID is not null
                && this.RulesByTriggerDocType.TryGetValue(docTypeID.Value, out var triggers))
            {
                return triggers;
            }

            return await base.GetRulesByTriggerCardAsync(triggerCard, cancellationToken);
        }

        /// <inheritdoc/>
        public override async ValueTask InitializeAsync(IList<IAclGenerationRule> allRules, CancellationToken cancellationToken = default)
        {
            await base.InitializeAsync(allRules, cancellationToken);

            var docTypes = new HashSet<Guid, KrDocType>(x => x.ID, await this.typesCache.GetDocTypesAsync(cancellationToken));
            List<KrDocType>? docTypesToMove = null;
            foreach (var (typeID, _) in this.RulesByTriggerCardType)
            {
                if (!docTypes.TryGetItem(typeID, out var docType))
                {
                    continue;
                }

                docTypesToMove ??= new();
                docTypesToMove.Add(docType);
            }

            if (docTypesToMove is not null)
            {
                foreach (var docType in docTypesToMove)
                {
                    if (!this.RulesByTriggerCardType.Remove(docType.ID, out var aclRules))
                    {
                        continue;
                    }

                    if (this.RulesByTriggerCardType.TryGetValue(docType.CardTypeID, out var cardTypeRules))
                    {
                        aclRules.AddRange(cardTypeRules.Where(x => !aclRules.Contains(x)));
                    }

                    this.RulesByTriggerDocType[docType.ID] = aclRules;
                }
            }
        }

        #endregion
    }
}
