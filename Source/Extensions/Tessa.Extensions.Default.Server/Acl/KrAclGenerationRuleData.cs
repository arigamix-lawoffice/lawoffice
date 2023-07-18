#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Roles.Acl;
using Tessa.Roles.Acl.Extensions;
using Tessa.Roles.Queries;
using Tessa.Roles.Queries.Parts;
using Tessa.Roles.Triggers;

namespace Tessa.Extensions.Default.Server.Acl
{
    /// <inheritdoc cref="IAclGenerationRuleData"/>
    public class KrAclGenerationRuleData : AclGenerationRuleData
    {
        #region Fields

        private readonly IKrTypesCache krTypesCache;

        #endregion

        #region Constructors

        /// <inheritdoc cref="AclGenerationRuleData(AclGenerationRuleDataSource, IAclGenerationRuleExtensionResolver, IComplexQueryBuilderFactory, IDbScope)"/>
        public KrAclGenerationRuleData(
            AclGenerationRuleDataSource source,
            IAclGenerationRuleExtensionResolver extensionsResolver,
            IComplexQueryBuilderFactory getItemsQueryBuilderFactory,
            IDbScope dbScope,
            IKrTypesCache krTypesCache)
            : base(
                  source,
                  extensionsResolver,
                  getItemsQueryBuilderFactory,
                  dbScope)
        {
            this.krTypesCache = NotNullOrThrow(krTypesCache);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Список типов карточек или <c>null</c>, если нет типов карточек для правила.
        /// </summary>
        public List<Guid>? CardTypes { get; private set; }

        /// <summary>
        /// Список типов документов или <c>null</c>, если нет типов документов для правила.
        /// </summary>
        public List<Guid>? DocTypes { get; private set; }
        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async ValueTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            var types = this.Source.CardTypes;
            var allDocTypes = await this.krTypesCache.GetDocTypesAsync(cancellationToken);

            foreach (var typeID in types)
            {
                // Не добавляем тип документа в список, если добавлен тип карточки для данного типа документа
                if (allDocTypes.TryFirst(x => x.ID == typeID, out var docType)
                    && !types.Contains(docType.CardTypeID))
                {
                    this.DocTypes ??= new();
                    this.DocTypes.Add(typeID);
                }
                else
                {
                    this.CardTypes ??= new();
                    this.CardTypes.Add(typeID);
                }
            }

            await base.InitializeAsync(cancellationToken);
        }

        /// <inheritdoc/>
        protected override async ValueTask InitializeGetAllCardsQueryAsync(CancellationToken cancellationToken = default)
        {
            // Если только типы карточек, то вызываем типовую реализацию.
            if (this.DocTypes is not { Count: > 0 })
            {
                await base.InitializeGetAllCardsQueryAsync(cancellationToken);
                return;
            }

            var builder = this.GetItemsQueryBuilderFactory.Create();
            builder.AddMainQuery(
                new IComplexQueryPart[]
                {
                    new FuncQueryPart((b, _) => b.Select().C(nameof(AclGenerationRuleData), Scheme.Names.Instances_ID, AclHelper.TypeIDKey)),
                    new ExtensionResultPlaceholder(),
                    new FuncQueryPart((builder, dataParameters) =>
                    {
                        builder
                            .From()
                            .StartExpression(true)
                                .Select()
                                    .C(KrConstants.DocumentCommonInfo.Name, KrConstants.DocumentCommonInfo.ID)
                                    .C(KrConstants.DocumentCommonInfo.Name, KrConstants.DocumentCommonInfo.DocTypeID).As(AclHelper.TypeIDKey)
                                .From(KrConstants.DocumentCommonInfo.Name).NoLock()
                                .Where().C(KrConstants.DocumentCommonInfo.DocTypeID).InArray(this.DocTypes, "DocTypes", out var arrayParameter);
                        dataParameters.Add(arrayParameter);

                        if (this.CardTypes is null)
                        {
                            return;
                        }

                        builder
                            .Union()
                            .Select()
                                .C(Scheme.Names.Instances, Scheme.Names.Instances_ID)
                                .C(Scheme.Names.Instances, Scheme.Names.Instances_TypeID).As(AclHelper.TypeIDKey)
                            .From(Scheme.Names.Instances).NoLock()
                            .Where().C(Scheme.Names.Instances, Scheme.Names.Instances_TypeID).InArray(this.CardTypes, "CardTypes", out var arrayParameter2);
                            dataParameters.Add(arrayParameter2);

                        builder.EndExpression(true).As(nameof(AclGenerationRuleData));
                    }, true),
                    new ExtensionJoinPlaceholder(nameof(AclGenerationRuleData), Scheme.Names.Instances_ID),
                    ConstsParts.WhereStartedPart,
                    new ExtensionFilterPlaceholder()
                });

            await this.ModifyBuilderWithExtensionsAsync(builder, cancellationToken);

            this.GetAllCardsQuery = await builder.BuildAsync(cancellationToken);
        }

        /// <inheritdoc/>
        protected override async ValueTask ModifyBuilderWithRuleAsync(IComplexQueryBuilder builder, CancellationToken cancellationToken = default)
        {
            // Если только типы карточек, то вызываем типовую реализацию.
            if (this.DocTypes is not { Count: > 0 })
            {
                await base.ModifyBuilderWithRuleAsync(builder, cancellationToken);
                return;
            }

            builder.AddSubQueryJoin(
                nameof(AclGenerationRuleData),
                new FuncQueryPart((builder, dataProperties) =>
                {
                    builder
                        .StartExpression()
                        .Select()
                            .C(KrConstants.DocumentCommonInfo.Name, KrConstants.DocumentCommonInfo.ID)
                            .C(KrConstants.DocumentCommonInfo.Name, KrConstants.DocumentCommonInfo.DocTypeID).As(AclHelper.TypeIDKey)
                        .From(KrConstants.DocumentCommonInfo.Name).NoLock()
                        .Where().C(KrConstants.DocumentCommonInfo.Name, KrConstants.DocumentCommonInfo.DocTypeID).InArray(this.DocTypes, "DocTypes", out var arrayParameter);
                    dataProperties.Add(arrayParameter);

                    if (this.CardTypes is { Count: > 0 })
                    {
                        builder
                            .Union()

                            .Select()
                                .C(Scheme.Names.Instances, Scheme.Names.Instances_ID)
                                .C(Scheme.Names.Instances, Scheme.Names.Instances_TypeID).As(AclHelper.TypeIDKey)
                            .From(Scheme.Names.Instances).NoLock()
                            .Where().C(Scheme.Names.Instances, Scheme.Names.Instances_TypeID).InArray(this.CardTypes, "CardTypes", out var arrayParameter2);
                        dataProperties.Add(arrayParameter2);
                    }

                    builder.EndExpression().As(nameof(AclGenerationRuleData));
                }, true),
                Scheme.Names.Instances_ID,
                false);
        }

        /// <inheritdoc/>
        protected override async ValueTask InitializeTriggersAsync(CancellationToken cancellationToken = default)
        {
            await base.InitializeTriggersAsync(cancellationToken);
            if (this.DocTypes is { Count: > 0 })
            {
                foreach (var trigger in this.Triggers)
                {
                    trigger.CheckTriggerCardAsyncFunc = this.CheckTriggerAsync;
                }
            }
        }

        #endregion

        #region Private Methods

        private async ValueTask<bool> CheckTriggerAsync(UpdateTrigger trigger, Card triggerCard, CancellationToken cancellationToken)
        {
            if (trigger.CardTypes.Contains(triggerCard.TypeID))
            {
                return true;
            }

            var docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(triggerCard, this.DbScope, cancellationToken);
            if (docTypeID is not null
                && trigger.CardTypes.Contains(docTypeID.Value))
            {
                return true;
            }

            return false;
        }

        #endregion
    }

}
