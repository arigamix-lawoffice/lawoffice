using System;
using Tessa.Platform.Collections;
using Unity;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    public class StageTypeFormatterContainer : IStageTypeFormatterContainer
    {
        #region nested types

        private struct RegistrationItem
        {
            public StageTypeDescriptor Descriptor;
            public Type Type;
        }

        #endregion

        #region fields

        private readonly IUnityContainer unityContainer;
        private readonly HashSet<Guid, RegistrationItem> items =
            new HashSet<Guid, RegistrationItem>(p => p.Descriptor.ID);

        #endregion

        #region constructor

        public StageTypeFormatterContainer(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        #endregion

        #region implementation

        /// <inheritdoc />
        public IStageTypeFormatterContainer RegisterFormatter<T>(StageTypeDescriptor descriptor)
            where T : IStageTypeFormatter
        {
            if (!this.unityContainer.IsRegistered<T>())
            {
                throw new ArgumentException(
                    $"Type {typeof(T).FullName} is not registered in UnityContainer.{Environment.NewLine}" +
                    $"Add container.RegisterType<{nameof(IStageTypeFormatter)}, {typeof(T).Name}>() in your Registrator class.");
            }
            this.RegisterFormatterInternal(descriptor, typeof(T));
            return this;
        }

        /// <inheritdoc />
        public IStageTypeFormatterContainer RegisterFormatter(
            StageTypeDescriptor descriptor,
            Type formatterType)
        {
            if (!this.unityContainer.IsRegistered(formatterType))
            {
                throw new ArgumentException(
                    $"Type {formatterType.FullName} is not registered in UnityContainer.{Environment.NewLine}" +
                    $"Add container.RegisterType<{nameof(IStageTypeFormatter)}, {formatterType.Name}>() in your Registrator class.");
            }
            this.RegisterFormatterInternal(descriptor, formatterType);
            return this;
        }

        /// <inheritdoc />
        public IStageTypeFormatter ResolveFormatter(Guid descriptorID)
        {
            if (this.items.TryGetItem(descriptorID, out var item))
            {
                return (IStageTypeFormatter)this.unityContainer.Resolve(item.Type);
            }
            return null;
        }

        #endregion

        #region private

        private void RegisterFormatterInternal(StageTypeDescriptor descriptor, Type t)
        {
            this.items.Replace(new RegistrationItem
            {
                Descriptor = descriptor,
                Type = t,
            });
        }

        #endregion
    }
}