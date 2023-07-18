using System;

namespace Tessa.Extensions.Default.Shared
{
    public static class DefaultRequestTypes
    {
        #region Static Fields

        /// <summary>
        /// Отправка информации для массового ознакомления
        /// </summary>
        public static readonly Guid Acquaintance = // 87E36C4A-0CB5-4226-8580-C339B4BBF2B7
            new Guid(0x87e36c4a, 0xcb5, 0x4226, 0x85, 0x80, 0xc3, 0x39, 0xb4, 0xbb, 0xf2, 0xb7);

        /// <summary>
        /// Запрос пользователей для заполнения списка информируемых
        /// </summary>
        public static readonly Guid GetDefaultAcquaintanceRoles = // 7A2CB692-7A55-4519-B193-0CE2DE435523
            new Guid(0x7a2cb692, 0x7a55, 0x4519, 0xb1, 0x93, 0xc, 0xe2, 0xde, 0x43, 0x55, 0x23);

        /// <summary>
        /// Получение информации по типу карточки и типу документа (если карточка имеет тип документа)
        /// по заданному идентификатору карточки.
        /// </summary>
        public static readonly Guid GetDocTypeInfo = // 6A7B57E9-5088-40C9-A4CA-75A489974A9C
            new Guid(0x6a7b57e9, 0x5088, 0x40c9, 0xa4, 0xca, 0x75, 0xa4, 0x89, 0x97, 0x4a, 0x9c);

        /// <summary>
        /// Тестовый пример на получение данных от некоторого внешнего сервиса 1С.
        /// </summary>
        public static readonly Guid GetFake1C =
            new Guid(0x86333B21, 0xA1C5, 0x4698, 0xB0, 0x23, 0xB4, 0x27, 0xC8, 0xBC, 0xCF, 0x94);

        /// <summary>
        /// Запрос на загрузку информации для визуализации резолюций.
        /// </summary>
        public static readonly Guid GetResolutionVisualizationData = // 254EDD6E-08C2-4D5D-AC67-9D2A406A8FF1
            new Guid(0x254edd6e, 0x08c2, 0x4d5d, 0xac, 0x67, 0x9d, 0x2a, 0x40, 0x6a, 0x8f, 0xf1);

        /// <summary>
        /// Получение списка недоступных для текущего пользователя типов карточек и документов,
        /// определённых в типовом решении.
        /// </summary>
        public static readonly Guid GetUnavailableTypes =
            new Guid(0x1c1c648e, 0x4092, 0x4242, 0xa1, 0xdc, 0x0b, 0x2b, 0x3e, 0xe3, 0xc5, 0x76);

        /// <summary>
        /// Получение списка доступных типов документов, определённых в типовом решении.
        /// </summary>
        public static readonly Guid KrGetDocTypes =
            new Guid(0xf4080450, 0xb0d3, 0x4277, 0x96, 0xa9, 0x1c, 0x12, 0x53, 0xcd, 0x64, 0x3b);

        /// <summary>
        /// Запрос на перенос контента файла на заданный источник <see cref="Tessa.Cards.CardFileSourceType"/>.
        /// Рекомендуется вызывать запрос методом <see cref="DefaultExtensionHelper.MoveFilesToAsync"/>.
        ///
        /// В запросе должен быть указан идентификатор карточки <see cref="Tessa.Cards.CardRequest.CardID"/>
        /// и источник <see cref="DefaultExtensionHelper.SetSourceID"/>.
        ///
        /// Если также указан идентификатор файла <see cref="Tessa.Cards.CardRequest.FileID"/>,
        /// то переносятся только все версии заданного файла, иначе - все версии всех файлов карточки.
        /// </summary>
        public static readonly Guid MoveFiles =
            new Guid(0x64565091, 0x2c00, 0x4ecd, 0xb6, 0x41, 0x1c, 0x8c, 0x8f, 0x91, 0x2c, 0x58);

        /// <summary>
        /// Создание тестовых данных из карточки настроек типового решения.
        /// </summary>
        public static readonly Guid TestData = // 207E75B5-ABB8-403A-A12A-897019AFCCF6
            new Guid(0x207e75b5, 0xabb8, 0x403a, 0xa1, 0x2a, 0x89, 0x70, 0x19, 0xaf, 0xcc, 0xf6);

        /// <summary>
        /// Расширение цифровой подписи
        /// </summary>
        public static readonly Guid CAdESSignature = // 0E780A0B-E270-48C6-81B8-4D476E01542B
            new Guid(0x0e780a0b, 0xe270, 0x48c6, 0x81, 0xb8, 0x4d, 0x47, 0x6e, 0x01, 0x54, 0x2b);


        #endregion
    }
}