using Big_Bang3_Assessment.Model;

namespace Big_Bang3_Assessment.Dto
{
    public class AdminDto
    {
        public string Admin_Name { get; set; }
        public string Admin_Password { get; set; }

        public ICollection<AdminPost> adminPosts { get; set; }

        public ICollection<Agency> agencies { get; set; }
    }
}
