namespace CreditCheck.UnitTest.CommonTest
{
    using CreditCheck.Common.Extensions;
    using CreditCheck.Models;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class ExtensionsTest
    {
        public static IEnumerable<object[]> TestData
            => new object[][] {
                new object[] { DateTime.Now.AddYears(-20), 20 },
                new object[] { DateTime.Now.AddYears(-18), 18 },
                new object[] { DateTime.Now, 0 },
                new object[] { null, DateTime.Now.Year-1 }
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public void ValidDOB_WithExpectedValue(DateTime actual, int expected)
        {
            Assert.Equal(expected, actual.ToGetAge());
        }


    }
}