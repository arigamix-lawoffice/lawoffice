
namespace Tessa.Extensions.Default.Shared
{
    public static class CycleGroupingInfoKeys
    {
        public const string MaxCycleNumberKey = "KrMaxCycleNumber";
        public const string FilesByCyclesKey = "KrFilesByCycles";
        public const string FilesModifiedByCyclesKey = "KrFilesModifiedByCycles";
        public const string CycleIDKey = "KrCycleID";
        public const string CycleOrderKey = "KrCycleorder";
        public const string CreatedByNameKey = "KrCreatedByName";
        public const string CreatedKey = "KrCreated";

        // Ключ, в инфо контрола файлов/UIContext/Model для корректного сохранения выбранного режима отображения файлов в группировке по циклам
        // В инфо контрола и CardModel - это просто режим - значение из справочника CycleGroupingMode
        // В инфо UIContext - это Dictionary<string, CycleGroupingMode>, где по алиасу контролла можно найти его режим, если алиас у котрола есть
        public const string CycleGroupingModeKey = "CycleGroupingMode";
    }
}