using System.ComponentModel.DataAnnotations;

namespace SalonFryzjerski.Data
{
    public class Rodzaj
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        [Range(0,double.MaxValue)]
        public decimal Cena { get; set; }

        public ICollection<Wizyta>? Wizyty { get; set; }

        
    }
}
