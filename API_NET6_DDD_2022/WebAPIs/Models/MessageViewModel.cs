using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPIs.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Active { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime ChangeDate { get; set; }

        public int UserId { get; set; }
    }
}
