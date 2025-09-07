using System.ComponentModel.DataAnnotations;

namespace Tayseer_AspMVC.Models
{
    public class Disability
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Hospital>? Hospitals { get; set; }

    }
}
