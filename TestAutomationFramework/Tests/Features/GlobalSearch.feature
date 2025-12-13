Feature: Global Search
  As a user
  I want to search using the magnifier icon
  So that I can see relevant results for my keyword

  @search @global
  Scenario Outline: Validate user can search based on criteria
    Given the user opens the global search
    When the user searches for "<keyword>"
    Then all search results should contain "<keyword>"

    Examples:
      | keyword    |
      | BLOCKCHAIN |
      | Cloud      |
      | Automation |
