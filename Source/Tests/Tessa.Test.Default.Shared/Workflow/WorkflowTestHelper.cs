using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.SmartMerge;
using Tessa.Test.Default.Shared.Cards;
using Tessa.Test.Default.Shared.Kr;
using Tessa.Workflow;

namespace Tessa.Test.Default.Shared.Workflow
{
    /// <summary>
    /// Содержит вспомогательные методы используемые в тестах WorkflowEngine.
    /// </summary>
    public static class WorkflowTestHelper
    {
        #region Constants

        /// <summary>
        /// Имя ключа, по которому в <see cref="ICardLifecycleCompanionData.OtherResponses"/> содержится информация о последнем запросе на обработку сигнала в <see cref="IWorkflowEngineProcessor"/>. Тип значения: <see cref="IWorkflowEngineProcessRequest"/>.
        /// </summary>
        public const string WorkflowEngineProcessRequestKey = nameof(WorkflowEngineProcessRequest);

        /// <summary>
        /// Имя ключа, по которому в <see cref="ICardLifecycleCompanionData.OtherResponses"/> содержится информация о последнем результате обработки сигнала в <see cref="IWorkflowEngineProcessor"/>. Тип значения: <see cref="IWorkflowEngineProcessResult"/>.
        /// </summary>
        public const string WorkflowEngineProcessResultKey = nameof(WorkflowEngineProcessResult);

        #endregion

        #region Public Methods

        /// <summary>
        /// Импортирует все карточки из папки Workflow из ресурсов указанной сборки.
        /// </summary>
        /// <param name="assembly">Сборка, содержащая ресурсы.</param>
        /// <param name="cardManager">Объект, управляющий операциями с карточками.</param>
        /// <param name="cardRepository">Репозиторий для управления типами карточек.</param>
        /// <param name="cardPredicateAsync">Функция определяющая возможность импорта карточки или значение по умолчанию для типа, если фильтрация не выполняется.</param>
        /// <param name="getMergeOptionsFuncAsync">Функция возвращающая параметры объединения для файла с заданным именем или значение по умолчанию для типа, если используются параметры по умолчанию. Параметры: имя файла для которого запрашиваются параметры объединения.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static Task ImportWorkflowCardsAsync(
            Assembly assembly,
            ICardManager cardManager,
            ICardRepository cardRepository,
            Func<Card, CancellationToken, ValueTask<bool>> cardPredicateAsync = default,
            Func<string, CancellationToken, ValueTask<ICardMergeOptions>> getMergeOptionsFuncAsync = default,
            CancellationToken cancellationToken = default) =>
            TestCardHelper.ImportCardsFromDirectoryAsync(
                assembly,
                cardManager,
                cardRepository,
                "Workflow",
                cardPredicateAsync: cardPredicateAsync,
                getMergeOptionsFuncAsync: getMergeOptionsFuncAsync,
                cancellationToken: cancellationToken);

        /// <summary>
        /// Импортирует указанную карточку Workflow из встроенных ресурсов указанной сборки.
        /// </summary>
        /// <param name="assembly">Сборка, содержащая ресурсы.</param>
        /// <param name="cardManager">Объект, управляющий операциями с карточками.</param>
        /// <param name="cardRepository">Репозиторий для управления типами карточек.</param>
        /// <param name="cardName">Имя карточки с расширением в папке сборки <paramref name="assembly"/>\Cards\Workflow.</param>
        /// <param name="mergeOptions">Опции слияния или <see langword="null"/>, если слияние выполняется с настройками по умолчанию.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static Task ImportWorkflowCardAsync(
            Assembly assembly,
            ICardManager cardManager,
            ICardRepository cardRepository,
            string cardName,
            ICardMergeOptions mergeOptions = default,
            CancellationToken cancellationToken = default) =>
            TestCardHelper.ImportCardFromTestResourcesAsync(
                assembly,
                Path.Combine("Workflow", cardName),
                cardManager,
                cardRepository,
                mergeOptions: mergeOptions,
                throwOnFailure: true,
                cancellationToken: cancellationToken);

        #endregion
    }
}
