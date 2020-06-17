using System.ComponentModel.DataAnnotations;

namespace CartApi.Models
{
    public class Card
    {
        [Key] public string ID { get; set; }

        public string name1 { get; set; }
        public string quantity1 { get; set; }
        public string name2 { get; set; }
        public string quantity2 { get; set; }
        public string name3 { get; set; }
        public string quantity3 { get; set; }
    }
}