using CodeChallenge;

internal class Program
{
    private static void Main(string[] args)
    {
        // FileScanner End to End Test
        string folderPath = "<folder-path>";

        string searchString = "<search-string>";

        try
        {
            FileScanner scanner = new(folderPath, searchString);
            scanner.ScanFiles();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        // DuplicateFinder End to End Test
        try
        {
            List<int> collectionA = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20];
            List<int> collectionS = [5, 15, 3, 19, 35, 50, -1, 0];

            DuplicateFinder<int> duplicateIdentifier = new(collectionA, collectionS);
            duplicateIdentifier.FindDuplicates();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
