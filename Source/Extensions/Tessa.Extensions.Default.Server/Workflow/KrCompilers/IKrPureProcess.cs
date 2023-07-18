namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Описывает объект предоставляющий информацию о вторичном процессе работающем в режиме "Простой процесс".
    /// </summary>
    public interface IKrPureProcess :
        IKrSecondaryProcess
    {
        /// <summary>
        /// Запуск процесса разрешен с клиента.
        /// </summary>
        bool AllowClientSideLaunch { get; }

        /// <summary>
        /// Проверять ограничения при запуске процесса.
        /// </summary>
        bool CheckRecalcRestrictions { get; }
    }
}