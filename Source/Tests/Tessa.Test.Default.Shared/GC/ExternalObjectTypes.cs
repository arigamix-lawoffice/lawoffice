using System;

namespace Tessa.Test.Default.Shared.GC
{
    /// <summary>
    /// Типы внешних объектов.
    /// </summary>
    public static class ExternalObjectTypes
    {
        /// <summary>
        /// База данных. {CB893150-691B-4A66-97AF-B15272DDEC4E}
        /// </summary>
        public static Guid Database = new Guid(0xcb893150, 0x691b, 0x4a66, 0x97, 0xaf, 0xb1, 0x52, 0x72, 0xdd, 0xec, 0x4e);

        /// <summary>
        /// Директория. {B65D36BE-879F-4052-B8F0-512DA6D67A42}
        /// </summary>
        public static Guid Folder = new Guid(0xb65d36be, 0x879f, 0x4052, 0xb8, 0xf0, 0x51, 0x2d, 0xa6, 0xd6, 0x7a, 0x42);

        /// <summary>
        /// Файл. {4AE51621-51AE-474D-93AD-3C620752629D}
        /// </summary>
        public static Guid File = new Guid(0x4ae51621, 0x51ae, 0x474d, 0x93, 0xad, 0x3c, 0x62, 0x7, 0x52, 0x62, 0x9d);

        /// <summary>
        /// Redis. {5D577250-BB14-4AE1-A785-EBA48868A5CE}
        /// </summary>
        public static Guid Redis = new Guid(0x5d577250, 0xbb14, 0x4ae1, 0xa7, 0x85, 0xeb, 0xa4, 0x88, 0x68, 0xa5, 0xce);
    }
}
