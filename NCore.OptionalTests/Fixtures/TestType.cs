using NCore.Optional;

namespace NCore.OptionalTests.Fixtures
{
  public class TestType
  {
    public int Id { get; set; }
    public Option<TestType> Recursive { get; set; }
    public Option<string> Value { get; set; }

    public override string ToString()
    {
      var value = Value ? Value.Unwrap("") : "None";
      return $"{Id}:{value}";
    }
  }
}