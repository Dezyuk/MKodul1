namespace MKodul1.Exceptions
{
    public class CategoryNotFoundException : Exception
    {
        public string Field { get; }
        public string Description { get; }

        public CategoryNotFoundException(string field, string description)
        {
            Field = field;
            Description = description;
        }
    }
}
