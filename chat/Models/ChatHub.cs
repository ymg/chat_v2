using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Microsoft.AspNet.SignalR;

namespace chat.Models
{
    public class ChatHub : Hub
    {

        private static readonly Dictionary<string, string> UserList = new Dictionary<string, string>();

        public void AddText(string chatMessage)
        {
            var validUser = UserList.SingleOrDefault(s => s.Key == Context.ConnectionId);
            var message = validUser.Value == string.Empty ? null : string.Format("<li><b>{0}</b>: {1} </li>", validUser.Value, HttpUtility.HtmlEncode(chatMessage));
            if (message != null)
                Clients.All.retrieveInput(message);
        }

        public string GetCurrentUsers()
        {
            var jsonObj = UserList.Count != -1
                             ? new JavaScriptSerializer().Serialize(UserList)
                             : string.Empty;
            if (jsonObj != string.Empty)
            {
                Clients.All.currentUsers(jsonObj);
                return jsonObj;
            }
            return null;
        }

        public void RemoveUser(string id)
        {
            var userInstance = UserList.SingleOrDefault(x => x.Key == id);
            if (userInstance.Value == string.Empty) return;
            UserList.Remove(id);
        }

        public void AddInUserList(string nickName, string connectionId)
        {
            UserList.Add(connectionId, nickName);
            Clients.All.currentUsers(GetCurrentUsers());
        }

        public override Task OnDisconnected()
        {
            RemoveUser(Context.ConnectionId);
            return Clients.All.currentUsers(GetCurrentUsers());
        }

        public override Task OnConnected()
        {
            return Clients.All.currentUsers(GetCurrentUsers());
        }
    }
}

