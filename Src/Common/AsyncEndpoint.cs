using Microsoft.AspNetCore.Mvc;

namespace RichillCapital.Exchange.Api.Common;

public static class AsyncEndpoint
{
    public static class WithRequest<TRequest>
    {
        public abstract class WithResponse<TResponse> : Endpoint
        {
            public abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
        }

        public abstract class WithoutResponse : Endpoint
        {
            public abstract Task HandleAsync(TRequest request, CancellationToken cancellationToken = default);
        }

        public abstract class WithActionResult<TResponse> : Endpoint
        {
            public abstract Task<ActionResult<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
        }

        public abstract class WithActionResult : Endpoint
        {
            public abstract Task<ActionResult> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
        }

        public abstract class WithAsyncEnumerableResult<T> : Endpoint
        {
            public abstract IAsyncEnumerable<T> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
        }
    }

    public static class WithoutRequest
    {
        public abstract class WithResponse<TResponse> : Endpoint
        {
            public abstract Task<TResponse> HandleAsync(CancellationToken cancellationToken = default);
        }

        public abstract class WithoutResponse : Endpoint
        {
            public abstract Task HandleAsync(CancellationToken cancellationToken = default);
        }

        public abstract class WithActionResult<TResponse> : Endpoint
        {
            public abstract Task<ActionResult<TResponse>> HandleAsync(CancellationToken cancellationToken = default);
        }

        public abstract class WithActionResult : Endpoint
        {
            public abstract Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default);
        }

        public abstract class WithAsyncEnumerableResult<T> : Endpoint
        {
            public abstract IAsyncEnumerable<T> HandleAsync(CancellationToken cancellationToken = default);
        }
    }
}