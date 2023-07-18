#nullable enable

using System.Threading;
using System.Threading.Tasks;
using Tessa.Compilation;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Базовая абстрактная реализация <see cref="IKrCompilationCacheBase"/>.
    /// </summary>
    public abstract class KrCompilationCacheBase :
        TessaCompilationObjectCacheBase<IKrCompilationContext, string, IKrScript>,
        IKrCompilationCacheBase
    {
        #region Fields

        private readonly ITypeProvider typeProvider;

        private readonly ITypeIdentifierProvider<string> typeIdentifierProvider;

        private readonly IInstanceCreationStrategy instanceCreationStrategy;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="categoryID"><inheritdoc cref="TessaCompilationObjectCacheCoreBase{TCompilerContext, TKey, TInstance}.CategoryID" path="/summary"/></param>
        /// <param name="tessaCompilationObjectGlobalCache"><inheritdoc cref="TessaCompilationObjectGlobalCache" path="/summary"/></param>
        /// <param name="compiler"><inheritdoc cref="IKrCompiler" path="/summary"/></param>
        /// <param name="tessaCompilationRepository"><inheritdoc cref="ITessaCompilationRepository" path="/summary"/></param>
        /// <param name="typeProvider"><inheritdoc cref="ITypeProvider" path="/summary"/></param>
        /// <param name="typeIdentifierProvider"><inheritdoc cref="ITypeIdentifierProvider{T}" path="/summary"/></param>
        /// <param name="instanceCreationStrategy"><inheritdoc cref="IInstanceCreationStrategy" path="/summary"/></param>
        /// <param name="unityDisposableContainer"><inheritdoc cref="IUnityDisposableContainer" path="/summary"/></param>
        protected KrCompilationCacheBase(
            string categoryID,
            TessaCompilationObjectGlobalCache tessaCompilationObjectGlobalCache,
            IKrCompiler compiler,
            ITessaCompilationRepository tessaCompilationRepository,
            ITypeProvider typeProvider,
            ITypeIdentifierProvider<string> typeIdentifierProvider,
            IInstanceCreationStrategy instanceCreationStrategy,
            IUnityDisposableContainer? unityDisposableContainer = null)
            : base(
                  categoryID,
                  tessaCompilationObjectGlobalCache,
                  compiler,
                  tessaCompilationRepository,
                  unityDisposableContainer)
        {
            this.typeProvider = NotNullOrThrow(typeProvider);
            this.typeIdentifierProvider = NotNullOrThrow(typeIdentifierProvider);
            this.instanceCreationStrategy = NotNullOrThrow(instanceCreationStrategy);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override ValueTask<ITessaCompilationFactory<string, IKrScript>> CreateCompilationFactoryAsync(
            ITessaCompilationResult tessaCompilationResult,
            CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult<ITessaCompilationFactory<string, IKrScript>>(
                new TessaCompilationFactory<string, IKrScript>(
                    tessaCompilationResult.AssemblyBytes,
                    this.typeProvider,
                    this.typeIdentifierProvider,
                    this.instanceCreationStrategy));
        }

        #endregion
    }
}
