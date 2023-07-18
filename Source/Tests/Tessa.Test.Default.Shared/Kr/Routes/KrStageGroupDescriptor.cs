using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Test.Default.Shared;

namespace Tessa.Test.Default.Shared.Kr.Routes
{
    /// <summary>
    /// Предоставляет информацию о группе этапов используемую в тестах.
    /// </summary>
    public sealed class KrStageGroupDescriptor
    {
        #region Properties

        /// <summary>
        /// Возвращает идентификатор группы этапов.
        /// </summary>
        public Guid ID { get; }

        /// <summary>
        /// Возвращает имя группы этапов.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Возвращает порядковый номер группы этапов.
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Возвращает дескриптор группы этапов по умолчанию ("Согласование").
        /// </summary>
        /// <remarks>
        /// Поле содержит значение, соответствующее параметрам по умолчанию.<para/>
        /// Для получения актуального значения из базы данных используйте метод <see cref="GetDefaultStageGroupAsync(IDbScope, CancellationToken)"/>.
        /// </remarks>
        public static KrStageGroupDescriptor DefaultStageGroup { get; } = new KrStageGroupDescriptor(
            KrConstants.DefaultApprovalStageGroup,
            KrConstants.DefaultApprovalStageGroupName,
            KrConstants.DefaultApprovalStageGroupOrder);

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrStageGroupDescriptor"/>.
        /// </summary>
        /// <param name="id">Идентификатор группы этапов.</param>
        /// <param name="name">Имя группы этапов.</param>
        /// <param name="order">Порядковый номер группы этапов.</param>
        public KrStageGroupDescriptor(
            Guid id,
            string name,
            int order)
        {
            this.ID = id;
            this.Name = name;
            this.Order = order;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Создаёт новый экземпляр класса <see cref="KrStageGroupDescriptor"/> и инициализирует его информацией содержащейся в карточке из указанного объекта.
        /// </summary>
        /// <param name="clc">Объект, содержащий карточку, данными которой буден инициализирован новый экземпляр класса <see cref="KrStageGroupDescriptor"/>. Карточка должна содержать секцию <see cref="KrConstants.KrStageGroups.Name"/>.</param>
        /// <returns>Объект типа <see cref="KrStageGroupDescriptor"/> инициализированный карточкой содержащейся в <paramref name="clc"/>.</returns>
        public static KrStageGroupDescriptor FromStageGroupLifecycleCompanion(
            ICardLifecycleCompanion clc)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            var card = clc.GetAndCheckCardTypeThrow(DefaultCardTypes.KrStageGroupTypeID, DefaultCardTypes.KrStageGroupTypeName);
            var groupSection = card.Sections[KrConstants.KrStageGroups.Name];

            return new KrStageGroupDescriptor(
                clc.CardID,
                groupSection.RawFields.Get<string>(KrConstants.KrStageGroups.NameField),
                groupSection.RawFields.Get<int>(KrConstants.KrStageGroups.Order));
        }

        /// <summary>
        /// Возвращает группу этапов по умолчанию. Поиск информации о группе этапов выполняется в базе данных.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Информация о группе этапов по умолчанию.</returns>
        /// <exception cref="InvalidOperationException">Default stage group (Approval, Card ID = "&lt;StageGroupID&gt;") does not exist in database.</exception>
        /// <remarks>
        /// Дескриптор группы этапов по умолчанию: "Согласование". Идентификатор карточки: <see cref="KrConstants.DefaultApprovalStageGroup"/>.<para/>
        /// Если используется не изменённая группа этапов по умолчанию, то дескриптор рекомендуется получать без обращения к базе данных, из свойства <see cref="DefaultStageGroup"/>.
        /// </remarks>
        public static async Task<KrStageGroupDescriptor> GetDefaultStageGroupAsync(
            IDbScope dbScope,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            await using (dbScope.Create())
            {
                var stageGroupID = KrConstants.DefaultApprovalStageGroup;
                var db = dbScope.Db;

                db.SetCommand(
                    dbScope.BuilderFactory
                        .Select()
                            .C("t", KrConstants.KrStageGroups.NameField)
                            .C("t", KrConstants.KrStageGroups.Order)
                        .From(KrConstants.KrStageGroups.Name, "t").NoLock()
                        .Where().C("t", KrConstants.KrStageGroups.ID).Equals().P("ID")
                        .Build(),
                    db.Parameter("ID", stageGroupID));

                await using var reader = await db.ExecuteReaderAsync(cancellationToken);

                if (!await reader.ReadAsync(cancellationToken))
                {
                    throw new InvalidOperationException($"Default stage group (Approval, Card ID = \"{stageGroupID}\") does not exist in database.");
                }

                return new KrStageGroupDescriptor(stageGroupID, reader.GetString(0), reader.GetInt32(1));
            }
        }

        #endregion

    }
}
