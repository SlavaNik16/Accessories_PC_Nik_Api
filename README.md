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
        guid Id
        enum role
    }
    test {
        guid id
        guid userId
        guid organizationId
        enum role
    }
```
