# Accessories_PC_Nik
Продажа комплектующих пк, а также их услуги

## Схема базы данных
```mermaid
erDiagram

    BaseAuditEntity {
        Guid ID
        DateTimeOffset CreatedAt
        string CreatedBy
        DateTimeOffset UpdatedAt
        string UpdatedBy
        DateTimeOffset DeleteddAt
    }

    AccessKey {
        Guid Key
        Enum Types
    }

    Client {
        string Surname
        string Name
        string Patronymic "null"
        string Phone
        string Email
        decimal Balance
    }

    Component {
        Enum TypeComponents
        string Description
        Enum MaterialType
        decimal Price
        int Count
    }

     Delivery {
        DateTimeOffset From
        DateTimeOffset To
        decimal Price
    }

    Order {
        Guid ClientId
        Guid ServiceI "null"
        Guid ComponentId "null"
        DateTime OrderTime
        Guid DeliveryId "null"
        string Comment "null"
    }
    Service {
        string Name
        string Description "null"
        DateTimeOffset Duration
        decimal Price
    }
    Worker {
        string Number
        string Series
        DateTime IssuedAt
        string IssuedBy
        Enum DocumentType
        Enum AccessLevel
        Guid ClientId
    }
    Delivery ||--o{ Order: is
    Service ||--o{ Order: is
    Component ||--o{ Order: is
    Client ||--o{ Order: is
    Client ||--o{ Worker: is

    BaseAuditEntity ||--o{ Delivery: allows
    BaseAuditEntity ||--o{ Service: allows
    BaseAuditEntity ||--o{ Component: allows
    BaseAuditEntity ||--o{ Worker: allows
    BaseAuditEntity ||--o{ Order: allows
    BaseAuditEntity ||--o{ AccessKey: allows
    BaseAuditEntity ||--o{ Client: allows
 ```


