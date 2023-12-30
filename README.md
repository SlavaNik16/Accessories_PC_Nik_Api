# Тема
Автоматизация процессов продажи компонентов пк

Описание предметной области
---
Данный проект предназначен для автоматизации покупок у клиентов компонентов пк, а также их услуги. 
В проекте присутствует база данных состоящая из следующих таблиц:
 - TAccessKey - таблица, хрянящаяя в себе ключи доступа, предназначенные для повышения прав у работника;
 - TClient - таблица, хрянящаяя в себе данные о клиентах;
 - TComponent - таблица, хрянящаяя в себе данные о комплектующих пк, которые есть в наличии;
 - TDelivery - таблица, хрянящаяя в себе данные о доставках, если клиент использует интернет приложение;
 - TService - таблица, хрянящаяя в себе данные о услугах, для обслуживания комплектующих пк;
 - TWorker - таблица, хрянящаяя в себе данные о работниках, которые помогают клиентам и создают ключи доступа;
 - TOrder - таблица, хрянящаяя в себе данные о заказе, который совершил клиент;

Бизнес домен
---
Комплектующие ПК

Автор
---
Николаев Вячеслав Алексеевич студент группы ИП 20-3

# Пример реального бизнес сценария

![RealProimer](https://github.com/SlavaNik16/Accessories_PC_Nik/assets/70444635/7eed2153-d7ab-43fa-9518-903fb782f0ef)

## Схема базы данных
```mermaid
erDiagram

    BaseAuditEntity {
        Guid Id
        DateTimeOffset CreatedAt
        string CreatedBy
        DateTimeOffset UpdatedAt
        string UpdatedBy
        DateTimeOffset DeleteddAt
    }

    AccessKey {
        Guid Key
        Enum Types
        Guid WorkerId
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
        string Name
        Enum TypeComponents
        string Description "null"
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
        Guid ServiceId "null"
        Guid ComponentId "null"
        DateTime OrderTime
        Guid DeliveryId "null"
        string Comment "null"
    }
    Service {
        string Name
        string Description "null"
        string Duration
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
    Worker ||--o{ AccessKey: is

    BaseAuditEntity ||--o{ Delivery: allows
    BaseAuditEntity ||--o{ Service: allows
    BaseAuditEntity ||--o{ Component: allows
    BaseAuditEntity ||--o{ Worker: allows
    BaseAuditEntity ||--o{ Order: allows
    BaseAuditEntity ||--o{ AccessKey: allows
    BaseAuditEntity ||--o{ Client: allows
 ```
# Sql - скрипты

GO
INSERT [dbo].[TAccessKey] ([Id], [Key], [Types], [WorkerId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'7e965c83-d0bf-4717-9fa7-599d2eab5654', N'a503aa32-7ffb-443e-b2c4-0fa8173020f9', 2, N'85aaebc3-3908-40d9-90ac-c2b1c696a0a5', CAST(N'2023-12-30T04:40:27.7438085+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:40:27.7438185+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
GO
INSERT [dbo].[TClient] ([Id], [Surname], [Name], [Patronymic], [Phone], [Email], [Balance], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'1e14eb02-d7f9-476a-89d3-38043fa4a644', N'Кисель', N'Александр', N'Игоревич', N'89313635338', N'Kicel@gmail.com', CAST(45000.00 AS Decimal(18, 2)), CAST(N'2023-12-30T04:19:32.9443000+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:19:32.9443105+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
INSERT [dbo].[TClient] ([Id], [Surname], [Name], [Patronymic], [Phone], [Email], [Balance], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'6cec111d-de6c-4137-b02e-413a4254751d', N'Новоселова', N'Анастасия', N'', N'89657343223', N'Kraskova@gmail.com', CAST(21.00 AS Decimal(18, 2)), CAST(N'2023-12-30T04:30:23.9368348+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:30:23.9368350+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
INSERT [dbo].[TClient] ([Id], [Surname], [Name], [Patronymic], [Phone], [Email], [Balance], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'4b590dca-1c03-43a0-86b4-9b764b5fb4e4', N'Халле', N'Александр', N'', N'88005553535', N'Halle@gmail.com', CAST(230000.00 AS Decimal(18, 2)), CAST(N'2023-12-30T04:20:35.7108679+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:20:35.7108681+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
INSERT [dbo].[TClient] ([Id], [Surname], [Name], [Patronymic], [Phone], [Email], [Balance], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'50407401-fdd4-40d2-8e71-acf6c96f5355', N'Николаев', N'Вячеслав', N'Алексеевич', N'79006357315', N'Nik@gmail.com', CAST(2400.00 AS Decimal(18, 2)), CAST(N'2023-12-30T04:17:47.3122417+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:17:47.3124310+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
GO
INSERT [dbo].[TComponent] ([Id], [Name], [TypeComponents], [Description], [MaterialType], [Price], [Count], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'9a9760e8-c833-438e-906c-3b28eef1fa55', N'Intel Core i9 13 700k', 0, N'Подходит для материнских плат Socket Z', 3, CAST(10000.00 AS Decimal(18, 2)), 2, CAST(N'2023-12-30T04:33:00.0362163+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:33:00.0362226+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
INSERT [dbo].[TComponent] ([Id], [Name], [TypeComponents], [Description], [MaterialType], [Price], [Count], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'597a69ba-6258-49e5-83b0-e64d10df252c', N'Intel Core i7 10 700k', 0, N'Как у Киселя', 2, CAST(20000.00 AS Decimal(18, 2)), 1, CAST(N'2023-12-30T04:33:43.2695069+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:33:43.2695072+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
INSERT [dbo].[TComponent] ([Id], [Name], [TypeComponents], [Description], [MaterialType], [Price], [Count], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'88631c4d-5cde-4a5e-9b55-f1403266e1ee', N'RTX 3090 ti', 4, N'Крутая видеокарта 8 гб', 1, CAST(30000.00 AS Decimal(18, 2)), 4, CAST(N'2023-12-30T04:35:15.1611007+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:35:15.1611012+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
GO
INSERT [dbo].[TDelivery] ([Id], [From], [To], [Price], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'1e5a06a9-f03c-4e36-8594-019b328af4a8', N'Москва', N'Нижний Новгород', CAST(1700.00 AS Decimal(18, 2)), CAST(N'2023-12-30T04:39:23.4277623+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:39:23.4277627+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
INSERT [dbo].[TDelivery] ([Id], [From], [To], [Price], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'9eb49753-13eb-468d-91f8-6ff3ef98bd7f', N'СПб', N'Москва', CAST(3000.00 AS Decimal(18, 2)), CAST(N'2023-12-30T04:38:34.3285855+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:38:34.3285920+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
INSERT [dbo].[TDelivery] ([Id], [From], [To], [Price], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'2940dc13-4fe9-424e-adc4-7a42bb0f2150', N'СПб', N'Нижний Новгород', CAST(1900.00 AS Decimal(18, 2)), CAST(N'2023-12-30T04:38:52.7550077+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:38:52.7550080+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
GO
INSERT [dbo].[TOrder] ([Id], [ClientId], [ServiceId], [ComponentId], [OrderTime], [DeliveryId], [Comment], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'38240888-ee34-4e0a-be1d-5b18706cb200', N'1e14eb02-d7f9-476a-89d3-38043fa4a644', N'6897feac-93f6-4957-8b28-fe4996a074ca', NULL, CAST(N'2023-12-30T04:41:30.3240000' AS DateTime2), NULL, N'', CAST(N'2023-12-30T04:45:09.7883599+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:45:09.7883602+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
INSERT [dbo].[TOrder] ([Id], [ClientId], [ServiceId], [ComponentId], [OrderTime], [DeliveryId], [Comment], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'a7ab3c17-a942-463c-bf82-f897967b5136', N'4b590dca-1c03-43a0-86b4-9b764b5fb4e4', N'f619dd91-ee27-4752-97e3-9ebdd29d824b', N'88631c4d-5cde-4a5e-9b55-f1403266e1ee', CAST(N'2023-12-30T04:41:30.3240000' AS DateTime2), N'9eb49753-13eb-468d-91f8-6ff3ef98bd7f', N'Очень крутой магазин, всем рекомендую', CAST(N'2023-12-30T04:42:56.8049534+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:42:56.8049613+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
GO
INSERT [dbo].[TService] ([Id], [Name], [Description], [Duration], [Price], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'f619dd91-ee27-4752-97e3-9ebdd29d824b', N'Заменить процессор', N'Очень сложное дело', 10, CAST(15000.00 AS Decimal(18, 2)), CAST(N'2023-12-30T04:36:28.0320906+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:36:28.0320910+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
INSERT [dbo].[TService] ([Id], [Name], [Description], [Duration], [Price], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'd1d1dcb5-62c8-46ed-a838-aab388ac9f1f', N'Починить видеокарту', N'Чиним любую видеокарту', 4, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2023-12-30T04:35:53.9361966+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:35:53.9362036+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
INSERT [dbo].[TService] ([Id], [Name], [Description], [Duration], [Price], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'6897feac-93f6-4957-8b28-fe4996a074ca', N'Почистить клавиатуру', N'Очень быстро', 0.3, CAST(100.00 AS Decimal(18, 2)), CAST(N'2023-12-30T04:37:19.7500912+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:37:58.6803787+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
GO
INSERT [dbo].[TWorker] ([Id], [Number], [Series], [IssuedAt], [IssuedBy], [DocumentType], [AccessLevel], [ClientId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'85aaebc3-3908-40d9-90ac-c2b1c696a0a5', N'916684', N'4118', CAST(N'2023-12-30T04:22:27.8700000' AS DateTime2), N'ТП УФМС №57 СПБ', 1, 3, N'1e14eb02-d7f9-476a-89d3-38043fa4a644', CAST(N'2023-12-30T04:23:46.8263790+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:25:17.9169788+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
INSERT [dbo].[TWorker] ([Id], [Number], [Series], [IssuedAt], [IssuedBy], [DocumentType], [AccessLevel], [ClientId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'552bdee3-5464-49a7-9fc0-dd7ceca07e7e', N'411792', N'4117', CAST(N'2023-12-30T04:22:27.8700000' AS DateTime2), N'ТП УФМС №57 СПБ', 1, 4, N'50407401-fdd4-40d2-8e71-acf6c96f5355', CAST(N'2023-12-30T04:27:13.2765209+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:27:13.2765226+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
INSERT [dbo].[TWorker] ([Id], [Number], [Series], [IssuedAt], [IssuedBy], [DocumentType], [AccessLevel], [ClientId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt]) VALUES (N'46e1cb60-b512-4148-934b-e085185d77d4', N'27.09.1997', N'19', CAST(N'2023-12-30T04:22:27.8700000' AS DateTime2), N'ТП УФМС №55 СПБ', 2, 1, N'4b590dca-1c03-43a0-86b4-9b764b5fb4e4', CAST(N'2023-12-30T04:28:09.2663922+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', CAST(N'2023-12-30T04:29:35.0334587+00:00' AS DateTimeOffset), N'Accessories_PC_Nik.Api', NULL)
GO


