#nullable enable

using System;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Объект, предоставляющий <see cref="FilterViewDialogDescriptor"/>.
    /// </summary>
    public interface IFilterViewDialogDescriptorRegistry
    {
        /// <summary>
        /// Регистрирует <paramref name="descriptor"/> для указанного <paramref name="compositionID"/>. Метод замещает предыдущую регистрацию при её наличии.
        /// </summary>
        /// <param name="compositionID">Уникальный идентификатор элемента рабочего места, для которого должно использоваться переопределение диалога с параметрами представления.<para/>
        /// 
        /// Значение расположено в TessaAdmin на вкладке "Рабочие места" в окне "Свойства" в поле "Id". К данному элементу дерева должно быть применено расширение <see cref="FilterViewDialogOverrideWorkplaceComponentExtension"/>.</param>
        /// <param name="descriptor"><inheritdoc cref="FilterViewDialogDescriptor" path="/summary"/></param>
        void Register(
            Guid compositionID,
            FilterViewDialogDescriptor descriptor);

        /// <summary>
        /// Возвращает <see cref="FilterViewDialogDescriptor"/> для заданного <paramref name="compositionID"/>.
        /// </summary>
        /// <param name="compositionID">Уникальный идентификатор элемента рабочего места, для которого должно использоваться переопределение диалога с параметрами представления.</param>
        /// <returns>Объект <see cref="FilterViewDialogDescriptor"/> или значение <see langword="null"/>, если не найден объект соответствующий <paramref name="compositionID"/>.</returns>
        FilterViewDialogDescriptor? TryGet(
            Guid compositionID);
    }
}
