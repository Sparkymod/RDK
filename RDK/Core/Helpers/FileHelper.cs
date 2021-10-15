using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace RDK.Core.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// Read an xml file.
        /// </summary>
        /// <param name="filename">File path.</param>
        /// <returns>XmlDocument with the data from the xml.</returns>
        public static XmlDocument ReadDataFromXml(string filename)
        {
            FileStream stream = File.OpenRead(filename);
            if (stream == null)
            {
                return null;
            }

            XmlDocument document = new();
            document.Load(stream);
            return document;
        }

        /// <summary>
        /// Read all files in a directory and subdirectory.
        /// </summary>
        /// <param name="path">Directory path.</param>
        /// <param name="resource">Resource you want to get all the files from.</param>
        /// <returns>List of folders with their files.</returns>
        public static List<string> GetAllFilesFrom(string path, string resource)
        {
            string delimiter = $"{resource}/*.*";
            List<string> files = new();

            foreach (string file in Directory.GetFiles(path, delimiter, SearchOption.AllDirectories))
            {
                if (string.IsNullOrEmpty(file))
                {
                    continue;
                }
                files.Add(file);
            }
            return files;
        }
    }
}
