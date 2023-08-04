
namespace Core.Services.Network
{
    public interface INetworkRequest
    {
        string Api { get; }
        
        string Json { get; }
        
        bool OverrideApi { get; }
        
        MethodType Method { get; }
    }
}