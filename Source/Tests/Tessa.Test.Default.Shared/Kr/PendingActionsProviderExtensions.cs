using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Содержит статические методы расширения <see cref="IPendingActionsProvider{T}"/>.
    /// </summary>
    public static class PendingActionsProviderExtensions
    {
        #region ApplyAction Methods

        /// <summary>
        /// Выполняет указанное асинхронное действие над заданным объектом.
        /// </summary>
        /// <typeparam name="T">Тип объекта над которым выполняется действие.</typeparam>
        /// <param name="obj">Объект над которым выполняется действие.</param>
        /// <param name="actionAsync">
        /// Действие.
        /// Параметры:
        /// Объект над которым выполняется действие;
        /// Информация об отложенном действии;
        /// Объект, посредством которого можно отменить асинхронную задачу.
        /// Возвращаемое значение:
        /// Результат выполнения.
        /// </param>
        /// <param name="name">Название действия.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в заданное действие.
        /// </remarks>
        public static T ApplyAction<T>(
            this T obj,
            Func<T, IPendingAction, CancellationToken, ValueTask<ValidationResult>> actionAsync,
            string name = default) where T : IPendingActionsProvider<IPendingAction, T>
        {
            Check.ArgumentNotNull(obj, nameof(obj));
            Check.ArgumentNotNull(actionAsync, nameof(actionAsync));

            var nameInternal = nameof(PendingActionsProviderExtensions) + "." + nameof(PendingActionsProviderExtensions.ApplyAction);

            if (!string.IsNullOrEmpty(name))
            {
                nameInternal += ": " + name;
            }

            obj.AddPendingAction(
                new PendingAction(
                    nameInternal,
                    (pendingAction, ct) => actionAsync(obj, pendingAction, ct)));

            return obj;
        }

        /// <summary>
        /// Выполняет указанное действие над заданным объектом.
        /// </summary>
        /// <typeparam name="T">Тип объекта над которым выполняется действие.</typeparam>
        /// <param name="obj">Объект над которым выполняется действие.</param>
        /// <param name="action">
        /// Действие.
        /// Параметры:
        /// Объект над которым выполняется действие;
        /// Информация об отложенном действии.
        /// </param>
        /// <param name="name">Название действия.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в заданное действие.
        /// </remarks>
        public static T ApplyAction<T>(
            this T obj,
            Action<T, IPendingAction> action,
            string name = default) where T : IPendingActionsProvider<IPendingAction, T>
        {
            Check.ArgumentNotNull(obj, nameof(obj));
            Check.ArgumentNotNull(action, nameof(action));

            ApplyAction(
                obj,
                (objClosure, actionClosure, ct) =>
                {
                    action(objClosure, actionClosure);
                    return new ValueTask<ValidationResult>(ValidationResult.Empty);
                },
                name: name);

            return obj;
        }

        /// <summary>
        /// Выполняет указанное действие над заданным объектом.
        /// </summary>
        /// <typeparam name="T">Тип объекта над которым выполняется действие.</typeparam>
        /// <param name="obj">Объект над которым выполняется действие.</param>
        /// <param name="action">
        /// Действие.
        /// Параметры:
        /// Объект над которым выполняется действие;
        /// Информация об отложенном действии.
        /// Возвращаемое значение:
        /// Результат выполнения.
        /// </param>
        /// <param name="name">Название действия.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в заданное действие.
        /// </remarks>
        public static T ApplyAction<T>(
            this T obj,
            Func<T, IPendingAction, ValidationResult> action,
            string name = default) where T : IPendingActionsProvider<IPendingAction, T>
        {
            Check.ArgumentNotNull(obj, nameof(obj));
            Check.ArgumentNotNull(action, nameof(action));

            ApplyAction(
                obj,
                (objClosure, actionClosure, ct) =>
                {
                    return new ValueTask<ValidationResult>(action(objClosure, actionClosure));
                },
                name: name);

            return obj;
        }

        #endregion

        #region If Methods

        /// <summary>
        /// Применяет указанное действие, если условие истинно.
        /// </summary>
        /// <typeparam name="T">Тип объекта над которым выполняется действие.</typeparam>
        /// <param name="obj">Объект над которым выполняется действие.</param>
        /// <param name="condition">Условие.</param>
        /// <param name="trueAction">Действие выполняемое при положительном значении условия.</param>
        /// <param name="elseAction">Действие выполняемое при отрицательном значении условия.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        public static T If<T>(
            this T obj,
            bool condition,
            Action<T> trueAction,
            Action<T> elseAction = default) where T : IPendingActionsProvider<IPendingAction, T>
        {
            Check.ArgumentNotNull(obj, nameof(obj));
            Check.ArgumentNotNull(trueAction, nameof(trueAction));

            if (condition)
            {
                trueAction(obj);
            }
            else
            {
                elseAction?.Invoke(obj);
            }

            return obj;
        }

        /// <summary>
        /// Применяет указанное действие, если условие истинно.
        /// </summary>
        /// <typeparam name="T">Тип объекта над которым выполняется действие.</typeparam>
        /// <param name="obj">Объект над которым выполняется действие.</param>
        /// <param name="condition">Условие.</param>
        /// <param name="trueAction">Действие выполняемое при положительном значении условия.</param>
        /// <param name="elseAction">Действие выполняемое при отрицательном значении условия.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        public static T If<T>(
            this T obj,
            Func<T, bool> condition,
            Action<T> trueAction,
            Action<T> elseAction = default) where T : IPendingActionsProvider<IPendingAction, T>
        {
            Check.ArgumentNotNull(obj, nameof(obj));
            Check.ArgumentNotNull(condition, nameof(condition));
            Check.ArgumentNotNull(trueAction, nameof(trueAction));

            if (condition(obj))
            {
                trueAction(obj);
            }
            else
            {
                elseAction?.Invoke(obj);
            }

            return obj;
        }

        #endregion

        /// <summary>
        /// Регистрирует отложенное действие в <paramref name="executor"/> для выполнения запланированных действий объекта <paramref name="producer"/>.
        /// </summary>
        /// <typeparam name="TExecutor">Тип объекта, в котором регистрируется действие по выполнению действий объекта типа <typeparamref name="TProducer"/>.</typeparam>
        /// <typeparam name="TProducer">Тип объекта, действия которого должны быть выполнены в <typeparamref name="TExecutor"/>.</typeparam>
        /// <param name="executor">Объект в котором регистрируется действие по выполнению отложенных действий объекта <paramref name="producer"/>.</param>
        /// <param name="producer">Объект, отложенные действия которого должны быть выполнены при выполнении отложенных действий <paramref name="executor"/>.</param>
        /// <returns>Объект типа <typeparamref name="TProducer"/>.</returns>
        /// <remarks>
        /// Данный метод позволяет зарегистрировать объект, отложенные действия которого должны быть выполнены при выполнения списка отложенных действий <paramref name="executor"/>.<para/>
        /// Для выполнения дополнительной валидации результатов обработки <paramref name="producer"/> используйте свойство <see cref="KrTestContext.ValidationFunc"/>.
        /// </remarks>
        public static TProducer RegisterPendingActionsProducer<TExecutor, TProducer>(
            this TExecutor executor,
            TProducer producer)
            where TExecutor : IPendingActionsProvider<IPendingAction, TExecutor>
            where TProducer : IPendingActionsExecutor<TProducer>
        {
            Check.ArgumentNotNull(executor, nameof(executor));
            Check.ArgumentNotNull(producer, nameof(producer));

            executor.AddPendingAction(
                new PendingAction(
                    nameof(PendingActionsProviderExtensions) + "." + nameof(PendingActionsProviderExtensions.RegisterPendingActionsProducer),
                    async (_, cancellationToken) =>
                    {
                        await producer.GoAsync(cancellationToken: cancellationToken);
                        return ValidationResult.Empty;
                    }));

            return producer;
        }
    }
}
