using System.ComponentModel.DataAnnotations.Schema;

namespace MKodul1.Entity
{
    [NotMapped]
    public class QuizRequest
    {
        public Guid CategoryId { get; set; }
        public int Count { get; set; }
    }
}
