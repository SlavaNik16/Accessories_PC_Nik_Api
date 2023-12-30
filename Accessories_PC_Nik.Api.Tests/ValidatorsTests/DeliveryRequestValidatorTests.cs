using Accessories_PC_Nik.Api.ModelsRequest.Delivery;
using Accessories_PC_Nik.Api.Validators.Delivery;
using Accessories_PC_Nik.Tests.Generator;
using FluentValidation.TestHelper;
using Xunit;

namespace Accessories_PC_Nik.Api.Tests.ValidatorsTests
{
    /// <summary>
    /// Тесты <see cref="CreateDeliveryRequestValidator"/>
    /// Тесты <see cref="EditDeliveryRequestValidator"/>
    /// </summary>
    public class DeliveryRequestValidatorTests
    {
        private readonly CreateDeliveryRequestValidator validatorCreateRequest;
        private readonly EditDeliveryRequestValidator validatorEditRequest;

        /// <summary>
        /// Инициализация <see cref="DeliveryRequestValidatorTests"/>
        /// </summary>
        public DeliveryRequestValidatorTests()
        {
            validatorCreateRequest = new CreateDeliveryRequestValidator();
            validatorEditRequest = new EditDeliveryRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateDeliveryRequest();

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
            var model = DataGeneratorApi.CreateDeliveryRequest();

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
            var model = new EditDeliveryRequest();

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
            var model = DataGeneratorApi.EditDeliveryRequest();

            //Act
            var validation = await validatorEditRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveAnyValidationErrors();
        }
    }
}
