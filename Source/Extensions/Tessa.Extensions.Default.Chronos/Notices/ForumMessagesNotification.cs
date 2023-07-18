using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Forums.Notifications;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    public class ForumMessagesNotification
    {
        #region Constructors

        public ForumMessagesNotification(
            Guid userID,
            string normalizedWebAddress)
        {
            this.UserID = userID;
            this.WebAddress = normalizedWebAddress;

            this.TopicsNotifications = new List<ITopicNotificationInfo>();
        }

        #endregion

        #region Properties

        public Guid UserID { get; }

        public string WebAddress { get; }

        public List<ITopicNotificationInfo> TopicsNotifications { get; set; }

        #endregion

        #region Methods

        public Dictionary<string, object> CreateInfo()
        {
            return new Dictionary<string, object>(StringComparer.Ordinal)
            {
                [nameof(this.TopicsNotifications)] = this.TopicsNotifications
                    .Select(x => (object) x is TopicNotificationInfo obj
                        ? obj.GetStorage()
                        : new TopicNotificationInfo(x).GetStorage())
                    .ToList(),
                ["HasWeb"] = BooleanBoxes.Box(!string.IsNullOrWhiteSpace(this.WebAddress)),
            };
        }

        #endregion
    }
}
