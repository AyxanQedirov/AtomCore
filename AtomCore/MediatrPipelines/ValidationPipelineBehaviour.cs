using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.MediatrPipelines
{
    public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _validator;
        public ValidationPipelineBehaviour(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            await OnBefore(request);
            var response = await next();

            return response;
        }

        private async Task OnBefore(TRequest request)
        {
            if (_validator is null)
                return;

            ValidationResult validationResult = await _validator.ValidateAsync(request);

            if (validationResult.IsValid is not true)
            {
                List<ValidationFailure> errors = validationResult.Errors;
                Dictionary<string, List<string>> errorDic = new();
                errors.ForEach(error =>
                {
                    if (errorDic.ContainsKey(error.PropertyName))
                        errorDic[error.PropertyName].Add(error.ErrorMessage);
                    else
                    {
                        List<string> errorMessages = [error.ErrorMessage];
                        errorDic.Add(error.PropertyName, errorMessages);
                    }
                });

                throw new ValidationException(errorDic);

            }
        }

    }
}
