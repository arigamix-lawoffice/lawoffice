namespace Tessa.Extensions.Default.Console.InvalidateCache
{
    public class OperationContext
    {
        /// <summary>
        /// Не равно <c>null</c>. Пустой массив приводит к сбросу кэшей.
        /// </summary>
        public string[] CacheNames { get; set; }
    }
}