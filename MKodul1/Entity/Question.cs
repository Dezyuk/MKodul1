namespace MKodul1.Entity
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }

        public List<Category> Categories { get; set; } = new();
    }
}
