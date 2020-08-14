using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTest
{
    // This is example and also template class that I made to be tested by using Xunit
    public class TemplateTests
    {
        // Methods below are based from TemplateMethods class
        // and you always could add or customize your own method there

        // Use [Fact] if you only use a sample within a function
        [Fact]
        public void BubbleTest1()
        {
            var data = new List<int>()
            {
                1, 2, 23, 12, 1, 23, 7
            };
            var result = new List<int>()
            {
                1, 1, 2, 7, 12, 23, 23
            };
            var equal = result.SequenceEqual(TemplateMethods.BubbleSort(data));
            Assert.True(equal);
        }

        [Fact]
        public void BubbleTest2()
        {
            var data = new List<int>()
            {
                1, 2, 0, 12, 12, 23, 7
            };
            var result = new List<int>()
            {
                1, 1, 0, 2, 7, 12, 23, 12
            };
            var equal = result.SequenceEqual(TemplateMethods.BubbleSort(data));
            Assert.False(equal);
        }

        // If you have many data to be tested from a method (or maybe pipeline)
        // Use [Theory] and [InlineData] as the example below
        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        [InlineData(9, false)]
        [InlineData(13, true)]
        [InlineData(23, true)]
        public void SqrtMethodTest(int n, bool res)
        {
            Assert.Equal(TemplateMethods.SqrtMethodPrime(n), res);
        }
    }
}
