namespace HttpClient.DelegatingHandlers;

public class LoggingDelegatingHandler(ILogger<LoggingDelegatingHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Http request for {requestUri} is about to start.", request.RequestUri);

            var result = await base.SendAsync(request, cancellationToken);

            result.EnsureSuccessStatusCode();

            logger.LogInformation("Http request for {requestUri} is successfully ended.", request.RequestUri);

            return result;
        }
        catch (Exception e)
        {
            logger.LogError(e, "HTTP request for {request.RequestUri} failed with message: {message}.", request.RequestUri, e.Message);

            throw;
        }
    }
}
