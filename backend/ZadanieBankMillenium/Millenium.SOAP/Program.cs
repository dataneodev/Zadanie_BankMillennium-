using Millenium.SOAP.FakeServices;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSoapCore();
builder.Services.AddSingleton<IFakeService, FakeService>();

builder.Services.AddCors();

var app = builder.Build();
app.UseRouting();
app.UseCors(
    builder =>
    {
        builder.SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithExposedHeaders("Content-Disposition");
    }
);
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IFakeService>("/FakeService.asmx",
        new SoapEncoderOptions(),
        SoapSerializer.XmlSerializer);
});

app.Run();