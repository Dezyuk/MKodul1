namespace MKodul1.Exceptions
{
    public class InvalidQuizRequestException : Exception
    {
        public string Field { get; }
        public string Description { get; }

        public InvalidQuizRequestException(string field, string description)
        {
            Field = field;
            Description = description;
        }
    }
}
