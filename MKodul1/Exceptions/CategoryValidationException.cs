namespace MKodul1.Exceptions
{
    public class CategoryValidationException : Exception
    {
        public string Field { get; }
        public string Description { get; }

        public CategoryValidationException(string field, string description)
        {
            Field = field;
            Description = description;
        }
    }
}
