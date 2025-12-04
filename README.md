# EPAM Search Automation Framework

A comprehensive Selenium-based test automation framework for testing search functionality, career portal, and content management features on the EPAM website.

## 👥 Authors

- Julian Andres Belmonte Ortiz

## 🚀 Features

- **Page Object Model (POM)** architecture for maintainable test code
- **Component-based** design for reusable UI elements (Navbar, Cookies)
- **Multi-browser support** (Chrome and Firefox)
- **Explicit waits** for reliable element interactions
- **Parameterized tests** using NUnit
- **File download validation**
- **Custom validators** for assertion logic

## 📋 Test Scenarios

### 1. Global Search Tests

Validates that users can search for keywords across the website and verify search results contain the expected terms.

- Test cases: BLOCKCHAIN, Cloud, Automation

### 2. Career Search Tests

Tests the career portal functionality including keyword search, location filtering, and remote job options.

- Test cases: Python, Java, C#

### 3. Download Function Tests

Verifies that PDF downloads work correctly from the About page.

### 4. Article Title Consistency Tests

Ensures that carousel slide titles match the corresponding article titles when navigating from Insights.

## 🛠️ Technology Stack

- **Language**: C# (.NET)
- **Testing Framework**: NUnit
- **Automation Tool**: Selenium WebDriver
- **Browser Drivers**: ChromeDriver, GeckoDriver (Firefox)
- **Additional Libraries**:
  - SeleniumExtras.WaitHelpers
  - DotNetSeleniumExtras.WaitHelpers

## 📁 Project Structure

```
SearchAutomation/
├── Business/
│    ├── Components/
│    │    ├── CookiesComponent.cs
│    │    └── NavbarComponent.cs
│    ├── Pages/
│    │    ├── BasePage.cs
│    │    ├── CareersPage.cs
│    │    ├── SearchPage.cs
│    │    ├── InsightsPage.cs
│    │    ├── AboutPage.cs
│    │    └── JobListingsPage.cs
├── Core/
│    ├── Configuration/
│    │    ├── ConfigurationManager.cs
│    │    └── IConfiguration.cs
│    ├── Enums/
│    │    ├── BrowserType.cs
│    │    └── Environment.cs
│    ├── Logging/
│    │    ├── Ilogger.cs
│    │    └── Logger.cs
│    ├── Utilities/
│    │    ├── DownloadsPath.cs
│    │    ├── FileUtil.cs
│    │    ├── ScreenshotHelper.cs
│    │    └── WaitHelper.cs
│    ├── WebDriver/
│    │    ├── BrowserFactory.cs
│    │    ├── DriverManager.cs
│    │    └── IBrowserFactory.cs
└── Test/
     ├── Configuration/
     │    ├── appsettings.Development.json
     │    ├── appsettings.json
     │    ├── appsettings.Production.json
     │    └── appsettings.Staging.json
     ├── ArticleTitleConsistencyTests.cs
     ├── BaseTest.cs
     ├── CareerSearchTests.cs
     ├── DownloadFunctionTests.cs
     ├── GlobalSearchTests.cs
     └── NLog.config

```

## 🔧 Setup Instructions

### Prerequisites

- .NET 6.0 or higher
- Chrome or Firefox browser installed
- NuGet package manager

### Installation

1. Clone the repository:

```bash
git clone <repository-url>
cd SearchAutomation
```

2. Restore NuGet packages:

```bash
dotnet restore
```

3. Build the project:

```bash
dotnet build
```

## ▶️ Running Tests

### Run all tests:

```bash
dotnet test
```

### Run specific test class:

```bash
dotnet test --filter "FullyQualifiedName~GlobalSearchTests"
```

### Run specific test case:

```bash
dotnet test --filter "Name~ValidateUserCanSearchBasedOnCriteria"
```

### Change browser:

In `BaseTest.cs`, modify the Setup method:

```csharp
driver = WebDriverFactory.Create(BrowserType.Firefox); // Change to Chrome or Firefox
```

## 📝 Key Components

### BaseTest

- Initializes WebDriver with browser configuration
- Sets up explicit waits (20 seconds default)
- Handles cookie acceptance
- Manages driver lifecycle (setup/teardown)

### ElementActions

Core methods for interacting with web elements:

- `Click(By locator)` - Click on element
- `Type(By locator, string text)` - Enter text into input field
- `GetText(By locator)` - Retrieve element text
- `WaitForElement(By locator)` - Wait for element to be clickable

### WebDriverFactory

Creates WebDriver instances with custom configurations:

- Chrome: Configured download directory, disabled prompts
- Firefox: Custom profile with anti-detection preferences

## 🔍 Test Examples

### Global Search Test

```csharp
[TestCase("BLOCKCHAIN")]
public void ValidateUserCanSearchBasedOnCriteria(string keyword)
{
    navbar
        .ClickMagnifierIcon()
        .EnterSearchKeyword(keyword);

    SearchPage searchPage = navbar.ClickFindButton();
    var searchResults = searchPage.GetSearchResults(keyword);

    GlobalSearchValidator.ValidateLinkTexts(searchResults, keyword);
}
```

### Career Search Test

```csharp
[TestCase("Python")]
public void ValidateUserCanSearchPositionBasedOnCriteria(string keyword)
{
    CareersPage careersPage = navbar.ClickCareersLink();
    careersPage
        .EnterKeyword(keyword)
        .SelectLocation()
        .SelectRemoteOption();

    JobListingsPage jobListingsPage = careersPage.ClickFindButton();
    jobListingsPage.SelectViewAndApplyFromLastResult();

    CareerSearchValidator.ValidateKeywordIsPresent(keyword, driver.PageSource);
}
```

## 📦 Downloads

Downloaded files are stored in the `Downloads` folder at the project root. The framework automatically:

- Creates the download directory if it doesn't exist
- Waits for file downloads to complete
- Validates successful downloads

## ⚙️ Configuration

### Wait Times

Explicit wait is set to 20 seconds in `BaseTest.cs`:

```csharp
wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
```

### Download Timeout

File download timeout is 10 seconds by default in `FileUtil.WaitForFileToDownload()`.

## 🐛 Known Issues

- `GlobalSearchValidator.ValidateLinkTexts()` has inverted assertion logic (should be `Is.True`)
- Some carousel transitions require hardcoded 2-second waits

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License.

## 🙏 Acknowledgments

- EPAM Systems website for test scenarios
- Selenium WebDriver community
- NUnit testing framework
