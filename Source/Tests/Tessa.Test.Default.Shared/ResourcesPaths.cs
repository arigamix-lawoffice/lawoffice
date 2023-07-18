namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет стандартные пути к встроенным ресурсам.
    /// </summary>
    public static class ResourcesPaths
    {
        /// <summary>
        /// Базовый путь по которому расположены ресурсы.
        /// </summary>
        public const string Resources = nameof(Resources);

        /// <summary>
        /// Путь по которому расположены библиотеки локализации: <see cref="Resources"/>\<see cref="Localization"/>.
        /// </summary>
        public const string Localization = nameof(Localization);

        /// <summary>
        /// Путь по которому расположены сценарии со скриптами: <see cref="Resources"/>\<see cref="Sql"/>.
        /// </summary>
        public const string Sql = nameof(Sql);

        /// <summary>
        /// Путь по которому расположены схемы данных: <see cref="Resources"/>\<see cref="Tsd"/>.
        /// </summary>
        public const string Tsd = nameof(Tsd);

        /// <summary>
        /// Путь по которому расположены представления: <see cref="Resources"/>\<see cref="Views"/>.
        /// </summary>
        public const string Views = nameof(Views);

        /// <summary>
        /// Предоставляет пути по которому доступны карточки.
        /// </summary>
        public static class Cards
        {
            /// <summary>
            /// Путь по которому расположены карточки ролей: <see cref="Cards"/>\<see cref="Roles"/>.
            /// </summary>
            public static class Roles
            {
                /// <summary>
                /// Путь по которому расположены карточки ролей: <see cref="Cards"/>\<see cref="Roles"/>.
                /// </summary>
                public const string Name = nameof(Roles);

                /// <summary>
                /// Путь по которому расположены карточки ролей предназначенные для использования на СУБД SqlServer: <see cref="Roles"/>\<see cref="SqlServer"/>.
                /// </summary>
                public const string SqlServer = nameof(SqlServer);

                /// <summary>
                /// Путь по которому расположены карточки ролей предназначенные для использования на СУБД PostgreSql: <see cref="Roles"/>\<see cref="PostgreSql"/>.
                /// </summary>
                public const string PostgreSql = nameof(PostgreSql);
            }

            /// <summary>
            /// Путь по которому расположены карточки: <see cref="Resources"/>\<see cref="Cards"/>.
            /// </summary>
            public const string Name = nameof(Cards);

            /// <summary>
            /// Путь по которому расположены карточки конфигурации: <see cref="Resources"/>\<see cref="Cards"/>\<see cref="Configuration"/>.
            /// </summary>
            public const string Configuration = nameof(Configuration);

            /// <summary>
            /// Путь по которому расположены карточки типов документов: <see cref="Cards"/>\<see cref="DocumentTypes"/>.
            /// </summary>
            public const string DocumentTypes = nameof(DocumentTypes);

            /// <summary>
            /// Путь по которому расположены карточки календарей: <see cref="Cards"/>\<see cref="Calendars"/>.
            /// </summary>
            public const string Calendars = nameof(Calendars);

            /// <summary>
            /// Путь по которому расположены карточки типов условий: <see cref="Cards"/>\<see cref="CalendarCalcMethods"/>.
            /// </summary>
            public const string ConditionTypes = nameof(ConditionTypes);

            /// <summary>
            /// Путь по которому расположены карточки маршрутов: <see cref="Cards"/>\<see cref="KrProcess"/>.
            /// </summary>
            public const string KrProcess = nameof(KrProcess);

            /// <summary>
            /// Путь по которому расположены карточки уведомлений: <see cref="Cards"/>\<see cref="Notifications"/>.
            /// </summary>
            public const string Notifications = nameof(Notifications);

            /// <summary>
            /// Путь по которому расположены карточки уведомлений: <see cref="Cards"/>\<see cref="NotificationTypes"/>.
            /// </summary>
            public const string NotificationTypes = nameof(NotificationTypes);

            /// <summary>
            /// Путь по которому расположены карточки настроек: <see cref="Cards"/>\<see cref="Settings"/>.
            /// </summary>
            public const string Settings = nameof(Settings);
        }

        /// <summary>
        /// Предоставляет пути по которому расположены типы: <see cref="Resources"/>\<see cref="Types"/>.
        /// </summary>
        public static class Types
        {
            /// <summary>
            /// Путь по которому расположены типы: <see cref="Resources"/>\<see cref="Types"/>.
            /// </summary>
            public const string Name = nameof(Types);

            /// <summary>
            /// Путь по которому расположены типы карточек: <see cref="Types"/>\<see cref="Cards"/>.
            /// </summary>
            public const string Cards = nameof(Cards);

            /// <summary>
            /// Путь по которому расположены типы карточек диалогов: <see cref="Types"/>\<see cref="Dialogs"/>.
            /// </summary>
            public const string Dialogs = nameof(Dialogs);

            /// <summary>
            /// Путь по которому расположены типы файлов: <see cref="Types"/>\<see cref="Files"/>.
            /// </summary>
            public const string Files = nameof(Files);

            /// <summary>
            /// Путь по которому расположены типы заданий: <see cref="Types"/>\<see cref="Tasks"/>.
            /// </summary>
            public const string Tasks = nameof(Tasks);
        }
    }
}
