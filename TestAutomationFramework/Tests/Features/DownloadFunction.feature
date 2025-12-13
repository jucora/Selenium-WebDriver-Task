Feature: Download Function
  As a user
  I want to download the corporate overview document
  So that I can verify the download functionality works correctly

  @download @about
  Scenario: Validate download function from About page
    Given the user is on the About page
    When the user clicks the download button
    Then the file "EPAM_Corporate_Overview_Sept_25.pdf" should be downloaded
