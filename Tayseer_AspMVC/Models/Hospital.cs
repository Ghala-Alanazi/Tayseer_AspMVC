using System.ComponentModel.DataAnnotations.Schema;

namespace Tayseer_AspMVC.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Services { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }
    }
}
