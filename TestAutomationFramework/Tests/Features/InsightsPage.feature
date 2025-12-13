Feature: Insights Carousel
  As a visitor of the Insights page
  I want carousel slides to match their article titles
  So that content is consistent

  @insights @carousel
  Scenario: Slide title matches the opened article title
    Given the user is on the Insights page
    When the user swipes the carousel
    And the user opens the article via Read More
    Then the article title should match the slide title
