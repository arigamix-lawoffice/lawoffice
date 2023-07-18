using System;

namespace Tessa.Extensions.Default.Console.ImportUsers
{
    public abstract class TimeZoneObject
    {
        #region Public Methods

        public void SetZoneInfo(string value)
        {
            this.ZoneInfoString = value;
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            if (OperationHelper.TimeZoneOffsetRegex.IsMatch(value))
            {
                if (value.StartsWith("+", StringComparison.Ordinal))
                {
                    this.ZoneOffset = (int) TimeSpan.Parse(value.Replace("+", string.Empty, StringComparison.Ordinal)).TotalMinutes;
                }
                else
                {
                    this.ZoneOffset = -1 * (int) TimeSpan.Parse(value.Replace("-", string.Empty, StringComparison.Ordinal)).TotalMinutes;
                }
            }
            else if (value == "-1")
            {
                this.IsInherit = true;
            }
            else
            {
                this.ZoneName = value;
            }
        }

        #endregion

        #region Properties

        public string ZoneInfoString { get; private set; }

        public string ZoneName { get; private set; }

        public int? ZoneOffset { get; private set; }

        public bool IsInherit { get; private set; }

        #endregion
    }
}