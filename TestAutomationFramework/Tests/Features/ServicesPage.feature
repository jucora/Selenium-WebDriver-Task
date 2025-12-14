Feature: Services Page
  As a website user
  I want to navigate to different service categories
  So that I can view the corresponding service information

  @navigation @services
  Scenario Outline: Validate navigation to a specific Services category
    When the user navigates to the "Services" section
    And the user selects the "<ServiceCategory>" category
    Then the page title should contain "<ExpectedTitle>"
    And the "Our Related Expertise" section should be displayed

    Examples:
      | ServiceCategory   | ExpectedTitle        |
      | Generative AI     | Generative AI        |
      | Responsible AI    | Responsible AI       |
