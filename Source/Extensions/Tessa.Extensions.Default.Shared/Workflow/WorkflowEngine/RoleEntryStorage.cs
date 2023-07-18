using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine
{
    /// <summary>
    /// Предоставляет информацию по роли.
    /// </summary>
    [StorageObjectGenerator(GenerateDefaultConstructor = false)]
    public sealed partial class RoleEntryStorage
        : StorageObject, INamedEntry
    {
        /// <summary>
        /// Возвращает или задаёт идентификатор роли.
        /// </summary>
        public Guid ID
        {
            get => this.TryGet<Guid>(nameof(this.ID));
            set => this.Set(nameof(this.ID), GuidBoxes.Box(value));
        }

        /// <summary>
        /// Возвращает или задаёт имя роли.
        /// </summary>
        public string Name
        { 
            get => this.TryGet<string>(nameof(this.Name));
            set => this.Set(nameof(this.Name), value);
        }

        /// <summary>
        /// Возвращает или задаёт идентификатор родительской роли.
        /// </summary>
        public Guid? ParentRoleID
        {
            get => this.Get<Guid?>(nameof(this.ParentRoleID));
            set => this.Set(nameof(this.ParentRoleID), GuidBoxes.Box(value));
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RoleEntryStorage"/>.
        /// </summary>
        /// <param name="id">Идентификатор роли.</param>
        /// <param name="name">Имя роли.</param>
        public RoleEntryStorage(Guid id, string name)
            : this(default, id, name)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RoleEntryStorage"/>.
        /// </summary>
        /// <param name="parentRoleID">Идентификатор родительской роли.</param>
        /// <param name="id">Идентификатор роли.</param>
        /// <param name="name">Имя роли.</param>
        public RoleEntryStorage(Guid? parentRoleID, Guid id, string name)
            : base(new Dictionary<string, object>(DefaultCapacity, StringComparer.Ordinal))
        {
            this.ID = id;
            this.ParentRoleID = parentRoleID;
            this.Name = name;
        }

        /// <inheritdoc />
        public RoleEntryStorage(
            Dictionary<string, object> storage)
            : base(storage)
        {
            this.Init(nameof(this.ID) , GuidBoxes.Empty);
            this.Init(nameof(this.ParentRoleID) , default);
            this.Init(nameof(this.Name) , default);
        }
    }
}
