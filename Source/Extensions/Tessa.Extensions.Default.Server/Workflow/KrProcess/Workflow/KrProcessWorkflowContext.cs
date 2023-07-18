using System;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Events;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    public sealed class KrProcessWorkflowContext : IWorkflowContext
    {
        #region constructor

        public KrProcessWorkflowContext(
            Guid cardID,
            Guid? docTypeID,
            KrComponents krComponents,
            ICardStoreExtensionContext cardStoreContext,
            CardStoreRequest nextRequest,
            ICardRepository cardRepositoryToCreateTasks,
            ICardTaskHistoryManager taskHistoryManager,
            ICardGetStrategy cardGetStrategy,
            IObjectModelMapper objectModelMapper,
            IKrProcessRunner asyncProcessRunner,
            IKrScope krScope,
            KrSettingsLazy settingsLazy,
            IKrProcessCache processCache,
            IKrEventManager eventManager,
            Dictionary<string, object> info = null)
        {
            this.CardID = cardID;
            this.DocTypeID = docTypeID;
            this.KrComponents = krComponents;

            this.CardStoreContext = cardStoreContext;
            this.NextRequest = nextRequest;

            this.CardRepositoryToCreateTasks = cardRepositoryToCreateTasks;
            this.TaskHistoryManager = taskHistoryManager;
            this.CardGetStrategy = cardGetStrategy;
            this.ObjectModelMapper = objectModelMapper;
            this.AsyncProcessRunner = asyncProcessRunner;
            this.KrScope = krScope;
            this.SettingsLazy = settingsLazy;
            this.ProcessCache = processCache;
            this.EventManager = eventManager;

            this.Info = new SerializableObject();
            if (info != null)
            {
                this.Info.SetStorage(info);
            }
        }

        #endregion

        #region properties

        public Guid CardID { get; }

        public Guid? DocTypeID { get; }

        public KrComponents KrComponents { get; }

        public ICardStoreExtensionContext CardStoreContext { get; }

        public ICardRepository CardRepositoryToCreateTasks { get; }

        public IObjectModelMapper ObjectModelMapper { get; }

        public IKrProcessRunner AsyncProcessRunner { get; }

        public IKrScope KrScope { get; }

        public KrSettingsLazy SettingsLazy { get; }

        public IKrProcessCache ProcessCache { get; }

        public IKrEventManager EventManager { get; }

        #endregion

        #region IWorkflowContext

        /// <inheritdoc />
        public CardStoreRequest Request => this.CardStoreContext.Request;

        /// <inheritdoc />
        public CardStoreRequest NextRequest { get; }

        /// <inheritdoc />
        public bool NextRequestPending { get; private set; } = false;

        /// <inheritdoc />
        public CardType CardType => this.CardStoreContext.CardType;

        /// <inheritdoc />
        public ICardMetadata CardMetadata => this.CardStoreContext.CardMetadata;

        /// <inheritdoc />
        public ISession Session => this.CardStoreContext.Session;

        /// <inheritdoc />
        public IDbScope DbScope => this.CardStoreContext.DbScope;

        /// <inheritdoc />
        public DateTime StoreDateTime => this.CardStoreContext.StoreDateTime ?? DateTime.MinValue;

        /// <inheritdoc />
        public ICardTaskHistoryManager TaskHistoryManager { get; }

        /// <inheritdoc />
        public ICardGetStrategy CardGetStrategy { get; }

        /// <inheritdoc />
        public IValidationResultBuilder ValidationResult => this.CardStoreContext.ValidationResult;

        /// <inheritdoc />
        public ISerializableObject Info { get; }

        /// <inheritdoc />
        public void NotifyNextRequestPending() => this.NextRequestPending = true;

        #endregion

    }
}