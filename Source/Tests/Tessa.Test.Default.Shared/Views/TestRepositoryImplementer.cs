using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Parser;

namespace Tessa.Test.Default.Shared.Views
{
    /// <summary>
    /// Тестовая имплементация репозитория представлений.
    /// </summary>
    public class TestRepositoryImplementer :
        IRepository<IGetModelRequest, IStoreTessaViewRequest, IEnumerable<TessaViewModel>>
    {
        #region Fields

        private readonly IMediatorServer mediator;

        private readonly List<TessaViewModel> models = new List<TessaViewModel>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TestRepositoryImplementer"/>.
        /// </summary>
        /// <param name="mediator">
        /// Посредник для отправки уведомления об изменении репозитория
        /// </param>
        /// <param name="fakeModelGenerator">
        /// Генератор фальшивых моделей представлений, предназначенных для тестирования.
        /// </param>
        public TestRepositoryImplementer(
            IMediatorServer mediator,
            Func<IEnumerable<TessaViewModel>> fakeModelGenerator = null)
        {
            this.mediator = mediator;
            if (fakeModelGenerator != null)
            {
                this.models.AddRange(fakeModelGenerator());
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Осуществляет изменение элементов хранилища.
        /// </summary>
        /// <param name="request">
        /// Параметры запроса.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task ChangeAsync(IStoreTessaViewRequest request, CancellationToken cancellationToken = default)
        {
            await this.DeleteAsync(request, cancellationToken);
            await this.NewAsync(request, cancellationToken);
        }

        /// <summary>
        /// Удаляет элементы из хранилища.
        /// </summary>
        /// <param name="request">
        /// Параметры запроса.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public Task DeleteAsync(IStoreTessaViewRequest request, CancellationToken cancellationToken = default)
        {
            foreach (var model in request.Models)
            {
                this.models.RemoveAll(x => x != null && x.Id == model.Id);
            }

            this.mediator.Notify();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Возвращает элементы из хранилища.
        /// </summary>
        /// <param name="request">
        /// Параметры запроса.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Результат выполнения запроса.
        /// </returns>
        public Task<IEnumerable<TessaViewModel>> GetAsync(
            IGetModelRequest request,
            CancellationToken cancellationToken = default)
        {
            IEnumerable<TessaViewModel> result =
                request.RequestedModels == null
                    ? this.models
                    : request.RequestedModels.Select(modelId => this.models.FirstOrDefault(x => x.Id == modelId))
                        .Where(model => model != null)
                        .ToList();
            return Task.FromResult(result);
        }

        /// <summary>
        /// Осуществляет пакетное добавление списка элементов.
        /// </summary>
        /// <param name="request">
        /// Параметры запроса.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public Task ImportAsync(IStoreTessaViewRequest request, CancellationToken cancellationToken = default) =>
            throw new NotSupportedException();

        /// <summary>
        /// Осуществляет создание новых элементов в хранилище.
        /// </summary>
        /// <param name="request">
        /// Параметры запроса.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public Task NewAsync(IStoreTessaViewRequest request, CancellationToken cancellationToken = default)
        {
            foreach (var model in request.Models)
            {
                if (this.models.Exists(m => ParserNames.IsEquals(m.Alias, model.Alias)))
                {
                    throw new UniqueAliasException(model.Alias);
                }

                this.models.Add(model);
            }

            this.mediator.Notify();
            return Task.CompletedTask;
        }

        #endregion
    }
}