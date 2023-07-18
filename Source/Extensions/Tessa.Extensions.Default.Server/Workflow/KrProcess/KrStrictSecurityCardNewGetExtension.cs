using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// В режимах <see cref="ConfigurationFlags.Sealed"/> или <see cref="ConfigurationFlags.StrictSecurity"/>
    /// недоступно редактирование C#-скриптов и SQL-запросов в объектах маршрутов.
    /// </summary>
    public sealed class KrStrictSecurityCardNewGetExtension :
        CardNewGetExtension
    {
        #region Constructors

        public KrStrictSecurityCardNewGetExtension(IConfigurationInfoProvider configurationInfoProvider)
        {
            this.configurationInfoProvider = configurationInfoProvider;
        }

        #endregion

        #region Fields

        private readonly IConfigurationInfoProvider configurationInfoProvider;

        #endregion

        #region Private Methods

        private Task AfterRequestAsync(ICardExtensionContext context, Card card)
        {
            if (this.configurationInfoProvider.GetFlags().HasNot(ConfigurationFlags.Sealed & ConfigurationFlags.StrictSecurity))
            {
                return Task.CompletedTask;
            }

            if (context.CardTypeIs(DefaultCardTypes.KrStageTemplateTypeID))
            {
                CardSectionPermissionInfo section = card.Permissions.Sections
                    .GetOrAddEntry(KrConstants.KrStageTemplates.Name);

                foreach (string fieldName in KrStrictSecurityHelper.KrStageTemplateFields)
                {
                    section.SetFieldPermissions(fieldName, CardPermissionFlagValues.ProhibitAllField, overwrite: true);
                }

                foreach (string tableName in KrStrictSecurityHelper.KrStageTemplateTables)
                {
                    CardSectionPermissionInfo table = card.Permissions.Sections
                        .GetOrAddTable(tableName);

                    foreach (string fieldName in KrStrictSecurityHelper.KrStageTemplateTableFields)
                    {
                        table.SetFieldPermissions(fieldName, CardPermissionFlagValues.ProhibitAllField, overwrite: true);
                    }
                }
            }
            else if (context.CardTypeIs(DefaultCardTypes.KrStageGroupTypeID))
            {
                CardSectionPermissionInfo section = card.Permissions.Sections
                    .GetOrAddEntry(KrConstants.KrStageGroups.Name);

                foreach (string fieldName in KrStrictSecurityHelper.KrStageGroupFields)
                {
                    section.SetFieldPermissions(fieldName, CardPermissionFlagValues.ProhibitAllField, overwrite: true);
                }
            }
            else if (context.CardTypeIs(DefaultCardTypes.KrSecondaryProcessTypeID))
            {
                CardSectionPermissionInfo section = card.Permissions.Sections
                    .GetOrAddEntry(KrConstants.KrSecondaryProcesses.Name);

                foreach (string fieldName in KrStrictSecurityHelper.KrSecondaryProcessFields)
                {
                    section.SetFieldPermissions(fieldName, CardPermissionFlagValues.ProhibitAllField, overwrite: true);
                }

                foreach (string tableName in KrStrictSecurityHelper.KrSecondaryProcessTables)
                {
                    CardSectionPermissionInfo table = card.Permissions.Sections
                        .GetOrAddTable(tableName);

                    foreach (string fieldName in KrStrictSecurityHelper.KrSecondaryProcessTableFields)
                    {
                        table.SetFieldPermissions(fieldName, CardPermissionFlagValues.ProhibitAllField, overwrite: true);
                    }
                }
            }
            else if (context.CardTypeIs(DefaultCardTypes.KrStageCommonMethodTypeID))
            {
                card.Permissions.Sections
                    .GetOrAddEntry(KrStrictSecurityHelper.KrStageCommonMethodSectionName)
                    .SetFieldPermissions(KrStrictSecurityHelper.KrStageCommonMethodFieldName, CardPermissionFlagValues.ProhibitAllField, overwrite: true);
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Base Overrides

        public override Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            if (context.Request.ServiceType == CardServiceType.Default
                || !context.RequestIsSuccessful
                || (card = context.Response.TryGetCard()) == null)
            {
                return Task.CompletedTask;
            }

            return this.AfterRequestAsync(context, card);
        }


        public override Task AfterRequest(ICardNewExtensionContext context)
        {
            Card card;
            if (context.Request.ServiceType == CardServiceType.Default
                || !context.RequestIsSuccessful
                || (card = context.Response.TryGetCard()) == null)
            {
                return Task.CompletedTask;
            }

            return this.AfterRequestAsync(context, card);
        }

        #endregion
    }
}
