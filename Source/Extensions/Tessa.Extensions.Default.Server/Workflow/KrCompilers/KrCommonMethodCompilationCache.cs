#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Compilation;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <inheritdoc cref="IKrCommonMethodCompilationCache"/>
    public sealed class KrCommonMethodCompilationCache :
        KrCompilationCacheBase,
        IKrCommonMethodCompilationCache
    {
        #region Fields

        private readonly IKrProcessCache krProcessCache;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="tessaCompilationObjectGlobalCache"><inheritdoc cref="TessaCompilationObjectGlobalCache" path="/summary"/></param>
        /// <param name="compiler"><inheritdoc cref="IKrCompiler" path="/summary"/></param>
        /// <param name="tessaCompilationRepository"><inheritdoc cref="ITessaCompilationRepository" path="/summary"/></param>
        /// <param name="typeProvider"><inheritdoc cref="ITypeProvider" path="/summary"/></param>
        /// <param name="typeIdentifierProvider"><inheritdoc cref="ITypeIdentifierProvider{T}" path="/summary"/></param>
        /// <param name="instanceCreationStrategy"><inheritdoc cref="IInstanceCreationStrategy" path="/summary"/></param>
        /// <param name="krProcessCache"><inheritdoc cref="IKrProcessCache" path="/summary"/></param>
        /// <param name="unityDisposableContainer"><inheritdoc cref="IUnityDisposableContainer" path="/summary"/></param>
        public KrCommonMethodCompilationCache(
            TessaCompilationObjectGlobalCache tessaCompilationObjectGlobalCache,
            IKrCompiler compiler,
            ITessaCompilationRepository tessaCompilationRepository,
            ITypeProvider typeProvider,
            ITypeIdentifierProvider<string> typeIdentifierProvider,
            IInstanceCreationStrategy instanceCreationStrategy,
            IKrProcessCache krProcessCache,
            [OptionalDependency] IUnityDisposableContainer? unityDisposableContainer = null)
            : base(
                  DefaultCompilationCacheNames.KrCommonMethod,
                  tessaCompilationObjectGlobalCache,
                  compiler,
                  tessaCompilationRepository,
                  typeProvider,
                  typeIdentifierProvider,
                  instanceCreationStrategy,
                  unityDisposableContainer) =>
            this.krProcessCache = NotNullOrThrow(krProcessCache);

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async Task<TessaCompilationContext<IKrCompilationContext>> GetCompilerContextAsync(
            Guid id,
            Func<IKrCompilationContext> getCompilerContextFunc,
            CancellationToken cancellationToken = default)
        {
            var commonMethods = await this.krProcessCache.GetAllCommonMethodsAsync(
                cancellationToken);

            var commonMethod = commonMethods.FirstOrDefault(i => i.ID == id);

            if (commonMethod is null)
            {
                return new TessaCompilationContext<IKrCompilationContext>(
                    id)
                {
                    ValidationResult = this.CreateSourceObjectNotFoundValidationResult(
                        id),
                };
            }

            // Всегда компилируем все объекты, т.к. они могут ссылаться друг на друга.

            var context = getCompilerContextFunc();
            context.SimpleAssemblyName = CompilationHelper.GetSimpleAssemblyName(
                this.CategoryID,
                id);

            context.CommonMethods.AddRange(commonMethods);

            return new TessaCompilationContext<IKrCompilationContext>(
                id)
            {
                CompilerContext = context,
            };
        }

        /// <inheritdoc/>
        protected override async Task<IList<TessaCompilationContext<IKrCompilationContext>>> GetCompilerContextAsync(
            Func<IKrCompilationContext> getCompilerContextFunc,
            CancellationToken cancellationToken = default)
        {
            var commonMethods = await this.krProcessCache.GetAllCommonMethodsAsync(
                cancellationToken);

            var contexts = new List<TessaCompilationContext<IKrCompilationContext>>(1);

            // Всегда компилируем все объекты, т.к. они могут ссылаться друг на друга.

            foreach (var commonMethod in commonMethods)
            {
                var context = getCompilerContextFunc();
                context.SimpleAssemblyName = CompilationHelper.GetSimpleAssemblyName(
                    this.CategoryID,
                    commonMethod.ID);

                context.CommonMethods.AddRange(commonMethods);

                contexts.Add(new TessaCompilationContext<IKrCompilationContext>(
                    commonMethod.ID)
                {
                    CompilerContext = context,
                });
            }

            return contexts;
        }

        #endregion
    }
}
