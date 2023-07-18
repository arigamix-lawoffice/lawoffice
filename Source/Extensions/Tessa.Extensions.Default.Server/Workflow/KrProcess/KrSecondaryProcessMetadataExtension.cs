using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    public sealed class KrSecondaryProcessMetadataExtension : CardTypeMetadataExtension
    {
        /// <inheritdoc />
        public override async Task ModifyTypes(
            ICardMetadataExtensionContext context)
        {
            var templateType = await this.TryGetCardTypeAsync(context, DefaultCardTypes.KrTemplateCardTypeID);
            if (templateType is null)
            {
                return;
            }
            // добавляем объекты в кэш
            
            using var ctx = new CardGlobalReferencesContext(context, templateType);
            // формы здесь нельзя делать глобальными, т.к. потом возникает интерференция в обработке KrCardMetadataExtension 
            //templateType.Forms.MakeGlobal(ctx);
            
            // валидаторы.
            templateType.Validators.MakeGlobal(ctx);
            
            // расширения.
            templateType.Extensions.MakeGlobal(ctx);

            var targetTypes = new[] { DefaultCardTypes.KrStageTemplateTypeID, DefaultCardTypes.KrSecondaryProcessTypeID, };
            foreach (var target in targetTypes)
            {
                var targetType = await this.TryGetCardTypeAsync(context, target);
                if (targetType is null)
                {
                    continue;
                }
                var templateClone = await templateType.DeepCloneAsync(context.CancellationToken);

                targetType.SchemeItems.AddRange(templateClone.SchemeItems);
                // новая логика - добавляем глобальные разделяемые объекты.
                targetType.Validators.AddRange(templateType.Validators);
                targetType.Extensions.AddRange(templateType.Extensions);

                targetType.Forms.InsertRange(1, templateClone.Forms);
            }
        }
    }
}
