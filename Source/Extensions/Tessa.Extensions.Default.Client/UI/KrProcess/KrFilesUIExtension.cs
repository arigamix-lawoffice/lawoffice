using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Platform.Client.Files;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.UI.KrProcess
{
    /// <summary>
    /// Добавляет ключ, сообщающий, что можно использовать методы API работы с Word'ом.
    /// </summary>
    public class KrFilesUIExtension : CardUIExtension
    {
        #region Fields

        private readonly IKrTypesCache typesCache;

        #endregion

        #region Constructors

        public KrFilesUIExtension(IKrTypesCache typesCache)
        {
            this.typesCache = typesCache;
        }

        #endregion

        #region CardUIExtension Methods

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            if ((await KrComponentsHelper.GetKrComponentsAsync(context.Card, this.typesCache, context.CancellationToken))
                .Has(KrComponents.Base))
            {
                context.Model.FileContainer.Info[FileExtensionHelper.EnableWordKey] = true;
            }
        }

        #endregion
    }
}
