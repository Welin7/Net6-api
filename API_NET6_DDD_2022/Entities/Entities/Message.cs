using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("tbl_Message")]
    public class Message : Notifications
    {
        [Column("Msg_ID")]
        public int Id { get; set; }

        [Column("Msg_Title")]
        [MaxLength(255)]
        public string Title { get; set; }

        [Column("Msg_Active")]
        public bool Active { get; set; }

        [Column("Msg_Registration_Date")]
        public DateTime RegistrationDate { get; set; }

        [Column("Msg_Change_Date")]
        public DateTime ChangeDate { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
