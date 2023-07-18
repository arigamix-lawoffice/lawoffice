using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Контейнер, предоставляющий информацию об областях выполнения.
    /// </summary>
    /// <remarks>Данный тип потокобезопасен.</remarks>
    /// <seealso cref="TestScopeAttribute"/>
    public sealed class TestScopeContainer :
        IAsyncDisposable
    {
        #region Fields

        private readonly object syncObject = new();

        private bool isDisposed;

        /// <summary>
        /// Доступная только для чтения коллекция содержащая информацию о областях выполнения.
        /// </summary>
        /// <remarks>Для инициализации используйте метод <see cref="Initialize(Assembly)"/>.</remarks>
        private HashSet<string, ScopeContext> scopeContexts;

        #endregion

        #region Constants And Static Fields

        /// <summary>
        /// Возвращает экземпляр объекта, который может использоваться из разных потоков одновременно.
        /// </summary>
        public static TestScopeContainer Instance { get; } = new TestScopeContainer();

        #endregion

        #region Public Methods

        /// <summary>
        /// Инициализирует контейнер.
        /// </summary>
        /// <param name="assembly">Сборка в которой выполняется поиск областей выполнения.</param>
        /// <exception cref="InvalidOperationException">Запрещено указание нескольких областей выполнения для одного класса.</exception>
        public void Initialize(Assembly assembly)
        {
            Check.ArgumentNotNull(assembly, nameof(assembly));

            if (this.scopeContexts is not null)
            {
                return;
            }

            lock (this.syncObject)
            {
                if (this.scopeContexts is null)
                {
                    this.scopeContexts = new HashSet<string, ScopeContext>(
                        static i => i.Name,
                        StringComparer.Ordinal,
                        GetTestScopeContexts(assembly));
                }
            }
        }

        /// <summary>
        /// Возвращает информацию по области выполнения с указанным названием.
        /// </summary>
        /// <param name="scopeName">Название области выполнения.</param>
        /// <param name="scopeContext">Объект содержащий информацию по области выполнения или значение по умолчанию для типа, если область выполнения с таким именем не найдена.</param>
        /// <returns>Значение <see langword="true"/>, если информация по области выполнения с указанным именем найдена, иначе - <see langword="false"/>.</returns>
        public bool TryGetScopeContext(
            string scopeName,
            out ScopeContext scopeContext)
        {
            if (this.scopeContexts is not null
                && TestScopeHelper.CheckScopeName(scopeName)
                && this.scopeContexts.TryGetItem(scopeName, out scopeContext))
            {
                return true;
            }

            scopeContext = null;
            return false;
        }

        #endregion

        #region IAsyncDisposable Members

        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            if (this.isDisposed
                || this.scopeContexts is null)
            {
                return;
            }

            foreach (var scopeContext in this.scopeContexts)
            {
                await scopeContext.DisposeAsync();
            }

            this.isDisposed = true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Возвращает коллекцию, содержащую информацию об областях выполнения определённых в указанной сборке.
        /// </summary>
        /// <param name="assembly">Сборка в которой выполняется поиск областей выполнения.</param>
        /// <returns>Коллекция, содержащая информацию об областях выполнения определённых в указанной сборке. Ключом в коллекции является название области выполнения.</returns>
        private static IEnumerable<ScopeContext> GetTestScopeContexts(
            Assembly assembly)
        {
            Check.ArgumentNotNull(assembly, nameof(assembly));

            var scopeContexts = new Dictionary<string, int>(StringComparer.Ordinal);
            var types = new List<TypeInfo>();

            // Не выполняется фильтрация классов, т.к. нет ограничений на применение атрибута.

            foreach (var definedType in assembly.DefinedTypes)
            {
                var attribute = definedType.GetCustomAttribute<TestScopeAttribute>(false);

                if (attribute is null)
                {
                    continue;
                }

                var scopeName = attribute.Name;

                foreach (var type in types)
                {
                    // Запрещено множественное указание областей выполнения.

                    if (type.IsAssignableFrom(definedType)
                        || definedType.IsAssignableFrom(type))
                    {
                        throw new InvalidOperationException(
                            $"Multiple execution scopes are not allowed. Scope name: \"{scopeName}\"." +
                            $" Types containing execution scopes (non-exhaustive list): {type}, {definedType}.");
                    }
                }

                if (!scopeContexts.TryAdd(scopeName, 1))
                {
                    scopeContexts[scopeName]++;
                }

                types.Add(definedType);
            }

            return scopeContexts.Select(static i => new ScopeContext(i.Key, i.Value));
        }

        #endregion
    }
}
