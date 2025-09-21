using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tayseer_AspMVC.Models
{
    public class School
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Gender { get; set; }

        public string Stages { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }
    }
}
