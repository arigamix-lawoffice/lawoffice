using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Предоставляет методы для запуска процесса на клиенте, не имеющим доступа к зависимостям определённым в <b>Tessa.UI</b>.
    /// </summary>
    public sealed class KrProcessClientLauncher :
        KrProcessClientLauncherBase
    {
        #region Nested Types

        /// <summary>
        /// Предоставляет параметры запуска процесса на клиенте, не имеющим доступа к зависимостям определённым в <b>Tessa.UI</b>.
        /// </summary>
        public sealed class SpecificParameters :
            KrProcessClientLauncherBaseSpecificParameters
        {

        }

        #endregion

        #region Fields

        private readonly ICardRepository cardRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrProcessClientLauncher"/>.
        /// </summary>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        public KrProcessClientLauncher(
            ICardRepository cardRepository)
        {
            this.cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override async Task<IKrProcessLaunchResult> LaunchCoreAsync(
            Dictionary<string, object> requestInfo,
            ICardExtensionContext cardContext = default,
            IKrProcessLauncherSpecificParameters specificParameters = default,
            CancellationToken cancellationToken = default)
        {
            var request = new CardRequest
            {
                RequestType = KrConstants.LaunchProcessRequestType,
                Info = requestInfo,
            };

            var response = await this.cardRepository.RequestAsync(request, cancellationToken);
            return response.GetKrProcessLaunchFullResult();
        }

        #endregion
    }
}