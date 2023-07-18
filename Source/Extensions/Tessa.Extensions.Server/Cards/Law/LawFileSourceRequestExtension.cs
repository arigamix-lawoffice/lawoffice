using System;
using Tessa.Cards.Extensions;
using Tessa.Cards;
using System.Threading.Tasks;
using System.Linq;
using Tessa.Extensions.Shared.Info;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Server.Cards.Law
{
    /// <summary>
    /// Расширение, устанавливающее файловое хранилище для добавленного файла при сохранении карточки.
    /// </summary>
    public sealed class LawFileSourceRequestExtension : CardRequestExtension
    {
        private readonly ICardFileSourceSettings sourceSettings;
        private readonly IDbScope dbScope;

        public LawFileSourceRequestExtension(
            ICardFileSourceSettings sourceSettings,
            IDbScope dbScope)
        {
            this.sourceSettings = sourceSettings;
            this.dbScope = dbScope;
        }

        /// <inheritdoc />
        public override async Task AfterRequest(ICardRequestExtensionContext context)
        {
            if (!context.RequestIsSuccessful || context.Response is null)
            {
                return;
            }

            CardFile file = context.Request.GetFile();
            string? path;
            await using (this.dbScope.CreateNew(ExtSchemeInfo.ConnectionString))
            {
                var query = this.dbScope.BuilderFactory
                    .Select()
                    .Top(1)
                    .C(ExtSchemeInfo.ShrambaDatotek.Lokacija)
                    .From(ExtSchemeInfo.ShrambaDatotek).NoLock()
                    .Where().C(ExtSchemeInfo.ShrambaDatotek.Privzeta).Equals().Value(1)
                    .Limit(1)
                    .Build();
                path = await this.dbScope.Db
                    .SetCommand(query)
                    .LogCommand()
                    .ExecuteAsync<string>(context.CancellationToken);
            }

            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            var arigamixPath = path.Replace(@"\\192.71.244.117\LawOffice2\", "/share/", StringComparison.OrdinalIgnoreCase);
            var sources = await this.sourceSettings.GetAllAsync(context.CancellationToken);
            var fileSource = sources.FirstOrDefault(s => string.Equals(s.Path, arigamixPath, StringComparison.OrdinalIgnoreCase));
            if (fileSource is null)
            {
                return;
            }

            context.Response.SetFileSource(fileSource.Type);
        }
    }
}
