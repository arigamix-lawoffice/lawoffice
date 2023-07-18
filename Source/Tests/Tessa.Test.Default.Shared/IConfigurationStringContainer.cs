namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Объект, содержащий имя строки подключения из конфигурационного файла.
    /// </summary>
    public interface IConfigurationStringContainer
    {
        /// <summary>
        /// Возвращает или задаёт имя строки подключения из конфигурационного файла.
        /// </summary>
        string ConfigurationString { get; set; }

        /// <summary>
        /// Возвращает или задаёт строку подключения, которую надо использовать с провайдером, указанным в свойстве <see cref="ConfigurationString"/>,
        /// или <see langword="null"/>, если строка подключения используется из строки конфигурации.
        /// </summary>
        string ConnectionString { get; set; }
    }
}
