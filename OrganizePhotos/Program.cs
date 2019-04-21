using System;
using System.IO;
using ExifLib;

namespace OrganizePhotos
{
    class Program
    {
        static void Main(string[] args)
        {
            string folder = args.Length == 0 ? "." : args[0];

            foreach (string file in Directory.EnumerateFiles(folder, "*.jpg"))
            {
                DateTime dateTime;
                try
                {
                    using (var reader = new ExifReader(file))
                    {
                        if (!reader.GetTagValue(ExifTags.DateTimeOriginal, out dateTime)) continue;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Couldn't process file '{file}'. {e.Message}");
                    continue;
                }

                string subFolder = Path.Combine(folder, dateTime.ToString("yyyy-MM-dd"));
                Directory.CreateDirectory(subFolder);
                string target = Path.Combine(subFolder, Path.GetFileName(file));
                try
                {
                    File.Move(file, target);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Couldn't move file '{file}' to '{target}'. {e.Message}");
                }
            }
        }
    }
}
