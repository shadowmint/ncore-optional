using System;
using System.Threading.Tasks;

namespace NCore.Optional
{
  public static class OptionExtensions
  {
    public static Option<string> Some(this string value)
    {
      return OrSomething(value);
    }

    public static Option<bool> Some(this bool value)
    {
      return OrSomething(value);
    }

    public static Option<byte> Some(this byte value)
    {
      return OrSomething(value);
    }

    public static Option<sbyte> Some(this sbyte value)
    {
      return OrSomething(value);
    }

    public static Option<short> Some(this short value)
    {
      return OrSomething(value);
    }

    public static Option<ushort> Some(this ushort value)
    {
      return OrSomething(value);
    }

    public static Option<int> Some(this int value)
    {
      return OrSomething(value);
    }

    public static Option<uint> Some(this uint value)
    {
      return OrSomething(value);
    }

    public static Option<long> Some(this long value)
    {
      return OrSomething(value);
    }

    public static Option<ulong> Some(this ulong value)
    {
      return OrSomething(value);
    }

    public static Option<IntPtr> Some(this IntPtr value)
    {
      return OrSomething(value);
    }

    public static Option<UIntPtr> Some(this UIntPtr value)
    {
      return OrSomething(value);
    }

    public static Option<char> Some(this char value)
    {
      return OrSomething(value);
    }

    public static Option<double> Some(this double value)
    {
      return OrSomething(value);
    }

    public static Option<float> Some(this float value)
    {
      return OrSomething(value);
    }

    public static Option<DateTime> Some(this DateTime value)
    {
      return OrSomething(value);
    }

    public static Option<Action> Some(this Action value)
    {
      return OrSomething(value);
    }

    public static Option<Action<T>> Some<T>(this Action<T> value)
    {
      return OrSomething(value);
    }

    public static Option<Action<T1, T2>> Some<T1, T2>(this Action<T1, T2> value)
    {
      return OrSomething(value);
    }

    public static Option<Task> Some(this Task value)
    {
      return OrSomething(value);
    }

    public static Option<Task<T>> Some<T>(this Task<T> value)
    {
      return OrSomething(value);
    }

    public static Option<Func<T>> Some<T>(this Func<T> value)
    {
      return OrSomething(value);
    }

    public static Option<Func<T, T1>> Some<T, T1>(this Func<T, T1> value)
    {
      return OrSomething(value);
    }

    public static Option<Func<T, T1, T2>> Some<T, T1, T2>(this Func<T, T1, T2> value)
    {
      return OrSomething(value);
    }

    private static Option<T> OrSomething<T>(T value)
    {
      return value == null ? Option.None<T>() : Option.Some(value);
    }
  }
}