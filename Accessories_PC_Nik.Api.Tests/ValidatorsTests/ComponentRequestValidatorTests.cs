using Accessories_PC_Nik.Api.ModelsRequest.Component;
using Accessories_PC_Nik.Api.Validators.Component;
using Accessories_PC_Nik.Services.Tests;
using FluentValidation.TestHelper;
using Xunit;

namespace Accessories_PC_Nik.Api.Tests.ValidatorsTests
{
    /// <summary>
    /// Тесты <see cref="CreateComponentRequestValidator"/>
    /// Тесты <see cref="EditComponentRequestValidator"/>
    /// </summary>
    public class ComponentRequestValidatorTests
    {
        private readonly CreateComponentRequestValidator validatorCreateRequest;
        private readonly EditComponentRequestValidator validatorEditRequest;

        public ComponentRequestValidatorTests()
        {
            validatorCreateRequest = new CreateComponentRequestValidator();
            validatorEditRequest = new EditComponentRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateComponentRequest();

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
            var model = TestDataGeneratorApi.CreateComponentRequest();

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
            var model = new EditComponentRequest();

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
            var model = TestDataGeneratorApi.EditComponentRequest();

            //Act
            var validation = await validatorEditRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveAnyValidationErrors();
        }
    }
}
