using System;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Platform;

namespace Tessa.Test.Default.Shared.Kr.Routes
{
    /// <summary>
    /// Предоставляет методы для создания и инициализации объектов типа <see cref="KrStageTemplateBuilder"/>.
    /// </summary>
    public sealed class KrStageTemplateFactory
    {
        #region Fields

        private readonly ICardLifecycleCompanionDependencies deps;

        private readonly KrStageGroupDescriptor defaultGroup;

        private readonly Guid defaultDocTypeID;

        private readonly string defaultDocTypeName;

        private readonly ITestCardManager testCardManager;

        #endregion

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrStageTemplateFactory"/>.
        /// </summary>
        /// <param name="defaultGroup">Группа этапов по умолчанию.</param>
        /// <param name="defaultDocTypeID">Идентификатор типа документа используемый по умолчанию в создаваемых шаблонах этапов для ограничения при пересчёте.</param>
        /// <param name="defaultDocTypeName">Имя типа документа используемого по умолчанию в создаваемых шаблонах этапов для ограничения при пересчёте.</param>
        /// <param name="deps">Зависимости, используемые объектами, управляющими жизненным циклом карточек.</param>
        /// <param name="testCardManager">Объект, выполняющий удаление созданных объектов.</param>
        public KrStageTemplateFactory(
            KrStageGroupDescriptor defaultGroup,
            Guid defaultDocTypeID,
            string defaultDocTypeName,
            ICardLifecycleCompanionDependencies deps,
            ITestCardManager testCardManager = null)
        {
            Check.ArgumentNotNull(deps, nameof(deps));

            this.deps = deps;
            this.defaultGroup = defaultGroup;
            this.defaultDocTypeID = defaultDocTypeID;
            this.defaultDocTypeName = defaultDocTypeName;
            this.testCardManager = testCardManager;
        }

        #region Public methods

        /// <summary>
        /// Создаёт и инициализирует объект управляющий построением карточки шаблонов этапов.
        /// </summary>
        /// <param name="name">Имя шаблона этапов.</param>
        /// <param name="order">Порядковый номер шаблона этапа.</param>
        /// <param name="groupPosition">Позиция шаблона этапов относительно этапов, добавленных вручную. Если значение не задано, то оно задаётся равным <see cref="GroupPosition.AtFirst"/>.</param>
        /// <param name="overrideGroup">Группа этапов используемая вместо группы по умолчанию.</param>
        /// <param name="addDefaultDocType">Значение <see langword="true"/>, если необходимо задать идентификатор типа документа по умолчанию, заданный в конструкторе <see cref="KrStageTemplateFactory(KrStageGroupDescriptor, Guid, string, ICardLifecycleCompanionDependencies, ITestCardManager)"/>, определяющий ограничения при пересчёте, иначе - <see langword="false"/>.</param>
        /// <returns>Объект типа <see cref="KrStageTemplateBuilder"/> выполняющий построение шаблона этапов.</returns>
        public KrStageTemplateBuilder Create(
            string name,
            int order = default,
            GroupPosition groupPosition = default,
            KrStageGroupDescriptor overrideGroup = default,
            bool addDefaultDocType = true)
        {
            var group = overrideGroup ?? this.defaultGroup ?? throw new InvalidOperationException($"No \"{nameof(overrideGroup)}\" or \"{nameof(this.defaultGroup)}\" stage group set.");

            var b = new KrStageTemplateBuilder(this.deps)
                .Create()
                .SetName(name)
                .SetOrder(order)
                .SetGroupPosition(groupPosition ?? GroupPosition.AtFirst)
                .SetStageGroup(group);

            if (addDefaultDocType)
            {
                b.ForDocType(this.defaultDocTypeID, this.defaultDocTypeName);
            }

            this.testCardManager?.DeleteCardAfterTest(b);

            return b;
        }

        #endregion
    }
}
