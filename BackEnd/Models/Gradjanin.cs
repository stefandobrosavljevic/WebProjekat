using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Models
{
    [Table("Gradjanin")]
    public class Gradjanin
    {
        [Key]
        [Column("JMBG")]
        public string JMBG { get; set; }

        [Column("Ime")]
        [MaxLength(255)]
        public string Ime { get; set; }

        [Column("Prezime")]
        [MaxLength(255)]
        public string Prezime { get; set; }

        [Column("BrojTelefona")]
        [MaxLength(15)]
        public string BrojTelefona { get; set; }

        [JsonIgnore]
        public virtual Vakcina Vakcina { get; set; }

        [JsonIgnore]
        public Ambulanta Ambulanta { get; set; }
    }
}


