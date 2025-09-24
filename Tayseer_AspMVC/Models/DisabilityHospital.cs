namespace Tayseer_AspMVC.Models
{
    public class DisabilityHospital
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

        public int DisabilityId { get; set; }
        public Disability? Disability { get; set; }
    }
}
