using System.ComponentModel.DataAnnotations;

namespace CartApi.Models
{
    public class Roles
    {
        [Key] public string ID { get; set; }

        public string username { get; set; }
        public string role { get; set; }
    }
}