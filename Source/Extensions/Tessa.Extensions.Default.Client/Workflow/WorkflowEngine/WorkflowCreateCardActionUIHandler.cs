using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;
using Tessa.UI.WorkflowViewer.Actions;
using Tessa.Workflow;
using Tessa.Workflow.Helpful;
using Tessa.Workflow.Storage;

namespace Tessa.Extensions.Default.Client.Workflow.WorkflowEngine
{
    public sealed class WorkflowCreateCardActionUIHandler : WorkflowActionUIHandlerBase
    {
        #region Fields

        private const string MainSection = "KrCreateCardAction";

        #endregion

        #region Constructors

        public WorkflowCreateCardActionUIHandler()
            :base(KrDescriptors.CreateCardDescriptor)
        {
        }

        #endregion

        #region Base Overrides

        protected override async Task AttachToCardCoreAsync(WorkflowEngineBindingContext bindingContext)
        {
            if (bindingContext.ActionTemplate != null)
            {
                bindingContext.Section = bindingContext.Card.Sections.GetOrAdd(MainSection);

                await AttachFieldToTemplateAsync(
                    bindingContext,
                    "Script",
                    typeof(string),
                    MainSection,
                    "Script");
            }
            else
            {
                // Перенос имени шаблона в настройках действия из TemplateDigest в TemplateCaption
                var action = bindingContext.Action;
                var template = action.Hash
                    .TryGet<Dictionary<string, object>>(MainSection)
                    ?.TryGet<Dictionary<string, object>>("Template");
                if (template != null
                    && template.TryGetValue("Digest", out var value))
                {
                    template["Caption"] = value;
                    template.Remove("Digest");
                }
            }

            await base.AttachToCardCoreAsync(bindingContext);
        }

        protected override Task UpdateFormCoreAsync(
            WorkflowStorageBase action,
            WorkflowStorageBase node,
            WorkflowStorageBase process,
            ICardModel cardModel,
            WorkflowActionStorage actionTemplate = null,
            CancellationToken cancellationToken = default)
        {
            var mainSection = cardModel.Card.Sections[MainSection];

            mainSection.FieldChanged += (s, e) =>
            {
                switch(e.FieldName)
                {
                    case "TemplateID":
                        if (e.FieldValue != null)
                        {
                            mainSection.Fields[WorkflowEngineHelper.BindingPrefix + "TypeID"] = null;
                            mainSection.Fields[WorkflowEngineHelper.BindingPrefix + "TypeCaption"] = null;
                            mainSection.Fields["TypeID"] = null;
                            mainSection.Fields["TypeCaption"] = null;
                        }
                        break;

                    case "TypeID":
                        if (e.FieldValue != null)
                        {
                            mainSection.Fields[WorkflowEngineHelper.BindingPrefix + "TemplateID"] = null;
                            mainSection.Fields[WorkflowEngineHelper.BindingPrefix + "TemplateCaption"] = null;
                            mainSection.Fields["TemplateID"] = null;
                            mainSection.Fields["TemplateCaption"] = null;
                        }
                        break;
                }
            };

            return Task.CompletedTask;
        }

        #endregion
    }
}
