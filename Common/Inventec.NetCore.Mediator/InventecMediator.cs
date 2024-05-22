using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Inventec.NetCore.Mediator
{
    public class InventecMediator : MediatR.Mediator
    {
        private readonly Func<IEnumerable<Func<INotification, CancellationToken, Task>>, INotification, CancellationToken, Task> _publish;

        public InventecMediator(ServiceFactory serviceFactory,
            Func<IEnumerable<Func<INotification, CancellationToken, Task>>, INotification, CancellationToken, Task> publish
        ) : base(serviceFactory)
        {
            _publish = publish;
        }

        protected override Task PublishCore(IEnumerable<Func<INotification, CancellationToken, Task>> allHandlers, INotification notification, CancellationToken cancellationToken)
        {
            return _publish(allHandlers, notification, cancellationToken);
        }
    }

    public interface IApiRequest<TResponse> : MediatR.IRequest<ApiResult<TResponse>>
    {
    }

    public interface IApiPagedRequest<TResponse> : MediatR.IRequest<PagedApiResult<TResponse>>
    {
    }

    public abstract class ApiRequestHandler<TRequest, TResponse> : MediatR.IRequestHandler<TRequest, ApiResult<TResponse>>
        where TRequest : MediatR.IRequest<ApiResult<TResponse>>
    {
        protected readonly ILogger<ApiRequestHandler<TRequest, TResponse>> _logger;
        protected ResultParam _param;

        public ApiRequestHandler(ILogger<ApiRequestHandler<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _param = new ResultParam();
        }

        public async Task<ApiResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _param.HasException = true;
                return await HandleExceptionAsync(ex, request, cancellationToken);
            }
        }

        public abstract Task<ApiResult<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken);

        public virtual async Task<ApiResult<TResponse>> HandleExceptionAsync(Exception ex, TRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return new ApiResult<TResponse>
            {
                Success = false,
                Status = HttpStatusCode.InternalServerError,
                Param = _param
            };
        }

    }

    public abstract class ApiPagedRequestHandler<TRequest, TResponse> : MediatR.IRequestHandler<TRequest, PagedApiResult<TResponse>>
        where TRequest : MediatR.IRequest<PagedApiResult<TResponse>>
    {
        protected readonly ILogger<ApiPagedRequestHandler<TRequest, TResponse>> _logger;
        protected ResultParam _param;

        public ApiPagedRequestHandler(ILogger<ApiPagedRequestHandler<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _param = new ResultParam();
        }

        public async Task<PagedApiResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _param.HasException = true;
                return await HandleExceptionAsync(ex, request, cancellationToken);
            }
        }

        public abstract Task<PagedApiResult<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken);

        public virtual async Task<PagedApiResult<TResponse>> HandleExceptionAsync(Exception ex, TRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return new PagedApiResult<TResponse>
            {
                Success = false,
                Status = HttpStatusCode.InternalServerError,
                Param = _param
            };
        }

    }
}