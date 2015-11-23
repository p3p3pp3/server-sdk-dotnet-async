using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace io.rong
{
    public class RongCloudServer
    {
        /**
         * 构建请求参数
         */

        private static string BuildQueryStr(Dictionary<string, string> dicList)
        {
            var postStr = "";

            foreach (var item in dicList)
            {
                postStr += item.Key + "=" + HttpUtility.UrlEncode(item.Value, Encoding.UTF8) + "&";
            }
            postStr = postStr.Substring(0, postStr.LastIndexOf('&'));
            return postStr;
        }

        private static string BuildParamStr(string[] arrParams)
        {
            var postStr = "";

            for (var i = 0; i < arrParams.Length; i++)
            {
                if (0 == i)
                {
                    postStr = "chatroomId=" + HttpUtility.UrlDecode(arrParams[0], Encoding.UTF8);
                }
                else
                {
                    postStr = postStr + "&" + "chatroomId=" + HttpUtility.UrlEncode(arrParams[i], Encoding.UTF8);
                }
            }
            return postStr;
        }

        /**
         * 获取 token
         */

        public static async Task<string> GetTokenAsync(string appkey, string appSecret, string userId, string name, string portraitUri)
        {
            var dicList = new Dictionary<string, string>();
            dicList.Add("userId", userId);
            dicList.Add("name", name);
            dicList.Add("portraitUri", portraitUri);

            var postStr = BuildQueryStr(dicList);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.GetTokenUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /**
         * 加入 群组
         */

        public static async Task<string> JoinGroupAsync(string appkey, string appSecret, string userId, string groupId, string groupName)
        {
            var dicList = new Dictionary<string, string>();
            dicList.Add("userId", userId);
            dicList.Add("groupId", groupId);
            dicList.Add("groupName", groupName);

            var postStr = BuildQueryStr(dicList);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.JoinGroupUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /**
         * 退出 群组
         */

        public static async Task<string> QuitGroupAsync(string appkey, string appSecret, string userId, string groupId)
        {
            var dicList = new Dictionary<string, string>();
            dicList.Add("userId", userId);
            dicList.Add("groupId", groupId);

            var postStr = BuildQueryStr(dicList);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.QuitGroupUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /**
         * 解散 群组
         */

        public static async Task<string> DismissGroupAsync(string appkey, string appSecret, string userId, string groupId)
        {
            var dicList = new Dictionary<string, string>();
            dicList.Add("userId", userId);
            dicList.Add("groupId", groupId);

            var postStr = BuildQueryStr(dicList);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.DismissUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /**
         * 同步群组
         */

        public static async Task<string> SyncGroupAsync(string appkey, string appSecret, string userId, string[] groupId,
            string[] groupName)
        {
            var postStr = "userId=" + userId + "&";
            string id, name;

            for (var i = 0; i < groupId.Length; i++)
            {
                id = HttpUtility.UrlEncode(groupId[i], Encoding.UTF8);
                name = HttpUtility.UrlEncode(groupName[i], Encoding.UTF8);
                postStr += "group[" + id + "]=" + name + "&";
            }

            postStr = postStr.Substring(0, postStr.LastIndexOf('&'));

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.SyncGroupUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appSecret"></param>
        /// <param name="fromUserId"></param>
        /// <param name="toUserId"></param>
        /// <param name="objectName"></param>
        /// <param name="content">
        ///     RC:TxtMsg消息格式{"content":"hello"}  RC:ImgMsg消息格式{"content":"ergaqreg",
        ///     "imageKey":"http://www.demo.com/1.jpg"}  RC:VcMsg消息格式{"content":"ergaqreg","duration":3}
        /// </param>
        /// <returns></returns>
        public static async Task<string> PublishMessageAsync(string appkey, string appSecret, string fromUserId, string toUserId,
            string objectName, string content)
        {
            var dicList = new Dictionary<string, string>();
            dicList.Add("fromUserId", fromUserId);
            dicList.Add("toUserId", toUserId);
            dicList.Add("objectName", objectName);
            dicList.Add("content", content);

            var postStr = BuildQueryStr(dicList);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.SendMsgUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///     广播消息暂时未开放
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appSecret"></param>
        /// <param name="fromUserId"></param>
        /// <param name="objectName"></param>
        /// <param name="content">
        ///     RC:TxtMsg消息格式{"content":"hello"}  RC:ImgMsg消息格式{"content":"ergaqreg",
        ///     "imageKey":"http://www.demo.com/1.jpg"}  RC:VcMsg消息格式{"content":"ergaqreg","duration":3}
        /// </param>
        /// <returns></returns>
        public static async Task<string> BroadcastMessageAsync(string appkey, string appSecret, string fromUserId, string objectName,
            string content)
        {
            var dicList = new Dictionary<string, string>
            {
                {"content", content},
                {"fromUserId", fromUserId},
                {"objectName", objectName},
                {"pushContent", ""},
                {"pushData", ""}
            };

            var postStr = BuildQueryStr(dicList);
            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.BroadcastUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static async Task<string> CreateChatroomAsync(string appkey, string appSecret, string[] chatroomId, string[] chatroomName)
        {
            string postStr = null;

            string id, name;

            for (var i = 0; i < chatroomId.Length; i++)
            {
                id = HttpUtility.UrlEncode(chatroomId[i], Encoding.UTF8);
                name = HttpUtility.UrlEncode(chatroomName[i], Encoding.UTF8);
                postStr += "chatroom[" + id + "]=" + name + "&";
            }

            postStr = postStr.Substring(0, postStr.LastIndexOf('&'));

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.CreateChatroomUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appSecret"></param>
        /// <param name="chatroomIdInfo">chatroomId=id1001</param>
        /// <returns></returns>
        public static async Task<string> DestroyChatroomAsync(string appkey, string appSecret, string[] chatroomIdInfo)
        {
            string postStr = null;

            postStr = BuildParamStr(chatroomIdInfo);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.DestroyChatroomUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        public static async Task<string> QueryChatroomAsync(string appkey, string appSecret, string[] chatroomId)
        {
            string postStr = null;

            postStr = BuildParamStr(chatroomId);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.QueryChatroomUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }
    }
}