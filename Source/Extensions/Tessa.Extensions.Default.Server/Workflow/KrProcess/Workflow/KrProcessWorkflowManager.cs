using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    public sealed class KrProcessWorkflowManager : WorkflowManager
    {
        #region fields

        private readonly KrSettingsLazy settingsLazy;

        private readonly AsyncLazy<Guid> satelliteIDLazy;

        private Guid? secondarySatelliteID;

        #endregion

        #region constructor

        public KrProcessWorkflowManager(
            KrProcessWorkflowContext context,
            IWorkflowQueueProcessor queueProcessor)
            : base(context, queueProcessor)
        {
            this.WorkflowContext = context;

            this.settingsLazy = context.SettingsLazy;
            this.satelliteIDLazy = new AsyncLazy<Guid>(async () => (await context.KrScope.GetKrSatelliteAsync(context.CardID))?.ID ?? Guid.Empty);
        }

        #endregion

        #region override

        /// <inheritdoc/>
        protected override async ValueTask<Guid> GetWorkflowCardIDAsync(CancellationToken cancellationToken = default)
            => this.secondarySatelliteID ?? await this.satelliteIDLazy.Value;
        #endregion

        #region properties

        public KrProcessWorkflowContext WorkflowContext { get; }

        #endregion

        #region methods

        public ValueTask<KrSettings> GetSettingsAsync(CancellationToken cancellationToken = default) => this.settingsLazy.GetValueAsync(cancellationToken);

        /// <summary>
        /// Задаёт ИД карточки сателлита вторичного процесса.
        /// </summary>
        /// <param name="secSatID">ИД карточки сателлита вторичного процесса.</param>
        public void SpecifySatelliteID(
            Guid secSatID)
        {
            this.SpecifySatelliteID(secSatID, true);
        }

        /// <summary>
        /// Задаёт ИД карточки сателлита вторичного процесса.
        /// </summary>
        /// <param name="secSatID">ИД карточки сателлита вторичного процесса.</param>
        /// <param name="isCheckSetSecondarySatelliteID">Проверять установлено ли значение ИД карточки сателлита вторичного процесса.</param>
        internal void SpecifySatelliteID(
            Guid secSatID,
            bool isCheckSetSecondarySatelliteID)
        {
            if (this.satelliteIDLazy.IsValueCreated)
            {
                throw new InvalidOperationException("Main satellite has already been used.");
            }

            if (isCheckSetSecondarySatelliteID && this.secondarySatelliteID.HasValue)
            {
                throw new InvalidOperationException("Secondary satellite ID already speciifed.");
            }

            this.secondarySatelliteID = secSatID;
        }

        #endregion
    }
}
