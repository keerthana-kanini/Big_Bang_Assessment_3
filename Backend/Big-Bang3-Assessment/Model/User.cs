using System.ComponentModel.DataAnnotations;

namespace Big_Bang3_Assessment.Model
{
    public class User
    {

        [Key]
        public int User_Id { get; set; }

        public string User_Name { get; set; }

        public string User_Email { get; set; }

        public string User_Phone { get; set; }

        public string User_Gender { get; set; }
        public string User_Location { get; set; }

        public string User_Password { get; set; }

        public ICollection<Booking> bookings { get; set; }
        public ICollection<FeedBack> feedBacks { get; set; }
    }
}
