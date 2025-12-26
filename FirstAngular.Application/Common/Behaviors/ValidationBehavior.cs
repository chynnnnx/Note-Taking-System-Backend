using FirstAngular.Application.Common.Results;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FirstAngular.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken))
                );

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Any())
                {
                    var errorMessage = string.Join(", ", failures.Select(f => f.ErrorMessage));

                    // Check if TResponse is Result<T> type
                    var resultType = typeof(TResponse);
                    if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
                    {
                        // Create Result<T>.Fail instance dynamically
                        var failMethod = resultType.GetMethod("Fail", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                        var failResult = failMethod.Invoke(null, new object[] { errorMessage });
                        return failResult as TResponse;
                    }

                    // Otherwise throw, because we don't know how to return failure
                    throw new System.InvalidOperationException("TResponse must be Result<T> to handle validation failures.");
                }
            }

            return await next();
        }
    }
}
