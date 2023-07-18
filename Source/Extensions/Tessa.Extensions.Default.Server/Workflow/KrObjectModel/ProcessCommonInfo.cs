using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrObjectModel
{
    /// <summary>
    /// Предоставляет информацию о процессе.
    /// </summary>
    [StorageObjectGenerator(GenerateDefaultConstructor = false)]
    public abstract partial class ProcessCommonInfo : StorageObject
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProcessCommonInfo"/>.
        /// </summary>
        /// <param name="currentStageRowID">Идентификатор текущего этапа.</param>
        /// <param name="info">Дополнительная информация по процессу.</param>
        /// <param name="secondaryProcessID">Идентификатор вторичного процесса.</param>
        /// <param name="authorID">Идентификатор инициатора процесса.</param>
        /// <param name="authorName">Имя инициатора процесса.</param>
        /// <param name="processOwnerID">Идентификатор владельца процесса.</param>
        /// <param name="processOwnerName">Имя владельца процесса.</param>
        protected ProcessCommonInfo(
            Guid? currentStageRowID,
            IDictionary<string, object> info,
            Guid? secondaryProcessID,
            Guid? authorID,
            string authorName,
            Guid? processOwnerID,
            string processOwnerName)
            : base(new Dictionary<string, object>(StringComparer.Ordinal))
        {
            this.Init(nameof(this.CurrentStageRowID), currentStageRowID);
            this.Init(nameof(this.Info), info);
            this.Init(nameof(this.SecondaryProcessID), secondaryProcessID);
            this.Init(nameof(this.AuthorID), authorID);
            this.Init(nameof(this.AuthorName), authorName);
            this.Init(nameof(this.AuthorTimestamp), Int64Boxes.Zero);
            this.Init(nameof(this.ProcessOwnerID), processOwnerID);
            this.Init(nameof(this.ProcessOwnerName), processOwnerName);
            this.Init(nameof(this.ProcessOwnerTimestamp), Int64Boxes.Zero);
        }

        /// <inheritdoc />
        protected ProcessCommonInfo(
            Dictionary<string, object> storage)
            : base(storage)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает или задаёт идентификатор текущего этапа.
        /// </summary>
        public Guid? CurrentStageRowID
        {
            get => this.Get<Guid?>(nameof(this.CurrentStageRowID));
            set => this.Set(nameof(this.CurrentStageRowID), value);
        }

        /// <summary>
        /// Возвращает или задаёт дополнительную информацию по процессу.
        /// </summary>
        public IDictionary<string, object> Info
        {
            get => this.TryGet<IDictionary<string, object>>(nameof(this.Info), _ => new Dictionary<string, object>(StringComparer.Ordinal));
            set => this.Set(nameof(this.Info), value);
        }

        /// <summary>
        /// Возвращает или задаёт идентификатор вторичного процесса.
        /// </summary>
        public Guid? SecondaryProcessID
        {
            get => this.TryGet<Guid?>(nameof(this.SecondaryProcessID));
            set => this.Set(nameof(this.SecondaryProcessID), value);
        }

        /// <summary>
        /// Возвращает или задаёт идентификатор инициатора процесса.
        /// </summary>
        public virtual Guid? AuthorID
        {
            get => this.Get<Guid?>(nameof(this.AuthorID));
            set => this.Set(nameof(this.AuthorID), value);
        }

        /// <summary>
        /// Возвращает или задаёт имя инициатора процесса.
        /// </summary>
        public virtual string AuthorName
        {
            get => this.Get<string>(nameof(this.AuthorName));
            set => this.Set(nameof(this.AuthorName), value);
        }

        /// <summary>
        /// Возвращает или задаёт штамп времени изменения инициатора процесса.
        /// </summary>
        public virtual long AuthorTimestamp
        {
            get => this.Get<long>(nameof(this.AuthorTimestamp));
            set => this.Set(nameof(this.AuthorTimestamp), value);
        }

        /// <summary>
        /// Возвращает или задаёт идентификатор владельца процесса.
        /// </summary>
        public virtual Guid? ProcessOwnerID
        {
            get => this.Get<Guid?>(nameof(this.ProcessOwnerID));
            set => this.Set(nameof(this.ProcessOwnerID), value);
        }

        /// <summary>
        /// Возвращает или задаёт имя владельца процесса.
        /// </summary>
        public virtual string ProcessOwnerName
        {
            get => this.Get<string>(nameof(this.ProcessOwnerName));
            set => this.Set(nameof(this.ProcessOwnerName), value);
        }

        /// <summary>
        /// Возвращает или задаёт штамп времени изменения владельца процесса.
        /// </summary>
        public virtual long ProcessOwnerTimestamp
        {
            get => this.Get<long>(nameof(this.ProcessOwnerTimestamp));
            set => this.Set(nameof(this.ProcessOwnerTimestamp), value);
        }

        #endregion
    }
}
