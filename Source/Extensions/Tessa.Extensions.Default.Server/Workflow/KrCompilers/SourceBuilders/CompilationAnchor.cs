using Microsoft.CodeAnalysis.CSharp;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    /// <summary>
    /// Предоставляет информацию об элементе компиляции.
    /// </summary>
    public readonly struct CompilationAnchor
    {
        #region Properties

        /// <summary>
        /// Возвращает имя элемента.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Возвращает тип элемента.
        /// </summary>
        public SyntaxKind SyntaxKind { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CompilationAnchor"/>.
        /// </summary>
        /// <param name="name">Имя элемента.</param>
        /// <param name="syntaxKind">Тип элемента.</param>
        public CompilationAnchor(
            string name,
            SyntaxKind syntaxKind)
        {
            this.Name = name;
            this.SyntaxKind = syntaxKind;
        }

        #endregion
    }
}
