namespace Millenium.Client.Services;

public interface IHttpService
{
    Task<string?> GetRestData();
}