using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tayseer_AspMVC.Models
{
    public class Centers
    {
        [Key]
        public int Id { get; set; }
        public string uid { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
        public string Gender { get; set; }
        public string Services { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }


        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }
    }
}
