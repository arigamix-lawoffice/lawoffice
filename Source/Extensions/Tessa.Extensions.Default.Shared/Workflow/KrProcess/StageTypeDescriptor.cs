using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Дескриптор типа этапа.
    /// </summary>
    public sealed class StageTypeDescriptor
    {
        #region Fields

        private readonly Guid id;
        private readonly string caption;

        #endregion

        #region Constructors

        private StageTypeDescriptor(
            Guid id,
            string caption,
            string defaultStageName,
            Guid? settingsCardTypeID,
            PerformerUsageMode performerUsageMode,
            bool performerIsRequired,
            string performerCaption,
            bool canOverrideAuthor,
            bool useTimeLimit,
            bool usePlanned,
            bool canBeHidden,
            bool canOverrideTaskHistoryGroup,
            bool useTaskKind,
            List<KrProcessRunnerMode> supportedModes,
            bool canBeSkipped)
        {
            this.id = id;
            this.caption = caption;
            this.DefaultStageName = defaultStageName ?? string.Empty;
            this.SettingsCardTypeID = settingsCardTypeID;
            this.PerformerUsageMode = performerUsageMode;
            this.PerformerIsRequired = performerIsRequired;
            this.PerformerCaption = performerCaption;
            this.CanOverrideAuthor = canOverrideAuthor;
            this.UseTimeLimit = useTimeLimit;
            this.UsePlanned = usePlanned;
            this.CanBeHidden = canBeHidden;
            this.CanOverrideTaskHistoryGroup = canOverrideTaskHistoryGroup;
            this.UseTaskKind = useTaskKind;
            this.SupportedModes = supportedModes.AsReadOnly();
            this.CanBeSkipped = canBeSkipped;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Уникальный идентификатор дескриптора типа этапа.
        /// </summary>
        public Guid ID
        {
            get
            {
                this.AssertInitialized(this.id, nameof(this.ID));
                return this.id;
            }
        }

        /// <summary>
        /// Название типа этапа.
        /// </summary>
        public string Caption
        {
            get
            {
                this.AssertInitialized(this.caption, nameof(this.Caption));
                return this.caption;
            }
        }

        /// <summary>
        /// Стандартное название для нового этапа.
        /// </summary>
        public string DefaultStageName { get; }

        /// <summary>
        /// Идентификатор типа карточки настроек.
        /// </summary>
        public Guid? SettingsCardTypeID { get; }

        /// <summary>
        /// Режим использования стандартного поля с исполнителями.
        /// </summary>
        public PerformerUsageMode PerformerUsageMode { get; }

        /// <summary>
        /// Проверить наличие хотя бы одного исполнителя при редактировании в UI и перед стартом этапа.
        /// </summary>
        public bool PerformerIsRequired { get; }

        /// <summary>
        /// Заголовок элемента управления исполнителя/исполнителей.
        /// </summary>
        public string PerformerCaption { get; }

        /// <summary>
        /// Использовать поле "От имени"
        /// </summary>
        public bool CanOverrideAuthor { get; }

        /// <summary>
        /// Использовать поле "Срок"
        /// </summary>
        public bool UseTimeLimit { get; }

        /// <summary>
        /// Использовать поле "Дата выполнения"
        /// </summary>
        public bool UsePlanned { get; }

        /// <summary>
        /// Разрешить скрывать этап в документах.
        /// </summary>
        public bool CanBeHidden { get; }

        /// <summary>
        /// Разрешить переопределять группу истории заданий в настройках этапа.
        /// </summary>
        public bool CanOverrideTaskHistoryGroup { get; }

        /// <summary>
        /// Разрешить выбирать вид задания.
        /// </summary>
        public bool UseTaskKind { get; }

        /// <summary>
        /// Поддерживаемые режимы выполнения.
        /// </summary>
        public IReadOnlyList<KrProcessRunnerMode> SupportedModes { get; }

        /// <summary>
        /// Возвращает значение, показывающее, разрешён ли пропуск этапа.
        /// </summary>
        public bool CanBeSkipped { get; }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override string ToString() => $"{nameof(this.ID)} = {this.ID:B}, {nameof(this.Caption)} = \"{this.Caption}\"";

        #endregion

        #region Private Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void AssertInitialized<T>(T val, string fieldName)
        {
            if (Equals(val, default(T)))
            {
                throw new StageTypeDescriptorNotInitializedException(this.id, this.caption, fieldName);
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Создать новый дескриптор.
        /// </summary>
        /// <param name="action">Метод, модифицирующий новый дескриптор.</param>
        /// <returns>Новый дескриптор.</returns>
        public static StageTypeDescriptor Create(
            Action<StageTypeDescriptorBuilder> action)
        {
            Check.ArgumentNotNull(action, nameof(action));

            var builder = new StageTypeDescriptorBuilder();
            action(builder);
            return new StageTypeDescriptor(
                builder.ID,
                builder.Caption,
                builder.DefaultStageName,
                builder.SettingsCardTypeID,
                builder.PerformerUsageMode,
                builder.PerformerIsRequired,
                builder.PerformerCaption,
                builder.CanOverrideAuthor,
                builder.UseTimeLimit,
                builder.UsePlanned,
                builder.CanBeHidden,
                builder.CanOverrideTaskHistoryGroup,
                builder.UseTaskKind,
                builder.SupportedModes,
                builder.CanBeSkipped);
        }

        /// <summary>
        /// Создать дескриптор на основании существующего.
        /// Переопределение идентификатора <see cref="StageTypeDescriptor.ID"/> является обязательным.
        /// </summary>
        /// <param name="oldDescriptor">Дескриптор, на основании которого нужно создать новый.</param>
        /// <param name="action">Метод, модифицирующий новый дескриптор.</param>
        /// <returns>Новый дескриптор.</returns>
        public static StageTypeDescriptor CreateBasedOn(
            StageTypeDescriptor oldDescriptor,
            Action<StageTypeDescriptorBuilder> action)
        {
            Check.ArgumentNotNull(oldDescriptor, nameof(oldDescriptor));
            Check.ArgumentNotNull(action, nameof(action));

            var builder = new StageTypeDescriptorBuilder
            {
                // ID обязательно должно быть новым
                Caption = oldDescriptor.Caption,
                DefaultStageName = oldDescriptor.DefaultStageName,
                SettingsCardTypeID = oldDescriptor.SettingsCardTypeID,
                PerformerUsageMode = oldDescriptor.PerformerUsageMode,
                PerformerIsRequired = oldDescriptor.PerformerIsRequired,
                CanOverrideAuthor = oldDescriptor.CanOverrideAuthor,
                UseTimeLimit = oldDescriptor.UseTimeLimit,
                UsePlanned = oldDescriptor.UsePlanned,
                CanBeHidden = oldDescriptor.CanBeHidden,
                CanOverrideTaskHistoryGroup = oldDescriptor.CanOverrideTaskHistoryGroup,
                UseTaskKind = oldDescriptor.UseTaskKind,
                SupportedModes = oldDescriptor.SupportedModes.ToList(),
                CanBeSkipped = oldDescriptor.CanBeSkipped
            };

            action(builder);
            return new StageTypeDescriptor(
                builder.ID,
                builder.Caption,
                builder.DefaultStageName,
                builder.SettingsCardTypeID,
                builder.PerformerUsageMode,
                builder.PerformerIsRequired,
                builder.PerformerCaption,
                builder.CanOverrideAuthor,
                builder.UseTimeLimit,
                builder.UsePlanned,
                builder.CanBeHidden,
                builder.CanOverrideTaskHistoryGroup,
                builder.UseTaskKind,
                builder.SupportedModes,
                builder.CanBeSkipped);
        }

        #endregion
    }
}
