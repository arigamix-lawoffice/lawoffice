namespace Tessa.Extensions.Default.Console.FileSource
{
    public class OperationContext
    {
        public int ID { get; set; }

        public string FileLocation { get; set; }

        public string DatabaseLocation { get; set; }

        public string Name { get; set; }

        public string FileExtensions { get; set; }

        public bool Remove { get; set; }

        public bool IsDefault { get; set; }
    }
}
