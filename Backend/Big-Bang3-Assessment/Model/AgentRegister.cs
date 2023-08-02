using System.ComponentModel.DataAnnotations;

namespace Big_Bang3_Assessment.Model
{
    public class AgentRegister
    {

        [Key]
        public int Agent_Id { get; set; }
        public string Agent_Name { get; set; }

        public string Agent_Password { get; set; }

        public string status { get; set; } = "pending";



        public ICollection<Agency> agencies { get; set; }

        public AdminRegister AdminRegister { get; set; }
    }
}
