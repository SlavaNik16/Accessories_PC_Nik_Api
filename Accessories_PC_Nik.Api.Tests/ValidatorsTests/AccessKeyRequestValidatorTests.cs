using Accessories_PC_Nik.Api.ModelsRequest.AccessKey;
using Accessories_PC_Nik.Api.Validators.AccessKey;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Tests;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Accessories_PC_Nik.Api.Tests.ValidatorsTests
{
    /// <summary>
    /// Тесты <see cref="CreateAccessKeyRequestValidator"/>
    /// </summary>
    public class AccessKeyRequestValidatorTests
    {
        private readonly CreateAccessKeyRequestValidator validatorCreateRequest;

        private readonly Mock<IWorkersReadRepository> workersReadRepositoryMock;

        public AccessKeyRequestValidatorTests()
        {
            workersReadRepositoryMock = new Mock<IWorkersReadRepository>();
            validatorCreateRequest = new CreateAccessKeyRequestValidator(workersReadRepositoryMock.Object);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateAccessKeyRequest();

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
            var model = TestDataGeneratorApi.CreateAccessKeyRequest();

            workersReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.WorkerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            var validation = await validatorCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveAnyValidationErrors();
        }

    }
}
