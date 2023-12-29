using Accessories_PC_Nik.Api.ModelsRequest.AccessKey;
using Accessories_PC_Nik.Api.ModelsRequest.Client;
using Accessories_PC_Nik.Api.ModelsRequest.Component;
using Accessories_PC_Nik.Api.ModelsRequest.Delivery;
using Accessories_PC_Nik.Api.ModelsRequest.Order;
using Accessories_PC_Nik.Api.ModelsRequest.Service;
using Accessories_PC_Nik.Api.ModelsRequest.Worker;
using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Services.Contracts.Enums;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;
using System.Linq.Expressions;

namespace Accessories_PC_Nik.Services.Tests
{
    static internal class TestDataGeneratorApi
    {
        static internal Client Client(Action<Client>? settings = null)
        {
            var result = new Client
            {
                Surname = $"Surname{Guid.NewGuid():N}",
                Name = $"Name{Guid.NewGuid():N}",
                Phone = $"Phone{Random.Shared.Next(10, 100)}",
                Email = $"Email{Guid.NewGuid():N}@gmail.com",
            };

            result.BaseAuditEntity();

            settings?.Invoke(result);
            return result;
        }

        static internal CreateClientRequest CreateClientRequest(Action<CreateClientRequest>? settings = null)
        {
            var result = new CreateClientRequest
            {
                Surname = $"Surname{Guid.NewGuid():N}",
                Name = $"Name{Guid.NewGuid():N}",
                Phone = $"Phone{Random.Shared.Next(10,100)}",
                Email = $"Email{Guid.NewGuid():N}@gmail.com",
            };

            settings?.Invoke(result);
            return result;
        }
        static internal EditClientRequest EditClientRequest(Action<EditClientRequest>? settings = null)
        {
            var result = new EditClientRequest
            {
                Id = Guid.NewGuid(),
                Surname = $"Surname{Guid.NewGuid():N}",
                Name = $"Name{Guid.NewGuid():N}",
                Phone = $"Phone{Random.Shared.Next(10, 100)}",
                Email = $"Email{Guid.NewGuid():N}@gmail.com",
            };
            settings?.Invoke(result);
            return result;
        }

        static internal Worker Worker(Action<Worker>? settings = null)
        {
            var result = new Worker
            {
                Number = $"Number{Guid.NewGuid():N}",
                Series = $"Series{Guid.NewGuid():N}",
                IssuedBy = $"IssuedBy{Guid.NewGuid():N}",
            };

            result.BaseAuditEntity();

            settings?.Invoke(result);
            return result;
        }
        static internal CreateWorkerRequest CreateWorkerRequest(Action<CreateWorkerRequest>? settings = null)
        {
            var result = new CreateWorkerRequest
            {
                Number = $"Num{Random.Shared.Next(0, 100)}",
                Series = $"Ser{Random.Shared.Next(0,100)}",
                IssuedBy = $"IssuedBy{Guid.NewGuid():N}",
                ClientId = Guid.NewGuid(),
            };

            settings?.Invoke(result);
            return result;
        }

        static internal EditWorkerRequest EditWorkerRequest(Action<EditWorkerRequest>? settings = null)
        {
            var result = new EditWorkerRequest
            {
                Id = Guid.NewGuid(),
                Number = $"Num{Random.Shared.Next(0, 100)}",
                Series = $"Ser{Random.Shared.Next(0, 100)}",
                IssuedBy = $"IssuedBy{Guid.NewGuid():N}",
                ClientId = Guid.NewGuid(),
            };

            settings?.Invoke(result);
            return result;
        }

        static internal Component Component(Action<Component>? settings = null)
        {
            var result = new Component
            {
                Name = $"Name{Guid.NewGuid():N}",
            };

            result.BaseAuditEntity();

            settings?.Invoke(result);
            return result;
        }
        static internal CreateComponentRequest CreateComponentRequest(Action<CreateComponentRequest>? settings = null)
        {
            var result = new CreateComponentRequest
            {
                Name = $"Name{Guid.NewGuid():N}",
            };


            settings?.Invoke(result);
            return result;
        }
        static internal EditComponentRequest EditComponentRequest(Action<EditComponentRequest>? settings = null)
        {
            var result = new EditComponentRequest
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
            };


            settings?.Invoke(result);
            return result;
        }

        static internal Service Service(Action<Service>? settings = null)
        {
            var result = new Service
            {
                Name = $"Name{Guid.NewGuid():N}",
                Duration = Random.Shared.Next(2, 6),
            };

            result.BaseAuditEntity();

            settings?.Invoke(result);
            return result;
        }
        static internal CreateServiceRequest CreateServiceRequest(Action<CreateServiceRequest>? settings = null)
        {
            var result = new CreateServiceRequest
            {
                Name = $"Name{Guid.NewGuid():N}",
                Duration = Random.Shared.Next(2, 6),
            };

            settings?.Invoke(result);
            return result;
        }
        static internal EditServiceRequest EditServiceRequest(Action<EditServiceRequest>? settings = null)
        {
            var result = new EditServiceRequest
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Duration = Random.Shared.Next(2, 6),
            };

            settings?.Invoke(result);
            return result;
        }
        static internal Delivery Delivery(Action<Delivery>? settings = null)
        {
            var result = new Delivery
            {
                From = $"From{Guid.NewGuid():N}",
                To = $"To{Guid.NewGuid():N}",
            };

            result.BaseAuditEntity();

            settings?.Invoke(result);
            return result;
        }
        static internal CreateDeliveryRequest CreateDeliveryRequest(Action<CreateDeliveryRequest>? settings = null)
        {
            var result = new CreateDeliveryRequest
            {
                From = $"From{Guid.NewGuid():N}",
                To = $"To{Guid.NewGuid():N}",
            };

            settings?.Invoke(result);
            return result;
        }
        static internal EditDeliveryRequest EditDeliveryRequest(Action<EditDeliveryRequest>? settings = null)
        {
            var result = new EditDeliveryRequest
            {
                Id = Guid.NewGuid(),
                From = $"From{Guid.NewGuid():N}",
                To = $"To{Guid.NewGuid():N}",
            };

            settings?.Invoke(result);
            return result;
        }

        static internal Order Order(Action<Order>? settings = null)
        {
            var result = new Order();

            result.BaseAuditEntity();

            settings?.Invoke(result);
            return result;
        }
        static internal CreateOrderRequest CreateOrderRequest(Action<CreateOrderRequest>? settings = null)
        {
            var result = new CreateOrderRequest
            {
                ClientId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                ComponentId = Guid.NewGuid(),
                DeliveryId = Guid.NewGuid(),
            };

            settings?.Invoke(result);
            return result;
        }
        static internal EditOrderRequest EditOrderRequest(Action<EditOrderRequest>? settings = null)
        {
            var result = new EditOrderRequest
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                ComponentId = Guid.NewGuid(),
                DeliveryId = Guid.NewGuid(),
            };

            settings?.Invoke(result);
            return result;
        }

        static internal AccessKey AccessKey(Action<AccessKey>? settings = null)
        {
            var result = new AccessKey
            {
                Key = Guid.NewGuid(),
                Types = AccessLevelTypes.Assistant,
            };

            result.BaseAuditEntity();

            settings?.Invoke(result);
            return result;
        }

        static internal CreateAccessKeyRequest CreateAccessKeyRequest(Action<CreateAccessKeyRequest>? settings = null)
        {
            var result = new CreateAccessKeyRequest
            {
                Types = AccessLevelTypesModel.Assistant,
                WorkerId = Guid.NewGuid(),
            };
            settings?.Invoke(result);
            return result;
        }


        private static void BaseAuditEntity<TEntity>(this TEntity entity) where TEntity : BaseAuditEntity
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.CreatedBy = $"CreatedBy{Guid.NewGuid():N}";
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}";
        }
    }
}
