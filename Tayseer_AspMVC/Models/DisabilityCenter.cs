namespace Tayseer_AspMVC.Models
{
    public class DisabilityCenter
    {
        public int Id { get; set; }

        public int CentersId { get; set; }
        public Centers? Centers { get; set; }

        public int DisabilityId { get; set; }
        public Disability? Disability { get; set; }

    }
}
