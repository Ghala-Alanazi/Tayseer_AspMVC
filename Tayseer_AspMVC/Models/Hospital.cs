namespace Tayseer_AspMVC.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Services { get; set; }
        public int DisabilityId { get; set; }
        public virtual Disability? Disability { get; set; }

    }
}
