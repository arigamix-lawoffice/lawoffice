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
    public sealed partial class MainProcessCommonInfo : ProcessCommonInfo
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MainProcessCommonInfo"/>.
        /// </summary>
        /// <param name="currentStageRowID">Идентификатор текущего этапа.</param>
        /// <param name="info">Дополнительная информация по процессу.</param>
        /// <param name="secondaryProcessID">Идентификатор вторичного процесса.</param>
        /// <param name="authorID">Идентификатор инициатора процесса.</param>
        /// <param name="authorName">Имя инициатора процесса.</param>
        /// <param name="authorComment">Комментарий инициатора процесса.</param>
        /// <param name="state">Идентификатор состояния.</param>
        /// <param name="processOwnerID">Идентификатор владельца процесса.</param>
        /// <param name="processOwnerName">Имя владельца процесса.</param>
        public MainProcessCommonInfo(
            Guid? currentStageRowID,
            IDictionary<string, object> info,
            Guid? secondaryProcessID,
            Guid? authorID,
            string authorName,
            string authorComment,
            int state,
            Guid? processOwnerID,
            string processOwnerName)
            : base(
                  currentStageRowID,
                  info,
                  secondaryProcessID,
                  authorID,
                  authorName,
                  processOwnerID,
                  processOwnerName)
        {
            this.Init(nameof(this.AuthorComment), authorComment);
            this.Init(nameof(this.AuthorCommentTimestamp), Int64Boxes.Zero);
            this.Init(nameof(this.State), Int32Boxes.Box(state));
            this.Init(nameof(this.StateTimestamp), Int64Boxes.Zero);
            this.Init(nameof(this.AffectMainCardVersionWhenStateChanged), BooleanBoxes.True);
            this.Init(nameof(this.AffectMainCardVersionWhenStateChangedTimestamp), Int64Boxes.Zero);
        }

        /// <inheritdoc />
        public MainProcessCommonInfo(
            Dictionary<string, object> storage)
            : base(storage)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает или задаёт комментарий инициатора процесса.
        /// </summary>
        public string AuthorComment
        {
            get => this.Get<string>(nameof(this.AuthorComment));
            set => this.Set(nameof(this.AuthorComment), value);
        }

        /// <summary>
        /// Возвращает или задаёт штамп времени изменения комментарий инициатора процесса.
        /// </summary>
        public long AuthorCommentTimestamp
        {
            get => this.Get<long>(nameof(this.AuthorCommentTimestamp));
            set => this.Set(nameof(this.AuthorCommentTimestamp), value);
        }

        /// <summary>
        /// Возвращает или задаёт состояние процесса.
        /// </summary>
        public int State
        {
            get => this.Get<int>(nameof(this.State));
            set => this.Set(nameof(this.State), value);
        }

        /// <summary>
        /// Возвращает или задаёт штамп времени изменения состояния процесса.
        /// </summary>
        public long StateTimestamp
        {
            get => this.Get<long>(nameof(this.StateTimestamp));
            set => this.Set(nameof(this.StateTimestamp), value);
        }

        /// <summary>
        /// Возвращает или задаёт признак, показывающий, что версия основной карточки должна быть изменена, если состояние документа изменилось.
        /// </summary>
        public bool AffectMainCardVersionWhenStateChanged
        {
            get => this.Get<bool>(nameof(this.AffectMainCardVersionWhenStateChanged));
            set => this.Set(nameof(this.AffectMainCardVersionWhenStateChanged), value);
        }

        /// <summary>
        /// Возвращает или задаёт штамп времени изменения флага версии основной карточки.
        /// </summary>
        public long AffectMainCardVersionWhenStateChangedTimestamp
        {
            get => this.Get<long>(nameof(this.AffectMainCardVersionWhenStateChangedTimestamp));
            set => this.Set(nameof(this.AffectMainCardVersionWhenStateChangedTimestamp), value);
        }

        #endregion

    }
}
