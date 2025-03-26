using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECDL.Models
{
    [Table("vizsgazok")]
    public class VizsgazoModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nev")]
        [MaxLength(255)]
        public string? Nev { get; set; }

        [Required]
        [Column("vizsgatipusId")]
        public int VizsgatipusId { get; set; }

        [Required]
        [Column("eredmeny")]
        public int Eredmeny {  get; set; }

        public VizsgatipusModel? Vizsgatipus { get; set; }
    }
}
