# QA Automation Projects

This repo demonstrates a lean but powerful automation framework for UI and API testing with **NUnit**, **Playwright** and **RestSharp**.

## Prerequisites

- [.NET SDK 8](https://dotnet.microsoft.com/download)
- Playwright browsers. Install once:
  ```bash
  dotnet tool install --global Microsoft.Playwright.CLI
  playwright install
  ```

## Running Tests

### VS Code
Press **F5** to execute all tests. The provided `launch.json` runs `dotnet test` on the solution.

### CLI
```bash
cd <repo>
dotnet test
```

Tests from both projects run in parallel thanks to `[assembly: Parallelizable]` and isolated Playwright contexts.

## Error Artifacts

On UI test failure a screenshot and page source are saved under `TestResults/<TestName>/`. Logs are written to the NUnit output window.

## Extending Tests

- Use the hooks in `PlaywrightTestBase` and `ApiTestBase` to customise browser/context options or add headers/authentication.
- Add additional test classes under `UIAutomation/Tests` and `APIAutomation/Tests`.
- Utilities live under `Common/Utils`.

## Structure
```
QATests.sln            Solution file
UIAutomation/          Playwright UI tests
  Infra/               Base classes
  Tests/               Example tests
APIAutomation/         REST API tests
  Infra/               Base classes
  Tests/               Example tests
Common/                Shared utilities
.vscode/               VS Code configuration
```
