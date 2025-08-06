namespace Millenium.SOAP.FakeServices;

public sealed class FakeService : IFakeService
{
    public async Task<int> Add(int x, int y)
    {
        await Task.Delay(120 * 1000);

        return x + y;
    }
}