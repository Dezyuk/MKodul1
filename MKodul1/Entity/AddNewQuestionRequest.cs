using System.ComponentModel.DataAnnotations.Schema;

namespace MKodul1.Entity
{
    [NotMapped]
    public class AddNewQuestionRequest
    {
        public string Title { get; set; }
        public string Answer { get; set; }
        public List<Guid> Categories { get; set; }
    }
}
