using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

namespace Big_Bang3_Assessment.Model
{
    public class AccommodationDetail
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int AccommodationDetailId { get; set; }

        public string Hotel_Name { get; set; }

        public string? HotelImagePath { get; set; }
        public string Food { get; set; }
        public string Place { get; set; }

        public string? PlaceImagePath { get; set; }

       public Agency agency { get; set; }
    }
}
