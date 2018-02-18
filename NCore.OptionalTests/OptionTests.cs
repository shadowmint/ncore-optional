using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCore.Optional;
using NCore.OptionalTests.Fixtures;
using Xunit;

namespace NCore.OptionalTests
{
  public class OptionTests
  {
    [Fact]
    public void TestOperators()
    {
      var instance = new TestType();
      if (instance.Value)
      {
        throw new Exception("Unreachable code hit");
      }

      instance.Value = Option.Some("Hello");
      if (!instance.Value)
      {
        throw new Exception("Unreachable code hit");
      }

      var x = "";
      if (instance.Value)
      {
        x = instance.Value.Unwrap(x);
      }
      Assert.Equal("Hello", x);
    }

    [Fact]
    public void TestLinq()
    {
      var collection = new List<TestType>();
      for (var i = 0; i < 10; i++)
      {
        collection.Add(new TestType()
        {
          Id = i,
          Value = Option.Some("Some value")
        });
      }
      foreach (var testType in collection.Where(i => i.Id % 3 == 0))
      {
        testType.Recursive = Option.Some(new TestType()
        {
          Value = Option.Some($"Hi-{testType.Id}")
        });
      }

      var matches = collection
        .Where(i => i.Recursive.Unwrap(() => new TestType()).Value.Some)
        .Select(i => i.Recursive.Unwrap(() => new TestType()).Value.Unwrap("Invalid"));
      Assert.Equal(string.Join(" ", matches), "Hi-0 Hi-3 Hi-6 Hi-9");

      var parents = collection
        .Where(i => i.Recursive.Unwrap(() => new TestType()).Value.Unwrap("").StartsWith("Hi"));
      Assert.Equal(string.Join(" ", parents), "0:Some value 3:Some value 6:Some value 9:Some value");
    }

    [Fact]
    public void TestExtensions()
    {
      Assert.True("Hello".Some().Some);
      Assert.True(100.Some().Some);
      Assert.True(1000.0.Some().Some);
      Assert.True(100f.Some().Some);
      Assert.True(false.Some().Some);
      Assert.True(true.Some().Some);
      Assert.True('x'.Some().Some);
      Assert.True(DateTime.Now.Some().Some);
      Assert.True(new Action(() => { }).Some().Some);
      Assert.True(new Action<int>(a => { }).Some().Some);
      Assert.True(new Func<int>(() => 0).Some().Some);
      Assert.True(Task.Run(() => { }).Some().Some);
      Assert.True(Task.Run(() => 0).Some().Some);
    }
  }
}