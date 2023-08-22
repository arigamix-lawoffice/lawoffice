using System;
using System.IO;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Shared.Info;
using Tessa.FileConverters;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.IO;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Server.Files
{
    /// <summary>
    /// The extension that creates the file conversion operation.
    /// </summary>
    public sealed class LawConverterFileContentExtension :
        CardGetFileContentExtension
    {
        #region Constructors

        public LawConverterFileContentExtension(IFileConverter fileConverter) =>
            this.fileConverter = fileConverter;

        #endregion

        #region Fields

        private readonly IFileConverter fileConverter;

        #endregion

        #region Base Overrides

        public override async Task BeforeRequest(ICardGetFileContentExtensionContext context)
        {
            CardGetFileContentRequest request = context.Request;
            FileConverterFormat? converterFormat = request.TryGetConverterFormat();

            Guid? cardID;
            Guid? fileID;
            string? fileName;
            var path = request.Info.Get<string>(InfoMarks.Path);

            if (context.Response != null
                || !converterFormat.HasValue
                || converterFormat.Value == FileConverterFormat.Unknown
                || !(cardID = request.CardID).HasValue
                || !(fileID = request.FileID).HasValue
                || string.IsNullOrEmpty(fileName = request.FileName)
                || string.IsNullOrEmpty(path))
            {
                return;
            }

            DateTime lastWriteTime;

            try
            {
                var fileFullName = Path.Combine(path, fileID.Value.ToString(), fileName);
                if (!File.Exists(fileFullName))
                {
                    var message = string.Format(LocalizationManager.Localize(LocalizationInfo.UICommonFileNotFound), fileFullName);
                    context.Response = new CardGetFileContentResponse();
                    context.Response.ValidationResult.AddError(this, message);
                    return;
                }

                lastWriteTime = File.GetLastWriteTime(fileFullName);
            }
            catch (Exception e)
            {
                context.Response = new CardGetFileContentResponse();
                context.Response.ValidationResult.Add(ValidationResult.FromException(this, e));
                return;
            }

            // The file versions change every time the card is updated,
            // since versions are not stored in the old system and when the file is changed,
            // it is overwritten. In order to store and then read files in the cache of converted files,
            // we create a version identifier based on the file ID and the date of the last file change.
            var versionID = (fileID.Value.ToString() + lastWriteTime.Ticks.ToString()).ToGuid();
            var converterRequest = new FileConverterRequest
            {
                EventName = FileConverterEventNames.ClientPreview,
                OutputFormat = converterFormat.Value,
                FileName = fileName,
                CardID = cardID.Value,
                FileID = fileID.Value,
                VersionID = versionID,
                FileTypeID = request.FileTypeID,
                FileTypeName = request.FileTypeName
            };
            converterRequest.FileRequestInfo[InfoMarks.Path] = path;

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