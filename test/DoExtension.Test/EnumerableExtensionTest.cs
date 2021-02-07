namespace DoExtension.Test
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class EnumerableExtensionTest
    {
        [Fact]
        public void IsEmptyTest()
        {
            List<object> nullList = null;
            var emptyList = new List<object>();
            var notEmptyList = new List<object>() { 1, 2 };

            nullList.IsEmpty().Should().Be(true);
            emptyList.IsEmpty().Should().Be(true);
            notEmptyList.IsEmpty().Should().Be(false);
        }

        [Fact]
        public void IsInTest()
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6 };

            var isInParamsTrue = 1.IsIn(1, 2, 3, 4, 5, 6);
            var isInEnumerableTrue = 1.IsIn(list);

            var isInParamsFalse = 7.IsIn(1, 2, 3, 4, 5, 6);
            var isInEnumerableFalse = 7.IsIn(list);

            isInParamsTrue.Should().Be(true);
            isInEnumerableTrue.Should().Be(true);
            isInParamsFalse.Should().Be(false);
            isInEnumerableFalse.Should().Be(false);
        }

        [Fact]
        public void JoinTest()
        {
            var list = new List<int> { 1, 2, 3 };

            var join1 = list.Join();
            var join2 = list.Join("|");

            join1.Should().Be("1, 2, 3");
            join2.Should().Be("1|2|3");
        }

        [Fact]
        public void GetValueTest()
        {
            var dic = new Dictionary<int, string>
            {
                { 1,"a" },
                { 2,"b" },
                { 3,"c" },
            };

            var val = dic.GetValue(1);
            Action action = () => dic.GetValue(4);
            val.Should().Be("a");
            action.Should().Throw<KeyNotFoundException>().WithMessage("Cannot find key 4");
        }

        [Fact]
        public void GetValueOrDefaultTest()
        {
            var dic = new Dictionary<int, string>
            {
                { 1,"a" },
                { 2,"b" },
                { 3,"c" },
            };

            var val1 = dic.GetValueOrDefault(1);
            var val2 = dic.GetValueOrDefault(4);
            val1.Should().Be("a");
            val2.Should().Be(null);
        }
    }
}
