using GitHub.CommitClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GitHub.CommitClient.Extensions;

namespace GitHub.CommitClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = @"https://api.github.com/repos/afranchimon/MyFirstRepo1/commits/eb5e12f4a6cfa4407e270ca560ed07d1290c49c5";
            var response = DownloadString(url);

            Console.WriteLine("Response OK+");
            var commit = JsonDeserializer.Deserialize<Commit>(response);
            Console.WriteLine("Data Deserialized+");

            response = DownloadString(commit.Files.First().ContentUrl);
            var file = JsonDeserializer.Deserialize<ContentFile>(response);


            Console.WriteLine(file.Save()); 

            Console.ReadLine();
        }

        public static string DownloadString(string url)
        {
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/vnd.github.v3+json");
            client.Headers.Add("Authorization", "token e6e767f5016fca526c844c43e0cf316d6f529569");
            client.Headers.Add("User-Agent", "MyAmazingApp"); 
            string response = client.DownloadString(url);
            return response;
            
        }
    }
}
