# Тема
Автоматизация процессов продажи компонентов пк

Описание предметной области
---
Данный проект предназначен для автоматизации покупок у клиентов компонентов пк, а также их услуги. 
В проекте присутствует база данных состоящая из следующих таблиц:
TAccessKey - таблица, хрянящаяя в себе ключи доступа, предназначенные для повышения прав у работника;
TClient - таблица, хрянящаяя в себе данные о клиентах;
TComponent - таблица, хрянящаяя в себе данные о комплектующих пк, которые есть в наличии;
TDelivery - таблица, хрянящаяя в себе данные о доставках, если клиент использует интернет приложение;
TService - таблица, хрянящаяя в себе данные о услугах, для обслуживания комплектующих пк;
TWorker - таблица, хрянящаяя в себе данные о работниках, которые помогают клиентам и создают ключи доступа;
TOrder - таблица, хрянящаяя в себе данные о заказе, который совершил клиент;

Бизнес домен
---
Комплектующие ПК

Автор
---
Николаев Вячеслав Алексеевич студент группы ИП 20-3

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


