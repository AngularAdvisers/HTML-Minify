using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace HTML_Minify
{
    public static class HtmlMinifier
    {
        private const string URL_HTML_MINIFIER = "https://html-minifier.com/raw";
        private const string POST_PAREMETER_NAME = "input";

        public static async Task<String> MinifyHtml(string inputHtml)
        {
            List<KeyValuePair<String, String>> contentData = new List<KeyValuePair<String, String>>
        {
            new KeyValuePair<String, String>(POST_PAREMETER_NAME, inputHtml)
        };

            using (HttpClient httpClient = new HttpClient())
            {
                using (FormUrlEncodedContent content = new FormUrlEncodedContent(contentData))
                {
                    using (HttpResponseMessage response = await httpClient.PostAsync(URL_HTML_MINIFIER, content))
                    {
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("A file name must be specified");
            }
            else
            {
                Console.WriteLine("This has {0} arguments, the arguments are: ", args.Length);
                for (int i = 0; i < args.Length; ++i) Console.WriteLine(args[i]);
                var filename = args[0];
                string line = File.ReadAllText(filename);
                string minline = "";
                using (Task<String> task = HtmlMinifier.MinifyHtml(line))
                {
                    task.Wait();
                    minline = task.Result; // ...
                }
                minline = "\"" + minline.Replace("\"", "\\\"") + "\"";
                File.WriteAllText(filename + ".min", minline);
            }
        }
    }
}
