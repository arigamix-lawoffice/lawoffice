#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Unity;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <inheritdoc cref="IKrProcessUIContainer"/>
    public sealed class KrProcessUIContainer :
        IKrProcessUIContainer
    {
        #region Constants And Static Fields

        /// <summary>
        /// Дескриптор фейкового типа этапа. Используется для регистрации UI обработчиков этапов, выполняющихся при обработке любого типа этапа.
        /// </summary>
        private readonly StageTypeDescriptor allStagesFakeDescriptor = StageTypeDescriptor.Create(
            static b => b.ID = new Guid(0x92D8F95C, 0x96FA, 0x484A, 0xA6, 0x50, 0xC8, 0x80, 0xC4, 0x4A, 0xC6, 0x6A));

        #endregion

        #region Fields

        private readonly IUnityContainer unityContainer;
        private readonly ConcurrentDictionary<Guid, List<Type>> stageTypeUIHandlers =
            new ConcurrentDictionary<Guid, List<Type>>();

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="unityContainer">Unity-контейнер.</param>
        public KrProcessUIContainer(IUnityContainer unityContainer) =>
            this.unityContainer = NotNullOrThrow(unityContainer);

        #endregion

        #region IKrProcessUIContainer Members

        /// <inheritdoc />
        public IKrProcessUIContainer RegisterUIHandler<T>()
            where T : IStageTypeUIHandler =>
            this.RegisterUIHandler<T>(this.allStagesFakeDescriptor);

        /// <inheritdoc />
        public IKrProcessUIContainer RegisterUIHandler<T>(StageTypeDescriptor descriptor)
            where T : IStageTypeUIHandler
        {
            ThrowIfNull(descriptor);

            this.CheckRegistered(typeof(T));
            this.RegisterUIHandlerInternal(descriptor, typeof(T));
            return this;
        }

        /// <inheritdoc />
        public IKrProcessUIContainer RegisterUIHandler(
            Type handlerType) =>
            this.RegisterUIHandler(this.allStagesFakeDescriptor, handlerType);

        /// <inheritdoc />
        public IKrProcessUIContainer RegisterUIHandler(
            StageTypeDescriptor descriptor,
            Type handlerType)
        {
            ThrowIfNull(descriptor);
            ThrowIfNull(handlerType);

            if (!handlerType.Implements<IStageTypeUIHandler>())
            {
                throw new ArgumentException(
                    $"Type {handlerType.FullName} is not implemented {typeof(IStageTypeUIHandler).FullName}.",
                    nameof(handlerType));
            }

            this.CheckRegistered(handlerType);
            this.RegisterUIHandlerInternal(descriptor, handlerType);
            return this;
        }

        /// <inheritdoc />
        public List<IStageTypeUIHandler> ResolveUIHandlers(Guid descriptorID)
        {
            var handlers = new List<IStageTypeUIHandler>();
            if (this.stageTypeUIHandlers.TryGetValue(this.allStagesFakeDescriptor.ID, out var allStagesHandlers))
            {
                foreach (var item in allStagesHandlers)
                {
                    handlers.Add((IStageTypeUIHandler) this.unityContainer.Resolve(item));
                }
            }

            if (this.stageTypeUIHandlers.TryGetValue(descriptorID, out var handlerRegistrationForDescriptor))
            {
                foreach (var item in handlerRegistrationForDescriptor)
                {
                    handlers.Add((IStageTypeUIHandler) this.unityContainer.Resolve(item));
                }
            }

            return handlers;
        }

        #endregion

        #region Private Methods

        private void RegisterUIHandlerInternal(
            StageTypeDescriptor descriptor,
            Type handlerType)
        {
            var registrationHandlers = this.stageTypeUIHandlers.GetOrAdd(descriptor.ID, new List<Type>());
            registrationHandlers.Add(handlerType);
        }

        private void CheckRegistered(
            Type handlerType)
        {
            if (!this.unityContainer.IsRegistered(handlerType))
            {
                throw new ArgumentException(
                    $"Type {handlerType.FullName} is not registered in UnityContainer.{Environment.NewLine}" +
                    $"Add container.RegisterType<{nameof(IStageTypeUIHandler)}, {handlerType.Name}>() in your Registrator class.");
            }
        }

        #endregion

    }
}
