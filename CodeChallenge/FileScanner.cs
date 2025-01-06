using System.Text;

namespace CodeChallenge
{
    public class FileScanner
    {
        private readonly string _folderPath;
        private readonly string _searchString;
        private const int BufferSize = 64 * 1024;

        public FileScanner(string folderPath, string searchString)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentException("Folder path cannot be null or empty.", nameof(folderPath));

            if (string.IsNullOrWhiteSpace(searchString))
                throw new ArgumentException("Search string cannot be null or empty.", nameof(searchString));

            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"The folder path '{folderPath}' does not exist.");

            _folderPath = folderPath;
            _searchString = searchString;
        }

        public void ScanFiles()
        {
            string[] filePaths = Directory.GetFiles(_folderPath);

            if (filePaths.Length == 0)
            {
                Console.WriteLine("No files found in the folder.");
                return;
            }

            foreach (string filePath in filePaths)
            {
                try
                {
                    bool isFound = false;
                    string fileName = Path.GetFileName(filePath);

                    using (FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize, FileOptions.SequentialScan))
                    using (StreamReader reader = new(fs, Encoding.UTF8, true, BufferSize))
                    {
                        char[] buffer = new char[BufferSize];
                        StringBuilder chunk = new StringBuilder();

                        int readCount;
                        while ((readCount = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            chunk.Append(buffer, 0, readCount);
                            if (chunk.ToString().Contains(_searchString, StringComparison.InvariantCultureIgnoreCase))
                            {
                                Console.WriteLine($"Present:{fileName}");
                                isFound = true;
                                break;
                            }
                            // Keep last few characters for cases where the search value is between chunks
                            if (readCount == buffer.Length)
                            {
                                chunk.Remove(0, chunk.Length - _searchString.Length + 1);
                            }
                            else
                            {
                                chunk.Clear();
                            }
                        }
                    }

                    if (!isFound)
                    {
                        Console.WriteLine($"Absent:{fileName}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading file {filePath}: {ex.Message}");
                }
            }
        }
    }
}
