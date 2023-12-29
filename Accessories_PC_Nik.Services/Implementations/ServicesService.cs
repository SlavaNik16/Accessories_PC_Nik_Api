using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Anchors;
using Accessories_PC_Nik.Services.Contracts.Exceptions;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class ServicesService : IServicesService, IServiceAnchor
    {
        private readonly IServicesReadRepository servicesReadRepository;
        private readonly IServicesWriteRepository servicesWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ServicesService(IServicesReadRepository servicesReadRepository,
            IServicesWriteRepository servicesWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.servicesReadRepository = servicesReadRepository;
            this.servicesWriteRepository = servicesWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        async Task<IEnumerable<ServiceModel>> IServicesService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await servicesReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ServiceModel>>(result);
        }

        async Task<ServiceModel?> IServicesService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await servicesReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new AccessoriesEntityNotFoundException<Service>(id);
            }

             return mapper.Map<ServiceModel>(item);
        }

        async Task<ServiceModel> IServicesService.AddAsync(ServiceRequestModel source, CancellationToken cancellationToken)
        {
            var item = new Service
            {
                Id = Guid.NewGuid(),
                Name = source.Name,
                Description = source.Description,
                Duration = source.Duration,
                Price = source.Price,
            };
            servicesWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ServiceModel>(item);
        }



        async Task<ServiceModel> IServicesService.EditAsync(ServiceRequestModel source, CancellationToken cancellationToken)
        {
            var targetService = await servicesReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetService == null)
            {
                throw new AccessoriesEntityNotFoundException<Service>(source.Id);
            }

            var isNameExists = await servicesReadRepository.AnyByNameAsync(source.Name, cancellationToken);
            if (isNameExists)
            {
                var isNameIsIdExists = await servicesReadRepository.AnyByNameIsIdAsync(source.Name, source.Id, cancellationToken);
                if (!isNameIsIdExists)
                {
                    throw new AccessoriesInvalidOperationException($"Данное имя должно уникально при изменение с другими услугами!");
                }
            }

            targetService.Name = source.Name;
            targetService.Description = source.Description;
            targetService.Duration = source.Duration;
            targetService.Price = source.Price;



            servicesWriteRepository.Update(targetService);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ServiceModel>(targetService);
        }
        async Task IServicesService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetService = await servicesReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetService == null)
            {
                throw new AccessoriesEntityNotFoundException<Service>(id);
            }

            servicesWriteRepository.Delete(targetService);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
