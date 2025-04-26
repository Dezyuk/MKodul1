namespace MKodul1.Exceptions
{
    public class QuestionNotFoundException : Exception
    {
        public string Field { get; }
        public string Description { get; }

        public QuestionNotFoundException(string field, string description)
        {
            Field = field;
            Description = description;
        }
    }
}
