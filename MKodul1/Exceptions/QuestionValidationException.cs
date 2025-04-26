namespace MKodul1.Exceptions
{
    public class QuestionValidationException : Exception
    {
        public string Field { get; }
        public string Description { get; }

        public QuestionValidationException(string field, string description)
        {
            Field = field;
            Description = description;
        }
    }
}
