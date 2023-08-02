using System.ComponentModel.DataAnnotations;

namespace Big_Bang3_Assessment.Model
{
    public class FeedBack
    {
        [Key]
        public int FeedBack_id { get; set; }

        public string FeedBack_area { get; set; }

        public int FeedBack_rating { get; set; }

        public User user { get; set; }
        public Agency agency { get; set; }
    }
}
