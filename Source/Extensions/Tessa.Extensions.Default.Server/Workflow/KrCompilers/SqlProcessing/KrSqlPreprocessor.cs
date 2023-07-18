using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tessa.Platform;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing
{
    /// <inheritdoc cref="IKrSqlPreprocessor"/>
    public sealed class KrSqlPreprocessor : IKrSqlPreprocessor
    {
        #region Constants

        // Директивы, которые необходимо искать в коде
        private const string StageRowIDDirective = "stage_rowid";
        private const string StageTypeIDDirective = "stage_type_id";
        private const string StageTemplateIDDirective = "stage_template_id";
        private const string StageGroupIDDirective = "stage_group_id";
        private const string SecondaryProcessIDDirective = "sec_proc_id";
        private const string UserIDDirective = "user_id";
        private const string UserNameDirective = "user_name";
        private const string CardIDDirective = "card_id";
        private const string CardTypeIDDirective = "card_type_id";
        private const string DocTypeIDDirective = "doc_type_id";
        private const string TypeIDDirective = "type_id";
        private const string DocumentStateDirective = "doc_state";

        // Для регулярки
        private const string DirectiveGroup = "directive";
        private const string ParamsGroup = "params";
        private const string RegexStr =
            "#(?<" + DirectiveGroup + ">\\w+)((\\s*\\((?<" + ParamsGroup + ">.{1,})\\))|\\b)";

        private delegate string DirectiveProcessingFunc(string[] parameters);

        #endregion

        #region Fields

        private readonly Dictionary<string, DirectiveProcessingFunc> directiveProcessingFunctions;

        private IKrSqlExecutorContext context;

        /// <summary>
        /// Актуальное значение userID. Берется из ISession и может быть подменено,
        /// если идентификатор пользователя указан в контексте.
        /// </summary>
        private Guid userID;

        /// <summary>
        /// Актуальное значение userName. Берется из ISession и может быть подменено,
        /// если имя пользователя указано в контексте.
        /// </summary>
        private string userName;

        private Dictionary<string, object> sqlParameters;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrSqlPreprocessor"/>.
        /// </summary>
        /// <param name="session">Сессия пользователя.</param>
        public KrSqlPreprocessor(ISession session)
        {
            Check.ArgumentNotNull(session, nameof(session));

            this.userID = session.User.ID;
            this.userName = session.User.Name;

            this.directiveProcessingFunctions =
                new Dictionary<string, DirectiveProcessingFunc>(StringComparer.Ordinal)
                {
                    {StageRowIDDirective, parameters => this.AddVariable(StageRowIDDirective, this.context.StageRowID)},
                    {StageTypeIDDirective, parameters => this.AddVariable(StageTypeIDDirective, this.context.StageTypeID)},
                    {StageTemplateIDDirective, parameters => this.AddVariable(StageRowIDDirective, this.context.StageTemplateID)},
                    {StageGroupIDDirective, parameters => this.AddVariable(StageGroupIDDirective, this.context.StageGroupID)},
                    {SecondaryProcessIDDirective, parameters => this.AddVariable(SecondaryProcessIDDirective, this.context.SecondaryProcess?.ID)},
                    {UserIDDirective, parameters => this.AddVariable(UserIDDirective, this.userID)},
                    {UserNameDirective, parameters => this.AddVariable(UserNameDirective, this.userName)},
                    {CardIDDirective, parameters => this.AddVariable(CardIDDirective, this.context.CardID)},
                    {CardTypeIDDirective, parameters => this.AddVariable(CardTypeIDDirective, this.context.CardTypeID)},
                    {DocTypeIDDirective, parameters => this.AddVariable(DocTypeIDDirective, this.context.DocTypeID)},
                    {TypeIDDirective, parameters => this.AddVariable(TypeIDDirective, this.context.TypeID)},
                    {DocumentStateDirective, parameters => this.AddVariable(DocumentStateDirective, (int?)this.context.State)},
                };
        }

        #endregion

        #region DirectiveProcessing

        private string AddVariable(string name, object value)
        {
            if (!this.sqlParameters.ContainsKey(name))
            {
                this.sqlParameters.Add(name, value);
            }
            return '@' + name;
        }

        #endregion

        #region on matching

        private string OnMatching(Match match)
        {
            var directive = match.Groups[DirectiveGroup].Value;
            try
            {
                var splittedParameters = match.Groups[ParamsGroup].Value.Split(',');
                return this.directiveProcessingFunctions[directive](splittedParameters);
            }
            catch (KeyNotFoundException)
            {
                var text = this.context.GetErrorTextFunc(
                    this.context,
                    "$KrProcess_ErrorMessage_SqlPreprocessorDirectiveNotFound",
                    new object[] { directive });

                throw new QueryExecutionException(text, this.context.Query);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                var errorText = this.context.GetErrorTextFunc(
                    this.context,
                    "$KrProcess_ErrorMessage_SqlPreprocessorGeneral",
                    Array.Empty<object>());

                throw new QueryExecutionException(errorText, this.context.Query, e);
            }
        }

        #endregion

        #region IKrSqlPreprocessor Members

        /// <inheritdoc />
        public IKrSqlPreprocessorResult Preprocess(IKrSqlExecutorContext ctx)
        {
            Check.ArgumentNotNull(ctx, nameof(ctx));

            this.context = ctx;
            if (this.context.UserID.HasValue
                && this.context.UserName is not null)
            {
                this.userID = this.context.UserID.Value;
                this.userName = this.context.UserName;
            }

            this.sqlParameters = new Dictionary<string, object>(StringComparer.Ordinal);

            var newQuery = Regex.Replace(
                this.context.Query,
                RegexStr,
                this.OnMatching,
                RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Compiled);

            return new KrSqlPreprocessorResult(
                newQuery,
                this.sqlParameters.ToList());
        }

        #endregion
    }
}