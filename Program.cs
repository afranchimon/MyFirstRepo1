using GitHub.CommitClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GitHub.CommitClient.Extensions;
using YouSource.QA.StyleCop;
using System.IO;

namespace GitHub.CommitClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = @"https://api.github.com/repos/afranchimon/MyFirstRepo1/commits/86e26fc19eb3c02ac44186bf615c36a8096b96d7";
            var response = DownloadString(url);

            Console.WriteLine("Response OK+");
            var commit = JsonDeserializer.Deserialize<Commit>(response);
            Console.WriteLine("Data Deserialized+");

            response = DownloadString(commit.Files.Last().ContentUrl);
            var file = JsonDeserializer.Deserialize<ContentFile>(response);

            var fileName = file.Save();

            var inspector = new StyleInspector(Directory.GetCurrentDirectory(), new string[] { fileName });
            var violations = inspector.GetViolations();
            var validator = new CodeAnalyzer(new string[] { fileName });
            var metricsViolations = validator.GetViolations();


            Console.ReadLine();
        }

        public static string DownloadString(string url)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(url))
            {
                WebClient client = new WebClient();
                client.Headers.Add("Accept", "application/vnd.github.v3+json");
                client.Headers.Add("Authorization", "token e6e767f5016fca526c844c43e0cf316d6f529569");
                client.Headers.Add("User-Agent", "MyAmazingApp");
                result = client.DownloadString(url);
            }
            
            return result;
            
        }
    }
}
