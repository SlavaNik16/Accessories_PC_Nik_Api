﻿using FluentValidation;
using Accessories_PC_Nik.Api.Validators.Discipline;
using Accessories_PC_Nik.Api.Validators.Document;
using Accessories_PC_Nik.Api.Validators.Employee;
using Accessories_PC_Nik.Api.Validators.Group;
using Accessories_PC_Nik.Api.Validators.Person;
using Accessories_PC_Nik.Api.Validators.TimeTableItem;
using Accessories_PC_Nik.Services.Contracts.Exceptions;
using Accessories_PC_Nik.Shared;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Api.ModelsRequest.Discipline;
using Accessories_PC_Nik.Api.Validators.Worker;

namespace Accessories_PC_Nik.Api.Infrastructures.Validator
{
    internal sealed class ApiValidatorService : IApiValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ApiValidatorService(IClientsReadRepository personReadRepository,
            IWorkersReadRepository employeeReadRepository)
        {
            Register<CreateAccessKeyRequestValidator>();
            Register<EditAccessKeyRequestValidator>();

            Register<CreateClientRequestValidator>();
            Register<EditClientRequestValidator>();

            Register<CreateComponentRequestValidator>();
            Register<EditComponentRequestValidator>();

            Register<CreateDeliveryRequestValidator>();
            Register<EditDeliveryRequestValidator>();

            Register<CreateOrderRequestValidator>();
            Register<EditOrderRequestValidator>();

            Register<CreateServiceRequestValidator>();
            Register<EditServiceRequestValidator>();

            Register<CreateWorkerRequestValidator>(personReadRepository);
            Register<EditWorkerRequestValidator>(personReadRepository);
        }

        ///<summary>
        /// Регистрирует валидатор в словаре
        /// </summary>
        public void Register<TValidator>(params object[] constructorParams)
            where TValidator : IValidator
        {
            var validatorType = typeof(TValidator);
            var innerType = validatorType.BaseType?.GetGenericArguments()[0];
            if (innerType == null)
            {
                throw new ArgumentNullException($"Указанный валидатор {validatorType} должен быть generic от типа IValidator");
            }

            if (constructorParams?.Any() == true)
            {
                var validatorObject = Activator.CreateInstance(validatorType, constructorParams);
                if (validatorObject is IValidator validator)
                {
                    validators.TryAdd(innerType, validator);
                }
            }
            else
            {
                validators.TryAdd(innerType, Activator.CreateInstance<TValidator>());
            }
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new TimeTableValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
