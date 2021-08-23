using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Models
{
    [Table("Vakcina")]
    public class Vakcina
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; } 

        [Column("ImeVakcine")]
        [MaxLength(255)]
        public string ImeVakcine { get; set; }

        [Column("Kolicina")]
        public int Kolicina { get; set; }

        [Column("BrojVakcinisanih")]
        public int BrojVakcinisanih { get; set; }


        [JsonIgnore]
        public Ambulanta Ambulanta { get; set; }


        public bool VakcinisiGradjanina()
        {
            if(this.Kolicina < 1){
                return false;
            }

            this.Kolicina--;
            this.BrojVakcinisanih++;
            return true;
        }

    }
}

