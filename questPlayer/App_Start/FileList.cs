using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using questPlayer.Models;


namespace questPlayer.App_Start
{
    public static class FileList
    {
        static FileList()
        {
           
            FillQuests();
        }
        public static List<FileModel> Quests { get; set; }

        public static void FillQuests()
        {
            Quests = new List<FileModel>();
            string path = HostingEnvironment.MapPath("~/App_Data");
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    var line = System.IO.File.ReadLines(file).First();
                    var split = line.Split(new[] { "***" }, StringSplitOptions.None);
                    Quests.Add(new FileModel()
                    {
                        FileName = file,
                        Name = split[0],
                        Id = int.Parse(split[1])
                    });
                }
            }
        }
    }

    public class FileModel
    {
        public string FileName { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}