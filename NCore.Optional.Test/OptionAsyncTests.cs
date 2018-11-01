using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NCore.Optional.Test
{
  public class OptionAsyncTests
  {
    [Fact]
    public async Task TestAsyncUnwrap()
    {
      var option = Option.None<int>();
      var value = await option.UnwrapAsync(() => Task.FromResult(1));
      Assert.Equal(1, value);

      value = await option.UnwrapAsync(GetSomeIntAsync(2));
      Assert.Equal(2, value);
    }

    [Fact]
    public async Task TestAsyncUnwrap__WithNoErrors__ReturnsFirstMatchingOption()
    {
      Assert.Equal("value:A:one", await GetSomeValue("one"));
    }

    [Fact]
    public async Task TestAsyncUnwrap__WithTwoErrors__ReturnsMatchingOption()
    {
      Assert.Equal("value:C:AB-two", await GetSomeValue("AB-two"));
    }

    [Fact]
    public async Task TestAsyncUnwrap__WithThreeErrors__ReturnsDefaultOption()
    {
      Assert.Equal("DEFAULT", await GetSomeValue("ABC-error"));
    }

    [Fact]
    public async Task TestFirstOrDefaultAsync__WithThreeErrors__ReturnsDefaultOption()
    {
      Assert.Equal("DEFAULT", await GetSomeValueEnumerable("ABC-error"));
    }

    [Fact]
    public async Task TestFirstOrDefaultAsync__WithNoErrors__ReturnsFirstMatchingOption()
    {
      Assert.Equal("value:A:one", await GetSomeValueEnumerable("one"));
    }

    [Fact]
    public async Task TestFirstOrDefaultAsync__WithTwoErrors__ReturnsMatchingOption()
    {
      Assert.Equal("value:C:AB-two", await GetSomeValueEnumerable("AB-two"));
    }

    private Task<string> GetSomeValueEnumerable(string key)
    {
      return Option.FirstOrDefaultAsync("DEFAULT",
        () => GetSomeValueFromA(key),
        () => GetSomeValueFromB(key),
        () => GetSomeValueFromC(key));
    }

    private async Task<string> GetSomeValue(string key)
    {
      return await (await GetSomeValueFromA(key))
        .UnwrapAsync(async () => await (await GetSomeValueFromB(key))
          .UnwrapAsync(async () => (await GetSomeValueFromC(key))
            .Unwrap("DEFAULT")));
    }

    private async Task<Option<string>> GetSomeValueFromC(string key)
    {
      try
      {
        return (await DoAsyncOperation(key, "C")).Some();
      }
      catch (Exception)
      {
        return Option.None<string>();
      }
    }

    private async Task<Option<string>> GetSomeValueFromB(string key)
    {
      try
      {
        return (await DoAsyncOperation(key, "B")).Some();
      }
      catch (Exception)
      {
        return Option.None<string>();
      }
    }

    private async Task<Option<string>> GetSomeValueFromA(string key)
    {
      try
      {
        return (await DoAsyncOperation(key, "A")).Some();
      }
      catch (Exception)
      {
        return Option.None<string>();
      }
    }

    private async Task<string> DoAsyncOperation(string key, string context)
    {
      return await Task.Run(() =>
      {
        Thread.Sleep(10);
        if (key.Contains(context))
        {
          throw new Exception("Nope!");
        }

        return $"value:{context}:{key}";
      });
    }

    private Task<int> GetSomeIntAsync(int i)
    {
      return Task.FromResult(i);
    }
  }
}