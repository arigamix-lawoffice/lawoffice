using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Unity;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter
{
    /// <summary>
    /// Представляет интерпретатор клиентских команд.
    /// </summary>
    public sealed class ClientCommandInterpreter : IClientCommandInterpreter
    {
        #region fields

        private readonly ConcurrentDictionary<string, Type> handlers = 
            new ConcurrentDictionary<string, Type>();

        private Lazy<ReadOnlyDictionary<string, Type>> handlersRoLazy;

        private readonly IUnityContainer unityContainer;

        #endregion

        #region constructor

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClientCommandInterpreter"/>.
        /// </summary>
        /// <param name="unityContainer">Unity контейнер.</param>
        public ClientCommandInterpreter(
            IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
            this.InitLazy();
        }

        #endregion

        #region implementation

        /// <inheritdoc />
        public IClientCommandInterpreter RegisterHandler(
            string commandType,
            Type handlerType)
        {
            if (!handlerType.Implements<IClientCommandHandler>())
            {
                throw new ArgumentException($"handlerType doesn't implement {nameof(IClientCommandHandler)}.");
            }
            this.handlers[commandType] = handlerType;
            this.InitLazy();
            return this;
        }

        /// <inheritdoc />
        public IClientCommandInterpreter RegisterHandler<T>(
            string commandType) where T: IClientCommandHandler
        {
            this.RegisterHandler(commandType, typeof(T));
            this.InitLazy();
            return this;
        }

        /// <inheritdoc />
        public async Task InterpretAsync(
            IEnumerable<KrProcessClientCommand> commands,
            object context,
            CancellationToken cancellationToken = default)
        {
            var ctx = new ClientCommandHandlerContext
            {
                OuterContext = context,
                Info = new Dictionary<string, object>(),
                CancellationToken = cancellationToken
            };
            
            var handlerRo = this.handlersRoLazy.Value;
            foreach (var command in commands)
            {
                if (!handlerRo.TryGetValue(command.CommandType, out var handlerType))
                {
                    continue;
                }

                ctx.Command = command;
                var handler = this.unityContainer.Resolve(handlerType);
                await ((IClientCommandHandler)handler).Handle(ctx);
            }
        }

        #endregion

        #region private

        private void InitLazy()
        {
            this.handlersRoLazy = new Lazy<ReadOnlyDictionary<string, Type>>(
                () => new ReadOnlyDictionary<string, Type>(this.handlers.ToDictionary(k => k.Key, v => v.Value)),
                LazyThreadSafetyMode.PublicationOnly);
        }

        #endregion
    }
}