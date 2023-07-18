using System.Threading.Tasks;
using Tessa.Extensions.Default.Client.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.UI.KrProcess
{
    /// <summary>
    /// Расширение на кнопки в карточках шаблона этапа KrStageTemplates и базового метода KrCommonMethod
    /// </summary>
    public sealed class KrStageSourceUIExtension: CardUIExtension
    {
        #region constants

        private const string CompileButtonAlias = "CompileButton";

        private const string CompileAllButtonAlias = "CompileAllButton";

        #endregion
        
        #region base overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            var cardModel = context.Model;

            if (cardModel.Controls.TryGet(CompileButtonAlias, out var compileControl)
                && compileControl is ButtonViewModel compileButton)
            {
                compileButton.CommandClosure.Execute = async o => await KrUIHelper.SendCompileRequestAsync(KrConstants.Keys.CompileWithValidationResult);
            }

            if (cardModel.Controls.TryGet(CompileAllButtonAlias, out var compileAllControl) 
                && compileAllControl is ButtonViewModel compileAllButton)
            {
                compileAllButton.CommandClosure.Execute = async o => await KrUIHelper.SendCompileRequestAsync(KrConstants.Keys.CompileAllWithValidationResult);
            }
        }

        #endregion
    }
}
