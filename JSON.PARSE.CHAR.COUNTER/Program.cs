using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JSON.PARSE.CHAR.COUNTER
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

            ParseJson();
        }

        private static void ParseJson()
        {
            Console.WriteLine("You can change char for pasre in appsettings.json, just put your chars in 'CharForParse'");
            var filesPath = $"{Directory.GetCurrentDirectory()}\\FilesToParse";
            if (!Directory.Exists(filesPath))
            {
                Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\FilesToParse");
                Console.WriteLine($"{filesPath} folder was created");
                Console.WriteLine("Put json files in folder to parse");
            }
            var filesList = Directory.GetFiles(filesPath);
            var chars = Configuration["CharForParse"];
            var files = new List<FileModel>();
            foreach (var item in filesList)
            {
                var t = item.Remove(0, filesPath.ToCharArray().Length + 1);
                Console.WriteLine($"{t} was founded in folder");
                var tempFile = File.ReadAllText(item);
                var q = tempFile.ToCharArray().Where(a => char.IsLetterOrDigit(a) && chars.ToCharArray().Any(b => b == a)).ToList();
                var file = new FileModel() { Name = item, CharCounter = q.Count() };
                files.Add(file);
                Console.WriteLine($"{t}:{file.CharCounter} count of chars");
            }
            int allCount = 0;
            files.ForEach(a => { allCount += a.CharCounter; });
            Console.WriteLine($"{allCount} all count of files ");

            Console.ReadKey();
        }
    }

}
