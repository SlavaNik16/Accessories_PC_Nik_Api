using Accessories_PC_Nik.Api.ModelsRequest.AccessKey;
using Accessories_PC_Nik.Api.ModelsRequest.Client;
using Accessories_PC_Nik.Api.Validators.AccessKey;
using Accessories_PC_Nik.Api.Validators.Client;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Tests;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Accessories_PC_Nik.Api.Tests.ValidatorsTests
{
    /// <summary>
    /// Тесты <see cref="CreateClientRequestValidator"/>
    /// Тесты <see cref="EditClientRequestValidator"/>
    /// </summary>
    public class ClientRequestValidatorTests
    {
        private readonly CreateClientRequestValidator validatorCreateRequest;
        private readonly EditClientRequestValidator validatorEditRequest;

        private readonly Mock<IClientsReadRepository> clientsReadRepositoryMock;

        public ClientRequestValidatorTests()
        {
            clientsReadRepositoryMock = new Mock<IClientsReadRepository>();
            validatorCreateRequest = new CreateClientRequestValidator(clientsReadRepositoryMock.Object);
            validatorEditRequest = new EditClientRequestValidator(clientsReadRepositoryMock.Object);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateClientRequest();

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
            var model = TestDataGeneratorApi.CreateClientRequest();

            clientsReadRepositoryMock.Setup(x => x.AnyByPhoneAsync(model.Phone, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            clientsReadRepositoryMock.Setup(x => x.AnyByEmailAsync(model.Email, It.IsAny<CancellationToken>()))
               .ReturnsAsync(false);

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
            var model = new EditClientRequest();

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
            var model = TestDataGeneratorApi.EditClientRequest();

            clientsReadRepositoryMock.Setup(x => x.AnyByPhoneAsync(model.Phone, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            clientsReadRepositoryMock.Setup(x => x.AnyByEmailAsync(model.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            //Act
            var validation = await validatorEditRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveAnyValidationErrors();
        }
    }
}
