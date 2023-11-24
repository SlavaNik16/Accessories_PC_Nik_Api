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
    
    Disciplines {
            guid Id
            string Name
            string Description
        }
    
    Employees {
        guid Id
        enum EmployeeType
        int PersonId
    }
    Persons ||--o{ Documents: is
    Persons ||--o{ Employees: is
```
