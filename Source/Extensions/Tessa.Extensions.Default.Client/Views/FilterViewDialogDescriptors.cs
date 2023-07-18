using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Criteria;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Предоставляет объекты типа <see cref="FilterViewDialogDescriptor"/>.
    /// </summary>
    public static class FilterViewDialogDescriptors
    {
        #region Constants And Static Fields

        /// <summary>
        /// Дескриптор, описывающий специальный диалог с параметрами фильтрации представления РМ Администратор/Тестирование/Автомобили.
        /// </summary>
        public static readonly FilterViewDialogDescriptor Cars =
            new FilterViewDialogDescriptor(
                "CarViewParameters",
                new ReadOnlyCollection<ParameterMapping>(
                    new List<ParameterMapping>()
                    {
                        new ParameterMapping(
                            "CarName",
                            "Parameters",
                            "Name"),
                        new ParameterMapping(
                            "CarMaxSpeed",
                            "Parameters",
                            "MaxSpeed")
                        {
                            CriteriaOperator = CriteriaHelper.GetCriteria(CriteriaOperatorConst.Equality),
                        },
                        new ParameterMapping(
                            "Driver",
                            "Parameters",
                            "DriverID")
                        {
                            DisplayValueSectionName = "Parameters",
                            DisplayValueFieldName = "DriverName",
                        },
                        new ParameterMapping(
                            "CarReleaseDateFrom",
                            "Parameters",
                            "ReleaseDateFrom"),
                        new ParameterMapping(
                            "CarReleaseDateTo",
                            "Parameters",
                            "ReleaseDateTo"),
                    }));

        #endregion
    }
}
