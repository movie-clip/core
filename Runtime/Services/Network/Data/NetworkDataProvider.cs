using System.Linq;
using Core.Services.Network.Data;

namespace Core.Network.Data
{
    public interface INetworkDataProvider
    {
        EnvironmentData CurrentEnv { get; }
        EndPointData EndPoint { get; }
        void SetEndPoint(EndPointData endPoint);
        void SetEnv(EnvironmentData envData);

        ProjectData GetProjectById(string id);
        EnvironmentData GetEnvByName(string projectId, string envName);
    }
    
    public class NetworkDataProvider : INetworkDataProvider
    {
        public EnvironmentData CurrentEnv { get; private set; }
        public EndPointData EndPoint { get; private set; }
        
        public void SetEndPoint(EndPointData endPoint)
        {
            EndPoint = endPoint;
        }
        
        public void SetEnv(EnvironmentData envData)
        {
            CurrentEnv = envData;
        }
        
        public ProjectData GetProjectById(string id)
        {
            return EndPoint.projects.FirstOrDefault(x => x.slug == id);
        }
        
        public EnvironmentData GetEnvByName(string projectId, string envName)
        {
            return EndPoint.projects.FirstOrDefault(x => x.slug == projectId)?.envs
                .FirstOrDefault(x => x.name == envName);
        }
    }
}