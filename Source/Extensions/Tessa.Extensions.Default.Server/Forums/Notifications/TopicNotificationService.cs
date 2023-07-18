using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Tessa.Cards;
using Tessa.Forums;
using Tessa.Forums.Models;
using Tessa.Forums.Notifications;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Unity;

namespace Tessa.Extensions.Default.Server.Forums.Notifications
{
    /// <inheritdoc />
    public class TopicNotificationService : TopicNotificationServiceBase
    {
        #region Private Fields

        private readonly ISession session;

        private readonly IForumUserNamingStrategy forumUserNamingStrategy;

        #endregion

        #region Constructor

        /// <summary>
        /// <inheritdoc cref="TopicNotificationService" path="/summary"/>
        /// </summary>
        /// <param name="dbScope"><inheritdoc cref="IDbScope" path="/summary"/></param>
        /// <param name="createTopicQueryBuilderFunc">Функция, инициализирующая новый экземпляр объекта <c>ITopicQueryBuilder</c>.</param>
        /// <param name="session"><inheritdoc cref="ISession" path="/summary"/></param>
        /// <param name="forumUserNamingStrategy"><inheritdoc cref="IForumUserNamingStrategy" path="/summary"/></param>
        public TopicNotificationService(
            IDbScope dbScope,
            Func<ITopicQueryBuilder> createTopicQueryBuilderFunc,
            ISession session,
            [OptionalDependency] IForumUserNamingStrategy forumUserNamingStrategy)
            : base(dbScope, createTopicQueryBuilderFunc)
        {
            this.session = session;
            this.forumUserNamingStrategy = forumUserNamingStrategy;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async ValueTask<IList<ITopicNotificationInfo>> GetNotificationsInfoAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            var result = new List<ITopicNotificationInfo>();

            await using (this.DbScope.Create())
            {
                var db = this.DbScope.Db;

                var normalizedWebAddress = await this.GetNormalizedWebAddressAsync(cancellationToken);
                var defaultUtcOffset = await this.GetDefaultUtcOffsetAsync(cancellationToken);

                await using var reader = await db.SetCommand(
                        this.BuildQuery(),
                        db.Parameter("CurrentPluginRunDateTime", endDate),
                        db.Parameter("LastPluginRunDateTime", startDate))
                    .LogCommand()
                    .ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    Guid userID = reader.GetGuid(0);
                    Guid cardID = reader.GetGuid(1);
                    Guid topicID = reader.GetGuid(2);
                    string topicTitle = await reader.GetSequentialNullableStringAsync(3, cancellationToken);
                    int? utcOffsetMinutes = reader.GetNullableInt32(4);
                    DateTime? messageDate = reader.GetNullableDateTimeUtc(5);

                    MessageType typeID = (MessageType) reader.GetInt32(6);
                    Guid messageID = reader.GetGuid(7);
                    string authorName = reader.GetNullableString(8);
                    string htmlText = await reader.GetSequentialNullableStringAsync(9, cancellationToken);
                    string topicDescription = await reader.GetSequentialNullableStringAsync(10, cancellationToken);

                    var body = ForumSerializationHelper.DeserializeMessageBody(htmlText);
                    htmlText = body.Text;
                    htmlText = Regex.Replace(htmlText, "color:#[0-9a-fA-F]{8}", p => p.Value[..^2]);

                    var info = new TopicNotificationInfo
                    {
                        UserID = userID,
                        CardID = cardID,
                        TopicID = topicID,
                        TopicTitle = HttpUtility.HtmlEncode(topicTitle),
                        MessageDate = messageDate.HasValue ? messageDate.Value + TimeSpan.FromMinutes(utcOffsetMinutes ?? defaultUtcOffset) : null,
                        Type = typeID,
                        Info = body.Info,
                        MessageID = messageID,
                        AuthorName = authorName,
                        HtmlText = htmlText,
                        TopicDescription = topicDescription,
                    };

                    info.Link = CardHelper.GetLink(this.session, info.CardID);
                    info.WebLink = CardHelper.GetWebLink(normalizedWebAddress, info.CardID, normalize: false);

                    result.Add(info);
                }
            }

            if (this.forumUserNamingStrategy is not null)
            {
                await this.forumUserNamingStrategy.ReplaceAsync(result, cancellationToken);
            }

            return result.OrderBy(p => p.UserID).ToList();
        }

        /// <inheritdoc />
        protected override ValueTask<int> GetDefaultUtcOffsetAsync(CancellationToken cancellationToken = default)
            => this.GetDefaultUtcOffsetCoreAsync(cancellationToken);

        /// <inheritdoc />
        protected override ValueTask<string> GetNormalizedWebAddressAsync(CancellationToken cancellationToken = default)
            => this.GetNormalizedWebAddressCoreAsync(cancellationToken);

        /// <inheritdoc />
        protected override string BuildQuery()
        {
            return this.CreateTopicQueryBuilderFunc()
                .AppendSelect()
                .AppendJoins()
                .AppendJoinTopicsAndUsers()
                .AppendWhere()
                .AppendOrderBy()
                .Build();
        }

        #endregion

        #region Private Methods

        private async ValueTask<string> GetNormalizedWebAddressCoreAsync(CancellationToken cancellationToken = default)
        {
            var db = this.DbScope.Db;
            string webAddress = await db
                .SetCommand(
                    this.DbScope.BuilderFactory
                        .Select().Top(1).C("WebAddress")
                        .From("ServerInstances").NoLock()
                        .Limit(1)
                        .Build())
                .LogCommand()
                .ExecuteAsync<string>(cancellationToken);

            return LinkHelper.NormalizeWebAddress(webAddress);
        }

        private async ValueTask<int> GetDefaultUtcOffsetCoreAsync(CancellationToken cancellationToken = default)
        {
            var db = this.DbScope.Db;
            return await db
                .SetCommand(
                    this.DbScope.BuilderFactory
                        .Select().Top(1).C("UtcOffsetMinutes")
                        .From("DefaultTimeZone").NoLock()
                        .Limit(1)
                        .Build())
                .LogCommand()
                .ExecuteAsync<int>(cancellationToken);
        }

        #endregion
    }
}
