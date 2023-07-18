#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Compilation;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform;
using Unity;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles.Compilation
{
    /// <inheritdoc cref="IKrVirtualFileCompilationCache"/>
    public sealed class KrVirtualFileCompilationCache :
        TessaCompilationObjectCacheBase<IKrVirtualFileCompilationContext, Guid, IKrVirtualFileScript>,
        IKrVirtualFileCompilationCache
    {
        #region Fields

        private readonly IKrVirtualFileCache krVirtualFileCache;

        private readonly ITypeProvider typeProvider;

        private readonly ITypeIdentifierProvider<Guid> typeIdentifierProvider;

        private readonly IInstanceCreationStrategy instanceCreationStrategy;

        private readonly IInstanceLifetimeManager instanceLifetimeManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="tessaCompilationObjectGlobalCache"><inheritdoc cref="TessaCompilationObjectGlobalCache" path="/summary"/></param>
        /// <param name="compiler"><inheritdoc cref="IKrVirtualFileCompiler" path="/summary"/></param>
        /// <param name="tessaCompilationRepository"><inheritdoc cref="ITessaCompilationRepository" path="/summary"/></param>
        /// <param name="krVirtualFileCache"><inheritdoc cref="IKrVirtualFileCache" path="/summary"/></param>
        /// <param name="typeProvider"><inheritdoc cref="ITypeProvider" path="/summary"/></param>
        /// <param name="typeIdentifierProvider"><inheritdoc cref="ITypeIdentifierProvider{T}" path="/summary"/></param>
        /// <param name="instanceCreationStrategy"><inheritdoc cref="IInstanceCreationStrategy" path="/summary"/></param>
        /// <param name="instanceLifetimeManager"><inheritdoc cref="IInstanceLifetimeManager" path="/summary"/></param>
        /// <param name="unityDisposableContainer"><inheritdoc cref="IUnityDisposableContainer" path="/summary"/></param>
        public KrVirtualFileCompilationCache(
            TessaCompilationObjectGlobalCache tessaCompilationObjectGlobalCache,
            IKrVirtualFileCompiler compiler,
            ITessaCompilationRepository tessaCompilationRepository,
            IKrVirtualFileCache krVirtualFileCache,
            ITypeProvider typeProvider,
            ITypeIdentifierProvider<Guid> typeIdentifierProvider,
            IInstanceCreationStrategy instanceCreationStrategy,
            [Dependency(InstanceLifetimeManagerNames.Singleton)] IInstanceLifetimeManager instanceLifetimeManager,
            [OptionalDependency] IUnityDisposableContainer? unityDisposableContainer = null)
            : base(
                  DefaultCompilationCacheNames.KrVirtualFile,
                  tessaCompilationObjectGlobalCache,
                  compiler,
                  tessaCompilationRepository,
                  unityDisposableContainer)
        {
            this.krVirtualFileCache = NotNullOrThrow(krVirtualFileCache);
            this.typeProvider = NotNullOrThrow(typeProvider);
            this.typeIdentifierProvider = NotNullOrThrow(typeIdentifierProvider);
            this.instanceCreationStrategy = NotNullOrThrow(instanceCreationStrategy);
            this.instanceLifetimeManager = NotNullOrThrow(instanceLifetimeManager);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async Task<TessaCompilationContext<IKrVirtualFileCompilationContext>> GetCompilerContextAsync(
            Guid id,
            Func<IKrVirtualFileCompilationContext> getCompilerContextFunc,
            CancellationToken cancellationToken = default)
        {
            var krVirtualFile = await this.krVirtualFileCache.TryGetAsync(
                id,
                cancellationToken);

            if (krVirtualFile is null)
            {
                return new TessaCompilationContext<IKrVirtualFileCompilationContext>(
                    id)
                {
                    ValidationResult = this.CreateSourceObjectNotFoundValidationResult(
                        id),
                };
            }

            var context = getCompilerContextFunc();

            context.ID = id;
            context.Name = krVirtualFile.Name;
            context.InitializationScenario = krVirtualFile.InitializationScenario;

            return new TessaCompilationContext<IKrVirtualFileCompilationContext>(
                id)
            {
                CompilerContext = context,
            };
        }

        /// <inheritdoc/>
        protected override async Task<IList<TessaCompilationContext<IKrVirtualFileCompilationContext>>> GetCompilerContextAsync(
            Func<IKrVirtualFileCompilationContext> getCompilerContextFunc,
            CancellationToken cancellationToken = default)
        {
            var krVirtualFiles = await this.krVirtualFileCache.GetAllAsync(
                cancellationToken);
            var contexts = new List<TessaCompilationContext<IKrVirtualFileCompilationContext>>(krVirtualFiles.Length);

            foreach (var krVirtualFile in krVirtualFiles)
            {
                var context = getCompilerContextFunc();

                context.ID = krVirtualFile.ID;
                context.Name = krVirtualFile.Name;
                context.InitializationScenario = krVirtualFile.InitializationScenario;

                contexts.Add(
                    new TessaCompilationContext<IKrVirtualFileCompilationContext>(
                        context.ID)
                    {
                        CompilerContext = context,
                    });
            }

            return contexts;
        }

        /// <inheritdoc/>
        protected override ValueTask<ITessaCompilationFactory<Guid, IKrVirtualFileScript>> CreateCompilationFactoryAsync(
            ITessaCompilationResult tessaCompilationResult,
            CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult<ITessaCompilationFactory<Guid, IKrVirtualFileScript>>(
                new TessaCompilationFactory<Guid, IKrVirtualFileScript>(
                    tessaCompilationResult.AssemblyBytes,
                    this.typeProvider,
                    this.typeIdentifierProvider,
                    this.instanceCreationStrategy,
                    this.instanceLifetimeManager));
        }

        #endregion
    }
}
