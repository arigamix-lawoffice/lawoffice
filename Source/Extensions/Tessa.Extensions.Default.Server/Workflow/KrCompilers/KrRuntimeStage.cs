using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <inheritdoc cref="IKrRuntimeStage"/>
    public sealed class KrRuntimeStage :
        IKrRuntimeStage
    {
        #region Fields

        private readonly AsyncLazy<IDictionary<string, object>> settingsLazy;

        private readonly Lazy<IReadOnlyList<IExtraSource>> extraSourcesLazy;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="IKrRuntimeStage"/>.
        /// </summary>
        /// <param name="templateID">Идентификатор шаблона этапов.</param>
        /// <param name="templateName">Название шаблона этапов.</param>
        /// <param name="groupID">Идентификатор группы, в которой находится шаблон, в котором находится этап.</param>
        /// <param name="groupName">Имя группы этапов, в которой находится шаблон, в котором находится этап.</param>
        /// <param name="groupOrder">Порядок группы, в которой находится шаблон, в котором находится этап.</param>
        /// <param name="stageID">Идентификатор этапа.</param>
        /// <param name="stageName">Название этапа.</param>
        /// <param name="order">Порядок этапа в шаблоне.</param>
        /// <param name="timeLimit">Срок выполнения (рабочие дни).</param>
        /// <param name="planned">Дата выполнения.</param>
        /// <param name="hidden">Этап является скрытым.</param>
        /// <param name="stageTypeID">Идентификатор типа этапа.</param>
        /// <param name="stageTypeCaption">Отображаемое название типа этапа.</param>
        /// <param name="sqlRoles">Запрос для вычисления SQL исполнителей.</param>
        /// <param name="settings">Настройки этапа, сериализованные в виде JSON.</param>
        /// <param name="extraSources">Список дополнительных методов, сериализованный в виде JSON.</param>
        /// <param name="runtimeSqlCondition">Текст SQL запроса условия времени выполнения.</param>
        /// <param name="sourceCondition">C# код условия времени выполнения.</param>
        /// <param name="sourceBefore">C# код сценария инициализации времени выполнения.</param>
        /// <param name="sourceAfter">C# код сценария постобработки времени выполнения.</param>
        /// <param name="skip">Флаг пропуска этапа.</param>
        /// <param name="canBeSkipped">Возвращает значение признака, показывающего, разрешено ли пропускать этап.</param>
        /// <param name="extraSourceSerializer">Сериализатор объектов, содержащих информацию о дополнительных методах.</param>
        /// <param name="serializer">Объект, предоставляющий методы для сериализации параметров этапов.</param>
        public KrRuntimeStage(
            Guid templateID,
            string templateName,
            Guid groupID,
            string groupName,
            int groupOrder,
            Guid stageID,
            string stageName,
            int? order,
            double? timeLimit,
            DateTime? planned,
            bool hidden,
            Guid stageTypeID,
            string stageTypeCaption,
            string sqlRoles,
            string settings,
            string extraSources,
            string runtimeSqlCondition,
            string sourceCondition,
            string sourceBefore,
            string sourceAfter,
            bool skip,
            bool canBeSkipped,
            IExtraSourceSerializer extraSourceSerializer,
            IKrStageSerializer serializer)
        {
            ThrowIfNull(extraSourceSerializer);
            ThrowIfNull(serializer);

            this.TemplateID = templateID;
            this.TemplateName = templateName;
            this.GroupID = groupID;
            this.GroupName = groupName;
            this.GroupOrder = groupOrder;
            this.StageID = stageID;
            this.StageName = stageName;
            this.Order = order;
            this.TimeLimit = timeLimit;
            this.Planned = planned;
            this.Hidden = hidden;
            this.StageTypeID = stageTypeID;
            this.StageTypeCaption = stageTypeCaption;
            this.SqlRoles = sqlRoles ?? string.Empty;
            this.RuntimeSqlCondition = runtimeSqlCondition ?? string.Empty;
            this.RuntimeSourceCondition = sourceCondition ?? string.Empty;
            this.RuntimeSourceBefore = sourceBefore ?? string.Empty;
            this.RuntimeSourceAfter = sourceAfter ?? string.Empty;
            this.Skip = skip;
            this.CanBeSkipped = canBeSkipped;

            this.extraSourcesLazy = new Lazy<IReadOnlyList<IExtraSource>>(
                () => extraSourceSerializer
                    .Deserialize(extraSources)
                    .Select(static p => p.ToReadonly())
                    .ToList()
                    .AsReadOnly(),
                LazyThreadSafetyMode.PublicationOnly);
            this.settingsLazy = new AsyncLazy<IDictionary<string, object>>(
                async () => await serializer.DeserializeSettingsStorageAsync(settings, this.StageID));
        }

        #endregion

        #region IKrRuntimeStage Members

        /// <inheritdoc />
        public Guid TemplateID { get; }

        /// <inheritdoc />
        public string TemplateName { get; }

        /// <inheritdoc />
        public Guid GroupID { get; }

        /// <inheritdoc />
        public string GroupName { get; }

        /// <inheritdoc />
        public int GroupOrder { get; }

        /// <inheritdoc />
        public Guid StageID { get; }

        /// <inheritdoc />
        public string StageName { get; }

        /// <inheritdoc />
        public int? Order { get; }

        /// <inheritdoc />
        public double? TimeLimit { get; }

        /// <inheritdoc />
        public DateTime? Planned { get; }

        /// <inheritdoc />
        public bool Hidden { get; }

        /// <inheritdoc />
        public Guid StageTypeID { get; }

        /// <inheritdoc />
        public string StageTypeCaption { get; }

        /// <inheritdoc />
        public string SqlRoles { get; }

        /// <inheritdoc />
        public async ValueTask<IDictionary<string, object>> GetSettingsAsync() => StorageHelper.Clone(await this.settingsLazy.Value);

        /// <inheritdoc />
        public string RuntimeSqlCondition { get; }

        /// <inheritdoc />
        public string RuntimeSourceCondition { get; }

        /// <inheritdoc />
        public string RuntimeSourceBefore { get; }

        /// <inheritdoc />
        public string RuntimeSourceAfter { get; }

        /// <inheritdoc />
        public IReadOnlyList<IExtraSource> ExtraSources => this.extraSourcesLazy.Value;

        /// <inheritdoc />
        public bool Skip { get; }

        /// <inheritdoc />
        public bool CanBeSkipped { get; }

        #endregion
    }
}
