using System.Linq;
using Core.Services.Network.Data;

namespace Core.Network.Data
{
    public interface INetworkDataProvider
    {
        EnvironmentData CurrentEnv { get; }
        EndPointData EndPoint { get; }
        ProjectData GetProjectById(string id);
        EnvironmentData GetEnvByName(string projectId, string envName);
    }
    
    public class NetworkModel : INetworkDataProvider
    {
        public EnvironmentData currentEnv;
        public EndPointData endPoint;

        public EnvironmentData CurrentEnv => currentEnv;
        public EndPointData EndPoint => endPoint;

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