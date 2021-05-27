using BattleShip.BLL.Constants;
using BattleShipAssignment.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BattleShipAssignment
{
    /// <summary>
    /// File Utility for Read and Write file operations
    /// </summary>
    public sealed class FileUtil : IFileOperations
    {
        private static IEnumerator<string> fileRecords { get; set; }
        private static string Directory = $"{AppConstants.Directory}{Path.DirectorySeparatorChar}";

        static FileUtil()
        {

        }
        private FileUtil()
        {
        }
        public static FileUtil Instance { get; } = new FileUtil();
        public static bool IsReadFromFile { get; set; } = false;
        /// <summary>
        /// Reading from file line by line
        /// </summary>
        /// <param name="key"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public string ReadFromFile(string key, int lineNumber)
        {
            string content = string.Empty;
            try
            {
                string path = Path.Combine(Environment.CurrentDirectory, Directory, AppConstants.InputFileName);
                using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (var streamreader = new StreamReader(filestream, Encoding.UTF8))
                    {
                        for (int i = 1; i < lineNumber; i++)
                        {
                            streamreader.ReadLine();

                            if (streamreader.EndOfStream)
                            {
                                Console.WriteLine($"End of file.  The file only contains {i} lines.");
                                break;
                            }
                        }
                        content = streamreader.ReadLine();
                        var commandArgs = content.Split(Path.VolumeSeparatorChar).ToList();
                        return commandArgs[1];
                    }
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("There was an error reading the file: ");
                Console.WriteLine(e.Message);
            }
            return content;

        }

        /// <summary>
        /// Write output to file
        /// </summary>
        /// <param name="output"></param>
        public void WriteToFile(IEnumerable<string> output)
        {
            var fileName = Path.Combine(Environment.CurrentDirectory, Directory, AppConstants.OutputFileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            using (StreamWriter streamWriter = File.CreateText(fileName))
            {
                foreach (var record in output)
                {
                    streamWriter.WriteLine(record);
                }
            }
        }
    }
}
