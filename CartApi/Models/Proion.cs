using System.ComponentModel.DataAnnotations;

namespace CartApi.Models
{
    public class Proion
    {
        [Key] public string ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Qantity { get; set; }
        public string Price { get; set; }
    }
}