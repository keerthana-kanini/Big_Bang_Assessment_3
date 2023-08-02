using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Big_Bang3_Assessment.Model
{
    public class Agency
    {
        [Key]
        public int Agency_Id { get; set; }
        public string Agency_Name { get; set; }
        public string Agency_Contact { get; set; }
        public string Agency_Rating { get; set; }


        public int Number_Of_Days { get; set; }
        public int rate_for_day { get; set; }

        
        public int Offer_For_Day { get; set; }

        public string tour_place { get; set; }
     
        public string? TourImagePath { get; set; }
       

       
        public AgentRegister agentRegister { get; set; }

        public AdminPost adminPost { get; set; }

        public ICollection<Booking> bookings { get; set; }

        public ICollection<AccommodationDetail> accommodationDetails { get; set; }

        public ICollection<FeedBack> feedBacks { get; set; }




    }

   
}
