namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет статические методы расширения для <see cref="TestActionOptions"/>.
    /// </summary>
    public static class TestActionOptionsExtensions
    {
        /// <doc path='info[@type="flags" and @item="Has"]'/>
        public static bool Has(this TestActionOptions flags, TestActionOptions flag) => (flags & flag) == flag;

        /// <doc path='info[@type="flags" and @item="HasAny"]'/>
        public static bool HasAny(this TestActionOptions flags, TestActionOptions flag) => (flags & flag) != 0;

        /// <doc path='info[@type="flags" and @item="HasNot"]'/>
        public static bool HasNot(this TestActionOptions flags, TestActionOptions flag) => (flags & flag) == 0;
    }
}
