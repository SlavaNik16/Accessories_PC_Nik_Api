# Accessories_PC_Nik
Продажа комплектующих пк, а также их услуги

## Схема базы данных
```mermaid
erDiagram
    Persons {
        guid Id
        string LastName
        string FirstName
        string Patronymic
        string Email
        string Phone
        guid GroupId
    }
    Documents {
        guid Id
        int Number
        int Series
        datetime IssuedAt
        string Issuedby
        enum DocumentType
        guid PersonId
    }
Persons ||--o{ Documents: Person.id - Documents.PersonId
```
