using Microsoft.Extensions.Options;
using MyIoTService.Core.Options;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyIoTService.Core.Services.Mqtt
{
    public class HiveMqCredentialsService : IHiveMqCredentialsService
    {
        readonly MqttOptions _options;

        const string DeviceRole = "device";

        public HiveMqCredentialsService(IOptions<MqttOptions> options)
        {
            _options = options.Value;
        }

        public async Task AddCredentials(string user, string password)
        {
            var doc = XDocument.Load(_options.UserCredentialsFileLocation);
            var users = doc.Root.Element("users");

            var userElem = new XElement("user");

            var nameElem = new XElement("name");
            nameElem.Value = user;
            userElem.Add(nameElem);

            var passwordElem = new XElement("password");
            passwordElem.Value = password;
            userElem.Add(passwordElem);

            var rolesElem = new XElement("roles");
            var rolesIdElem = new XElement("id");
            rolesIdElem.Value = DeviceRole;
            rolesElem.Add(rolesIdElem);
            userElem.Add(rolesElem);

            users.Add(userElem);

            doc.Save(_options.UserCredentialsFileLocation);
        }

        public async Task DeleteCredentials(string user)
        {
            var doc = XDocument.Load(_options.UserCredentialsFileLocation);

            var users = doc.Root.Element("users");
            var userElem = users.Elements("user").Where(x => x.Element("name")?.Value == user).FirstOrDefault();

            if (userElem is { })
            {
                userElem.Remove();
                doc.Save(_options.UserCredentialsFileLocation);
            }

        }

        public async Task EditCredentials(string user, string password)
        {
            var doc = XDocument.Load(_options.UserCredentialsFileLocation);

            var users = doc.Root.Element("users");
            var userElem = users.Elements("user").Where(x => x.Element("name")?.Value == user).FirstOrDefault();

            if(userElem is { })
            {
                userElem.Element("password").Value = password;
                doc.Save(_options.UserCredentialsFileLocation);
            }
        }
    }
}
