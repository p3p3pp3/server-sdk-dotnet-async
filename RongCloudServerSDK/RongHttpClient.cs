using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace io.rong
{
    internal class RongHttpClient
    {
        private readonly string _appkey;
        private readonly string _appSecret;
        private readonly string _methodUrl;
        private readonly string _postStr;

        public RongHttpClient(string appkey, string appSecret, string methodUrl, string postStr)
        {
            this._methodUrl = methodUrl;
            this._postStr = postStr;
            this._appkey = appkey;
            this._appSecret = appSecret;
        }

        public async Task<string> ExecutePostAsync()
        {
            var rd = new Random();
            var rdI = rd.Next();
            var nonce = Convert.ToString(rdI);

            var timestamp = Convert.ToString(ConvertDateTimeInt(DateTime.Now));

            var signature = GetHash(_appSecret + nonce + timestamp);

            var myRequest = (HttpWebRequest) WebRequest.Create(_methodUrl);

            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";

            myRequest.Headers.Add("App-Key", _appkey);
            myRequest.Headers.Add("Nonce", nonce);
            myRequest.Headers.Add("Timestamp", timestamp);
            myRequest.Headers.Add("Signature", signature);
            myRequest.ReadWriteTimeout = 30*1000;

            var data = Encoding.UTF8.GetBytes(_postStr);
            myRequest.ContentLength = _postStr.Length;

            var newStream = await myRequest.GetRequestStreamAsync().ConfigureAwait(false);

            // Send the data.
            await newStream.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
            newStream.Close();

            HttpWebResponse myResponse;

            try
            {
                myResponse =  (HttpWebResponse)await myRequest.GetResponseAsync().ConfigureAwait(false);
                var reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

                var content = await reader.ReadToEndAsync().ConfigureAwait(false);
                return content;
            }
                //异常请求
            catch (WebException e)
            {
                myResponse = (HttpWebResponse) e.Response;
                using (var errData = myResponse.GetResponseStream())
                {
                    using (var reader = new StreamReader(errData))
                    {
                        var text =await reader.ReadToEndAsync().ConfigureAwait(false);

                        return text;
                    }
                }
            }

        }

        /// <summary>
        ///     DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        private static int ConvertDateTimeInt(DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int) (time - startTime).TotalSeconds;
        }

        private static string GetHash(string input)
        {
            //建立SHA1对象
            SHA1 sha = new SHA1CryptoServiceProvider();

            //将mystr转换成byte[]
            var enc = new UTF8Encoding();
            var dataToHash = enc.GetBytes(input);

            //Hash运算
            var dataHashed = sha.ComputeHash(dataToHash);

            //将运算结果转换成string
            var hash = BitConverter.ToString(dataHashed).Replace("-", "");

            return hash;
        }

        /// <summary>
        ///     Certificate validation callback.
        /// </summary>
        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain,
            SslPolicyErrors error)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (error == SslPolicyErrors.None)
            {
                return true;
            }

            Console.WriteLine("X509Certificate [{0}] Policy Error: '{1}'",
                cert.Subject,
                error);

            return false;
        }
    }
}