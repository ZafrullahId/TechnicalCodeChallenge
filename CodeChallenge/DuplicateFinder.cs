namespace CodeChallenge
{
    public class DuplicateFinder<T>(List<T> collectionA, List<T> collectionS) where T : IEquatable<T>
    {
        private readonly List<T> _collectionA = collectionA ??
                throw new ArgumentNullException(nameof(collectionA), "Collection A cannot be null.");
        private readonly List<T> _collectionS = collectionS ??
                throw new ArgumentNullException(nameof(collectionS), "Collection S cannot be null.");

        public void FindDuplicates()
        {
            HashSet<T> setA = new(_collectionA);

            foreach (T element in _collectionS)
            {
                bool isPresent = setA.Contains(element);
                Console.WriteLine($"{element}:{isPresent.ToString().ToLower()}");
            }
        }
    }

}
