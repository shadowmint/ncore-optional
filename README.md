# NCore.Optional

A generic Option type for C#

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
