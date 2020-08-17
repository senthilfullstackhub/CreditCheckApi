namespace CreditCheck.UnitTest.MockData
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public static class ObjectExtensions
    {
        public static Task<List<T>> AsListTask<T>(this T value)
            => new List<T> { value }.AsTask();

        public static Task<T> AsTask<T>(this T value)
            => Task.FromResult(value);
    }
}
