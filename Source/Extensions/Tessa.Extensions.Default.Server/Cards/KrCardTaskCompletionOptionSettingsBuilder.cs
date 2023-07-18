#nullable enable

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Предоставляет билдер объектов типа <see cref="CardTaskCompletionOptionSettings"/> реализующий функционал специфичный для параметров используемых в маршрутах.
    /// </summary>
    public class KrCardTaskCompletionOptionSettingsBuilder :
        CardTaskCompletionOptionSettingsBuilder
    {
        #region Fields

        private readonly IKrTypesCache typesCache;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrCardTaskCompletionOptionSettingsBuilder"/>.
        /// </summary>
        /// <param name="typesCache">Кэш по типам карточек и документов, содержащих информацию по типовому решению.</param>
        public KrCardTaskCompletionOptionSettingsBuilder(IKrTypesCache typesCache) =>
            this.typesCache = NotNullOrThrow(typesCache);

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async Task ModifyResultAsync(
            CardTaskCompletionOptionSettings result,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken)
        {
            var typeID = result.DialogTypeID;
            var docType = (await this.typesCache.GetDocTypesAsync(cancellationToken)).FirstOrDefault(x => x.ID == typeID);

            if (docType is not null)
            {
                result.Info[Keys.DocTypeID] = typeID;
                result.Info[Keys.DocTypeTitle] = docType.Caption;
                result.DialogTypeID = docType.CardTypeID;
            }
        }

        #endregion
    }
}
