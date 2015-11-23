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

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appSecret"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="portraitUri"></param>
        /// <returns></returns>
        public static async Task<string> GetTokenAsync(string appkey, string appSecret, string userId, string name, string portraitUri)
        {
            var dicList = new Dictionary<string, string>
            {
                {"userId", userId},
                {"name", name},
                {"portraitUri", portraitUri}
            };

            var postStr = BuildQueryStr(dicList);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.GetTokenUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 加入群组
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appSecret"></param>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static async Task<string> JoinGroupAsync(string appkey, string appSecret, string userId, string groupId, string groupName)
        {
            var dicList = new Dictionary<string, string>
            {
                {"userId", userId},
                {"groupId", groupId},
                {"groupName", groupName}
            };

            var postStr = BuildQueryStr(dicList);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.JoinGroupUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }


        /// <summary>
        /// 退出群组
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appSecret"></param>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static async Task<string> QuitGroupAsync(string appkey, string appSecret, string userId, string groupId)
        {
            var dicList = new Dictionary<string, string>
            {
                {"userId", userId},
                { "groupId", groupId}
            };

            var postStr = BuildQueryStr(dicList);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.QuitGroupUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 解散群组
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appSecret"></param>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static async Task<string> DismissGroupAsync(string appkey, string appSecret, string userId, string groupId)
        {
            var dicList = new Dictionary<string, string>
            {
                {"userId", userId},
                { "groupId", groupId}
            };

            var postStr = BuildQueryStr(dicList);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.DismissUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 同步群组
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appSecret"></param>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static async Task<string> SyncGroupAsync(string appkey, string appSecret, string userId, string[] groupId,
            string[] groupName)
        {
            var postStr = "userId=" + userId + "&";

            for (var i = 0; i < groupId.Length; i++)
            {
                var id = HttpUtility.UrlEncode(groupId[i], Encoding.UTF8);
                var name = HttpUtility.UrlEncode(groupName[i], Encoding.UTF8);
                postStr += "group[" + id + "]=" + name + "&";
            }

            postStr = postStr.Substring(0, postStr.LastIndexOf('&'));

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.SyncGroupUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 发送消息
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
            var dicList = new Dictionary<string, string>
            {
                {"fromUserId", fromUserId},
                {"toUserId", toUserId},
                {"objectName", objectName},
                {"content", content}
            };

            var postStr = BuildQueryStr(dicList);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.SendMsgUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 广播消息暂时未开放
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
        /// 创建聊天室
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appSecret"></param>
        /// <param name="chatroomId"></param>
        /// <param name="chatroomName"></param>
        /// <returns></returns>
        public static async Task<string> CreateChatroomAsync(string appkey, string appSecret, string[] chatroomId, string[] chatroomName)
        {
            string postStr = null;

            for (var i = 0; i < chatroomId.Length; i++)
            {
                var id = HttpUtility.UrlEncode(chatroomId[i], Encoding.UTF8);
                var name = HttpUtility.UrlEncode(chatroomName[i], Encoding.UTF8);
                postStr += "chatroom[" + id + "]=" + name + "&";
            }

            postStr = postStr.Substring(0, postStr.LastIndexOf('&'));

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.CreateChatroomUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 销毁聊天室
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appSecret"></param>
        /// <param name="chatroomIdInfo">chatroomId=id1001</param>
        /// <returns></returns>
        public static async Task<string> DestroyChatroomAsync(string appkey, string appSecret, string[] chatroomIdInfo)
        {
            var postStr = BuildParamStr(chatroomIdInfo);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.DestroyChatroomUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 查询聊天室
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appSecret"></param>
        /// <param name="chatroomId"></param>
        /// <returns></returns>
        public static async Task<string> QueryChatroomAsync(string appkey, string appSecret, string[] chatroomId)
        {
            var postStr = BuildParamStr(chatroomId);

            var client = new RongHttpClient(appkey, appSecret, InterfaceUrl.QueryChatroomUrl, postStr);

            return await client.ExecutePostAsync().ConfigureAwait(false);
        }
    }
}