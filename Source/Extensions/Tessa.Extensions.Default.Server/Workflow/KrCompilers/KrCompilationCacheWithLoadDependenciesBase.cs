#nullable enable

using System.Threading;
using System.Threading.Tasks;
using Tessa.Compilation;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Реализация <see cref="IKrCompilationCacheBase"/> с поддержкой загрузки зависимостей.
    /// </summary>
    public abstract class KrCompilationCacheWithLoadDependenciesBase :
        KrCompilationCacheBase
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="categoryID"><inheritdoc cref="TessaCompilationObjectCacheCoreBase{TCompilerContext,TKey,TInstance}.CategoryID" path="/summary"/></param>
        /// <param name="tessaCompilationObjectGlobalCache"><inheritdoc cref="TessaCompilationObjectGlobalCache" path="/summary"/></param>
        /// <param name="compiler"><inheritdoc cref="IKrCompiler" path="/summary"/></param>
        /// <param name="tessaCompilationRepository"><inheritdoc cref="ITessaCompilationRepository" path="/summary"/></param>
        /// <param name="typeProvider"><inheritdoc cref="ITypeProvider" path="/summary"/></param>
        /// <param name="typeIdentifierProvider"><inheritdoc cref="ITypeIdentifierProvider{T}" path="/summary"/></param>
        /// <param name="instanceCreationStrategy"><inheritdoc cref="IInstanceCreationStrategy" path="/summary"/></param>
        /// <param name="commonMethodCompilationCache"><inheritdoc cref="IKrCommonMethodCompilationCache" path="/summary"/></param>
        /// <param name="unityDisposableContainer"><inheritdoc cref="IUnityDisposableContainer" path="/summary"/></param>
        protected KrCompilationCacheWithLoadDependenciesBase(
            string categoryID,
            TessaCompilationObjectGlobalCache tessaCompilationObjectGlobalCache,
            IKrCompiler compiler,
            ITessaCompilationRepository tessaCompilationRepository,
            ITypeProvider typeProvider,
            ITypeIdentifierProvider<string> typeIdentifierProvider,
            IInstanceCreationStrategy instanceCreationStrategy,
            IKrCommonMethodCompilationCache commonMethodCompilationCache,
            IUnityDisposableContainer? unityDisposableContainer = null)
            : base(
                  categoryID,
                  tessaCompilationObjectGlobalCache,
                  compiler,
                  tessaCompilationRepository,
                  typeProvider,
                  typeIdentifierProvider,
                  instanceCreationStrategy,
                  unityDisposableContainer) =>
            this.CommonMethodCompilationCache = NotNullOrThrow(commonMethodCompilationCache);

        #endregion

        #region Protected Declarations

        protected IKrCommonMethodCompilationCache CommonMethodCompilationCache { get; }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async ValueTask<ITessaCompilationFactory<string, IKrScript>> CreateCompilationFactoryAsync(
            ITessaCompilationResult tessaCompilationResult,
            CancellationToken cancellationToken = default)
        {
            var factory = await base.CreateCompilationFactoryAsync(
                tessaCompilationResult,
                cancellationToken);

            if (!tessaCompilationResult.ValidationResult.IsSuccessful)
            {
                return factory;
            }

            var assembly = factory.GetAssembly();

            if (assembly is not null)
            {
                var references = assembly.GetReferencedAssemblies();

                foreach (var reference in references)
                {
                    var simpleAssemblyName = reference.Name;

                    if (string.IsNullOrEmpty(simpleAssemblyName))
                    {
                        continue;
                    }

                    var res = CompilationHelper.GetSimpleAssemblyNameInfo(simpleAssemblyName);

                    if (!res.HasValue
                        || res.Value.CategoryID != DefaultCompilationCacheNames.KrCommonMethod)
                    {
                        continue;
                    }

                    var commonMethodCompilationResult = await this.CommonMethodCompilationCache.GetAsync(
                        res.Value.ObjectID,
                        cancellationToken: cancellationToken);

                    if (!commonMethodCompilationResult.Result.ValidationResult.IsSuccessful
                        || commonMethodCompilationResult.Result.AssemblyBytes is null)
                    {
                        throw new ValidationException(commonMethodCompilationResult.Result.ValidationResult);
                    }

                    factory.AssemblyLoadContext.LoadFromImage(
                        commonMethodCompilationResult.Result.AssemblyBytes);
                }
            }

            return factory;
        }

        #endregion
    }
}
