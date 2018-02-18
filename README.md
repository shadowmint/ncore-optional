# NCore.Optional

A generic Option type for C#

# Usage

    using NCore.Optional;

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

    var parents = collection
      .Where(i => i.Recursive.Unwrap(() => new TestType()).Value.Unwrap("").StartsWith("Hi"));

You can also use extension methods for many built in types:

      "Hello".Some()
      100.Some()
      1000.0.Some()
      100f.Some()
      false.Some()
      true.Some()
      'x'.Some()
      DateTime.Now.Some()
      new Action(() => { }).Some()
      new Action<int>(a => { }).Some()
      new Func<int>(() => 0).Some()
      Task.Run(() => { }).Some()
      Task.Run(() => 0).Some()

# Installing

    npm install --save shadowmint/ncore-optional

Now add the `NuGet.Config` to the project folder:

    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
     <packageSources>
        <add key="local" value="./packages" />
     </packageSources>
    </configuration>

Now you can install the package:

    dotnet add package ncore-optional

You may also want to use `dotnet nuget locals all --clear` to clear cached objects.

# Building a new package version

    npm run build

# Testing

    npm test
