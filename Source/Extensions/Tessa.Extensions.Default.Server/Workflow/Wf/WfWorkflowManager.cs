using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards.Caching;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Notices;
using Tessa.Roles;

namespace Tessa.Extensions.Default.Server.Workflow.Wf
{
    /// <summary>
    /// Объект, предоставляющий возможности для управления бизнес-процессами Workflow.
    /// </summary>
    public class WfWorkflowManager :
        WorkflowManager
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием значений его свойств.
        /// </summary>
        /// <param name="context">
        /// Контекст бизнес-процесса, содержащий информацию по сохраняемой карточке.
        /// </param>
        /// <param name="queueProcessor">
        /// Объект, выполняющий обработку действий в очереди <see cref="WorkflowQueue"/>.
        /// </param>
        /// <param name="settingsLazy">Функция, возвращающая настройки решения для Wf.</param>
        /// <param name="roleGetStrategy">Стратегия для получения информации о ролях.</param>
        /// <param name="krTypesCache">Кэш с карточками и дополнительными настройками.</param>
        /// <param name="cardCache">Кэш карточек</param>
        /// <param name="notificationManager">Объект, управляющий отправкой уведомлений.</param>
        public WfWorkflowManager(
            IWorkflowContext context,
            IWorkflowQueueProcessor queueProcessor,
            KrSettingsLazy settingsLazy,
            IRoleGetStrategy roleGetStrategy,
            IKrTypesCache krTypesCache,
            ICardCache cardCache,
            INotificationManager notificationManager)
            : base(context, queueProcessor)
        {
            this.settingsLazy = settingsLazy ?? throw new ArgumentNullException(nameof(settingsLazy));
            this.RoleGetStrategy = roleGetStrategy ?? throw new ArgumentNullException(nameof(roleGetStrategy));
            this.KrTypesCache = krTypesCache ?? throw new ArgumentNullException(nameof(krTypesCache));
            this.CardCache = cardCache ?? throw new ArgumentNullException(nameof(cardCache));
            this.NotificationManager = notificationManager ?? throw new ArgumentNullException(nameof(notificationManager));
        }

        #endregion

        #region Fields

        private readonly KrSettingsLazy settingsLazy;

        #endregion

        #region Properties

        /// <summary>
        /// Стратегия для получения информации о ролях.
        /// </summary>
        public IRoleGetStrategy RoleGetStrategy { get; }

        /// <summary>
        /// Кэш с карточками и дополнительными настройками.
        /// </summary>
        public IKrTypesCache KrTypesCache { get; }

        /// <summary>
        /// Кэш карточек.
        /// </summary>
        public ICardCache CardCache { get; }

        /// <summary>
        /// Объект, управляющий отправкой уведомлений.
        /// </summary>
        public INotificationManager NotificationManager { get; }

        #endregion

        #region Base Overrides

        /// <summary>
        /// Идентификатор карточки-сателлита Workflow, используемой для хранения информации по бизнес-процессам,
        /// или идентификатор основной карточки, если идентификатор карточки-сателлита не был установлен
        /// в текущем контексте.
        /// </summary>
        protected override Guid WorkflowCardID
        {
            get
            {
                Guid? satelliteID = WfHelper.TryGetSatelliteID(this);
                if (satelliteID.HasValue)
                {
                    return satelliteID.Value;
                }

                throw new InvalidOperationException($"Can't determine satellite identifier for {this.GetType().Name}.");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Возвращает настройки решения для Wf.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public ValueTask<KrSettings> GetSettingsAsync(CancellationToken cancellationToken = default) =>
            this.settingsLazy.GetValueAsync(cancellationToken);

        #endregion
    }
}
