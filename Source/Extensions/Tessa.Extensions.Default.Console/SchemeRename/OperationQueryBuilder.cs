#nullable enable
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Tessa.Platform;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Console.SchemeRename
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public sealed class OperationQueryBuilder :
        IQueryBuilder
    {
        #region Fields

        private readonly StringBuilder builder = new();

        #endregion

        #region Constructors

        public OperationQueryBuilder(Dbms dbms) => this.Dbms = dbms;

        #endregion

        #region Private Methods

        private string GetDebuggerDisplay() => $"{DebugHelper.GetTypeName(this)}: \"{this}\"";

        #endregion

        #region Base Overrides

        public override string ToString() => this.builder.ToString();

        #endregion

        #region IStringBuilderProvider Members

        StringBuilder IStringBuilderProvider.GetStringBuilder() => this.builder;

        #endregion

        #region IQueryBuilder Members

        public Dbms Dbms { get; }

        public IQueryBuilder Append(string? value)
        {
            this.builder.Append(value);
            this.OnAppended();
            return this;
        }

        public IQueryBuilder Append([InterpolatedStringHandlerArgument("")] ref AppendInterpolatedStringProviderHandler handler)
        {
            // executed after already appended to string builder
            this.OnAppended();
            return this;
        }

        public IQueryBuilder Append(IFormatProvider? provider, [InterpolatedStringHandlerArgument("", "provider")] ref AppendInterpolatedStringProviderHandler handler)
        {
            // executed after already appended to string builder
            this.OnAppended();
            return this;
        }

        public void OnAppended()
        {
            this.RequiresComma = false;

            if (this.builder.Length > 0)
            {
                var ch = this.builder[^1];
                this.RequiresWhitespace = ch != '(' && !char.IsWhiteSpace(ch);
            }
        }

        public IQueryBuilder IncreaseIndent()
        {
            this.Indent++;
            return this;
        }

        public IQueryBuilder DecreaseIndent()
        {
            if (this.Indent > 0)
            {
                this.Indent--;
            }

            return this;
        }

        public int Indent { get; private set; }

        public bool RequiresWhitespace { get; private set; }

        public IQueryBuilder RequireComma(bool value = true)
        {
            this.RequiresComma = value;
            return this;
        }

        public bool RequiresComma { get; private set; }

        string IQueryBuilder.Build() => this.builder.ToString();

        void IQueryBuilder.Clear() => this.builder.Clear();

        #endregion
    }
}
