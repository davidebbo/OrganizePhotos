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
                using (var reader = new ExifReader(file))
                {
                    if (!reader.GetTagValue(ExifTags.DateTimeOriginal, out dateTime)) continue;
                }

                string subFolder = Path.Combine(folder, dateTime.ToString("yyyy-MM-dd"));
                Directory.CreateDirectory(subFolder);
                File.Move(file, Path.Combine(subFolder, Path.GetFileName(file)));
            }
        }
    }
}
