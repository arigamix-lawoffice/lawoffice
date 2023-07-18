using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// В режимах <see cref="ConfigurationFlags.Sealed"/> или <see cref="ConfigurationFlags.StrictSecurity"/>
    /// недоступно редактирование C#-скриптов и SQL-запросов в объектах маршрутов.
    /// </summary>
    public sealed class KrStrictSecurityCardStoreExtension :
        CardStoreExtension
    {
        #region Constructors

        public KrStrictSecurityCardStoreExtension(IConfigurationInfoProvider configurationInfoProvider)
        {
            this.configurationInfoProvider = configurationInfoProvider;
        }

        #endregion

        #region Fields

        private readonly IConfigurationInfoProvider configurationInfoProvider;

        #endregion

        #region Private

        private static bool CheckTableSections(
            StringDictionaryStorage<CardSection> sections,
            ICollection<string> tables,
            ICollection<string> tableFields)
        {
            var hasError = false;
            foreach (string tableName in tables)
            {
                ListStorage<CardRow> rows;
                if (sections.TryGetValue(tableName, out CardSection table)
                    && (rows = table.TryGetRows()) != null
                    && rows.Count > 0)
                {
                    foreach (CardRow row in rows)
                    {
                        CardRowState state = row.State;
                        if (state == CardRowState.Inserted || state == CardRowState.Modified)
                        {
                            foreach (string fieldName in tableFields)
                            {
                                string value = row.TryGet<string>(fieldName);
                                if (!string.IsNullOrWhiteSpace(value))
                                {
                                    hasError = true;
                                    break;
                                }
                            }

                            if (hasError)
                            {
                                break;
                            }
                        }
                    }
                }

                if (hasError)
                {
                    break;
                }
            }

            return hasError;
        }

        #endregion

        #region Base Overrides

        public override Task BeforeRequest(ICardStoreExtensionContext context)
        {
            StringDictionaryStorage<CardSection> sections;
            if (context.Request.ServiceType == CardServiceType.Default
                || (sections = context.Request.TryGetCard()?.TryGetSections()) == null)
            {
                return Task.CompletedTask;
            }

            ConfigurationFlags flags = this.configurationInfoProvider.GetFlags();
            if (!flags.HasAny(ConfigurationFlags.Sealed | ConfigurationFlags.StrictSecurity))
            {
                return Task.CompletedTask;
            }

            bool hasError = false;
            if (context.CardTypeIs(DefaultCardTypes.KrStageTemplateTypeID))
            {
                if (sections.TryGetValue(KrConstants.KrStageTemplates.Name, out CardSection section))
                {
                    Dictionary<string, object> fields = section.RawFields;

                    foreach (string fieldName in KrStrictSecurityHelper.KrStageTemplateFields)
                    {
                        string value = fields.TryGet<string>(fieldName);
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            hasError = true;
                            break;
                        }
                    }
                }

                if (!hasError)
                {
                    hasError = CheckTableSections(
                        sections,
                        KrStrictSecurityHelper.KrStageTemplateTables,
                        KrStrictSecurityHelper.KrStageTemplateTableFields);
                }
            }
            else if (context.CardTypeIs(DefaultCardTypes.KrStageGroupTypeID))
            {
                if (sections.TryGetValue(KrConstants.KrStageGroups.Name, out CardSection section))
                {
                    Dictionary<string, object> fields = section.RawFields;

                    foreach (string fieldName in KrStrictSecurityHelper.KrStageGroupFields)
                    {
                        string value = fields.TryGet<string>(fieldName);
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            hasError = true;
                            break;
                        }
                    }
                }
            }
            else if (context.CardTypeIs(DefaultCardTypes.KrSecondaryProcessTypeID))
            {
                if (sections.TryGetValue(KrConstants.KrSecondaryProcesses.Name, out CardSection section))
                {
                    Dictionary<string, object> fields = section.RawFields;

                    foreach (string fieldName in KrStrictSecurityHelper.KrSecondaryProcessFields)
                    {
                        string value = fields.TryGet<string>(fieldName);
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            hasError = true;
                            break;
                        }
                    }

                    if (!hasError)
                    {
                        hasError = CheckTableSections(
                            sections,
                            KrStrictSecurityHelper.KrSecondaryProcessTables,
                            KrStrictSecurityHelper.KrSecondaryProcessTableFields);
                    }
                }
            }
            else if (context.CardTypeIs(DefaultCardTypes.KrStageCommonMethodTypeID))
            {
                if (sections.TryGetValue(KrStrictSecurityHelper.KrStageCommonMethodSectionName, out CardSection section))
                {
                    string value = section.RawFields.TryGet<string>(KrStrictSecurityHelper.KrStageCommonMethodFieldName);
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        hasError = true;
                    }
                }
            }

            if (hasError)
            {
                if (flags.Has(ConfigurationFlags.StrictSecurity))
                {
                    ValidationSequence
                        .Begin(context.ValidationResult)
                        .SetObjectName(this)
                        .Error(ValidationKeys.ConfigurationIsStrictSecurity)
                        .End();
                }
                else if (flags.Has(ConfigurationFlags.Sealed))
                {
                    ValidationSequence
                        .Begin(context.ValidationResult)
                        .SetObjectName(this)
                        .Error(ValidationKeys.ConfigurationIsSealed)
                        .End();
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
