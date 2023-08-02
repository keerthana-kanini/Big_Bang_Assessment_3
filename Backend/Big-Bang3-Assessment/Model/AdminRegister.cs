using System.ComponentModel.DataAnnotations;

namespace Big_Bang3_Assessment.Model
{
    public class AdminRegister
    {
        [Key]
        public int Admin_Id { get; set; }
        public string Admin_Name { get; set; }
        public string Admin_Password { get; set; }

        public ICollection<AdminPost> adminPosts { get; set; }

        public ICollection<Agency> agencies { get; set; }

    }
}
