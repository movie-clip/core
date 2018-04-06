
namespace Core.ViewManager
{
    public interface IViewManager
    {
        void Init();
        
        void RegisterView(string viewId, string layerId);

        BaseView SetView(string viewId, object options = null);
        
    }
}
