# Description

A demo project to demonstrate test automation in ASP.NET Core back ends.

## Requirements

* Dotnet Core SDK 3.1

## How to build

1. Due to a problem in SpecFlow you'll have to set the environment variable MSBUILDSINGLELOADCONTEXT to 1 
   (see [Github Issue](https://github.com/SpecFlowOSS/SpecFlow/issues/1912)): `export MSBUILDSINGLELOADCONTEXT=1
2. Go to to tests project: `cd Tests`
3. Run the tests: `dotnet test`
