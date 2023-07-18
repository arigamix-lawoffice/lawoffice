namespace Tessa.Extensions.Default.Console.ConvertConfiguration
{
    public class OperationContext
    {
        public string Source { get; set; }
        
        public string Target { get; set; }
        
        public bool DoNotDelete { get; set; }
        
        public ConversionMode ConversionMode { get; set; }
    }
}
