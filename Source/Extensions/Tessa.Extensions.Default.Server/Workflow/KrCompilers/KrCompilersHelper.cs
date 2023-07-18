#nullable enable

using System;
using System.Linq;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    public static class KrCompilersHelper
    {
        #region Public Methods

        /// <summary>
        /// Возвращает имя автоматически сгенерированного класса.
        /// </summary>
        /// <param name="prefix">Префикс.</param>
        /// <param name="alias">Алиас.</param>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Имя автоматически сгенерированного класса: <paramref name="prefix"/>_<paramref name="alias"/>_<paramref name="id"/>.</returns>
        public static string FormatClassName(
            string prefix,
            string alias,
            Guid id) => $"{prefix}_{alias}_{id:N}";

        /// <summary>
        /// Возвращает значение, показывающее, что указанный этап относится к группе с заданным идентификатором.
        /// </summary>
        /// <param name="stageGroupID">Идентификатор группы этапов.</param>
        /// <param name="st">Проверяемый этап.</param>
        /// <returns>Значение <see langword="true"/>, если <see cref="Stage.StageGroupID"/> равно <paramref name="stageGroupID"/> или <paramref name="stageGroupID"/> равно <see cref="Guid.Empty"/>, иначе - <see langword="false"/>.</returns>
        public static bool ReferToGroup(
            Guid stageGroupID,
            Stage st)
        {
            ThrowIfNull(st);

            return st.StageGroupID == stageGroupID || stageGroupID == Guid.Empty;
        }

        /// <summary>
        /// Очищает коллекцию строк физической секции <see cref="KrConstants.KrStages.Name"/>.
        /// </summary>
        /// <param name="card">Карточка из которой удаляется информация.</param>
        public static void ClearPhysicalSections(Card card)
        {
            ThrowIfNull(card);

            if (card.TryGetSections()?.TryGetValue(KrConstants.KrStages.Name, out var sec) == true)
            {
                sec.TryGetRows()?.Clear();
            }
        }

        /// <summary>
        /// Проверяет, были ли изменены сценарии этапов.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <returns>Значение <see langword="true"/>, если карточка содержит этапы с изменёнными сценариями, иначе - <see langword="false"/>.</returns>
        public static bool HasStagesScriptsChanged(Card card)
        {
            ThrowIfNull(card);

            // Данные содержатся в физической секции при импорте.
            // При обычном сохранении с клиента данные содержатся в виртуальной секции.
            return (card.Sections.TryGetValue(KrConstants.KrStages.Virtual, out var sec)
                || card.Sections.TryGetValue(KrConstants.KrStages.Name, out sec))
                && sec.TryGetRows()?.Any(p =>
                    p.State == CardRowState.Deleted
                    || p.State == CardRowState.Inserted
                    || p.ContainsKey(KrConstants.KrStages.RuntimeSourceAfter)
                    || p.ContainsKey(KrConstants.KrStages.RuntimeSourceBefore)
                    || p.ContainsKey(KrConstants.KrStages.RuntimeSourceCondition)) == true;
        }

        /// <summary>
        /// Проверяет, были ли изменены сценарии шаблона этапов.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <returns>Значение <see langword="true"/>, если карточка содержит изменённые сценарии шаблона этапов, иначе - <see langword="false"/>.</returns>
        public static bool HasStageTemplateScriptsChanged(Card card)
        {
            ThrowIfNull(card);

            return card.TryGetSections() is { } sections
                && sections.TryGetValue(KrConstants.KrStageTemplates.Name, out var krStageTemplateSec)
                && krStageTemplateSec.TryGetRawFields() is { } krStageTemplateSecFields
                && (krStageTemplateSecFields.ContainsKey(KrConstants.KrStageTemplates.SourceCondition)
                    || krStageTemplateSecFields.ContainsKey(KrConstants.KrStageTemplates.SourceBefore)
                    || krStageTemplateSecFields.ContainsKey(KrConstants.KrStageTemplates.SourceAfter));
        }

        #endregion
    }
}
