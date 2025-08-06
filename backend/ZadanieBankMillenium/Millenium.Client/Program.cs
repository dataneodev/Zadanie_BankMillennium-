using Microsoft.Extensions.DependencyInjection;
using Millenium.Client.Services;

var services = new ServiceCollection();
services.AddTransient<IHttpService, HttpService>();
services.AddHttpClient();
services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; // Adres twojego serwera Redis
    options.InstanceName = "SampleInstance";
});

var serviceProvider = services.BuildServiceProvider();
var httpService = serviceProvider.GetRequiredService<IHttpService>();


while (true)
{
    var data = await httpService.GetRestDataAsync();

    Console.WriteLine("Data from service: ");
    Console.WriteLine(data);

    await Task.Delay(5000);
}