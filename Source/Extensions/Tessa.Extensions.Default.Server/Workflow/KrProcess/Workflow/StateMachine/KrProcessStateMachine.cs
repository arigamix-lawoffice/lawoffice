using System;
using System.Threading.Tasks;
using Tessa.Platform.Collections;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine
{
    public sealed class KrProcessStateMachine : IKrProcessStateMachine
    {
        #region fields

        private readonly ConcurrentContainer<string, Type> stateHandlers =
            new ConcurrentContainer<string, Type>();

        private readonly IUnityContainer unityContainer;

        #endregion

        #region constructor

        public KrProcessStateMachine(
            IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        #endregion

        #region public

        /// <inheritdoc />
        public IKrProcessStateMachine RegisterHandler(
            string stateName,
            Type type)
        {
            if (!this.unityContainer.IsRegistered(type))
            {
                throw new ArgumentException(
                    $"Type {type.FullName} is not registered in UnityContainer.{Environment.NewLine}" +
                    $"Add container.RegisterType<{nameof(IStateHandler)}, {type.Name}>() in your Registrator class.");
            }

            this.stateHandlers[stateName] = type;
            return this;
        }

        /// <inheritdoc />
        public IKrProcessStateMachine RegisterHandler<T>(
            string stateName)
            where T : IStateHandler
        {
            return this.RegisterHandler(stateName, typeof(T));
        }

        /// <inheritdoc />
        public async Task<IStateHandlerResult> HandleStateAsync(
            IStateHandlerContext context)
        {
            var state = context.State ?? KrProcessState.Default;

            IStateHandler handler;
            if (this.stateHandlers.TryGetValue(state.Name, out var handlerType))
            {
                handler = (IStateHandler)this.unityContainer.Resolve(handlerType);
            }
            else if (this.stateHandlers.TryGetValue(KrProcessState.Default.Name, out handlerType))
            {
                handler = (IStateHandler)this.unityContainer.Resolve(handlerType);
            }
            else
            {
                return StateHandlerResult.EmptyResult;
            }

            return await handler.HandleAsync(context);
        }

        #endregion
    }
}