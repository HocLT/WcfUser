using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WcfUser
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        [WebInvoke(UriTemplate = "/api/users", Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task<List<UserContract>> GetUsers();
        
        [WebInvoke(UriTemplate = "/api/user/{id}", Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task<UserContract> GetUser(string id);

        [WebInvoke(UriTemplate = "/api/user/username/{username}", Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task<UserContract> FindByUsername(string username);

        [WebInvoke(UriTemplate = "/api/user/check/{name}", Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task<UserContract> FindByUsernameOrEmail(string name);

        [WebInvoke(UriTemplate = "/api/user", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task<bool> Create(UserContract user);
    }
}
