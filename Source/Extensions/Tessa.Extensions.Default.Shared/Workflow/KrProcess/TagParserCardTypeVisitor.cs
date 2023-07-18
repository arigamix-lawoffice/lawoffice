using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Помощник переноса тэгов в формате [Tag1],..,[TagN] из имён блоков и контролов в
    /// настройки блока/контрола (TagsListSetting). Также удаляет тэги из имени элемента.
    /// </summary>
    public class TagParserCardTypeVisitor : CardTypeVisitor
    {
        private const string Primary = "p";
        private const string Secondary = "s";

        private const string TagRegex = @"\[((?<" + Primary + @">[a-zA-Z\d\s]+)(,(?<" + Secondary + @">[a-zA-Z\d\s]+))*)\]";


        /// <inheritdoc cref="CardTypeVisitor(IValidationResultBuilder)"/>
        public TagParserCardTypeVisitor(
            IValidationResultBuilder validationResult = null)
            : base(validationResult)
        {
        }

        /// <inheritdoc />
        public override async ValueTask VisitBlockAsync(
            CardTypeBlock block,
            CardTypeForm form,
            CardType type,
            CancellationToken cancellationToken = default)
        {
            ParseTags(block);
        }

        /// <inheritdoc />
        public override async ValueTask VisitControlAsync(
            CardTypeControl control,
            CardTypeBlock block,
            CardTypeForm form,
            CardType type,
            CancellationToken cancellationToken = default)
        {
            ParseTags(control);
        }

        private static void ParseTags(
            CardTypeBlock block)
        {
            if (string.IsNullOrWhiteSpace(block.Name))
            {
                return;
            }

            block.Name = ParseTags(block.Name, block.BlockSettings);
        }

        private static void ParseTags(
            CardTypeControl control)
        {
            if (string.IsNullOrWhiteSpace(control.Name))
            {
                return;
            }

            control.Name = ParseTags(control.Name, control.ControlSettings);
        }

        private static string ParseTags(string name, ISerializableObject settings)
        {
            var tagsList = new List<string>();
            string pureName = Regex.Replace(
                name,
                TagRegex,
                m => OnMatching(m, tagsList),
                RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Compiled
            );
            settings[KrConstants.Ui.TagsListSetting] = tagsList;
            return pureName.Trim();
        }

        private static string OnMatching(Match match, List<string> tagsList)
        {
            tagsList.Add(match.Groups[Primary].Value);
            foreach (var capture in match.Groups[Secondary].Captures)
            {
                tagsList.Add(capture.ToString().Trim());
            }

            return string.Empty;
        }
    }
}
