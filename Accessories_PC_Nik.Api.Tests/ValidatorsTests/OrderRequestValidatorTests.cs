using Accessories_PC_Nik.Api.ModelsRequest.Order;
using Accessories_PC_Nik.Api.ModelsRequest.Worker;
using Accessories_PC_Nik.Api.Validators.Order;
using Accessories_PC_Nik.Api.Validators.Worker;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Tests;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Accessories_PC_Nik.Api.Tests.ValidatorsTests
{

    /// <summary>
    /// Тесты <see cref="CreateOrderRequestValidator"/>
    /// Тесты <see cref="EditOrderRequestValidator"/>
    /// </summary>
    public class OrderRequestValidatorTests
    {
        private readonly CreateOrderRequestValidator validatorCreateRequest;
        private readonly EditOrderRequestValidator validatorEditRequest;

        private readonly Mock<IClientsReadRepository> clientsReadRepositoryMock;
        private readonly Mock<IComponentsReadRepository> componentsReadRepositoryMock;
        private readonly Mock<IServicesReadRepository> servicesReadRepositoryMock;
        private readonly Mock<IDeliveryReadRepository> deliveryReadRepositoryMock;

        public OrderRequestValidatorTests()
        {
            clientsReadRepositoryMock = new Mock<IClientsReadRepository>();
            componentsReadRepositoryMock = new Mock<IComponentsReadRepository>();
            deliveryReadRepositoryMock = new Mock<IDeliveryReadRepository>();
            servicesReadRepositoryMock = new Mock<IServicesReadRepository>();

            validatorCreateRequest = new CreateOrderRequestValidator(
                clientsReadRepositoryMock.Object,
                componentsReadRepositoryMock.Object,
                servicesReadRepositoryMock.Object,
                deliveryReadRepositoryMock.Object);
            validatorEditRequest = new EditOrderRequestValidator(
                clientsReadRepositoryMock.Object,
                componentsReadRepositoryMock.Object,
                servicesReadRepositoryMock.Object,
                deliveryReadRepositoryMock.Object);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateOrderRequest();

            //Act
            var validation = await validatorCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGeneratorApi.CreateOrderRequest();

            clientsReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.ClientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

          if (model.ComponentId.HasValue)
            {
                  componentsReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.ComponentId.Value, It.IsAny<CancellationToken>()))
                   .ReturnsAsync(true);
            }

            if (model.DeliveryId.HasValue)
            {
                deliveryReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.DeliveryId.Value, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(true);
            }

            if (model.ServiceId.HasValue)
            {
                servicesReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.ServiceId.Value, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(true);
            }

            //Act
            var validation = await validatorCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorEditRequestShouldError()
        {
            //Arrange
            var model = new EditOrderRequest();

            //Act
            var validation = await validatorEditRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorEditRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGeneratorApi.EditOrderRequest();

            clientsReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.ClientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            if (model.ComponentId.HasValue)
            {
                componentsReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.ComponentId.Value, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(true);
            }

            if (model.DeliveryId.HasValue)
            {
                deliveryReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.DeliveryId.Value, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(true);
            }

            if (model.ServiceId.HasValue)
            {
                servicesReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.ServiceId.Value, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(true);
            }

            //Act
            var validation = await validatorEditRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveAnyValidationErrors();
        }
    }
}
