using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ElectronCore
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                HttpListener listener = new HttpListener();
                listener.Prefixes.Add("http://localhost:10000/");
                listener.Start();
                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerResponse res = context.Response;
                    res.StatusCode = 200;
                    byte[] content = Encoding.UTF8.GetBytes("HELLO");
                    res.OutputStream.Write(content, 0, content.Length);
                    res.Close();

                    var parameters = new Dictionary<string, string>()
                    {
                        { "foo", "hoge" },
                        { "bar", "fuga1 fuga2" },
                        { "baz", "あいうえお" },
                    };

                    var str = JsonSerializer.Serialize(parameters);
                    var body = new StringContent(JsonSerializer.Serialize(parameters));;

                    using (var client = new HttpClient())
                    {
                        var response = client.PostAsync($"http://localhost:3000", body);
                        response.Wait();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
