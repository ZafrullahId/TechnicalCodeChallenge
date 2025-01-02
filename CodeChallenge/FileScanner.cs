namespace CodeChallenge
{
    public class FileScanner
    {
        private readonly string _folderPath;
        private readonly string _searchString;

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
                    string fileContent = File.ReadAllText(filePath);
                    string fileName = Path.GetFileName(filePath);

                    if (fileContent.Contains(_searchString))
                    {
                        Console.WriteLine($"Present:{fileName}");
                    }
                    else
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
