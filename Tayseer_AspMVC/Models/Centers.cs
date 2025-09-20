using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tayseer_AspMVC.Models
{
    public class Centers
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Gender { get; set; }
        public string Services { get; set; }


        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }
    }
}
