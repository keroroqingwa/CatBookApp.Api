using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;

namespace CatBookApp.Utils
{
    public class HttpHelper
    {
        public static string Post(string url, string paramData)
        {
            return Post(url, paramData, Encoding.UTF8);
        }

        public static string Post(string url, string paramData, Encoding encoding)
        {
            string result;

            if (url.ToLower().IndexOf("https", System.StringComparison.Ordinal) > -1)
            {
                ServicePointManager.ServerCertificateValidationCallback =
                               new RemoteCertificateValidationCallback((sender, certificate, chain, errors) => { return true; });
            }

            try
            {
                var wc = new WebClient();
                if (string.IsNullOrEmpty(wc.Headers["Content-Type"]))
                    wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                wc.Encoding = encoding;

                result = wc.UploadString(url, "POST", paramData);
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public static string Get(string url)
        {
            return Get(url, Encoding.UTF8);
        }

        public static string Get(string url, Encoding encoding)
        {
            try
            {
                var wc = new WebClient { Encoding = encoding };
                var readStream = wc.OpenRead(url);
                using (var sr = new StreamReader(readStream, encoding))
                {
                    var result = sr.ReadToEnd();
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
