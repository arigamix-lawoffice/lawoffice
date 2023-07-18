using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Platform.Data;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Console.MigrateFiles
{
    public sealed class MigrationTask
    {
        #region Constructors

        public MigrationTask(
            List<FileVersionInfo> versionInfoList,
            CardFileSourceType fromSourceTypeID,
            ICardContentStrategy fromSourceContentStrategy,
            CardFileSourceType toSourceTypeID,
            ICardContentStrategy toSourceContentStrategy,
            IDbScope dbScope,
            int[] completedCount,
            int[] errorCount,
            int taskIndex,
            bool removeFromTargetFileSource,
            Func<ValidationResult, Task> logResultActionAsync)
        {
            this.versionInfoList = versionInfoList;
            this.fromSourceTypeID = fromSourceTypeID;
            this.fromSourceContentStrategy = fromSourceContentStrategy;
            this.toSourceTypeID = toSourceTypeID;
            this.toSourceContentStrategy = toSourceContentStrategy;
            this.dbScope = dbScope;
            this.completedCount = completedCount;
            this.errorCount = errorCount;
            this.taskIndex = taskIndex;
            this.removeFromTargetFileSource = removeFromTargetFileSource;
            this.logResultActionAsync = logResultActionAsync;
        }

        #endregion

        #region Fields

        private readonly List<FileVersionInfo> versionInfoList;

        private readonly CardFileSourceType fromSourceTypeID;

        private readonly ICardContentStrategy fromSourceContentStrategy;

        private readonly CardFileSourceType toSourceTypeID;

        private readonly ICardContentStrategy toSourceContentStrategy;

        private readonly IDbScope dbScope;

        private readonly int[] completedCount;

        private readonly int[] errorCount;

        private readonly int taskIndex;

        private readonly bool removeFromTargetFileSource;

        private readonly Func<ValidationResult, Task> logResultActionAsync;

        #endregion

        #region Methods

        public async Task RunAsync(CancellationToken cancellationToken = default)
        {
            var validationResult = new ValidationResultBuilder();
            await using (this.dbScope.Create())
            {
                DbManager db = this.dbScope.Db;

                DataParameter[] parameters = new DataParameter[2];

                DataParameter versionParameter = db.Parameter("RowID", null, DataType.Guid);
                parameters[0] = versionParameter;
                parameters[1] = db.Parameter("SourceID", (short) this.toSourceTypeID.ID, DataType.Int16);

                string updateSourceText = this.dbScope.BuilderFactory
                    .Update("FileVersions")
                    .C("SourceID").Assign().P("SourceID")
                    .Where().C("RowID").Equals().P("RowID")
                    .Build();

                foreach (FileVersionInfo versionInfo in this.versionInfoList)
                {
                    CardContentContext fromContentContext = versionInfo.CreateContext(this.fromSourceTypeID, validationResult);
                    CardContentContext toContentContext = versionInfo.CreateContext(this.toSourceTypeID, validationResult);

                    try
                    {
                        // удаляем контент в целевом местоположении (есть смысл, если мы выполняем повторный перенос контента после того, как его прервали)
                        if (this.removeFromTargetFileSource)
                        {
                            await this.toSourceContentStrategy.DeleteAsync(toContentContext, cancellationToken);
                        }

                        // копируем содержимое контента версии в целевое местоположение
                        await using (Stream contentStream = await this.fromSourceContentStrategy.GetAsync(fromContentContext, cancellationToken))
                        {
                            if (contentStream != null && validationResult.IsSuccessful())
                            {
                                await this.toSourceContentStrategy.StoreAsync(toContentContext, contentStream, cancellationToken);
                            }
                        }

                        if (validationResult.IsSuccessful())
                        {
                            // выполняем UPDATE для того, чтобы в базе файл смотрел на новое хранилище (в нём контент уже есть 100%)
                            versionParameter.Value = versionInfo.VersionRowID;

                            await db
                                .SetCommand(updateSourceText, parameters)
                                .ExecuteNonQueryAsync(cancellationToken);

                            // удаляем контент в старом хранилище
                            await this.fromSourceContentStrategy.DeleteAsync(fromContentContext, cancellationToken);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        // любые ошибки логируются, но не прерывают перенос контента
                        validationResult.AddException(this, ex);
                    }

                    if (validationResult.HasData())
                    {
                        if (!validationResult.IsSuccessful())
                        {
                            Interlocked.Increment(ref this.errorCount[this.taskIndex]);
                        }

                        await this.logResultActionAsync(validationResult.Build());
                        validationResult.Clear();
                    }

                    // увеличиваем количество завершённых переносов контента (необязательно успешных)
                    int incremented = Interlocked.Increment(ref this.completedCount[this.taskIndex]);
                    if (incremented < 0)
                    {
                        // асинхронная отмена
                        break;
                    }
                }
            }
        }

        #endregion
    }
}