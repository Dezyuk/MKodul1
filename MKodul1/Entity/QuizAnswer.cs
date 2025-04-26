using System.ComponentModel.DataAnnotations.Schema;

namespace MKodul1.Entity
{
    [NotMapped]
    public class QuizAnswer
    {
        public Guid QuestionId { get; set; }
        public string Title { get; set; }
        public List<string> Answers { get; set; } = new();
    }
}
