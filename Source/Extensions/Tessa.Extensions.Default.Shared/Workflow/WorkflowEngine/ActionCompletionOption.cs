using System;

using Tessa.Platform;

namespace Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine
{
    /// <summary>
    /// Предоставляет информацию о варианте завершения действия.
    /// </summary>
    public sealed class ActionCompletionOption
    {
        /// <summary>
        /// Возвращает идентификатор варианта завершения действия.
        /// </summary>
        public Guid ID { get; }

        /// <summary>
        /// Возвращает имя варианта завершения действия.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Возвращает отображаемое имя варианта завершения действия.
        /// </summary>
        public string Caption { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ActionCompletionOption"/>.
        /// </summary>
        /// <param name="id">Идентификатор варианта завершения действия.</param>
        /// <param name="name">Имя варианта завершения действия.</param>
        /// <param name="caption">Отображаемое имя варианта завершения действия.</param>
        public ActionCompletionOption(
            Guid id,
            string name,
            string caption)
        {
            Check.ArgumentNotNull(name, nameof(name));
            Check.ArgumentNotNull(caption, nameof(caption));

            this.ID = id;
            this.Name = name;
            this.Caption = caption;
        }
    }
}
