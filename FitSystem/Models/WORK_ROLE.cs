using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitSystem.Models
{
    public class WorkRole
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleId { get; set; } //  Permission ID - Taken to Permission Granting
        [Required]
        public string  RoleName { get; set; }
        public string Description { get; set; }


    }
}
