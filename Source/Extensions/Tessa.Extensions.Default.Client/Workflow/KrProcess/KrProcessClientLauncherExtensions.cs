using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess
{
    /// <summary>
    /// Предоставляет статические методы расширения для <see cref="IKrProcessLauncher"/>.
    /// </summary>
    public static class KrProcessClientLauncherExtensions
    {
        /// <summary>
        /// Запускает указанный процесс с использованием заданного представления карточки на клиенте (<paramref name="cardEditor"/>).
        /// </summary>
        /// <param name="launcher">Объект выполняющий запуск процессов.</param>
        /// <param name="krProcess">Запускаемый процесс.</param>
        /// <param name="cardEditor">Редактируемое представление карточки на клиенте.</param>
        /// <param name="raiseErrorWhenExecutionIsForbidden">Значение <see langword="true"/>, если необходимо создавать ошибку, если процесс не может быть выполнен из-за ограничений (параметр вторичного процесса "Сообщение при невозможности выполнения процесса"), иначе - <see langword="false"/>.</param>
        /// <returns></returns>
        public static async Task<IKrProcessLaunchResult> LaunchWithCardEditorAsync(
            this IKrProcessLauncher launcher,
            KrProcessInstance krProcess,
            ICardEditorModel cardEditor,
            bool raiseErrorWhenExecutionIsForbidden)
        {
            krProcess = KrProcessBuilder
                .ModifyProcess(krProcess)
                .Build();

            await using (UIContext.Create(new UIContext(cardEditor)))
            {
                var specificParam = new KrProcessUILauncher.SpecificParameters
                {
                    UseCurrentCardEditor = true,
                    RaiseErrorWhenExecutionIsForbidden = raiseErrorWhenExecutionIsForbidden
                };
                return await launcher.LaunchAsync(krProcess, null, specificParam);
            }
        }
    }
}