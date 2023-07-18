using System;

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Идентификаторы вариантов завершения, задействованных в типовом решении.
    /// </summary>
    public static class DefaultCompletionOptions
    {
        #region Static Fields

        /// <summary>
        /// Вариант завершения "Добавить комментарий".
        /// </summary>
        public static readonly Guid AddComment =                    // 78C2FD7D-D0FE-0EDE-93A6-9DE4F372E8E6
            new Guid(0x78c2fd7d, 0xd0fe, 0x0ede, 0x93, 0xa6, 0x9d, 0xe4, 0xf3, 0x72, 0xe8, 0xe6);

        /// <summary>
        /// Вариант завершения "Принять".
        /// </summary>
        public static readonly Guid Accept =                        // 7000EA10-EFD8-0479-A6D4-B5E37A27F30A
            new Guid(0x7000ea10, 0xefd8, 0x0479, 0xa6, 0xd4, 0xb5, 0xe3, 0x7a, 0x27, 0xf3, 0x0a);

        /// <summary>
        /// Вариант завершения "Согласовать".
        /// </summary>
        public static readonly Guid Approve =                       // 8CF5CF41-8347-05B4-B3B2-519E8E621225
            new Guid(0x8cf5cf41, 0x8347, 0x05b4, 0xb3, 0xb2, 0x51, 0x9e, 0x8e, 0x62, 0x12, 0x25);

        /// <summary>
        /// Вариант завершения "Отмена".
        /// </summary>
        public static readonly Guid Cancel =                        // 2582B66F-375A-0D59-AE86-A149309C5785
            new Guid(0x2582b66f, 0x375a, 0x0d59, 0xae, 0x86, 0xa1, 0x49, 0x30, 0x9c, 0x57, 0x85);

        /// <summary>
        /// Вариант завершения "Отменить процесс согласования".
        /// </summary>
        public static readonly Guid CancelApprovalProcess =         // 6E244482-2E2F-46FD-8EC3-0DE6DAEA2930
            new Guid(0x6e244482, 0x2e2f, 0x46fd, 0x8e, 0xc3, 0x0d, 0xe6, 0xda, 0xea, 0x29, 0x30);

        /// <summary>
        /// Вариант завершения "Завершить".
        /// </summary>
        public static readonly Guid Complete =                      // 5B108223-92DB-49B9-8085-336758CCABAA
            new Guid(0x5b108223, 0x92db, 0x49b9, 0x80, 0x85, 0x33, 0x67, 0x58, 0xcc, 0xab, 0xaa);

        /// <summary>
        /// Вариант завершения "Продолжить".
        /// </summary>
        public static readonly Guid Continue =                      // 9BA9F111-FA2F-4C8E-8236-C924280A4A07
            new Guid(0x9ba9f111, 0xfa2f, 0x4c8e, 0x82, 0x36, 0xc9, 0x24, 0x28, 0x0a, 0x4a, 0x07);

        /// <summary>
        /// Вариант завершения "Создать подзадачу".
        /// </summary>
        public static readonly Guid CreateChildResolution =         // 793BBAFA-7F62-4AF8-A156-515887D4D066
            new Guid(0x793bbafa, 0x7f62, 0x4af8, 0xa1, 0x56, 0x51, 0x58, 0x87, 0xd4, 0xd0, 0x66);

        /// <summary>
        /// Вариант завершения "Делегировать".
        /// </summary>
        public static readonly Guid Delegate =                      // B997A7F2-AD57-036F-8798-298C14309F46
            new Guid(0xb997a7f2, 0xad57, 0x036f, 0x87, 0x98, 0x29, 0x8c, 0x14, 0x30, 0x9f, 0x46);

        /// <summary>
        /// Вариант завершения "Отменить регистрацию документа".
        /// </summary>
        public static readonly Guid DeregisterDocument =            // 66E0A7E1-484A-40A6-B123-06118CE3B160
            new Guid(0x66e0a7e1, 0x484a, 0x40a6, 0xb1, 0x23, 0x06, 0x11, 0x8c, 0xe3, 0xb1, 0x60);

        /// <summary>
        /// Вариант завершения "Не согласовать".
        /// </summary>
        public static readonly Guid Disapprove =                    // 811D41EF-5610-421E-A573-FCDFD821713E
            new Guid(0x811d41ef, 0x5610, 0x421e, 0xa5, 0x73, 0xfc, 0xdf, 0xd8, 0x21, 0x71, 0x3e);

        /// <summary>
        /// Вариант завершения "Изменить параметры как автор".
        /// </summary>
        public static readonly Guid ModifyAsAuthor =               // 89ADA741-6829-4D9F-892B-72D76ECF4EE6
            new Guid(0x89ada741, 0x6829, 0x4d9f, 0x89, 0x2b, 0x72, 0xd7, 0x6e, 0xcf, 0x4e, 0xe6);

        /// <summary>
        /// Вариант завершения "Новый цикл согласования".
        /// </summary>
        public static readonly Guid NewApprovalCycle =              // C0B704B3-3AC5-4A0D-BCB6-1210E9CDB0B3
            new Guid(0xc0b704b3, 0x3ac5, 0x4a0d, 0xbc, 0xb6, 0x12, 0x10, 0xe9, 0xcd, 0xb0, 0xb3);

        /// <summary>
        /// Вариант завершения "Вариант А" для тестового процесса согласования.
        /// </summary>
        public static readonly Guid OptionA =                       // D6FBBF34-D22D-4226-831D-F3F1F31B9954
            new Guid(0xd6fbbf34, 0xd22d, 0x4226, 0x83, 0x1d, 0xf3, 0xf1, 0xf3, 0x1b, 0x99, 0x54);

        /// <summary>
        /// Вариант завершения "Вариант Б" для тестового процесса согласования.
        /// </summary>
        public static readonly Guid OptionB =                       // 679A8309-F251-4ACF-8B2E-7C5277B04D63
            new Guid(0x679a8309, 0xf251, 0x4acf, 0x8b, 0x2e, 0x7c, 0x52, 0x77, 0xb0, 0x4d, 0x63);

        /// <summary>
        /// Вариант завершения "Вернуть документ на доработку".
        /// </summary>
        public static readonly Guid RebuildDocument =               // 174D3F96-C658-07B7-BA6A-D51A893390D8
            new Guid(0x174d3f96, 0xc658, 0x07b7, 0xba, 0x6a, 0xd5, 0x1a, 0x89, 0x33, 0x90, 0xd8);

        /// <summary>
        /// Вариант завершения "Зарегистрировать документ".
        /// </summary>
        public static readonly Guid RegisterDocument =              // 48AE0FD4-8A0D-494A-B89D-CA8FC33EFE7C
            new Guid(0x48ae0fd4, 0x8a0d, 0x494a, 0xb8, 0x9d, 0xca, 0x8f, 0xc3, 0x3e, 0xfe, 0x7c);

        /// <summary>
        /// Вариант завершения "Отозвать согласование".
        /// </summary>
        public static readonly Guid RejectApproval =                // D97D75A9-96AE-00CA-83AD-BAA5C6AA811B
            new Guid(0xd97d75a9, 0x96ae, 0x00ca, 0x83, 0xad, 0xba, 0xa5, 0xc6, 0xaa, 0x81, 0x1b);

        /// <summary>
        /// Вариант завершения "Запросить комментарии".
        /// </summary>
        public static readonly Guid RequestComments =               // FFFB3209-2B67-09F0-BD25-BA4EC94CA5E8
            new Guid(0xfffb3209, 0x2b67, 0x09f0, 0xbd, 0x25, 0xba, 0x4e, 0xc9, 0x4c, 0xa5, 0xe8);

        /// <summary>
        /// Вариант завершения "Дополнительное согласование".
        /// </summary>
        public static readonly Guid AdditionalApproval =            // c726d8ba-73b9-4867-87fe-387d4c61a75a
            new Guid(0xc726d8ba, 0x73b9, 0x4867, 0x87, 0xfe, 0x38, 0x7d, 0x4c, 0x61, 0xa7, 0x5a);

        /// <summary>
        /// Вариант завершения "Отозвать".
        /// </summary>
        public static readonly Guid Revoke =                        // 6472FEA9-F818-4AB5-9F31-9CCDAEA9B412
            new Guid(0x6472fea9, 0xf818, 0x4ab5, 0x9f, 0x31, 0x9c, 0xcd, 0xae, 0xa9, 0xb4, 0x12);

        /// <summary>
        /// Вариант завершения "Отправить исполнителю".
        /// </summary>
        public static readonly Guid SendToPerformer =               // F4EBE563-14F6-4B20-A61F-0BAC4C11C8AC
            new Guid(0xf4ebe563, 0x14f6, 0x4b20, 0xa6, 0x1f, 0x0b, 0xac, 0x4c, 0x11, 0xc8, 0xac);

        /// <summary>
        /// Вариант завершения "Подписать".
        /// </summary>
        public static readonly Guid Sign =                          // 45D6F756-D30B-4C98-9D72-6ADF1A15D075
            new Guid(0x45d6f756, 0xd30b, 0x4c98, 0x9d, 0x72, 0x6a, 0xdf, 0x1a, 0x15, 0xd0, 0x75);

        /// <summary>
        /// Вариант завершения "Отказать".
        /// </summary>
        public static readonly Guid Decline =                 // 4DE44FFD-C2CA-4FAD-835B-631222B076E1
            new Guid(0x4de44ffd, 0xc2ca, 0x4fad, 0x83, 0x5b, 0x63, 0x12, 0x22, 0xb0, 0x76, 0xe1);

        /// <summary>
        /// Вариант завершения "Показать диалог".
        /// </summary>
        public static readonly Guid ShowDialog =
            new Guid(0xA9067834, 0x1A01, 0x468C, 0x97, 0x6B, 0x0E, 0xC7, 0xA9, 0x93, 0x93, 0x31);
        
        #endregion
    }
}
