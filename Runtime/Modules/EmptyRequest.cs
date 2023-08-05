using Core.Actions;
using Core.Network;
using Core.Services.Network;

namespace Core.Modules
{
    public class EmptyMessageParams : IActionParams
    {
        public IActionParams Clone()
        {
            return new EmptyMessageParams();
        }
    }

    public class EmptyRequest : NetworkRequest
    {
    }
}
