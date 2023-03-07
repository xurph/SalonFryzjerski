using System.ComponentModel.DataAnnotations;

namespace SalonFryzjerski.Data
{
    public class Wizyta
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int RodzajId { get; set; }
        public string UserId { get; set; }
        [Range(0,5)]
        public int Ocena { get; set; }

        public Rodzaj? Rodzaj { get; set; }
    }
}
