#nullable enable

using System.Threading;
using Tessa.Cards;
using Tessa.Files;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Unity;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles.Compilation
{
    /// <inheritdoc cref="IKrVirtualFileScriptContext"/>
    public sealed class KrVirtualFileScriptContext :
        IKrVirtualFileScriptContext
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="container"><inheritdoc cref="Container" path="/summary"/></param>
        /// <param name="dbScope"><inheritdoc cref="DbScope" path="/summary"/></param>
        /// <param name="session"><inheritdoc cref="Session" path="/summary"/></param>
        /// <param name="card"><inheritdoc cref="Card" path="/summary"/></param>
        /// <param name="file"><inheritdoc cref="File" path="/summary"/></param>
        /// <param name="cardFile"><inheritdoc cref="CardFile" path="/summary"/></param>
        public KrVirtualFileScriptContext(
            IUnityContainer container,
            IDbScope dbScope,
            ISession session,
            Card card,
            IFile file,
            CardFile cardFile)
        {
            this.Container = NotNullOrThrow(container);
            this.DbScope = NotNullOrThrow(dbScope);
            this.Session = NotNullOrThrow(session);
            this.Card = NotNullOrThrow(card);
            this.File = NotNullOrThrow(file);
            this.CardFile = NotNullOrThrow(cardFile);
        }

        #endregion

        #region IKrVirtualFileScriptContext Implementation

        /// <inheritdoc/>
        public IUnityContainer Container { get; }

        /// <inheritdoc/>
        public IDbScope DbScope { get; }

        /// <inheritdoc/>
        public ISession Session { get; }

        /// <inheritdoc/>
        public Card Card { get; }

        /// <inheritdoc/>
        public IFile File { get; }

        /// <inheritdoc/>
        public CardFile CardFile { get; }

        /// <inheritdoc/>
        public CancellationToken CancellationToken { get; set; }

        #endregion
    }
}
