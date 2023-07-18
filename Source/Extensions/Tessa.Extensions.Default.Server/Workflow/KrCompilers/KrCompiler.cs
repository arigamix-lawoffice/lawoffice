#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Compilation;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders;
using Tessa.Platform.Collections;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <inheritdoc cref="IKrCompiler"/>
    public sealed class KrCompiler :
        TessaCompilerBase<IKrCompilationContext>,
        IKrCompiler
    {
        #region fields

        private readonly IKrSourceBuilderFactory builderFactory;

        private readonly ICompilationSourceProvider compilationSourceProvider;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="compiler"><inheritdoc cref="ICompiler" path="/summary"/></param>
        /// <param name="builderFactory"><inheritdoc cref="IKrSourceBuilderFactory" path="/summary"/></param>
        public KrCompiler(
            ICompiler compiler,
            IKrSourceBuilderFactory builderFactory,
            ICompilationSourceProvider compilationSourceProvider)
            : base(compiler)
        {
            this.builderFactory = NotNullOrThrow(builderFactory);
            this.compilationSourceProvider = NotNullOrThrow(compilationSourceProvider);

            this.DefaultStatics.AddRange(
                CompilationHelper.TessaDefaultStatics);

            this.DefaultReferences.AddRange(
                CompilationHelper.TessaExtensionsDefaultReferences);

            this.DefaultUsings.AddRange(
                CompilationHelper.TessaDefaultUsings);
            this.DefaultUsings.Add(
                "Tessa.Extensions.Default.Shared"); // DefaultCardTypes.SomeTypeID
            this.DefaultUsings.Add(
                "Tessa.Extensions.Default.Shared.Workflow.KrProcess");
            this.DefaultUsings.Add(
                "Tessa.Extensions.Default.Server.Workflow.KrObjectModel");
            this.DefaultUsings.Add(
                "Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI");
            this.DefaultUsings.Add(
                "Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow");
            this.DefaultUsings.Add(
                "Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers");
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override IKrCompilationContext CreateContext() =>
            new KrCompilationContext();

        /// <inheritdoc />
        protected override ValueTask PrepareCompilationContextAsync(
            IKrCompilationContext tessaCompilationContext,
            ICompilationContext compilationContext,
            CancellationToken cancellationToken = default)
        {
            this.SetSources(
                tessaCompilationContext,
                compilationContext);

            compilationContext.MetadataReferences.AddRange(tessaCompilationContext.MetadataReferences);
            compilationContext.SimpleAssemblyName = tessaCompilationContext.SimpleAssemblyName;

            return ValueTask.CompletedTask;
        }

        /// <inheritdoc />
        protected override async ValueTask<ValidationResult> PrepareValidationResultAsync(
            IKrCompilationContext tessaCompilerContext,
            ICompilationResult compilationResult,
            CancellationToken cancellationToken = default)
        {
            if (compilationResult.CompilerOutput.Count == 0)
            {
                return ValidationResult.Empty;
            }

            var validationResult = new ValidationResultBuilder(compilationResult.CompilerOutput.Count);
            foreach (var compilerOutputItem in compilationResult.CompilerOutput)
            {
                var source = compilerOutputItem.Source;
                var name = source?.Name ?? string.Empty;

                var sourceCode = source is not null
                    && tessaCompilerContext.AnchorsMap.TryGetValue(source.ID, out var anchorItem)
                    ? CompilationHelper.FormatErrorIntoMember(
                        source,
                        compilerOutputItem,
                        anchorItem.Name,
                        anchorItem.SyntaxKind)
                    : string.Empty;
                var errorText = string.IsNullOrWhiteSpace(name)
                    ? compilerOutputItem.ToString()!
                    : $"{name}:{Environment.NewLine}{compilerOutputItem}";

                var validator = ValidationSequence
                    .Begin(validationResult)
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

            return validationResult.Build();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Получение исходных кодов и подготовка их к компиляции.
        /// </summary>
        /// <param name="krContext">Контекст, передаваемый в компилятор IKrCompiler.</param>
        /// <param name="context">Контекст сеанса компиляции для компилятора.</param>
        /// <param name="anchorsMap">Возвращаемое значение. Коллекция ключ-значение, где ключ - идентификатор исходного кода; значение - объект идентифицирующий элемент компиляции.</param>
        private void SetSources(
            IKrCompilationContext krContext,
            ICompilationContext context)
        {
            var commonMethodBuilder = this.builderFactory
                .GetKrCommonMethodBuilder()
                .SetSources(krContext.CommonMethods)
                .FillAnchorsMap(krContext.AnchorsMap);
            context.Sources.AddRange(commonMethodBuilder.BuildSources());

            foreach (var stage in krContext.Stages)
            {
                string? stageTemplateName;
                string? stageGroupName;
                string? secondaryProcessName;

                if (krContext.SecondaryProcesses.TryFirst(i => i.ID == stage.GroupID, out var secondaryProcess))
                {
                    stageTemplateName = null;
                    stageGroupName = null;
                    secondaryProcessName = secondaryProcess.Name;
                }
                else
                {
                    stageTemplateName = stage.TemplateName;
                    stageGroupName = stage.GroupName;
                    secondaryProcessName = null;
                }

                var sources = this.builderFactory
                        .GetKrRuntimeScriptBuilder()
                        .SetClassID(stage.StageID)
                        .SetClassAlias(SourceIdentifiers.StageAlias)
                        .SetLocation(
                            stage.StageName,
                            stageTemplateName,
                            stageGroupName,
                            secondaryProcessName)
                        .SetSources(stage)
                        .SetExtraSources(stage)
                        .FillAnchorsMap(krContext.AnchorsMap)
                        .BuildSources()
                    ;
                context.Sources.AddRange(sources);
            }

            foreach (var template in krContext.StageTemplates)
            {
                var sources = this.builderFactory
                        .GetKrDesignScriptBuilder()
                        .SetClassID(template.ID)
                        .SetClassAlias(SourceIdentifiers.TemplateAlias)
                        .SetLocation(
                            stageTemplateName: template.Name,
                            stageGroupName: template.StageGroupName)
                        .SetSources(template)
                        .FillAnchorsMap(krContext.AnchorsMap)
                        .BuildSources()
                    ;
                context.Sources.AddRange(sources);
            }

            foreach (var stageGroup in krContext.StageGroups)
            {
                var sources = this.builderFactory
                        .GetKrDesignScriptBuilder()
                        .SetClassID(stageGroup.ID)
                        .SetClassAlias(SourceIdentifiers.GroupAlias)
                        .SetLocation(stageGroupName: stageGroup.Name)
                        .SetSources(stageGroup)
                        .FillAnchorsMap(krContext.AnchorsMap)
                        .BuildSources()
                    ;
                context.Sources.AddRange(sources);

                sources = this.builderFactory
                        .GetKrRuntimeScriptBuilder()
                        .SetClassID(stageGroup.ID)
                        .SetClassAlias(SourceIdentifiers.GroupAlias)
                        .SetLocation(stageGroupName: stageGroup.Name)
                        .SetSources(stageGroup)
                        .FillAnchorsMap(krContext.AnchorsMap)
                        .BuildSources()
                    ;
                context.Sources.AddRange(sources);
            }

            foreach (var secondaryProcess in krContext.SecondaryProcesses)
            {
                var sources = this.builderFactory
                        .GetKrExecutionScriptBuilder()
                        .SetClassID(secondaryProcess.ID)
                        .SetClassAlias(SourceIdentifiers.SecondaryProcessAlias)
                        .SetLocation(secondaryProcessName: secondaryProcess.Name)
                        .SetSources(secondaryProcess)
                        .FillAnchorsMap(krContext.AnchorsMap)
                        .BuildSources()
                    ;
                context.Sources.AddRange(sources);

                if (secondaryProcess is IKrProcessButton button)
                {
                    sources = this.builderFactory
                            .GetKrVisibilityScriptBuilder()
                            .SetClassID(button.ID)
                            .SetClassAlias(SourceIdentifiers.SecondaryProcessAlias)
                            .SetLocation(secondaryProcessName: button.Name)
                            .SetSources(button)
                            .FillAnchorsMap(krContext.AnchorsMap)
                            .BuildSources()
                        ;
                    context.Sources.AddRange(sources);
                }
            }
        }

        #endregion
    }
}
