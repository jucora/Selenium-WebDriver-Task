# EPAM Career & Global Search Automation

Test automation project for the EPAM website using Selenium WebDriver with C# and NUnit.

## 🕵️ Author

Julian Andres Belmonte Ortiz

## 📋 Description

This project contains end-to-end automated tests to validate two main functionalities of the EPAM website:

1. **Career Search**: Search for job positions based on specific criteria (keyword, location, remote option)
2. **Global Search**: Global website search with result validation

## 🛠️ Technologies Used

- **C# / .NET**
- **Selenium WebDriver 4.x**
- **NUnit Framework**
- **SeleniumExtras.WaitHelpers**
- **ChromeDriver / FirefoxDriver**

## 📁 Project Structure

```
CareerSearchAutomation/
├── Drivers/
│   └── WebDriverFactory.cs          # Factory for WebDriver creation
├── Helpers/
│   ├── BaseTest.cs                  # Base class with Setup/TearDown
│   ├── CareerSearchTestHelper.cs    # Helper methods for Career Search
│   └── GlobalSearchTestHelper.cs    # Helper methods for Global Search
├── Locators/
│   ├── CareerSearchLocator.cs       # Locators for Career Search
│   └── GlobalSearchLocator.cs       # Locators for Global Search
└── Tests/
    ├── CareerSearchTests.cs         # Career search tests
    └── GlobalSearchTests.cs         # Global search tests
```

## 🚀 Installation

### Prerequisites

- .NET SDK 6.0 or higher
- Visual Studio 2022 / Visual Studio Code / Rider
- Chrome or Firefox browser installed

### Required NuGet Packages

```bash
dotnet add package Selenium.WebDriver
dotnet add package Selenium.WebDriver.ChromeDriver
dotnet add package Selenium.WebDriver.GeckoDriver
dotnet add package NUnit
dotnet add package NUnit3TestAdapter
dotnet add package DotNetSeleniumExtras.WaitHelpers
```

## ⚙️ Configuration

### Change Browser

In `BaseTest.cs`, modify the line:

```csharp
driver = WebDriverFactory.Create("chrome"); // Change to "firefox" if needed
```

### Adjust Wait Times

Modify the timeout in `BaseTest.cs`:

```csharp
wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // Adjust as needed
```

## 📝 Test Cases

### Career Search Tests

Validates that a user can search for job positions based on:
- **Keyword**: Python, Java, C#
- **Location**: All Locations
- **Modality**: Remote

**Test Cases:**
```csharp
[TestCase("Python")]
[TestCase("Java")]
[TestCase("C#")]
```

### Global Search Tests

Validates that the global site search returns relevant results:

**Test Cases:**
```csharp
[TestCase("BLOCKCHAIN")]
[TestCase("Cloud")]
[TestCase("Automation")]
```

## 🏃 Running Tests

### From Visual Studio
1. Open Test Explorer (View → Test Explorer)
2. Click "Run All"

### From Command Line

```bash
# Run all tests
dotnet test

# Run specific tests
dotnet test --filter "FullyQualifiedName~CareerSearchTests"
dotnet test --filter "FullyQualifiedName~GlobalSearchTests"

# Run with verbose output
dotnet test --logger "console;verbosity=detailed"
```

### Using NUnit Console

```bash
nunit3-console.exe YourProject.dll
```

## 🔍 Key Features

### WebDriverFactory Pattern
- Support for multiple browsers (Chrome, Firefox)
- Centralized driver configuration
- Automatic window maximization

### Page Object Model (POM)
- Separation of locators in dedicated classes
- Reusable helper methods
- Simplified maintenance

### Explicit Waits
- WebDriverWait with configurable timeout
- ExpectedConditions for greater stability
- Reduction of flaky tests

### Inheritance and Reusability
- BaseTest class with common Setup/TearDown
- Specialized helpers per functionality
- DRY (Don't Repeat Yourself) code

## ⚠️ Known Issues

### Assertion Logic Issue

In `GlobalSearchTestHelper.cs`, the current validation checks that **NOT all** links contain the keyword:

```csharp
Assert.That(allContainKeyword, Is.False, // should be true?
```

**Recommended fix:** Change to `Is.True` to correctly validate that all results contain the searched keyword.

## 🐛 Troubleshooting

### WebDriver not found
```bash
# Reinstall the corresponding driver
dotnet add package Selenium.WebDriver.ChromeDriver --version [latest]
```

### Elements not found
- Verify that CSS/XPath selectors are correct
- Increase the WebDriverWait timeout
- Validate that the website structure hasn't changed

### Intermittently failing tests
- Increase explicit wait times
- Check internet connection stability
- Verify there are no popups or modals blocking elements

## 📄 License

This project is for educational and demonstration purposes.

## 👥 Contributing

Contributions are welcome. Please:
1. Fork the project
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📧 Contact

For questions or suggestions about this project, please open an issue in the repository.

---

**Note**: This project is designed for testing and learning purposes. Make sure to comply with EPAM's website terms of use when running the tests.
