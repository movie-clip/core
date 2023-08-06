using Core.Utils.ActionUtils.Actions;

namespace Samples.IntegrationSample
{
    public class AppInitializeAction : BaseAction
    {
        public override void Execute()
        {
            base.Execute();
            Complete();
        }
    }
}