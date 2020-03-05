Feature: Adding participants to projects
    As a project manager, I want to add participants to a project.

    Scenario: Add Participant
        Given there is one project "WebAPI Demo"
        Given there is a participant "Alex"
        When I add "Alex" to "WebAPI Demo"
        Then the project "WebAPI Demo" contains a participant named "Alex"