using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

using Tessa.Cards;

namespace Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine
{
    /// <summary>
    /// Объект предоставляющий доступ к вариантам завершения действий.
    /// </summary>
    public sealed class ActionCompletionOptionsProvider :
        IActionCompletionOptionsProvider
    {
        #region Fields

        private readonly ICardMetadata cardMetadata;

        #endregion

        #region Constructor

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ActionCompletionOptionsProvider"/>.
        /// </summary>
        /// <param name="cardMetadata">Метаинформация, необходимая для использования в типах карточек совместно с пакетом карточек.</param>
        public ActionCompletionOptionsProvider(
            ICardMetadata cardMetadata)
        {
            this.cardMetadata = cardMetadata;
        }

        #endregion

        #region Base overrides

        /// <inheritdoc/>
        public async ValueTask<ReadOnlyDictionary<Guid, ActionCompletionOption>> GetActionCompletionOptionsAsync(
            CancellationToken cancellationToken = default)
        {
            var enumerations =
                await this.cardMetadata.GetEnumerationsAsync(cancellationToken).ConfigureAwait(false);

            if (!enumerations.TryGetValue(WorkflowConstants.KrWeActionCompletionOptions.SectionName, out var enumeration))
            {
                return new ReadOnlyDictionary<Guid, ActionCompletionOption>(new Dictionary<Guid, ActionCompletionOption>());
            }

            var result = new Dictionary<Guid, ActionCompletionOption>();
            foreach (var record in enumeration.Records)
            {
                var id = (Guid)record[WorkflowConstants.KrWeActionCompletionOptions.ID];
                var name = (string)record[WorkflowConstants.KrWeActionCompletionOptions.Name];
                var caption = (string)record[WorkflowConstants.KrWeActionCompletionOptions.Caption];

                result.Add(id, new ActionCompletionOption(id, name, caption));
            }

            return new ReadOnlyDictionary<Guid, ActionCompletionOption>(result);
        }

        #endregion
    }
}
