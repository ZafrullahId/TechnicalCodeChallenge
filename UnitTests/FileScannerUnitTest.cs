using CodeChallenge;

namespace UnitTests
{
    public class FileScannerUnitTest
    {
        [Fact]
        public void ScanFiles_ShouldIdentifyFilesWithAndWithoutSearchString()
        {
            // Arrange
            string testFolderPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(testFolderPath);

            try
            {
                // Create test files
                string file1 = Path.Combine(testFolderPath, "testfile1.txt");
                string file2 = Path.Combine(testFolderPath, "testfile2.txt");
                string file3 = Path.Combine(testFolderPath, "testfile3.txt");

                File.WriteAllText(file1, "This file contains the search string.");
                File.WriteAllText(file2, "This file does not contain it.");
                File.WriteAllText(file3, "Another example with the search string.");

                string searchString = "search string";
                var fileScanner = new FileScanner(testFolderPath, searchString);

                // Act
                using var stringWriter = new StringWriter();
                Console.SetOut(stringWriter);

                fileScanner.ScanFiles();

                string output = stringWriter.ToString().Trim();

                // Assert
                Assert.Contains("Present:testfile1.txt", output);
                Assert.Contains("Absent:testfile2.txt", output);
                Assert.Contains("Present:testfile3.txt", output);
            }
            finally
            {
                // Clean up test files and folder
                if (Directory.Exists(testFolderPath))
                    Directory.Delete(testFolderPath, true);
            }
        }
        [Fact]
        public void Constructor_ShouldThrowException_WhenFolderPathIsInvalid()
        {
            // Arrange
            string invalidFolderPath = "invalidPath";
            string searchString = "test";

            // Act & Assert
            var exception = Assert.Throws<DirectoryNotFoundException>(() =>
                new FileScanner(invalidFolderPath, searchString));

            Assert.Contains("does not exist", exception.Message);
        }
        [Fact]
        public void Constructor_ShouldThrowException_WhenSearchStringIsEmpty()
        {
            // Arrange
            string testFolderPath = Path.GetTempPath();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new FileScanner(testFolderPath, ""));

            Assert.Equal("Search string cannot be null or empty. (Parameter 'searchString')", exception.Message);
        }
        [Fact]
        public void ScanFiles_ShouldHandleEmptyFolderGracefully()
        {
            // Arrange
            string emptyFolderPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(emptyFolderPath);

            try
            {
                string searchString = "test";
                var fileScanner = new FileScanner(emptyFolderPath, searchString);

                // Act
                using var stringWriter = new StringWriter();
                Console.SetOut(stringWriter);

                fileScanner.ScanFiles();

                string output = stringWriter.ToString().Trim();

                // Assert
                Assert.Equal("No files found in the folder.", output);
            }
            finally
            {
                // Clean up test folder
                if (Directory.Exists(emptyFolderPath))
                    Directory.Delete(emptyFolderPath, true);
            }
        }
    }
}
