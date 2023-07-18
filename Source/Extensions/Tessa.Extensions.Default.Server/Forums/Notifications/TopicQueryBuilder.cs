using System;
using Tessa.Forums;
using Tessa.Forums.Notifications;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Roles;

namespace Tessa.Extensions.Default.Server.Forums.Notifications
{
    /// <inheritdoc />
    public class TopicQueryBuilder : ITopicQueryBuilder
    {
        #region Constructors

        /// <summary>
        /// Инциализирует новый экземпляр класса.
        /// </summary>
        /// <param name="dbScope">Используется для получения <see cref="IQueryBuilder"/>.</param>
        public TopicQueryBuilder(IDbScope dbScope)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));
            
            this.QueryBuilder = dbScope.BuilderFactory.Create();
        }
        
        #endregion
        
        #region Protected Properties

        /// <summary>
        /// Объект, выполняющий построение SQL-запроса.
        /// </summary>
        protected IQueryBuilder QueryBuilder { get; }
        
        #endregion
        
        #region ITopicQueryBuilder Members

        /// <inheritdoc />
        public virtual string Build() => this.QueryBuilder.Build();

        /// <inheritdoc />
        public virtual ITopicQueryBuilder AppendSelect()
        {
            this.QueryBuilder.Select()
                .C("usr", "UserID")
                .C("s", "MainCardID")
                .C("msg", "TopicRowID")
                .C("tp", "Title")
                .C("r", "TimeZoneUtcOffsetMinutes")
                .C("msg", "Created", "TypeID", "RowID", "AuthorName", "Body")
                .C("tp", "Description")
                .From("FmMessages", "msg").NoLock();
            return this;
        }

        /// <inheritdoc />
        public virtual ITopicQueryBuilder AppendJoins()
        {
            this.QueryBuilder.InnerJoin("FmTopics", "tp").NoLock()
                .On().C("msg", "TopicRowID").Equals().C("tp", "RowID")
                .InnerJoin("Satellites", "s").NoLock()
                .On().C("tp", "ID").Equals().C("s", "ID")
                .And().C("s", "TypeID").Equals().V(ForumHelper.ForumSatelliteTypeID);
            return this;
        }

        /// <inheritdoc />
        public virtual ITopicQueryBuilder AppendJoinTopicsAndUsers(
            Action<IQueryBuilder> unionAdditionalRowsAction = null,
            Action<IQueryBuilder> subQueryExpression = null)
        {
            this.QueryBuilder.InnerJoinLateral(q =>
                    {
                        q.SelectDistinct()
                            .C("t", "TopicRowID", "UserID")
                            .From(t =>
                                {
                                    t.Select()
                                        .C("tp", "TopicRowID", "UserID")
                                        .From("FmTopicParticipants", "tp").NoLock()
                                        .Where()
                                        .C("tp", "TopicRowID").Equals().C("msg", "TopicRowID")
                                        .UnionAll()
                                        .Select()
                                        .C("tpr", "TopicRowID")
                                        .C("ru", "UserID")
                                        .From("FmTopicParticipantRoles", "tpr").NoLock()
                                        .InnerJoin(RoleStrings.RoleUsers, "ru").NoLock()
                                        .On().C("tpr", "RoleID").Equals().C("ru", "ID")
                                        .Where()
                                        .C("tpr", "TopicRowID").Equals().C("msg", "TopicRowID");

                                    if (unionAdditionalRowsAction is not null)
                                    {
                                        t.UnionAll();
                                        unionAdditionalRowsAction(t);
                                    }
                                }, "t");
                        
                        subQueryExpression?.Invoke(q);
                    },
                    "usr")
                .InnerJoin(RoleStrings.Roles, "r").NoLock()
                .On()
                .C("r", "ID").Equals().C("usr", "UserID")
                // пока пользователь не открыл обсуждение в карточке, в таблице FmUserStat для него отсутствует строка,
                // но почтовые уведомления мы хотим отправлять сразу после подписки - поэтому LEFT JOIN
                .LeftJoin("FmUserStat", "fst").NoLock()
                .On()
                .C("tp", "RowID").Equals().C("fst", "TopicRowID")
                .And()
                .C("usr", "UserID").Equals().C("fst", "UserID");
            return this;
        }

        /// <inheritdoc />
        public virtual ITopicQueryBuilder AppendWhere(Action<IQueryBuilder> andAdditionalConditionsAction = null)
        {
            this.QueryBuilder.Where()
                .C("msg", "Created").LessOrEquals().P("CurrentPluginRunDateTime")
                .And()
                .C("msg", "Created").Greater().P("LastPluginRunDateTime")
                .And(q => q
                    .C("msg", "Created").Greater().C("fst", "LastReadMessageTime")
                    .Or()
                    .C("fst", "LastReadMessageTime").IsNull());

            if (andAdditionalConditionsAction is not null)
            {
                this.QueryBuilder.And(andAdditionalConditionsAction);
            }
            
            return this;
        }

        /// <inheritdoc />
        public virtual ITopicQueryBuilder AppendOrderBy()
        {
            this.QueryBuilder.OrderBy("usr", "UserID").By("usr", "TopicRowID").By("msg", "Created");
            return this;
        }

        /// <inheritdoc />
        public virtual ITopicQueryBuilder Append(Action<IQueryBuilder> expression)
        {
            Check.ArgumentNotNull(expression, nameof(expression));
            
            expression(this.QueryBuilder);
            return this;
        }

        #endregion
    }
}
