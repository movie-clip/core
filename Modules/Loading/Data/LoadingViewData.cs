using System.Collections.Generic;
using Core.UI.Data;

namespace Core.Modules.Loading.Data
{
    public class LoadingViewData : ViewData
    {
        public List<ProjectViewData> Projects;
        
        protected override void OnDispose()
        {
        }
    }
    
    public class ProjectViewData : ViewData
    {
        public string Name;
        public string Slug;
        public List<EnvironmentViewData> EnvironmentViewDatas;
        
        protected override void OnDispose()
        {
        }
    }
    
    public class EnvironmentViewData
    {
        public string Slug;
        public string Name;
        public string Api;
        public bool Enabled;
    }
}