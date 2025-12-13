Feature: Career Search
  As a job seeker
  I want to search job listings by keyword
  So that I can find positions matching my skills

  @careers @search
  Scenario Outline: Validate user can search positions based on criteria
    Given the user is on the Careers page
    When the user enters the keyword "<keyword>"
    And the user selects remote option
    And the user selects a location
    And the user clicks Find Jobs
    And the user opens the last job result
    Then the job description should contain the keyword "<keyword>"

    Examples:
      | keyword |
      | Python  |
      | Java    |
      | C#      |
