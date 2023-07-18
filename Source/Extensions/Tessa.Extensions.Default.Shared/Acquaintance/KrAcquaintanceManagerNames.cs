namespace Tessa.Extensions.Default.Shared.Acquaintance
{
    public static class KrAcquaintanceManagerNames
    {

        /// <summary>
        /// Поставщик менеджера ознакомлениий по умолчанию, использующий транзакции.
        /// </summary>
        public const string Default = nameof(Default);

        /// <summary>
        /// Поставщик менеджера ознакомлениий без транзакции и блокировок. Доступен только на сервере.
        /// </summary>
        public const string WithoutTransaction = nameof(WithoutTransaction);

    }
}
