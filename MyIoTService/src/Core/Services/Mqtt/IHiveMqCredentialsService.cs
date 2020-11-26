using System.Threading.Tasks;

namespace MyIoTService.Core.Services.Mqtt
{
    public interface IHiveMqCredentialsService 
    {
        Task AddCredentials(string user, string password);
        Task EditCredentials(string user, string password);
        Task DeleteCredentials(string user);
    }
}
