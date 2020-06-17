using System.ComponentModel.DataAnnotations;

namespace CartApi.Models
{
    public class User
    {
        [Key] public string ID { get; set; }

        public string username { get; set; }
        public string password { get; set; }

        public string role { get; set; }
    }
}