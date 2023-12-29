using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;

namespace Accessories_PC_Nik.Services.Tests
{
    static internal class TestDataGeneratorService
    {
        static internal Client Client(Action<Client>? settings = null)
        {
            var result = new Client
            {
                Surname = $"Surname{Guid.NewGuid():N}",
                Name = $"Name{Guid.NewGuid():N}",
                Phone = $"Phone{Guid.NewGuid():N}",
                Email = $"Email{Guid.NewGuid():N}@gmail.com",
            };

            result.BaseAuditEntity();

            settings?.Invoke(result);
            return result;
        }

        static internal ClientRequestModel ClientRequestModel(Action<ClientRequestModel>? settings = null)
        {
            var result = new ClientRequestModel
            {
                Id = Guid.NewGuid(),
                Surname = $"Surname{Guid.NewGuid():N}",
                Name = $"Name{Guid.NewGuid():N}",
                Phone = $"Phone{Guid.NewGuid():N}",
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
        static internal WorkerRequestModel WorkerRequestModel(Action<WorkerRequestModel>? settings = null)
        {
            var result = new WorkerRequestModel
            {
                Id = Guid.NewGuid(),
                Number = $"Number{Guid.NewGuid():N}",
                Series = $"Series{Guid.NewGuid():N}",
                IssuedBy = $"IssuedBy{Guid.NewGuid():N}",
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
        static internal ComponentRequestModel ComponentRequestModel(Action<ComponentRequestModel>? settings = null)
        {
            var result = new ComponentRequestModel
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
        static internal ServiceRequestModel ServiceRequestModel(Action<ServiceRequestModel>? settings = null)
        {
            var result = new ServiceRequestModel
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
        static internal DeliveryRequestModel DeliveryModel(Action<DeliveryRequestModel>? settings = null)
        {
            var result = new DeliveryRequestModel
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

        static internal OrderRequestModel OrderModel(Action<OrderRequestModel>? settings = null)
        {
            var result = new OrderRequestModel
            {
                Id = Guid.NewGuid(),
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

        static internal AccessKeyRequestModel AccessKeyRequestModel(Action<AccessKeyRequestModel>? settings = null)
        {
            var result = new AccessKeyRequestModel
            {
                Id = Guid.NewGuid(),
                Types = AccessLevelTypes.Assistant,
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
