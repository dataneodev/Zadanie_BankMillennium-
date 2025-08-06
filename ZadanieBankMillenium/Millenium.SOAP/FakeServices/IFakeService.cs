using System.ServiceModel;

namespace Millenium.SOAP.FakeServices;

[ServiceContract]
public interface IFakeService
{
    [OperationContract]
    Task<int> Add(int x, int y);
}