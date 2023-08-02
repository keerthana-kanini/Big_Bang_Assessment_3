using System.ComponentModel.DataAnnotations;

namespace Big_Bang3_Assessment.Model
{
    public class Booking
    {
        [Key]
        public int Booking_Id { get; set; }


        public string Customer_Date_Of_Booking { get; set; }

        public int booking_amount { get; set; }




        public User user { get; set; }

        public Agency agency { get; set; }

        
    }
}
