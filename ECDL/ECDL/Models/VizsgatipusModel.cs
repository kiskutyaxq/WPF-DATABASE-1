using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECDL.Models
{
    [Table("vizsgatipus")]
    public class VizsgatipusModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column("modul")]
        public string? Modul { get; set; }
    }
}
