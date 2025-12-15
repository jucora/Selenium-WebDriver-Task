# EPAM Test Automation Framework

A robust and scalable **UI Test Automation Framework** built with **C#, Selenium WebDriver, NUnit, and Reqnroll (BDD)** for validating key functionalities of the EPAM website, including search, careers, downloads, and services navigation.

This framework is designed following **Clean Code**, **SOLID principles**, and **industry best practices** for maintainable and extensible test automation.

---

## 🕵️ Author

**[Julian Andres Belmonte Ortiz](https://www.julianbelmonte.com/)**

---

## 📁 Project Structure

```
|   .gitignore
|   BDDFramework.sln
|   README.md
|
\---TestAutomationFramework
    |   NLog.config
    +---Business
    |   +---Components
    |   |       CookiesComponent.cs
    |   |       NavbarComponent.cs
    |   |
    |   \---Pages
    |       |   AboutPage.cs
    |       |   BasePage.cs
    |       |   CareersPage.cs
    |       |   InsightsPage.cs
    |       |   JobListingsPage.cs
    |       |   SearchPage.cs
    |       |
    |       \---Services
    |               AIServiceBasePage.cs
    |               GenerativeAIPage.cs
    |               ResponsibleAIPage.cs
    |
    +---Core
    |   +---Configuration
    |   |       ConfigurationManager.cs
    |   |       IConfiguration.cs
    |   |
    |   +---Enums
    |   |       BrowserType.cs
    |   |       Environment.cs
    |   |
    |   +---Logging
    |   |       Ilogger.cs
    |   |       Logger.cs
    |   |
    |   +---Utilities
    |   |       DownloadsPath.cs
    |   |       FileUtil.cs
    |   |       ScreenshotHelper.cs
    |   |       WaitHelper.cs
    |   |
    |   \---WebDriver
    |           BrowserFactory.cs
    |           DriverManager.cs
    |           IBrowserFactory.cs
    |
    +---Tests
        |   ArticleTitleConsistencyTests.cs
        |   BaseTest.cs
        |   CareerSearchTests.cs
        |   DownloadFunctionTests.cs
        |   GlobalSearchTests.cs
        |
        +---Configuration
        |       appsettings.Development.json
        |       appsettings.json
        |       appsettings.Production.json
        |       appsettings.Staging.json
        |
        +---Context
        |       UiTestContext.cs
        |
        +---Features
        |       CareerSearch.feature
        |       CareerSearch.feature.cs
        |       DownloadFunction.feature
        |       DownloadFunction.feature.cs
        |       GlobalSearch.feature
        |       GlobalSearch.feature.cs
        |       InsightsPage.feature
        |       InsightsPage.feature.cs
        |       ServicesPage.feature
        |       ServicesPage.feature.cs
        |
        +---Hooks
        |       ReqnrollDriverHooks.cs
        |
        \---Steps
                CareerSearchSteps.cs
                DownloadFunctionSteps.cs
                GlobalSearchSteps.cs
                InsightsPageSteps.cs
                ServicesPageSteps.cs
```

## 🚀 Key Features

- **Page Object Model (POM)** architecture
- **BDD support using Reqnroll** (SpecFlow-compatible)
- **Component-based design** for reusable UI elements (Navbar, Cookies, etc.)
- **Hybrid testing strategy**
  - Classic NUnit tests
  - BDD scenarios with Gherkin
- **Multi-browser execution** (Chrome & Firefox)
- **Explicit waits and custom wait helpers**
- **File download validation**
- **Centralized WebDriver lifecycle management**
- **Custom validators for assertions**
- **Structured logging with NLog**
- **Screenshot capture on failure**
- **Environment-based configuration** (Dev / Staging / Prod)

---

## 🥒 BDD with Reqnroll

This framework supports **Behavior Driven Development (BDD)** using **Reqnroll**, enabling collaboration between QA engineers, developers, and business stakeholders through **Gherkin scenarios**.

### BDD Stack

- **Reqnroll**
- **Gherkin syntax**
- **NUnit integration**
- **Reqnroll Hooks & Context**

### BDD Folder Structure

```text
Tests/
├── Features        # Gherkin feature files (.feature)
├── Steps           # Step Definitions (Given / When / Then)
├── Hooks           # Scenario hooks (BeforeScenario / AfterScenario)
├── Context         # Scenario-level shared context
```

### Example BDD Scenario

```gherkin
Feature: Global Search

  Scenario Outline: User searches for a keyword
    Given the user is on the EPAM home page
    When the user searches for "<keyword>"
    Then search results should contain "<keyword>"

    Examples:
      | keyword      |
      | BLOCKCHAIN   |
      | Cloud        |
      | Automation   |
```

---

## 🧩 Test Strategy

The framework follows a **hybrid testing approach** combining classic NUnit tests with BDD scenarios to balance execution speed, readability, and maintainability.

---

## 🔄 WebDriver Lifecycle (BDD)

- Browser initialized in `BeforeScenario`
- Screenshot captured on failure
- Browser disposed in `AfterScenario`

```text
Feature → Scenario → Hook → Step → Page Object
```

---

## 🛠️ Technology Stack

- **Language**: C# (.NET 8)
- **Automation Tool**: Selenium WebDriver
- **Test Framework**: NUnit
- **BDD Framework**: Reqnroll
- **Browsers**: Chrome, Firefox
- **Logging**: NLog

---

## 🔧 Setup Instructions

```bash
git clone <repository-url>
cd TestAutomationFramework
dotnet restore
dotnet build
```

---

## ▶️ Running Tests

```bash
dotnet test
```

---

## 📄 License

This project is licensed under the **MIT License**.
