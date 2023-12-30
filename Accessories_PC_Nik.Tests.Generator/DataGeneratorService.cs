using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;

namespace Accessories_PC_Nik.Tests.Generator
{
    public class DataGeneratorService
    {
        static public ClientRequestModel ClientRequestModel(Action<ClientRequestModel>? settings = null)
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
        static public WorkerRequestModel WorkerRequestModel(Action<WorkerRequestModel>? settings = null)
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

        static public ComponentRequestModel ComponentRequestModel(Action<ComponentRequestModel>? settings = null)
        {
            var result = new ComponentRequestModel
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
            };


            settings?.Invoke(result);
            return result;
        }

        static public ServiceRequestModel ServiceRequestModel(Action<ServiceRequestModel>? settings = null)
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

        static public DeliveryRequestModel DeliveryRequestModel(Action<DeliveryRequestModel>? settings = null)
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

        static public OrderRequestModel OrderRequestModel(Action<OrderRequestModel>? settings = null)
        {
            var result = new OrderRequestModel
            {
                Id = Guid.NewGuid(),
            };

            settings?.Invoke(result);
            return result;
        }

        static public AccessKeyRequestModel AccessKeyRequestModel(Action<AccessKeyRequestModel>? settings = null)
        {
            var result = new AccessKeyRequestModel
            {
                Id = Guid.NewGuid(),
                Types = AccessLevelTypes.Assistant,
            };
            settings?.Invoke(result);
            return result;
        }

    }
}
