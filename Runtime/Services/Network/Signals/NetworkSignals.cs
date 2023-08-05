using Core.Services.Network.Data;

namespace Core.Services.Network.Signals
{
    public class NetworkSignals
    {
        public class EndPointReceived
        {
            public EndPointData EndPoint { get; }

            public EndPointReceived(EndPointData endPoint)
            {
                EndPoint = endPoint;
            }
        }
    }
}