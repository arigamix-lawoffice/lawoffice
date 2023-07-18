#nullable enable

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <inheritdoc cref="IKrCompilationContext" />
    public sealed class KrCompilationContext :
        IKrCompilationContext
    {
        #region Fields

        private string simpleAssemblyName;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public KrCompilationContext() =>
            this.simpleAssemblyName = Guid.NewGuid().ToString("N");

        #endregion

        #region IKrCompilationContext Members

        /// <inheritdoc />
        public string SimpleAssemblyName
        {
            get => this.simpleAssemblyName;
            set => this.simpleAssemblyName = NotWhiteSpaceOrThrow(value);
        }

        /// <inheritdoc />
        public IList<IKrCommonMethod> CommonMethods { get; } = new List<IKrCommonMethod>();

        /// <inheritdoc />
        public IList<IKrRuntimeStage> Stages { get; } = new List<IKrRuntimeStage>();

        /// <inheritdoc />
        public IList<IKrStageTemplate> StageTemplates { get; } = new List<IKrStageTemplate>();

        /// <inheritdoc />
        public IList<IKrStageGroup> StageGroups { get; } = new List<IKrStageGroup>();

        /// <inheritdoc />
        public IList<IKrSecondaryProcess> SecondaryProcesses { get; } = new List<IKrSecondaryProcess>();

        /// <inheritdoc />
        public Dictionary<Guid, CompilationAnchor> AnchorsMap { get; } = new Dictionary<Guid, CompilationAnchor>();

        /// <inheritdoc/>
        public IList<MetadataReference> MetadataReferences { get; } = new List<MetadataReference>();

        #endregion
    }
}
