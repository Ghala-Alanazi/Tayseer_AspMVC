namespace Tayseer_AspMVC.Models
{
    public class DisabilitySchool
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public School? School { get; set; }
        public int DisabilityId { get; set; }
        public Disability? Disability { get; set; }
    }
}
