using System.Collections.Generic;

namespace Core.UI
{
    public class ViewLayer : BaseMonoBehaviour
    {
        private readonly List<BaseView> _openViews = new List<BaseView>();

        public void AddView(BaseView view)
        {
            _openViews.Add(view);
        }

        public void RemoveAllViews(string exception = null)
        {
            for (int i = _openViews.Count - 1; i >= 0; i--)
            {
                var view = _openViews[i];
                if (!string.IsNullOrEmpty(exception) && exception == view.name)
                {
                    continue;
                }

                ViewManager.Instance.RemoveView(view);
            }
        }

        public void RemoveView(BaseView view)
        {
            _openViews.Remove(view);
            Destroy(view.gameObject);
        }
    }
}