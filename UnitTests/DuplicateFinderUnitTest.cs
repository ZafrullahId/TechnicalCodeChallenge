using CodeChallenge;

namespace UnitTests
{
    public class DuplicateFinderUnitTest
    {
        [Fact]
        public void FindDuplicates_ShouldCorrectlyFindMatchingAndNonMatchingElements()
        {
            // Arrange
            var collectionA = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            var collectionS = new List<int> { 5, 15, 3, 19, 35, 50, -1, 0 };
            var duplicateIdentifier = new DuplicateFinder<int>(collectionA, collectionS);

            // Act & Assert
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            duplicateIdentifier.FindDuplicates();

            string output = stringWriter.ToString().Trim();

            Assert.Contains("5:true", output);
            Assert.Contains("15:true", output);
            Assert.Contains("3:true", output);
            Assert.Contains("19:true", output);
            Assert.Contains("35:false", output);
            Assert.Contains("50:false", output);
            Assert.Contains("-1:false", output);
            Assert.Contains("0:false", output);
        }
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenCollectionAIsNull()
        {
            // Arrange
            List<int> collectionA = null;
            var collectionS = new List<int> { 5, 15, 3 };

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new DuplicateFinder<int>(collectionA, collectionS));
            Assert.Equal("Collection A cannot be null. (Parameter 'collectionA')", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenCollectionSIsNull()
        {
            // Arrange
            var collectionA = new List<int> { 1, 2, 3 };
            List<int> collectionS = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new DuplicateFinder<int>(collectionA, collectionS));
            Assert.Equal("Collection S cannot be null. (Parameter 'collectionS')", exception.Message);
        }
        [Fact]
        public void FindDuplicates_ShouldHandleEmptyCollectionA()
        {
            // Arrange
            var collectionA = new List<int>();
            var collectionS = new List<int> { 5, 15, 3 };
            var duplicateIdentifier = new DuplicateFinder<int>(collectionA, collectionS);

            // Act and Assert
            using var stringWriter = new System.IO.StringWriter();
            Console.SetOut(stringWriter);

            duplicateIdentifier.FindDuplicates();

            string output = stringWriter.ToString().Trim();
            Assert.Contains("5:false", output);
            Assert.Contains("15:false", output);
            Assert.Contains("3:false", output);
        }
        [Fact]
        public void FindDuplicates_ShouldHandleEmptyCollectionS()
        {
            // Arrange
            var collectionA = new List<int> { 1, 2, 3, 4, 5 };
            var collectionS = new List<int>();
            var duplicateIdentifier = new DuplicateFinder<int>(collectionA, collectionS);

            // Act & Assert
            using var stringWriter = new System.IO.StringWriter();
            Console.SetOut(stringWriter);

            duplicateIdentifier.FindDuplicates();

            string output = stringWriter.ToString().Trim();
            Assert.Equal(string.Empty, output);
        }
        [Fact]
        public void FindDuplicates_ShouldHandleOtherGenericGenericType()
        {
            // Arrange
            var collectionA = new List<string> { "apple", "banana", "cherry" };
            var collectionS = new List<string> { "apple", "banana", "pear", "peach" };
            var duplicateIdentifier = new DuplicateFinder<string>(collectionA, collectionS);

            // Act & Assert
            using var stringWriter = new System.IO.StringWriter();
            Console.SetOut(stringWriter);

            duplicateIdentifier.FindDuplicates();

            string output = stringWriter.ToString().Trim();

            Assert.Contains("apple:true", output);
            Assert.Contains("banana:true", output);
            Assert.Contains("pear:false", output);
            Assert.Contains("peach:false", output);
        }
    }
}
