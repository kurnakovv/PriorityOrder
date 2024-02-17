using PriorityOrder.UnitTests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PriorityOrder.UnitTests
{
    public class PriorityEnumerableTests
    {
        private IEnumerable<Driver> _source = new List<Driver>
        {
            new Driver() { Name = "Bob", CategoryString = "Second category", CategoryEnum = DriverCategory.SecondCategory, CategoryInt = -10002, },
            new Driver() { Name = "Huxley", CategoryString = "First category", CategoryEnum = DriverCategory.FirstCategory, CategoryInt = 345230001, },
            new Driver() { Name = "Tom", CategoryString = "Third category", CategoryEnum = DriverCategory.ThirdCategory, CategoryInt = 3430003, },
            new Driver() { Name = "Sutton", CategoryString = "Third category", CategoryEnum = DriverCategory.ThirdCategory, CategoryInt = 3430003, },
            new Driver() { Name = "Lev", CategoryString = "First category", CategoryEnum = DriverCategory.FirstCategory, CategoryInt = 345230001,  },
            new Driver() { Name = "Max", CategoryString = "Second category", CategoryEnum = DriverCategory.SecondCategory, CategoryInt = -10002, },
            new Driver() { Name = "Willow", CategoryString = "None", CategoryEnum = DriverCategory.None, CategoryInt = 0, },
            new Driver() { Name = "Jack", CategoryString = "Third category", CategoryEnum = DriverCategory.ThirdCategory, CategoryInt = 3430003, },
            new Driver() { Name = "Ruby", CategoryString = "Second category", CategoryEnum = DriverCategory.SecondCategory, CategoryInt = -10002, },
            new Driver() { Name = "Willow", CategoryString = "First category", CategoryEnum = DriverCategory.FirstCategory, CategoryInt = 345230001,  },
        };

        [Fact]
        public void OrderByPriority_CannotOrderWhenParametersIsNull_ArgumentNullException()
        {
            IEnumerable<string>? nullSource = null;
            Func<string, string>? nullKeySelector = null;
            IEnumerable<string>? nullPriorities = null;

            Assert.Throws<ArgumentNullException>(() => nullSource.OrderByPriority(x => x, new List<string>() { "Test" }));
            Assert.Throws<ArgumentNullException>(() => new List<string>() { "Test" }.OrderByPriority(nullKeySelector, new List<string>() { "Test" }));
            Assert.Throws<ArgumentNullException>(() => new List<string>() { "Test" }.OrderByPriority(x => x, nullPriorities));
        }

        [Fact]
        public void OrderedByPriority_CannotOrderWhenPrioritiesContainDuplicateValues_ArgumentException()
        {
            var prioritiesWithDuplicateValues = new List<string> { "Test", "Test" };

            Assert.Throws<ArgumentException>(() => new List<string>() { "Test" }.OrderByPriority(x => x, prioritiesWithDuplicateValues));
        }

        [Fact]
        public void OrderByPriority_CanOrderByStringPriorities_OrderedEnumerable()
        {
            IEnumerable<string> source = new List<string> { "SUPER LOW", "LOW", "HIGH", "SUPER SUPER LOW", "MEDIUM" };
            IEnumerable<string> priorities = new List<string> { "HIGH", "MEDIUM", "LOW" };

            List<string> result = source.OrderByPriority(x => x, priorities).ToList();

            Assert.Equal("HIGH", result[0]);
            Assert.Equal("MEDIUM", result[1]);
            Assert.Equal("LOW", result[2]);
            Assert.Equal("SUPER LOW", result[3]);
            Assert.Equal("SUPER SUPER LOW", result[4]);
        }

        [Fact]
        public void OrderByPriority_CanOrderByStringProperty_OrderedEnumerable()
        {
            IEnumerable<string> priorities = new List<string>() 
            { 
                "First category", 
                "Second category", 
                "Third category" 
            };

            var result = _source.OrderByPriority(x => x.CategoryString, priorities).ToList();

            Assert.True(result.Take(3).All(x => x.CategoryString == "First category"));
            Assert.True(result.Skip(3).Take(3).All(x => x.CategoryString == "Second category"));
            Assert.True(result.Skip(6).Take(3).All(x => x.CategoryString == "Third category"));
            Assert.True(result.Last().CategoryString == "None");
        }

        [Fact]
        public void OrderByPriority_CanOrderByEnumPriorities_OrderedEnumerable()
        {
            IEnumerable<DriverCategory> source = new List<DriverCategory> 
            { 
                DriverCategory.ThirdCategory, 
                DriverCategory.FirstCategory, 
                DriverCategory.None, 
                DriverCategory.SecondCategory, 
            };
            IEnumerable<DriverCategory> priorities = new List<DriverCategory> 
            { 
                DriverCategory.FirstCategory, 
                DriverCategory.SecondCategory, 
                DriverCategory.ThirdCategory 
            };

            var result = source.OrderByPriority(x => x, priorities).ToList();

            Assert.Equal(DriverCategory.FirstCategory, result[0]);
            Assert.Equal(DriverCategory.SecondCategory, result[1]);
            Assert.Equal(DriverCategory.ThirdCategory, result[2]);
            Assert.Equal(DriverCategory.None, result[3]);
        }

        [Fact]
        public void OrderByPriority_CanOrderByEnumProperty_OrderedEnumerable()
        {
            IEnumerable<DriverCategory> priorities = new List<DriverCategory>()
            {
                DriverCategory.FirstCategory,
                DriverCategory.SecondCategory,
                DriverCategory.ThirdCategory
            };

            var result = _source.OrderByPriority(x => x.CategoryEnum, priorities).ToList();

            Assert.True(result.Take(3).All(x => x.CategoryEnum == DriverCategory.FirstCategory));
            Assert.True(result.Skip(3).Take(3).All(x => x.CategoryEnum == DriverCategory.SecondCategory));
            Assert.True(result.Skip(6).Take(3).All(x => x.CategoryEnum == DriverCategory.ThirdCategory));
            Assert.True(result.Last().CategoryEnum == DriverCategory.None);
        }

        [Fact]
        public void OrderByPriority_CanOrderByIntPriorities_OrderedEnumerable()
        {
            IEnumerable<int> source = new List<int> { 3430003, 0, 345230001, -10002, };
            // Let's imagine, that you want to order by "000_NUMBER".
            IEnumerable<int> priorities = new List<int> { 345230001, -10002, 3430003, };

            List<int> result = source.OrderByPriority(x => x, priorities).ToList();

            Assert.Equal(345230001, result[0]);
            Assert.Equal(-10002, result[1]);
            Assert.Equal(3430003, result[2]);
            Assert.Equal(0, result[3]);
        }

        [Fact]
        public void OrderByPriority_CanOrderByIntProperty_OrderedEnumerable()
        {
            // Let's imagine, that you want to order by "000_NUMBER".
            IEnumerable<int> priorities = new List<int> { 345230001, -10002, 3430003, };

            var result = _source.OrderByPriority(x => x.CategoryInt, priorities).ToList();

            Assert.True(result.Take(3).All(x => x.CategoryInt == 345230001));
            Assert.True(result.Skip(3).Take(3).All(x => x.CategoryInt == -10002));
            Assert.True(result.Skip(6).Take(3).All(x => x.CategoryInt == 3430003));
            Assert.True(result.Last().CategoryInt == 0);
        }
    }
}
