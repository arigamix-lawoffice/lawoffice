using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Platform.Shared.Cards;
using Tessa.FileConverters;
using Tessa.Platform.IO;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Files
{
    /// <summary>
    /// Расширение на конвертацию виртуальных файлов из справочника "Виртуальных файлов" при запросе с конвертацией (например, предпросмотр в ЛК).
    /// Своё расширение необходимо, т.к. конвертация виртуальных файлов данного типа должна происходить без долговременного кеширования.
    /// </summary>
    public sealed class KrVirtualFileConvertGetContentExtension : CardGetFileContentExtension
    {
        #region Fields

        private readonly IFileConverter fileConverter;
        private readonly ISignedSessionTokenProvider sessionTokenProvider;

        #endregion

        #region Constructors

        public KrVirtualFileConvertGetContentExtension(IFileConverter fileConverter, ISignedSessionTokenProvider sessionTokenProvider)
        {
            this.fileConverter = fileConverter;
            this.sessionTokenProvider = sessionTokenProvider;
        }

        #endregion

        #region Base Overrides

        public override async Task BeforeRequest(ICardGetFileContentExtensionContext context)
        {
            CardGetFileContentRequest request = context.Request;
            FileConverterFormat? converterFormat = request.TryGetConverterFormat();

            Guid? cardID;
            Guid? fileID;
            Guid? versionID;
            string fileName;

            if (context.Response != null
                || !converterFormat.HasValue
                || converterFormat.Value == FileConverterFormat.Unknown
                || !(cardID = request.CardID).HasValue
                || !(fileID = request.FileID).HasValue
                || !(versionID = request.VersionRowID).HasValue
                || string.IsNullOrEmpty(fileName = request.FileName))
            {
                return;
            }

            var converterRequest = new FileConverterRequest
            {
                EventName = FileConverterEventNames.ClientPreview,
                OutputFormat = converterFormat.Value,
                FileName = fileName,
                CardID = cardID.Value,
                FileID = fileID.Value,
                VersionID = versionID.Value,
                FileTypeID = request.FileTypeID,
                FileTypeName = request.FileTypeName,
                Flags = FileConverterRequestFlags.IgnoreCacheBeforeConversion,
            };

            converterRequest.Parameters["KrVirtualFileSpecialID"] = Guid.NewGuid();
            SignedSessionToken signedToken = await this.sessionTokenProvider.CreateTokenAsync(context.Session.Token, modifyTokenAction: token =>
            {
                token.Info[FileTemplateHelper.SkipFileTemplatePermissionsCheck] = true;
            }, cancellationToken: context.CancellationToken);
            signedToken.Set(converterRequest.FileRequestInfo);

            IFileConverterResponse converterResponse = await this.fileConverter.ConvertFileAsync(converterRequest, context.CancellationToken);
            bool hasContent = converterResponse.ValidationResult.IsSuccessful;

            var response = new CardGetFileContentResponse { HasContent = hasContent };
            response.ValidationResult.Add(converterResponse.ValidationResult);
            context.Response = response;

            if (hasContent)
            {
                string extension = "." + converterFormat.Value.GetExtension();
                if (extension.Length <= 1)
                {
                    extension = FileHelper.GetExtension(fileName);
                }

                string suggestedFileName =
                    FileHelper.RemoveInvalidFileNameChars(
                        FileHelper.GetFileNameWithoutExtension(fileName, true),
                        FileHelper.InvalidCharReplacement)
                    + extension;

                response.SetSuggestedFileName(suggestedFileName);
                response.Size = converterResponse.Size;

                context.ContentFuncAsync = converterResponse.GetStreamOrThrowAsync;
            }
        }

        #endregion
    }
}
