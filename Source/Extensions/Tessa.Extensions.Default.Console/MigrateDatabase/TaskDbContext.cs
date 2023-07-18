using Tessa.Platform.Collections;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Console.MigrateDatabase
{
    public sealed class TaskDbContext
    {
        #region Constructors

        public TaskDbContext(
            DbManager sourceDb,
            IQueryBuilderFactory sourceBuilderFactory,
            Dbms sourceDbms,
            DbManager targetDb,
            IQueryBuilderFactory targetBuilderFactory,
            Dbms targetDbms,
            IBulkInsertExecutor insertExecutor,
            int bulkSize)
        {
            this.SourceDb = sourceDb;
            this.SourceBuilderFactory = sourceBuilderFactory;
            this.SourceDbms = sourceDbms;
            this.TargetDb = targetDb;
            this.TargetBuilderFactory = targetBuilderFactory;
            this.TargetDbms = targetDbms;
            this.InsertExecutor = insertExecutor;
            this.BulkSize = bulkSize;

            // capacity - это количество колонок в таблицах, который вряд ли будет больше указанного числа
            this.ValueArrayPool = new ObjectPool<object[]>(() => new object[this.BulkSize], capacity: 32);
        }

        #endregion

        #region Properties

        public DbManager SourceDb { get; }

        public IQueryBuilderFactory SourceBuilderFactory { get; }

        // ReSharper disable once MemberCanBePrivate.Local
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public Dbms SourceDbms { get; }

        public DbManager TargetDb { get; }

        public IQueryBuilderFactory TargetBuilderFactory { get; }

        // ReSharper disable once MemberCanBePrivate.Local
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public Dbms TargetDbms { get; }

        public IBulkInsertExecutor InsertExecutor { get; }

        public int BulkSize { get; }

        public ObjectPool<object[]> ValueArrayPool { get; }

        #endregion
    }
}