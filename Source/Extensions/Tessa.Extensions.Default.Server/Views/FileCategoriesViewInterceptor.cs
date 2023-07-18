using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Extensions;
using Tessa.Platform;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Criteria;

namespace Tessa.Extensions.Default.Server.Views
{
    /// <summary>
    /// Перехватчик представлений файловых категорий, добавляющий вариант "Без категории" на первую страницу первым пунктом.
    /// По умолчанию перехватывает стандартные представления отображения категорий файлов.
    /// Класс можно наследовать и применять для своих представлений, отображающих категорию файлов.
    /// </summary>
    public class FileCategoriesViewInterceptor : ViewInterceptorBase
    {
        #region Fields

        /// <summary>
        /// Имя параметра, наличие которого включает отображение варианта "Без категории" в результате представления на первой странице.
        /// </summary>
        public const string IncludeWithoutCategoryParamName = "IncludeWithoutCategory";

        /// <summary>
        /// Отображаемое значение варианта "Без категории".
        /// </summary>
        public const string WithoutCategoryValue = "$UI_Cards_FileNoCategory";

        #endregion

        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса перехватчика предсталвений для представлений по умолчанию.
        /// </summary>
        public FileCategoriesViewInterceptor()
            : base(
                  new[]
                    {
                        "FileCategoriesAll",
                        "FileCategoriesFiltered",
                    })
        {
        }

        /// <summary>
        /// Создаёт экземпляр класса перехватчика предсталвений для представлений по указанному списку.
        /// </summary>
        protected FileCategoriesViewInterceptor(string[] viewAliases)
            : base(viewAliases)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task<ITessaViewResult> GetDataAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
        {
            if (!this.InterceptedViews.TryGetValue(request.ViewAlias, out var view))
            {
                throw new InvalidOperationException($"Can't find view with alias:'{request.ViewAlias}'");
            }

            // Не нашли искомый параметр, выполняем представление без перехвата
            if (request.TryGetParameter(IncludeWithoutCategoryParamName) is not { } parameter)
            {
                return await view.GetDataAsync(request, cancellationToken);
            }

            var viewMetadata = await view.GetMetadataAsync(cancellationToken);
            if (!await this.FilterWithoutCategoryAsync(request, viewMetadata, cancellationToken))
            {
                return await view.GetDataAsync(request, cancellationToken);
            }

            bool showWithoutCategory = true;
            if (request.TryGetParameter(ViewSpecialParametersConst.PageOffset) is { } offsetParameter
                && request.TryGetParameter(ViewSpecialParametersConst.PageLimit) is { } limitParameter
                && offsetParameter.CriteriaValues[0].Values[0].Value is int offset
                && limitParameter.CriteriaValues[0].Values[0].Value is int limit)
            {
                if (offset == 1)
                {
                    limitParameter.CriteriaValues[0].Values[0].Value = limit - 1;
                }
                else
                {
                    showWithoutCategory = false;
                    offsetParameter.CriteriaValues[0].Values[0].Value = offset - 1;
                }
            }

            var result = await view.GetDataAsync(request, cancellationToken);

            if (showWithoutCategory
                && this.TryCreateRowData(viewMetadata) is { } rowData)
            {
                result.RowCount += 1;
                result.Rows = new object[] { rowData }
                    .Union(result.Rows).ToArray();
            }

            return result;
        }


        #endregion

        #region Virtual properties and Methods

        /// <summary>
        /// Имя параметра для поиска файловой категории по имени.
        /// </summary>
        protected virtual string NameParameterAlias => "Name";

        /// <summary>
        /// Рассчитывает по параметрам фильтрации признак, который показывает, можно ли отображать значение "Без категории" или нет в результате представления.
        /// По умолчанию выполняет фильтрацию по названию с именем параметр <see cref="NameParameterAlias"/>
        /// </summary>
        /// <param name="request">Запрос к представлению.</param>
        /// <param name="viewMetadata">Метаданные представления.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значени <c>true</c>, если по результатам фильтрации значение "Без категории" можно отображать, иначе <c>false</c>.</returns>
        protected virtual ValueTask<bool> FilterWithoutCategoryAsync(ITessaViewRequest request, IViewMetadata viewMetadata, CancellationToken cancellationToken)
        {
            if (request.TryGetParameter(this.NameParameterAlias) is { } nameParameter)
            {
                var withoutCategoryName = Localize(WithoutCategoryValue);
                foreach (var criteria in nameParameter.CriteriaValues)
                {
                    Func<string, string, bool> valueCheck = null;
                    switch (criteria.CriteriaName)
                    {
                        case CriteriaOperatorConst.Contains:
                            valueCheck = (s1, s2) => s1.Contains(s2, StringComparison.OrdinalIgnoreCase);
                            break;

                        case CriteriaOperatorConst.Equality:
                            valueCheck = (s1, s2) => s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
                            break;

                        case CriteriaOperatorConst.NonEquality:
                            valueCheck = (s1, s2) => !s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
                            break;

                        case CriteriaOperatorConst.StartWith:
                            valueCheck = (s1, s2) => s1.StartsWith(s2, StringComparison.OrdinalIgnoreCase);
                            break;

                        case CriteriaOperatorConst.EndWith:
                            valueCheck = (s1, s2) => s1.EndsWith(s2, StringComparison.OrdinalIgnoreCase);
                            break;

                        case CriteriaOperatorConst.IsNotNull:
                            valueCheck = (s1, s2) => true;
                            break;
                    }

                    if (valueCheck is null)
                    {
                        return ValueTask.FromResult(false);
                    }

                    foreach (var value in criteria.Values)
                    {
                        if (!valueCheck.Invoke(withoutCategoryName, value.Value as string))
                        {
                            return ValueTask.FromResult(false);
                        }
                    }
                }
            }

            return ValueTask.FromResult(true);
        }

        /// <summary>
        /// Создаёт строку с данными по переданным метаданным представления или <c>null</c>, если строку не удалось создать.
        /// Реализация по умолчанию выбирает первый объект <see cref="IViewReferenceMetadata"/> из метаданных представления,
        /// для колонок выбранной ссыкли записывает <see cref="Guid.Empty"/> в качестве идентификатора и <see cref="WithoutCategoryValue"/> в качествве отображаемого имени,
        /// а для прочих записывает значение по умолчанию в соответствии с типом колонки.
        /// </summary>
        /// <param name="viewMetadata">Метаданные представления.</param>
        /// <returns>Строка с данными или <c>null</c>, если строку не удалось создать.</returns>
        protected virtual object[] TryCreateRowData(IViewMetadata viewMetadata)
        {
            var reference = viewMetadata.References.FirstOrDefault();
            if (reference is null)
            {
                return null;
            }

            object[] rowData = new object[viewMetadata.Columns.Count];
            string referenceIDColumn = reference.RefSection + "ID";
            for (int i = 0; i < viewMetadata.Columns.Count; i++)
            {
                var column = viewMetadata.Columns[i];
                if (column.Alias.Equals(referenceIDColumn, StringComparison.Ordinal))
                {
                    rowData[i] = GuidBoxes.Empty;
                }
                else if (column.Alias == reference.DisplayValueColumn)
                {
                    rowData[i] = WithoutCategoryValue;
                }
                else
                {
                    rowData[i] = column.SchemeType.ClrType.GetDefaultValue();
                }
            }

            return rowData;
        }

        #endregion
    }
}
