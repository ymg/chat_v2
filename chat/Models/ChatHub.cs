using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace chat.Models
{
    public class ChatHub : Hub
    {
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        private static Dictionary<string, string> _userList = new Dictionary<string, string>();
        // ReSharper restore FieldCanBeMadeReadOnly.Local

        public void AddText(string chatMessage)
        {
            var validUser = _userList.SingleOrDefault(s => s.Key == Context.ConnectionId);
            var message = validUser.Value == string.Empty ? null : string.Format("<li><b>{0}</b>: {1} </li>", validUser.Value, HttpUtility.HtmlEncode(chatMessage));
            if (message != null)
                Clients.All.retrieveInput(message);
        }

        public void AddInUserList(string nickName, string connectionId)
        {
            _userList.Add(connectionId, HttpUtility.HtmlEncode(nickName));
            OnConnected();
        }

        public override Task OnDisconnected()
        {
            var userInstance = _userList.SingleOrDefault(x => x.Key == Context.ConnectionId);
            if (userInstance.Value != string.Empty)
                _userList.Remove(Context.ConnectionId);
            OnConnected();
            return base.OnDisconnected();
        }

        public override Task OnConnected()
        {
            var jsonObj = _userList.Count != -1
                             ? new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(_userList)
                             : string.Empty;
            if (jsonObj != string.Empty)
                Clients.All.currentUsers(jsonObj);

            return base.OnConnected();
        }
    }
}

