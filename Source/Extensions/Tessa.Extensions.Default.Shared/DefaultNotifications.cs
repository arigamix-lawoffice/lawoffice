using System;

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Идентификаторы карточек уведомлений, используемые в типовом решении и в платформе.
    /// </summary>
    public static class DefaultNotifications
    {
        /// <summary>
        /// Идентификатор карточки уведомления для массового ознакомления.
        /// </summary>
        public static readonly Guid AcquaintanceID = new Guid(0x9e3d20a6, 0x0dff, 0x4667, 0xa2, 0x9d, 0x30, 0x29, 0x66, 0x35, 0xc8, 0x9a); // 9e3d20a6-0dff-4667-a29d-30296635c89a

        /// <summary>
        /// Идентификатор карточки уведомления для предупреждений по сроку истечения пароля.
        /// </summary>
        public static readonly Guid PasswordExpiresID = new Guid(0x70da1d51, 0x9f21, 0x4693, 0xae, 0xbd, 0x0a, 0x05, 0xe2, 0x19, 0x00, 0x27); // 70da1d51-9f21-4693-aebd-0a05e2190027

        /// <summary>
        /// Уведомление о задании типового решения
        /// </summary>
        public static Guid TaskNotification = new Guid(0x20411ff1, 0xbe39, 0x4bcd, 0x9b, 0xd2, 0xd6, 0x44, 0xbf, 0x2b, 0xb7, 0x77); // 20411ff1-be39-4bcd-9bd2-d644bf2bb777

        /// <summary>
        /// Уведомление о завершении задания доп. согласования
        /// </summary>
        public static Guid AdditionalApprovalNotification = new Guid(0xe0c88f19, 0xbb4a, 0x4d63, 0xb9, 0xaa, 0xc4, 0x1e, 0x05, 0x33, 0xa7, 0x3a); // e0c88f19-bb4a-4d63-b9aa-c41e0533a73a

        /// <summary>
        /// Уведомление о завершении доп. согласования
        /// </summary>
        public static Guid AdditionalApprovalNotificationCompleted = new Guid(0x4ef60907, 0xb3c3, 0x4c3b, 0xbc, 0x73, 0xca, 0x56, 0x03, 0xa1, 0x3b, 0x44); // 4ef60907-b3c3-4c3b-bc73-ca5603a13b44

        /// <summary>
        /// Уведомление об ответе на запрос комментария
        /// </summary>
        public static Guid CommentNotification = new Guid(0xd1c0d80e, 0xf000, 0x4797, 0x9d, 0x56, 0x17, 0xbe, 0xe2, 0xc1, 0x33, 0xf9); // d1c0d80e-f000-4797-9d56-17bee2c133f9

        /// <summary>
        /// Уведомление о необходимости изменить токен для подписи
        /// </summary>
        public static Guid TokenNotification = new Guid(0xff600045, 0x49b1, 0x47d0, 0xa7, 0x5e, 0xb0, 0xe2, 0x06, 0x01, 0xd3, 0xae); // ff600045-49b1-47d0-a75e-b0e20601d3ae

        /// <summary>
        /// Уведомление о завершении подзадачи
        /// </summary>
        public static Guid WfChildResolutionNotification = new Guid(0xca6fa961, 0xd220, 0x4bf2, 0x8a, 0x33, 0xb5, 0x4f, 0x84, 0x13, 0x6b, 0x0c); // ca6fa961-d220-4bf2-8a33-b54f84136b0c

        /// <summary>
        /// Уведомление об отзыве задачи
        /// </summary>
        public static Guid WfRevokeNotification = new Guid(0x26b2e1a1, 0x8c57, 0x4028, 0xbb, 0x41, 0x19, 0x66, 0x55, 0x26, 0x56, 0xe2); // 26b2e1a1-8c57-4028-bb41-1966552656e2

        /// <summary>
        /// Уведомление о согласовании
        /// </summary>
        public static Guid ApprovedNotification = new Guid(0xb745d1e2, 0xb1c2, 0x410f, 0xbe, 0x0b, 0xe0, 0xdc, 0x96, 0x02, 0xbb, 0xc1); // b745d1e2-b1c2-410f-be0b-e0dc9602bbc1

        /// <summary>
        /// Уведомление о возврате задания из отложенного
        /// </summary>
        public static Guid ReturnFromPostponeNotification = new Guid(0x93a7fb23, 0x6658, 0x46f9, 0xb9, 0xf4, 0x83, 0xe1, 0x46, 0x3a, 0x12, 0x3a); // 93a7fb23-6658-46f9-b9f4-83e1463a123a

        /// <summary>
        /// Уведомление об отправке ежедневных уведомлений о задании. 
        /// </summary>
        public static Guid TasksNotification = new Guid(0x8cb57058, 0xcc51, 0x4ce9, 0xa3, 0x59, 0x9f, 0x40, 0x8f, 0x1a, 0xe8, 0x08); // 8cb57058-cc51-4ce9-a359-9f408f1ae808

        /// <summary>
        /// Уведомление о новых сообщениях в обсуждениях.
        /// </summary>
        public static Guid ForumNewMessagesNotification = new Guid(0x39e0f3ea, 0xe71f, 0x494e, 0x93, 0x7f, 0x17, 0xdf, 0x7d, 0x42, 0x03, 0x19); // 39e0f3ea-e71f-494e-937f-17df7d420319
    }
}
