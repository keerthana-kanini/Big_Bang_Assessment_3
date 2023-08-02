using System.ComponentModel.DataAnnotations;

namespace Big_Bang3_Assessment.Model
{
    public class AdminPost
    {
        [Key]
        public int id { get; set; }

        public string place_name { get; set; }

        public string? PlaceImagePath { get; set; }

        public AdminRegister adminRegister { get; set; }

        public ICollection<Agency> agencies { get; set; }
    }
}
