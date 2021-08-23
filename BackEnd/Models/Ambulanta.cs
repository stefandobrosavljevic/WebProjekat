using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    [Table("Ambulanta")]
    public class Ambulanta
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Ime")]
        [MaxLength(255)]
        public string Ime { get; set; }

        [Column("Grad")]
        [MaxLength(255)]
        public string Grad { get; set; }

        [Column("Adresa")]
        [MaxLength(255)]
        public string Adresa { get; set; }

        public int BrojPunktova { get; set; }

        
        public virtual List<Vakcina> Vakcine { get; set; }

        public virtual List<Gradjanin> Gradjani { get; set; }
    }
}

