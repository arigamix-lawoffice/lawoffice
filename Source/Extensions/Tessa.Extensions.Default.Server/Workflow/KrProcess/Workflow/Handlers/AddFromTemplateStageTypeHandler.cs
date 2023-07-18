using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Files;
using Tessa.Platform.Data;
using Tessa.Platform.IO;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="StageTypeDescriptors.AddFromTemplateDescriptor"/>.
    /// </summary>
    public class AddFromTemplateStageTypeHandler : StageTypeHandlerBase
    {
        #region Properties

        /// <summary>
        /// Репозиторий для потокового управления карточками на сервере.
        /// </summary>
        protected ICardStreamServerRepository CardStreamRepository { get; }

        /// <summary>
        /// Объект, управляющий операциями с плейсхолдерами.
        /// </summary>
        protected IPlaceholderManager PlaceholderManager { get; }

        /// <summary>
        /// Объект для взаимодействия с базой данных.
        /// </summary>
        protected IDbScope DbScope { get; }

        /// <summary>
        /// unity-контейнер.
        /// </summary>
        protected IUnityContainer UnityContainer { get; }

        /// <summary>
        /// Сессия пользователя.
        /// </summary>
        protected ISession Session { get; }

        /// <summary>
        /// Объект, предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.
        /// </summary>
        protected IKrScope KrScope { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddFromTemplateStageTypeHandler"/>.
        /// </summary>
        /// <param name="cardStreamRepository">Репозиторий для потокового управления карточками на сервере.</param>
        /// <param name="placeholderManager">Объект, управляющий операциями с плейсхолдерами.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="session">Сессия пользователя.</param>
        /// <param name="unityContainer">Unity-контейнер.</param>
        /// <param name="krScope">Объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.</param>
        public AddFromTemplateStageTypeHandler(
            ICardStreamServerRepository cardStreamRepository,
            IPlaceholderManager placeholderManager,
            IDbScope dbScope,
            ISession session,
            IUnityContainer unityContainer,
            IKrScope krScope)
        {
            this.CardStreamRepository = cardStreamRepository;
            this.PlaceholderManager = placeholderManager;
            this.DbScope = dbScope;
            this.Session = session;
            this.UnityContainer = unityContainer;
            this.KrScope = krScope;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<StageHandlerResult> HandleStageStartAsync(
            IStageTypeHandlerContext context)
        {
            var templateID = context.Stage.SettingsStorage.TryGet<Guid?>(KrConstants.KrAddFromTemplateSettingsVirtual.FileTemplateID);
            var fileName = context.Stage.SettingsStorage.TryGet<string>(KrConstants.KrAddFromTemplateSettingsVirtual.Name);

            if (templateID.HasValue)
            {
                var result = await this.CardStreamRepository.GenerateFileFromTemplateAsync(
                    templateID.Value,
                    context.MainCardID,
                    info: new Dictionary<string, object>(StringComparer.Ordinal)
                    {
                        [PlaceholderHelper.CardFuncAsyncKey] =
                            new Func<IPlaceholderContext, ValueTask<Card>>(ctx => context.MainCardAccessStrategy.GetCardAsync(withoutTransaction: true, cancellationToken: context.CancellationToken)),
                    },
                    cancellationToken: context.CancellationToken);

                context.ValidationResult.Add(result.Response.ValidationResult);

                if (result.HasContent)
                {
                    var fileContainer = await context.MainCardAccessStrategy.GetFileContainerAsync(cancellationToken: context.CancellationToken);

                    if (fileContainer is null)
                    {
                        return StageHandlerResult.EmptyResult;
                    }

                    await using var s = await result.GetContentOrThrowAsync(context.CancellationToken);
                    var data = await s.ReadAllBytesAsync(context.CancellationToken);

                    await fileContainer
                        .FileContainer
                        .BuildFile(await this.GetFileNameAsync(context, result.Response.TryGetSuggestedFileName(), fileName))
                        .SetContent(data)
                        .AddWithNotificationAsync(cancellationToken: context.CancellationToken);
                }
            }

            return StageHandlerResult.CompleteResult;
        }

        #endregion

        #region Protected Mathods

        /// <summary>
        /// Возвращает имя создаваемого файла.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="suggestedName">Предпочитаемое имя файла, которое используется для загрузки предпросмотра или создания файла по шаблону, или <c>null</c>, если используется уже известное имя файла (то, которое задано в шаблоне).</param>
        /// <param name="fileNameTemplate">Имя файла заданное в шаблоне.</param>
        /// <returns>Имя создаваемого файла.</returns>
        protected async Task<string> GetFileNameAsync(
            IStageTypeHandlerContext context,
            string suggestedName,
            string fileNameTemplate)
        {
            if (string.IsNullOrWhiteSpace(fileNameTemplate))
            {
                return suggestedName;
            }

            var extension = Path.GetExtension(suggestedName);

            return await this.ExtendFileNameAsync(context, fileNameTemplate) + extension;
        }

        /// <summary>
        /// Заменяет плейсхолдеры в указаном имени файла.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="fileNameTemplate">Имя файла заданное в шаблоне.</param>
        /// <returns>Имя файла в котором заменены плейсхолдеры.</returns>
        protected async Task<string> ExtendFileNameAsync(IStageTypeHandlerContext context, string fileNameTemplate) =>
            await this.PlaceholderManager.ReplaceTextAsync(
                fileNameTemplate,
                this.Session,
                this.UnityContainer,
                this.DbScope,
                null,
                await context.MainCardAccessStrategy.GetCardAsync(cancellationToken: context.CancellationToken),
                info: CreatePlaceholderInfo(context),
                cancellationToken: context.CancellationToken);

        /// <summary>
        /// Создаёт дополнительную информацию, добавляемая в info замены плейсхолдеров.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Дополнительная информацию, добавляемая в info замены плейсхолдеров.</returns>
        protected static Dictionary<string, object> CreatePlaceholderInfo(IStageTypeHandlerContext context) =>
            new Dictionary<string, object>(StringComparer.Ordinal)
            {
                [PlaceholderHelper.TaskKey] = context.TaskInfo?.Task,
                ["WorkflowProcess"] = context.WorkflowProcess,
                ["Stage"] = context.Stage,
            };

        #endregion

    }
}
