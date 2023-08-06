using Core.UI.Data;

namespace Samples.IntegrationSample.UI
{
    public class LobbyViewData : ViewData
    {
        public string ProfileID { get; set; }
        
        public LobbyViewData(string profileID)
        {
            ProfileID = profileID;
        }
        
        protected override void OnDispose()
        {
        }
    }
}