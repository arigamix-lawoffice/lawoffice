﻿#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Tessa.Compilation;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <inheritdoc cref="IKrSecondaryProcessCompilationCache"/>
    public sealed class KrSecondaryProcessCompilationCache :
        KrCompilationCacheWithLoadDependenciesBase,
        IKrSecondaryProcessCompilationCache
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
        /// <param name="commonMethodCompilationCache"><inheritdoc cref="IKrCommonMethodCompilationCache" path="/summary"/></param>
        /// <param name="unityDisposableContainer"><inheritdoc cref="IUnityDisposableContainer" path="/summary"/></param>
        public KrSecondaryProcessCompilationCache(
            TessaCompilationObjectGlobalCache tessaCompilationObjectGlobalCache,
            IKrCompiler compiler,
            ITessaCompilationRepository tessaCompilationRepository,
            ITypeProvider typeProvider,
            ITypeIdentifierProvider<string> typeIdentifierProvider,
            IInstanceCreationStrategy instanceCreationStrategy,
            IKrProcessCache krProcessCache,
            IKrCommonMethodCompilationCache commonMethodCompilationCache,
            [OptionalDependency] IUnityDisposableContainer? unityDisposableContainer = null)
            : base(
                  DefaultCompilationCacheNames.KrSecondaryProcess,
                  tessaCompilationObjectGlobalCache,
                  compiler,
                  tessaCompilationRepository,
                  typeProvider,
                  typeIdentifierProvider,
                  instanceCreationStrategy,
                  commonMethodCompilationCache,
                  unityDisposableContainer)
        {
            this.krProcessCache = NotNullOrThrow(krProcessCache);

            this.GetDependentInvalidationActions(DefaultCompilationCacheNames.KrCommonMethod)
                .Add(this.KrCommonMethodInvalidationActionAsync);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async Task<TessaCompilationContext<IKrCompilationContext>> GetCompilerContextAsync(
            Guid id,
            Func<IKrCompilationContext> getCompilerContextFunc,
            CancellationToken cancellationToken = default)
        {
            var secondaryProcess = await this.krProcessCache.TryGetSecondaryProcessAsync(
                id,
                cancellationToken);

            if (secondaryProcess is null)
            {
                return new TessaCompilationContext<IKrCompilationContext>(
                    id)
                {
                    ValidationResult = this.CreateSourceObjectNotFoundValidationResult(
                        id),
                };
            }

            var commonMethods = await this.krProcessCache.GetAllCommonMethodsAsync(
                cancellationToken);

            var context = getCompilerContextFunc();
            context.SimpleAssemblyName = CompilationHelper.GetSimpleAssemblyName(
                this.CategoryID,
                id);

            var commonMethod = commonMethods
                .OrderBy(static i => i.ID)
                .FirstOrDefault();

            if (commonMethod is null)
            {
                context.CommonMethods.Add(KrCommonMethod.FakeObject);
            }
            else
            {
                (var result, var reference) = await CompilationHelper.CreateReferenceToCompilationObjectAsync(
                    this.CommonMethodCompilationCache,
                    commonMethod.ID,
                    cancellationToken);

                if (reference is null)
                {
                    return new TessaCompilationContext<IKrCompilationContext>(
                        id)
                    {
                        ValidationResult = result,
                    };
                }

                context.MetadataReferences.Add(reference);
            }

            var stages = await this.krProcessCache.GetRuntimeStagesForTemplateAsync(
                id,
                cancellationToken);

            context.SecondaryProcesses.Add(secondaryProcess);
            context.Stages.AddRange(stages);

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
            var buttons = await this.krProcessCache.GetAllButtonsAsync(
                cancellationToken);
            var actions = await this.krProcessCache.GetAllActionsAsync(
                cancellationToken);
            var pureProcesses = await this.krProcessCache.GetAllPureProcessesAsync(
                cancellationToken);
            var contexts = new List<TessaCompilationContext<IKrCompilationContext>>(
                buttons.Count
                + actions.Count
                + pureProcesses.Count);

            var commonMethod = commonMethods
                .OrderBy(static i => i.ID)
                .FirstOrDefault();

            await this.AppendSourcesAsync(
                buttons.Values,
                commonMethod,
                getCompilerContextFunc,
                contexts,
                cancellationToken);

            await this.AppendSourcesAsync(
                actions.Values,
                commonMethod,
                getCompilerContextFunc,
                contexts,
                cancellationToken);

            await this.AppendSourcesAsync(
                pureProcesses.Values,
                commonMethod,
                getCompilerContextFunc,
                contexts,
                cancellationToken);

            return contexts;
        }

        #endregion

        #region Private Methods

        private async ValueTask AppendSourcesAsync(
            IEnumerable<IKrSecondaryProcess> secondaryProcesses,
            IKrCommonMethod? commonMethod,
            Func<IKrCompilationContext> getCompilerContextFunc,
            IList<TessaCompilationContext<IKrCompilationContext>> contexts,
            CancellationToken cancellationToken = default)
        {
            var result = ValidationResult.Empty;
            PortableExecutableReference? reference = null;

            if (commonMethod is not null)
            {
                (result, reference) = await CompilationHelper.CreateReferenceToCompilationObjectAsync(
                    this.CommonMethodCompilationCache,
                    commonMethod.ID,
                    cancellationToken);
            }

            foreach (var secondaryProcess in secondaryProcesses)
            {
                if (!result.IsSuccessful)
                {
                    contexts.Add(new TessaCompilationContext<IKrCompilationContext>(
                        secondaryProcess.ID)
                    {
                        ValidationResult = result,
                    });

                    continue;
                }

                var context = getCompilerContextFunc();
                context.SimpleAssemblyName = CompilationHelper.GetSimpleAssemblyName(
                    this.CategoryID,
                    secondaryProcess.ID);

                if (reference is null)
                {
                    context.CommonMethods.Add(KrCommonMethod.FakeObject);
                }
                else
                {
                    context.MetadataReferences.Add(reference);
                }

                var stages = await this.krProcessCache.GetRuntimeStagesForTemplateAsync(
                    secondaryProcess.ID,
                    cancellationToken);

                context.SecondaryProcesses.Add(secondaryProcess);
                context.Stages.AddRange(stages);

                contexts.Add(new TessaCompilationContext<IKrCompilationContext>(
                    secondaryProcess.ID)
                {
                    CompilerContext = context,
                });
            }
        }

        #endregion

        #region Private Methods

        private async Task KrCommonMethodInvalidationActionAsync(
            TessaCompilationObjectCacheEventArgs args,
            CancellationToken cancellationToken = default)
        {
            await this.InvalidateLocalAsync(
                this.CreateInvalidationArgsForRelatedCache(
                    args,
                    null),
                cancellationToken);
        }

        #endregion
    }
}
