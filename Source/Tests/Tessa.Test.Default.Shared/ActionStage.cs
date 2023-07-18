namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Этап выполнения действия.
    /// </summary>
    public enum ActionStage
    {
        /// <summary>
        /// Перед любыми действиями выполняемыми однократно при инициализации тестов.
        /// </summary>
        BeforeInitialize,

        /// <summary>
        /// Перед инициализацией Unity-контейнера.
        /// </summary>
        /// <remarks>
        /// Не получайте зависимости из контейнера, кроме тех, которые необходимы для его инициализации.
        /// </remarks>
        BeforeInitializeContainer,

        /// <summary>
        /// После инициализации Unity-контейнера.
        /// </summary>
        /// <remarks>
        /// Не получайте зависимости из контейнера, кроме тех, которые необходимы для его инициализации.
        /// </remarks>
        AfterInitializeContainer,

        /// <summary>
        /// Перед инициализацией области выполнения.
        /// </summary>
        BeforeInitializeScope,

        /// <summary>
        /// После инициализации области выполнения.
        /// </summary>
        AfterInitializeScope,

        /// <summary>
        /// После выполнения всех действий выполняемых однократно при инициализации тестов.
        /// </summary>
        AfterInitialize,

        /// <summary>
        /// Перед выполнением любыми другими действиями выполняемыми перед выполнением каждого теста.
        /// </summary>
        BeforeSetUp,

        /// <summary>
        /// После выполнения любых других действий выполняемых перед выполнением каждого теста.
        /// </summary>
        AfterSetUp,

        /// <summary>
        /// Перед выполнением любыми другими действиями выполняемыми после выполнения каждого теста.
        /// </summary>
        BeforeTearDown,

        /// <summary>
        /// После выполнения любых других действий выполняемых после выполнением каждого теста.
        /// </summary>
        AfterTearDown,

        /// <summary>
        /// Перед выполнением любых других действий выполняемых после выполнения всех тестов.
        /// </summary>
        BeforeOneTimeTearDown,

        /// <summary>
        /// После выполнения любых других действий выполняемых после выполнения всех тестов.
        /// </summary>
        AfterOneTimeTearDown,

        /// <summary>
        /// Перед освобождением области выполнения.
        /// </summary>
        BeforeOneTimeTearDownScope,

        /// <summary>
        /// После освобождения области выполнения.
        /// </summary>
        AfterOneTimeTearDownScope,
    }
}
