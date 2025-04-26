namespace MKodul1.Exceptions
{
    public class CategoryException : Exception
    {
        public string Field { get; }
        public string Description { get; }

        public CategoryException(string field, string description)
        {
            Field = field;
            Description = description;
        }
    }
}
