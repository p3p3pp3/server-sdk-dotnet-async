namespace io.rong
{
    internal class InterfaceUrl
    {
        public static string ServerAddr = "http://api.cn.ronghub.com";
        public static string GetTokenUrl = ServerAddr + "/user/getToken.json";
        public static string JoinGroupUrl = ServerAddr + "/group/join.json";
        public static string QuitGroupUrl = ServerAddr + "/group/quit.json";
        public static string DismissUrl = ServerAddr + "/group/dismiss.json";
        public static string SyncGroupUrl = ServerAddr + "/group/sync.json";
        public static string SendMsgUrl = ServerAddr + "/message/publish.json";
        public static string BroadcastUrl = ServerAddr + "/message/broadcast.json";
        public static string CreateChatroomUrl = ServerAddr + "/chatroom/create.json";
        public static string DestroyChatroomUrl = ServerAddr + "/chatroom/destroy.json";
        public static string QueryChatroomUrl = ServerAddr + "/chatroom/query.json";
    }
}