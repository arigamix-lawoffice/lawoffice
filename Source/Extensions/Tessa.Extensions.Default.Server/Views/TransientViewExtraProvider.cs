#nullable enable
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Roles;
using Tessa.Scheme;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Types;

namespace Tessa.Extensions.Default.Server.Views
{
    /// <summary>
    /// Провайдер для тестового представления <c>TransientViewExample</c>.
    /// </summary>
    public sealed class TransientViewExtraProvider :
        IExtraViewProvider
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его зависимостей.
        /// </summary>
        /// <param name="schemeTypeConverter"><inheritdoc cref="ISchemeTypeConverter" path="/summary"/></param>
        /// <param name="dbScope"><inheritdoc cref="IDbScope" path="/summary"/></param>
        public TransientViewExtraProvider(
            ISchemeTypeConverter schemeTypeConverter,
            IDbScope dbScope)
        {
            this.schemeTypeConverter = NotNullOrThrow(schemeTypeConverter);
            this.dbScope = NotNullOrThrow(dbScope);
        }

        #endregion

        #region TransientView Private Class

        private sealed class TransientView :
            ITessaViewAccess,
            ITessaView
        {
            #region Static Fields

            private static readonly Role[] roles =
            {
                new()
                {
                    // 132b2a3c-7bc9-4a0f-8805-91adc5b0fe46
                    ID = new Guid(0x132b2a3c, 0x7bc9, 0x4a0f, 0x88, 0x05, 0x91, 0xad, 0xc5, 0xb0, 0xfe, 0x46),
                    Name = "IT"
                }
            };

            #endregion

            #region Constructors

            public TransientView(ISchemeTypeConverter schemeTypeConverter, IDbScope dbScope)
            {
                this.schemeTypeConverter = schemeTypeConverter;
                this.dbScope = dbScope;

                this.metadata = new ViewMetadata { Alias = "TransientViewExample", Caption = "View example", };
                this.metadata.Parameters.Add(
                    new ViewParameterMetadata { Alias = "Name", Caption = "Caption", SchemeType = SchemeType.String });
                this.metadata.Parameters.Add(
                    new ViewParameterMetadata { Alias = "Count", Caption = "Quantity", SchemeType = SchemeType.Int32 });
            }

            #endregion

            #region Fields

            private readonly ISchemeTypeConverter schemeTypeConverter;

            private readonly IDbScope dbScope;

            private readonly IViewMetadata metadata;

            #endregion

            #region Private Methods

            private static IList<object?> GetRows(ITessaViewRequest request)
            {
                var result = new List<object?>();

                var count = request.GetFirstParameterValue<int>("Count");
                var name = request.GetFirstParameterValue<string>("Name");

                for (var i = 0; i < count; i++)
                {
                    result.Add(new List<object?> { $"{name}: {i}", i });
                }

                return result;
            }

            #endregion

            #region ITessaView Members

            /// <inheritdoc cref="ITessaView.GetMetadataAsync" />
            public ValueTask<IViewMetadata> GetMetadataAsync(CancellationToken cancellationToken = default) =>
                new(this.metadata);

            /// <inheritdoc/>
            public async Task<ITessaViewResult> GetDataAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
            {
                Dbms dbms = await this.dbScope.GetDbmsAsync(cancellationToken);

                return new TessaViewResult
                {
                    Columns = new List<object> { "Name", "Count" },
                    Rows = GetRows(request),
                    SchemeTypes = new[] { SchemeType.String, SchemeType.Int32 },
                    DataTypes = new List<object?>
                    {
                        this.schemeTypeConverter.TryGetSqlTypeName(SchemeType.String, dbms),
                        this.schemeTypeConverter.TryGetSqlTypeName(SchemeType.Int32, dbms),
                    },
                };
            }

            #endregion

            #region ITessaViewAccess Members

            /// <inheritdoc/>
            public ValueTask<IReadOnlyList<INamedEntry>> GetRolesAsync(CancellationToken cancellationToken = default) => new(roles);

            #endregion
        }

        #endregion

        #region Fields

        private readonly ISchemeTypeConverter schemeTypeConverter;

        private readonly IDbScope dbScope;

        private ITessaView? cachedView;

        #endregion

        #region IExtraViewProvider Members

        /// <inheritdoc/>
        public ValueTask<ITessaView?> TryGetExtraViewAsync(CancellationToken cancellationToken = default) =>
            new(this.cachedView ??= new TransientView(this.schemeTypeConverter, this.dbScope));

        #endregion
    }
}
