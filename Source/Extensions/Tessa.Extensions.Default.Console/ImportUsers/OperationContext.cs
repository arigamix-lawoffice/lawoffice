namespace Tessa.Extensions.Default.Console.ImportUsers
{
    public class OperationContext
    {
        public ImportType ImportType { get; set; }

        public string PathToUserFile { get; set; }

        public string PathToDepartmentFile { get; set; }

        public string ConfigurationString { get; set; }

        public string DatabaseName { get; set; }
    }
}
