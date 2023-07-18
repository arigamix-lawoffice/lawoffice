using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Platform;
using Tessa.Platform.Initialization;
using Tessa.Platform.Storage;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Методы-расширения для пространства имён <c>Tessa.Extensions.Default.Shared.Workflow.KrProcess</c>.
    /// </summary>
    public static class KrProcessSharedExtensions
    {
        #region Flags Extensions

        /// <doc path='info[@type="flags" and @item="Has"]'/>
        public static bool Has(this KrComponents flags, KrComponents flag)
        {
            return (flags & flag) == flag;
        }

        /// <doc path='info[@type="flags" and @item="HasAny"]'/>
        public static bool HasAny(this KrComponents flags, KrComponents flag)
        {
            return (flags & flag) != 0;
        }

        /// <doc path='info[@type="flags" and @item="HasNot"]'/>
        public static bool HasNot(this KrComponents flags, KrComponents flag)
        {
            return (flags & flag) == 0;
        }

        /// <doc path='info[@type="flags" and @item="Has"]'/>
        public static bool Has(this InfoAboutChanges flags, InfoAboutChanges flag)
        {
            return (flags & flag) == flag;
        }

        /// <doc path='info[@type="flags" and @item="HasAny"]'/>
        public static bool HasAny(this InfoAboutChanges flags, InfoAboutChanges flag)
        {
            return (flags & flag) != 0;
        }

        /// <doc path='info[@type="flags" and @item="HasNot"]'/>
        public static bool HasNot(this InfoAboutChanges flags, InfoAboutChanges flag)
        {
            return (flags & flag) == 0;
        }

        #endregion

        #region Card Extensions

        /// <summary>
        /// Возвращает секцию заданной карточки содержащую информацию по процессу.
        /// </summary>
        /// <param name="card">Карточка, для которой требуется получить секцию.</param>
        /// <returns>
        /// Секция карточки.<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Идентификатор типа карточки</description>
        ///         <description>Имя возвращаемой секции карточки</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSatelliteTypeID"/></description>
        ///         <description><see cref="KrApprovalCommonInfo.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSecondarySatelliteTypeID"/></description>
        ///         <description><see cref="KrSecondaryProcessCommonInfo.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description>Любой другой тип</description>
        ///         <description><see cref="KrApprovalCommonInfo.Virtual"/></description>
        ///     </item>
        /// </list>
        /// </returns>
        public static CardSection GetApprovalInfoSection(this Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            return card.Sections[GetApprovalInfoAlias(card.TypeID)];
        }

        /// <summary>
        /// Возвращает секцию заданной карточки содержащую информацию по процессу.
        /// </summary>
        /// <param name="card">Карточка, для которой требуется получить секцию.</param>
        /// <param name="section">
        /// Возвращаемое значение. Секция карточки или значение по умолчанию для типа, если секцию карточки получить не удалось.<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Идентификатор типа карточки</description>
        ///         <description>Имя возвращаемой секции карточки</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSatelliteTypeID"/></description>
        ///         <description><see cref="KrApprovalCommonInfo.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSecondarySatelliteTypeID"/></description>
        ///         <description><see cref="KrSecondaryProcessCommonInfo.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description>Любой другой тип</description>
        ///         <description><see cref="KrApprovalCommonInfo.Virtual"/></description>
        ///     </item>
        /// </list>
        /// </param>
        /// <returns>Значение <see langword="true"/>, если секцию содержащую информацию по процессу удалось получить, иначе - <see langword="false"/>.</returns>
        public static bool TryGetKrApprovalCommonInfoSection(this Card card, out CardSection section)
        {
            Check.ArgumentNotNull(card, nameof(card));

            return card.Sections.TryGetValue(GetApprovalInfoAlias(card.TypeID), out section);
        }

        /// <summary>
        /// Возвращает секцию заданной карточки содержащую информацию по этапам процесса.
        /// </summary>
        /// <param name="card">Карточка, для которой требуется получить секцию.</param>
        /// <param name="preferVirtual">Значение <see langword="true"/>, если для типов карточек с идентификатором <see cref="DefaultCardTypes.KrStageTemplateTypeID"/> или <see cref="DefaultCardTypes.KrSecondaryProcessTypeID"/> необходимо возвратить секцию <see cref="KrStages.Virtual"/>, иначе - <see langword="false"/> - будет возвращена секция <see cref="KrStages.Name"/>.</param>
        /// <returns>
        /// Секция карточки.<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Идентификатор типа карточки</description>
        ///         <description>Значение параметра <paramref name="preferVirtual"/></description>
        ///         <description>Имя возвращаемой секции карточки</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSatelliteTypeID"/></description>
        ///         <description>Любое значение</description>
        ///         <description><see cref="KrStages.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSecondarySatelliteTypeID"/></description>
        ///         <description>Любое значение</description>
        ///         <description><see cref="KrStages.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrStageTemplateTypeID"/></description>
        ///         <description><see langword="false"/></description>
        ///         <description><see cref="KrStages.Virtual"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrStageTemplateTypeID"/></description>
        ///         <description><see langword="true"/></description>
        ///         <description><see cref="KrStages.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSecondaryProcessTypeID"/></description>
        ///         <description><see langword="false"/></description>
        ///         <description><see cref="KrStages.Virtual"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSecondaryProcessTypeID"/></description>
        ///         <description><see langword="true"/></description>
        ///         <description><see cref="KrStages.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description>Любой другой тип</description>
        ///         <description>Любое значение</description>
        ///         <description><see cref="KrStages.Virtual"/></description>
        ///     </item>
        /// </list>
        /// </returns>
        public static CardSection GetStagesSection(this Card card, bool preferVirtual = false)
        {
            Check.ArgumentNotNull(card, nameof(card));

            return card.Sections[GetStagesAlias(card.TypeID, preferVirtual)];
        }

        /// <summary>
        /// Возвращает секцию заданной карточки содержащую информацию по этапам процесса.
        /// </summary>
        /// <param name="card">Карточка, для которой требуется получить секцию.</param>
        /// <param name="section">
        /// Возвращаемое значение. Секция карточки или значение по умолчанию для типа, если секцию карточки получить не удалось.<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Идентификатор типа карточки</description>
        ///         <description>Значение параметра <paramref name="preferVirtual"/></description>
        ///         <description>Имя возвращаемой секции карточки</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSatelliteTypeID"/></description>
        ///         <description>Любое значение</description>
        ///         <description><see cref="KrStages.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSecondarySatelliteTypeID"/></description>
        ///         <description>Любое значение</description>
        ///         <description><see cref="KrStages.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrStageTemplateTypeID"/></description>
        ///         <description><see langword="false"/></description>
        ///         <description><see cref="KrStages.Virtual"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrStageTemplateTypeID"/></description>
        ///         <description><see langword="true"/></description>
        ///         <description><see cref="KrStages.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSecondaryProcessTypeID"/></description>
        ///         <description><see langword="false"/></description>
        ///         <description><see cref="KrStages.Virtual"/></description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSecondaryProcessTypeID"/></description>
        ///         <description><see langword="true"/></description>
        ///         <description><see cref="KrStages.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description>Любой другой тип</description>
        ///         <description>Любое значение</description>
        ///         <description><see cref="KrStages.Virtual"/></description>
        ///     </item>
        /// </list>
        /// </param>
        /// <param name="preferVirtual">Значение <see langword="true"/>, если для типов карточек с идентификатором <see cref="DefaultCardTypes.KrStageTemplateTypeID"/> или <see cref="DefaultCardTypes.KrSecondaryProcessTypeID"/> необходимо возвратить секцию <see cref="KrStages.Virtual"/>, иначе - <see langword="false"/> - будет возвращена секция <see cref="KrStages.Name"/>.</param>
        /// <returns>Значение <see langword="true"/>, если секцию содержащую информацию по этапам процесса удалось получить, иначе - <see langword="false"/>.</returns>
        public static bool TryGetStagesSection(this Card card, out CardSection section, bool preferVirtual = false)
        {
            Check.ArgumentNotNull(card, nameof(card));

            var sections = card.TryGetSections();
            if (sections is null || sections.Count == 0)
            {
                section = null;
                return false;
            }

            return sections.TryGetValue(GetStagesAlias(card.TypeID, preferVirtual), out section);
        }

        /// <summary>
        /// Возвращает секцию карточки содержащей исполнителей этапов.
        /// </summary>
        /// <param name="card">Карточка содержащая получаемую секцию.</param>
        /// <returns>
        /// Секция карточки.<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Идентификатор типа карточки</description>
        ///         <description>Имя возвращаемой секции карточки</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrApprovalStageTypeSettingsTypeID"/></description>
        ///         <description><see cref="KrPerformersVirtual.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description>Любой другой тип</description>
        ///         <description><see cref="KrPerformersVirtual.Synthetic"/></description>
        ///     </item>
        /// </list>
        /// </returns>
        public static CardSection GetPerformersSection(this Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            return card.Sections[GetPerformersAlias(card.TypeID)];
        }

        /// <summary>
        /// Возвращает секцию карточки содержащей исполнителей этапов.
        /// </summary>
        /// <param name="card">Карточка содержащая получаемую секцию.</param>
        /// <param name="section">
        /// Возвращаемое значение. Секция карточки.<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Идентификатор типа карточки</description>
        ///         <description>Имя возвращаемой секции карточки</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrApprovalStageTypeSettingsTypeID"/></description>
        ///         <description><see cref="KrPerformersVirtual.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description>Любой другой тип</description>
        ///         <description><see cref="KrPerformersVirtual.Synthetic"/></description>
        ///     </item>
        /// </list>
        /// </param>
        /// <returns>Значение <see langword="true"/>, если секцию содержащую информацию по этапам процесса удалось получить, иначе - <see langword="false"/>.</returns>
        public static bool TryGetPerformersSection(this Card card, out CardSection section)
        {
            Check.ArgumentNotNull(card, nameof(card));

            return card.Sections.TryGetValue(GetPerformersAlias(card.TypeID), out section);
        }

        /// <summary>
        /// Возвращает секцию карточки содержащую активные задания.
        /// </summary>
        /// <param name="card">Карточка содержащая получаемую секцию.</param>
        /// <returns>
        /// Секция карточки.<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Идентификатор типа карточки</description>
        ///         <description>Имя возвращаемой секции карточки</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSatelliteTypeID"/></description>
        ///         <description><see cref="KrActiveTasks.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description>Любой другой тип</description>
        ///         <description><see cref="KrActiveTasks.Synthetic"/></description>
        ///     </item>
        /// </list>
        /// </returns>
        public static CardSection GetActiveTasksSection(this Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            return card.Sections[GetActiveTaskAlias(card.TypeID)];
        }

        /// <summary>
        /// Возвращает секцию карточки содержащую активные задания.
        /// </summary>
        /// <param name="card">Карточка содержащая получаемую секцию.</param>
        /// <param name="section">
        /// Возвращаемое значение. Секция карточки.<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Идентификатор типа карточки</description>
        ///         <description>Имя возвращаемой секции карточки</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSatelliteTypeID"/></description>
        ///         <description><see cref="KrActiveTasks.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description>Любой другой тип</description>
        ///         <description><see cref="KrActiveTasks.Synthetic"/></description>
        ///     </item>
        /// </list>
        /// </param>
        /// <returns>Значение <see langword="true"/>, если секцию содержащую информацию по этапам процесса удалось получить, иначе - <see langword="false"/>.</returns>
        public static bool TryGetActiveTasksSection(this Card card, out CardSection section)
        {
            Check.ArgumentNotNull(card, nameof(card));

            return card.Sections.TryGetValue(GetActiveTaskAlias(card.TypeID), out section);
        }

        /// <summary>
        /// Возвращает секцию карточки содержащую сопоставление истории заданий с историей согласования (с учетом циклов согласования).
        /// </summary>
        /// <param name="card">Карточка содержащая получаемую секцию.</param>
        /// <returns>
        /// Секция карточки.<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Идентификатор типа карточки</description>
        ///         <description>Имя возвращаемой секции карточки</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSatelliteTypeID"/></description>
        ///         <description><see cref="KrApprovalHistory.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description>Любой другой тип</description>
        ///         <description><see cref="KrApprovalHistory.Synthetic"/></description>
        ///     </item>
        /// </list>
        /// </returns>
        public static CardSection GetKrApprovalHistorySection(this Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            return card.Sections[GetApprovalHistoryAlias(card.TypeID)];
        }

        /// <summary>
        /// Возвращает секцию карточки содержащую сопоставление истории заданий с историей согласования (с учетом циклов согласования).
        /// </summary>
        /// <param name="card">Карточка содержащая получаемую секцию.</param>
        /// <param name="section">
        /// Возвращаемое значение. Секция карточки.<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Идентификатор типа карточки</description>
        ///         <description>Имя возвращаемой секции карточки</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="DefaultCardTypes.KrSatelliteTypeID"/></description>
        ///         <description><see cref="KrApprovalHistory.Name"/></description>
        ///     </item>
        ///     <item>
        ///         <description>Любой другой тип</description>
        ///         <description><see cref="KrApprovalHistory.Synthetic"/></description>
        ///     </item>
        /// </list>
        /// </param>
        /// <returns>Значение <see langword="true"/>, если секцию содержащую информацию по этапам процесса удалось получить, иначе - <see langword="false"/>.</returns>
        public static bool TryGetKrApprovalHistorySection(this Card card, out CardSection section)
        {
            Check.ArgumentNotNull(card, nameof(card));

            return card.Sections.TryGetValue(GetApprovalHistoryAlias(card.TypeID), out section);
        }

        private static string GetApprovalInfoAlias(Guid cardTypeID)
        {
            string secAlias;
            if (cardTypeID == DefaultCardTypes.KrSatelliteTypeID)
            {
                secAlias = KrApprovalCommonInfo.Name;
            }
            else if (cardTypeID == DefaultCardTypes.KrSecondarySatelliteTypeID)
            {
                secAlias = KrSecondaryProcessCommonInfo.Name;
            }
            else
            {
                secAlias = KrApprovalCommonInfo.Virtual;
            }

            return secAlias;
        }

        private static string GetStagesAlias(
            Guid cardTypeID,
            bool preferVirtual)
        {
            string secAlias;
            if (cardTypeID == DefaultCardTypes.KrSatelliteTypeID
                || cardTypeID == DefaultCardTypes.KrSecondarySatelliteTypeID
                || (cardTypeID == DefaultCardTypes.KrStageTemplateTypeID && !preferVirtual)
                || (cardTypeID == DefaultCardTypes.KrSecondaryProcessTypeID && !preferVirtual))
            {
                secAlias = KrStages.Name;
            }
            else
            {
                secAlias = KrStages.Virtual;
            }

            return secAlias;
        }

        private static string GetPerformersAlias(Guid cardTypeID)
        {
            return cardTypeID == DefaultCardTypes.KrApprovalStageTypeSettingsTypeID
                ? KrPerformersVirtual.Name
                : KrPerformersVirtual.Synthetic;
        }

        private static string GetActiveTaskAlias(Guid cardTypeID)
        {
            return cardTypeID == DefaultCardTypes.KrSatelliteTypeID
                ? KrActiveTasks.Name
                : KrActiveTasks.Virtual;
        }

        private static string GetApprovalHistoryAlias(Guid cardTypeID)
        {
            return cardTypeID == DefaultCardTypes.KrSatelliteTypeID
                ? KrApprovalHistory.Name
                : KrApprovalHistory.Virtual;
        }

        #endregion

        #region CardInfoStorageObject

        #region SecondaryProcessInfo

        internal const string SecondaryProcessInfoMark = nameof(SecondaryProcessInfoMark);

        /// <summary>
        /// Устанавливает информацию о процессе, запускаемого посредством <see cref="WorkflowStoreExtension"/>.
        /// </summary>
        /// <param name="request">Запрос к сервису карточек на сохранение карточки.</param>
        /// <param name="info">Информация о запускаемом процессе.</param>
        /// <returns>Текущий объект <paramref name="request"/> для цепочки действий.</returns>
        public static void SetStartingSecondaryProcess(
            this CardInfoStorageObject request,
            StartingSecondaryProcessInfo info)
        {
            Check.ArgumentNotNull(request, nameof(request));
            Check.ArgumentNotNull(info, nameof(info));

            request.Info[SecondaryProcessInfoMark] = info.GetStorage();
        }

        /// <summary>
        /// Возвращает из объекта содержащего дополнительную информацию, информацию необходимую для запуска процесса.
        /// </summary>
        /// <param name="cardInfoStorageObject">Объект содержащий информацию.</param>
        /// <returns>Информацию необходимая для запуска процесса или значение по умолчанию для типа, если её не удалось получить.</returns>
        public static StartingSecondaryProcessInfo GetStartingSecondaryProcess(
            this CardInfoStorageObject cardInfoStorageObject)
        {
            Check.ArgumentNotNull(cardInfoStorageObject, nameof(cardInfoStorageObject));

            Dictionary<string, object> info;

            if ((info = cardInfoStorageObject.TryGetInfo()) != null
                && info.TryGetValue(SecondaryProcessInfoMark, out var startingSecondaryProcessInfoStorageObj)
                && startingSecondaryProcessInfoStorageObj is Dictionary<string, object> startingSecondaryProcessInfoStorage)
            {
                return new StartingSecondaryProcessInfo(startingSecondaryProcessInfoStorage);
            }

            return null;
        }

        /// <summary>
        /// Удаляет из объекта содержащего дополнительную информацию, информацию необходимую для запуска процесса добавленную <see cref="SetStartingSecondaryProcess(CardInfoStorageObject, StartingSecondaryProcessInfo)"/>.
        /// </summary>
        /// <param name="cardInfoStorageObject">Объект содержащий информацию.</param>
        public static void RemoveSecondaryProcess(this CardInfoStorageObject cardInfoStorageObject)
        {
            Check.ArgumentNotNull(cardInfoStorageObject, nameof(cardInfoStorageObject));

            cardInfoStorageObject.TryGetInfo()?.Remove(SecondaryProcessInfoMark);
        }

        #endregion

        #region KrProcessClientCommandInfo

        public const string KrProcessClientCommandInfoMark = nameof(KrProcessClientCommandInfoMark);

        /// <summary>
        /// Добавляет в указанное хранилище список клиентских команд.
        /// </summary>
        /// <param name="storage">Хранилище в котором должно быть сохранено значение.</param>
        /// <param name="commands">Список клиентских команд.</param>
        public static void AddKrProcessClientCommands(
            this CardInfoStorageObject storage,
            List<KrProcessClientCommand> commands)
        {
            Check.ArgumentNotNull(storage, nameof(storage));
            Check.ArgumentNotNull(commands, nameof(commands));

            if (!storage.Info.TryGetValue(KrProcessClientCommandInfoMark, out var clientCommandsObj)
                || clientCommandsObj is not IList clientCommands)
            {
                clientCommands = new List<object>();
            }

            foreach (var command in commands.Select(p => p.GetStorage()))
            {
                clientCommands.Add(command);
            }

            storage.Info[KrProcessClientCommandInfoMark] = clientCommands;
        }

        /// <summary>
        /// Возвращает из указанного хранилища список клиентских команд или значение по умолчанию для типа, если оно их не содержало.
        /// </summary>
        /// <param name="storage">Хранилище содержащее получаемое значение.</param>
        /// <returns>Список клиентских команд или значение по умолчанию для типа, если оно их не содержало.</returns>
        public static List<KrProcessClientCommand> GetKrProcessClientCommands(
            this CardInfoStorageObject storage)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            return storage.Info.GetKrProcessClientCommands();
        }

        /// <summary>
        /// Возвращает из указанной коллекции &lt;ключ-значение&gt; список клиентских команд или значение по умолчанию для типа, если она их не содержала.
        /// </summary>
        /// <param name="storage">Коллекция &lt;ключ-значение&gt; содержащая клиентские команды.</param>
        /// <returns>Список клиентских команд или значение по умолчанию для типа, если коллекция их не содержит.</returns>
        public static List<KrProcessClientCommand> GetKrProcessClientCommands(
            this IDictionary<string, object> storage)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            if (storage.TryGetValue(KrProcessClientCommandInfoMark, out var commandObj))
            {
                switch (commandObj)
                {
                    case List<object> groupsObjList:
                        return groupsObjList
                            .Cast<Dictionary<string, object>>()
                            .Select(p => new KrProcessClientCommand(p))
                            .ToList();

                    case List<Dictionary<string, object>> groupsStorage:
                        return groupsStorage
                            .Select(p => new KrProcessClientCommand(p))
                            .ToList();
                }
            }

            return null;
        }

        /// <summary>
        /// Возвращает из указанного хранилища список клиентских команд или значение по умолчанию для типа, если оно их не содержало.
        /// </summary>
        /// <param name="storage">Хранилище содержащее получаемое значение.</param>
        /// <param name="clientCommands">Возвращаемое значение. Список клиентских команд или значение по умолчанию для типа, если коллекция их не содержит.</param>
        /// <returns>Значение <see langword="true"/>, если хранилище содержит клиентские команды, иначе - <see langword="false"/>.</returns>
        public static bool TryGetKrProcessClientCommands(
            this CardInfoStorageObject storage,
            out List<KrProcessClientCommand> clientCommands)
        {
            // storage будет проверен в GetKrProcessClientCommands.

            var value = GetKrProcessClientCommands(storage);
            clientCommands = value;
            return value is not null;
        }

        #endregion

        #region IgnoreButtonsInfo

        internal const string IgnoreButtonsInfoMark = nameof(IgnoreButtonsInfoMark);

        /// <summary>
        /// Устанавливает значение, показывающее, что при загрузке карточки не надо добавлять в ответ информацию по тайлам вторичных процессов.
        /// </summary>
        /// <param name="request">Хранилище в котором устанавливается флаг.</param>
        /// <param name="value">Значение <see langword="true"/>, если при загрузке карточки не надо добавлять в ответ информацию по тайлам вторичных процессов, иначе - <see langword="false"/>.</param>
        public static void IgnoreButtons(
            this CardInfoStorageObject request,
            bool value = true)
        {
            Check.ArgumentNotNull(request, nameof(request));

            request.Info[IgnoreButtonsInfoMark] = BooleanBoxes.Box(value);
        }

        /// <summary>
        /// Получает из заданного хранилища значение флага показывающего, что при загрузке карточки не надо добавлять в ответ информацию по тайлам вторичных процессов.
        /// </summary>
        /// <param name="request">Хранилище из которого запрашивается значение флага.</param>
        /// <returns>Значение <see langword="true"/>, если при загрузке карточки не надо добавлять в ответ информацию по тайлам вторичных процессов, иначе - <see langword="false"/>.</returns>
        public static bool AreButtonsIgnored(
            this CardInfoStorageObject request)
        {
            Check.ArgumentNotNull(request, nameof(request));

            return request.Info.TryGet(IgnoreButtonsInfoMark, false);
        }

        #endregion

        #region DontHideStagesInfo

        internal const string DontHideStagesInfoMark = nameof(DontHideStagesInfoMark);

        /// <summary>
        /// Добавляет в указанный словарь значение, показывающее, необходимо ли загружать в карточку скрытые этапы маршрута или нет.
        /// </summary>
        /// <param name="storage">Словарь, в который должно быть добавлено значение управляющее загрузкой скрытых этапов маршрута.</param>
        /// <param name="value">Значение <see langword="true"/>, если в карточку должны быть загружены скрытые этапы маршрута, иначе - <see langword="false"/>.</param>
        public static void DontHideStages(
            this IDictionary<string, object> storage,
            bool value = true)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            storage[DontHideStagesInfoMark] = BooleanBoxes.Box(value);
        }

        /// <summary>
        /// Добавляет в указанное хранилище значение, показывающее, необходимо ли загружать в карточку скрытые этапы маршрута или нет.
        /// </summary>
        /// <param name="request">Хранилище в котором устанавливается флаг.</param>
        /// <param name="value">Значение <see langword="true"/>, если в карточку должны быть загружены скрытые этапы маршрута, иначе - <see langword="false"/>.</param>
        public static void DontHideStages(
            this CardInfoStorageObject request,
            bool value = true)
        {
            Check.ArgumentNotNull(request, nameof(request));

            request.Info.DontHideStages(value);
        }

        /// <summary>
        /// Возвращает значение, показывающее, что в карточку должны быть загружены скрытые этапы маршрута.
        /// </summary>
        /// <param name="request">Хранилище из которого запрашивается значение флага.</param>
        /// <returns>Значение <see langword="true"/>, если в карточку должны быть загружены скрытые этапы маршрута, иначе - <see langword="false"/>.</returns>
        public static bool ConsiderHiddenStages(
            this CardInfoStorageObject request)
        {
            Check.ArgumentNotNull(request, nameof(request));

            return !request.Info.TryGet(DontHideStagesInfoMark, false);
        }

        #endregion

        #region IgnoreKrSatelliteInfo

        internal const string IgnoreKrSatelliteInfoMark = nameof(IgnoreKrSatelliteInfoMark);

        /// <summary>
        /// Устанавливает значение, показывающее, что при загрузке карточки не надо загружать и обрабатывать информацию содержащуюся в основном сателлите (<see cref="DefaultCardTypes.KrSatelliteTypeID"/>) карточки.
        /// </summary>
        /// <param name="request">Хранилище в котором устанавливается флаг.</param>
        /// <param name="value">Значение <see langword="true"/>, если при загрузке карточки не надо загружать и обрабатывать информацию содержащуюся в основном сателлите (<see cref="DefaultCardTypes.KrSatelliteTypeID"/>) карточки, иначе - <see langword="false"/>.</param>
        public static void IgnoreKrSatellite(
            this CardInfoStorageObject request,
            bool value = true)
        {
            Check.ArgumentNotNull(request, nameof(request));

            request.Info[IgnoreKrSatelliteInfoMark] = BooleanBoxes.Box(value);
        }

        /// <summary>
        /// Возвращает значение, показывающее, что при загрузке карточки не надо загружать и обрабатывать информацию содержащуюся в основном сателлите (<see cref="DefaultCardTypes.KrSatelliteTypeID"/>) карточки.
        /// </summary>
        /// <param name="request">Хранилище из которого запрашивается значение флага.</param>
        /// <returns>Значение <see langword="true"/>, если при загрузке карточки не надо загружать и обрабатывать информацию содержащуюся в основном сателлите (<see cref="DefaultCardTypes.KrSatelliteTypeID"/>) карточки, иначе - <see langword="false"/>.</returns>
        public static bool IsKrSatelliteIgnored(
            this CardInfoStorageObject request)
        {
            Check.ArgumentNotNull(request, nameof(request));

            return request.Info.TryGet(IgnoreKrSatelliteInfoMark, false);
        }

        #endregion

        #region GlobalTilesInfo

        internal const string GlobalTilesInfoMark = nameof(GlobalTilesInfoMark);

        /// <summary>
        /// Сохраняет в указанном объекте коллекцию объектов содержащих информацию о глобальных тайлах маршрутов.
        /// </summary>
        /// <param name="response">Ответ на запрос на инициализацию приложения.</param>
        /// <param name="tileInfos">Коллекция объектов содержащих информацию о глобальных тайлах маршрутов.</param>
        public static void SetGlobalTiles(
            this InitializationResponse response,
            List<KrTileInfo> tileInfos)
        {
            Check.ArgumentNotNull(response, nameof(response));
            Check.ArgumentNotNull(tileInfos, nameof(tileInfos));

            response.Info[GlobalTilesInfoMark] = tileInfos
                .Select(p => p.GetStorage())
                .ToList();
        }

        /// <summary>
        /// Получает из указанного объекта коллекцию объектов содержащих информацию о глобальных тайлах маршрутов.
        /// </summary>
        /// <param name="response">Ответ на запрос на инициализацию приложения.</param>
        /// <returns>Коллекция объектов содержащих информацию о глобальных тайлах маршрутов.</returns>
        public static List<KrTileInfo> GetGlobalTiles(
            this InitializationResponse response)
        {
            Check.ArgumentNotNull(response, nameof(response));

            if (response.Info.TryGetValue(GlobalTilesInfoMark, out var groupsObj))
            {
                switch (groupsObj)
                {
                    case List<object> groupsObjList:
                        return groupsObjList
                            .Cast<Dictionary<string, object>>()
                            .Select(p => new KrTileInfo(p))
                            .ToList();
                    case List<Dictionary<string, object>> groupsStorage:
                        return groupsStorage
                            .Select(p => new KrTileInfo(p))
                            .ToList();
                }
            }

            return null;
        }

        /// <summary>
        /// Получает из указанного объекта коллекцию объектов содержащих информацию о глобальных тайлах маршрутов.
        /// </summary>
        /// <param name="response">Ответ на запрос на инициализацию приложения.</param>
        /// <param name="globalTiles">Возвращаемое значение. Коллекция объектов содержащих информацию о глобальных тайлах маршрутов.</param>
        /// <returns>Значение <see langword="true"/>, если объект содержал коллекцию объектов содержащих информацию о глобальных тайлах маршрутов, иначе - <see langword="false"/>.</returns>
        public static bool TryGetGlobalTiles(
            this InitializationResponse response,
            out List<KrTileInfo> globalTiles)
        {
            // response будет проверен в GetGlobalTiles.

            var value = GetGlobalTiles(response);
            globalTiles = value;
            return value is not null;
        }

        #endregion

        #region LocalTilesInfo

        internal const string LocalTilesInfoMark = nameof(LocalTilesInfoMark);

        /// <summary>
        /// Сохраняет в указанном объекте коллекцию объектов содержащих информацию о локальных тайлах маршрутов.
        /// </summary>
        /// <param name="storage">Хранилище в котором должно быть сохранено значение.</param>
        /// <param name="tileInfos">Коллекция объектов содержащих информацию о локальных тайлах маршрутов.</param>
        public static void SetLocalTiles(
            this CardInfoStorageObject storage,
            List<KrTileInfo> tileInfos)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            storage.Info[LocalTilesInfoMark] = tileInfos
                .Select(p => p.GetStorage())
                .ToList();
        }

        /// <summary>
        /// Получает из указанного объекта коллекцию объектов содержащих информацию о локальных тайлах маршрутов.
        /// </summary>
        /// <param name="storage">Хранилище из которого запрашивается значение.</param>
        /// <returns>Коллекция объектов содержащих информацию о локальных тайлах маршрутов или значение <see langword="null"/>, если информация о тайлах не найдена.</returns>
        public static List<KrTileInfo> GetLocalTiles(
            this CardInfoStorageObject storage)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            if (storage.Info.TryGetValue(LocalTilesInfoMark, out var groupsObj))
            {
                switch (groupsObj)
                {
                    case List<object> groupsObjList:
                        return groupsObjList
                            .Cast<Dictionary<string, object>>()
                            .Select(p => new KrTileInfo(p))
                            .ToList();
                    case List<Dictionary<string, object>> groupsStorage:
                        return groupsStorage
                            .Select(p => new KrTileInfo(p))
                            .ToList();
                }
            }

            return null;
        }

        /// <summary>
        /// Получает из указанного объекта коллекцию объектов содержащих информацию о локальных тайлах маршрутов.
        /// </summary>
        /// <param name="storage">Хранилище из которого запрашивается значение.</param>
        /// <param name="localTiles">Возвращаемое значение. Коллекция объектов содержащих информацию о локальных тайлах маршрутов.</param>
        /// <returns>Значение <see langword="true"/>, если объект содержал коллекцию объектов содержащих информацию о локальных тайлах маршрутов, иначе - <see langword="false"/>.</returns>
        public static bool TryGetLocalTiles(
            this CardInfoStorageObject storage,
            out List<KrTileInfo> localTiles)
        {
            // storage будет проверен в GetLocalTiles.

            var value = GetLocalTiles(storage);
            localTiles = value;
            return value is not null;
        }

        /// <summary>
        /// Удаляет из заданного хранилища информацию по локальным тайлам маршрутов.
        /// </summary>
        /// <param name="storage">Хранилище из которого удаляется значение.</param>
        public static void RemoveLocalTiles(
            this CardInfoStorageObject storage) => storage?.Info?.Remove(LocalTilesInfoMark);

        #endregion

        #region StartKrProcessInstance

        internal const string StartKrProcessInstance = nameof(StartKrProcessInstance);

        /// <summary>
        /// Сохраняет в указанном объекте информация об экземпляре процесса.
        /// </summary>
        /// <param name="storage">Хранилище в котором должно быть сохранено значение.</param>
        /// <param name="krProcess">Информация об экземпляре процесса.</param>
        public static void SetKrProcessInstance(
            this CardInfoStorageObject storage,
            KrProcessInstance krProcess)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            storage.Info.SetKrProcessInstance(krProcess);
        }

        /// <summary>
        /// Сохраняет в указанном объекте информация об экземпляре процесса.
        /// </summary>
        /// <param name="storage">Хранилище в котором должно быть сохранено значение.</param>
        /// <param name="krProcess">Информация об экземпляре процесса.</param>
        public static void SetKrProcessInstance(
            this Dictionary<string, object> storage,
            KrProcessInstance krProcess)
        {
            Check.ArgumentNotNull(storage, nameof(storage));
            Check.ArgumentNotNull(krProcess, nameof(krProcess));

            storage[StartKrProcessInstance] = krProcess.GetStorage();
        }

        /// <summary>
        /// Возвращает информацию об экземпляре процесса из  указанного хранилища.
        /// </summary>
        /// <param name="storage">Хранилище из которого должно быть получено значение.</param>
        /// <returns>Информация об экземпляре процесса.</returns>
        public static KrProcessInstance GetKrProcessInstance(
            this CardInfoStorageObject storage)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            if (storage.Info.TryGetValue(StartKrProcessInstance, out var processObj)
                && processObj is Dictionary<string, object> processDict)
            {
                return new KrProcessInstance(processDict);
            }

            return null;
        }

        /// <summary>
        /// Возвращает информацию об экземпляре процесса из  указанного хранилища.
        /// </summary>
        /// <param name="storage">Хранилище из которого должно быть получено значение.</param>
        /// <param name="krProcessInstance">Возвращаемое значение. Информация об экземпляре процесса.</param>
        /// <returns>Значение <see langword="true"/>, если объект содержал искомую информацию, иначе - <see langword="false"/>.</returns>
        public static bool TryGetKrProcessInstance(
            this CardInfoStorageObject storage,
            out KrProcessInstance krProcessInstance)
        {
            // storage будет проверен в GetKrProcessInstance.

            var value = GetKrProcessInstance(storage);
            krProcessInstance = value;
            return value is not null;
        }

        #endregion

        #region KrProcessLaunchResult

        internal const string KrProcessLaunchResult = nameof(KrProcessLaunchResult);

        /// <summary>
        /// Сохраняет результаты запуска процесса в указанном хранилище.
        /// </summary>
        /// <param name="storage">Хранилище в котором должны быть сохранены результаты запуска процесса.</param>
        /// <param name="launchResult">Объект содержащий результат запуска процесса.</param>
        public static void SetKrProcessLaunchResult(
            this CardInfoStorageObject storage,
            KrProcessLaunchResult launchResult)
        {
            Check.ArgumentNotNull(storage, nameof(storage));
            Check.ArgumentNotNull(launchResult, nameof(launchResult));

            storage.Info[KrProcessLaunchResult] = launchResult.GetStorage();
        }

        /// <summary>
        /// Возвращает объект содержащий результат запуска процесса или значение по умолчанию для типа, если указанный объект его не содержит.
        /// </summary>
        /// <param name="storage">Хранилище содержащее результаты запуска процесса.</param>
        /// <returns>Объект содержащий результат запуска процесса или значение по умолчанию для типа, если указанный объект его не содержит.</returns>
        public static KrProcessLaunchResult GetKrProcessLaunchResult(
            this CardInfoStorageObject storage)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            if (storage.Info.TryGetValue(KrProcessLaunchResult, out var processObj)
                && processObj is Dictionary<string, object> processDict)
            {
                return new KrProcessLaunchResult(processDict);
            }

            return null;
        }

        /// <summary>
        /// Возвращает объект, содержащий результат запуска процесса, с заполненными свойствами содержащими информацию по объекту его содержащему.
        /// </summary>
        /// <param name="cardStoreResponse">Ответ на запрос содержащий результаты запуска процесса. Может иметь значение по умолчанию для типа.</param>
        /// <returns>Объект содержащий результат запуска процесса.</returns>
        public static KrProcessLaunchResult GetKrProcessLaunchFullResult(
            this CardStoreResponse cardStoreResponse)
        {
            KrProcessLaunchStatus launchStatus;
            Guid? processID;
            IDictionary<string, object> processInfo;
            if (cardStoreResponse is not null && cardStoreResponse.TryGetKrProcessLaunchResult(out var result))
            {
                launchStatus = result.LaunchStatus;
                processID = result.ProcessID;
                processInfo = result.ProcessInfo;
            }
            else
            {
                launchStatus = KrProcessLaunchStatus.Undefined;
                processID = null;
                processInfo = null;
            }

            var krProcessLaunchResult = new KrProcessLaunchResult()
            {
                LaunchStatus = launchStatus,
                ProcessID = processID,
                ProcessInfo = processInfo,
                StoreResponse = cardStoreResponse,
            };

            if (cardStoreResponse is not null)
            {
                krProcessLaunchResult.ValidationResult.Add(cardStoreResponse.ValidationResult);
            }

            return krProcessLaunchResult;
        }

        /// <summary>
        /// Возвращает объект, содержащий результат запуска процесса, с заполненными свойствами содержащими информацию по объекту его содержащему.
        /// </summary>
        /// <param name="cardResponse">Ответ на запрос содержащий результаты запуска процесса. Может иметь значение по умолчанию для типа.</param>
        /// <returns>Объект содержащий результат запуска процесса.</returns>
        public static KrProcessLaunchResult GetKrProcessLaunchFullResult(
            this CardResponse cardResponse)
        {
            KrProcessLaunchStatus launchStatus;
            Guid? processID;
            IDictionary<string, object> processInfo;
            if (cardResponse is not null && cardResponse.TryGetKrProcessLaunchResult(out var result))
            {
                launchStatus = result.LaunchStatus;
                processID = result.ProcessID;
                processInfo = result.ProcessInfo;
            }
            else
            {
                launchStatus = KrProcessLaunchStatus.Undefined;
                processID = null;
                processInfo = null;
            }

            var krProcessLaunchResult = new KrProcessLaunchResult()
            {
                LaunchStatus = launchStatus,
                ProcessID = processID,
                ProcessInfo = processInfo,
                CardResponse = cardResponse,
            };

            if (cardResponse is not null)
            {
                krProcessLaunchResult.ValidationResult.Add(cardResponse.ValidationResult);
            }

            return krProcessLaunchResult;
        }

        /// <summary>
        /// Возвращает объект, содержащий результат запуска процесса.
        /// </summary>
        /// <param name="storage">Хранилище из которого должно быть получено значение.</param>
        /// <param name="result">Результаты запуска процесса.</param>
        /// <returns>Значение <see langword="true"/>, если объект содержал искомую информацию, иначе - <see langword="false"/>.</returns>
        public static bool TryGetKrProcessLaunchResult(
            this CardInfoStorageObject storage,
            out KrProcessLaunchResult result)
        {
            // storage будет проверен в GetKrProcessLaunchResult.

            var value = GetKrProcessLaunchResult(storage);
            result = value;
            return value is not null;
        }

        #endregion

        #region StagePositionsInfo

        internal const string StagePositionsInfoMark = "StagePositions";

        /// <summary>
        /// Задаёт в <see cref="Card"/>.<see cref="CardInfoStorageObject.Info"/> информацию о позиции этапов.
        /// </summary>
        /// <param name="card">Карточка в <see cref="CardInfoStorageObject.Info"/> которой сохраняется информация о позиции этапов.</param>
        /// <param name="stagePositions">Список содержащий информацию о позиции этапов.</param>
        public static void SetStagePositions(
            this Card card,
            List<object> stagePositions)
        {
            ThrowIfNull(card);
            ThrowIfNull(stagePositions);

            card.Info[StagePositionsInfoMark] = stagePositions;
        }

        /// <summary>
        /// Задаёт в <see cref="Card"/>.<see cref="CardInfoStorageObject.Info"/> информацию о позиции этапов.
        /// </summary>
        /// <param name="card">Карточка в <see cref="CardInfoStorageObject.Info"/> которой сохраняется информация о позиции этапов.</param>
        /// <param name="stagePositions">Список содержащий информацию о позиции этапов.</param>
        public static void SetStagePositions(
            this Card card,
            List<KrStagePositionInfo> stagePositions)
        {
            ThrowIfNull(card);
            ThrowIfNull(stagePositions);

            card.SetStagePositions(stagePositions.Select(i => i.GetStorage()).Cast<object>().ToList());
        }

        /// <summary>
        /// Возвращает информацию о позиции этапов.
        /// </summary>
        /// <param name="card">Карточка из <see cref="CardInfoStorageObject.Info"/> которой возвращается информация о позиции этапов.</param>
        /// <returns>Список содержащий информацию о позиции этапов или значение по умолчанию для типа, если её нет.</returns>
        public static List<KrStagePositionInfo> GetStagePositions(
            this Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            if (card.Info.TryGetValue(StagePositionsInfoMark, out var stagePositions)
                && stagePositions is IList stagePositionsList)
            {
                return stagePositionsList
                    .Cast<Dictionary<string, object>>()
                    .Select(p => new KrStagePositionInfo(p))
                    .ToList();
            }

            return null;
        }

        /// <summary>
        /// Возвращает информацию о позиции этапов.
        /// </summary>
        /// <param name="card">Карточка из <see cref="CardInfoStorageObject.Info"/> которой возвращается информация о позиции этапов.</param>
        /// <param name="stagePositions">Возвращаемое значение. Список содержащий информацию о позиции этапов или значение по умолчанию для типа, если её нет.</param>
        /// <returns>Значение <see langword="true"/>, если объект содержал искомую информацию, иначе - <see langword="false"/>.</returns>
        public static bool TryGetStagePositions(
            this Card card,
            out List<KrStagePositionInfo> stagePositions)
        {
            // card будет проверен в GetStagePositions.

            var value = GetStagePositions(card);
            stagePositions = value;
            return value is not null;
        }

        /// <summary>
        /// Возвращает значение, показывающее, что в <see cref="Card"/>.<see cref="CardInfoStorageObject.Info"/> содержится информация о позиции этапов.
        /// </summary>
        /// <param name="card">Карточка в <see cref="CardInfoStorageObject.Info"/> которой проверяется наличие информации о позиции этапов.</param>
        /// <param name="atLeastOne">Значение <see langword="true"/>, если необходимо проверить, что информации о позиции этапов содержит записи, иначе - <see langword="false"/> только её наличие.</param>
        /// <returns>Значение <see langword="true"/>, если в <see cref="Card"/>.<see cref="CardInfoStorageObject.Info"/> содержится информация о позиции этапов.</returns>
        public static bool HasStagePositions(
            this Card card,
            bool atLeastOne)
        {
            ThrowIfNull(card);

            return card.Info.TryGetValue(StagePositionsInfoMark, out var stagePositions)
                && stagePositions is IList stagePositionsList
                && (!atLeastOne
                    || stagePositionsList.Count > 0);
        }

        /// <summary>
        /// Возвращает значение, показывающее, что в информации о позиции этапов содержится информация о скрытых этапах.
        /// </summary>
        /// <param name="card">Карточка в <see cref="CardInfoStorageObject.Info"/> которой содержится информация о позиции этапов.</param>
        /// <returns>Значение, показывающее, что в информации о позиции этапов содержится информация о скрытых этапах.</returns>
        public static bool HasHiddenStages(
            this Card card)
        {
            ThrowIfNull(card);

            return card.Info.TryGetValue(StagePositionsInfoMark, out var stagePositions)
                && stagePositions is IList stagePositionsList
                && stagePositionsList.Cast<object>().Any(p => (p as Dictionary<string, object>)?.TryGet<bool?>(nameof(KrStagePositionInfo.Hidden)) == true);
        }

        /// <summary>
        /// Удаляет из <see cref="Card"/>.<see cref="CardInfoStorageObject.Info"/> информацию о позиции этапов.
        /// </summary>
        /// <param name="card">Карточка в <see cref="CardInfoStorageObject.Info"/> которой удаляется информация о позиции этапов.</param>
        public static void RemoveStagePositions(this Card card)
        {
            ThrowIfNull(card);

            card.Info.Remove(StagePositionsInfoMark);
        }

        #endregion

        #region AsyncProcessCompletedSimultaniosly

        internal const string AsyncProcessCompletedSimultaniosly = nameof(AsyncProcessCompletedSimultaniosly);

        /// <summary>
        /// Устанавливает значение, показывающее, что асинхронный процесс был завершён.
        /// </summary>
        /// <param name="info">Хранилище в котором должно быть сохранено значение.</param>
        /// <param name="flag">Значение <see langword="true"/>, если асинхронный процесс был завершён, иначе - <see langword="false"/>.</param>
        public static void SetAsyncProcessCompletedSimultaniosly(
            this IDictionary<string, object> info,
            bool flag = true)
        {
            Check.ArgumentNotNull(info, nameof(info));

            info[AsyncProcessCompletedSimultaniosly] = BooleanBoxes.Box(flag);
        }

        /// <summary>
        /// Возвращает значение, показывающее, что асинхронный процесс был завершён.
        /// </summary>
        /// <param name="info">Хранилище из которого должно быть получено значение.</param>
        /// <returns>Значение <see langword="true"/>, если асинхронный процесс был завершён, иначе - <see langword="false"/>.</returns>
        public static bool GetAsyncProcessCompletedSimultaniosly(
            this IDictionary<string, object> info)
        {
            Check.ArgumentNotNull(info, nameof(info));

            return info.TryGet<bool?>(AsyncProcessCompletedSimultaniosly) == true;
        }

        #endregion

        #region ProcessInfoAtEnd

        internal const string ProcessInfoAtEnd = nameof(ProcessInfoAtEnd);

        /// <summary>
        /// Задаёт дополнительную информацию завершившегося асинхронного процесса в указанном хранилище.
        /// </summary>
        /// <param name="info">Хранилище в котором должно быть сохранено значение.</param>
        /// <param name="processInfo">Дополнительную информация завершившегося асинхронного процесса.</param>
        public static void SetProcessInfoAtEnd(
            this IDictionary<string, object> info,
            IDictionary<string, object> processInfo)
        {
            Check.ArgumentNotNull(info, nameof(info));

            info[ProcessInfoAtEnd] = processInfo.ToDictionaryStorage();
        }

        /// <summary>
        /// Возвращает дополнительную информацию завершившегося асинхронного процесса.
        /// </summary>
        /// <param name="info">Хранилище из которого должно быть получено значение.</param>
        /// <returns>Дополнительная информация завершившегося асинхронного процесса.</returns>
        public static IDictionary<string, object> GetProcessInfoAtEnd(
            this IDictionary<string, object> info)
        {
            Check.ArgumentNotNull(info, nameof(info));

            return info.TryGet<IDictionary<string, object>>(ProcessInfoAtEnd);
        }

        #endregion

        #region StartingKrProcessParameters

        private const string StartingKrProcessParametersKey = CardHelper.SystemKeyPrefix + "startKrProcessParameters";

        /// <summary>
        /// Устанавливает параметры запускаемого процесса.
        /// </summary>
        /// <param name="storage">Хранилище в котором задаётся значение.</param>
        /// <param name="info">Параметры запускаемого процесса. Могут иметь значение по умолчанию для типа.</param>
        public static void SetStartingKrProcessParameters(
            this IDictionary<string, object> storage,
            Dictionary<string, object> info)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            storage[StartingKrProcessParametersKey] = info;
        }

        /// <summary>
        /// Устанавливает параметры запускаемого процесса.
        /// </summary>
        /// <param name="storage">Хранилище в котором задаётся значение.</param>
        /// <param name="info">Параметры запускаемого процесса. Могут иметь значение по умолчанию для типа.</param>
        public static void SetStartingKrProcessParameters(
            this CardInfoStorageObject storage,
            Dictionary<string, object> info)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            storage.Info.SetStartingKrProcessParameters(info);
        }

        /// <summary>
        /// Возвращает параметры запускаемого процесса.
        /// </summary>
        /// <param name="storage">Хранилище из которого возвращается значение.</param>
        /// <returns>Параметры запускаемого процесса или значение по умолчанию для типа, если они не содержатся в указанном хранилище.</returns>
        public static Dictionary<string, object> TryGetStartingKrProcessParameters(
            this IDictionary<string, object> storage)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            return storage.TryGet<Dictionary<string, object>>(StartingKrProcessParametersKey);
        }

        /// <summary>
        /// Возвращает параметры запускаемого процесса.
        /// </summary>
        /// <param name="storage">Хранилище из которого возвращается значение.</param>
        /// <returns>Параметры запускаемого процесса или значение по умолчанию для типа, если они не содержатся в указанном хранилище.</returns>
        public static Dictionary<string, object> TryGetStartingKrProcessParameters(
            this CardInfoStorageObject storage)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            return storage.TryGetInfo()?.TryGetStartingKrProcessParameters();
        }

        #endregion

        #endregion

        #region Recalc

        private const string RecalcInfoMark = CardHelper.SystemKeyPrefix + "Recalc";

        private const string InfoAboutChangesInfoMark = CardHelper.SystemKeyPrefix + "InfoAboutChanges";

        private const string HasChangesInRoute = CardHelper.SystemKeyPrefix + "HasChangesInRoute";

        private const string RouteChanges = CardHelper.SystemKeyPrefix + "RouteChanges";

        /// <summary>
        /// Возвращает значение, показывающее, должен ли быть выполнен пересчёт маршрута или нет.
        /// </summary>
        /// <param name="info">Хранилище из которого должно быть получено значение.</param>
        /// <returns>Значение <see langword="true"/>, если должен быть выполнен пересчёт маршрута, иначе - <see langword="false"/>.</returns>
        public static bool GetRecalcFlag(
            this IDictionary<string, object> info)
        {
            Check.ArgumentNotNull(info, nameof(info));

            return info.TryGetValue(RecalcInfoMark, out var flagObj)
                && flagObj is bool flag
                && flag;
        }

        /// <summary>
        /// Задаёт значение, показывающее, что должен быть выполнен пересчёт маршрута.
        /// </summary>
        /// <param name="info">Хранилище в котором должно быть сохранено значение.</param>
        public static void SetRecalcFlag(
            this IDictionary<string, object> info)
        {
            Check.ArgumentNotNull(info, nameof(info));

            info[RecalcInfoMark] = BooleanBoxes.True;
        }

        /// <summary>
        /// Возвращает значение, показывающее, должен ли быть выполнен пересчёт маршрута или нет.
        /// </summary>
        /// <param name="storage">Хранилище из которого должно быть получено значение.</param>
        /// <returns>Значение <see langword="true"/>, если должен быть выполнен пересчёт маршрута, иначе - <see langword="false"/>.</returns>
        public static bool GetRecalcFlag(
            this CardInfoStorageObject storage)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            return storage.Info.GetRecalcFlag();
        }

        /// <summary>
        /// Задаёт значение, показывающее, что должен быть выполнен пересчёт маршрута.
        /// </summary>
        /// <param name="storage">Хранилище в котором должно быть сохранено значение.</param>
        public static void SetRecalcFlag(
            this CardInfoStorageObject storage)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            storage.Info.SetRecalcFlag();
        }

        /// <summary>
        /// Возвращает режим вывода информации об изменениях в маршруте после пересчёта или значение по умолчанию для типа, если хранилище его не содержало.
        /// </summary>
        /// <param name="info">Хранилище из которого должно быть получено значение.</param>
        /// <returns>Режим вывода информации об изменениях в маршруте после пересчёта или значение по умолчанию для типа, если хранилище его не содержало.</returns>
        public static InfoAboutChanges? GetInfoAboutChanges(
            this IDictionary<string, object> info)
        {
            Check.ArgumentNotNull(info, nameof(info));

            return info.TryGetValue(InfoAboutChangesInfoMark, out var iacObj) && iacObj is int iac
                ? (InfoAboutChanges?) iac
                : null;
        }

        /// <summary>
        /// Устанавливает в хранилище информацию о режиме информирования об изменениях в маршруте после пересчёта.
        /// </summary>
        /// <param name="info">Хранилище в котором должно быть сохранено значение.</param>
        /// <param name="infoAboutChanges">Перечисление режимов вывода информации об изменениях в маршруте после пересчёта.</param>
        public static void SetInfoAboutChanges(
            this IDictionary<string, object> info,
            InfoAboutChanges infoAboutChanges)
        {
            Check.ArgumentNotNull(info, nameof(info));

            info[InfoAboutChangesInfoMark] = Int32Boxes.Box((int) infoAboutChanges);
        }

        /// <summary>
        /// Возвращает режим вывода информации об изменениях в маршруте после пересчёта или значение по умолчанию для типа, если хранилище его не содержало.
        /// </summary>
        /// <param name="storage">Хранилище из которого должно быть получено значение.</param>
        /// <returns>Режим вывода информации об изменениях в маршруте после пересчёта или значение по умолчанию для типа, если хранилище его не содержало.</returns>
        public static InfoAboutChanges? GetInfoAboutChanges(
            this CardInfoStorageObject storage)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            return storage.Info.GetInfoAboutChanges();
        }

        /// <summary>
        /// Устанавливает в хранилище информацию о режиме информирования об изменениях в маршруте после пересчёта.
        /// </summary>
        /// <param name="storage">Хранилище в котором должно быть сохранено значение.</param>
        /// <param name="infoAboutChanges">Перечисление режимов вывода информации об изменениях в маршруте после пересчёта.</param>
        public static void SetInfoAboutChanges(
            this CardInfoStorageObject storage,
            InfoAboutChanges infoAboutChanges)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            storage.Info.SetInfoAboutChanges(infoAboutChanges);
        }

        /// <summary>
        /// Возвращает значение, показывающее, что после пересчёта были изменения в маршруте или нет. Информация добавляется только при выставленном флаге <see cref="InfoAboutChanges.HasChangesToInfo"/>.
        /// </summary>
        /// <param name="obj">Хранилище из которого должно быть получено значение.</param>
        /// <returns>Значение <see langword="true"/>, если после пересчёта были изменения в маршруте, иначе <see langword="false"/>.</returns>
        public static bool? GetHasRecalcChanges(
            this CardInfoStorageObject obj)
        {
            Check.ArgumentNotNull(obj, nameof(obj));

            return obj.Info.TryGet<bool?>(HasChangesInRoute);
        }

        /// <summary>
        /// Задаёт значение, показывающее, что после пересчёта были изменения в маршруте или нет. Информация добавляется только при выставленном флаге <see cref="InfoAboutChanges.HasChangesToInfo"/>.
        /// </summary>
        /// <param name="storage">Хранилище в котором должно быть сохранено значение.</param>
        /// <param name="hasChanges">Значение <see langword="true"/>, если после пересчёта были изменения в маршруте, иначе <see langword="false"/>.</param>
        public static void SetHasRecalcChanges(
            this CardInfoStorageObject storage,
            bool hasChanges)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            storage.Info[HasChangesInRoute] = hasChanges;
        }

        /// <summary>
        /// Возвращает информацию о различиях в маршруте до и после пересчёта.
        /// </summary>
        /// <param name="obj">Хранилище из которого должно быть получено значение.</param>
        /// <returns>Список содержащий различия до и после пересчёта маршрута.</returns>
        public static List<RouteDiff> GetRecalcChanges(
            this CardInfoStorageObject obj)
        {
            Check.ArgumentNotNull(obj, nameof(obj));

            var info = obj.Info;
            if (info.TryGetValue(RouteChanges, out var diffsObj)
                && diffsObj is IEnumerable diffsEnum)
            {
                var diffs = new List<RouteDiff>();
                foreach (var diffObj in diffsEnum)
                {
                    if (diffObj is Dictionary<string, object> diffStorage)
                    {
                        diffs.Add(new RouteDiff(diffStorage));
                    }
                }

                return diffs;
            }

            return null;
        }

        /// <summary>
        /// Сохраняет в заданном хранилище информацию о различиях в маршруте до и после пересчёта.
        /// </summary>
        /// <param name="obj">Хранилище в котором должно быть сохранено значение.</param>
        /// <param name="diffs">Перечисление содержащее различия до и после пересчёта маршрута.</param>
        public static void SetRecalcChanges(
            this CardInfoStorageObject obj,
            IEnumerable<RouteDiff> diffs)
        {
            Check.ArgumentNotNull(obj, nameof(obj));
            Check.ArgumentNotNull(diffs, nameof(diffs));

            obj.Info[RouteChanges] = diffs.Select(p => p.GetStorage()).ToList();
        }

        /// <summary>
        /// Возвращает значение, показывающее, наличие скрытых пропущенных этапов.
        /// </summary>
        /// <param name="card">Карточка в которой выполняется проверка.</param>
        /// <returns>Значение true, если скрытые этапы содержатся, иначе - false.</returns>
        public static bool HasSkipStages(
            this Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            card.Sections.TryGetValue(KrConstants.KrStages.Virtual, out var krStagesVirtual);
            var krStagesVirtualRows = krStagesVirtual?.Rows;

            return card.Info.TryGetValue(StagePositionsInfoMark, out var stagePositions)
                && stagePositions is IList stagePositionsList
                && stagePositionsList.Cast<object>().Any(p =>
                {
                    if (p is not Dictionary<string, object> pTyped)
                    {
                        return false;
                    }

                    return pTyped.TryGet<bool?>(nameof(KrStagePositionInfo.Skip)) == true
                        && krStagesVirtualRows?.Any(i => i.RowID == pTyped.TryGet<Guid?>(nameof(KrStagePositionInfo.RowID))) == false;
                });
        }

        /// <summary>
        /// Ключ элемента в <see cref="Card"/>.<see cref="CardInfoStorageObject.Info"/> содержащий значение флага, показывающего, требуется ли показать пропущенные этапы. Тип значения: <see cref="bool"/>.
        /// </summary>
        private const string DontHideSkippedStagesInfoMark = nameof(DontHideSkippedStagesInfoMark);

        /// <summary>
        /// Устанавливает значение в заданном хранилище, показывающее, требуется ли отображать пропущенные этапы.
        /// </summary>
        /// <param name="storage">Хранилище в котором должно быть сохранено значение.</param>
        /// <param name="value">Значение <see langword="true"/>, если необходимо отображать пропущенные этапа, иначе - <see langword="false"/>.</param>
        public static void DontHideSkippedStages(
            this Dictionary<string, object> storage,
            bool value = true)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            storage[DontHideSkippedStagesInfoMark] = BooleanBoxes.Box(value);
        }

        /// <summary>
        /// Возвращает значение из заданного хранилища, показывающее, требуется ли отображать пропущенные этапы.
        /// </summary>
        /// <param name="storage">Хранилище из которого должно быть получено значение.</param>
        /// <returns>Значение <see langword="true"/>, если необходимо отображать пропущенные этапа, иначе - <see langword="false"/>.</returns>
        public static bool ConsiderSkippedStages(
            this CardInfoStorageObject storage)
        {
            Check.ArgumentNotNull(storage, nameof(storage));

            return storage.Info.TryGet(DontHideSkippedStagesInfoMark, false);
        }

        #endregion

        #region ICardMetadata

        /// <summary>
        /// Возвращает название состояния в типовом решении по его идентификатору.
        /// Если состояние не является стандартным, то значение запрашивается из метаданных секции <see cref="KrDocState"/>.
        /// </summary>
        /// <param name="metadata">Метаинформация, необходимая для использования типов карточек совместно с пакетом карточек.</param>
        /// <param name="state">Состояние согласования документа.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Название состояния согласования документа.</returns>
        public static async ValueTask<string> GetDocumentStateNameAsync(
            this ICardMetadata metadata,
            KrState state,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(metadata, nameof(metadata));

            if (state.IsDefault())
            {
                return state.TryGetDefaultName();
            }

            var stateID = state.ID;
            var record = (await metadata
                .GetEnumerationsAsync(cancellationToken))[DefaultSchemeHelper.KrDocStateSectionID]
                .Records
                .FirstOrDefault(p => (int) p[KrDocState.ID] == stateID);

            if (record?[KrDocState.Name] is not string name)
            {
                throw new InvalidOperationException(
                    $"State with ID = {state.ID} doesn't exist in KrDocState enumeration table.");
            }

            return name;
        }

        /// <summary>
        /// Возвращает название состояния этапа. Если состояние не является стандартным, то значение запрашивается из метаданных секции <see cref="KrConstants.KrStageState"/>.
        /// </summary>
        /// <param name="metadata">Метаинформация, необходимая для использования типов карточек совместно с пакетом карточек.</param>
        /// <param name="state">Состояние этапа.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Название состояния этапа.</returns>
        public static async ValueTask<string> GetStageStateNameAsync(
            this ICardMetadata metadata,
            KrStageState state,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(metadata, nameof(metadata));

            if (state.IsDefault())
            {
                return state.TryGetDefaultName();
            }

            var stateID = state.ID;
            var record = (await metadata
                .GetEnumerationsAsync(cancellationToken))[DefaultSchemeHelper.KrStageStateSectionID]
                .Records
                .FirstOrDefault(p => (int) p[KrConstants.KrStageState.ID] == stateID);

            if (record?[KrConstants.KrStageState.Name] is not string name)
            {
                throw new InvalidOperationException(
                    $"State with ID = {state.ID} doesn't exist in KrStageState enumeration table.");
            }

            return name;
        }

        #endregion
    }
}
