using Accessories_PC_Nik.Api.ModelsRequest.AccessKey;
using Accessories_PC_Nik.Api.ModelsRequest.Client;
using Accessories_PC_Nik.Api.ModelsRequest.Component;
using Accessories_PC_Nik.Api.ModelsRequest.Delivery;
using Accessories_PC_Nik.Api.ModelsRequest.Order;
using Accessories_PC_Nik.Api.ModelsRequest.Service;
using Accessories_PC_Nik.Api.ModelsRequest.Worker;
using Accessories_PC_Nik.Services.Contracts.Enums;

namespace Accessories_PC_Nik.Tests.Generator
{
    public class DataGeneratorApi
    {
        static public CreateClientRequest CreateClientRequest(Action<CreateClientRequest>? settings = null)
        {
            var result = new CreateClientRequest
            {
                Surname = $"Surname{Guid.NewGuid():N}",
                Name = $"Name{Guid.NewGuid():N}",
                Phone = $"Phone{Random.Shared.Next(10, 10000)}",
                Email = $"Email{Guid.NewGuid():N}@gmail.com",
            };

            settings?.Invoke(result);
            return result;
        }
        static public EditClientRequest EditClientRequest(Action<EditClientRequest>? settings = null)
        {
            var result = new EditClientRequest
            {
                Id = Guid.NewGuid(),
                Surname = $"Surname{Guid.NewGuid():N}",
                Name = $"Name{Guid.NewGuid():N}",
                Phone = $"Phone{Random.Shared.Next(10, 10000)}",
                Email = $"Email{Guid.NewGuid():N}@gmail.com",
            };
            settings?.Invoke(result);
            return result;
        }

        static public CreateWorkerRequest CreateWorkerRequest(Action<CreateWorkerRequest>? settings = null)
        {
            var result = new CreateWorkerRequest
            {
                Number = $"Num{Random.Shared.Next(0, 100000)}",
                Series = $"Ser{Random.Shared.Next(0, 100000)}",
                IssuedBy = $"IssuedBy{Guid.NewGuid():N}",
                ClientId = Guid.NewGuid(),
            };

            settings?.Invoke(result);
            return result;
        }

        static public EditWorkerRequest EditWorkerRequest(Action<EditWorkerRequest>? settings = null)
        {
            var result = new EditWorkerRequest
            {
                Id = Guid.NewGuid(),
                Number = $"Num{Random.Shared.Next(0, 100000)}",
                Series = $"Ser{Random.Shared.Next(0, 100000)}",
                IssuedBy = $"IssuedBy{Guid.NewGuid():N}",
                ClientId = Guid.NewGuid(),
            };

            settings?.Invoke(result);
            return result;
        }

        static public CreateComponentRequest CreateComponentRequest(Action<CreateComponentRequest>? settings = null)
        {
            var result = new CreateComponentRequest
            {
                Name = $"Name{Guid.NewGuid():N}",
            };


            settings?.Invoke(result);
            return result;
        }
        static public EditComponentRequest EditComponentRequest(Action<EditComponentRequest>? settings = null)
        {
            var result = new EditComponentRequest
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
            };


            settings?.Invoke(result);
            return result;
        }

        static public CreateServiceRequest CreateServiceRequest(Action<CreateServiceRequest>? settings = null)
        {
            var result = new CreateServiceRequest
            {
                Name = $"Name{Guid.NewGuid():N}",
                Duration = Random.Shared.Next(2, 6),
            };

            settings?.Invoke(result);
            return result;
        }
        static public EditServiceRequest EditServiceRequest(Action<EditServiceRequest>? settings = null)
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

        static public CreateDeliveryRequest CreateDeliveryRequest(Action<CreateDeliveryRequest>? settings = null)
        {
            var result = new CreateDeliveryRequest
            {
                From = $"From{Guid.NewGuid():N}",
                To = $"To{Guid.NewGuid():N}",
            };

            settings?.Invoke(result);
            return result;
        }
        static public EditDeliveryRequest EditDeliveryRequest(Action<EditDeliveryRequest>? settings = null)
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

        static public CreateOrderRequest CreateOrderRequest(Action<CreateOrderRequest>? settings = null)
        {
            var result = new CreateOrderRequest
            {
                ClientId = Guid.NewGuid(),
            };

            settings?.Invoke(result);
            return result;
        }
        static public EditOrderRequest EditOrderRequest(Action<EditOrderRequest>? settings = null)
        {
            var result = new EditOrderRequest
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
            };

            settings?.Invoke(result);
            return result;
        }

        static public CreateAccessKeyRequest CreateAccessKeyRequest(Action<CreateAccessKeyRequest>? settings = null)
        {
            var result = new CreateAccessKeyRequest
            {
                Types = AccessLevelTypesModel.DeputyDirector,
                WorkerId = Guid.NewGuid(),
            };
            settings?.Invoke(result);
            return result;
        }
    }
}
