# VeterinaryJournalSystem

```mermaid
classDiagram
    class StaffUser {
        string Id
        string FullName
        string StaffCode
    }

    class Owner {
        string Id
        string FullName
        string PhoneNumber
        string PersonalNumber
        string Comment
    }

    class Pet {
        string Id
        string Name
        string Species
        string Breed
        DateTime DateOfBirth
        bool IsInsured
        string CurrentMedications
        string Allergies
        string OwnerId
    }

    class Visit {
        string Id
        DateTime ScheduledAt
        string ReasonForVisit
        string Symptoms
        string Examination
        string Diagnosis
        string Treatment
        string VeterinarianNotes
        VisitStatus Status
        string PetId
        string VeterinarianId
    }

    class VisitStatus {
        <<enumeration>>
        Scheduled
        InProgress
        Completed
        Cancelled
    }

    Owner "1" --> "*" Pet
    Pet "1" --> "*" Visit
    StaffUser "1" --> "*" Visit
    Visit --> VisitStatus

    ```
