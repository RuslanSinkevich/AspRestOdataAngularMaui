namespace TodoREST.Services.IServices
{
	public interface IHttpsClientHandlerService
	{
        HttpMessageHandler GetPlatformMessageHandler();
    }
}

