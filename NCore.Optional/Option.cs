using System;

namespace NCore.Optional
{
  public class Option
  {
    public static Option<T> Some<T>(T value)
    {
      return new Option<T>(value);
    }

    public static Option<T> None<T>()
    {
      return new Option<T>();
    }
  }

  public struct Option<T>
  {
    private readonly T _value;

    public Option(T value)
    {
      _value = value;
      Some = true;
    }

    public bool Some { get; private set; }

    public bool None => !Some;

    public T Unwrap(T fallback)
    {
      return Some ? _value : fallback;
    }

    /// <summary>
    /// Sometimes its convenient to use a factory function to generate a value. 
    /// </summary>
    public T Unwrap(Func<T> fallback)
    {
      return Some ? _value : fallback();
    }

    public static bool operator true(Option<T> option)
    {
      return option.Some;
    }

    public static bool operator false(Option<T> option)
    {
      return option.None;
    }

    public static bool operator !(Option<T> option)
    {
      return option.None;
    }
  }
}