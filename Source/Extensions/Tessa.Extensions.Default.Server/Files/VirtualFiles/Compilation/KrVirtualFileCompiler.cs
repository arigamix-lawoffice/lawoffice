#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Compilation;
using Tessa.Localization;
using Tessa.Platform.Collections;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles.Compilation
{
    /// <inheritdoc cref="IKrVirtualFileCompiler"/>
    public sealed class KrVirtualFileCompiler :
        TessaCompilerBase<IKrVirtualFileCompilationContext>,
        IKrVirtualFileCompiler
    {
        #region Fields

        private readonly ICompilationSourceProvider compilationSourceProvider;

        /// <summary>
        /// Параметры скрипта.
        /// </summary>
        private static readonly Tuple<string, string>[] parameters =
        {
            new Tuple<string, string>(nameof(IKrVirtualFileScriptContext), "context")
        };

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="compiler"><inheritdoc cref="ICompiler" path="/summary"/></param>
        /// <param name="compilationSourceProvider"><inheritdoc cref="ICompilationSourceProvider" path="/summary"/></param>
        public KrVirtualFileCompiler(
            ICompiler compiler,
            ICompilationSourceProvider compilationSourceProvider)
            : base(compiler)
        {
            this.compilationSourceProvider = NotNullOrThrow(compilationSourceProvider);

            this.DefaultStatics.AddRange(
                CompilationHelper.TessaDefaultStatics);

            this.DefaultReferences.AddRange(
                CompilationHelper.TessaExtensionsDefaultReferences);

            this.DefaultUsings.AddRange(
                CompilationHelper.TessaDefaultUsings);
            this.DefaultUsings.Add(
                "Tessa.Extensions.Default.Server.Files.VirtualFiles");
            this.DefaultUsings.Add(
                "Tessa.Extensions.Default.Server.Files.VirtualFiles.Compilation");
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override IKrVirtualFileCompilationContext CreateContext() =>
            new KrVirtualFileCompilationContext();

        /// <inheritdoc />
        protected override ValueTask PrepareCompilationContextAsync(
            IKrVirtualFileCompilationContext tessaCompilationContext,
            ICompilationContext compilationContext,
            CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace(tessaCompilationContext.InitializationScenario))
            {
                compilationContext.Sources.Add(this.PrepareSource(
                    tessaCompilationContext.ID,
                    tessaCompilationContext.Name,
                    tessaCompilationContext.InitializationScenario));
            }

            return ValueTask.CompletedTask;
        }

        /// <inheritdoc />
        protected override async ValueTask<ValidationResult> PrepareValidationResultAsync(
            IKrVirtualFileCompilationContext tessaCompilerContext,
            ICompilationResult compilationResult,
            CancellationToken cancellationToken = default)
        {
            if (compilationResult.ValidationResult.IsSuccessful)
            {
                return await base.PrepareValidationResultAsync(
                    tessaCompilerContext,
                    compilationResult,
                    cancellationToken);
            }

            var result = new ValidationResultBuilder(
                compilationResult.CompilerOutput.Count);

            foreach (var compilerOutputItem in compilationResult.CompilerOutput)
            {
                var source = NotNullOrThrow(compilerOutputItem.Source);

                var errorText = await LocalizationManager.FormatAsync(
                    "$KrVirtualFiles_CompilationErrorTemplate",
                    await LocalizationManager.LocalizeAsync(
                        source.Name,
                        cancellationToken),
                    compilerOutputItem.ToString(),
                    cancellationToken);

                var sourceCode = CompilationHelper.FormatErrorIntoMember(
                    source,
                    compilerOutputItem,
                    CompilationHelper.GetMemberName(
                        source,
                        compilerOutputItem));

                var validator = ValidationSequence
                    .Begin(result)
                    .SetObjectName(this);

                if (compilerOutputItem.IsWarning)
                {
                    validator.WarningDetails(errorText, sourceCode);
                }
                else
                {
                    validator.ErrorDetails(errorText, sourceCode);
                }

                validator.End();
            }

            return result.Build();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Создаёт элемент компиляции.
        /// </summary>
        /// <param name="id">Идентификатор элемента компиляции.</param>
        /// <param name="name">Имя элемента компиляции.</param>
        /// <param name="source">C# сценарий.</param>
        /// <returns><inheritdoc cref="ICompilationSource" path="/summary"/></returns>
        private ICompilationSource PrepareSource(
            Guid id,
            string name,
            string source)
        {
            var syntaxTreeBuilder = this.compilationSourceProvider
                .AcquireSyntaxTree();

            syntaxTreeBuilder
                .SetID(id)
                .SetName(name)
                .Namespace("Tessa.Extensions.Default.Servier.Files.VirtualFiles.Generated")
                .Class(
                    $"VirtualFile_{id:N}",
                    AccessModifier.Public,
                    new[] { nameof(IKrVirtualFileScript) },
                    guid: id)
                .AddMethod(
                    nameof(Task),
                    nameof(IKrVirtualFileScript.InitializationScenarioAsync),
                    parameters,
                    source,
                    isAsync: true);

            return syntaxTreeBuilder.Build();
        }

        #endregion
    }
}
