using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Test.Default.Shared.Kr.Routes
{
    /// <summary>
    /// Предоставляет вспомогательные методы для тестирования этапов подсистемы маршрутов.
    /// </summary>
    public static class StageTypesTestHelper
    {
        /// <summary>
        /// Возвращает перечисление дескрипторов этапов, объявленных статическими полями в заданном классе.
        /// </summary>
        /// <param name="descriptorsType">Тип содержащий дескрипторы. Если значение не задано, то поиск выполняется в классе <see cref="StageTypeDescriptors"/>.</param>
        /// <param name="predicate">Условие фильтрации.</param>
        /// <returns>Список дескрипторов, объявленных статическими полями в классе <paramref name="descriptorsType"/>.</returns>
        public static IEnumerable<StageTypeDescriptor> StaticDescriptors(
            Type descriptorsType = default,
            Func<StageTypeDescriptor, bool> predicate = default)
        {
            descriptorsType ??= typeof(StageTypeDescriptors);

            foreach (var field in GetAllFields(descriptorsType))
            {
                if (field.FieldType == typeof(StageTypeDescriptor))
                {
                    var descriptor = (StageTypeDescriptor)field.GetValue(null);

                    if (predicate?.Invoke(descriptor) != false)
                    {
                        yield return descriptor;
                    }
                }
            }
        }

        private static List<FieldInfo> GetAllFields(Type type)
        {
            var fieldInfoList = new List<FieldInfo>();
            for (Type type1 = type; (object) type1 != (object) typeof (object); type1 = type1.GetTypeInfo().BaseType)
            {
                if (type1 is null)
                {
                    break;
                }

                FieldInfo[] fields = TypeExtensions.GetFields(type1, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                fieldInfoList.AddRange(fields);
            }

            return fieldInfoList;
        }

        /// <summary>
        /// Возвращает перечисление объектов <see cref="TestCaseData"/> содержащих дескрипторы этапов, объявленных статическими полями в классе <see cref="StageTypeDescriptors"/>.
        /// </summary>
        /// <param name="predicate">Условие фильтрации.</param>
        /// <returns>Перечисление объектов <see cref="TestCaseData"/> содержащих дескрипторы этапов.</returns>
        public static IEnumerable<TestCaseData> StaticDescriptorsTestCases(
            Func<StageTypeDescriptor, bool> predicate = null) =>
            StaticDescriptors(predicate: predicate).Select(p => new TestCaseData(p));
    }
}