# QA Automation Projects

This repository contains two NUnit test projects targeting .NET 8:

- **UIAutomation** – browser based tests using Playwright
- **APIAutomation** – REST API tests using RestSharp

## Prerequisites

- [.NET SDK 8](https://dotnet.microsoft.com/download)
- Playwright browsers. Install once with:
  ```
  dotnet tool install --global Microsoft.Playwright.CLI
  playwright install
  ```

## Running Tests

### VS Code

Use **F5** to run all tests. The provided `launch.json` invokes `dotnet test` on the solution.

### CLI

Execute all tests from a terminal:

```bash
cd <repo>
dotnet test
```

## Structure

```
QATests.sln          Solution file
UIAutomation/        Playwright UI tests
APIAutomation/       REST API tests
```

Each project can be executed independently with `dotnet test` in its directory.
