using MediatR;

namespace Notes.Application.Common.Behaviours
{
    // Fluent validation will work after this settings
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            throw new NotImplementedException();
        }
    }
}
