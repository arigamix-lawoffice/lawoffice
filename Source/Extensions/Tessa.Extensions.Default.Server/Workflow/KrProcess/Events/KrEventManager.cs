using System;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Events
{
    /// <summary>
    /// Объект предоставляющий методы для отправки событий маршрутов документов.
    /// </summary>
    public sealed class KrEventManager : IKrEventManager
    {
        #region Fields

        private readonly IExtensionContainer extensionContainer;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует ноыый экземпляр класса <see cref="KrEventManager"/>.
        /// </summary>
        /// <param name="extensionContainer">Контейнер расширений.</param>
        public KrEventManager(IExtensionContainer extensionContainer) =>
            this.extensionContainer = extensionContainer ?? throw new ArgumentNullException(nameof(extensionContainer));

        #endregion

        #region IKrEventManager Members

        /// <inheritdoc />
        public async Task RaiseAsync(IKrEventExtensionContext context)
        {
            await using var executor = await this.extensionContainer.ResolveExecutorAsync<IKrEventExtension>(context.CancellationToken);
            await executor.ExecuteAsync(nameof(IKrEventExtension.HandleEvent), context);
        }

        #endregion
    }
}