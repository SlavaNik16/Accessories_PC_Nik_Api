using Accessories_PC_Nik.Api.ModelsRequest.Service;
using Accessories_PC_Nik.Api.Validators.Service;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Tests.Generator;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Accessories_PC_Nik.Api.Tests.ValidatorsTests
{
    /// <summary>
    /// Тесты <see cref="CreateServiceRequestValidator"/>
    /// Тесты <see cref="EditServiceRequestValidator"/>
    /// </summary>
    public class ServiceRequestValidatorTests
    {
        private readonly CreateServiceRequestValidator validatorCreateRequest;
        private readonly EditServiceRequestValidator validatorEditRequest;

        private readonly Mock<IServicesReadRepository> servicesReadRepositoryMock;

        /// <summary>
        /// Инициализация <see cref="ServiceRequestValidatorTests"/>
        /// </summary>
        public ServiceRequestValidatorTests()
        {
            servicesReadRepositoryMock = new Mock<IServicesReadRepository>();
            validatorCreateRequest = new CreateServiceRequestValidator(servicesReadRepositoryMock.Object);
            validatorEditRequest = new EditServiceRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateServiceRequest();

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
            var model = DataGeneratorApi.CreateServiceRequest();

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
            var model = new EditServiceRequest();

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
            var model = DataGeneratorApi.EditServiceRequest();

            //Act
            var validation = await validatorEditRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveAnyValidationErrors();
        }
    }
}