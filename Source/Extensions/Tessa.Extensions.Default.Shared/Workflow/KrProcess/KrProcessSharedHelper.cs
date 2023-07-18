using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions.Templates;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    public static class KrProcessSharedHelper
    {
        /// <summary>
        /// Возвращает значение, показывающее, может ли указанный тип карточки содержать шаблоны этапов.
        /// </summary>
        /// <param name="typeID">Идентификатор типа карточки.</param>
        /// <returns>Значение <see langword="true"/>, если указанный тип карточки может содержать шаблоны этапов, иначе - <see langword="false"/>.</returns>
        public static bool DesignTimeCard(Guid typeID) =>
            typeID == DefaultCardTypes.KrStageTemplateTypeID
            || typeID == DefaultCardTypes.KrSecondaryProcessTypeID;

        /// <summary>
        /// Возвращает значение, показывающее, является ли указанный тип карточки типом карточки в котором выполняется маршрут.
        /// </summary>
        /// <param name="typeID">Идентификатор типа карточки.</param>
        /// <returns>Значение <see langword="true"/>, если указанный тип карточки может содержать выполняющийся маршрут, иначе - <see langword="false"/>.</returns>
        public static bool RuntimeCard(Guid typeID) => !DesignTimeCard(typeID);

        /// <summary>
        /// Асинхронно возвращает идентификатор типа документа для карточки с указанным идентификатором.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки для которой требуется получить идентификатор типа документа.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Идентификатор типа документа или значение <see langword="null"/>, если не удалось получить идентификатор типа документа для указанной карточки.</returns>
        public static async Task<Guid?> GetDocTypeIDAsync(
            Guid cardID,
            IDbScope dbScope,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            await using (dbScope.Create())
            {
                var query = dbScope.BuilderFactory
                    .Select()
                    .C(DocumentCommonInfo.DocTypeID)
                    .From(DocumentCommonInfo.Name).NoLock()
                    .Where().C(DocumentCommonInfo.ID).Equals().P("CardID")
                    .Build();
                return await dbScope.Db.SetCommand(
                        query,
                        dbScope.Db.Parameter("CardID", cardID))
                    .LogCommand()
                    .ExecuteAsync<Guid?>(cancellationToken);
            }
        }

        /// <summary>
        /// Возвращает идентификатор типа документа для заданной карточки.
        /// </summary>
        /// <param name="card">Карточка, для которой требуется получить идентификатор типа документа.</param>
        /// <returns>Идентификатор типа документа или значение <see langword="null"/>, если его не удалось получить.</returns>
        /// <remarks>Метод не выполняет запросов к базе данных.</remarks>
        public static Guid? GetDocTypeID(
            Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            var cardInfo = card.TryGetInfo();

            if (cardInfo is not null)
            {
                var krToken = KrToken.TryGet(cardInfo);

                if (krToken is not null
                    && krToken.TryGetDocTypeID(out var docTypeID))
                {
                    return docTypeID;
                }
            }

            // Тип лежит в секции DocumentCommonInfo.DocTypeID.
            if (card.Sections.TryGetValue(DocumentCommonInfo.Name, out var sec)
                && sec.Fields.TryGetValue(DocumentCommonInfo.DocTypeID, out var docTypeIDObj))
            {
                return docTypeIDObj as Guid?;
            }

            // Тип закэширован в Card.Info.
            if (card.Info.TryGetValue(Keys.DocTypeID, out docTypeIDObj))
            {
                return docTypeIDObj as Guid?;
            }

            // В карточке ничего нет.
            return null;
        }

        /// <summary>
        /// Асинхронно возвращает идентификатор типа документа для карточки с указанным идентификатором.
        /// Метод кэширует тип документа в Card.Info, если он не был найден в объекте карточки.
        /// </summary>
        /// <param name="card">Карточка, для которой требуется получить идентификатор типа документа.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Идентификатор типа документа или значение <see langword="null"/>, если его не удалось получить для указанной карточки.</returns>
        public static async ValueTask<Guid?> GetDocTypeIDAsync(
            Card card,
            IDbScope dbScope,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(card, nameof(card));
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            var docTypeID = GetDocTypeID(card);

            if (docTypeID.HasValue)
            {
                return docTypeID;
            }

            // В карточке ничего нет, придется лезть в базу.
            docTypeID = await GetDocTypeIDAsync(card.ID, dbScope, cancellationToken);
            card.Info[Keys.DocTypeID] = docTypeID;

            return docTypeID;
        }

        /// <summary>
        /// Асинхронно возвращает из базы данных состояние согласования для карточки с указанным идентификатором.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="cardID">Идентификатор основной карточки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Состояние карточки указанное в основном сателлите процесса или значение <see langword="null"/>, если основного сателлита для карточки с идентификатором <paramref name="cardID"/> нет.</returns>
        public static async Task<KrState?> GetKrStateAsync(
            Guid cardID,
            IDbScope dbScope,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            await using (dbScope.Create())
            {
                //Получаем состояние карточки
                var db = dbScope.Db;
                var builderFactory = dbScope.BuilderFactory;
                var cardState = await db
                    .SetCommand(
                        builderFactory
                            .Select().Coalesce(b => b.C(KrApprovalCommonInfo.StateID).V(KrState.Draft.ID))
                            .From(KrApprovalCommonInfo.Name).NoLock()
                            .Where().C(KrProcessCommonInfo.MainCardID).Equals().P("CardID")
                            .Build(),
                        db.Parameter("CardID", cardID))
                    .LogCommand()
                    .ExecuteAsync<int?>(cancellationToken).ConfigureAwait(false);
                return cardState.HasValue
                    ? new KrState(cardState.Value)
                    : null;
            }
        }

        /// <summary>
        /// Асинхронно возвращает состояние карточки из возможных источников:<para/>
        /// Секция KrApprovalCommonInfoVirtual;<para/>
        /// Сателлит из Info карточки;<para/>
        /// БД (опционально).
        /// </summary>
        /// <param name="card">Карточка для которой требуется получить состояние.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Состояние карточки.</returns>
        public static async ValueTask<KrState?> GetKrStateAsync(
            Card card,
            IDbScope dbScope = null,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(card, nameof(card));

            KrState? result = null;

            if (card.Sections.TryGetValue(KrApprovalCommonInfo.Virtual, out var section))
            {
                result = (KrState?) section.RawFields.TryGet<int?>(KrApprovalCommonInfo.StateID);
            }

            if (!result.HasValue)
            {
                // возможно удалённая карточка
                var satelliteCard = CardSatelliteHelper.TryGetSingleSatelliteCardFromList(card, CardSatelliteHelper.SatellitesKey, DefaultCardTypes.KrSatelliteTypeID);
                if (satelliteCard != null)
                {
                    result = satelliteCard.Sections[KrApprovalCommonInfo.Name].RawFields.Get<KrState?>(KrApprovalCommonInfo.StateID);
                }
            }

            if (!result.HasValue
                && dbScope != null)
            {
                result = await GetKrStateAsync(card.ID, dbScope, cancellationToken).ConfigureAwait(false);
            }

            return result;
        }

        /// <summary>
        /// Возвращает эффективные настройки для типа карточки или типа документа <see cref="IKrType"/>
        /// по карточке <paramref name="card"/>, которая загружена со всеми секциями, или <c>null</c>, если настройки нельзя получить.
        /// </summary>
        /// <param name="krTypesCache">Кэш типов карточек.</param>
        /// <param name="card">Карточка, загруженная со всеми секциями.</param>
        /// <param name="cardTypeID">Идентификатор типа карточки.</param>
        /// <param name="validationResult">
        /// Объект, в который записываются сообщения об ошибках, или <c>null</c>, если сообщения никуда не записываются.
        /// </param>
        /// <param name="validationObject">
        /// Объект, информация о котором записывается в сообщениях об ошибках в <paramref name="validationResult"/>,
        /// или <c>null</c>, если информация об объекте не будет указана.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Эффективные настройки для типа карточки или типа документа
        /// или <c>null</c>, если настройки нельзя получить.
        /// </returns>
        public static async ValueTask<IKrType> TryGetKrTypeAsync(
            IKrTypesCache krTypesCache,
            Card card,
            Guid cardTypeID,
            IValidationResultBuilder validationResult = null,
            object validationObject = null,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(krTypesCache, nameof(krTypesCache));
            Check.ArgumentNotNull(card, nameof(card));

            var krCardType = (await krTypesCache.GetCardTypesAsync(cancellationToken).ConfigureAwait(false))
                .FirstOrDefault(x => x.ID == cardTypeID);

            if (krCardType == null)
            {
                // карточка может не входить в типовое решение, тогда возвращается null
                // при этом нельзя кидать ошибку в ValidationResult, иначе любое действие с такой карточкой будет неудачным
                return null;
            }

            IKrType result = krCardType;
            if (krCardType.UseDocTypes)
            {
                if (card.Sections.TryGetValue(DocumentCommonInfo.Name, out var section))
                {
                    if (section.RawFields.TryGetValue(DocumentCommonInfo.DocTypeID, out var value))
                    {
                        if (value is Guid docTypeID)
                        {
                            result = (await krTypesCache.GetDocTypesAsync(cancellationToken).ConfigureAwait(false))
                                .FirstOrDefault(x => x.ID == docTypeID);

                            if (result == null)
                            {
                                validationResult?.AddError(validationObject, "$KrMessages_UnableToFindTypeWithID", docTypeID);

                                return null;
                            }
                        }
                        else
                        {
                            validationResult?.AddError(validationObject, "$KrMessages_DocTypeNotSpecified");

                            return null;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Восстанавливает порядок сортировки для списка строк.
        /// </summary>
        /// <typeparam name="T">Тип значения поля, содержащего порядок сортировки.</typeparam>
        /// <param name="rows">Список сортируемых строк. Тип значений: <c>IDictionary&lt;string, object&gt;</c>.</param>
        /// <param name="orderField">Имя поля, содержащего порядок сортировки.</param>
        /// <remarks>
        /// Метод выполняет следующие действия:
        /// 
        /// <list type="number">
        ///     <item>Упорядочивает строки в соответствии с порядком сортировки. Используется стабильная сортировка.</item>
        ///     <item>Восстанавливает порядок сортировки, т.о. что бы он располагался в интервале [0; <see cref="ICollection.Count"/>) и не имел разрывов.</item>
        /// </list>
        /// </remarks>
        public static void RepairStorageRowsOrders<T>(
            IList rows,
            string orderField)
            where T : IComparable<T>
        {
            ThrowIfNull(rows);
            ThrowIfNullOrEmpty(orderField);

            if (rows.Count == 0)
            {
                return;
            }

            if (rows.Count == 1)
            {
                var row = (IDictionary<string, object>) rows[0];

                if (row.Get<int>(orderField) != 0)
                {
                    row[orderField] = Int32Boxes.Zero;
                }
                return;
            }

            // Выполняем сортировку вставками, т.к. в большинстве случаев считаем последовательность упорядоченной.
            // В случае, когда последовательность упорядочена, сортировка выполняется за O(n).
            // Критически важна стабильность сортировки.

            // Кэширование порядковых номеров не даёт особого выигрыша в производительности.
            // Разница заметна только на тысячах элементов.

            // По скорости выполнения лучше использовать Enumerable.OrderBy, но там будут аллокации.

            for (var i = 1; i < rows.Count; i++)
            {
                var row = rows[i];
                var j = i - 1;

                while (j >= 0
                    && ((IDictionary<string, object>) rows[j]).Get<int>(orderField)
                        .CompareTo(((IDictionary<string, object>) row).Get<int>(orderField)) > 0)
                {
                    rows[j + 1] = rows[j];
                    j--;
                }
                rows[j + 1] = row;
            }

            // Восстановление последовательности порядка строк.
            for (var i = 0; i < rows.Count; i++)
            {
                var row = (IDictionary<string, object>) rows[i];
                if (row.Get<int>(orderField) != i)
                {
                    row[orderField] = i;
                }
            }
        }

        /// <summary>
        /// Определяет порядок добавленного вручную этапа при вставке в маршрут.
        /// </summary>
        /// <param name="groupID">Идентификатор группы этапов.</param>
        /// <param name="groupOrder">Порядок группы этапов.</param>
        /// <param name="rows">Список строк, в который выполняется вставка нового этапа.</param>
        /// <returns>Порядок добавленного вручную этапа при вставке в маршрут.</returns>
        public static int ComputeStageOrder(
            Guid groupID,
            int groupOrder,
            IReadOnlyList<CardRow> rows)
        {
            Check.ArgumentNotNull(rows, nameof(rows));

            if (rows.Count == 0)
            {
                return 0;
            }

            var rowIndex = 0;

            Guid GetID() => rows[rowIndex].TryGet(KrStages.StageGroupID, Guid.Empty);
            int GetOrder() => rows[rowIndex].TryGet(KrStages.StageGroupOrder, int.MaxValue);
            bool NestedStage() => rows[rowIndex].TryGet<bool>(Keys.NestedStage);

            var cnt = rows.Count;

            // Достигаем начало требуемой группы
            while (rowIndex < cnt
                && (GetID() != groupID
                    && GetOrder() < groupOrder
                    || NestedStage()))
            {
                rowIndex++;
            }

            // На нестеде тут мы быть не можем, т.к. пропустили возможные нестеды выше
            // Проверим, что мы в конце или на другой группе
            if (rows.Count == rowIndex
                || (GetID() != groupID
                    && GetOrder() != groupOrder))
            {
                // Текущая группа последняя
                // В текущей группе нет этапов, просто добавляем на нужное место.
                return rowIndex;
            }

            var firstIndexInGroup = rowIndex;
            rowIndex++;

            // Спускаемся до конца группы
            while (rowIndex < cnt
                && (GetID() == groupID
                    && GetOrder() == groupOrder
                    || NestedStage()))
            {
                rowIndex++;
            }

            // Поднимаемся вверх до возможного места добавления
            var position = rowIndex;
            var sortedRows = rows.OrderBy(p => p.Get<int>(KrStages.Order)).ToArray();
            for (int i = rowIndex - 1; i >= firstIndexInGroup; i--)
            {
                var row = sortedRows[i];
                if (row.TryGet<bool>(Keys.NestedStage))
                {
                    continue;
                }

                if (row.Fields.TryGetValue(KrStages.BasedOnStageTemplateGroupPositionID, out var gpObj)
                    && GroupPosition.GetByID(gpObj) == GroupPosition.AtLast
                    && row.Fields.TryGetValue(KrStages.OrderChanged, out var orderChangedObj)
                    && orderChangedObj is bool orderChanged
                    && !orderChanged)
                {
                    position = i;
                }
            }

            return position;
        }

        /// <summary>
        /// Возвращает значение, показывающее, разрешено ли скрытие этапа в дескрипторе типа этапа или нет.
        /// </summary>
        /// <param name="row">Строка этапа.</param>
        /// <param name="descriptors">Коллекция дескрипторов типов этапов, в которой выполняется поиск информации.</param>
        /// <returns>Значение <see langword="true"/>, если скрытие этапа разрешено в дескрипторе типа этапа, иначе - <see langword="false"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanBeHidden(
            CardRow row,
            ICollection<StageTypeDescriptor> descriptors)
        {
            ThrowIfNull(row);
            ThrowIfNull(descriptors);

            var stageTypeID = row.TryGet<Guid?>(KrConstants.KrStages.StageTypeID);

            if (stageTypeID.HasValue)
            {
                foreach (var descriptor in descriptors)
                {
                    if (descriptor.ID == stageTypeID.Value)
                    {
                        return descriptor.CanBeHidden;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Возвращает значение, показывающее, возможен ли пропуск указанного этапа.
        /// </summary>
        /// <param name="row">Строка этапа, для которого выполняется проверка.</param>
        /// <returns>Значение, показывающее, возможен ли пропуск указанного этапа.</returns>
        public static bool CanBeSkipped(CardRow row)
        {
            ThrowIfNull(row);

            return row.Fields.TryGetValue(KrConstants.KrStages.BasedOnStageTemplateID, out var basedOnStageTemplateIDObj)
                && basedOnStageTemplateIDObj is not null
                && row.Fields.TryGet<bool>(KrConstants.KrStages.CanBeSkipped);
        }

        /// <summary>
        /// Выполняет пропуск этапа.
        /// </summary>
        /// <param name="stageRow">Строка этапа, пропуск которого выполняется.</param>
        /// <returns>Значение <see langword="true"/>, если этап был пропущен, иначе - <see langword="false"/>.</returns>
        public static bool SkipStage(CardRow stageRow)
        {
            // Параметр stageRow будет проверен в CanBeSkipped.
            if (CanBeSkipped(stageRow))
            {
                if (stageRow.State == CardRowState.Deleted)
                {
                    stageRow.State = CardRowState.Modified;
                }

                stageRow.Fields[KrConstants.KrStages.Skip] = BooleanBoxes.True;

                return true;
            }

            return false;
        }
    }
}
