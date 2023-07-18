using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Базовый абстрактный класс предоставляющий методы для запуска процесса на клиенте.
    /// </summary>
    public abstract class KrProcessClientLauncherBase :
        IKrProcessLauncher
    {
        #region IKrProcessLauncher Members

        /// <inheritdoc/>
        public Task<IKrProcessLaunchResult> LaunchAsync(
            KrProcessInstance krProcess,
            ICardExtensionContext cardContext = default,
            IKrProcessLauncherSpecificParameters specificParameters = default,
            CancellationToken cancellationToken = default)
        {
            var requestInfo = new Dictionary<string, object>(StringComparer.Ordinal);
            requestInfo.SetKrProcessInstance(krProcess);
            this.PrepareParametes(requestInfo, specificParameters);

            return this.LaunchCoreAsync(
                requestInfo,
                cardContext,
                specificParameters,
                cancellationToken);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Запускает процесс.
        /// </summary>
        /// <param name="requestInfo">Коллекция пар &lt;ключ-значение&gt; содержащая дополнительную информацию используемую при запуске процесса.</param>
        /// <param name="cardContext">Контекст процесса взаимодействия с карточкой в рамках которого запускается процесс.</param>
        /// <param name="specificParameters">Параметры запуска процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns></returns>
        protected abstract Task<IKrProcessLaunchResult> LaunchCoreAsync(
            Dictionary<string, object> requestInfo,
            ICardExtensionContext cardContext = default,
            IKrProcessLauncherSpecificParameters specificParameters = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Обрабатывает параметры запуска процесса, которые должны быть добавлены в дополнительную информацию используемую при запуске процесса.
        /// </summary>
        /// <param name="requestInfo">Коллекция пар &lt;ключ-значение&gt; содержащая дополнительную информацию используемую при запуске процесса.</param>
        /// <param name="specificParameters">Параметры запуска процесса. Параметр может иметь значение по умолчанию для типа.</param>
        protected virtual void PrepareParametes(
            IDictionary<string, object> requestInfo,
            IKrProcessLauncherSpecificParameters specificParameters)
        {
            if (specificParameters is null)
            {
                return;
            }

            if (specificParameters.RaiseErrorWhenExecutionIsForbidden)
            {
                requestInfo[KrConstants.RaiseErrorWhenExecutionIsForbidden] = BooleanBoxes.True;
            }

            if (specificParameters is not KrProcessClientLauncherBaseSpecificParameters clientSpecificParametersBase)
            {
                return;
            }

            if (clientSpecificParametersBase.RequestInfo is not null)
            {
                StorageHelper.Merge(clientSpecificParametersBase.RequestInfo, requestInfo);
            }
        }

        #endregion
    }
}
