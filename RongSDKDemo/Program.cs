using System;
using io.rong;

namespace RongSDKDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string retstr;
            const string appKey = "k51hidwq1bu1b";
            const string appSecret = "B6GaMZVg2z";

            retstr = RongCloudServer.GetTokenAsync(appKey, appSecret, "232424", "xugang",
                "http://www.qqw21.com/article/UploadPic/2012-11/201211259378560.jpg").Result;
            Console.WriteLine("getToken: " + retstr);
            Console.ReadKey();

            retstr = RongCloudServer.JoinGroupAsync(appKey, appSecret, "232424", "group001", "wsw").Result;
            Console.WriteLine("joinGroup: " + retstr);
            Console.ReadKey();

            string[] arrId = {"group001", "group002", "group003"};
            string[] arrName = {"测试 01", "测试 02", "测试 03"};
            retstr = RongCloudServer.SyncGroupAsync(appKey, appSecret, "42424", arrId, arrName).Result;
            Console.WriteLine("syncGroup: " + retstr);
            Console.ReadKey();

            retstr = RongCloudServer.DismissGroupAsync(appKey, appSecret, "42424", "group001").Result;
            Console.WriteLine("dismissgroup: " + retstr);
            Console.ReadKey();

            retstr = RongCloudServer.PublishMessageAsync(appKey, appSecret, "2191", "2191", "RC:TxtMsg",
                "{\"content\":\"c#hello\"}").Result;
            Console.WriteLine("PublishMsg: " + retstr);
            Console.ReadKey();

            retstr = RongCloudServer.BroadcastMessageAsync(appKey, appSecret, "2191", "RC:TxtMsg",
                "{\"content\":\"c#hello\"}").Result;
            Console.WriteLine("Broad: " + retstr);
            Console.ReadKey();

            retstr = RongCloudServer.JoinGroupAsync(appKey, appSecret, "423424", "dwef", "dwef").Result;
            Console.WriteLine("JoinGroup: " + retstr);
            Console.ReadKey();

            retstr = RongCloudServer.CreateChatroomAsync(appKey, appSecret, arrId, arrName).Result;
            Console.WriteLine("createChat: " + retstr);
            Console.ReadKey();

            retstr = RongCloudServer.DestroyChatroomAsync(appKey, appSecret, new[] {"001", "002"}).Result;
            Console.WriteLine("Destroy: " + retstr);
            Console.ReadKey();

            string[] aaa = {"group002", "group003"};

            retstr = RongCloudServer.QueryChatroomAsync(appKey, appSecret, aaa).Result;
            Console.WriteLine("queryChatroom: " + retstr);

            Console.ReadKey();

            Console.WriteLine("接口测试结束！");
            Console.ReadKey();
        }
    }
}